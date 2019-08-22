namespace CPPENL.Maestros
{
    using System;

    class EntitiesMaestros
    {
    }

    /// <summary>
    /// clase que mapea las columnas de la tabla menus
    /// </summary>
    public class EntitiesMenus
    {
        /// <summary>
        /// id primario de la tabla
        /// </summary>
        public int cppMn_IdMenu { get; set; }

        /// <summary>
        /// textop a visualizar el usuario
        /// </summary>
        public string cppMn_Texto { get; set; }

        /// <summary>
        /// descripcion de la funcionalidad podra ser usada como tooltip
        /// </summary>
        public string cppMn_Descripcion { get; set; }

        /// <summary>
        /// id del codigo del menu padre
        /// </summary>
        public int cppMn_idPadre { get; set; }

        /// <summary>
        /// url ruta a la funcionalidad
        /// </summary>
        public string cppMn_url { get; set; }

        /// <summary>
        /// indica si esta activa en el menu
        /// </summary>
        public bool cppMn_Activo { get; set; }
    }

    /// <summary>
    /// mapea la informacion de los usuarios del sistema CPP
    /// </summary>
    public class EntitiesUsuarios
    {
        public int cppUsr_Idusuario { get; set; }
        /// <summary>
        /// identificacion del usuario
        /// </summary>
        public int cppUsr_Identificacion { get; set; }
        /// <summary>
        /// tipo de identificacion del usuario
        /// </summary>
        public int cppUsr_TipoIdentificacion { get; set; }
        /// <summary>
        /// nombre del usuario
        /// </summary>
        public string cppUsr_Nombre { get; set; }
        /// <summary>
        /// apellido del usuario
        /// </summary>
        public string cppUsr_Apellido { get; set; }
        /// <summary>
        /// id del perfils asociado al usuario
        /// </summary>
        public int cpp_IdPerfil { get; set; }
        /// <summary>
        /// descripcion del perfil
        /// </summary>
        public string descripcionPerfil { get; set; }
        /// <summary>
        /// email del usuario
        /// </summary>
        public string cppUsr_email { get; set; }
        /// <summary>
        /// indica si el usuario esta activo o no
        /// </summary>
        public bool cppUsr_Activo { get; set; }

    }

    /// <summary>
    /// mapea la informacion de las actividades agropecuarias
    /// </summary>
    public class EntitiesActividadesAgropecuarias
    {
        /// <summary>
        /// id de la actividad
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// actividad agropecuaria
        /// </summary>
        public string actividad { get; set; }

        /// <summary>
        /// estaod de la actividad
        /// </summary>
        public bool estado { get; set; }

    }

    /// <summary>
    /// mapea la informacion de los beneficiarios tabla cpp_Datos_Beneficiario
    /// </summary>
    public class EntitiesBeneficiarios
    {
        /// <summary>
        /// identificador de la tabla
        /// </summary>
        public int identificador { get; set; }

        /// <summary>
        /// id del tipo de documento
        /// </summary>
        public int tipo_Documento { get; set; }

        /// <summary>
        /// tipo de documento
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// N° identificación
        /// </summary>
        public Int64 identificacion { get; set; }

        /// <summary>
        /// fecha de expedicion de la identificacion
        /// </summary>
		public DateTime? fecha_Expedicion { get; set; }

        /// <summary>
        /// nombre
        /// </summary>
		public string nombre { get; set; }

        /// <summary>
        /// apellido
        /// </summary>
        public string apellido { get; set; }

        /// <summary>
        /// nombre y apellido
        /// </summary>
        public string nombreCompleto { get; set; }

        /// <summary>
        /// direccion
        /// </summary>
        public string direccion { get; set; }

        /// <summary>
        /// telefono
        /// </summary>
        public Int64? telefono { get; set; }

        /// <summary>
        /// numero de celular
        /// </summary>
        public Int64? celular { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string correo { get; set; }

        /// <summary>
        /// montos de los activos
        /// </summary>
        public decimal? montos_Activos { get; set; }

        /// <summary>
        /// fecha de corte de los activos
        /// </summary>
        public DateTime? fecha_Corte_Activos { get; set; }

        /// <summary>
        /// id tipo de productor
        /// </summary>
        public int tipo_Productor { get; set; }

        /// <summary>
        /// tipo de productor
        /// </summary>
        public string productor { get; set; }

        /// <summary>
        /// id actividad agropecuaria
        /// </summary>
        public int actividad_Agropecuaria { get; set; }

        /// <summary>
        /// actividad agropecuaria
        /// </summary>
        public string actividad { get; set; }

        /// <summary>
        /// id departamento
        /// </summary>
        public int idDepartamento { get; set; }

        /// <summary>
        /// departamento
        /// </summary>
        public string departamento { get; set; }

        /// <summary>
        /// id municipio
        /// </summary  
        public int idMunicipio { get; set; }

        /// <summary>
        /// municipio
        /// </summary>
        public string municipio { get; set; }

    }

    /// <summary>
    /// mapea la informacion de los tipo de productor
    /// </summary>
    public class EntitiesTipoProductor
    {
        /// <summary>
        /// id del tipo de productor
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// productor
        /// </summary>
        public string productor { get; set; }

        /// <summary>
        /// estaod 
        /// </summary>
        public bool estadoProd { get; set; }

    }

    /// <summary>
    /// Mapea la informacion de los tipos de cesion
    /// </summary>
    public class EntitiesTipoCesion
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de los tipos de cesion
    /// </summary>
    public class EntitiesTipoCuenta
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// abonoCancelacion 
        /// </summary>
        public string abonoCancelacion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de la calificacion
    /// </summary>
    public class EntitiesCalificacion
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }

    }

    /// <summary>
    /// Mapea la informacion de los tipos de cuenta contable
    /// </summary>
    public class EntitiesCodigosCuentaContable
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id Cesion 
        /// </summary>
        public int idTipoCesion { get; set; }

        /// <summary>
        /// Cesion
        /// </summary>
        public string cesion { get; set; }

        /// <summary>
        /// Id Tipo cuenta 
        /// </summary>
        public int idTipoCuenta { get; set; }

        /// <summary>
        /// Cuenta
        /// </summary>
        public string cuenta { get; set; }

        /// <summary>
        /// Codigo cuenta
        /// </summary>
        public string codigoCuenta { get; set; }

        /// <summary>
        /// Nombre cuenta
        /// </summary>
        public string nombreCuenta { get; set; }

        /// <summary>
        /// Efectua Movimiento
        /// </summary>
        public bool efectuaMovimiento { get; set; }

        /// <summary>
        /// Id Calificacion
        /// </summary>
        public int idCalificacion { get; set; }

        /// <summary>
        /// Calificacion
        /// </summary>
        public string calificacion { get; set; }

        /// <summary>
        /// Campo concatenado para mostrat la cuenta y el nombre
        /// </summary>
        public string cuentaNombreCuenta { get; set; }

        /// <summary>
        /// Id Aplica cuenta
        /// </summary>
        public int idAplicaCuenta { get; set; }

        /// <summary>
        /// aplica cuenta descripcion
        /// </summary>
        public string aplicaCuenta { get; set; }

        /// <summary>
        /// id al cual pertenece el tipo de actividad
        /// </summary>
        public int cppCCC_IdCuenta { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de los bancos recaudadores y aseguradoras
    /// </summary>
    public class EntitiesBancoRecaudadorAse
    {
        /// <summary>
        /// id del banco recaudador
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Codigo de la entidad
        /// </summary>
        public int? codigoEntidad { get; set; }

        /// <summary>
        /// Nombre de la entidad
        /// </summary>
        public string nombreEntidad { get; set; }

        /// <summary>
        /// Nit de la entidad
        /// </summary>
        public int? nit { get; set; }

        /// <summary>
        /// Nombre de la entidad extendida
        /// </summary>
        public string nombreEntidadExtendido { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de los bancos recaudadores y aseguradoras contra las cuentas contables
    /// </summary>
    public class EntitiesBancoCuenta
    {
        /// <summary>
        /// id de la relacion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// id del banco
        /// </summary>
        public int idBanco { get; set; }

        /// <summary>
        /// id de la cuenta
        /// </summary>
        public int idCuenta { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de los tipos de cuenta si aplica
    /// </summary>
    public class EntitiesAplicaCuenta
    {
        /// <summary>
        /// id del tipo aplica cuenta
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// mapea los datos de la tabla cpp_Tipo_Plan_Pagos
    /// </summary>
    public class EntitiescppTipoPlanPagos
    {
        /// <summary>
        /// id del registro en la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion del tipo de plan de pagos
        /// </summary>
        public string tipoPlanPagos { get; set; }

        /// <summary>
        /// estado del regsitro 0 = incativo, 1 = activo
        /// </summary>
        public bool estado { get; set; }
    }

    /// <summary>
    /// mapea los datos de la tabla cpp_Modalidad_Capital
    /// </summary>
    public class EntitiescppModalidadCapital
    {
        /// <summary>
        /// id del registro en base de datos
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion de lamodalidad
        /// </summary>
        public string modalidad { get; set; }

        /// <summary>
        /// estado del registro en la base de datos 0 = inactivo, 1 = activo
        /// </summary>
        public bool estado { get; set; }
    }

    /// <summary>
    /// mapea los datos de la tabla cpp_Plan_Pagos
    /// </summary>
    public class EntitiesPlanPagos
    {
        /// <summary>
        /// identificador del registro en la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// codigo de la entidad bancaria
        /// </summary>
        public int idIntermediario { get; set; }

        /// <summary>
        /// nombre de la entidad bancaria
        /// </summary>
        public string nombreIntermediario { get; set; }

        /// <summary>
        /// Descuento por amortizar
        /// </summary>
        public double descuentoPorAmortizar { get; set; }

        /// <summary>
        /// Descuento amortizado
        /// </summary>
        public double descuentoAmortizado { get; set; }

        /// <summary>
        /// Descuento timbre
        /// </summary>
        public double impuestoTimbre { get; set; }

        /// <summary>
        /// Periocidad capital
        /// </summary>
        public int periocidadCapital { get; set; }

        /// <summary>
        /// Periodo de gracia
        /// </summary>
        public int periodoGracia { get; set; }

        /// <summary>
        /// Periodo muerto
        /// </summary>
        public int periodoMuerto { get; set; }

        /// <summary>
        /// Plazo total de la obligacion
        /// </summary>
        public int plazoTotalObligacion { get; set; }

        /// <summary>
        /// Numero de cuotas plan de pagos
        /// </summary>
        public int numeroCuotasPlanPagos { get; set; }

        /// <summary>
        /// id de plan pagos
        /// </summary>
        public int idPlanPagos { get; set; }

        /// <summary>
        /// descripcion plan pagos
        /// </summary>
        public string planPagos { get; set; }

        /// <summary>
        /// id de modalidad Capital
        /// </summary>
        public int idModalidadCapital { get; set; }

        /// <summary>
        /// descripcion modalidad de capital
        /// </summary>
        public string modalidadCapital { get; set; }

        /// <summary>
        /// Periocidad Intereses Corrientes
        /// </summary>
        public int IdperiocidadInteresesCorrientes { get; set; }

        /// <summary>
        /// Periocidad Intereses Corrientes
        /// </summary>
        public string periocidadInteresesCorrientes { get; set; }

        /// Tasa de intereses corrientes
        /// </summary>
        public double tasaInteresesCorrientes { get; set; }

        /// Puntos Contigentes Intereses corrientes
        /// </summary>
        public int? puntosContigentesInt { get; set; }

        /// Tasa de Intereses Moratorios 
        /// </summary>
        public double tasaInteresesMoratorios { get; set; }

        /// Fecha de pago
        /// </summary>
        public DateTime fechaPago { get; set; }

        /// Descripcion
        /// </summary>
        public string descripcion { get; set; }

        // <summary>
        /// Id - Intermediario
        /// </summary>
        public string idPlanPagoIntermediario { get; set; }
    }

    /// <summary>
    /// mapea la tabla cpp_Periodicidad_Intereses_Corrientes
    /// </summary>
    public class EntitiesPeriocidadInteresesCorrientes
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// estado 
        /// </summary>
        public bool estado { get; set; }

    }

    public class EntitiesCantidadPagares
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }
    }

    public class EntitiesCentroUtilidad
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Codigo centro utilidad
        /// </summary>
        public string codigoCentroUtilidad { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string codigodescripcion { get; set; }
    }

    public class EntitiesPrograma
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// nombre del programa
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// Fecha inicial
        /// </summary>
        public DateTime fechaInicial { get; set; }

        /// <summary>
        /// id del pagare
        /// </summary>
        public int idPagares { get; set; }

        /// <summary>
        /// descripcion del pagare
        /// </summary>
        public string pagares { get; set; }

        /// <summary>
        /// id del centro de utilidad
        /// </summary>
        public int idCentroUtilidad { get; set; }

        /// <summary>
        /// descripcion del centro de Utilidad
        /// </summary>
        public string centroUtilidad { get; set; }

        /// <summary>
        /// descripcion del programa
        /// </summary>
        public string descripcion { get; set; }
    }

    public class EntitiesProgramaPlanPago
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// id Programa
        /// </summary>
        public int idPrograma { get; set; }

        /// <summary>
        /// id Programa
        /// </summary>
        public string programa { get; set; }

        /// <summary>
        /// id Plan Pago
        /// </summary>
        public int idPlanPago { get; set; }

        /// <summary>
        /// id Plan Pago
        /// </summary>
        public string planPago { get; set; }

        /// <summary>
        /// Convenio
        /// </summary>
        public string convenio { get; set; }

    }

    /// <summary>
    /// calse que mapea los valores para la DTF y el IBR
    /// </summary>
    public class EntitiesDTFIBR
    {
        /// <summary>
        /// identificador de la tasa
        /// </summary>
        public int idtasa { get; set; }

        /// <summary>
        /// tipo de perididicidad
        /// </summary>
        public string periodicidad { get; set; }

        /// <summary>
        /// abreviatura del registro
        /// </summary>
        public string abreviatura { get; set; }

        /// <summary>
        /// simbolo empleado para identificar los registros
        /// </summary>
        public string simbolo { get; set; }

        /// <summary>
        /// fecha desde la que se activa el registro
        /// </summary>
        public DateTime fechaVigenciaDesde { get; set; }

        /// <summary>
        /// fecha en la que termina la vigencia del registro
        /// </summary>
        public DateTime fechaVigenciaHasta { get; set; }

        /// <summary>
        /// valor del registro
        /// </summary>
        public string valor { get; set; }

        /// <summary>
        /// inidca el tipo de registro
        /// </summary>
        public string historico { get; set; }

    }

    /// <summary>
    /// clase que envia los parametros al servicio DTF e IBR
    /// </summary>
    public class EntitieConsultarIBRDTF
    {
        /// <summary>
        /// fecha inicial IBR
        /// </summary>
        public DateTime? IBRfechaVigenciaDesde { get; set; }

        /// <summary>
        /// Fecha final IBR
        /// </summary>
        public DateTime? IBRfechaVigenciaHasta { get; set; }

        /// <summary>
        /// Fecha Inicial DTF
        /// </summary>
        public DateTime? DTFfechaVigenciaDesde { get; set; }

        /// <summary>
        /// Fecha final DTF
        /// </summary>
        public DateTime? DTFfechaVigenciaHasta { get; set; }

        /// <summary>
        /// accion a realizar 1 = trae todo, 2 = trae solo los registros mas actuales
        /// </summary>
        public int accion { get; set; }
    }

    /// <summary>
    ///clase que mapea la tabla cpp_Tipo_Concepto
    /// </summary>
    public class EntitiesTipoConcepto
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int cppTc_Id { get; set; }

        /// <summary>
        /// descripcion del concepto
        /// </summary>
        public string cppTc_Descripcion { get; set; }

        /// <summary>
        /// estado del registro
        /// </summary>
        public bool cppTc_Estado { get; set; }

    }

    /// <summary>
    ///clase que mapea la tabla cpp_ConceptosAnuales
    /// </summary>
    public class EntitiesConceptosAnuales
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int cppCp_Id { get; set; }

        /// <summary>
        /// id del concepto
        /// </summary>
        public int cppCp_IdTipoCon { get; set; }

        /// <summary>
        /// descripcion del concepto
        /// </summary>
        public string cppCp_Descripcion { get; set; }

        /// <summary>
        /// valor del registro
        /// </summary>
        public string cppCp_Valor { get; set; }

        /// <summary>
        /// estado del registro
        /// </summary>
        public DateTime? cppCp_FechaVigenciaDesde { get; set; }

        /// <summary>
        /// estado del registro
        /// </summary>
        public DateTime? cppCp_FechaVigenciaHasta { get; set; }

        /// <summary>
        /// indica si es un historico
        /// </summary>
        public string historico { get; set; }
    }

    /// <summary>
    ///clase que mapea la tabla cpp_SituacionJuridica
    /// </summary>
    public class EntitiesSituacionJuridica
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }

    }

    /// <summary>
    ///clase que mapea la tabla cpp_CodigoCIIU
    /// </summary>
    public class EntitiesCodigoCIIU
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Codigo CIIU
        /// </summary>
        public string codigoCIIU { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string codigoDescripcion { get; set; }

    }

    /// <summary>
    ///clase que mapea la tabla cpp_Obligacion
    /// </summary>
    public class EntitiesObligacion
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// id del programa
        /// </summary>
        public int idPrograma { get; set; }

        /// <summary>
        /// programa
        /// </summary>
        public string programa { get; set; }

        /// <summary>
        /// id del plan de pago
        /// </summary>
        public int? idPlanPago { get; set; }

        /// <summary>
        /// plan pago
        /// </summary>
        public string planPago { get; set; }

        /// <summary>
        /// id del plan de pago
        /// </summary>
        public int? idPlanPagoUnico { get; set; }

        /// <summary>
        /// plan pago
        /// </summary>
        public string planPagoUnico { get; set; }

        /// <summary>
        /// convenio
        /// </summary>
        public string convenio { get; set; }

        /// <summary>
        /// id del beneficiario
        /// </summary>
        public int idBeneficiario { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string beneficiario { get; set; }

        /// <summary>
        /// id del banco recaudador
        /// </summary>
        public int idBancoRecaudador { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string bancoRecaudador { get; set; }

        /// <summary>
        /// id de la situacion juridica
        /// </summary>
        public int idSituacionJuridica { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string situacionJuridica { get; set; }

        /// <summary>
        /// id de la situacion juridica
        /// </summary>
        public int operacionIntermediario { get; set; }

        /// <summary>
        /// base compra
        /// </summary>
        public double baseCompra { get; set; }

        /// <summary>
        /// porcentaje
        /// </summary>
        public double porcentaje { get; set; }

        /// <summary>
        /// Valor Pagado Finagro
        /// </summary>
        public double valorPagadoFinagro { get; set; }

        /// <summary>
        /// aporte dinero
        /// </summary>
        public double aporteDinero { get; set; }

        /// <summary>
        /// aporte financiado
        /// </summary>
        public double aporteFinanciado { get; set; }

        /// <summary>
        /// Valor cartera inicial
        /// </summary>
        public double valorCarteraInicial { get; set; }

        /// <summary>
        /// Fecha compra
        /// </summary>
        public DateTime fechaCompra { get; set; }

        /// <summary>
        /// Id del destino
        /// </summary>
        public int idDestino { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string destino { get; set; }

        /// <summary>
        /// Id actividad agropecuaria
        /// </summary>
        public int idActividadAgropecuaria { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string actividadAgropecuaria { get; set; }

        /// <summary>
        /// Id codigo CIIU
        /// </summary>
        public int idCodigoCIIU { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string codigoCIIU { get; set; }

        /// <summary>
        /// Id departamento compra
        /// </summary>
        public int idDepartamentoCompra { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string departamentoCompra { get; set; }

        /// <summary>
        /// Id departamento compra
        /// </summary>
        public int idMunicipioCompra { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string municipioCompra { get; set; }

        /// <summary>
        /// Id departamento origen
        /// </summary>
        public int idDepartamentoOrigen { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string departamentoOrigen { get; set; }

        /// <summary>
        /// Id departamento origen
        /// </summary>
        public int idMunicipioOrigen { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string municipioOrigen { get; set; }

        /// <summary>
        /// Id departamento inversion
        /// </summary>
        public int idDepartamentoInversion { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string departamentoInversion { get; set; }

        /// <summary>
        /// Id departamento inversion
        /// </summary>
        public int idMunicipioInversion { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string municipioInversion { get; set; }

        /// <summary>
        /// Id tipo de garantia
        /// </summary>
        public int? idTipoGarantia { get; set; }

        /// <summary>
        /// beneficiario
        /// </summary>
        public string tipoGarantia { get; set; }

    }

    /// <summary>
    /// mapea la informacion de los destinos de credito
    /// </summary>
    public class EntitiesDestinos
    {
        /// <summary>
        /// id de la actividad
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// actividad agropecuaria
        /// </summary>
        public string actividad { get; set; }

        /// <summary>
        /// estaod de la actividad
        /// </summary>
        public bool estado { get; set; }

    }

    /// <summary>
    /// Clase que mapea la administracion de los beneficios de capital
    /// </summary>
    public class EntitiesAdminBeneficiosCapital
    {
        /// <summary>
        /// identificador interno de la tabla cpp_Beneficios_Capital
        /// </summary>
        public int cppAb_Id { get; set; }

        /// <summary>
        /// identificador del programa asociado
        /// </summary>
        public int cppAb_IdPr { get; set; }

        /// <summary>
        /// descripcion del programa
        /// </summary>
        public string programa { get; set; }

        /// <summary>
        /// identificador de la condicion
        /// </summary>
        public int cppAb_IdCd { get; set; }

        /// <summary>
        /// descripcion de la condicion
        /// </summary>
        public string condicion { get; set; }

        /// <summary>
        /// identificador del departamento asociado
        /// </summary>
        public int cppAb_IdDepto { get; set; }

        /// <summary>
        /// descripcion del departamento
        /// </summary>
        public string depto { get; set; }

        /// <summary>
        /// identificador del municipio asocado
        /// </summary>
        public int cppAb_IdMun { get; set; }

        /// <summary>
        /// descripcion del municipio
        /// </summary>
        public string municipio { get; set; }

        /// <summary>
        /// identificador de la actividad economica asociada
        /// </summary>
        public int cppAb_IdActividad { get; set; }

        /// <summary>
        /// columna que contiene las actividades asociadas
        /// </summary>
        public string cppAb_IdActividadTotal { get; set; }

        /// <summary>
        /// decripcion de la actividad
        /// </summary>
        public string Actividad { get; set; }

        /// <summary>
        /// valor del beneficio
        /// </summary>
        public decimal valor { get; set; }

        /// <summary>
        /// fecha inicio
        /// </summary>
        public DateTime? cppAb_FechaInicio { get; set; }

        /// <summary>
        /// fecha final
        /// </summary>
        public DateTime? cppAb_FechaFinal { get; set; }

        /// <summary>
        /// detalle del beneficio
        /// </summary>
        public string cppAb_Descripcion { get; set; }

        /// <summary>
        /// valor del beneficio
        /// </summary>
        public decimal? segundoValor { get; set; }

    }

    /// <summary>
    /// mapea la tabla cpp_condiciones
    /// </summary>
    public class EntitiesCondiciones
    {
        /// <summary>
        /// indicador de la tabla 
        /// </summary>
        public int cppCd_Id { get; set; }

        /// <summary>
        /// descripcion del registro
        /// </summary>
        public string cppCd_descripcion { get; set; }

        /// <summary>
        /// estado del registro
        /// </summary>
        public bool cppCd_Estado { get; set; }

    }

    /// <summary>
    /// Mapea la informacion de los tipos de beneficiado
    /// </summary>
    public class EntitiesTipoBeneficiado
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de la vigencia de los beneficios de Ley
    /// </summary>
    public class EntitiesVigencia
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de la tasa de mora
    /// </summary>
    public class EntitiesTasaMora
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de el plazo de obligacion
    /// </summary>
    public class EntitiesPlazoObligacion
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de periodo muerto
    /// </summary>
    public class EntitiesPeriodoMuerto
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de periodo de gracia
    /// </summary>
    public class EntitiesPeriodoGracia
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de periodo de gracia
    /// </summary>
    public class EntitiesBeneficiosIntereses
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de periodo de gracia
    /// </summary>
    public class EntitiesBeneficiosSeguroVida
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de capitalizacion intereses
    /// </summary>
    public class EntitiesCapitalizacionIntereses
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    public class EntitiesTasaIntereses
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de la cabecera beneficio de ley
    /// </summary>
    public class EntitiesCabeceraBeneficioLey
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string nombreBeneficio { get; set; }

        /// <summary>
        /// Id del programa 
        /// </summary>
        public int idPrograma { get; set; }

        /// <summary>
        /// nombre del programa 
        /// </summary>
        public string programa { get; set; }

        /// <summary>
        /// Id del Departamento 
        /// </summary>
        public int idDepartamento { get; set; }

        /// <summary>
        /// nombre del departamento 
        /// </summary>
        public string departamento { get; set; }

        /// <summary>
        /// Id del municipio 
        /// </summary>
        public int idMunicipio { get; set; }

        /// <summary>
        /// nombre del municipio 
        /// </summary>
        public string municipio { get; set; }

        /// <summary>
        /// Fecha Inicial
        /// </summary>
        public DateTime fechaInicial { get; set; }

        /// <summary>
        /// Fecha Inicial
        /// </summary>
        public DateTime fechaFinal { get; set; }

        /// <summary>
        /// id cantidad de pagares
        /// </summary>
        public int idCantidadPagares { get; set; }

        /// <summary>
        /// cantidad de pagares
        /// </summary>
        public string cantidadPagares { get; set; }

        /// <summary>
        /// id cantidad de intermediario financiero
        /// </summary>
        public int? idIntermediario { get; set; }

        /// <summary>
        /// intermediario financiero
        /// </summary>
        public string intermediario { get; set; }

        /// <summary>
        /// Tope maximo
        /// </summary>
        public int? topeMaximo { get; set; }

        /// <summary>
        /// Id del tipo de beneficiado
        /// </summary>
        public int idTipoBeneficiado { get; set; }

        /// <summary>
        /// tipo de beneficiado
        /// </summary>
        public string tipoBeneficiado { get; set; }

        /// <summary>
        /// id de la actividad agropecuaria
        /// </summary>
        public int idActividadAgropecuaria { get; set; }

        /// <summary>
        /// id de la actividad agropecuaria
        /// </summary>
        public string actividadAgropecuaria { get; set; }

        /// <summary>
        /// id de la vigencia
        /// </summary>
        public int idVigencia { get; set; }

        /// <summary>
        /// vigencia
        /// </summary>
        public string vigencia { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de los pagares beneficio de ley 
    /// </summary>
    public class EntitiesPagareBeneficioLey
    {
        /// <summary>
        /// id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// id de la cabecera
        /// </summary>
        public int idCabecera { get; set; }

        /// <summary>
        /// id de la tasa de Interes corrie
        /// </summary>
        public int? idTasaInteresCorriente { get; set; }

        /// <summary>
        /// tasa de intereses 
        /// </summary>
        public string tasaInteresCorriente { get; set; }

        /// <summary>
        /// puntos Adicionales
        /// </summary>
        public double? puntosAdicionales { get; set; }

        /// <summary>
        /// id de la tasa de mora
        /// </summary>
        public int idTasaMora { get; set; }

        /// <summary>
        /// tasa de mora
        /// </summary>
        public string tasaMora { get; set; }

        /// <summary>
        /// id del plazo de obligacion
        /// </summary>
        public int idPlazoObligacion { get; set; }

        /// <summary>
        /// plazo de obligacion
        /// </summary>
        public string plazoObligacion { get; set; }

        /// <summary>
        /// cantidad de años
        /// </summary>
        public int cantidadAnios { get; set; }

        /// <summary>
        /// Porcentaje Distribuccion Base compra P1
        /// </summary>
        public double PorcentajeP1 { get; set; }

        /// <summary>
        /// Porcentaje Distribuccion Base compra P1
        /// </summary>
        public double? PorcentajeP2 { get; set; }

        /// <summary>
        /// id de la periocidad intereses
        /// </summary>
        public int idPeriocidadIntereses { get; set; }

        /// <summary>
        /// periocidad intereses
        /// </summary>
        public string periocidadIntereses { get; set; }

        /// <summary>
        /// id del periodo muerto
        /// </summary>
        public int idPeriodoMuerto { get; set; }

        /// <summary>
        /// Periodo muerto
        /// </summary>
        public string periodoMuerto { get; set; }

        /// <summary>
        /// cantidad de años periodo muerto
        /// </summary>
        public int? cantidadAñosPeriodoMuerto { get; set; }

        /// <summary>
        /// id de la periodo de gracia
        /// </summary>
        public int idPeriodogracia { get; set; }

        /// <summary>
        /// Periodo de gracia
        /// </summary>
        public string periodoGracia { get; set; }

        /// <summary>
        /// cantidad de años periodo gracia
        /// </summary>
        public int? cantidadAñosPeriodoGracia { get; set; }

        /// <summary>
        /// id del beneficio capital
        /// </summary>
        public int? idBeneficioCapital { get; set; }

        /// <summary>
        /// Periodo de gracia
        /// </summary>
        public string beneficioCapital { get; set; }

        /// <summary>
        /// Valor de beneficios Capital
        /// </summary>
        public double? valorBeneficioCapital { get; set; }

        /// <summary>
        /// id de beneficios intereses
        /// </summary>
        public int idBeneficioInteres { get; set; }

        /// <summary>
        /// beneficio Interes
        /// </summary>
        public string beneficioInteres { get; set; }

        /// <summary>
        /// Fecha Inicio beneficio interes
        /// </summary>
        public DateTime? fechaInicioInteres { get; set; }

        /// <summary>
        /// Fecha fin beneficio interes
        /// </summary>
        public DateTime? fechaFinInteres { get; set; }

        /// <summary>
        /// id de beneficios seguro de vida
        /// </summary>
        public int idBeneficioSeguroVida { get; set; }

        /// <summary>
        /// seguro de vida
        /// </summary>
        public string beneficioSeguroVida { get; set; }

        /// <summary>
        /// Fecha Inicio beneficio Seguro vida
        /// </summary>
        public DateTime? fechaInicioSeguroVida { get; set; }

        /// <summary>
        /// Fecha fin beneficio seguro de vida
        /// </summary>
        public DateTime? fechaFinSeguroVida { get; set; }

        /// <summary>
        /// id de calificacion cartera
        /// </summary>
        public int idCalificacionCartera { get; set; }

        /// <summary>
        /// calificacion cartera
        /// </summary>
        public string calificacioCartera { get; set; }

        /// <summary>
        /// id de calificacion cartera
        /// </summary>
        public int idCapitalizacionIntereses { get; set; }

        /// <summary>
        /// calificacion cartera
        /// </summary>
        public string capitalizacionIntereses { get; set; }

        /// <summary>
        /// id de beneficios seguro de vida
        /// </summary>
        public int idOtrosBeneficios { get; set; }

        /// <summary>
        /// seguro de vida
        /// </summary>
        public string otrosBeneficios { get; set; }

        /// <summary>
        /// Fecha Inicio beneficio Seguro vida
        /// </summary>
        public DateTime? fechaInicioOtrosBeneficios { get; set; }

        /// <summary>
        /// Fecha fin beneficio seguro de vida
        /// </summary>
        public DateTime? fechaFinOtrosBeneficios { get; set; }

    }

    /// <summary>
    /// clase que mapea la tabla cpp_TiposCuenta
    /// </summary>
    public class EntitiesTiposCuenta
    {
        /// <summary>
        /// identificadro del registro
        /// </summary>
        public int cppCt_Id { get; set; }

        /// <summary>
        /// nombre de la cuenta
        /// </summary>
        public string cppCt_Nombre { get; set; }

        /// <summary>
        /// estado de la cuenta
        /// </summary>
        public bool cppCt_Estado { get; set; }
    }

    /// <summary>
    /// mapea la tabla cpp_InterfacesContables
    /// </summary>
    public class EntitiesInterfaz
    {
        /// <summary>
        /// id del registro en base de datos
        /// </summary>
        public int cppIn_id { get; set; }

        /// <summary>
        /// descripcion del registro
        /// </summary>
        public string cppIn_descripcion { get; set; }

        /// <summary>
        /// estado del registro
        /// </summary>
        public bool cppIn_estado { get; set; }
    }

    /// <summary>
    /// Mapea la tabla cpp_Interfaz_Cuenta
    /// </summary>
    public class EntitiesInterfazCuenta
    {
        /// <summary>
        /// identificador del registro en la tabla
        /// </summary>
        public int cppCPt_id { get; set; }

        /// <summary>
        /// identificador del tipo de interfaz
        /// </summary>
        public int cppIn_id { get; set; }

        /// <summary>
        /// descripcion de la interfaz
        /// </summary>
        public string interfaz { get; set; }

        /// <summary>
        /// identificador de tipo de cesion 
        /// </summary>
        public int cppTp_id { get; set; }

        /// <summary>
        /// descripcion de la cesion
        /// </summary>
        public string cesion { get; set; }

        /// <summary>
        /// identificador del tipo de cuenta
        /// </summary>
        public int cppTc_id { get; set; }

        /// <summary>
        /// descripcion de la cuenta
        /// </summary>
        public string cuenta { get; set; }

        /// <summary>
        /// identificador del tipo de calificacion
        /// </summary>
        public int cppCa_id { get; set; }

        /// <summary>
        /// descripcion de la calificacion
        /// </summary>
        public string calificacion { get; set; }

        /// <summary>
        /// id's de las cuentas asociadas
        /// </summary>
        public string cppCuentas { get; set; }

    }

    /// <summary>
    /// mapea la tabla de codeudor
    /// </summary>
    public class EntitiesCodeudor
    {
        /// <summary>
        /// identificador del registro en la tabla
        /// </summary>
        public int cppCo_Id { get; set; }

        /// <summary>
        /// identificador del tipo de documento
        /// </summary>
        public int cppCo_IdTipoiden { get; set; }

        /// <summary>
        /// tipo de documento
        /// </summary>
        public string Tipoiden { get; set; }

        /// <summary>
        /// numero del documento
        /// </summary>
        public Int64 cppCo_Identificacion { get; set; }

        /// <summary>
        /// nombre codeudor
        /// </summary>
        public string cppCo_Nombre { get; set; }

        /// <summary>
        /// apellido del codeudor
        /// </summary>
        public string cppCo_Apellido { get; set; }

        /// <summary>
        /// nombre completo del codeudor
        /// </summary>
        public string nombreCompleto { get; set; }

        /// <summary>
        /// direccion del codeudor
        /// </summary>
        public string cppCo_Direccion { get; set; }

        /// <summary>
        /// telefono del codeudor
        /// </summary>
        public Int64? cppCo_Telefono { get; set; }

        /// <summary>
        /// celular del codeudor
        /// </summary>
        public Int64? cppCo_Celular { get; set; }

        /// <summary>
        /// email del codeudor
        /// </summary>
        public string cppCo_email { get; set; }

        /// <summary>
        /// identificador del departamento
        /// </summary>
        public int cppCo_IdDepto { get; set; }

        /// <summary>
        /// departamento
        /// </summary>
        public string departamento { get; set; }

        /// <summary>
        /// identificador del municipio
        /// </summary>
        public int cppCo_Idmun { get; set; }

        /// <summary>
        /// municipio
        /// </summary>
        public string municipio { get; set; }

        /// <summary>
        /// municipio
        /// </summary>
        public string CedulaNombre { get; set; }
    }

    /// <summary>
    ///clase que mapea la tabla tipo garantia
    /// </summary>
    public class EntitiesTipoGarantia
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }

    }

    /// <summary>
    ///clase que mapea la tabla tipo de inmueble
    /// </summary>
    public class EntitiesTipoInmueble
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string descripcion { get; set; }
    }

    /// <summary>
    ///clase que mapea la tabla tipo de inmueble
    /// </summary>
    public class EntitiesObligacionInmueble
    {
        /// <summary>
        /// identificador dentro de la tabla
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// id de la obligacion
        /// </summary>
        public int idObligacion { get; set; }

        /// <summary>
        /// id del tipo del inmueble
        /// </summary>
        public int idtipoInmueble { get; set; }

        /// <summary>
        /// tipo del inmueble
        /// </summary>
        public string tipoInmueble { get; set; }

        /// <summary>
        /// matricula inmobiliaria
        /// </summary>
        public string matriculaInmobiliaria { get; set; }

        /// <summary>
        /// direccion 
        /// </summary>
        public string direccion { get; set; }

        /// <summary>
        /// direccion 
        /// </summary>
        public double valorInmueble { get; set; }
    }

    /// <summary>
    /// Mapea la informacion de otros beneficios
    /// </summary>
    public class EntitiesOtrosBeneficios
    {
        /// <summary>
        /// id del tipo de cesion
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descripcion 
        /// </summary>
        public string descripcion { get; set; }
    }
}
