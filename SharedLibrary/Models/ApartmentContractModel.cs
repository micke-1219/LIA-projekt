using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class ApartmentContractModel
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string ApartmentNumber { get; set; }
        public decimal Rent { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCanceled { get; set; }
        public int UserId { get; set; }
    }
}
