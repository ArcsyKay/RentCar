using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Login jest wymagany")]
        public string UserLogin { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }
    }
}