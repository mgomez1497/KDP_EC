using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Helpdesk2._0.Data
{
    public class EnvioCorreo
    {
        
            public static string mensajeError;

            private static string servidor_outlook = "smtp-mail.outlook.com";
            private static string servidor_gmail = "smtp.gmail.com";

            public static bool Enviar_Correo_Electronico(string correoSalida, string asunto, string correoDestino, string clave, string mensaje, bool outlook = true)
            {

                try
                {
                    MailMessage correo = new MailMessage();
                    correo.From = new MailAddress(correoSalida, asunto, System.Text.Encoding.UTF8);
                    correo.To.Add(correoDestino); //Correo destino
                    correo.Subject = asunto; //Asunto
                    correo.Body = mensaje; //Mensaje del correo
                    correo.IsBodyHtml = true;
                    correo.Priority = MailPriority.Normal;
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;

                    if (outlook)
                    {
                        smtp.Host = servidor_outlook; //Host del servidor de correo
                        smtp.Port = 587; //Puerto de salida
                    }

                    else
                    {
                        smtp.Host = servidor_gmail; //Host del servidor de correo
                        smtp.Port = 25; //Puerto de salida
                    }


                    // smtp.Port = 25; //Puerto de salida
                    smtp.Credentials = new System.Net.NetworkCredential(correoSalida, clave);//Cuenta de correo
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                    smtp.Send(correo);
                }
                catch (Exception e)
                {
                    mensajeError = e.Message;
                    return false;
                }


                return true;

            }
        }
    }

