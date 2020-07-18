using System;
using Domain.Discounts;
using NUnit.Framework;

namespace Application.Tests.Unit.Discounts
{
    public class DiscountCodeGeneratorTests
    {
        [Test]
        public void When_Make_Then_ReturnsNotEmptyCode()
        {
            //Arrange
            var codeGenerator = new DiscountCodeGenerator();
            //Act
            var discount = codeGenerator.GenerateCodeForSubscriber(Guid.NewGuid(), Guid.NewGuid());
            //Assert
            Assert.That(string.IsNullOrWhiteSpace(discount.Code), Is.False);
        }

        [Test]
        public void When_Make_Then_ReturnsUniqueDiscountCode()
        {
            //Arrange
            var codeGenerator = new DiscountCodeGenerator();
            //Act
            var discount1 = codeGenerator.GenerateCodeForSubscriber(Guid.NewGuid(), Guid.NewGuid());
            var discount2 = codeGenerator.GenerateCodeForSubscriber(Guid.NewGuid(), Guid.NewGuid());
            //Assert
            Assert.That(discount1.Code, Is.Not.EquivalentTo(discount2.Code));
        }
    }
}