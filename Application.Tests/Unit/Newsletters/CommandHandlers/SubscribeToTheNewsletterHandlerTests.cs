using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Discounts.Repositories;
using Application.Mailing;
using Application.Newsletters.CommandHandlers;
using Application.Newsletters.Commands;
using Application.Newsletters.Repositories;
using Domain.Discounts;
using Domain.Newsletters;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Unit.Newsletters.CommandHandlers
{
    public class SubscribeToTheNewsletterHandlerTests
    {
        private IDiscountCodeGenerator _discountCodeGenerator;
        private Mock<IDiscountRepository> _discountRepositoryMock;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IIdentityProvider> _identityProviderMock;
        private Mock<INewsletterRepository> _newsletterRepositoryMock;

        private SubscribeToTheNewsletterHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _identityProviderMock = new Mock<IIdentityProvider>();
            _identityProviderMock.Setup(x => x.Next())
                .Returns(Guid.NewGuid());
            _newsletterRepositoryMock = new Mock<INewsletterRepository>();
            _discountCodeGenerator = new DiscountCodeGenerator();
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _sut = new SubscribeToTheNewsletterHandler(
                _emailServiceMock.Object,
                _identityProviderMock.Object,
                _newsletterRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _discountCodeGenerator);
        }

        [Test]
        public async Task Given_SubscribeToTheNewsletterWithNotEmptyEmail_When_HandleAsync_Then_SubscriberIsAddedToRepositoryAndDiscountCodeIsAddedToRepositoryAndEmailIsSent()
        {
            //Arrange
            var command = new SubscribeToTheNewsletter
            {
                Id = Guid.NewGuid(),
                Email = "marcin@marcinlovescode.com"
            };
            var emailSent = false;
            Subscriber createdSubscriber = null;
            Discount createdDiscount = null;

            _emailServiceMock.Setup(x => x.SendEmailWithDiscountCode(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Callback<string, CancellationToken>((discount, cancellationToken) => { emailSent = true; });
            _discountRepositoryMock.Setup(x => x.Add(It.IsAny<Discount>(), It.IsAny<CancellationToken>()))
                .Callback<Discount, CancellationToken>((discount, cancellationToken) => { createdDiscount = discount; });
            _newsletterRepositoryMock.Setup(x => x.Add(It.IsAny<Subscriber>(), It.IsAny<CancellationToken>()))
                .Callback<Subscriber, CancellationToken>((subscriber, cancellationToken) => { createdSubscriber = subscriber; });

            //Act
            await _sut.HandleAsync(command);

            //Assert
            Assert.That(emailSent, Is.True);
            Assert.That(createdDiscount, Is.Not.Null);
            Assert.That(createdSubscriber.Email, Is.EqualTo(command.Email));
        }
    }
}