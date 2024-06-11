using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Hospital.Services
{
    public partial class RegistrationForm : ObservableValidator
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsValid()
        {
            
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, validateAllProperties: true);
            

            if (!isValid)
                return false;

            if (!IsValidEmail(Email))
                return false;
            return true;
        }
        /// <summary>
        /// Tjekke om email er en valid email
        /// ved hjælp af regex
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
