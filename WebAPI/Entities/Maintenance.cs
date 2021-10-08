using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Entities
{
    public partial class Maintenance
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Maintenances")]
        public virtual User User { get; set; }
    }
}
