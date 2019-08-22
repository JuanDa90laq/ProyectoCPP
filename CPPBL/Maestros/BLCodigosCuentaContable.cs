namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLCodigosCuentaContable
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesCodigosCuentaContable> lstCodigosCuentaContable = null;
        private EntitiesCodigosCuentaContable codigosCuentaContable = null;

        #endregion

        #region "Metodos publicos"

        /// <summary>
        /// consulta los beneficiarios
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCodigosCuentaContable> ConsultarCuentasContables(DLAccesEntities parametros)
        {
            try
            {
                this.lstCodigosCuentaContable = new List<EntitiesCodigosCuentaContable>();
                DataTable cuentasContablesb = this.acceso.Consultas(parametros);

                if (cuentasContablesb != null && cuentasContablesb.Rows.Count > 0)
                {
                    foreach (DataRow fila in cuentasContablesb.Rows)
                    {
                        this.codigosCuentaContable = new EntitiesCodigosCuentaContable
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            idTipoCesion = Convert.ToInt32(fila["IdTipoCesion"].ToString()),
                            cesion = string.IsNullOrEmpty(fila["TipoCesion"].ToString()) ? null : fila["TipoCesion"].ToString(),
                            idTipoCuenta = Convert.ToInt32(fila["IdTipoCuenta"].ToString()),
                            cuenta = string.IsNullOrEmpty(fila["TipoCuenta"].ToString()) ? null : fila["TipoCuenta"].ToString(),
                            codigoCuenta = string.IsNullOrEmpty(fila["CodigoCuenta"].ToString()) ? null : fila["CodigoCuenta"].ToString(),
                            nombreCuenta = string.IsNullOrEmpty(fila["NombreCuenta"].ToString()) ? null : fila["NombreCuenta"].ToString(),
                            efectuaMovimiento = Convert.ToBoolean(fila["EfectuaMovimiento"].ToString()),
                            idCalificacion = Convert.ToInt32(fila["IdCalificacion"].ToString()),
                            calificacion = string.IsNullOrEmpty(fila["Calificacion"].ToString()) ? null : fila["Calificacion"].ToString(),
                            cuentaNombreCuenta = fila["CodigoCuenta"].ToString() + " " + fila["NombreCuenta"].ToString(),
                            idAplicaCuenta = Convert.ToInt32(fila["IdAplicaCuenta"].ToString()),
                            aplicaCuenta = string.IsNullOrEmpty(fila["AplicaCuenta"].ToString()) ? null : fila["AplicaCuenta"].ToString(),
                            cppCCC_IdCuenta = string.IsNullOrEmpty(fila["cppCCC_IdCuenta"].ToString()) ? 0 : Convert.ToInt32(fila["cppCCC_IdCuenta"].ToString())
                        };

                        this.lstCodigosCuentaContable.Add(codigosCuentaContable);
                    }
                }
                return lstCodigosCuentaContable;
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
        public ResultConsulta RegistrarEditarCodigosCuentasContables(DLAccesEntities parametros)
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
