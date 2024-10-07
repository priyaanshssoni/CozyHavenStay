using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.Models
{
        public class Booking
        {
            [Key]
            public int ReservationId { get; set; }

            [Required]
            [ForeignKey("User")]
            public int UserId { get; set; }

            [Required]
            [ForeignKey("Room")]
            public int RoomId { get; set; }

            [Required]
            public DateTime CheckInDate { get; set; }

            [Required]
            public DateTime CheckOutDate { get; set; }

            [Required]
            public int Adults { get; set; }

            [Required]
            public int Children { get; set; }

            [Required]
            public float TotalPrice { get; set; }

            [Required]
            public BookingStatus Status { get; set; }
            public DateTime BookedDate { get; set; }

         
      
            public User? User { get; set; }

            public Room? Room { get; set; }
        }
    
}

