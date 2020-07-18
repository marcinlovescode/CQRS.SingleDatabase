using System;
using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class GuidBasedIdentityProviderTests
    {
        [Test]
        public void When_Next_Then_ReturnsNotDefaultGuid()
        {
            //Arrange
            var guidBasedIdentityProvider = new GuidBasedIdentityProvider();
            //Act
            var guid = guidBasedIdentityProvider.Next();
            //Assert
            Assert.That(guid, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void When_Next_Then_ReturnsUniqueGuid()
        {
            //Arrange
            var guidBasedIdentityProvider = new GuidBasedIdentityProvider();
            //Act
            var guid1 = guidBasedIdentityProvider.Next();
            var guid2 = guidBasedIdentityProvider.Next();
            //Assert
            Assert.That(guid1, Is.Not.EqualTo(guid2));
        }
    }
}