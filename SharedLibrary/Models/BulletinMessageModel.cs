using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class BulletinMessageModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [MaxLength(50, ErrorMessage = "Fältet får innehålla max 300 tecken.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [MinLength(5, ErrorMessage = "Fältet måste innehålla minst 5 tecken.")]
        [MaxLength(300, ErrorMessage = "Fältet får innehålla max 300 tecken.")]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
