using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class Herramientas
    {
        public bool ValidarUrl(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD"; // Solo revisamos los headers para ver si es una imagen y no descargamos todo el contenido.

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Validar si la respuesta es válida (200 OK) y si es un tipo de imagen
                    if (response.StatusCode == HttpStatusCode.OK &&
                        response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void enviarMail(string mail, string bodyText, string nombre)
        {
            var fromAddress = new MailAddress("tpwebsebastiancastro@gmail.com", "Lavadero los 3 locos que quieren aprobar esta bella materia");
            var toAddress = new MailAddress(mail, nombre);
            const string fromPassword = "vyml wcpm vtko uvem";
            const string subject = "¡Tu turno a sido agendado con EXITO!";
            string body = bodyText;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }


        public static bool sesionActiva(object user)
        {
            Usuario usu = user != null ? (Usuario)user : null;
            if (usu != null && usu.Id != 0 && usu.Estado == 1)
                return true;
            else
                return false;
        }

        public bool esAdmin(object user)
        {
            Empleado emp = user != null ? (Empleado)user : null;

            if (emp != null)
            {
                if (emp.Id != 0 && emp.Estado == 1 && emp.nivelAcceso.Id == 1) return true;
            }

            return false;

        } 
    }
}
