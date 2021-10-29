using System;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Common
{

    public class Validator
    {
        private const string emailRegex = "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{1,}$";
        private const string phoneNumberRegex = @"^(\d)*(\d{10})$";

        public static bool EmailIsValid(string email)
        {
            try
            {
                var match = Regex.Match(email, emailRegex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool PhoneNumberIsValid(string phoneNumber)
        {
            try
            {
                var match = Regex.Match(phoneNumber, phoneNumberRegex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool PasswordIsValid(string password)
        {
            if (ValidatePassword(password) == PasswordValidationResult.Valid)
                return true;
            return false;
        }
        public static PasswordValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return PasswordValidationResult.LessThan8Symbols;
            }
            if (password.Length < 8)
                return PasswordValidationResult.LessThan8Symbols;
            if (password.Length > 16)
                return PasswordValidationResult.MoreThan16Symbols;
            return PasswordValidationResult.Valid;
        }
    }
    public enum PasswordValidationResult
    {
        Valid, LessThan8Symbols, MoreThan16Symbols
    }

}