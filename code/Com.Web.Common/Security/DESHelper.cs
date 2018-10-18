using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Com.Web.Common
{
    /// <summary>
    /// DES 加解密
    /// </summary>
    public sealed class DESHelper
    {

        private const string DefaultKey = "vnjs*&%5&dew";

        #region Des 加解密
        /// <summary>
        /// 加密原函数
        /// </summary>
        /// <param name="pToEncrypt">加密前的明文</param>
        /// <param name="sKey">Key</param>
        /// <returns>返回加密后的密文</returns>
        public static string Encrypt(string pToEncrypt, string sKey = DefaultKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                des.Key = Encoding.ASCII.GetBytes(MD5Helper.Md5Encryptor16(sKey).Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(MD5Helper.Md5Encryptor16(sKey).Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                    ret.AppendFormat("{0:X2}", b);
                ret.ToString();
                return ret.ToString();
            }
            catch { return null; }
        }

        /// <summary>
        /// 解密原函数
        /// </summary>
        /// <param name="pToDecrypt">加密后的密文</param>
        /// <param name="sKey">Key</param>
        /// <returns>返回加密前的明文</returns>
        public static string Decrypt(string pToDecrypt, string sKey = DefaultKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = Encoding.ASCII.GetBytes(MD5Helper.Md5Encryptor16(sKey).Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(MD5Helper.Md5Encryptor16(sKey).Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch { return null; }
        }

        #endregion
    }
}
