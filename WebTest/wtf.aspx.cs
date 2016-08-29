using System;
using System . Collections . Specialized;
using System . Drawing . Imaging;
using System . IO;
using System . Security . Cryptography;
using System . Text;
using static SarCAPTCHA . CAPTCHA;

namespace WebTest
{
    public partial class wtf : System . Web . UI . Page
    {
        string privateKey = "<RSAKeyValue><Modulus>sJ37XmQ+yzJudemsRSpaHWBbM/j5duKrI967Bqi6Dqv+PhS3zXCCED4pnollVWMarzbWJqT/ykbX9hQSFEEP2fy5WBE5pJbGFhhS21/0kk+GnnOL1mh0KRMKhyJ9liaR9seNTQuxWGOfgbUApHxWJC3+bAA78qBhqIg2AJTj2V2G663Yxp2sRrQI4f2GsOo0sbJd5OpyeJeMVol6Sqey8LUqQ3QakG8vkZmTVwvLOAsKMXgCPVL5wrgJ04+Y9wl9Ok4hsU0GIZcJOti/uWhS8JUOyetOYGR5li28hsAKBNANprOa0u7m38tWK+lOw8Iqf6xGXI0f0O2wHAytXWAp4Q==</Modulus><Exponent>AQAB</Exponent><P>0OgX1HIPAi10FTwWy+zfSoKY6MELMjBTDxjse7s5RZigHAPntVyiHJcywsn8/WjfbN0cdABMzSat3QC5oc+vgdvXR5JEThArJB8jQkxE62s9iaIKZ3Pb192R9EalY7F20hNIVMqwli0lGKtGY+4YFOsgEAbwUA/Grj3/sbEyZ78=</P><Q>2G55+QIZYqlybhnH7ifpCrzaGTdPjvN/9P0WzJqOQA32/RMxWtskVcDSj+NRgKZaAFCDtHJtTerGbPp8r1nvioMw4YYR1DMKkhxMw/61T9xdwOHLiDtTLOzshZDbobbZLDLgukSeIHp/WG5SP1XD22kqn518S3gIba2i/FOO1l8=</Q><DP>G/B+h2Y9xZ/In7Jqphlm/7MZuj9fPPYjlahSsHWowjsYZsbK7YGXGNXL7ytOj6HAB/JGhOkpXGKo7B7VtONu6KI7V5IjWoFlE66qa4qByS6Ni2PFmnJvW56Bj7cHFZ89cfksOlLlbSBajsMRZfoI9HtBzYLwDvUdMF6QXPaC9Ec=</DP><DQ>AxTHjcD25Y2uAF1DmGNfEPURUcyUyF81EiLMQUtt/QF147JzQGzgPoxJMx00WtWDcH+08Eu0vKg7/O8v+TsZiupFNuC2vkOfwea/PQcSQ7nKZ+WPBTh6/ae90kT2q32z58ototk5OxMjXvnrF9W7vRLpNW6Dh3uE+zK+oEL8RJc=</DQ><InverseQ>sGnvy0ziWYQ/rjxev14p+pF9UH0rjaGjkUWBO/dz7F7FzAYooJgLdE0hhd69JxKEp9h+5n88pUauVCpQnVU1HzO6BuOkOHNEryfpy8bpkSJqCYaosvHAti2Nri3zT7dUxHOXVJzfVIbBG1/ZjYn8hLuJ7PXRRX5LMXsXgi2JXJc=</InverseQ><D>JsDDYxbD4HoUWrMHJbYBR+jYmjLLKhbaTmiUyQVEret3Qc/x9JX3M/Ev2KV1IqM4O7Hgk2Io7g1VXZ1RyjfsbnxRfs1oxICAvmBf7drEI0Zn12SrDkSlGE4hRT2AKO4VoPxwOLOUGjzhxugcV/sgO5gL906l2LrwMraQgtgolozdtdYNde5d2l5SK0imxKt6B+t3FBUDaAInk2DenUiDJ3wpcH4ZaxK7UmMBPT1YLNixhyJZm2anlmWzNpmAc8YjNTnJLmcd4LGiMICeD2kAsAb91hLiUCfWlMrD6wf8zwoaVaiQuj/v1KJUKQyiVNIDeXlURNp9rD2VbnoTmTJlsQ==</D></RSAKeyValue>";
        NameValueCollection QueryString;

        protected void Page_Load ( object sender , EventArgs e )
        {
            Response . CacheControl = "no-cache";
            Response . ClearContent ();

            QueryString = Request . QueryString;
            if ( GET ( "text" ) != null && GET ( "width" ) != null && GET ( "height" ) != null )
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider ();
                rsa . FromXmlString ( privateKey );

                string text = Encoding . UTF8 . GetString (
                    rsa . Decrypt (
                        Convert . FromBase64String ( GET ( "text" ) )
                    , true )
                );
                int width = int . Parse ( GET ( "width" ) );
                int height = int . Parse ( GET ( "height" ) );
                string args = GET ( "args" );
                string token = GET ( "token" );

                MemoryStream ms = new MemoryStream ();
                var img = Create ( text , width , height );
                string format = GET ( "format" , "png" ) . ToLower ();
                switch ( format )
                {
                case "png":
                    img . Save ( ms , ImageFormat . Png );
                    Response . ContentType = "image/png";
                    break;
                case "jpg":
                case "jpeg":
                    img . Save ( ms , ImageFormat .Jpeg );
                    Response . ContentType = "image/jpeg";
                    break;
                default:
                    Response . Write ( "This format is not supported." );
                    return;
                }

                Response . BinaryWrite ( ms . GetBuffer () );
            }
            else
            {
                Response . Write ( "Required arguments: text, width, height" );
            }
        }

        string GET ( string key , string alt = null )
        {
            return Request . QueryString [ key ] != null ? QueryString [ key ] : alt;
        }
    }
}