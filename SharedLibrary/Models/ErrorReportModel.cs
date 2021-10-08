using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class ErrorReportModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Välj en kategori.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [MinLength(5, ErrorMessage = "Fältet måste innehålla minst 5 tecken.")]
        [MaxLength(300, ErrorMessage = "Fältet får innehålla max 300 tecken.")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
