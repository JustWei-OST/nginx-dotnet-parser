using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NginxDotnetParser.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// 判断指定的字符串是 null 还是 Empty 字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// 判断指定的字符串是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// 将字符串转换为MD5加密字符串(UTF8编码)
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToMD5(this string str)
        {
            return ToMD5(str, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串转换为MD5加密字符串(指定字符编码)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">指定的字符编码</param>
        /// <returns></returns>
        public static string ToMD5(this string str, Encoding encoding)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bs = md5.ComputeHash(encoding.GetBytes(str));
                var sb = new StringBuilder();
                foreach (byte b in bs)
                {
                    sb.Append(b.ToString("x2").ToLower());
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 将指定的字符串转换为UTF-8的BASE64编码
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string str) => str.ToBase64(Encoding.UTF8);
        /// <summary>
        /// 将指定的字符串用指定的编码转换为BASE64
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="encoding">转换的编码</param>
        /// <returns></returns>
        public static string ToBase64(this string str, Encoding encoding)
        {
            byte[] bs = encoding.GetBytes(str);
            var base64Str = Convert.ToBase64String(bs);
            return base64Str;
        }
    }

}
