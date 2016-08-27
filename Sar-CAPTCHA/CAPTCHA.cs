using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Drawing;

namespace SarCAPTCHA
{
    public class CAPTCHA
    {
        public static Image Create()
        {

        }

        public static Image Create(string text, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.
            var random = new Random();
            var font = FontFamily.Families[random.Next(FontFamily.Families.Length)];


            var size = graphics.MeasureString(text, font);
        }
    }
}
