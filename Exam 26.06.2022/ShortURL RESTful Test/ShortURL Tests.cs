using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace ShortURL_RESTful_Test
{

    public class Tests
    {
        private const string url = "https://shorturl.nakov.repl.co/api";
        RestClient client;
        RestRequest request;
        [SetUp]
        public void Setup()
        {
            client = new RestClient();
        }

        [Test]
        public void TestShortURL_ListShortURLs()
        {    //Arr
            this.request = new RestRequest(url + "/urls");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var shortURLs = JsonSerializer.Deserialize<List<URLs>>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(shortURLs.Count, Is.GreaterThan(0));
           
        }
        [Test]
        public void TestShortURL_FindURL_By_ShortCode_ValidData()
        {
            //Arr
            this.request = new RestRequest(url + "/urls/nak");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var searchCode = JsonSerializer.Deserialize<URLs>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(searchCode.url, Is.EqualTo("https://nakov.com"));
            Assert.That(searchCode.shortCode, Is.EqualTo("nak"));
           
        }
        [Test]
        public void TestShortURL_FindURL_By_ShortCode_InvalidData()
        {
            //Arr
            this.request = new RestRequest(url + "/urls/QAcode");

            // Act
            var response = this.client.Execute(request, Method.Get);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Short code not found: QAcode\"}"));
            
        }
        [Test]
        public void TestShortURL_CreateNewShortUrl_ValidData()
        {
            //Arr
            this.request = new RestRequest(url + "/urls");
            var body = new
            {
                 url = "https://qaz.com", 
                shortCode = "qaz"
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }
        [Test]
        public void TestShortURL_Delete_newURLsCode()
        {
            // Arrange
            this.request = new RestRequest(url + "/urls/qaz");
            // Act
            var response = this.client.Execute(request, Method.Delete);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"msg\":\"URL deleted: qaz\"}"));
        }
    }
}
