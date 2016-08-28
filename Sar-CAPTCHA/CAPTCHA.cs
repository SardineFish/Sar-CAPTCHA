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

        static double sizeDeviation = 0.4;
        public static double SizeDeviation
        {
            get
            {
                return sizeDeviation;
            }
            set
            {
                sizeDeviation = value;
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
            var random = new Random();

            var widthList = new double[text.Length];
            double sum = 0;
            for(var i = 0; i < widthList.Length; i++)
            {
                widthList[i] = SizeDeviation * random.NextDouble();
                var sign = Math.Sign(0.5 - random.NextDouble());
                widthList[i] *= sign;
                widthList[i] += 1;
                sum += widthList[i];
            }
            for (var i = 0; i < widthList.Length; i++)
            {
                widthList[i] = width * (widthList[i] / sum);
            }


            float x = 0;
            float y = 0;
            float widthRest = width;
            for (int i = 0; i < text.Length; i++)
            {
                var chr = text[i].ToString();
                var fontFamily = FontFamily.Families[random.Next(FontFamily.Families.Length)];
                FontFamily f = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
                float fontSize = widthRest / text.Length;
                fontSize = (float)(widthList[i]);
                var font = new Font(f, fontSize, GraphicsUnit.Pixel);
                var size = graphics.MeasureString(chr, font);
                y = (height - size.Height) / 2;
                graphics.DrawString(chr, font, Brushes.Black, new PointF(x, y));
                x += size.Width;
                widthRest -= size.Width;
            }
            return bitmap;
        }
    }
}
