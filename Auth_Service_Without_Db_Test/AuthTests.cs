using Auth_Service_Without_DB.Controllers;
using Auth_Service_Without_DB.Models;
using Auth_Service_Without_DB.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Auth_Service_Without_Db_Test
{
    public class Tests
    {
        
        
        Mock<IConfiguration> config;
        [SetUp]
        public void Setup()
        {
            
            config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
        }

        [Test]
        public void Login_Success_Test()
        {
            Mock<IAuthRepo> mock = new Mock<IAuthRepo>();
            User user = new User() { UserId = 4, Username = "Suresh", Password = "Deep@40"};
            mock.Setup(x => x.Login(user)).Returns(user);            
            var controller = new AuthController(config.Object, mock.Object);
            var res = controller.Login(user) as OkObjectResult;
            Assert.AreEqual(200, res.StatusCode);
            
            
        }
        [Test]
        public void Login_Failure_Test()
        {
            Mock<IAuthRepo> mock = new Mock<IAuthRepo>();
            mock.Setup(x => x.Login(It.IsAny<User>())).Returns((User)null);
            var controller = new AuthController(config.Object, mock.Object);
            var res = controller.Login(new User() { UserId = 4, Username = "Suresh", Password = "Deep@40" }) as StatusCodeResult;
            Assert.AreEqual(404, res.StatusCode);


        }
    }
}