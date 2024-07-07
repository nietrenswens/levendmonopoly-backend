using System.Text.RegularExpressions;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;

namespace LevendMonopoly.Api.InputValidation
{
    public class UserValidation
    {
        public static bool IsValidUser(User? user)
        {
            if (user != null)
            {
                if (nameValid(user.Name) && emailValid(user.Email) && passwordValid(user.PasswordHash))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidUser(UserPostBody? user)
        {
           return IsValidUser(new User()
           {
               Name = user?.Name ?? "",
               Email = user?.Email ?? "",
               PasswordHash = user?.Password ?? ""
           });
        }

        private static bool emailValid(string email)
        {
            if (email.Length > 0 && email.Length < 150)
            {
                if (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool passwordValid(string password)
        {
            if (password.Length > 0 && password.Length < 50)
            {
                return true;
            }
            return false;
        }

        private static bool nameValid(string name)
        {
            if (name.Length > 0 && name.Length < 50)
            {
                if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}