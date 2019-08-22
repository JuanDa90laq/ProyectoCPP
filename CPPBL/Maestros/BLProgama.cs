namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    public class BLProgama
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesCantidadPagares> lstCantidadPagares = null;
        private EntitiesCantidadPagares cantidadPagares = null;
        private List<EntitiesCentroUtilidad> lstCentroUtilidad = null;
        private EntitiesCentroUtilidad centroUtilidad = null;
        private List<EntitiesPrograma> lstProgramas = null;
        private EntitiesPrograma programa = null;
        private List<EntitiesProgramaPlanPago> lstProgramaPlanPago = null;
        private EntitiesProgramaPlanPago programaPlanPago = null;

        #endregion

        #region "Metodos publicos"

        /// <summary>
        /// consulta la cantidad de pagares
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCantidadPagares> ConsultarCantidadPagares(DLAccesEntities parametros)
        {
            try
            {
                this.lstCantidadPagares = new List<EntitiesCantidadPagares>();
                DataTable CantidadPagaresb = this.acceso.Consultas(parametros);

                if (CantidadPagaresb != null && CantidadPagaresb.Rows.Count > 0)
                {
                    foreach (DataRow fila in CantidadPagaresb.Rows)
                    {
                        this.cantidadPagares = new EntitiesCantidadPagares
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            descripcion = string.IsNullOrEmpty(fila["Descripcion"].ToString()) ? string.Empty : fila["Descripcion"].ToString().Trim()
                        };

                        this.lstCantidadPagares.Add(cantidadPagares);
                    }
                }

                lstCantidadPagares = lstCantidadPagares.OrderBy(a => a.descripcion).ToList();

                return lstCantidadPagares;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarCantidadPagares :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta los centros de utilidad
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCentroUtilidad> ConsultarCentroUtilidad(DLAccesEntities parametros)
        {
            try
            {
                this.lstCentroUtilidad = new List<EntitiesCentroUtilidad>();
                DataTable CentroUtilidadb = this.acceso.Consultas(parametros);

                if (CentroUtilidadb != null && CentroUtilidadb.Rows.Count > 0)
                {
                    foreach (DataRow fila in CentroUtilidadb.Rows)
                    {
                        this.centroUtilidad = new EntitiesCentroUtilidad
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            codigoCentroUtilidad = string.IsNullOrEmpty(fila["CentroUtilidad"].ToString()) ? string.Empty : fila["CentroUtilidad"].ToString().Trim(),
                            descripcion = string.IsNullOrEmpty(fila["Descripcion"].ToString()) ? string.Empty : fila["Descripcion"].ToString().Trim(),
                            codigodescripcion = fila["CentroUtilidad"].ToString() + " " + fila["Descripcion"].ToString()
                        };

                        this.lstCentroUtilidad.Add(centroUtilidad);
                    }
                }

                lstCentroUtilidad = lstCentroUtilidad.OrderBy(a => a.codigoCentroUtilidad).ToList();

                return lstCentroUtilidad;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarCentroUtilidad :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta Programas
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPrograma> ConsultarProgramas(DLAccesEntities parametros)
        {
            try
            {
                this.lstProgramas = new List<EntitiesPrograma>();
                DataTable Programasb = this.acceso.Consultas(parametros);

                if (Programasb != null && Programasb.Rows.Count > 0)
                {
                    foreach (DataRow fila in Programasb.Rows)
                    {
                        this.programa = new EntitiesPrograma
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            nombre = fila["Nombre"].ToString().Trim(),
                            fechaInicial = Convert.ToDateTime(fila["FechaInicial"].ToString()),
                            idPagares = Convert.ToInt32(fila["IdCantidadPagares"].ToString()),
                            pagares = fila["CantidadPagares"].ToString().Trim(),
                            idCentroUtilidad = Convert.ToInt32(fila["CantidadPagares"].ToString()),
                            centroUtilidad = fila["CentroUtilidad"].ToString().Trim(),
                            descripcion = fila["Descripcion"].ToString().Trim()
                        };

                        this.lstProgramas.Add(programa);
                    }
                }

                lstProgramas = lstProgramas.OrderBy(a => a.id).ToList();

                return lstProgramas;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarProgramas : " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codigo cuenta contable
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarProgramas(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarProgramas :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta los convenios y planes Asociados
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesProgramaPlanPago> ConsultarProgramaPlanPago(DLAccesEntities parametros)
        {
            try
            {
                this.lstProgramaPlanPago = new List<EntitiesProgramaPlanPago>();
                DataTable ProgramaPlanPagob = this.acceso.Consultas(parametros);

                if (ProgramaPlanPagob != null && ProgramaPlanPagob.Rows.Count > 0)
                {
                    foreach (DataRow fila in ProgramaPlanPagob.Rows)
                    {
                        this.programaPlanPago = new EntitiesProgramaPlanPago
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            idPrograma = Convert.ToInt32(fila["IdPrograma"].ToString()),
                            programa = fila["Programa"].ToString().Trim(),
                            idPlanPago = Convert.ToInt32(fila["IdPlanPago"].ToString()),
                            planPago = fila["PlanPago"].ToString().Trim(),
                            convenio = fila["Convenio"].ToString().Trim()
                        };

                        this.lstProgramaPlanPago.Add(programaPlanPago);
                    }
                }

                lstProgramaPlanPago = lstProgramaPlanPago.OrderBy(a => a.id).ToList();

                return lstProgramaPlanPago;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarProgramaPlanPago :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codigo cuenta contable
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarProgramasPlanPago(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarProgramasPlanPago :: " + ex.Message));
            }
        }

        #endregion
    }
}
