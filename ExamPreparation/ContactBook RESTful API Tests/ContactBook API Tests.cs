
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ContactBook_RESTful_API_Tests
{
    public class Tests
    {
        private const string url = "https://contactbook.nakov.repl.co/api/contacts";
        RestClient client;
        RestRequest request;
        [SetUp]
        public void Setup()
        {
            client = new RestClient();
        }

        [Test]
        public void ContactBookTest_GetAllContacts_CheckFirstContact()
        {
            request = new RestRequest(url, Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(contacts[0].id, Is.EqualTo(1));
            Assert.That(contacts[0].firstName, Is.EqualTo("Steve"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Jobs"));

        }
        [Test]
        public void ContactBookTest_SearchValidContact_CheckFirstContact()
        {
            request = new RestRequest(url + "/search/albert", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(contacts[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Einstein"));

        }
        [Test]
        public void ContactBookTest_SearchInvalidContact()
        {
            request = new RestRequest(url + "/search/invalidcontact", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(contacts.Count, Is.EqualTo(0));

        }
        [Test]
        public void ContactBookTest_CreateNewInvalidContact()
        {
            request = new RestRequest(url, Method.Post);
            request.AddJsonBody(new
            {
                firstName = "invalidName",
                email = "asda@abv.bgg",
                phone = "123123123123",
            });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Last name cannot be empty!\"}"));
        }
        [Test]
        public void ContactBookTest_CreateNewValidContact_CheckNewContact()
        {
            request = new RestRequest(url);
            var newContact = (new
            {
                firstName = "Bai" + DateTime.Now.Ticks,
                lastName = "ivan" + DateTime.Now.Ticks,
                email = DateTime.Now.Ticks + "baiivan@abv.bg",
                phone = +DateTime.Now.Ticks +"08888745",
            }); ;
            request.AddJsonBody(newContact);
            var response = client.Execute(request,Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var allContact = client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(allContact.Content).ToList();
            var contact = contacts.Last();
            Assert.That(contact.firstName, Is.EqualTo(newContact.firstName));
            Assert.That(contact.lastName, Is.EqualTo(newContact.lastName));

        }
    }
}