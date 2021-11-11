using NUnit.Framework;
using Payroll.Model;
using Payroll.Service;
using FluentAssertions;
using Payroll.Common;

namespace PayrollApplicationTest
{
    public class TaxRateServiceTest
    {
        private TaxRateService taxRateService;

        [SetUp]
        public void Setup()
        {
            taxRateService = new TaxRateService();

        }

        [Test]
        [TestCase(1)]
        [TestCase(10000)]
        [TestCase(18200)]
        public void CalculateTaxRule_ShouldReturn0_WhenIncomeIsLessThan18201(int annualSalary)
        {
            // Act
            int actual = this.taxRateService.CalculateTaxRate(annualSalary);

            // Assert
            int expected = 0;

            expected.Should().Equals(actual);
        }

        [Test]
        [TestCase(18201, 0)]
        [TestCase(25000, 108)]
        [TestCase(37000, 298)]
        public void CalculateTaxRule_ShouldReturn0PlusTaxRate_WhenIncomeIsBetween18201And37000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRateService.CalculateTaxRate(annualSalary);

            // Assert
            actual.Equals(expected);
        }

        [Test]
        [TestCase(37001, 298)]
        [TestCase(60000, 921)]
        [TestCase(80000, 1462)]
        public void CalculateTaxRule_ShouldReturn3572PlusTaxRate_WhenIncomeIsBetween37001And80000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRateService.CalculateTaxRate(annualSalary);

            // Assert
            actual.Equals(expected);
        }

        [Test]
        [TestCase(80001, 1462)]
        [TestCase(120000, 2696)]
        [TestCase(180000, 4546)]
        public void CalculateTaxRule_ShouldReturn17547PlusTaxRate_WhenIncomeIsBetween80001And180000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRateService.CalculateTaxRate(annualSalary);

            // Assert
            actual.Equals(expected);
        }

        [Test]
        [TestCase(180001, 4546)]
        [TestCase(330000, 10171)]
        [TestCase(int.MaxValue, 80528432)]
        public void CalculateTaxRule_ShouldReturn54547PlusTaxRate_WhenIncomeIsBetween180001AndIntMax(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRateService.CalculateTaxRate(annualSalary);

            // Assert
            actual.Equals(expected);
        }
    }
}