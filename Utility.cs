using System;

namespace BlockChain
{
    public static class Utility
    {
        static Utility()
        {
        }

        public static DateTime Now
        {
            get
            {
                var result =
                    DateTime.Now;
                return result;
            }
        }

        public static string ConvertObjectToJson
            (object theObject, bool writeIndented = true)
        {
            var options =
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = writeIndented,
                };

            var result =
                System.Text.Json.JsonSerializer
                .Serialize(value: theObject, options: options);

            return result;
        }

        public static string GetSha256(string text)
        {
            var inputBytes =
                System.Text.Encoding.UTF8.GetBytes(s: text);

            var sha =
                System.Security.Cryptography.SHA256.Create();

            var outputBytes =
                sha.ComputeHash(buffer: inputBytes);

            sha.Dispose();
            //sha = null;

            var result =
                Convert.ToBase64String(inArray: outputBytes);

            return result;
        }
    }
}
