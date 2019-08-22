namespace CPPPresentacion.Maestros
{
    using CPPBL.Maestros;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using CPPENTL.Enumeraciones;
    using CPPPresentacion.Generica;
    using DevExpress.Web;
    using Newtonsoft.Json;
    using SSO.Finagro;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class CPPDatosBeneficiariosOriginal : System.Web.UI.Page
    {
        #region "definicion variables"

        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private BLDatosbeneficiario beneficiarioBl = null;
        private BLTipoProductor tipoproductBL = null;
        private ParametrizacionSPQUERY parametros = null;
        private List<EntitiesBeneficiarios> lstBeneficiarios = null;
        private ResultConsulta resultadoOperacion = null;
        private string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();

        #endregion

        #region "metodos protegidos"

        /// <summary>
        /// cuando se carga la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.formato.NumberDecimalSeparator = ".";
                this.formato.NumberGroupSeparator = ",";
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];

                if (!this.IsPostBack)
                {
                    this.CargaCombos();
                    this.CargarDepartmentos();
                    this.CargarMunicipios();
                    this.Cargaractividades();
                    this.txtNumDocumento.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.txIdentificacion.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.ValidarChecks();
                }
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        /// <summary>
        /// se ejecuta al dar clic al boton consultar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficiario();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Pagina) :: " + ex.Message);
            }
        }

        /// <summary>
        /// se ejecuta cuando se desea crear un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Limpiarobjetos();
                this.ValidarChecks();
                this.HabilitarObjetos(true);
                this.ppBeneficiario.HeaderText = "Beneficiario";
                this.ppBeneficiario.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        /// <summary>
        /// se dispara cuando se selecciona un depratamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargarMunicipio();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Departamentos):: " + ex.Message);
            }
        }

        /// <summary>
        /// se dispara cuandop se da clic al boton aceptar al registrar un nuevo beneficiario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtFechaExped.Text) || (!string.IsNullOrEmpty(this.txtFechaExped.Text) && string.IsNullOrEmpty(this.txFechaCorte.Text)))
                {
                    if (!string.IsNullOrEmpty(this.hditemSelec.Value))
                        this.EjeciconSQLEditInser();
                    else
                        this.ShowMensajes(2, "Beneficiario no tiene actividad económica asociada.");
                }
                else if (Convert.ToDateTime(this.txtFechaExped.Text) < Convert.ToDateTime(this.txFechaCorte.Text))
                {
                    if (!string.IsNullOrEmpty(this.hditemSelec.Value))
                        this.EjeciconSQLEditInser();
                    else
                        this.ShowMensajes(2, "Beneficiario no tiene actividad económica asociada.");
                }                
                else
                    this.ShowMensajes(2, "La fecha de expedición no puede ser mayor a la fecha de corte de activos.");
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ejecución) :: " + ex.Message);
            }
        }

        protected void gvBeneficiarios_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                ASPxButton btnEditar = new ASPxButton();
                ASPxButton btnEliminar = new ASPxButton();
                ASPxLabel lbIdentificadorAgro = new ASPxLabel();
                ASPxLabel lbActividad = new ASPxLabel();
                ASPxLabel lbId = new ASPxLabel();
                ASPxTextBox txtActividad = new ASPxTextBox();
                ASPxCheckBox ckEstadoIni = new ASPxCheckBox();
                RequiredFieldValidator validador = new RequiredFieldValidator();

                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        this.accesoDatos = new DLAccesEntities();
                        this.accesoDatos.procedimiento = "CPP_SP_ConsultarBeneficiario";
                        this.accesoDatos.tipoejecucion = 1;
                        this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppBn_Id";
                        this.parametros.parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0]);
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarBeneficiarioEdicion();
                        this.ppBeneficiario.HeaderText = "(Detalle-Edición) Beneficiario";
                        this.ppBeneficiario.ShowOnPageLoad = true;
                        this.ValidarChecks();

                        break;

                    case "MostrarActividades":

                        string[] idActividades = e.KeyValue.ToString().Split('|')[1].Split(',');
                        Session["idActividades"] = idActividades;
                        this.ckConsulta.DataSource = this.CargarActividad(idActividades);
                        this.ckConsulta.DataBind();
                        /*this.gvActividades.DataSource = this.CargarActividad(idActividades); 
                        this.gvActividades.DataBind();*/
                        this.ppActividadesAsociadas.ShowOnPageLoad = true;
                        ///ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), "MostrarActividadesGrid", "MostrarActividadesAsociadas()", true);

                        break;

                }
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        protected void gvBeneficiarios_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficiario();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvBeneficiarios_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficiario();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvBeneficiarios_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficiario();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtActividadFiltro.Text))
                {
                    this.CargarActividad();
                    this.ValidarChecks();
                }
                else
                    this.ShowMensajes(2, "Por favor diligencie el filtro.");
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        protected void btnAceptarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidarSeleccionActividadesEconomicasFinal())
                {
                    this.CargarActividadBeneficiario();
                    this.ppAsociarActividad.ShowOnPageLoad = false;
                }

                else
                    this.ShowMensajes(2, "Seleccione al menos una actividad econócmica.");
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            this.ppAsociarActividad.ShowOnPageLoad = true;
        }

        protected void btnTodosAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntitiesActividadesAgropecuarias> lstActividadesAgroAdd = new List<EntitiesActividadesAgropecuarias>();
                EntitiesActividadesAgropecuarias actividad = new EntitiesActividadesAgropecuarias();

                for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                    actividad.actividad = this.ckListActividades2.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }

                for (int i = 0; i < this.ckListActividades.Items.Count; i++)
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                    actividad.actividad = this.ckListActividades.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }

                lstActividadesAgroAdd = lstActividadesAgroAdd.OrderBy(a => a.actividad).ToList();
                this.ckListActividades2.Items.Clear();
                this.ckListActividades2.DataSource = lstActividadesAgroAdd;
                this.ckListActividades2.DataBind();

                this.ckListActividades.Items.Clear();
                this.ValidarChecks();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        protected void btnTodosLess_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntitiesActividadesAgropecuarias> lstActividadesAgroAdd = new List<EntitiesActividadesAgropecuarias>();
                EntitiesActividadesAgropecuarias actividad = new EntitiesActividadesAgropecuarias();

                for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                    actividad.actividad = this.ckListActividades2.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }

                for (int i = 0; i < this.ckListActividades.Items.Count; i++)
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                    actividad.actividad = this.ckListActividades.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }

                lstActividadesAgroAdd = lstActividadesAgroAdd.OrderBy(a => a.actividad).ToList();
                this.ckListActividades.Items.Clear();
                this.ckListActividades.DataSource = lstActividadesAgroAdd;
                this.ckListActividades.DataBind();

                this.ckListActividades2.Items.Clear();
                this.ValidarChecks();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        protected void btnless_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidarSeleccionActividadesEconomicas2())
                {
                    this.EliminarActividades();
                    this.ValidarChecks();
                }
                else
                    this.ShowMensajes(2, "Seleccione al menos una actividad econócmica.");
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.ValidarSeleccionActividadesEconomicas())
            {
                this.AdicionarActividades();
                this.ValidarChecks();
            }
            else
                this.ShowMensajes(2, "Seleccione al menos una actividad econócmica.");
        }

        protected void gvBeneficiarios_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType.Equals(DevExpress.Web.GridViewRowType.Data))
            {
                ASPxButton btnActivi = this.gvBeneficiarios.FindRowCellTemplateControl(e.VisibleIndex, null, "btnVerActividades") as ASPxButton;
                if (!string.IsNullOrEmpty(e.GetValue("actividad_Agropecuaria").ToString()))
                    btnActivi.Visible = true;                                    
                else
                    btnActivi.Visible = false;


            }
        }

        
        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga el combo que filtra las actividades
        /// </summary>
        private void CargaCombos()
        {
            CDocumento registro = new CDocumento { tdoc_id_tipo_documento = 1000, tdoc_vCodigo = string.Empty };
            List<CDocumento> lstDocumento = new List<CDocumento>();
            lstDocumento = this.usuarioCpp.ConsultarTipoDocumento(Convert.ToInt64(this.usuarioCpp.Usuario.iAplicacion), this.usuarioCpp.Usuario.Token);
            lstDocumento = lstDocumento.OrderBy(a => a.tdoc_vCodigo).ToList();
            this.cbTipoDoc.DataSource = lstDocumento;
            this.cbTipoDoc.DataBind();
            lstDocumento.Add(registro);
            lstDocumento = lstDocumento.OrderBy(a => a.tdoc_vCodigo).ToList();
            this.cbTipoDocumento.DataSource = lstDocumento;
            this.cbTipoDocumento.DataBind();
            Session["TipoDoc"] = lstDocumento;

            this.tipoproductBL = new BLTipoProductor();
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarTiposProductor";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.cbProductor.DataSource = this.tipoproductBL.ConsultaProductores(this.accesoDatos);
            this.cbProductor.DataBind();
        }

        /// <summary>
        /// muestra los mensajes al usuario
        /// </summary>
        /// <param name="tipo">tipod e operacion a realizar 0 -> procesos fallidos Mensaje a mostrar, 1 - > procesos Exitosos, 2 -> Informacion</param>
        /// <param name="mensaje">mensaje</param>
        private void ShowMensajes(int tipo, string mensaje)
        {
            this.validar.Mensajes(ref this.ppMensajesConfirmacion, ref this.lbMensaje, ref this.btCerrarSuperior, ref this.dvBtnAceptar, ref this.dvBtnCancelar, tipo, mensaje);
            this.ppMensajesConfirmacion.ShowOnPageLoad = true;
        }

        /// <summary>
        /// consulta las actividades agropecuarias registradas en la base de datos
        /// </summary>
        private void ConsultarBeneficiarios()
        {
            this.beneficiarioBl = new BLDatosbeneficiario();
            this.lstBeneficiarios = new List<EntitiesBeneficiarios>();
            string valor = this.cbTipoDocumento.SelectedItem != null ? string.IsNullOrEmpty(this.cbTipoDocumento.SelectedItem.Value.ToString()) ? "-1" : this.cbTipoDocumento.SelectedItem.Value.ToString() : this.cbTipoDocumento.SelectedItem.Value.ToString();
            string valor2 = this.txtNumDocumento.Text.Trim().Equals(string.Empty) ? "-1" : this.txtNumDocumento.Text.Trim();
            this.accesoDatos = new DLAccesEntities(); ;
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.parametros = new ParametrizacionSPQUERY() { parametro = "@cppBn_IdTipoiden", parametroValor = valor };
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY() { parametro = "@cppBn_Identificacion", parametroValor = valor2 };
            this.accesoDatos.parametros.Add(this.parametros);
            this.lstBeneficiarios = this.beneficiarioBl.Consultarbeneficiarios(this.accesoDatos);

            this.HomologarCodigoActividades();
            this.HomologarCodigoProductores();

            this.gvBeneficiarios.DataSource = this.lstBeneficiarios;
            this.gvBeneficiarios.DataBind();

            if (this.lstBeneficiarios.Count > 0)
                this.divConsulta.Visible = true;
            else
            {
                this.divConsulta.Visible = false;
                this.ShowMensajes(2, "No hay datos para mostrar");
            }
        }

        /// <summary>
        /// coloca los nombres de las actividades
        /// </summary>
        private void HomologarCodigoActividades()
        {
            BLActividadAgropecuaria actividadesAgroBL = new BLActividadAgropecuaria();
            List<EntitiesActividadesAgropecuarias> lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@id_Actividad";
            this.parametros.parametroValor = "0";
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.parametros.Add(this.parametros);
            lstActividadesAgro = actividadesAgroBL.ConsultarActividadAgropecuaria(this.accesoDatos);

            foreach (EntitiesBeneficiarios item in this.lstBeneficiarios)
            {
                item.actividad = lstActividadesAgro.Where(c => c.id.Equals(item.actividad_Agropecuaria)).Select(a => a.actividad).ToString();
            }
        }

        /// <summary>
        /// coloca los nombres de los tipos de productor
        /// </summary>
        private void HomologarCodigoProductores()
        {
            BLTipoProductor productoresBL = new BLTipoProductor();
            List<EntitiesTipoProductor> lstproductores = new List<EntitiesTipoProductor>();
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@id_Actividad";
            this.parametros.parametroValor = "0";
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.parametros.Add(this.parametros);
            lstproductores = productoresBL.ConsultaProductores(this.accesoDatos);

            foreach (EntitiesBeneficiarios item in this.lstBeneficiarios)
            {
                item.productor = lstproductores.Where(c => c.id.Equals(item.tipo_Productor)).Select(a => a.productor).ToString();
            }
        }

        /// <summary>
        /// consulta los departamentos y carga el combo deptos
        /// </summary>
        private void CargarDepartmentos()
        {

            List<Depatamentos> lstDeptos = new List<Depatamentos>();
            Depatamentos departamento = null;

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApi + "/api/customers/Departamentos/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.usuarioCpp.Usuario.Token);

                var responseTask = client.GetAsync(sUrlApi2);
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
                            departamento = new Depatamentos();
                            departamento.id = Convert.ToInt32(r[0].ToString());
                            departamento.depratamento = r[1].ToString();
                            lstDeptos.Add(departamento);
                        }
                    }
                    Session["Depto"] = lstDeptos;
                    this.cbDepartamento.DataSource = lstDeptos;
                    this.cbDepartamento.DataBind();
                }
            }
        }

        /// <summary>
        /// consulta los departamentos y carga el combo deptos
        /// </summary>
        private void CargarMunicipios()
        {

            List<Municipios> lstMunicipios = new List<Municipios>();
            Municipios municipio = null;

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApi + "/api/customers/Municipios/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.usuarioCpp.Usuario.Token);

                var responseTask = client.GetAsync(sUrlApi2 + "0");
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
                            municipio = new Municipios();
                            municipio.id = Convert.ToInt32(r[0].ToString());
                            municipio.municipio = r[1].ToString();
                            municipio.idDepto = Convert.ToInt32(r[2].ToString());
                            lstMunicipios.Add(municipio);
                        }
                    }

                    Session["municipios"] = lstMunicipios;
                }
            }
        }

        /// <summary>
        /// li´mpia los objetos del form
        /// </summary>
        private void Limpiarobjetos()
        {
            this.hdIdBeneficiario.Value = string.Empty;
            this.cbTipoDoc.SelectedIndex = -1;
            this.txIdentificacion.Text = string.Empty;
            this.txtFechaExped.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtApellido.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtCelular.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.cbProductor.SelectedIndex = -1;
            this.txMontoActivos.Text = string.Empty;
            this.txFechaCorte.Text = string.Empty;
            this.cbDepartamento.SelectedIndex = -1;
            this.cbMunicipio.Items.Clear();
            this.cbMunicipio.Text = string.Empty;
            this.txtActividadFiltro.Text = string.Empty;
            this.hditemSelec.Value = string.Empty;
            this.ckListActividades.Items.Clear();
            this.ckListActividades2.Items.Clear();
            this.actividadesEco.Visible = false;
            this.ckConsulta.DataSource = null;
            this.ckConsulta.DataBind();
            this.ckConsulta.Items.Clear();
            /*this.gvActividades.DataSource = null;
            this.gvActividades.DataBind();*/
            this.btnAceptarFiltro.Visible = false;
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjeciconSQLEditInser()
        {
            int TipoDocumento = 0;
            long identificacion = 0;
            this.accesoDatos = new DLAccesEntities();
            this.beneficiarioBl = new BLDatosbeneficiario();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarBeneficiario";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdBeneficiario.Value))
            {
                this.parametros.parametro = "@cppBn_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdBeneficiario.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_IdTipoiden";
            this.parametros.parametroValor = Convert.ToInt32(this.cbTipoDoc.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);
            TipoDocumento = Convert.ToInt32(this.cbTipoDoc.SelectedItem.Value.ToString().Trim());

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_Identificacion";
            this.parametros.parametroValor = Convert.ToInt64(this.txIdentificacion.Text.Trim());
            this.accesoDatos.parametros.Add(this.parametros);
            identificacion = Convert.ToInt64(this.txIdentificacion.Text.Trim());

            if (!string.IsNullOrEmpty(this.txtFechaExped.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_FechaExpedicion";
                this.parametros.parametroValor = Convert.ToDateTime(this.txtFechaExped.Text.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_Nombre";
            this.parametros.parametroValor = this.txtNombre.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_Apellido";
            this.parametros.parametroValor = this.txtApellido.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.txtDireccion.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_Direccion";
                this.parametros.parametroValor = this.txtDireccion.Text.Trim();
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtTelefono.Text.Trim()))
            {
                this.txtTelefono.Text = this.txtTelefono.Text.Replace(" ", "");
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_Telefono";
                this.parametros.parametroValor = Convert.ToInt64(this.txtTelefono.Text);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtCelular.Text.Trim()))
            {
                this.txtCelular.Text = this.txtCelular.Text.Replace(" ", "");
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_Celular";
                this.parametros.parametroValor = Convert.ToInt64(this.txtCelular.Text);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtEmail.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_email";
                this.parametros.parametroValor = this.txtEmail.Text.Trim();
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txMontoActivos.Text.Trim()))
            {
                this.txMontoActivos.Text = this.txMontoActivos.Text.Trim().Replace(",", "");
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_MontoActivos";
                this.parametros.parametroValor = Convert.ToDecimal(this.txMontoActivos.Text.Trim(), this.formato);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txFechaCorte.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_FechaCorteActivos";
                this.parametros.parametroValor = Convert.ToDateTime(this.txFechaCorte.Text.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_idproduc";
            this.parametros.parametroValor = Convert.ToInt32(this.cbProductor.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_IdActividadAgro";
            this.parametros.parametroValor = this.hditemSelec.Value;
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_IdDepto";
            this.parametros.parametroValor = Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppBn_Idmun";
            this.parametros.parametroValor = Convert.ToInt32(this.cbMunicipio.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = string.IsNullOrEmpty(this.hdIdBeneficiario.Value) ? "@cppBn_UsuarioCrea" : "@cppBn_UsuarioMod";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = string.IsNullOrEmpty(this.hdIdBeneficiario.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false");
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                this.accesoDatos.procedimiento = "CPP_SP_ConsultarBeneficiario";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_IdTipoiden";
                this.parametros.parametroValor = Convert.ToInt32(TipoDocumento.ToString());
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_Identificacion";
                this.parametros.parametroValor = Convert.ToInt64(identificacion.ToString());
                this.accesoDatos.parametros.Add(this.parametros);

                this.ConsultarBeneficiario();
                this.ppBeneficiario.ShowOnPageLoad = false;
            }
        }

        /// <summary>
        /// consulta beneficiarios
        /// </summary>
        private void ConsultarBeneficiario()
        {
            this.beneficiarioBl = new BLDatosbeneficiario();
            this.lstBeneficiarios = new List<EntitiesBeneficiarios>();
            this.lstBeneficiarios = this.beneficiarioBl.Consultarbeneficiarios(this.accesoDatos);

            if (this.lstBeneficiarios.Count > 0)
            {
                foreach (EntitiesBeneficiarios registro in this.lstBeneficiarios)
                {
                    registro.departamento = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(registro.idDepartamento)).Select(a => a.depratamento).FirstOrDefault();
                    registro.municipio = ((List<Municipios>)Session["municipios"]).Where(a => a.id.Equals(registro.idMunicipio) && a.idDepto.Equals(registro.idDepartamento)).Select(a => a.municipio).FirstOrDefault();
                    registro.Documento = ((List<CDocumento>)Session["TipoDoc"]).Where(a => a.tdoc_id_tipo_documento.Equals(registro.tipo_Documento)).Select(a => a.tdoc_vCodigo).FirstOrDefault();
                }
                this.gvBeneficiarios.DataSource = this.lstBeneficiarios;
                this.gvBeneficiarios.DataBind();
                this.divConsulta.Visible = true;
            }
            else
            {
                this.divConsulta.Visible = false;
                this.ShowMensajes(1, "No hay información para consultar.");
            }
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
        {
            this.beneficiarioBl = new BLDatosbeneficiario();
            this.resultadoOperacion = this.beneficiarioBl.RegistrarEditarBeneficiario(this.accesoDatos);

            if (this.resultadoOperacion.estado.Equals(1))
            {
                this.ShowMensajes(1, "Operación Exitosa.");
                return true;
            }
            else
            {
                this.ShowMensajes(0, "Problemas en la operacion :: " + this.resultadoOperacion.mensaje);
                return false;
            }
        }

        /// <summary>
        /// configura la cosulta
        /// </summary>
        private void ConfigurarConsulta()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarBeneficiario";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (this.cbTipoDocumento.SelectedItem != null && !Convert.ToInt32(this.cbTipoDocumento.SelectedItem.Value).Equals(1000))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_IdTipoiden";
                this.parametros.parametroValor = Convert.ToInt32(this.cbTipoDocumento.SelectedItem.Value.ToString().Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtNumDocumento.Text))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppBn_Identificacion";
                this.parametros.parametroValor = Convert.ToInt64(this.txtNumDocumento.Text.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }
        }

        /// <summary>
        /// consulta beneficiarios Edición
        /// </summary>
        private void ConsultarBeneficiarioEdicion()
        {
            this.beneficiarioBl = new BLDatosbeneficiario();
            this.lstBeneficiarios = new List<EntitiesBeneficiarios>();
            this.lstBeneficiarios = this.beneficiarioBl.Consultarbeneficiarios(this.accesoDatos);

            foreach (EntitiesBeneficiarios registro in this.lstBeneficiarios)
            {
                registro.departamento = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(registro.idDepartamento)).Select(a => a.depratamento).FirstOrDefault();
                registro.municipio = ((List<Municipios>)Session["municipios"]).Where(a => a.id.Equals(registro.idMunicipio) && a.idDepto.Equals(registro.idDepartamento)).Select(a => a.municipio).FirstOrDefault();
                registro.Documento = ((List<CDocumento>)Session["TipoDoc"]).Where(a => a.tdoc_id_tipo_documento.Equals(registro.tipo_Documento)).Select(a => a.tdoc_vCodigo).FirstOrDefault();
            }

            this.Limpiarobjetos();
            this.hdIdBeneficiario.Value = this.lstBeneficiarios[0].identificador.ToString();
            this.cbTipoDoc.SelectedIndex = this.cbTipoDoc.Items.FindByText(this.lstBeneficiarios[0].Documento).Index;
            this.txIdentificacion.Text = this.lstBeneficiarios[0].identificacion.ToString();
            this.txtFechaExped.Text = this.lstBeneficiarios[0].fecha_Expedicion.ToString();
            this.txtNombre.Text = this.lstBeneficiarios[0].nombre.ToString();
            this.txtApellido.Text = this.lstBeneficiarios[0].apellido.ToString();
            this.txtApellido.Text = this.lstBeneficiarios[0].apellido.ToString();
            this.txtTelefono.Text = this.lstBeneficiarios[0].telefono.ToString();
            this.txtCelular.Text = this.lstBeneficiarios[0].celular.ToString();
            this.txtDireccion.Text = this.lstBeneficiarios[0].direccion.ToString();
            this.txtEmail.Text = this.lstBeneficiarios[0].correo.ToString();
            this.cbProductor.SelectedIndex = this.cbProductor.Items.FindByText(this.lstBeneficiarios[0].productor).Index;
            this.txMontoActivos.Text = string.IsNullOrEmpty(this.lstBeneficiarios[0].montos_Activos.ToString()) ? string.Empty : String.Format("{0:0,0.00}", Convert.ToDecimal(this.lstBeneficiarios[0].montos_Activos.ToString())).Replace(",", "-").Replace(".", ",").Replace("-", ".");
            this.txFechaCorte.Text = this.lstBeneficiarios[0].fecha_Corte_Activos.ToString();
            this.cbDepartamento.SelectedIndex = this.cbDepartamento.Items.FindByText(this.lstBeneficiarios[0].departamento).Index;
            this.CargarMunicipio();
            this.cbMunicipio.SelectedIndex = this.cbMunicipio.Items.FindByText(this.lstBeneficiarios[0].municipio).Index;
            this.HabilitarObjetos(false);

            string[] idActividades = this.lstBeneficiarios[0].actividad.Split(',');
            List<EntitiesActividadesAgropecuarias> lstActividadesAgro = this.CargarActividad(idActividades);
            
            if (!idActividades.Length.Equals(0))
            {
                this.ckListActividades2.DataSource = lstActividadesAgro;
                this.ckListActividades2.DataBind();
                this.actividadesEco.Visible = true;
                foreach(EntitiesActividadesAgropecuarias registro in lstActividadesAgro)
                    this.hditemSelec.Value = string.IsNullOrEmpty(this.hditemSelec.Value) ? registro.id.ToString() : this.hditemSelec.Value + "," + registro.id.ToString();
            }
        }

        /// <summary>
        /// carga los municipios en el combo municipio
        /// </summary>
        private void CargarMunicipio()
        {
            this.cbMunicipio.Items.Clear();
            List<Municipios> lstMunicipios = new List<Municipios>();
            lstMunicipios = (List<Municipios>)Session["municipios"];
            this.cbMunicipio.DataSource = lstMunicipios.Where(a => a.idDepto.Equals(Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString())));
            this.cbMunicipio.DataBind();
            this.cbMunicipio.SelectedIndex = -1;
        }

        /// <summary>
        /// carga las actividades
        /// </summary>
        private void CargarActividad()
        {
            List<EntitiesActividadesAgropecuarias> lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();            
            string[] busqueda = this.txtActividadFiltro.Text.TrimEnd().TrimStart().Split(',');

            foreach (string dato in busqueda)
                lstActividadesAgro = lstActividadesAgro.Concat(this.ConsultarActividades(dato)).ToList();

            lstActividadesAgro = lstActividadesAgro.OrderBy(a => a.id).ThenBy(a => a.actividad).ToList();
            if (lstActividadesAgro.Count > 0)
            {
                this.actividadesEco.Visible = true;
                this.ckListActividades.DataSource = lstActividadesAgro;
                this.ckListActividades.DataBind();
                this.ControlCargueFiltro();                
            }
            else
            {
                this.actividadesEco.Visible = false;
                this.btnAceptarFiltro.Visible = false;
                this.ShowMensajes(1, "No existen registros con el filtro configurado.");
            }
        }

        /// <summary>
        /// consulta las actividades economicas
        /// </summary>
        /// <returns></returns>
        private List<EntitiesActividadesAgropecuarias> ConsultarActividades(string filtro)
        {
            List<EntitiesActividadesAgropecuarias> lstActividades = new List<EntitiesActividadesAgropecuarias>();
            if(string.IsNullOrEmpty(filtro))
                lstActividades = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).ToList();
            else
                lstActividades = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).ToList().Where(a => a.actividad.ToLower().Contains(filtro.ToLower())).ToList();

            return lstActividades;
        }

        /// <summary>
        /// habilita la caja de texto del número de identificacion
        /// </summary>
        /// <param name="enabled">true o false</param>
        private void HabilitarObjetos(bool enabled)
        {
            this.txIdentificacion.Enabled = enabled;
        }

        /// <summary>
        /// valida si se ha seleccionado una actividad económica
        /// </summary>
        /// <returns></returns>
        private bool ValidarSeleccionActividadesEconomicas()
        {
            for (int i = 0; i < this.ckListActividades.Items.Count; i++)
            {
                if (this.ckListActividades.Items[i].Selected.Equals(true))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// valida si existen actividades seleccioandas
        /// </summary>
        /// <returns></returns>
        private bool ValidarSeleccionActividadesEconomicasFinal()
        {
            if (this.ckListActividades2.Items.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// valida si se ha seleccionado una actividad económica
        /// </summary>
        /// <returns></returns>
        private bool ValidarSeleccionActividadesEconomicas2()
        {

            for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
            {
                if (this.ckListActividades2.Items[i].Selected.Equals(true))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// preasocia las actividades al beneficiario antes del almacenamiento final
        /// </summary>
        private void CargarActividadBeneficiario()
        {
            this.hditemSelec.Value = string.Empty;
            for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
                this.hditemSelec.Value = string.IsNullOrEmpty(this.hditemSelec.Value) ? this.ckListActividades2.Items[i].Value : this.hditemSelec.Value + "," + this.ckListActividades2.Items[i].Value;
        }

        /// <summary>
        /// adiciona las actividades economicas
        /// </summary>
        private void AdicionarActividades()
        {
            List<EntitiesActividadesAgropecuarias> lstActividadesAgroAdd = new List<EntitiesActividadesAgropecuarias>();
            List<EntitiesActividadesAgropecuarias> lstActividadesAgroless = new List<EntitiesActividadesAgropecuarias>();
            EntitiesActividadesAgropecuarias actividad = new EntitiesActividadesAgropecuarias();

            for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
            {
                actividad = new EntitiesActividadesAgropecuarias();
                actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                actividad.actividad = this.ckListActividades2.Items[i].Text;
                lstActividadesAgroAdd.Add(actividad);
            }

            for (int i = 0; i < this.ckListActividades.Items.Count; i++)
            {
                if (this.ckListActividades.Items[i].Selected.Equals(true))
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                    actividad.actividad = this.ckListActividades.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }
                else
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                    actividad.actividad = this.ckListActividades.Items[i].Text;
                    lstActividadesAgroless.Add(actividad);
                }
            }

            this.ckListActividades2.Items.Clear();
            this.ckListActividades2.DataSource = lstActividadesAgroAdd.OrderBy(a => a.actividad).ToList();
            this.ckListActividades2.DataBind();

            this.ckListActividades.Items.Clear();
            this.ckListActividades.DataSource = lstActividadesAgroless.OrderBy(a => a.actividad).ToList();
            this.ckListActividades.DataBind();
        }

        /// <summary>
        /// quita las actividades seleccionadas
        /// </summary>
        private void EliminarActividades()
        {
            List<EntitiesActividadesAgropecuarias> lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();
            List<EntitiesActividadesAgropecuarias> lstActividadesAgroAdd = new List<EntitiesActividadesAgropecuarias>();
            EntitiesActividadesAgropecuarias actividad = new EntitiesActividadesAgropecuarias();


            for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
            {
                if (this.ckListActividades2.Items[i].Selected.Equals(true))
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                    actividad.actividad = this.ckListActividades2.Items[i].Text;
                    lstActividadesAgro.Add(actividad);
                }
                else
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                    actividad.actividad = this.ckListActividades2.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }
            }

            for (int i = 0; i < this.ckListActividades.Items.Count; i++)
            {
                actividad = new EntitiesActividadesAgropecuarias();
                actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                actividad.actividad = this.ckListActividades.Items[i].Text;

                lstActividadesAgro.Add(actividad);
            }

            this.ckListActividades.Items.Clear();
            this.ckListActividades.DataSource = lstActividadesAgro.OrderBy(a => a.actividad).ToList();
            this.ckListActividades.DataBind();

            this.ckListActividades2.Items.Clear();
            this.ckListActividades2.DataSource = lstActividadesAgroAdd.OrderBy(a => a.actividad).ToList();
            this.ckListActividades2.DataBind();
        }

        /// <summary>
        /// realiza el paso de items de la lista de seleccion a la de agregacion
        /// </summary>
        private void ControlCargueFiltro()
        {
            List<EntitiesActividadesAgropecuarias> lstActividadesAgroAdd = new List<EntitiesActividadesAgropecuarias>();
            List<EntitiesActividadesAgropecuarias> lstActividadesAgroless = new List<EntitiesActividadesAgropecuarias>();
            EntitiesActividadesAgropecuarias actividad = new EntitiesActividadesAgropecuarias();

            for (int i = 0; i < this.ckListActividades2.Items.Count; i++)
            {
                actividad = new EntitiesActividadesAgropecuarias();
                actividad.id = Convert.ToInt32(this.ckListActividades2.Items[i].Value);
                actividad.actividad = this.ckListActividades2.Items[i].Text;
                lstActividadesAgroless.Add(actividad);
            }

            for (int i = 0; i < this.ckListActividades.Items.Count; i++)
            {
                if (lstActividadesAgroless.Where(a => a.id.Equals(Convert.ToInt32(this.ckListActividades.Items[i].Value))).Count().Equals(0))
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad.id = Convert.ToInt32(this.ckListActividades.Items[i].Value);
                    actividad.actividad = this.ckListActividades.Items[i].Text;
                    lstActividadesAgroAdd.Add(actividad);
                }
            }

            this.ckListActividades.Items.Clear();
            if (lstActividadesAgroAdd.Count > 0)
            {
                this.btnAceptarFiltro.Visible = true;
                this.ckListActividades.DataSource = lstActividadesAgroAdd.OrderBy(a => a.actividad);
                this.ckListActividades.DataBind();
            }
        }

        /// <summary>
        /// realiza el carge general de las actividades agropecuarias
        /// </summary>
        private void Cargaractividades()
        {
            List<EntitiesActividadesAgropecuarias> lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();
            EntitiesActividadesAgropecuarias actividadesAgro = null;

            /*BLActividadAgropecuaria actividadesAgroBL = new BLActividadAgropecuaria();
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            lstActividadesAgro = actividadesAgroBL.ConsultarActividadAgropecuaria(this.accesoDatos);
            lstActividadesAgro = lstActividadesAgro.OrderBy(a => a.id).ThenBy(a => a.actividad).ToList();
            Session["Actividades"] = lstActividadesAgro;*/

            /*List<Depatamentos> lstDeptos = new List<Depatamentos>();
            Depatamentos departamento = null;*/

            using (var client = new HttpClient())
            {
                string sUrlApi2 = sUrlApi + "/api/customers/DestinosCredito/";
                client.BaseAddress = new Uri(sUrlApi2);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.usuarioCpp.Usuario.Token);

                var responseTask = client.GetAsync(sUrlApi2);
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
                            actividadesAgro = new EntitiesActividadesAgropecuarias();
                            actividadesAgro.id = Convert.ToInt32(r[0].ToString());
                            actividadesAgro.actividad = r[1].ToString();
                            lstActividadesAgro.Add(actividadesAgro);
                        }
                    }
                    Session["Actividades"] = lstActividadesAgro;
                }
            }
        }

        /// <summary>
        /// realiza el carge general de las actividades agropecuarias filtradas
        /// </summary>
        private List<EntitiesActividadesAgropecuarias> CargarActividad(string[] idActividades)
        {
            List<EntitiesActividadesAgropecuarias> lstActividades = null;

            if (!idActividades.Length.Equals(0))
            {
                lstActividades = new List<EntitiesActividadesAgropecuarias>();
                EntitiesActividadesAgropecuarias actividad = null;

                foreach (string id in idActividades)
                {
                    actividad = new EntitiesActividadesAgropecuarias();
                    actividad = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).Where(a => a.id.Equals(Convert.ToInt32(id))).ToList().FirstOrDefault();
                    lstActividades.Add(actividad);
                }
            }

            return lstActividades;
        }

        /// <summary>
        /// valida la actividad de los botones dependiendo de si existe data o no en las lsitas de seleccion
        /// </summary>
        private void ValidarChecks()
        {
            if (this.ckListActividades2.Items.Count > 0)
            {
                this.btnAceptarFiltro.Visible = true;
                this.btnless.Enabled = true;
                this.btnTodosLess.Enabled = true;
            }
            else
            {
                this.btnAceptarFiltro.Visible = false;
                this.btnless.Enabled = false;
                this.btnTodosLess.Enabled = false;
            }

            if (this.ckListActividades.Items.Count > 0)
            {
                this.btnAdd.Enabled = true;
                this.btnTodosAdd.Enabled = true;
            }
            else
            {
                this.btnAdd.Enabled = false;
                this.btnTodosAdd.Enabled = false;
            }
        }

        #endregion

        /*protected void gvActividades_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.gvActividades.DataSource = this.CargarActividad((String[])Session["idActividades"]);
                this.gvActividades.DataBind();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void gvActividades_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.gvActividades.DataSource = this.CargarActividad((String[])Session["idActividades"]);
                this.gvActividades.DataBind();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvActividades_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.gvActividades.DataSource = this.CargarActividad((String[])Session["idActividades"]);
                this.gvActividades.DataBind();                
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }*/
    }
}