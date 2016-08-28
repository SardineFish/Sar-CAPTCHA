using System;
using System . Collections . Generic;
using System . Linq;
using System . Security . Cryptography;
using System . Web;
using System . Web . UI;
using System . Web . UI . WebControls;

namespace WebTest
{
    public partial class wtf : System . Web . UI . Page
    {
        protected void Page_Load ( object sender , EventArgs e )
        {
            Response . CacheControl = "no-cache";
            var GET = Request . QueryString;
            if ( GET [ "text" ] != null && GET [ "width" ] != null && GET [ "height" ] != null )
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider ();
                
                string text = GET [ "text" ];
                string width = GET [ "width" ];
                string height = GET [ "height" ];
                string args = GET [ "args" ];
                string token = GET [ "token" ];

            }
        }
    }
}