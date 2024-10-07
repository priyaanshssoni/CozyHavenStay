using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
	public interface IPaymentService
	{
        Task<Payment> AddPayment(PaymentDTO payment);
        Task<Payment> DeletePayment(int paymentId);
        Task<Payment> GetPayment(int paymentId);
        Task<List<Payment>> GetAllPayments();
        Task<Payment> UpdatePayment(Payment payment);
        //Task<List<Payment>> GetAllPaymentByUser(int UserId);
        //Task<List<Payment>> GetAllPaymentsByOwner(int ownerId);
    }
}

