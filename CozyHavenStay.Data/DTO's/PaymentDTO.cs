using System;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.DTOs
{
	public class PaymentDTO
	{
        public int PaymentID { get; set; }
        public int BookingId { get; set; }
        public float Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}

