using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Entities
{
    public partial class User
    {
        public User()
        {
            ApartmentContracts = new HashSet<ApartmentContract>();
            Bills = new HashSet<Bill>();
            BulletinMessages = new HashSet<BulletinMessage>();
            ErrorReports = new HashSet<ErrorReport>();
            LaundryBookings = new HashSet<LaundryBooking>();
            Maintenances = new HashSet<Maintenance>();
            ParkingContracts = new HashSet<ParkingContract>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        public string PersonalIdentityNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        [InverseProperty(nameof(ApartmentContract.User))]
        public virtual ICollection<ApartmentContract> ApartmentContracts { get; set; }
        [InverseProperty(nameof(Bill.User))]
        public virtual ICollection<Bill> Bills { get; set; }
        [InverseProperty(nameof(BulletinMessage.User))]
        public virtual ICollection<BulletinMessage> BulletinMessages { get; set; }
        [InverseProperty(nameof(ErrorReport.User))]
        public virtual ICollection<ErrorReport> ErrorReports { get; set; }
        [InverseProperty(nameof(LaundryBooking.User))]
        public virtual ICollection<LaundryBooking> LaundryBookings { get; set; }
        [InverseProperty(nameof(Maintenance.User))]
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        [InverseProperty(nameof(ParkingContract.User))]
        public virtual ICollection<ParkingContract> ParkingContracts { get; set; }

        public void CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool ValidatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);
            var ch = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] != PasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
