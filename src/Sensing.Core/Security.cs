using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Sensing.Core
{
    /// <summary>
    /// 安全
    /// <remarks>
    /// 创建：2014.02.11
    /// </remarks>
    /// </summary>
    public class Security
    {
        /// <summary>
        /// 创建验证码字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        public static string CreateVerificationText(int length)
        {
            var verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var random = new Random();
            for (int i = 0; i < length; i++) { verification[i] = dictionary[random.Next(dictionary.Length - 1)]; }
            return new string(verification);
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="verificationText">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns>图片</returns>
        public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
        {
            var pen= new Pen(Color.Black);
            var font = new Font("Arial", 14, FontStyle.Bold);
            var bitmap = new Bitmap(width,height);
            var g = Graphics.FromImage(bitmap);
            var totalSizeF = g.MeasureString(verificationText,font);
            var startPointF = new PointF((width-totalSizeF.Width)/2,(height-totalSizeF.Height)/2);
            //随机数产生器
            var random =new Random();
            g.Clear(Color.White);
            foreach (var t in verificationText)
            {
                Brush brush = new LinearGradientBrush(new Point(0,0),new Point(1,1),Color.FromArgb(random.Next(255),random.Next(255),random.Next(255)),Color.FromArgb(random.Next(255),random.Next(255),random.Next(255)));
                g.DrawString(t.ToString(CultureInfo.InvariantCulture),font,brush,startPointF);
                var curCharSizeF = g.MeasureString(t.ToString(CultureInfo.InvariantCulture),font);
                startPointF.X+= curCharSizeF.Width;
            }
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 256位散列加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public static string Sha256(string plainText)
        {
            var sha256 = new SHA256Managed();
            byte[] cipherText = sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            return Convert.ToBase64String(cipherText);
        }
    }
}
