using Xunit;
using Hospital.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            
        }

    }
    public class RegistrationFormTests
    {
        [Fact]
        public void RegistrationForm_WithValidData_ShouldBeValid()
        {
            var form = new RegistrationForm
            {
                Email = "test@example.com",
                Password = "P@ssw0rd!",
                FirstName = "John",
                LastName = "Doe"
            };

            bool isValid = form.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void RegistrationForm_WithInvalidEmail_ShouldBeInvalid()
        {
            var form = new RegistrationForm
            {
                Email = "invalid-email",
                Password = "P@ssw0rd!",
                FirstName = "John",
                LastName = "Doe"
            };

            bool isValid = form.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void RegistrationForm_WithMissingRequiredFields_ShouldBeInvalid()
        {
            var form = new RegistrationForm
            {
                Email = "test@example.com",
                Password = "P@ssw0rd!",
                //mangler feilds
            };

            var context = new ValidationContext(form, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(form, context, results, validateAllProperties: true);

            Assert.False(isValid);
        }

        [Fact]
        public void RegistrationForm_WithValidEmailPattern_ShouldBeValid()
        {
            
            var form = new RegistrationForm
            {
                Email = "valid.email@example.com",
                Password = "P@ssw0rd!",
                FirstName = "John",
                LastName = "Doe"
            };

            bool isValid = form.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void RegistrationForm_WithWhitespaceEmail_ShouldBeInvalid()
        {
            var form = new RegistrationForm
            {
                Email = " ",
                Password = "P@ssw0rd!",
                FirstName = "John",
                LastName = "Doe"
            };


            bool isValid = form.IsValid();

            Assert.False(isValid);
        }
    }


}