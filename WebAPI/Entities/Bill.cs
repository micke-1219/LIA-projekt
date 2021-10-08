using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Entities
{
    public partial class Bill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Object { get; set; }
        [StringLength(100)]
        public string Adress { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(2)]
        public string Month { get; set; }
        [Required]
        [StringLength(4)]
        public string Year { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpiryDate { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bills")]
        public virtual User User { get; set; }
    }
}
