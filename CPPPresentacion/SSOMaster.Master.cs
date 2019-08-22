namespace SSO.Finagro
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    public partial class SSOMaster : System.Web.UI.MasterPage
    {
        private string RedirecLogin = ConfigurationManager.AppSettings["SSO_DIR_APP_LOGIN"].ToString();
        private string sUrlApiIBRDTF = ConfigurationManager.AppSettings["SERVICIOSCPP"].ToString();
        private string[] sValue = null;

        /// <summary>
        /// Cargue inicial de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            string sPaginaActual = string.Empty;
            string sToken = string.Empty;
            CUsuario ressult = new CUsuario();
            try
            {
               
                sPaginaActual = Path.GetFileName(Request.Url.AbsolutePath);
                FinagroHomeApp.PostBackUrl = ConfigurationManager.AppSettings["SSO_DIR_APP"].ToString();

                if (!sPaginaActual.Equals("AccesoDenegado"))
                {

                    if (!IsPostBack)
                    {
                        SSOSesion sSesion = new SSOSesion();
                        if (Request.QueryString["Value"] != null)
                        {
                            string Valores = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Value"]));
                            sValue = Valores.Split('|');
                            //0. posicion = Token
                            //1. posicion = id aplicacion
                            //2. posicion = id Usuario
                            //3. posicion = email usuario
                            //4. idCookie


                            this.AbrirSesion(sValue[0].ToString());

                            if (sValue[4].ToString() == HttpContext.Current.Session.SessionID)
                            {
                                //valida que el token de las cookies sea el correcto
                                if (!string.IsNullOrEmpty(sValue[0].ToString()))
                                    sToken = sSesion.ValidarSesion(sValue[0].ToString()) ? sValue[0].ToString() : string.Empty;
                                else
                                    Response.Redirect(ConfigurationManager.AppSettings["SSO_DIR_APP_LOGIN"].ToString(), false);

                                //carga las variables para las aplicaciones
                                if (sSesion.ValidarSesion(sValue[0].ToString()) == true)
                                {
                                    sSesion = sSesion.SSOAdicionaParam(Convert.ToInt32(sValue[1]),
                                                                        sValue[0].ToString(),
                                                                        Convert.ToInt32(sValue[2]),
                                                                        sValue[3].ToString(),
                                                                        ressult.Token);

                                    if (Session["FINAGRO_SSO_SESION"] == null)
                                        Session["FINAGRO_SSO_SESION"] = sSesion;
                                    MenusUsuario(sSesion);
                                }
                            }
                            else
                                Response.Redirect(ConfigurationManager.AppSettings["SSO_DIR_APP_LOGIN"].ToString(), false);
                        }
                        else
                        {
                            if (Session["FINAGRO_SSO_SESION"] != null)
                            {
                                sSesion = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                                MenusUsuario(sSesion);
                            }
                            else
                                Response.Redirect("~/AccesoDenegado.aspx", false);
                        }

                    }
                }
            }        
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(sValue[2].ToString()), Convert.ToInt32(sValue[1].ToString()), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="mensaje"></param>
        /// <param name="exMessage"></param>
        /// <param name="exStackTrace"></param>
        /// <param name="Origen"></param>
        public void LogSSO(int idUsuario, int iAplicacion, string mensaje, string exMessage, string exStackTrace, string Origen)
        {
            SSOSesion sSesion = new SSOSesion();

            string sCarpeta = ConfigurationManager.AppSettings["SSO_LOG_DIR"].ToString();
            string ip = sSesion.GetIPAddress();

            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "id_usuario :" + idUsuario == null ? "id_usuario :" : "id_usuario :" + idUsuario.ToString());
            dict.Add(2, "Aplicacion : " + iAplicacion);
            dict.Add(3, "origen : " + Origen);
            dict.Add(4, "mensaje : " + mensaje);
            dict.Add(5, "Error: " + exMessage + "---" + exStackTrace);
            dict.Add(6, "ip : " + ip);
            dict.Add(7, "Fecha : " + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

            DateTime aDate = DateTime.Now;
            string pathToFiles = Server.MapPath(sCarpeta);
            string path = pathToFiles + aDate.ToString("yyyy'-'MM'-'dd") + ".txt";
            File.AppendAllLines(path, new String[] { "[-------------------  " + DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'") + "   ------------------]" });
            foreach (KeyValuePair<int, string> pair in dict)
                File.AppendAllLines(path, new String[] { pair.Value });
            File.AppendAllLines(path, new String[] { "[-------------------------------------------------------------------------]" });
        }

        public void PresentarMensaje(SSOSesion.TipoMensaje tmMensaje, string sTitulo, string sMensaje, bool tipoSesion)
        {
            string IconId = "";
            switch (tmMensaje)
            {
                case SSOSesion.TipoMensaje.Rojo:
                    IconId = "actions_cancel_32x32office2013";
                    break;
                case SSOSesion.TipoMensaje.Verde:
                    IconId = "actions_apply_32x32office2013";
                    break;
                case SSOSesion.TipoMensaje.Amarillo:
                    IconId = "status_warning_32x32";
                    break;
                case SSOSesion.TipoMensaje.Pregunta:
                    IconId = "support_question_32x32office2013";
                    break;
                default:
                    IconId = "actions_cancel_32x32office2013";
                    break;
            }


            dvBtnmensaje.Image.IconID = IconId;
            dvLbNebsaje.Text = sMensaje;
            ppSession.HeaderText = sTitulo;
            ppSession.ShowOnPageLoad = true;
            ppSession.ShowCloseButton = false;
            LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), this.dvLbNebsaje.Text, "", "", Path.GetFileName(Request.Url.AbsolutePath));
        }

        /// <summary>
        /// cnosulta el Token de las Cookies del navegador
        /// </summary>
        /// <returns></returns>
        private string ConsultarTokenCookie()
        {
            string sToken = string.Empty;
            try
            {
                // Valida existencia de cookie antes de consultar
                if (Request.Cookies["FINAGRO_SSO_COOKIE"] != null)
                {
                    // Crea objeto cookie
                    HttpCookie objCookie = Request.Cookies["FINAGRO_SSO_COOKIE"];

                    // Valida existencia de campo token
                    if (objCookie.Values["Token"] != null)
                        sToken = objCookie.Values["Token"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(sValue[2].ToString()), Convert.ToInt32(sValue[1].ToString()), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }
            return sToken;
        }

        private void MenusUsuario(SSOSesion sSesion)
        {
            CUsuario cUsuario = new CUsuario();
            try
            {
                LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), "Ingresa a la aplicación", "", "", Path.GetFileName(Request.Url.AbsolutePath));
                cUsuario.ListaMenus = sSesion.ConsultarMenusDeUsuario(sSesion.Usuario.Identificador, sSesion.Usuario.Token);
                cUsuario.ListaMenus = cUsuario.ListaMenus.Where(m=>m.Activo.Equals(true)).OrderBy(a => a.Orden).ToList();
                DataSet dsMenuUsuario = ConsultarMenusUsuario(cUsuario.ListaMenus, sSesion.Usuario.iAplicacion);
                mUsuario.DataSource = new HierarchicalDataSet(dsMenuUsuario, "IdMenu", "IdMenuPadre");
                mUsuario.DataBind();

            }
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaMenus"></param>
        /// <param name="aplicativoID"></param>
        /// <returns></returns>
        private DataSet ConsultarMenusUsuario(List<CMenu> listaMenus, int aplicativoID)
        {
            DataSet dsRetorno = new DataSet();
            try
            {
                DataTable autorizaciones = new DataTable("Table");
                autorizaciones.Columns.Add("Text", typeof(string));
                autorizaciones.Columns.Add("IdMenu", typeof(int));
                autorizaciones.Columns.Add("IdMenuPadre", typeof(int));
                autorizaciones.Columns.Add("URL", typeof(string));

                foreach (CMenu menu in listaMenus)
                {
                    if (menu.IdAplicativo == aplicativoID)
                    {
                        DataRow fila = autorizaciones.NewRow();
                        fila["IdMenu"] = menu.Identificador;
                        if (menu.IdPadre != 0)
                            fila["IdMenuPadre"] = menu.IdPadre;
                        fila["Text"] = menu.Titulo;
                        fila["URL"] = menu.URL;
                        autorizaciones.Rows.Add(fila);
                    }
                }
                dsRetorno.Tables.Add(autorizaciones);
            }
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }

            return dsRetorno;
        }


        public void PresentarMensaje(SSOSesion.TipoMensaje tmMensaje, string sTitulo, string sMensaje)
        {

            string IconId = "";
            switch (tmMensaje)
            {
                case SSOSesion.TipoMensaje.Rojo:
                    IconId = "actions_cancel_32x32office2013";
                    break;
                case SSOSesion.TipoMensaje.Verde:
                    IconId = "actions_apply_32x32office2013";
                    break;
                case SSOSesion.TipoMensaje.Amarillo:
                    IconId = "status_warning_32x32";
                    break;
                case SSOSesion.TipoMensaje.Pregunta:
                    IconId = "support_question_32x32office2013";
                    break;
                default:
                    IconId = "actions_cancel_32x32office2013";
                    break;
            }

            btnMensaje.Image.IconID = IconId;
            lblMensaje.Text = sMensaje;
            pcMensajeSitio.HeaderText = sTitulo;
            pcMensajeSitio.ShowOnPageLoad = true;
        }

        public object ConsultarUsuarios(int iAplicativo)
        {
            // Valida si tiene una sesión activa
            SSOSesion cUsuario = (SSOSesion)Session["FINAGRO_SSO_SESION"];
            if (cUsuario != null)
                return cUsuario.ConsultarUsuarios(iAplicativo, cUsuario.Usuario.Token);
            return null;
        }

        public int ConsultarIdAplicativo()
        {
            int iAplicacion = 0;
            cUsuarioSession cUsuario = (cUsuarioSession)Session["FINAGRO_SSO_SESION"];
            if (cUsuario != null)
                iAplicacion = cUsuario.iAplicacion;

            return iAplicacion;
        }

        public CUsuario ConsultarUsuarioSesion()
        {
            SSOSesion sSesion = (SSOSesion)Session["FINAGRO_SSO_SESION"];
            if (sSesion != null)
                return sSesion.Usuario;
            else
                CerrarSesion();

            return null;
        }

        public void CerrarSesion()
        {
            SSOSesion sSesion = new SSOSesion();
            try
            {
                sSesion = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (sSesion == null)
                    return;

                //LogSSO(sSesion, "Cierra la sesión del usuario", "", "", Path.GetFileName(Request.Url.AbsolutePath));
                // Crea cookie nuevamente con fecha de expiración anterior a la fecha actual para que sea destruida por el browser
                HttpCookie UserCookie = new HttpCookie("FINAGRO_SSO_COOKIE");
                UserCookie.Value = string.Empty;
                UserCookie.Expires = DateTime.UtcNow.AddMonths(-10);
                Response.Cookies.Add(UserCookie);
                this.CerrarCookieToken();

                // Limpia objetos de sesión
                Session.Clear();
                Session.Abandon();

                // Redirecciona página de login
                Response.Redirect(RedirecLogin, false);

            }
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }
        }

        private void CerrarCookieToken()
        {
            try
            {
                SSOSesion ssoSesion = (SSOSesion)Session["FINAGRO_SSO_SESION"];

                if (ssoSesion.CerrarSesionToken(ssoSesion, 2))
                {
                    //LogSSO(ssoSesion, "Redirecciona a la pagina inicial y limpia las sesiones", "", "", Path.GetFileName(Request.Url.AbsolutePath));
                    this.Direccionar();
                }
                else
                {
                    //LogSSO(ssoSesion, "No se pudo cerrar la sesión", "", "", Path.GetFileName(Request.Url.AbsolutePath));
                    PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: Error al cerrando sesión", "No se pudo cerrar la sesión");
                }
            }
            catch (Exception ex)
            {
                LogSSO(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["App"]), this.ASPxLabel2.Text, ex.Message, ex.StackTrace, Path.GetFileName(Request.Url.AbsolutePath));
                string sDetalle = ex.Message + "; " + ex.StackTrace;
                PresentarMensaje(SSOSesion.TipoMensaje.Rojo, "Finagro :: se a presentado un error", sDetalle, true);
            }
        }

        private void Direccionar()
        {
            HttpCookie UserCookie = new HttpCookie("FINAGRO_SSO_COOKIE");
            UserCookie.Value = string.Empty;
            UserCookie.Expires = DateTime.UtcNow.AddMonths(-10);
            Response.Cookies.Add(UserCookie);
            Session.Clear();
            Session.Abandon();
            Response.Redirect(RedirecLogin, false);

        }

        /// <summary>
        /// Inicia la sesión del usuario de cara al SSO
        /// </summary>
        /// <param name="ssoSesion"></param>
        private void AbrirSesion(string sToken)
        {
            HttpCookieCollection MyCookieColl;

            MyCookieColl = Request.Cookies;
            // Capture all cookie names into a string array.
            String[] arr1 = MyCookieColl.AllKeys;

            // Crea cookie y asocia el token generado
            HttpCookie appCookie = new HttpCookie("FINAGRO_SSO_COOKIE", arr1[0].ToString());
            string sDomain = ConfigurationManager.AppSettings["FINAGRO_SSO_COOKIE"].ToString();

            if (!(sDomain == "" || sDomain == "*"))
            {
                appCookie.Domain = ConfigurationManager.AppSettings["FINAGRO_SSO_COOKIE"].ToString();
                appCookie.Secure = true;
            }

            // Valores - Token asociado
            appCookie.Values.Add("Token", sToken);

            // Disponibilidad solo desde el servidor
            appCookie.HttpOnly = true;

            // Genera cookie
            HttpContext.Current.Response.SetCookie(appCookie);
            //Session["FINAGRO_SSO_SESION"] = ssoSesion;
            //Session["SSOInicio"] = true;            
        }
    }

}