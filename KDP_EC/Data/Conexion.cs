using Microsoft.Data.SqlClient;

using System.Data;
using System.Configuration;

namespace Helpdesk2._0.Data
{
    public class Conexion
    {
        public static SqlConnection conexion;
        private static string cadenaConexion;

        public static string instanciaSqlserver;
        public static string nombreBaseDatos;
        private static string bd_Pass;
        static bool conexion_OK;
        public static string mensajeError;

        public static void Abrir_Conexion()
        {
          
            mensajeError = "";
            conexion_OK = false;
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                    conexion_OK = true;
                }

                conexion_OK = true;
                
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }

        }

        /// <summary>
        /// Carga la información de los datos personalizados de la aplicación
        /// </summary>

        public static void  SetCadenadeconexion(string cadena )
        {
            cadenaConexion = cadena;  
        }



    }
}
