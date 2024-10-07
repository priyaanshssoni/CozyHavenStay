using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public float Rating { get; set; }


        public string? Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public Hotel? Hotel { get; set; }

        public User? User { get; set; }
    }
}

