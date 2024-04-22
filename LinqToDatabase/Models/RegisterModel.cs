using System.ComponentModel.DataAnnotations;

namespace LinqToDatabase.Models
{
    public class RegisterModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required DateTime Birthday { get; set; }

    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class Response
    {
        public string? Ststus { get; set; }
        public string? Message { get; set; }
        public string Status { get; internal set; }
    }
}
