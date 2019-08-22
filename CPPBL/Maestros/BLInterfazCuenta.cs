namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLInterfazCuenta
    {
        #region "Definicion de variables privadas"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesInterfazCuenta> lsInterfazCuenta = null;
        private EntitiesInterfazCuenta interfazCuenta = null;

        #endregion

        /// <summary>
        /// consulta las interfaces contables y las cuentas asociadas
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesInterfazCuenta> ConcultarIterfaz(DLAccesEntities parametros)
        {
            try
            {
                this.lsInterfazCuenta = new List<EntitiesInterfazCuenta>();
                DataTable iterfazTb = this.acceso.Consultas(parametros);

                if (iterfazTb != null && iterfazTb.Rows.Count > 0)
                {
                    foreach (DataRow fila in iterfazTb.Rows)
                    {
                        this.interfazCuenta = new EntitiesInterfazCuenta();
                        this.interfazCuenta.cppCPt_id = Convert.ToInt32(fila["cppCPt_id"].ToString());
                        this.interfazCuenta.cppIn_id = Convert.ToInt32(fila["cppIn_id"].ToString());
                        this.interfazCuenta.interfaz = fila["cppIn_descripcion"].ToString();
                        this.interfazCuenta.cppTp_id = Convert.ToInt32(fila["cppTp_id"].ToString());
                        this.interfazCuenta.cesion = fila["cppTc_Descripcion"].ToString();
                        this.interfazCuenta.cppTc_id = Convert.ToInt32(fila["cppTc_id"].ToString());
                        this.interfazCuenta.cuenta = fila["cppTct_Descripcion"].ToString();
                        this.interfazCuenta.cppCa_id = Convert.ToInt32(fila["cppCa_id"].ToString());
                        this.interfazCuenta.calificacion = fila["cppCl_Descripcion"].ToString();
                        this.interfazCuenta.cppCuentas = fila["cppCuentas"].ToString();

                        this.lsInterfazCuenta.Add(this.interfazCuenta);
                    }
                }
                return lsInterfazCuenta;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLInterfazCuenta - ConcultarIterfaz :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarInterfazCuentas(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLInterfazCuenta - RegistrarEditarInterfazCuentas :: " + ex.Message));
            }
        }
    }
}
