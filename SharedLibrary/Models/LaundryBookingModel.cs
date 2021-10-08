using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class LaundryBookingModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public bool IsBooked { get; set; }
        public int UserId { get; set; }
    }
}
