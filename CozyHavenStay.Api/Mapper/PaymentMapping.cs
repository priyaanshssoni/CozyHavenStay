using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
	public static class PaymentMapping
	{
        public static Payment MapToPayment(PaymentDTO paymentDTO)
        {
            if (paymentDTO == null)
                return null;

            return new Payment
            {
                PaymentId = paymentDTO.PaymentID,
                Amount = paymentDTO.Amount,
                BookingId = paymentDTO.BookingId,
                Status = paymentDTO.PaymentStatus,
                PaymentMethod = paymentDTO.PaymentMethod,
                PaymentDate = paymentDTO.PaymentDate
            };
        }

        public static PaymentDTO MapToPaymentDTO(Payment payment)
        {
            if (payment == null)
                return null;

            return new PaymentDTO
            {
                PaymentID = payment.PaymentId,
                Amount = payment.Amount,
                BookingId = payment.BookingId,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}

