using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SarCAPTCHA;
using System.IO;
using System.Drawing;

namespace WebTest
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            var img = CAPTCHA.Create("Test", 100, 50);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Response.ClearContent();
            Response.ContentType = "image/PNG";
            Response.BinaryWrite(ms.GetBuffer());
        }
    }
}