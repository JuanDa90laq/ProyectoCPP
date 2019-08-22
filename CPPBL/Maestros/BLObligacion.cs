namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BLObligacion
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesSituacionJuridica> lstSituacionJuridica = null;
        private EntitiesSituacionJuridica situacionJuridica = null;
        private List<EntitiesCodigoCIIU> lstCodigoCIIU = null;        
        private EntitiesCodigoCIIU codigoCIIU = null;
        private List<EntitiesTipoGarantia> lstTipoGarantia = null;
        private EntitiesTipoGarantia tipoGarantia = null;
        private List<EntitiesTipoInmueble> lstTipoInmueble = null;
        private EntitiesTipoInmueble tipoInmueble = null;
        private List<EntitiesObligacionInmueble> lstObligacionInmueble = null;
        private EntitiesObligacionInmueble obligacionInmueble = null;
        private List<EntitiesObligacion> lstObligacion = null;
        private EntitiesObligacion obligacion = null;

        #endregion

        /// <summary>
        /// consulta las situaciones Juridicas
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesSituacionJuridica> ConsultaSituacionJuridica(DLAccesEntities parametros)
        {
            this.lstSituacionJuridica = new List<EntitiesSituacionJuridica>();
            DataTable situacionJuridicab = this.acceso.Consultas(parametros);

            if (situacionJuridicab != null && situacionJuridicab.Rows.Count > 0)
            {
                foreach (DataRow fila in situacionJuridicab.Rows)
                {
                    this.situacionJuridica = new EntitiesSituacionJuridica
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstSituacionJuridica.Add(situacionJuridica);
                }
            }
            return lstSituacionJuridica;
        }

        /// <summary>
        /// consulta los codigos CIIU
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCodigoCIIU> ConsultaCodigoCIIU(DLAccesEntities parametros)
        {
            this.lstCodigoCIIU = new List<EntitiesCodigoCIIU>();
            DataTable codigoCIIUb = this.acceso.Consultas(parametros);

            if (codigoCIIUb != null && codigoCIIUb.Rows.Count > 0)
            {
                foreach (DataRow fila in codigoCIIUb.Rows)
                {
                    this.codigoCIIU = new EntitiesCodigoCIIU
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        codigoCIIU = fila["codigoCIIU"].ToString(),
                        descripcion = fila["descripcion"].ToString(),
                        codigoDescripcion = fila["codigoCIIU"].ToString() + " - " + fila["descripcion"].ToString()
                    };

                    this.lstCodigoCIIU.Add(codigoCIIU);
                }
            }
            return lstCodigoCIIU;
        }

        /// <summary>
        /// consulta los tipos de garantia
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoGarantia> ConsultaTipoGarantia(DLAccesEntities parametros)
        {
            this.lstTipoGarantia = new List<EntitiesTipoGarantia>();
            DataTable tipoGarantiab = this.acceso.Consultas(parametros);

            if (tipoGarantiab != null && tipoGarantiab.Rows.Count > 0)
            {
                foreach (DataRow fila in tipoGarantiab.Rows)
                {
                    this.tipoGarantia = new EntitiesTipoGarantia
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString(),
                    };

                    this.lstTipoGarantia.Add(tipoGarantia);
                }
            }
            return lstTipoGarantia;
        }

        /// <summary>
        /// consulta los tipos de inmueble
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoInmueble> ConsultaTipoInmueble(DLAccesEntities parametros)
        {
            this.lstTipoInmueble = new List<EntitiesTipoInmueble>();
            DataTable tipoInmuebleb = this.acceso.Consultas(parametros);

            if (tipoInmuebleb != null && tipoInmuebleb.Rows.Count > 0)
            {
                foreach (DataRow fila in tipoInmuebleb.Rows)
                {
                    this.tipoInmueble = new EntitiesTipoInmueble
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString(),
                    };

                    this.lstTipoInmueble.Add(tipoInmueble);
                }
            }
            return lstTipoInmueble;
        }

        /// <summary>
        /// consulta la relacion de obligacion e inmueble
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesObligacionInmueble> ConsultaObligacionInmueble(DLAccesEntities parametros)
        {
            this.lstObligacionInmueble = new List<EntitiesObligacionInmueble>();
            DataTable obligacionInmuebleb = this.acceso.Consultas(parametros);

            if (obligacionInmuebleb != null && obligacionInmuebleb.Rows.Count > 0)
            {
                foreach (DataRow fila in obligacionInmuebleb.Rows)
                {
                    this.obligacionInmueble = new EntitiesObligacionInmueble
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        idObligacion = Convert.ToInt32(fila["IdObligacion"].ToString()),
                        idtipoInmueble = Convert.ToInt32(fila["IdTipoInmueble"].ToString()),
                        tipoInmueble = fila["TipoInmueble"].ToString(),
                        matriculaInmobiliaria = fila["MatriculaInmobiliaria"].ToString(),
                        direccion = fila["Direccion"].ToString(),
                        valorInmueble = Convert.ToDouble(fila["Valor"].ToString()),
                    };

                    this.lstObligacionInmueble.Add(obligacionInmueble);
                }
            }
            return lstObligacionInmueble;
        }

        /// <summary>
        /// registra o edita una obligacion nueva
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarObligacionInmueble(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarObligacionInmueble :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta los codigos CIIU
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesObligacion> ConsultaObligacion(DLAccesEntities parametros)
        {
            this.lstObligacion = new List<EntitiesObligacion>();
            DataTable Obligacionb = this.acceso.Consultas(parametros);

            if (Obligacionb != null && Obligacionb.Rows.Count > 0)
            {
                foreach (DataRow fila in Obligacionb.Rows)
                {
                    this.obligacion = new EntitiesObligacion
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        idPrograma = Convert.ToInt32(fila["idPrograma"].ToString()),
                        programa = fila["Programa"].ToString(),
                        idPlanPago = string.IsNullOrEmpty(fila["idPlanPago"].ToString()) ? (int?)null : Convert.ToInt32(fila["idPlanPago"].ToString()),
                        planPago = fila["PlanPago"].ToString(),
                        idPlanPagoUnico = string.IsNullOrEmpty(fila["idPlanPagoUnico"].ToString()) ? (int?)null : Convert.ToInt32(fila["idPlanPagoUnico"].ToString()),
                        planPagoUnico = fila["PlanPagoUnico"].ToString(),
                        convenio = fila["Convenio"].ToString(),
                        idBeneficiario = Convert.ToInt32(fila["idBeneficiario"].ToString()),
                        beneficiario = fila["Beneficiario"].ToString(),
                        idBancoRecaudador = Convert.ToInt32(fila["idBancoRecaudador"].ToString()),
                        bancoRecaudador = fila["cppBra_NombreEntidad"].ToString(),
                        idSituacionJuridica = Convert.ToInt32(fila["idSituacionJuridica"].ToString()),
                        situacionJuridica = fila["SituacionJuridica"].ToString(),
                        operacionIntermediario = Convert.ToInt32(fila["OperacionIntermediario"].ToString()),
                        baseCompra = Convert.ToDouble(fila["BaseCompra"].ToString()),
                        porcentaje = Convert.ToDouble(fila["Porcentaje"].ToString()),
                        valorPagadoFinagro = Convert.ToDouble(fila["ValorPagadoFinagro"].ToString()),
                        aporteDinero = Convert.ToDouble(fila["AporteDinero"].ToString()),
                        aporteFinanciado = Convert.ToDouble(fila["AporteFinanciado"].ToString()),
                        valorCarteraInicial = Convert.ToDouble(fila["ValorCarteraInicial"].ToString()),
                        fechaCompra = Convert.ToDateTime(fila["FechaCompra"].ToString()),
                        idDestino = Convert.ToInt32(fila["IdDestino"].ToString()),
                        idActividadAgropecuaria = Convert.ToInt32(fila["IdActividadAgropecuaria"].ToString()),
                        idCodigoCIIU = Convert.ToInt32(fila["IdCodigoCIIU"].ToString()),
                        codigoCIIU =fila["CodigoCIIU"].ToString(),
                        idDepartamentoCompra = Convert.ToInt32(fila["IdDeptoCompra"].ToString()),
                        idMunicipioCompra = Convert.ToInt32(fila["IdMunCompra"].ToString()),
                        idDepartamentoOrigen = Convert.ToInt32(fila["IdDeptoOrigen"].ToString()),
                        idMunicipioOrigen = Convert.ToInt32(fila["IdMunOrigen"].ToString()),
                        idDepartamentoInversion = Convert.ToInt32(fila["IdDeptoInv"].ToString()),
                        idMunicipioInversion = Convert.ToInt32(fila["IdMunInv"].ToString()),
                        idTipoGarantia = string.IsNullOrEmpty(fila["IdTipoGarantia"].ToString()) ? (int?)null : Convert.ToInt32(fila["IdTipoGarantia"].ToString()),
                        tipoGarantia = fila["TipoGarantia"].ToString()
                    };

                    this.lstObligacion.Add(obligacion);
                }
            }
            return lstObligacion;
        }

        /// <summary>
        /// registra o edita una obligacion nueva
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarObligaciones(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + ex.Message));
            }
        }
    }
}