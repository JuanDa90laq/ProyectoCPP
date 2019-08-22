namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Data;

    public class BLUsuarios
    {
        /// <summary>
        /// instacia el acceso a datos
        /// </summary>
        private readonly DLDataAcces acceso = new DLDataAcces();

        /// <summary>
        /// trae la informacion del usuario
        /// </summary>
        /// <param name="datos">objeto que contiene los parametros a consultar</param>
        /// <returns>objeto con la informacion del usuario</returns>
        public EntitiesUsuarios ConsultarUsuario(DLAccesEntities datos)
        {
            EntitiesUsuarios usuario = new EntitiesUsuarios();
            DataTable usuarioTable = this.acceso.Consultas(datos);

            if (usuarioTable != null && usuarioTable.Rows.Count > 0)
            {
                usuario.cppUsr_Idusuario = Convert.ToInt32(usuarioTable.Rows[0]["cppUsr_Idusuario"].ToString());
                usuario.cpp_IdPerfil = Convert.ToInt32(usuarioTable.Rows[0]["cpp_IdPerfil"].ToString());
                usuario.cppUsr_Identificacion = Convert.ToInt32(usuarioTable.Rows[0]["cppUsr_Identificacion"].ToString());
                usuario.cppUsr_TipoIdentificacion = Convert.ToInt32(usuarioTable.Rows[0]["cppUsr_TipoIdentificacion"].ToString());
                usuario.cppUsr_Activo = Convert.ToBoolean(usuarioTable.Rows[0]["cppUsr_Activo"].ToString());
                usuario.cppUsr_Nombre = usuarioTable.Rows[0]["cppUsr_Nombre"].ToString();
                usuario.cppUsr_Apellido= usuarioTable.Rows[0]["cppUsr_Apellido"].ToString();
                usuario.descripcionPerfil = usuarioTable.Rows[0]["cpp_descripcionPerfil"].ToString();             
            }

            return usuario;
        }
    }
}
