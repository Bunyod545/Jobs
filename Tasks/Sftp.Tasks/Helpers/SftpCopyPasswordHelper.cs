using Jobs.Tasks.Common.Helpers;

namespace Sftp.Tasks.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SftpCopyPasswordHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] SftpCopyKeyBytes = { 0x1, 0x3, 0x3, 0x4, 0x2, 0x3, 0x4, 0x2, 0x3, 0x6, 0x2, 0x3, 0x4, 0x2, 0x3, 0x4 };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string dataToEncrypt)
        {
            return PasswordHelper.Encrypt(SftpCopyKeyBytes, dataToEncrypt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedString)
        {
            return PasswordHelper.Decrypt(SftpCopyKeyBytes, encryptedString);
        }
    }
}
