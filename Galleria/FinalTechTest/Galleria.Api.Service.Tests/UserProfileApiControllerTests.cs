using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Galleria.Api.Service.Tests
{
    /// <summary>
    /// A class that tests the methods offered by the <see cref="UserProfileApiController"/> class.
    /// </summary>
    [TestClass]
    public sealed class UserProfileApiControllerTests
    {
        private UserProfileApiController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new UserProfileApiController();
            _controller.ControllerContext = new HttpControllerContext()
            {
                Configuration = new System.Web.Http.HttpConfiguration()
            };
        }

        [TestMethod]
        public void GetUsers_ValidRequest_StatusCode200()
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:55555/api/users"));

            var responseTask = _controller.GetUsers().ExecuteAsync(new System.Threading.CancellationToken());
            responseTask.Wait();

            var response = responseTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void GetUser_ExistingUserId_StatusCode200()
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:55555/api/users/1"));

            var responseTask = _controller.GetUser(userId: 1).ExecuteAsync(new System.Threading.CancellationToken());
            responseTask.Wait();

            var response = responseTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void GetUser_NonExistentUserId_StatusCode404()
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:55555/api/users/99999"));

            var responseTask = _controller.GetUser(userId: 99999).ExecuteAsync(new System.Threading.CancellationToken());
            responseTask.Wait();

            var response = responseTask.Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}