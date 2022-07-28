using System;
using System.Collections.Generic;

namespace parking_project_api.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public double? TotalDue { get; set; }
        public int ParkingSpaceId { get; set; }
        public int CustomerId { get; set; }
    }
}
