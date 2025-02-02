﻿using System;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.DTOs
{
	public class BookingDTO
	{
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public float TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}

