using System;
namespace CozyHavenStay.Data.Models
{
	public class OwnerBookingDetails
	{
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfBookings { get; set; }
    }
}

