namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BLConceptoAnual
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesConceptosAnuales> lstConcepto = null;
        private EntitiesConceptosAnuales concepto = null;

        #endregion

        /// <summary>
        /// consulta los tipos de cocepto
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesConceptosAnuales> ConsultaConcepto(DLAccesEntities parametros)
        {
            try
            {
                this.lstConcepto = new List<EntitiesConceptosAnuales>();
                DataTable tbConcepto = this.acceso.Consultas(parametros);

                if (tbConcepto != null && tbConcepto.Rows.Count > 0)
                {
                    foreach (DataRow fila in tbConcepto.Rows)
                    {
                        this.concepto = new EntitiesConceptosAnuales
                        {
                            cppCp_Id = Convert.ToInt32(fila["cppCp_Id"].ToString()),
                            cppCp_Valor = fila["cppCp_Valor"].ToString(),
                            cppCp_FechaVigenciaDesde = !string.IsNullOrEmpty(fila["cppCp_FechaVigenciaDesde"].ToString()) ? Convert.ToDateTime(fila["cppCp_FechaVigenciaDesde"].ToString()) : (DateTime?)null,
                            cppCp_FechaVigenciaHasta = !string.IsNullOrEmpty(fila["cppCp_FechaVigenciaHasta"].ToString()) ? Convert.ToDateTime(fila["cppCp_FechaVigenciaHasta"].ToString()) : (DateTime?)null,
                            cppCp_IdTipoCon = Convert.ToInt32(fila["cppCp_IdTipoCon"].ToString()),
                            historico = fila["historico"].ToString()
                        };

                        this.lstConcepto.Add(concepto);
                    }
                }
                return lstConcepto;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLConceptoAnual - ConsultaConcepto :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta los tipos de cocepto
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesConceptosAnuales> ConsultaConceptoSet(DLAccesEntities parametros)
        {
            try
            {
                this.lstConcepto = new List<EntitiesConceptosAnuales>();
                DataSet tbConcepto = this.acceso.ConsultasSet(parametros);

                foreach (DataTable tabla in tbConcepto.Tables)
                {
                    if (tbConcepto != null && tabla.Rows.Count > 0)
                    {
                        foreach (DataRow fila in tabla.Rows)
                        {
                            this.concepto = new EntitiesConceptosAnuales
                            {
                                cppCp_Id = Convert.ToInt32(fila["cppCp_Id"].ToString()),
                                cppCp_Valor = fila["cppCp_Valor"].ToString(),
                                cppCp_FechaVigenciaDesde = !string.IsNullOrEmpty(fila["cppCp_FechaVigenciaDesde"].ToString()) ? Convert.ToDateTime(fila["cppCp_FechaVigenciaDesde"].ToString()) : (DateTime?)null,
                                cppCp_FechaVigenciaHasta = !string.IsNullOrEmpty(fila["cppCp_FechaVigenciaHasta"].ToString()) ? Convert.ToDateTime(fila["cppCp_FechaVigenciaDesde"].ToString()) : (DateTime?)null,
                                cppCp_IdTipoCon = Convert.ToInt32(fila["cppCp_IdTipoCon"].ToString()),
                                historico = fila["historico"].ToString()
                            };

                            this.lstConcepto.Add(concepto);
                        }
                    }
                }
                return lstConcepto.OrderByDescending(a=>a.cppCp_IdTipoCon).ThenBy(a=>a.cppCp_FechaVigenciaDesde).ToList();
            }
            catch (Exception ex)
            {
                throw (new Exception("BLConceptoAnual - ConsultaConceptoSet :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo beneficiario
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarConceptos(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLConceptoAnual - RegistrarConceptos :: " + ex.Message));
            }
        }
    }
}
