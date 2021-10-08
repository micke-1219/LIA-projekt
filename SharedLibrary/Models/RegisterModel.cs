using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class RegisterModel
    {
        [Required (ErrorMessage = "Fältet får inte vara tomt.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]*$", ErrorMessage = "Namnet får enbart bestå av bokstäver.")]
        [MinLength(2, ErrorMessage = "Fältet måste innehålla minst 2 tecken.")]
        [MaxLength(50, ErrorMessage = "Fältet får innehålla max 50 tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]*$", ErrorMessage = "Namnet får enbart bestå av bokstäver.")]
        [MinLength(2, ErrorMessage = "Fältet måste innehålla minst 2 tecken.")]
        [MaxLength(50, ErrorMessage = "Fältet får innehålla max 50 tecken.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Personnumret får enbart bestå av siffror.")]
        [MinLength(10, ErrorMessage = "Fältet måste innehålla minst 10 tecken.")]
        [MaxLength(10, ErrorMessage = "Fältet får innehålla max 10 tecken.")]
        public string PersonalIdentityNumber { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Ange en giltig E-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefonnumret får enbart bestå av siffror.")]
        [MinLength(5, ErrorMessage = "Fältet måste innehålla minst 5 tecken.")]
        [MaxLength(15, ErrorMessage = "Fältet får innehålla max 15 tecken.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Fältet måste innehålla minst 8 tecken.")]
        [MaxLength(50, ErrorMessage = "Fältet får innehålla max 50 tecken.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte varandra.")]
        public string PasswordConfirm { get; set; }
    }
}
