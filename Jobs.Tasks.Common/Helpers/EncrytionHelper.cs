using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jobs.Tasks.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class EncrytionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] DefaultKeyBytes = { 0x1, 0x2, 0x3, 0x4, 0x2, 0x3, 0x4, 0x2, 0x3, 0x4, 0x2, 0x3, 0x4, 0x2, 0x3, 0x4 };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string dataToEncrypt)
        {
            return Encrypt(DefaultKeyBytes, dataToEncrypt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] key, string dataToEncrypt)
        {
            try
            {
                return InternalEncrypt(key, dataToEncrypt);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        private static string InternalEncrypt(byte[] key, string dataToEncrypt)
        {
            var encryptor = new AesManaged { Key = key, IV = key };
            using (var encryptionStream = new MemoryStream())
            using (var encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                var utfD1 = Encoding.UTF8.GetBytes(dataToEncrypt);
                encrypt.Write(utfD1, 0, utfD1.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();

                return Convert.ToBase64String(encryptionStream.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedString)
        {
            return Decrypt(DefaultKeyBytes, encryptedString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(byte[] key, string encryptedString)
        {
            try
            {
                return InternalDecrypt(key, encryptedString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        private static string InternalDecrypt(byte[] key, string encryptedString)
        {
            var decryptor = new AesManaged();
            var encryptedData = Convert.FromBase64String(encryptedString);
            decryptor.Key = key;
            decryptor.IV = key;

            using (var decryptionStream = new MemoryStream())
            using (var decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                decrypt.Write(encryptedData, 0, encryptedData.Length);
                decrypt.Flush();
                decrypt.Close();

                var decryptedData = decryptionStream.ToArray();
                return Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
            }
        }
    }
}
