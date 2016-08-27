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
            //Background color
            graphics.Clear(Color.White);

            float x = 0;
            float y = 0;
            foreach(var chr in text)
            {
                var random = new Random();
                var font = new Font(FontFamily.GenericSerif, 20);
                var size = graphics.MeasureString(chr.ToString(), font);

                graphics.DrawString(chr.ToString(), font, Brushes.Black, new PointF(x, y));

                x += size.Width;
            }
            return bitmap;
        }
    }
}
