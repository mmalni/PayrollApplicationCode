using NUnit.Framework;
using Payroll.Model;
using Payroll.Service;
using FluentAssertions;
using Payroll.Common;

namespace PayrollApplicationTest
{
    public class PayCalculatorServiceTest
    {
        private PayCalculatorService payCalculatorService;

        [SetUp]
        public void Setup()
        {
            payCalculatorService = new PayCalculatorService(new TaxRateService());

        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 60050,
                SuperRate = 0.09M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Salary actual = this.payCalculatorService.CalculateMonthlyPay(employee);

            // Assert
            Salary expected = new Salary
            {
                EmployeeName = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 5004,
                IncomeTax = 922,
                NetIncome = 4082,
                Super = 450
            };
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIs0_SuperRateIs0()
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 0,
                SuperRate = 0M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Salary actual = this.payCalculatorService.CalculateMonthlyPay(employee);

            // Assert
            Salary expected = new Salary
            {
                EmployeeName = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 0,
                IncomeTax = 0,
                NetIncome = 0,
                Super = 0
            };

            expected.Should().BeEquivalentTo(actual);
        }
        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIs0AndSuperRateIs50()
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 0,
                SuperRate = 0.50M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Salary actual = this.payCalculatorService.CalculateMonthlyPay(employee);

            // Assert
            Salary expected = new Salary
            {
                EmployeeName = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 0,
                IncomeTax = 0,
                NetIncome = 0,
                Super = 0
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIsIntMaxAndSuperRateIs50()
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 0.5M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Salary actual = this.payCalculatorService.CalculateMonthlyPay(employee);

            // Assert
            Salary expected = new Salary 
            {
                EmployeeName = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 178956970,
                IncomeTax = 80528432,
                NetIncome = 98428538,
                Super = 89478485
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(-0.1)]
        [TestCase(0.51)]
        public void CalculatePayslip_ShouldThrowServiceException_WhenAnnualSalaryIsIntMaxAndSuperRateIsGreaterThan50OrLessThan0(decimal superRate)
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = -1,
                SuperRate = superRate,
                PaymentStartDate = "01 March – 31 March",
            };
            Assert.Throws<PayCalculatorServiceException>(() => this.payCalculatorService.CalculateMonthlyPay(employee));
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(0.36)]
        [TestCase(0.49)]
        public void CalculatePayslip_ShouldNOTThrowServiceException_WhenAnnualSalaryIsIntMaxAndSuperRateIsGreaterThan50OrLessThan0(decimal superRate)
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = superRate,
                PaymentStartDate = "01 March – 31 March",
            };

            Assert.DoesNotThrow(() => this.payCalculatorService.CalculateMonthlyPay(employee));
        }


        [Test]
        public void CalculatePayslip_ShouldThrowOverflowException_WhenAnnualSalaryIsNegative()
        {
            // Arrange
            Employee employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = -1,
                SuperRate = 0,
                PaymentStartDate = "01 March – 31 March",
            };

            Assert.Throws<PayCalculatorServiceException>(() => this.payCalculatorService.CalculateMonthlyPay(employee));

        }

    }
}