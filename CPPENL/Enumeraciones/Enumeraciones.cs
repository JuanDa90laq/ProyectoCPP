
namespace CPPENTL.Enumeraciones
{
    class Enumeraciones
    {
    }

    /// <summary>
    /// controla el tipo de ejecucion en la base de datos 1 - SP, 2 - Consulta Sql
    /// </summary>
    public enum TiposEjecucion
    {
        Procedimiento = 1,
        ConsultaSql = 2
    }

    public enum TipoDato
    {
        int_ = 1,
        decimal_ = 2,
        string_ = 3,
        date_ = 4,
        bool_ = 5
    }

    public enum TipoAccionLog
    {
        actualizacion = 1,
        consulta = 2,
        insercion = 3,
        otro = 4
    }
}
