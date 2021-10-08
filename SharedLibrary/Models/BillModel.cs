using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Adress { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
    }
}
