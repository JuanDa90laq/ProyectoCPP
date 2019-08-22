namespace CPPDL
{
    using CPPENL.Transversal;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class DLDataAcces
    {
        #region "variables pribadas"

        private SqlConnection cnn = new SqlConnection();
        private ResultConsulta result = null;

        #endregion

        #region "metodos publicos"

        /// <summary>
        /// utilizado para realizar consultas a la base de datos
        /// </summary>
        /// <param name="datos">objeto que contiene la parametrización a ejecutar</param>
        /// <returns>tabla con la informacion consultada</returns>
        public DataTable Consultas(DLAccesEntities datos)
        {
            DataTable resultTb = new DataTable();

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = datos.tipoejecucion.Equals(1) ? CommandType.StoredProcedure : CommandType.Text;
                comando.CommandText = datos.tipoejecucion.Equals(1) ? datos.procedimiento : datos.sqlQuery;

                if (datos.tipoejecucion.Equals(1) && datos.parametros != null)
                {
                    foreach (ParametrizacionSPQUERY param in datos.parametros)
                    {
                        ///comando.Parameters.AddWithValue(param.parametro, param.parametroValor);
                        this.CargarParametros(comando, param);
                    }
                }

                this.Conexion();
                comando.Connection = this.cnn;
                resultTb.Load(comando.ExecuteReader());
                this.CloseConexion();                
            }
            catch (Exception ex)
            {
                if (cnn != null && cnn.State.Equals(ConnectionState.Open))
                    this.CloseConexion();

                throw new Exception(ex.Message);
            }

            return resultTb;
        }

        /// <summary>
        /// utilizado para realizar consultas a la base de datos
        /// </summary>
        /// <param name="datos">objeto que contiene la parametrización a ejecutar</param>
        /// <returns>tablas con la informacion consultada</returns>
        public DataSet ConsultasSet(DLAccesEntities datos)
        {
            DataSet resultTb = new DataSet();

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = datos.tipoejecucion.Equals(1) ? CommandType.StoredProcedure : CommandType.Text;
                comando.CommandText = datos.tipoejecucion.Equals(1) ? datos.procedimiento : datos.sqlQuery;

                if (datos.tipoejecucion.Equals(1) && datos.parametros != null)
                {
                    foreach (ParametrizacionSPQUERY param in datos.parametros)
                        this.CargarParametros(comando, param);                    
                }

                this.Conexion();
                comando.Connection = this.cnn;
                IDataReader reader = comando.ExecuteReader();

                while (!reader.IsClosed)
                    resultTb.Tables.Add().Load(reader);

                    this.CloseConexion();
            }
            catch (Exception ex)
            {
                if (cnn != null && cnn.State.Equals(ConnectionState.Open))
                    this.CloseConexion();

                throw new Exception(ex.Message);
            }

            return resultTb;
        }

        /// <summary>
        /// realiza inserciones y actualizaciones a la base de datos
        /// </summary>
        /// <param name="datos">objeto que contiene la parametrización a ejecutar</param>
        /// <returns>objeto con la informacion del error</returns>
        public ResultConsulta Registros(DLAccesEntities datos)
        {
            result = new ResultConsulta();

            try
            {
                DataTable resultTb = new DataTable();
                SqlCommand comando = new SqlCommand();
                SqlParameter paramt = new SqlParameter();
                comando.CommandType = datos.tipoejecucion.Equals(1) ? CommandType.StoredProcedure : CommandType.Text;
                comando.CommandText = datos.tipoejecucion.Equals(1) ? datos.procedimiento : datos.sqlQuery;

                if (datos.tipoejecucion.Equals(1) && datos.parametros != null)
                {
                    foreach (ParametrizacionSPQUERY param in datos.parametros)
                        this.CargarParametros(comando, param);                    
                }

                this.Conexion();
                comando.Connection = this.cnn;
                resultTb.Load(comando.ExecuteReader());
                result.mensaje = resultTb.Rows[0]["Mensaje"].ToString();
                result.estado = Convert.ToInt32(resultTb.Rows[0]["estado"].ToString());
                this.CloseConexion();                
            }
            catch (Exception ex)
            {
                if (cnn != null && cnn.State.Equals(ConnectionState.Open))
                    this.CloseConexion();

                result.mensaje = ex.Message.ToString();
                result.estado = 0;

                throw new Exception(ex.Message);
            }

            return result;
        }
        
        /// <summary>
        /// realiza inserciones del log de acceso a la base de datos
        /// </summary>
        /// <param name="log">objeto que contiene el log</param>        
        public void RegLog(LogAccesodatos log)
        {
            result = new ResultConsulta();

            try
            {
                DataTable resultTb = new DataTable();
                SqlCommand comando = new SqlCommand();                
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "CPP_SP_LogCPP";

                comando.Parameters.AddWithValue("@cppLg_Formulario", log.formulario);
                comando.Parameters.AddWithValue("@cppLg_MetodoAplicacion", log.metodoEnAplicacion);
                comando.Parameters.AddWithValue("@cppLg_procedimiento", log.procedimientoConsume);
                comando.Parameters.AddWithValue("@cppLg_accion", log.accion);
                comando.Parameters.AddWithValue("@cppLg_Usuario", log.idUsuario);

                this.Conexion();
                comando.Connection = this.cnn;
                comando.ExecuteNonQuery();
                this.CloseConexion();
            }
            catch (Exception ex)
            {
                if (cnn != null && cnn.State.Equals(ConnectionState.Open))
                    this.CloseConexion();

                throw new Exception(ex.Message + " En el registro del log.");
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        ///realiza la conexion a la base de datos
        /// </summary>
        private void Conexion()
        {
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["CPP_DD"].ToString();
            cnn.Open();
        }

        /// <summary>
        /// realiza la liberacion de recursos y cierra la base de datos
        /// </summary>
        private void CloseConexion()
        {
            cnn.Dispose();
            cnn.Close();
        }

        private void CargarParametros(SqlCommand comando, ParametrizacionSPQUERY parametros)
        {
            SqlParameter paramt = null;

            switch (Type.GetTypeCode(parametros.parametroValor.GetType()))
            {
                case TypeCode.Int32:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.Int);
                    paramt.Value = parametros.parametroValor.ToString();
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.Int64:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.BigInt);
                    paramt.Value = parametros.parametroValor.ToString();
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.Decimal:                    
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.Decimal);
                    paramt.Value = parametros.parametroValor;
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.String:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.VarChar);
                    paramt.Value = parametros.parametroValor.ToString();
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.DateTime:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.Date);
                    paramt.Value = Convert.ToDateTime(parametros.parametroValor.ToString()).ToShortDateString();
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.Boolean:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.Bit);
                    paramt.Value = parametros.parametroValor.ToString();
                    comando.Parameters.Add(paramt);
                    break;

                case TypeCode.Double:
                    paramt = new SqlParameter(parametros.parametro, SqlDbType.Decimal);
                    paramt.Value = parametros.parametroValor;
                    comando.Parameters.Add(paramt);
                    break;
            }
        }

        #endregion

    }
}
