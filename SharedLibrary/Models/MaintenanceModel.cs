using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class MaintenanceModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Välj en kategori.")]
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
