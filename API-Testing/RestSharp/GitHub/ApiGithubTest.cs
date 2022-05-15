using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nunit_API_tests
{
    public class ApiGithubTest
    {
        private RestClient client;
        private RestRequest request;
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://api.github.com");
            request = new RestRequest("/repos/USE YOUR USERNAME/QA-Automation/issues");
            client.Authenticator = new HttpBasicAuthenticator("USE YOUR USERNAME", "USE YOUR TOKEN HERE");
        }
        private async Task<Issue> CreateIssue(string title, string body)
        {
            request.AddBody(new { title, body });
            var response = await client.ExecuteAsync(request, Method.Post);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;

        }
        private async Task<Issue> EditIssue(string title)
        {
            request.AddBody(new { title });
            var response = await client.ExecuteAsync(request, Method.Patch);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;
        }
        [Test]
        public async Task APIRequestStatusCode()
        {
            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task APIRequestGetAllIssues()
        {
            var response = await client.ExecuteAsync(request);
            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);
            Assert.That(issues.Count > 1);
            foreach (var item in issues)
            {
                Assert.Greater(item.id, 0);
                Assert.Greater(item.number, 0);
                Assert.IsNotEmpty(item.title);
            }
        }
        [Test]
        public async Task APIRequestGetIssueByNumber()
        {
            request = new RestRequest("/repos/USE YOUR USERNAME/USE YOUR REPOSITORY/issues/10");
            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            Assert.That(issue.id > 0);
            Assert.AreEqual(issue.number, 10);

        }
        [Test]
        public async Task APIRequestCreateIssue()
        {
            string title = "RestSharp Issue";
            string body = "this is issue body";
            var issue = await CreateIssue(title, body);
            Assert.That(issue.id > 0);
            Assert.That(issue.number > 0);
            Assert.IsNotEmpty(issue.title);
        }
        [Test]
        public async Task APIRequestEditExistIssue()
        {
            string newTitle = "Edited from RestSharp";
            request = new RestRequest("/repos/USE YOUR USERNAME/USE YOUR REPOSITORY/issues/40");
            var issue = await EditIssue(newTitle);
            Assert.AreEqual(newTitle, issue.title);
        }
        [Test]
        public async Task APIRequestRetriveAllCommentsFromIssue()
        {
            request = new RestRequest("/repos/USE YOUR USERNAME/USE YOUR REPOSITORY/issues/13/comments");
            var response = await client.ExecuteAsync(request);
            var issueComments = JsonSerializer.Deserialize<List<Issue>>(response.Content);
            Assert.Greater(issueComments.Count, 0);
            foreach (var comment in issueComments)
            {
                Assert.Greater(comment.id,0);
                Assert.IsNotEmpty(comment.body);
            }
        }

        [Test]
        public async Task APIRequestRetriveLabelsFromIssue()
        {
            request = new RestRequest("/repos/USE YOUR USERNAME/USE YOUR REPOSITORY/issues/12/labels");
            var respone = await client.ExecuteAsync(request);
            var issue = JsonSerializer.Deserialize<List<Issue>>(respone.Content);
            Assert.Greater(issue.Count, 0);
            foreach (var item in issue)
            {
                Assert.That(item.name,Is.Not.Null);
            }

        }
        
    }
}
