namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    public class BLPlanPagos
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiescppTipoPlanPagos> lstTipoPlanPagos = null;
        private EntitiescppTipoPlanPagos tipoPlanPagos = null;
        private List<EntitiescppModalidadCapital> lstModalidadCapital = null;
        private EntitiescppModalidadCapital modalidadCapital = null;
        private List<EntitiesPeriocidadInteresesCorrientes> lstPeriocidadInt = null;
        private EntitiesPeriocidadInteresesCorrientes PeriocidadInt = null;
        private List<EntitiesPlanPagos> lstPlanPagos = null;
        private EntitiesPlanPagos planPagos = null;


        #endregion

        // <summary>
        /// consulta los tipos de planes de pagos
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiescppTipoPlanPagos> ConsultarTipoPlanPagos(DLAccesEntities parametros)
        {
            try
            {
                this.lstTipoPlanPagos = new List<EntitiescppTipoPlanPagos>();
                DataTable dtTipoPlanPagos = this.acceso.Consultas(parametros);

                if (dtTipoPlanPagos != null && dtTipoPlanPagos.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtTipoPlanPagos.Rows)
                    {
                        this.tipoPlanPagos = new EntitiescppTipoPlanPagos();
                        this.tipoPlanPagos.id = Convert.ToInt32(fila["identificador"].ToString());
                        this.tipoPlanPagos.tipoPlanPagos = fila["plan de pagos"].ToString();
                        this.tipoPlanPagos.estado = Convert.ToBoolean(fila["estadoReg"].ToString());
                        this.lstTipoPlanPagos.Add(tipoPlanPagos);
                    }
                }

                lstTipoPlanPagos = lstTipoPlanPagos.OrderBy(a => a.tipoPlanPagos).ToList();

                return lstTipoPlanPagos;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - BLPlanPagos - ConsultarTipoPlanPagos :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta la modalidad de capital
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiescppModalidadCapital> ConsultarModalidadCapital(DLAccesEntities parametros)
        {
            try
            {
                this.lstModalidadCapital = new List<EntitiescppModalidadCapital>();
                DataTable dtModalidadCapital = this.acceso.Consultas(parametros);

                if (dtModalidadCapital != null && dtModalidadCapital.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtModalidadCapital.Rows)
                    {
                        this.modalidadCapital = new EntitiescppModalidadCapital();
                        this.modalidadCapital.id = Convert.ToInt32(fila["identificador"].ToString());
                        this.modalidadCapital.modalidad = fila["modalidad"].ToString();
                        this.modalidadCapital.estado = Convert.ToBoolean(fila["estadoReg"].ToString());
                        this.lstModalidadCapital.Add(modalidadCapital);
                    }
                }

                lstModalidadCapital = lstModalidadCapital.OrderBy(a => a.modalidad).ToList();

                return lstModalidadCapital;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - BLPlanPagos - ConsultarModalidadCapital :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta la periocidad de intereses corrientes
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPeriocidadInteresesCorrientes> ConsultarPeriocidadInteresesCorrientes(DLAccesEntities parametros)
        {
            try
            {
                this.lstPeriocidadInt = new List<EntitiesPeriocidadInteresesCorrientes>();
                DataTable dtPeriocidadInt = this.acceso.Consultas(parametros);

                if (dtPeriocidadInt != null && dtPeriocidadInt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtPeriocidadInt.Rows)
                    {
                        this.PeriocidadInt = new EntitiesPeriocidadInteresesCorrientes
                        {
                            id = Convert.ToInt32(fila["Id"].ToString()),
                            descripcion = fila["Descripcion"].ToString(),
                            estado = Convert.ToBoolean(fila["Estado"].ToString())
                        };
                        this.lstPeriocidadInt.Add(PeriocidadInt);
                    }
                }

                lstPeriocidadInt = lstPeriocidadInt.OrderBy(a => a.id).ToList();

                return lstPeriocidadInt;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - BLPlanPagos - ConsultarPeriocidadInteresesCorrientes :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta los planes de pago
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPlanPagos> ConsultarPlanesPago(DLAccesEntities parametros)
        {
            try
            {
                this.lstPlanPagos = new List<EntitiesPlanPagos>();
                DataTable planPagosb = this.acceso.Consultas(parametros);

                if (planPagosb != null && planPagosb.Rows.Count > 0)
                {
                    foreach (DataRow fila in planPagosb.Rows)
                    {
                        this.planPagos = new EntitiesPlanPagos
                        {
                            id = Convert.ToInt32(fila["id"].ToString()),
                            idIntermediario = Convert.ToInt32(fila["IdIntermediario"].ToString()),
                            nombreIntermediario = fila["NombreIntermediario"].ToString(),
                            descuentoPorAmortizar = Convert.ToDouble(fila["DescuentoPorAmortizar"].ToString()),
                            descuentoAmortizado = Convert.ToDouble(fila["DescuentoAmortizado"].ToString()),
                            impuestoTimbre = Convert.ToDouble(fila["ImpuestoTimbre"].ToString()),
                            periocidadCapital = Convert.ToInt32(fila["PeriocidadCapital"].ToString()),
                            periodoGracia = Convert.ToInt32(fila["PeriodoGracia"].ToString()),
                            periodoMuerto = Convert.ToInt32(fila["PeriodoMuerto"].ToString()),
                            plazoTotalObligacion = Convert.ToInt32(fila["PlazoTotalObligacion"].ToString()),
                            numeroCuotasPlanPagos = Convert.ToInt32(fila["NumeroCuotasPlanPagos"].ToString()),
                            idPlanPagos = Convert.ToInt32(fila["IdPlanPagos"].ToString()),
                            planPagos = string.IsNullOrEmpty(fila["PlanPagos"].ToString()) ? null : fila["PlanPagos"].ToString(),
                            idModalidadCapital = Convert.ToInt32(fila["IdModalidadCapital"].ToString()),
                            modalidadCapital = string.IsNullOrEmpty(fila["ModalidadCapital"].ToString()) ? null : fila["ModalidadCapital"].ToString(),
                            IdperiocidadInteresesCorrientes = Convert.ToInt32(fila["IdPeriocidadInteresesCorrientes"].ToString()),
                            periocidadInteresesCorrientes = string.IsNullOrEmpty(fila["PeriocidadInteresesCorrientes"].ToString()) ? null : fila["PeriocidadInteresesCorrientes"].ToString(),
                            tasaInteresesCorrientes = Convert.ToDouble(fila["TasaInteresesCorrientes"].ToString()),
                            puntosContigentesInt = string.IsNullOrEmpty(fila["PuntosContigentesInt"].ToString()) ? (int?)null : Convert.ToInt32(fila["PuntosContigentesInt"].ToString()),
                            tasaInteresesMoratorios = Convert.ToDouble(fila["TasaInteresesMoratorios"].ToString()),
                            fechaPago = Convert.ToDateTime(fila["FechaPago"].ToString()),
                            descripcion = string.IsNullOrEmpty(fila["Descripcion"].ToString()) ? null : fila["Descripcion"].ToString(),
                            idPlanPagoIntermediario = fila["id"].ToString() + "-" + fila["NombreIntermediario"].ToString()
                        };

                        this.lstPlanPagos.Add(planPagos);
                    }
                }
                return lstPlanPagos;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - ConsultarPlanesPagos :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codigo cuenta contable
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarPlanesPago(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarPlanesPago :: " + ex.Message));
            }
        }

    }
}
