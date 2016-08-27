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
            throw new NullReferenceException();
        }

        public static Image Create(string text, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            var random = new Random();
            var font = new Font(FontFamily.GenericSerif, 20);


            var size = graphics.MeasureString(text, font);
            graphics.DrawString(text, font, Brushes.Black, new PointF(0, 0));
            return bitmap;
        }
    }
}
