using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Deviot.Common
{
    public static class Utils
    {
        private static readonly IDictionary<Type, IDictionary<string, PropertyInfo>> _dictionaryType = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

        private const string MEDIA_TYPE = "application/json";
        private const string SELECT_PROCESSOR_ID = "Select ProcessorID From Win32_processor";
        private const string PROCESSOR_ID = "ProcessorID";
        private const string SELECT_HD = "SELECT * FROM Win32_PhysicalMedia";
        private const string SERIAL_NUMBER = "SerialNumber";

        private static IDictionary<string, PropertyInfo> GetTypeProperies<T>()
        {
            var type = typeof(T);
            if(_dictionaryType.ContainsKey(type))
            {
                return _dictionaryType[type];
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var dic = properties.ToDictionary(k => k.Name, v => v);
            _dictionaryType.Add(type, dic);
            return dic;
        }

        public static string GetProcessorID()
        {
            var result = string.Empty;
            try
            {
                var mbs = new ManagementObjectSearcher(SELECT_PROCESSOR_ID);
                var mbsList = mbs.Get();

                foreach (var mo in mbsList)
                    result = mo[PROCESSOR_ID].ToString();
            }
            catch (Exception)
            {
                result = string.Empty;
            }

            return result;
        }

        public static string GetHDID()
        {
            var result = string.Empty;
            try
            {
                var mbs = new ManagementObjectSearcher(SELECT_HD);
                var mbsList = mbs.Get();

                foreach (var mo in mbsList)
                    result = mo[SERIAL_NUMBER].ToString();
            }
            catch (Exception)
            {
                result = string.Empty;
            }

            return result;
        }

        public static string GetHostName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetDeviceIdentifier()
        {
            try
            {
                var result = GetProcessorID();
                if (!string.IsNullOrEmpty(result))
                    return result;

                result = GetHDID();
                if (!string.IsNullOrEmpty(result))
                    return result;

                return GetHostName();
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public static void SetProperty<T>(this T target, string name, object value)
        {
            var prop = GetTypeProperies<T>()[name];
            prop.SetValue(target, value);
        }

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
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public static string PrettyJson(string unPrettyJson)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

                return JsonSerializer.Serialize(jsonElement, options);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Serializer<T>(T value, bool unsafeRelaxedJsonEscaping = false)
        {
            if (value is null)
                return String.Empty;

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            if(unsafeRelaxedJsonEscaping)
                options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            return JsonSerializer.Serialize<T>(value, options);
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

        public static bool ValidateParameter(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return new Regex("^[a-zA-Z0-9,]*$").IsMatch(value);
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
