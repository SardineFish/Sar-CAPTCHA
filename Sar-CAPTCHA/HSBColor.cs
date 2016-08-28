using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SarCAPTCHA
{
    public struct HSBColor
    {
        double h;
        public double H
        {
            get
            {
                return h;
            }
            set
            {
                h = (value + 360) % 360;
            }
        }

        double s;
        public double S
        {
            get
            {
                return s;
            }
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0;
                s = value;
            }
        }

        double b;
        public double B
        {
            get
            {
                return b;
            }
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0;
                b = value;
            }
        }

        double a;
        public double A
        {
            get
            {
                return a;
            }
            set
            {
                if (value > 255)
                    value = 255;
                else if (value < 0)
                    value = 0;
                a = value;
            }
        }

        public HSBColor (double hue,double saturation, double brightness):this (hue,saturation,brightness,255)
        {

        }

        public HSBColor(double hue,double saturation, double brightness, double alpha)
        {
            h = 0;
            s = 0;
            b = 1;
            a = 255;
            this.H = hue;
            this.S = saturation;
            this.B = brightness;
            this.A = alpha;
        }

        public static HSBColor Random(Random random = null)
        {
            if (random == null)
                random = new System.Random();
            var h= 360 * random.NextDouble();
            var s = random.NextDouble();
            var b= random.NextDouble();
            return new SarCAPTCHA.HSBColor(h, s, b);
        }

        public Color ToRGB()
        {
            var hue = this.H;
            var sat = this.S;
            var bri = this.B;
            Func<double, double> Clamp = (x) =>
              {
                  if (x < 0) return 0;
                  if (x > 255) return 255;
                  return x;
              };
            while (hue < 0) { hue += 360; };
            while (hue >= 360) { hue -= 360; };
            double R, G, B;
            if (bri <= 0)
            { R = G = B = 0; }
            else if (sat <= 0)
            {
                R = G = B = bri;
            }
            else
            {
                double hf = hue / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = bri * (1 - sat);
                double qv = bri * (1 - sat * f);
                double tv = bri * (1 - sat * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = bri;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = bri;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = bri;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = bri;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = bri;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = bri;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = bri;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = bri;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = bri; // Just pretend its black/white
                        break;
                }
            }
            var r = Clamp(R * 255.0);
            var g = Clamp(G * 255.0);
            var b = Clamp(B * 255.0);
            return Color.FromArgb((int)r, (int)g, (int)b);
        }

    }
}
