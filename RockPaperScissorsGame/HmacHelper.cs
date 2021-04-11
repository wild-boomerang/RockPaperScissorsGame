using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissorsGame
{
    class HmacHelper
    {
        public HmacHelper()
        {
            
        }

        // Returns secret key in hex
        public static string GenerateSecretKey(int secretKeySize)
        {
            byte[] secretKey = new Byte[secretKeySize]; // 16
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secretKey);
            }
            return GetHexStringFromBytes(secretKey);
        }

        // Gets secret key in hex and message
        // Returns HMAC in hex
        public static string GenerateHmac(string hexSecretKey, string message)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(RandomNumberGenerator.GetInt32(1, moves.Length + 1));
            //}

            using HMACSHA256 hmac = new HMACSHA256(HexDecode(hexSecretKey));
            byte[] hashValue = hmac.ComputeHash(GetBytesFromString(message));
            return GetHexStringFromBytes(hashValue);
        }

        // byte[] -> hex string
        public static string GetHexStringFromBytes(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }

        // string -> byte[]
        public static byte[] GetBytesFromString(string text)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetBytes(text);
        }

        // hex string -> byte[]
        public static byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }
    }
}
