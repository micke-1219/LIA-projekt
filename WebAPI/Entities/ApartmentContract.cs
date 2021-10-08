using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Entities
{
    public partial class ApartmentContract
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Object { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(5)]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(4)]
        public string ApartmentNumber { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Rent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        public bool IsCanceled { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("ApartmentContracts")]
        public virtual User User { get; set; }
    }
}
