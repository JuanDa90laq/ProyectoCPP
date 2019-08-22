namespace SSO.Finagro
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Clase con los métodos que permiten al aplicativo optener la información del usuario conectado.
    /// </summary>
    public class SSOSesion : IDisposable
    {

    

        private bool bTieneSesion;        
        private CUsuario cUsuario;
        string IPAddress = string.Empty;

        // URL del servicio, Id de la aplicación
        private string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();
        private string sAppId = ConfigurationManager.AppSettings["FINAGRO_APP_ID"].ToString();





        /// <summary>
        /// Se debe usar en cada petición para saber si la sesión de usuario aun está activa
        /// </summary>
        public bool TieneSesion
        {
            get { return bTieneSesion; }
        }

        /// <summary>
        /// define objeto tipo CUsuario
        /// </summary>
        public CUsuario Usuario
        {
            get { return cUsuario; }
        }

        public enum Respuesta : int
        {
            /// <summary>
            /// Valores retornados cuando un error sucede
            /// </summary>
            CorreoContraseñaErroneos = 0,
            NoSeEncontraronDatos = -3,
            ErrorBaseDeDatos = -2,
            ErrorValidacion = -1,

            /// <summary>
            /// Valores retornados cuando la operacion fue
            /// ejecutada correctamente, pero no hay valores
            /// a retornar
            /// </summary>
            OperacionCorrecta = 1
        }

        /// <summary>
        /// Tipos de mensajes
        /// </summary>
        public enum TipoMensaje
        {
            Rojo = 0,
            Amarillo = 1,
            Verde = 2,
            Pregunta = 3
        }

        public enum Emails
        {
            recuperacionContrasena = 0,
            activacionCuenta = 1,
            bloqContrasena = 2,
            DesbloqContrasena = 3
        }

        public SSOSesion()
        {
        }

        /*public SSOSesion SSOAdicionaParam(int iAplicacion, string Token, int iUsuario)
        {
            SSOSesion ssoSesion = new SSOSesion();           
            ssoSesion.cUsuario = new CUsuario();
            ssoSesion.cUsuario.Identificador = iUsuario;
            ssoSesion.cUsuario.iAplicacion = iAplicacion;
            ssoSesion.cUsuario.Token = Token;
            return ssoSesion;
        }*/

        public SSOSesion SSOAdicionaParam(int iAplicacion, string Token, int iUsuario, string Email, string Token2)
        {
            SSOSesion ssoSesion = new SSOSesion();
            ssoSesion.cUsuario = new CUsuario();
            ssoSesion.cUsuario.Identificador = iUsuario;
            ssoSesion.cUsuario.iAplicacion = iAplicacion;
            ssoSesion.cUsuario.Token = Token;
            ssoSesion.cUsuario.Correo = Email;            
            return ssoSesion;
        }

        /// <summary>
        /// Consulta los items del menu segun usuario
        /// </summary>
        /// <param name="lUsuario">log del usuario</param>
        /// <param name="sToken">token generado</param>
        /// <returns>lista con los items del menu por usuario</returns>
        public List<CMenu> ConsultarMenusDeUsuario(long lUsuario, string sToken)
        {
            List<CMenu> lCMenu = new List<CMenu>();
            //string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();

            try
            {
                using (var client = new HttpClient())
                {
                    string sUrlApi2 = sUrlApi + "/api/customers/menu/";
                    client.BaseAddress = new Uri(sUrlApi2);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sToken);

                    var responseTask = client.GetAsync(sUrlApi2 + lUsuario.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Retorno consulta Json
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        // Corrige saltos de linea y demas
                        string json = readTask.Result;

                        // Deserializa el objeto
                        DataTable dtResultado = JsonConvert.DeserializeObject<DataTable>(json);

                        if (dtResultado.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtResultado.Rows)
                            {
                                CMenu cMenu = new CMenu();
                                if (r["Resultado"].ToString() == "Correcto")
                                {
                                    cMenu.Identificador = int.Parse(r["IdMenu"].ToString());
                                    cMenu.IdPadre = int.Parse(r["IdMenuPadre"].ToString());
                                    cMenu.IdAplicativo = int.Parse(r["IdAplicativo"].ToString());
                                    cMenu.Titulo = r["Titulo"].ToString();
                                    cMenu.URL = r["URL"].ToString();
                                    cMenu.Activo = bool.Parse(r["Activo"].ToString());
                                    cMenu.Orden = int.Parse(r["Orden"].ToString());
                                }
                                else
                                {
                                    cMenu.Identificador = (int)Respuesta.ErrorValidacion;
                                    cMenu.Mensajes = r["Mensaje"].ToString();
                                }

                                lCMenu.Add(cMenu);
                            }
                        }
                        else
                        {
                            CMenu cMenu = new CMenu
                            {
                                Identificador = (int)Respuesta.NoSeEncontraronDatos,
                                Mensajes = "No hay Menus registrados para el usuario."
                            };
                            lCMenu.Add(cMenu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMenu cMenu = new CMenu
                {
                    Identificador = (int)Respuesta.ErrorBaseDeDatos,
                    Mensajes = ex.Message + " - " + ex.StackTrace
                };
                lCMenu.Add(cMenu);
            }

            return lCMenu;
        }

        public void Dispose()
        {
            CerrarSesion();
        }

        public void CerrarSesion()
        {
            bTieneSesion = false;
        }

        /// <summary>
        /// elimina el registro de sesion en la base de datos
        /// </summary>
        /// <param name="accion">accion a realizar 2 elimina un registro teniendo en cuenta el usuario y el token
        /// 3 elimina un registro teniendo en cuenta solo el usuario</param>
        /// <param name="sSesion">datos del usuario logueado</param>
        /// <returns>true o false proceso exitoso = true</returns>
        public bool CerrarSesionToken(SSOSesion sSesion, int accion)
        {
            bool proceso = true;
            using (var client2 = new HttpClient())
            {
                string sUrlApi2 = sUrlApi + "/api/login/sessionregister/";
                client2.BaseAddress = new Uri(sUrlApi2);
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var registroSesion = new SessionRegister() { cookie = string.Empty, ip = "0", usuario = sSesion.Usuario.Correo, tok = sSesion.Usuario.Token, accion = accion.ToString() };
                var postTask2 = client2.PostAsJsonAsync<SessionRegister>(sUrlApi2, registroSesion);
                postTask2.Wait();

                var result = postTask2.Result;
                proceso = result.IsSuccessStatusCode;
            }

            return proceso;
        }

        /// <summary>
        /// valida si el token esta activo
        /// </summary>
        /// <param name="token">valor del token a validar</param>
        /// <returns></returns>
        public bool ValidarSesion(string token)
        {
            cUsuario = new CUsuario();

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApi + "/api/login/validarToken/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseTask = client.GetAsync(sUrlApi2);
                responseTask.Wait();

                var result = responseTask.Result;
                string mensajes = string.Empty;
                return result.IsSuccessStatusCode;
            }

        }

        /// <summary>
        /// Consulta los usuarios registrados en el sistema
        /// </summary>
        /// <param name="iAplicativo">id del aplicativo alque se quiere acceder</param>
        /// <param name="sToken">token generado</param>
        /// <returns>arreglo tipo CUsuario con lainformacion del usuario consultado</returns>
        public CUsuario[] ConsultarUsuarios(int iAplicativo, string sToken)
        {
            List<CUsuario> lCUsuario = new List<CUsuario>();
            CUsuario[] cResultado = new CUsuario[0];
            //string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();

            try
            {
                using (var client = new HttpClient())
                {
                    string sUrlApi2 = sUrlApi + "/api/customers/lista/";
                    client.BaseAddress = new Uri(sUrlApi2);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sToken);

                    var responseTask = client.GetAsync(sUrlApi2 + iAplicativo.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Retorno consulta Json
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        // Deserializa el objeto
                        DataTable dtResultado = JsonConvert.DeserializeObject<DataTable>(json);

                        if (dtResultado.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtResultado.Rows)
                            {
                                CUsuario cUsuario = new CUsuario();
                                cUsuario.Identificador = long.Parse(r["Identificador"].ToString());
                                cUsuario.Correo = r["Correo"].ToString();
                                cUsuario.TipoDocumento = r["TipoDocumentoId"].ToString() == "" ? 0 : int.Parse(r["TipoDocumentoId"].ToString());
                                cUsuario.Documento = r["Documento"].ToString();
                                cUsuario.Nombre = r["Nombre"].ToString();
                                cUsuario.Apellido = r["Apellido"].ToString();

                                if (iAplicativo != 1) cUsuario.Activo = (bool)r["Activo"];
                                cUsuario.ListaPerfiles = ConsultarPerfilesDeUsuario(cUsuario.Identificador, iAplicativo, sToken);
                                cUsuario.Resultado = (int)Respuesta.OperacionCorrecta;
                                cUsuario.Mensajes = "Correcto";

                                lCUsuario.Add(cUsuario);
                            }
                            cResultado = lCUsuario.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CUsuario cUsuario = new CUsuario
                {
                    Resultado = (int)Respuesta.ErrorBaseDeDatos,
                    Mensajes = ex.Message + " - " + ex.StackTrace
                };
                lCUsuario.Add(cUsuario);
            }

            return cResultado;
        }


        /// <summary>
        /// Consulta el perfil del usuario
        /// </summary>
        /// <param name="lUsuario">log del usuario</param>
        /// <param name="iAplicativo">id del aplicativo al que se quiere acceder</param>
        /// <param name="sToken">tojken generado</param>
        /// <returns>lista de los perfiles del usuario</returns>
        private List<CPerfil> ConsultarPerfilesDeUsuario(long lUsuario, int iAplicativo, string sToken)
        {
            List<CPerfil> lCPerfil = new List<CPerfil>();
            //string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();

            try
            {
                using (var client = new HttpClient())
                {
                    string sUrlApi2 = sUrlApi + "/api/customers/perfiles/";
                    client.BaseAddress = new Uri(sUrlApi2);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sToken);

                    var responseTask = client.GetAsync(sUrlApi2 + lUsuario.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Retorno consulta Json
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        // Corrige saltos de linea y demas
                        string json = readTask.Result;

                        // Deserializa el objeto
                        DataTable dtResultado = JsonConvert.DeserializeObject<DataTable>(json);

                        if (dtResultado.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtResultado.Rows)
                            {
                                CPerfil cPerfil = new CPerfil();
                                if (r["Resultado"].ToString() == "Correcto")
                                {
                                    cPerfil.Identificador = int.Parse(r["IdPerfil"].ToString());
                                    cPerfil.IdAplicativo = int.Parse(r["IdAplicativo"].ToString());
                                    cPerfil.Codigo = r["Codigo"].ToString();
                                    cPerfil.Nombre = r["Nombre"].ToString();
                                    cPerfil.Descripcion = r["Descripcion"].ToString();
                                    cPerfil.Activo = bool.Parse(r["Activo"].ToString());
                                }
                                else
                                {
                                    cPerfil.Identificador = (int)Respuesta.ErrorValidacion;
                                    cPerfil.Mensajes = r["Mensaje"].ToString();
                                }

                                lCPerfil.Add(cPerfil);
                            }
                        }
                        else
                        {
                            CPerfil cPerfil = new CPerfil
                            {
                                Identificador = (int)Respuesta.NoSeEncontraronDatos,
                                Mensajes = "No hay Perfiles registrados para el usuario."
                            };
                            lCPerfil.Add(cPerfil);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CPerfil cPerfil = new CPerfil
                {
                    Identificador = (int)Respuesta.ErrorBaseDeDatos,
                    Mensajes = ex.Message + " - " + ex.StackTrace
                };
                lCPerfil.Add(cPerfil);
            }

            return lCPerfil;
        }

        /// <summary>
        /// consulta los tipo de documentos como CC o NIT - etc.
        /// </summary>
        /// <param name="lUsuario"></param>
        /// <param name="sToken"></param>
        /// <returns></returns>
        public List<CDocumento> ConsultarTipoDocumento(long iApp, string sToken)
        {
            List<CDocumento> lCTipoDocumento = new List<CDocumento>();

            try
            {
                using (var client = new HttpClient())
                {
                    string sUrlApi2 = sUrlApi + "/api/customers/tipodocumento/";
                    client.BaseAddress = new Uri(sUrlApi2);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sToken);

                    var responseTask = client.GetAsync(sUrlApi2 + iApp.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Retorno consulta Json
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        // Corrige saltos de linea y demas
                        string json = readTask.Result;

                        // Deserializa el objeto
                        DataTable dtResultado = JsonConvert.DeserializeObject<DataTable>(json);

                        if (dtResultado.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtResultado.Rows)
                            {
                                CDocumento cTipoDocumento = new CDocumento();

                                cTipoDocumento.tdoc_id_tipo_documento = int.Parse(r["tdoc_id_tipo_documento"].ToString());
                                cTipoDocumento.tdoc_vCodigo = r["tdoc_vCodigo"].ToString();

                                lCTipoDocumento.Add(cTipoDocumento);
                            }
                        }
                        else
                        {
                            CDocumento cMenu = new CDocumento
                            {
                                Identificador = (int)Respuesta.NoSeEncontraronDatos,
                                Mensajes = "No hay tipos de documentos ligados a esta aplicacion."
                            };
                            lCTipoDocumento.Add(cMenu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CDocumento cMenu = new CDocumento
                {
                    Identificador = (int)Respuesta.ErrorBaseDeDatos,
                    Mensajes = ex.Message + " - " + ex.StackTrace
                };
                lCTipoDocumento.Add(cMenu);
            }

            return lCTipoDocumento;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }


    }
}