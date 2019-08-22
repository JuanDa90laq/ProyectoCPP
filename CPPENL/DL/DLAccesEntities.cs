namespace CPPENL.Transversal
{
    using System.Collections.Generic;

    /// <summary>
    /// clase que contiene los elementos necesarios apra invocar un procedimiento almancendado
    /// </summary>
    public class DLAccesEntities
    {
        /// <summary>
        /// indica el nombre del procedimeinto a ejecutar
        /// </summary>
        public string procedimiento { get; set; }

        /// <summary>
        /// indica la consulta sql a ejecutar
        /// </summary>
        public string sqlQuery { get; set; }

        /// <summary>
        /// indica que tipo de ejecuicion se realizara 1 -> procedimiento almancenado, 2 -> consulta sql
        /// </summary>
        public int tipoejecucion { get;  set; }

        /// <summary>
        /// Lista de valores para la ejecucion de procedimeinto almacenados
        /// </summary>
        public List<ParametrizacionSPQUERY> parametros;
    }

    /// <summary>
    /// clase que mapea los parametros en base de datos
    /// </summary>
    public class ParametrizacionSPQUERY
    {
        /// <summary>
        /// parametro que recibe el SP
        /// </summary>
        public string parametro { get;  set; }

        /// <summary>
        /// valor del parametro
        /// </summary>
        public object parametroValor { get; set; }
    }

    /// <summary>
    /// se carga cuando se invoca la base de datos
    /// </summary>
    public class ResultConsulta
    {
        /// <summary>
        /// 1 -> Exitoso, 0 -> No Exitoso 
        /// </summary>
        public int estado { get; set; }

        /// <summary>
        /// Camptura el error 
        /// </summary>
        public string mensaje { get; set; }
    }

    /// <summary>
    /// mapea los departamentos
    /// </summary>
    public class Depatamentos
    {
        /// <summary>
        /// id del departamento
        /// </summary>
        public int id { get; set;}

        /// <summary>
        /// despartamento
        /// </summary>
        public string depratamento { get; set; }
    }

    /// <summary>
    /// mapea los municipios
    /// </summary>
    public class Municipios
    {
        /// <summary>
        /// id del municipio
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// municipio
        /// </summary>
        public string municipio { get; set; }

        /// <summary>
        /// id del departamento
        /// </summary>
        public int idDepto { get; set; }
    }

    /// <summary>
    /// se utiliza para registrar los log de acceso a datos 
    /// en la base de datos de cartera de primer piso
    /// </summary>
    public class LogAccesodatos
    {
        /// <summary>
        /// usuario que realiza la cosulta
        /// </summary>
        public int idUsuario { get; set; }

        /// <summary>
        /// formulario desde que se accede al procedimeinto
        /// </summary>
        public string formulario { get; set; }

        /// <summary>
        /// metodo que invoca desde la aplicacion
        /// </summary>
        public string metodoEnAplicacion { get; set; }

        /// <summary>
        /// sp que consume la palicacion
        /// </summary>
        public string procedimientoConsume { get; set; }

        /// <summary>
        /// accion que realiza sobre la bd
        /// </summary>
        public string accion { get; set; }

        /// <summary>
        /// error generado
        /// </summary>
        public string mensajeError { get; set; }

        /// <summary>
        /// usuario al que se le genera el error
        /// </summary>
        public int usuario { get; set; }
    }
}
