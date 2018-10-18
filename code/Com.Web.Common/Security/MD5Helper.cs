using System;
using System.Security.Cryptography;
using System.Text;

namespace Com.Web.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// 32位MD5算法加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="time">需要加密的次数</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5Encryptor32(string str, int time)
        {
            do
            {
                str = Md5Encryptor32(str);
                time--;
            } while (time > 0);
            return str;
        }
        
        /// <summary>
        /// 32位MD5算法加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="time">需要加密的次数</param>
        /// <param name="length">加密的长度32或16</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5Encryptor32(string str, int time, int length)
        {
            do
            {
                if (length == 32)
                {
                    str = Md5Encryptor32(str);
                }
                else
                {
                    str = Md5Encryptor16(str);
                }
                time--;
            } while (time > 0);
            return str;
        }
        
        /// <summary>
        /// 32位MD5算法加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="lower">小写还是大写（默认小写）</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5Encryptor32(string str, bool lower = true)
        {
            string password = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte b in s)
                password += b.ToString("X2");
            return lower ? password.ToLower() : password.ToUpper();
        }
       
        /// <summary>
        /// 16位MD5算法加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="lower">小写还是大写（默认小写）</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5Encryptor16(string str, bool lower = true)
        {
            string password = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            password = BitConverter.ToString(s, 4, 8).Replace("-", "");
            return lower ? password.ToLower() : password.ToUpper();
        }
    }
}
