namespace CPPBL.Transversal
{
    using CPPDL;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    

    public class BLValidaciones_Logs : System.Web.UI.Page
    {

        private readonly DLDataAcces acceso = new DLDataAcces();        

        public void RegistroLogs(LogAccesodatos parametros)
        {
            try
            {
                this.acceso.RegLog(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLValidaciones_Logs - RegistroLogs :: " + ex.Message));
            }
        }

        /// <summary>
        /// escribe el log de errores en una ruta espacifica
        /// </summary>
        /// <param name="parametros"></param>
        public void GenerarLogError(LogAccesodatos parametros)
        {
            string sCarpeta = ConfigurationManager.AppSettings["LOG_ERRORES_CPP"].ToString();
            string id = parametros.idUsuario.Equals(0) ? "Usuario perdio la sesión" : parametros.idUsuario.ToString();
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "id_usuario :" + id);
            dict.Add(2, "Formulario : " + parametros.formulario);
            dict.Add(3, "Metodo : " + parametros.metodoEnAplicacion);
            dict.Add(4, "Mensaje : " + parametros.mensajeError);            
            dict.Add(7, "Fecha : " + DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"));

            DateTime aDate = DateTime.Now;
            string pathToFiles = Server.MapPath(sCarpeta);
            string path = pathToFiles + "CPPLog_" + aDate.ToString("yyyy'-'MM'-'dd") + ".txt";
            File.AppendAllLines(path, new String[] { "[-------------------  " + DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'") + "   ------------------]" });
            foreach (KeyValuePair<int, string> pair in dict)
                File.AppendAllLines(path, new String[] { pair.Value });
            File.AppendAllLines(path, new String[] { "[-------------------------------------------------------------------------]" });
        }
    }
}
