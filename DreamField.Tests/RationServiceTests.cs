using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.Model;

namespace DreamField.Tests
{
    [TestClass]
    public class RationServiceTests
    {


        [TestMethod]
        public void TestCreateRation()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.UserRepository.GetById(1)).Returns(new User());
        }
    }
}
