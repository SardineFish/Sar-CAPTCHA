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

        static double maxZoom = 1.5;
        public static double MaxZoom
        {
            get
            {
                return maxZoom;
            }
            set
            {
                maxZoom = value;
            }
        }

        static double minZoom = 0.5;
        public static double MinZoom
        {
            get
            {
                return minZoom;
            }
            set
            {
                minZoom = value;
            }
        }

        static double rotateLimit = 30;
        public static double RotateLimit
        {
            get
            {
                return rotateLimit;
            }
            set
            {
                rotateLimit = value;
            }
        }

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
            float widthRest = width;
            for (int i = 0; i < text.Length; i++)
            {
                var chr = text[i].ToString();
                var random = new Random();
                var fontFamily = FontFamily.Families[random.Next(FontFamily.Families.Length)];
                float fontSize = width / text.Length;

                #region Zoom



                #endregion
            }
            foreach (var chr in text)
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
