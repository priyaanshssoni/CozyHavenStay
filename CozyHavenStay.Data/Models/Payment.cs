using System;
using static CozyHavenStay.Data.Models.Booking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.Models
{
	public class Payment
	{
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

    
        public Booking? Booking { get; set; }
    }
}

