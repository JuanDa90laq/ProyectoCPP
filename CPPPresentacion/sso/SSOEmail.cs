namespace SSO.Finagro
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class SSOEmail
    {
        #region definicion de variables privadas

        private MailMessage msg = null;
        private NetworkCredential credentials;
        private SmtpClient smtp = null;
        private string smtpServer = string.Empty;
        private int port = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        public SSOEmail()
        {
            this.msg = new MailMessage();
            this.credentials = new NetworkCredential();
            this.credentials.UserName = ConfigurationManager.AppSettings["UserServer"];
            this.credentials.Password = ConfigurationManager.AppSettings["PwdServer"];
            ///this.smtpServer = "pod51009.outlook.com";
            this.smtpServer = ConfigurationManager.AppSettings["EmailServer"];
            ///this.port = 587;
            this.port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]); 
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Envia el correo electronico el cual permitira al usuario reestablecer su contraseña metodo publico
        /// </summary>
        /// <param name="email">email asociado a la contraseña a ser actualizada</param>
        /// <param name="token">token generado para la actualizacion de la contraseña</param>
        /// <param name="url">url de la pagina que realiza la actualizacion de la contraseña</param>
        /// <returns>true o false</returns>
        public bool sendMessage(string email, string token, string url, int? mensaje)
        {
            return this.message(email, token, url, mensaje);
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        /// Envia el correo electronico el cual permitira al usuario reestablecer su contraseña metodo privado
        /// </summary>
        /// <param name="email">email asociado a la contraseña a ser actualizada</param>
        /// <param name="token">token generado para la actualizacion de la contraseña</param>
        /// <param name="url">url de la pagina que realiza la actualizacion de la contraseña</param>
        /// <returns>true o false</returns>
        private bool message(string email, string token, string url, int? mensaje)
        {
            string mensajeCuerpo = string.Empty;

            this.msg.To.Add(new MailAddress(email));
            this.msg.From = new MailAddress(ConfigurationManager.AppSettings["remitenteEmail"], ConfigurationManager.AppSettings["remitente"]); 
            msg.Subject = ConfigurationManager.AppSettings["SubjectMessaje"];

            if (mensaje == 1)
                mensajeCuerpo = ConfigurationManager.AppSettings["msgActivacionRegistro"].ToString();
            else
                mensajeCuerpo = ConfigurationManager.AppSettings["mensaje"].ToString();

            msg.Body = mensajeCuerpo + "<br/><br/> <a href='" + ConfigurationManager.AppSettings["urlActualizar"] + "'>Actualizar Contraseña</a><br/><br/>";
            string hostName = System.Net.Dns.GetHostName();
            msg.Body = msg.Body.Replace("resetPWD", url  +"?registro="+ token+"&usuario="+ email+"&accion=2");
            msg.IsBodyHtml = true;

            using (this.smtp = new SmtpClient())
            {
                this.smtp.Credentials = credentials;
                this.smtp.Host = this.smtpServer;
                this.smtp.Port = this.port;
                this.smtp.EnableSsl = false;
                this.smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                this.smtp.Send(msg);

                return true;
            }
        }

        
        public bool enviaMensaje(string email, int mensaje)
        {
            return this.NotificacionEmail(email, mensaje);
        }

        private bool NotificacionEmail(string email, int mensaje)
        {
            string mensajeCuerpo = string.Empty;

            this.msg.To.Add(new MailAddress(email));
            this.msg.From = new MailAddress(ConfigurationManager.AppSettings["remitenteEmail"], ConfigurationManager.AppSettings["remitente"]);
            msg.Subject = ConfigurationManager.AppSettings["SubjectMessaje"];
            if (mensaje == 2)
                mensajeCuerpo = ConfigurationManager.AppSettings["BloqPASSmensaje"].ToString();
            else if(mensaje == 3)
                mensajeCuerpo = ConfigurationManager.AppSettings["DesBloqPASSmensaje"].ToString();
            else
                mensajeCuerpo = "*";
            msg.Body = mensajeCuerpo;
            string hostName = System.Net.Dns.GetHostName();
            //msg.Body = msg.Body.Replace("resetPWD", url + "?registro=" + token + "&usuario=" + email + "&accion=2");
            msg.IsBodyHtml = true;

            using (this.smtp = new SmtpClient())
            {
                this.smtp.Credentials = credentials;
                this.smtp.Host = this.smtpServer;
                this.smtp.Port = this.port;
                this.smtp.EnableSsl = false;
                this.smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                this.smtp.Send(msg);

                return true;
            }
        }

        #endregion
    }
}