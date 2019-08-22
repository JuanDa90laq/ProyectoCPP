namespace CPPPresentacion.Generica
{
    using CPPBL.Transversal;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using Newtonsoft.Json;
    using SSO.Finagro;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Linq;

    public class Validaciones
    {
        private BLValidaciones_Logs logs = null;
        private string sUrlApiIBRDTF = ConfigurationManager.AppSettings["SERVICIOSCPP"].ToString();

        /// <summary>
        /// visualiza los mensajes de error e infomativos en las validaciones del form
        /// </summary>
        /// <param name="objeto">objeto de tipo ASPxPopupControl</param>
        /// <param name="mensaje">objeto de tipo ASPxLabel</param>
        ///<param name="cerrarSuperior">objeto de tipo ASPxButton</param>
        ///<param name="aceptar">Objeto de tipo ASPxButton</param>
        ///<param name="cancelar">Objeto de tipo ASPxButton</param>
        ///<param name="tipo"> 0 -> procesos fallidos Mensaje a mostrar, 1 - > procesos Exitosos, 2 -> Informacion</param>
        ///<param name="message">Mensaje a Mostrar</param>
        //////<param name="cerrarSuperior">objeto de tipo ASPxButton</param>
        public void Mensajes(ref DevExpress.Web.ASPxPopupControl objeto, ref DevExpress.Web.ASPxLabel mensaje,
                              ref DevExpress.Web.ASPxButton cerrarSuperior, ref DevExpress.Web.ASPxButton aceptar,
                              ref DevExpress.Web.ASPxButton cancelar, int tipo, string message)
        {
            switch (tipo)
            {
                case 0:

                    cerrarSuperior.Image.IconID = "actions_cancel_32x32office2013";
                    mensaje.Text = message;
                    mensaje.Text = mensaje.Text.Replace("*", "<br/> ");
                    mensaje.Text = mensaje.Text.Replace("+", "&nbsp;&nbsp;&nbsp;");
                    objeto.ShowCloseButton = true;
                    aceptar.Visible = false;
                    cancelar.Visible = false;
                    cerrarSuperior.Visible = true;
                    break;

                case 1:

                    cerrarSuperior.Image.IconID = "actions_apply_32x32office2013";
                    mensaje.Text = message;
                    mensaje.Text = mensaje.Text.Replace("*", "<br/> ");
                    mensaje.Text = mensaje.Text.Replace("+", "&nbsp;&nbsp;&nbsp;");
                    objeto.ShowCloseButton = true;
                    aceptar.Visible = false;
                    cancelar.Visible = false;
                    cerrarSuperior.Visible = true;
                    break;

                case 2:

                    cerrarSuperior.Image.IconID = "status_warning_32x32";
                    mensaje.Text = message;
                    mensaje.Text = mensaje.Text.Replace("*", "<br/> ");
                    mensaje.Text = mensaje.Text.Replace("+", "&nbsp;&nbsp;&nbsp;");
                    objeto.ShowCloseButton = true;
                    aceptar.Visible = false;
                    cancelar.Visible = false;
                    cerrarSuperior.Visible = true;
                    break;

                case 3:

                    cerrarSuperior.Image.IconID = "actions_apply_32x32office2013";
                    mensaje.Text = message;
                    mensaje.Text = mensaje.Text.Replace("*", "<br/> ");
                    mensaje.Text = mensaje.Text.Replace("+", "&nbsp;&nbsp;&nbsp;");
                    objeto.ShowCloseButton = false;
                    aceptar.Visible = true;
                    break;

            }
        }


        /// <summary>
        /// trae los departamentos
        /// </summary>
        /// <returns></returns>
        public List<Depatamentos> CargarDepartamentos()
        {
            string json = string.Empty;
            List<Depatamentos> resultDt = new List<Depatamentos>();

            using (var client = new HttpClient())
            {
                // Parametros de usuario al servicio de autenticación                
                string sUrlApi2 = this.sUrlApiIBRDTF + "/Finagro/Consultas/Departamentos/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());
                EntitieConsultarIBRDTF parametros = new EntitieConsultarIBRDTF { DTFfechaVigenciaDesde = null, DTFfechaVigenciaHasta = null, IBRfechaVigenciaDesde = null, IBRfechaVigenciaHasta = null };
                var postTask = client.PostAsJsonAsync<EntitieConsultarIBRDTF>(sUrlApi2, parametros);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    json = readTask.Result;
                    resultDt = JsonConvert.DeserializeObject<List<Depatamentos>>(json);
                }
            }

            return resultDt;
        }

        /// <summary>
        /// consulta los municipios y carga el combo municipios
        /// </summary>
        public List<Municipios> CargarMunicipios(int idDepto)
        {
            List<Municipios> lstMunicipios = new List<Municipios>();

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApiIBRDTF + "/Finagro/Consultas/Municipios/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());

                var responseTask = client.GetAsync(sUrlApi2 + idDepto.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Retorno consulta Json
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;
                    lstMunicipios = JsonConvert.DeserializeObject<List<Municipios>>(json);
                }
            }

            return lstMunicipios;
        }

        /// <summary>
        /// consulta las actividades agropecuarias
        /// </summary>
        public List<EntitiesActividadesAgropecuarias> CargarActividades()
        {
            List<EntitiesActividadesAgropecuarias> lstActividades = new List<EntitiesActividadesAgropecuarias>();

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApiIBRDTF + "/Finagro/Consultas/ActividadEconomica/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());

                var responseTask = client.GetAsync(sUrlApi2);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Retorno consulta Json
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;
                    lstActividades = JsonConvert.DeserializeObject<List<EntitiesActividadesAgropecuarias>>(json);
                    lstActividades = lstActividades.OrderBy(a => a.actividad).ToList();
                }
            }

            return lstActividades;
        }


        /// <summary>
        /// consulta los destinos de credito
        /// </summary>
        public List<EntitiesDestinos> DestinosCredito()
        {
            List<EntitiesDestinos> lstDestinos = new List<EntitiesDestinos>();

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApiIBRDTF + "/Finagro/Consultas/DestinosCredito/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());

                var responseTask = client.GetAsync(sUrlApi2);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Retorno consulta Json
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;
                    lstDestinos = JsonConvert.DeserializeObject<List<EntitiesDestinos>>(json);
                }
            }

            return lstDestinos;
        }

        #region "generacion logs"

        /// <summary>
        /// genera log en base de datos
        /// </summary>
        /// <param name="metodo">valor del metodo que se va a registrar como log</param>
        /// <param name="procedimiento">procedimeinto almacenado que se va a generar en el log</param>
        /// <param name="accion">accion a realizar</param>
        /// <param name="usuario">usaurio que efectual la accion</param>
        /// <param name="formulario">formulario que se esta trabajando</param>
        public void RegLog(string metodo, string procedimiento, string accion, int usuario, string formulario)
        {
            this.logs = new BLValidaciones_Logs();
            LogAccesodatos log = new LogAccesodatos();
            log.idUsuario = usuario;
            log.formulario = formulario;
            log.metodoEnAplicacion = metodo;
            log.procedimientoConsume = procedimiento;
            log.accion = accion;
            this.logs.RegistroLogs(log);
        }

        /// <summary>
        /// genera log en base de datos
        /// </summary>
        /// <param name="metodo">valor del metodo que se va a registrar como log</param>
        /// <param name="procedimiento">procedimeinto almacenado que se va a generar en el log</param>
        /// <param name="accion">accion a realizar</param>
        /// <param name="error">error generado</param>
        /// <param name="usuario">usaurio que efectual la accion</param>
        /// <param name="formulario">formulario que se esta trabajando</param>
        public void RegLogError(string metodo, string procedimiento, string accion, string error, int usuario, string formulario)
        {
            this.logs = new BLValidaciones_Logs();
            LogAccesodatos log = new LogAccesodatos();
            log.idUsuario = usuario;
            log.formulario = formulario;
            log.metodoEnAplicacion = metodo;
            log.procedimientoConsume = procedimiento;
            log.accion = accion;
            log.mensajeError = error;
            this.logs.GenerarLogError(log);
        }

        #endregion

        #region "logeo servicio CPP"

        /// <summary>
        /// se utiliza para realizar las peticiones al servicio res que consume CPP para todas sus consultas a la base de datos
        /// </summary>
        /// <returns></returns>
        private string LogServiciosCPP()
        {

            // Definición de variables
            string json = string.Empty;
            string token = string.Empty;

            using (var client = new HttpClient())
            {
                // Parametros de usuario al servicio de autenticación                
                string sUrlApi2 = this.sUrlApiIBRDTF + "/Finagro/login/authenticate";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var login = new LoginRequest() { Username = ConfigurationManager.AppSettings["JWT_USUARIO"].ToString(), Password = ConfigurationManager.AppSettings["JWT_PASSWORD"].ToString() };
                var postTask = client.PostAsJsonAsync<LoginRequest>(sUrlApi2, login);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Retorno consulta Json
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    json = readTask.Result;
                    token = JsonConvert.DeserializeObject<string>(json);
                }
            }
            return token;
        }

        #endregion

        #region "carga los valores de la DTF e IBR para el form CPPConceptosAnuales"

        /// <summary>
        /// carga los valores de la IBR y DTF
        /// </summary>
        /// <returns></returns>
        public List<EntitiesDTFIBR> CargarDTFIBR(DateTime? dTFfechaVigenciaDesde, DateTime? dTFfechaVigenciaHasta, DateTime? iBRfechaVigenciaDesde, DateTime? iBRfechaVigenciaHasta, int accion)
        {
            List<EntitiesDTFIBR> lstDtfIbr = new List<EntitiesDTFIBR>();
            EntitiesDTFIBR dtfIbr = new EntitiesDTFIBR();            
            string json = string.Empty;

            using (var client = new HttpClient())
            {
                // Parametros de usuario al servicio de autenticación                
                string sUrlApi2 = this.sUrlApiIBRDTF + "/Finagro/Consultas/Consulta_IBR_DTF/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());
                EntitieConsultarIBRDTF parametros = new EntitieConsultarIBRDTF { DTFfechaVigenciaDesde = dTFfechaVigenciaDesde, DTFfechaVigenciaHasta = dTFfechaVigenciaHasta, IBRfechaVigenciaDesde = iBRfechaVigenciaDesde, IBRfechaVigenciaHasta = iBRfechaVigenciaHasta, accion = accion };
                var postTask = client.PostAsJsonAsync<EntitieConsultarIBRDTF>(sUrlApi2, parametros);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    json = readTask.Result;
                    lstDtfIbr = JsonConvert.DeserializeObject<List<EntitiesDTFIBR>>(json);
                }
            }

            return lstDtfIbr;
        }

        /// <summary>
        /// carga los valores de la IBR y DTF
        /// </summary>
        /// <returns></returns>
        public DataTable CargarDTFIBRT()
        {
            string json = string.Empty;
            DataTable resultDt = new DataTable();

            using (var client = new HttpClient())
            {
                // Parametros de usuario al servicio de autenticación                
                string sUrlApi2 = this.sUrlApiIBRDTF + "/Finagro/Consultas/Consulta_IBR_DTF/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.LogServiciosCPP());
                EntitieConsultarIBRDTF parametros = new EntitieConsultarIBRDTF { DTFfechaVigenciaDesde = null, DTFfechaVigenciaHasta = null, IBRfechaVigenciaDesde = null, IBRfechaVigenciaHasta = null };
                var postTask = client.PostAsJsonAsync<EntitieConsultarIBRDTF>(sUrlApi2, parametros);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    json = readTask.Result;
                    resultDt = JsonConvert.DeserializeObject<DataTable>(json);
                }
            }

            return resultDt;
        }

        #endregion

    }

}