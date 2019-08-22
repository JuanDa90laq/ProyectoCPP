namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class BLBancosRecaudadoresAse
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesBancoRecaudadorAse> lstBancosRecaudadoresAse = null;
        private EntitiesBancoRecaudadorAse bancosRecaudadoresAse = null;

        #endregion

        #region "Metodos publicos"

        /// <summary>
        /// consulta los bancos recaudadores
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesBancoRecaudadorAse> ConsultarBancosRecaudadoresAse(DLAccesEntities parametros)
        {
            try
            {
                this.lstBancosRecaudadoresAse = new List<EntitiesBancoRecaudadorAse>();
                DataTable bancosRecaudadoresAseb = this.acceso.Consultas(parametros);

                if (bancosRecaudadoresAseb != null && bancosRecaudadoresAseb.Rows.Count > 0)
                {
                    foreach (DataRow fila in bancosRecaudadoresAseb.Rows)
                    {
                        this.bancosRecaudadoresAse = new EntitiesBancoRecaudadorAse
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            codigoEntidad = string.IsNullOrEmpty(fila["CodigoEntidad"].ToString()) ? (int?)null: Convert.ToInt32(fila["CodigoEntidad"].ToString()),
                            nombreEntidad = fila["NombreEntidad"].ToString(),
                            nit = string.IsNullOrEmpty(fila["Nit"].ToString()) ? (int?)null : Convert.ToInt32(fila["Nit"].ToString()),
                            nombreEntidadExtendido = fila["NombreEntidad"].ToString() + " - Cod: " + fila["CodigoEntidad"].ToString() + " - Nit: " + fila["Nit"].ToString()
                        };

                        this.lstBancosRecaudadoresAse.Add(bancosRecaudadoresAse);
                    }
                }

                lstBancosRecaudadoresAse = lstBancosRecaudadoresAse.OrderBy(a => a.nombreEntidad).ToList();

                return lstBancosRecaudadoresAse;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarBancosRecaudadoresAse :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codigo cuenta contable
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarBancosRecaudadoresAse(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarBancosRecaudadoresAse :: " + ex.Message));
            }
        }
        #endregion
    }
}
