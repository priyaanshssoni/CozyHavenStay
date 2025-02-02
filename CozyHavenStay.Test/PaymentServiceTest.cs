﻿
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;

namespace CozyHavenStay.Test
{
    [TestFixture]
    public class PaymentServiceTest
    {
        private Mock<IRepository<int, Payment>> _mockRepository;
        private Mock<IUserService> _mockUserService;
        private IPaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<int, Payment>>();
  
            _mockUserService = new Mock<IUserService>();
            _paymentService = new PaymentService(_mockRepository.Object, _mockUserService.Object);
        }

        [Test]
        public async Task AddPayment_ValidPaymentDTO_ReturnsAddedPayment()
        {
            // Arrange
            var paymentDto = new PaymentDTO(); 
            var addedPayment = new Payment(); 
            _mockRepository.Setup(repo => repo.Add(It.IsAny<Payment>())).ReturnsAsync(addedPayment); 

            // Act
            var result = await _paymentService.AddPayment(paymentDto);

            // Assert
            Assert.That(result != null);
        }

        [Test]
        public async Task DeletePayment_ExistingPaymentId_ReturnsDeletedPayment()
        {
            // Arrange
            var paymentId = 1;  
            var paymentToDelete = new Payment();  
            _mockRepository.Setup(repo => repo.Delete(paymentId)).ReturnsAsync(paymentToDelete);  

            // Act
            var result = await _paymentService.DeletePayment(paymentId);

            // Assert
            Assert.That(result != null);  
        }

        [Test]
        public async Task GetPayment_ExistingPaymentId_ReturnsPayment()
        {
            // Arrange
            var paymentId = 1;  
            var payment = new Payment(); 
            _mockRepository.Setup(repo => repo.GetById(paymentId)).ReturnsAsync(payment);  

            // Act
            var result = await _paymentService.GetPayment(paymentId);

            // Assert
            Assert.That(result != null);  
        }

        [Test]
        public async Task GetAllPayments_ReturnsListOfPayments()
        {
            // Arrange
            var payments = new List<Payment>(); 
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(payments);  

            // Act
            var result = await _paymentService.GetAllPayments();

            // Assert
            Assert.That(result != null);  
        }

        [Test]
        public async Task UpdatePayment_ValidPayment_ReturnsUpdatedPayment()
        {
            // Arrange
            var payment = new Payment(); 
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Payment>())).ReturnsAsync(payment);  

            // Act
            var result = await _paymentService.UpdatePayment(payment);

            // Assert
            Assert.That(result != null);  
        }
    }
}