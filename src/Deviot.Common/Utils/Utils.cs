using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Deviot.Common
{
    public static class Utils
    {
        private const string MEDIA_TYPE = "application/json";

        public static StringContent CreateStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
        }

        public static StringContent CreateStringContent()
        {
            return new StringContent(string.Empty, Encoding.UTF8, MEDIA_TYPE);
        }

        public static T Deserializer<T>(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string Serializer<T>(T value)
        {
            return JsonSerializer.Serialize<T>(value);
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool ValidateHexadecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return Regex.IsMatch(value, @"^[a-fA-F0-9]*$");
        }

        public static bool ValidateAlphanumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return Regex.IsMatch(value, @"^[a-zA-Z0-9]*$");
        }

        public static bool ValidateNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return new Regex("^[0-9]*$").IsMatch(value);
        }

        public static bool ValidateAlphanumericWithUnderline(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return new Regex("^[a-zA-Z0-9_]*$").IsMatch(value);
        }

        public static Exception ConvertException(IEnumerable<string> messages)
        {
            Exception exception = null;
            foreach (var message in messages)
                exception = new Exception(message, exception);

            return exception;
        }

        public static List<string> GetAllExceptionMessages(Exception exception)
        {
            var exceptions = new List<string>();
            if (exception.InnerException != null)
                exceptions = GetAllExceptionMessages(exception.InnerException);

            exceptions.Add(exception.Message);
            return exceptions;
        }

        public static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        public static string Encript(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (SHA512 hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);

                foreach (byte b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
