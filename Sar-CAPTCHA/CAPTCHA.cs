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

        static double sizeOffset = 0.4;
        public static double SizeOffset
        {
            get
            {
                return sizeOffset;
            }
            set
            {
                sizeOffset = value;
            }
        }

        static double positionOffset = 0.3;
        public static double PositionOffset
        {
            get
            {
                return positionOffset;
            }
            set
            {
                positionOffset = value;
            }
        }

        static double colorOffset = 0.5;
        public static double ColorOffset
        {
            get
            {
                return colorOffset;
            }
            set
            {
                colorOffset = value;
            }
        }

        static List<FontFamily> fontFamilies = new List<FontFamily>(FontFamily.Families);
        static public List<FontFamily> FontFamilies
        {
            get
            {
                return fontFamilies;
            }
            set
            {
                fontFamilies = value;
            }
        }

        static List<string> ignoreFonts = new List<string>(new string[]
            { "Marlett",
            "Bauhaus 93",
            "Bookshelf Symbol 7",
            "Brush Script MT",
            "Brush Script Std",
            "Freestyle Script",
            "French Script MT",
            "Giddyup Std",
            "Harlow Solid Italic",
            "Informal Roman",
            "Juice ITC",
            "Kunstler Script",
            "Magneto",
            "Matura MT Script Capitals",
            "Mesquite Std",
            "Mistral",
            "MS Reference Specialty",
            "MT Extra",
            "Old English Text MT",
            "Parchment",
            "Rosewood Std Regular",
            "Snap ITC",
            "Symbol",
            "Vivaldi",
            "Vladimir Script",
            "Webdings",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Microsoft Himalaya",
            "Niagara Engraved",
            "Niagara Solid",
            "Onyx",
            "Playbill",
            "Small Fonts",
            "Microsoft Yi Baiti",

            });
        public static List<string> IgnoreFonts
        {
            get
            {
                return ignoreFonts;
            }
            set
            {
                ignoreFonts = value;
                foreach ( var ignoreFont in value)
                {
                    foreach (var font in fontFamilies)
                    {
                        if (ignoreFont == font.Name )
                        {
                            FontFamilies.Remove(font);
                            break;
                        }
                    }
                }
                
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

        static CAPTCHA()
        {
            IgnoreFonts = ignoreFonts;
        }

        public static Image Create()
        {
            throw new NullReferenceException();
        }

        public static Image Create(string text, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            var random = new Random();
            //Background color
            var bgColor = HSBColor.Random();
            /*bgColor.S = 0.3;
            bgColor.B = -(bgColor.B - 1) * (bgColor.B - 1) + 1;*/
            bgColor.S = 0.33;
            bgColor.B = 1;
            graphics.Clear(Color.White);

            var widthList = new double[text.Length];
            double sum = 0;
            for(var i = 0; i < widthList.Length; i++)
            {
                widthList[i] = SizeOffset * random.NextDouble();
                var sign = Math.Sign(0.5 - random.NextDouble());
                widthList[i] *= sign;
                widthList[i] += 1;
                sum += widthList[i];
            }
            for (var i = 0; i < widthList.Length; i++)
            {
                widthList[i] = width * (widthList[i] / sum);
            }
            

            float nextX = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var chr = text[i].ToString();
                Retry:
                try
                {
                    //Random Font
                    var fontFamily = FontFamilies[random.Next(FontFamilies.Count)];
                    FontFamily f = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);

                    //Random Size
                    var fontSize = 20f;// (float)(widthList[i]);
                    var font = new Font(fontFamily, fontSize, GraphicsUnit.Pixel);
                    var size = graphics.MeasureString(chr, font);
                    fontSize = (float)(20 / size.Width * widthList[i]);
                    font = new Font(fontFamily, fontSize, GraphicsUnit.Pixel);
                    size = graphics.MeasureString(chr, font);

                    //Random Position
                    var dx = size.Width * (PositionOffset * random.NextDouble()) * Math.Sign(0.5 - random.NextDouble());
                    var dy = size.Height * (PositionOffset * random.NextDouble()) * Math.Sign(0.5 - random.NextDouble());
                    float x = (float)(nextX + dx);
                    float y = (float)((height - size.Height) / 2 + dy);

                    //Ramdom Color
                    var color = HSBColor.Random();
                    var offset = ColorOffset * random.NextDouble() * Math.Sign(0.5 - random.NextDouble()) + 1;
                    color.S = 1;
                    color.B = 0.5;
                    color.H += 180;
                    color.H *= offset;
                    graphics.DrawString(chr, font, new SolidBrush(color.ToRGB()), new PointF(x, y));
                    nextX += size.Width;
                    
                }
                catch (Exception ex)
                {
                    goto Retry;
                }
            }
            return bitmap;
        }
    }
}
