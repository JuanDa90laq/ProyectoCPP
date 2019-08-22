namespace CPPPresentacion
{
    using CPPBL.Maestros;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;

    public partial class SiteMaster : MasterPage
    {
        private DLAccesEntities accesoDatosEntity = new DLAccesEntities();
        private EntitiesUsuarios usuariosEntity = new EntitiesUsuarios();
        private ParametrizacionSPQUERY parametros = new ParametrizacionSPQUERY();
        private BLUsuarios usuariosBL = new BLUsuarios();
        private BLMenus menuBL = new BLMenus();
        private EntitiesMenus menuEntity = new EntitiesMenus();
        private List<EntitiesMenus> lstMenu = new List<EntitiesMenus>();

        protected void Page_Load(object sender, EventArgs e)
        {
            /*lblMensaje.Text = "Lo sentimos, no tienes permisos suficientes para " +
                "vizualizar la pagina " + (Session["SPF-AccesoDenegadoMensaje"] ?? " que intentas cargar ") + ". Por favor comunicate con el " +
                "Area encargada de administrar el Aplicativo.";*/
            this.CargarUsuarios();
            this.mMenu.DataSource = new HierarchicalDataSet(this.ConvertiRMenuTabla(this.CargarMenus()), "idMenu", "idMenuPadre");
            this.mMenu.DataBind();
        }

        private void CargarUsuarios()
        {
            this.accesoDatosEntity.procedimiento = "CPP_SP_ConsultarUsuario";
            this.accesoDatosEntity.tipoejecucion = 1;
            this.accesoDatosEntity.parametros = new List<ParametrizacionSPQUERY>();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@usrc_vCorreo";
            this.parametros.parametroValor = "fmarquez@finagro.com.co";
            this.accesoDatosEntity.parametros.Add(this.parametros);
            this.usuariosEntity = usuariosBL.ConsultarUsuario(this.accesoDatosEntity);
        }

        private List<EntitiesMenus> CargarMenus()
        {
            this.accesoDatosEntity.procedimiento = "CPP_SP_ConsultarOpcionesMenu";
            this.accesoDatosEntity.tipoejecucion = 1;
            this.accesoDatosEntity.parametros = new List<ParametrizacionSPQUERY>();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@usrc_id";
            this.parametros.parametroValor = this.usuariosEntity.cppUsr_Idusuario.ToString();
            this.accesoDatosEntity.parametros.Add(this.parametros);
            return menuBL.Consultarmenus(this.accesoDatosEntity);
        }

        private DataSet ConvertiRMenuTabla(List<EntitiesMenus> menus)
        {
            DataSet tablas = new DataSet("Tables");
            DataTable menu = new DataTable("menuTabla");
            menu.Columns.Add("idMenu", typeof(int));
            menu.Columns.Add("idMenuPadre", typeof(int));
            menu.Columns.Add("Text", typeof(string));
            menu.Columns.Add("url", typeof(string));

            foreach (EntitiesMenus rows in menus)
            {
                DataRow fila = menu.NewRow();
                fila["idMenu"] = Convert.ToInt32(rows.cppMn_IdMenu);
                if(!rows.cppMn_idPadre.Equals(0))
                    fila["idMenuPadre"] = rows.cppMn_idPadre;
                fila["Text"] = rows.cppMn_Texto;
                fila["url"] = rows.cppMn_url;                
                menu.Rows.Add(fila);
            }

            // Menú de opciones del usuario Prueba
            /*DataRow filaMenu = menu.NewRow();
            filaMenu["IdMenu"] = 999999;
            filaMenu["Text"] = "Adminsitracion";
            menu.Rows.Add(filaMenu);

            filaMenu = menu.NewRow();
            filaMenu["IdMenu"] = 99997;
            filaMenu["IdMenuPadre"] = 999999;
            filaMenu["Text"] = "Mis datos";
            menu.Rows.Add(filaMenu);

            filaMenu = menu.NewRow();
            filaMenu["IdMenu"] = 99998;
            filaMenu["IdMenuPadre"] = 999999;
            filaMenu["Text"] = "Cambio de Contraseña";
            menu.Rows.Add(filaMenu);

            filaMenu = menu.NewRow();
            filaMenu["IdMenu"] = 99999;
            filaMenu["IdMenuPadre"] = 999999;
            filaMenu["Text"] = "Cerrar Sesión";
            menu.Rows.Add(filaMenu);

            filaMenu = menu.NewRow();
            filaMenu["IdMenu"] = 99998;
            filaMenu["IdMenuPadre"] = 99997;
            filaMenu["Text"] = "Datos de ella";
            menu.Rows.Add(filaMenu);*/

            tablas.Tables.Add(menu);

            return tablas;
        }
    }
}