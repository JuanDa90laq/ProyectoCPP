namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BLBancoCuenta
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesBancoCuenta> lstBancoCuenta = null;
        private EntitiesBancoCuenta bancoCuenta = null;

        #endregion

        #region "Metodos publicos"

        /// <summary>
        /// consulta las relaciones de los bancos con las cuentas contables
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesBancoCuenta> ConsultarBancoCuenta(DLAccesEntities parametros)
        {
            try
            {
                this.lstBancoCuenta = new List<EntitiesBancoCuenta>();
                DataTable bancoCuentab = this.acceso.Consultas(parametros);

                if (bancoCuentab != null && bancoCuentab.Rows.Count > 0)
                {
                    foreach (DataRow fila in bancoCuentab.Rows)
                    {
                        this.bancoCuenta = new EntitiesBancoCuenta
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            idBanco = Convert.ToInt32(fila["Banco"].ToString()),
                            idCuenta = Convert.ToInt32(fila["Cuenta"].ToString())
                        };

                        this.lstBancoCuenta.Add(bancoCuenta);
                    }
                }
                return lstBancoCuenta;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarCuentasContables :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codigo cuenta contable
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarCuentasBancos(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarCodigosCuentasContables :: " + ex.Message));
            }
        }
        #endregion
    }
}
