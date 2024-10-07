using System;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.DTOs
{
	public class RoomDTO
	{
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public float RoomSize { get; set; }
        public RoomType RoomType { get; set; }
        public float Price { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public ICollection<string> ImageLinks { get; set; }
    }
}

