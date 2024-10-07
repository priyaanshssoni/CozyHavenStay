using System;
namespace CozyHavenStay.Data.DTOs
{
    public class ReviewDTO
    {
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}

