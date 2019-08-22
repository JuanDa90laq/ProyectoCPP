namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    public class BLBeneficioLey
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTipoBeneficiado> lstTipoBeneficiado = null;
        private EntitiesTipoBeneficiado tipoBeneficiado = null;
        private List<EntitiesVigencia> lstVigencia = null;
        private EntitiesVigencia vigencia = null;
        private List<EntitiesTasaMora> lstTasaMora = null;
        private EntitiesTasaMora tasaMora = null;
        private List<EntitiesPlazoObligacion> lstPlazoObligacion = null;
        private EntitiesPlazoObligacion plazoObligacion = null;
        private List<EntitiesPeriodoMuerto> lstPeriodoMuerto = null;
        private EntitiesPeriodoMuerto periodoMuerto = null;
        private List<EntitiesPeriodoGracia> lstPeriodoGracia = null;
        private EntitiesPeriodoGracia periodoGracia = null;
        private List<EntitiesBeneficiosIntereses> lstBeneficiosIntereses = null;
        private EntitiesBeneficiosIntereses beneficiosIntereses = null;
        private List<EntitiesBeneficiosSeguroVida> lstBeneficiosSeguroVida = null;        
        private EntitiesBeneficiosSeguroVida beneficiosSeguroVida = null;
        private List<EntitiesOtrosBeneficios> lstOtrosBeneficios = null;
        private EntitiesOtrosBeneficios otrosBeneficios = null;
        private List<EntitiesCabeceraBeneficioLey> lstCabeceraBenefioLey = null;
        private EntitiesCabeceraBeneficioLey cabeceraBeneficioLey = null;
        private List<EntitiesPagareBeneficioLey> lstPagaresBenefioLey = null;
        private EntitiesPagareBeneficioLey pagareBeneficioLey = null;
        private List<EntitiesCapitalizacionIntereses> lstCapitalizacionIntereses = null;
        private EntitiesCapitalizacionIntereses capitalizacionIntereses = null;
        private List<EntitiesTasaIntereses> lstTasaInteres = null;
        private EntitiesTasaIntereses tasaInteres = null;

        #endregion

        /// <summary>
        /// consulta los tipos de beneficiado
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoBeneficiado> ConsultaTipoBeneficiado(DLAccesEntities parametros)
        {
            this.lstTipoBeneficiado = new List<EntitiesTipoBeneficiado>();
            DataTable tipoBeneficiadob = this.acceso.Consultas(parametros);

            if (tipoBeneficiadob != null && tipoBeneficiadob.Rows.Count > 0)
            {
                foreach (DataRow fila in tipoBeneficiadob.Rows)
                {
                    this.tipoBeneficiado = new EntitiesTipoBeneficiado
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstTipoBeneficiado.Add(tipoBeneficiado);
                }
            }
            return lstTipoBeneficiado.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta la tabla de vigencia
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesVigencia> ConsultaVigencia(DLAccesEntities parametros)
        {
            this.lstVigencia = new List<EntitiesVigencia>();
            DataTable vigenciab = this.acceso.Consultas(parametros);

            if (vigenciab != null && vigenciab.Rows.Count > 0)
            {
                foreach (DataRow fila in vigenciab.Rows)
                {
                    this.vigencia = new EntitiesVigencia
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstVigencia.Add(vigencia);
                }
            }
            return lstVigencia.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta las tasas de mora
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTasaMora> ConsultaTasaMora(DLAccesEntities parametros)
        {
            this.lstTasaMora = new List<EntitiesTasaMora>();
            DataTable tasaMorab = this.acceso.Consultas(parametros);

            if (tasaMorab != null && tasaMorab.Rows.Count > 0)
            {
                foreach (DataRow fila in tasaMorab.Rows)
                {
                    this.tasaMora = new EntitiesTasaMora
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstTasaMora.Add(tasaMora);
                }
            }
            return lstTasaMora.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta los plazos de obligacion
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPlazoObligacion> ConsultaPlazoObligacion(DLAccesEntities parametros)
        {
            this.lstPlazoObligacion = new List<EntitiesPlazoObligacion>();
            DataTable plazoObligacionb = this.acceso.Consultas(parametros);

            if (plazoObligacionb != null && plazoObligacionb.Rows.Count > 0)
            {
                foreach (DataRow fila in plazoObligacionb.Rows)
                {
                    this.plazoObligacion = new EntitiesPlazoObligacion
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstPlazoObligacion.Add(plazoObligacion);
                }
            }
            return lstPlazoObligacion.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta los periodos muertos
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPeriodoMuerto> ConsultaPeriodoMuerto(DLAccesEntities parametros)
        {
            this.lstPeriodoMuerto = new List<EntitiesPeriodoMuerto>();
            DataTable periodoMuertob = this.acceso.Consultas(parametros);

            if (periodoMuertob != null && periodoMuertob.Rows.Count > 0)
            {
                foreach (DataRow fila in periodoMuertob.Rows)
                {
                    this.periodoMuerto = new EntitiesPeriodoMuerto
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstPeriodoMuerto.Add(periodoMuerto);
                }
            }
            return lstPeriodoMuerto.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta los periodos de gracia
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPeriodoGracia> ConsultaPeriodoGracia(DLAccesEntities parametros)
        {
            this.lstPeriodoGracia = new List<EntitiesPeriodoGracia>();
            DataTable periodoGraciab = this.acceso.Consultas(parametros);

            if (periodoGraciab != null && periodoGraciab.Rows.Count > 0)
            {
                foreach (DataRow fila in periodoGraciab.Rows)
                {
                    this.periodoGracia = new EntitiesPeriodoGracia
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstPeriodoGracia.Add(periodoGracia);
                }
            }
            return lstPeriodoGracia.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta los beneficios de interes
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesBeneficiosIntereses> ConsultaBeneficiosInteres(DLAccesEntities parametros)
        {
            this.lstBeneficiosIntereses = new List<EntitiesBeneficiosIntereses>();
            DataTable beneficiosInteresesb = this.acceso.Consultas(parametros);

            if (beneficiosInteresesb != null && beneficiosInteresesb.Rows.Count > 0)
            {
                foreach (DataRow fila in beneficiosInteresesb.Rows)
                {
                    this.beneficiosIntereses = new EntitiesBeneficiosIntereses
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstBeneficiosIntereses.Add(beneficiosIntereses);
                }
            }
            return lstBeneficiosIntereses.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta los beneficios de seguro de vida
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCapitalizacionIntereses> ConsultaCapitalizacionIntereses(DLAccesEntities parametros)
        {
            this.lstCapitalizacionIntereses = new List<EntitiesCapitalizacionIntereses>();
            DataTable capitalizacionInteresesb = this.acceso.Consultas(parametros);

            if (capitalizacionInteresesb != null && capitalizacionInteresesb.Rows.Count > 0)
            {
                foreach (DataRow fila in capitalizacionInteresesb.Rows)
                {
                    this.capitalizacionIntereses = new EntitiesCapitalizacionIntereses
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstCapitalizacionIntereses.Add(capitalizacionIntereses);
                }
            }
            return lstCapitalizacionIntereses.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta la capitalizacion de intereses
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesBeneficiosSeguroVida> ConsultaBeneficiosSeguroVida(DLAccesEntities parametros)
        {
            this.lstBeneficiosSeguroVida = new List<EntitiesBeneficiosSeguroVida>();
            DataTable beneficiosSeguroVidab = this.acceso.Consultas(parametros);

            if (beneficiosSeguroVidab != null && beneficiosSeguroVidab.Rows.Count > 0)
            {
                foreach (DataRow fila in beneficiosSeguroVidab.Rows)
                {
                    this.beneficiosSeguroVida = new EntitiesBeneficiosSeguroVida
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstBeneficiosSeguroVida.Add(beneficiosSeguroVida);
                }
            }
            return lstBeneficiosSeguroVida.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta otros beneficios
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesOtrosBeneficios> ConsultaOtrosBeneficios(DLAccesEntities parametros)
        {
            this.lstOtrosBeneficios = new List<EntitiesOtrosBeneficios>();
            DataTable otrosBeneficiosb = this.acceso.Consultas(parametros);

            if (otrosBeneficiosb != null && otrosBeneficiosb.Rows.Count > 0)
            {
                foreach (DataRow fila in otrosBeneficiosb.Rows)
                {
                    this.otrosBeneficios = new EntitiesOtrosBeneficios
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstOtrosBeneficios.Add(otrosBeneficios);
                }
            }
            return lstOtrosBeneficios.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta la tabla de vigencia
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTasaIntereses> ConsultaTasaInteres(DLAccesEntities parametros)
        {
            this.lstTasaInteres = new List<EntitiesTasaIntereses>();
            DataTable tasaInteresesb = this.acceso.Consultas(parametros);

            if (tasaInteresesb != null && tasaInteresesb.Rows.Count > 0)
            {
                foreach (DataRow fila in tasaInteresesb.Rows)
                {
                    this.tasaInteres = new EntitiesTasaIntereses
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstTasaInteres.Add(tasaInteres);
                }
            }
            return lstTasaInteres.OrderBy(x => x.descripcion).ToList();
        }

        /// <summary>
        /// consulta la cabecera de beneficio ley
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCabeceraBeneficioLey> ConsultaCabeceraBeneficioLey(DLAccesEntities parametros)
        {
            this.lstCabeceraBenefioLey = new List<EntitiesCabeceraBeneficioLey>();
            DataTable cabeceraBeneficioLeyb = this.acceso.Consultas(parametros);

            if (cabeceraBeneficioLeyb != null && cabeceraBeneficioLeyb.Rows.Count > 0)
            {
                foreach (DataRow fila in cabeceraBeneficioLeyb.Rows)
                {
                    this.cabeceraBeneficioLey = new EntitiesCabeceraBeneficioLey
                    {
                        id = Convert.ToInt32(fila["Id"].ToString()),
                        nombreBeneficio = fila["Nombre"].ToString(),
                        idPrograma = Convert.ToInt32(fila["IdPrograma"].ToString()),
                        programa = fila["Programa"].ToString(),
                        idDepartamento = Convert.ToInt32(fila["IdDepartamento"].ToString()),
                        idMunicipio = Convert.ToInt32(fila["IdMunicipio"].ToString()),
                        fechaInicial = Convert.ToDateTime(fila["FechaInicial"].ToString()),
                        fechaFinal = Convert.ToDateTime(fila["FechaFinal"].ToString()),
                        idCantidadPagares = Convert.ToInt32(fila["IdCantidaPagares"].ToString()),
                        cantidadPagares = fila["CantidadPagares"].ToString(),
                        idIntermediario = string.IsNullOrEmpty(fila["IdIntermediario"].ToString()) ? (int?)null : Convert.ToInt32(fila["IdIntermediario"].ToString()),
                        intermediario= fila["Intermediario"].ToString(),
                        topeMaximo = string.IsNullOrEmpty(fila["TopeMaximo"].ToString()) ? (int?)null: Convert.ToInt32(fila["TopeMaximo"].ToString()),
                        idTipoBeneficiado = Convert.ToInt32(fila["IdTipoBeneficiado"].ToString()),
                        tipoBeneficiado = fila["TipoBeneficiado"].ToString(),
                        idActividadAgropecuaria = Convert.ToInt32(fila["IdActividadAgropecuaria"].ToString()),
                        idVigencia = Convert.ToInt32(fila["IdEstadoVigencia"].ToString()),
                        vigencia = fila["EstadoVigencia"].ToString(),
                    };

                    this.lstCabeceraBenefioLey.Add(cabeceraBeneficioLey);
                }
            }
            return lstCabeceraBenefioLey.OrderBy(x => x.id).ToList();
        }

        /// <summary>
        /// consulta los pagares del beneficio de Ley
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesPagareBeneficioLey> ConsultaPagareBeneficioLey(DLAccesEntities parametros)
        {
            this.lstPagaresBenefioLey = new List<EntitiesPagareBeneficioLey>();
            DataTable pagareBeneficioLeyb = this.acceso.Consultas(parametros);

            if (pagareBeneficioLeyb != null && pagareBeneficioLeyb.Rows.Count > 0)
            {
                foreach (DataRow fila in pagareBeneficioLeyb.Rows)
                {
                    this.pagareBeneficioLey = new EntitiesPagareBeneficioLey
                    {
                        id = Convert.ToInt32(fila["Id"].ToString()),
                        idCabecera = Convert.ToInt32(fila["IdCabeceraBeneficioLey"].ToString()),
                        idTasaInteresCorriente = Convert.ToInt32(fila["IdTasaInteres"].ToString()),
                        tasaInteresCorriente = fila["TasaInteres"].ToString(),
                        puntosAdicionales = string.IsNullOrEmpty(fila["PuntosAdicionales"].ToString()) ? (double?)null : Convert.ToDouble(fila["PuntosAdicionales"].ToString()),
                        idTasaMora = Convert.ToInt32(fila["IdTasaMora"].ToString()),
                        tasaMora = fila["TasaMora"].ToString(),
                        idPlazoObligacion = Convert.ToInt32(fila["IdPlazoObligacion"].ToString()),
                        plazoObligacion = fila["PlazoObligacion"].ToString(),
                        cantidadAnios = Convert.ToInt32(fila["CantidadAnios"].ToString()),
                        PorcentajeP1 = Convert.ToDouble(fila["PorcentajeP1"].ToString()),
                        PorcentajeP2 = string.IsNullOrEmpty(fila["PorcentajeP2"].ToString()) ? (double?)null : Convert.ToDouble(fila["PorcentajeP2"].ToString()),
                        idPeriocidadIntereses = Convert.ToInt32(fila["IdPeriocidadIntereses"].ToString()),
                        periocidadIntereses = fila["PeriocidadIntereses"].ToString(),
                        idPeriodoMuerto = Convert.ToInt32(fila["IdPeriodoMuerto"].ToString()),
                        periodoMuerto = fila["PeriodoMuerto"].ToString(),
                        cantidadAñosPeriodoMuerto = string.IsNullOrEmpty(fila["CantidadAniosPeriodoMuerto"].ToString()) ? (int?)null : Convert.ToInt32(fila["CantidadAniosPeriodoMuerto"].ToString()),
                        idPeriodogracia = Convert.ToInt32(fila["IdPeriodoGracia"].ToString()),
                        periodoGracia = fila["PeriodoGracia"].ToString(),
                        cantidadAñosPeriodoGracia = string.IsNullOrEmpty(fila["CantidadAniosPeriodoGracia"].ToString()) ? (int?)null : Convert.ToInt32(fila["CantidadAniosPeriodoGracia"].ToString()),
                        idBeneficioCapital = string.IsNullOrEmpty(fila["IdBeneficioCapital"].ToString()) ? (int?)null : Convert.ToInt32(fila["IdBeneficioCapital"].ToString()),
                        beneficioCapital = fila["BeneficioCapital"].ToString(),
                        valorBeneficioCapital = string.IsNullOrEmpty(fila["ValorBeneficioCapital"].ToString()) ? (double?)null : Convert.ToDouble(fila["ValorBeneficioCapital"].ToString()),
                        idBeneficioInteres = Convert.ToInt32(fila["IdBeneficiosInteres"].ToString()),
                        beneficioInteres = fila["BeneficiosInteres"].ToString(),
                        fechaInicioInteres = string.IsNullOrEmpty(fila["FechaInicioBeneficioInteres"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaInicioBeneficioInteres"].ToString()),
                        fechaFinInteres = string.IsNullOrEmpty(fila["FechaFinBeneficioInteres"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaFinBeneficioInteres"].ToString()),
                        idBeneficioSeguroVida = Convert.ToInt32(fila["IdBeneficioSeguroVida"].ToString()),
                        beneficioSeguroVida = fila["BeneficioSeguroVida"].ToString(),
                        fechaInicioSeguroVida = string.IsNullOrEmpty(fila["FechaInicioSeguroVida"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaInicioSeguroVida"].ToString()),
                        fechaFinSeguroVida = string.IsNullOrEmpty(fila["FechaFinSeguroVida"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaFinSeguroVida"].ToString()),
                        idCalificacionCartera = Convert.ToInt32(fila["IdCalificacionCartera"].ToString()),
                        calificacioCartera = fila["CalificacionCartera"].ToString(),
                        idCapitalizacionIntereses = Convert.ToInt32(fila["IdCapitalizacionIntereses"].ToString()),
                        capitalizacionIntereses = fila["CapitalizacionIntereses"].ToString(),
                        idOtrosBeneficios = Convert.ToInt32(fila["IdOtrosBeneficios"].ToString()),
                        otrosBeneficios = fila["OtrosBeneficios"].ToString(),
                        fechaInicioOtrosBeneficios = string.IsNullOrEmpty(fila["FechaInicioOtrosBeneficios"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaInicioOtrosBeneficios"].ToString()),
                        fechaFinOtrosBeneficios = string.IsNullOrEmpty(fila["FechaFinOtrosBeneficios"].ToString()) ? (DateTime?)null : Convert.ToDateTime(fila["FechaFinOtrosBeneficios"].ToString()),
                    };

                    this.lstPagaresBenefioLey.Add(pagareBeneficioLey);
                }
            }
            return lstPagaresBenefioLey.OrderBy(x => x.id).ToList();
        }

        /// <summary>
        /// registra o edita una obligacion nueva
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarPagares(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - RegistrarEditarPagares :: " + ex.Message));
            }
        }
    }
}
