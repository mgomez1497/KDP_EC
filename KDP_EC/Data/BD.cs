using Microsoft.Data.SqlClient;
using System.Data;

namespace Helpdesk2._0.Data
{
    public class BD
    {
        public SqlConnection conexion;
        public string mensaje_Error { get; set; }

        public BD(SqlConnection Conexion)
        {
            conexion = Conexion;
        }


        /// <summary>
        /// Metodo que recibe una sentencia SQL y cuyo
        /// resultado debe ser cargado en un DataTable
        /// </summary>
        /// <param name = "sentenciaSQL" </param>
        /// <param name = "nombreAAsignar" </param>
        /// <param name = "transaccion" </param>
        /// <returns></returns>

        public DataTable cargar_Tabla(string sentenciaSQL, string nombreAAsignar = "", SqlTransaction transaccion = null)
        {
            DataTable dt = new DataTable(nombreAAsignar);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter(sentenciaSQL, conexion);
                adap.SelectCommand.Transaction = transaccion;
                adap.Fill(dt);
                adap.Dispose();

                return dt;

            }
            catch (Exception ex)
            {
                mensaje_Error = ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje_Error = ex.Message;
                }
                throw new Exception(ex.Message, ex);
            }
        }
        public List<string> cargar_Lista(string sql)
        {
            List<string> lista = new List<string>();

            DataTable dtp = new DataTable();

            dtp = cargar_Tabla(sql);

            foreach (DataRow row in dtp.Rows)
                foreach (DataColumn column in dtp.Columns)
                    lista.Add(row[column].ToString());

            return lista;
        }


        /// <summary>
        ///Metodo que recibe una sentencia SQL,
        /// cuyo resultado debe ser cargado a un
        /// datarow
        /// </summary>
        /// <param name = "sentenciaSQL" </param>
        /// <param name = "transaccion" </param>
        /// <returns></returns>

        public DataRow cargar_Fila(string sentenciaSQL, SqlTransaction transaccion = null)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter(sentenciaSQL, conexion);
                adap.SelectCommand.Transaction = transaccion;
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                return null;
            }
            catch (Exception ex)
            {
                mensaje_Error = ex.Message;
                if (ex.InnerException != null)
                    mensaje_Error = ex.Message;

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Metodo que recibe una sentencia SQL
        /// y devuelve un único valor de tipo Object
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <returns></returns>

        public Object escalar(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            Object resultado = null;
            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                SqlCommand com = new SqlCommand(sentenciaSql, conexion);
                com.Transaction = transaccion;
                resultado = com.ExecuteScalar();
                com.Dispose();
            }
            catch (Exception ex)
            {
                mensaje_Error = ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje_Error = ex.InnerException.Message;
                }
                if (lanzarExcepcion) throw new Exception(ex.Message, ex);
            }

            return resultado;
        }

        /// <summary>
        /// Retorna un valor escalar de tipo string
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <param name="lanzarExcepcion"></param>
        /// <returns></returns>
        public string escalarSTR(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            return Conv.AStr(escalar(sentenciaSql, transaccion, lanzarExcepcion));
        }


        /// <summary>
        /// Retorna un valor escalar de tipo Entero
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <param name="lanzarExcepcion"></param>
        /// <returns></returns>
        public int escalarENT(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            return Conv.AEntero(escalar(sentenciaSql, transaccion, lanzarExcepcion));
        }

        /// <summary>
        /// Retorna un valor escalar de tipo double
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <param name="lanzarExcepcion"></param>
        /// <returns></returns>
        public double escalarNUM(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            return Conv.ANum(escalar(sentenciaSql, transaccion, lanzarExcepcion));


        }

        /// <summary>
        /// Retorna un valor escalar de tipo booleano
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <param name="lanzarExcepcion"></param>
        /// <returns></returns>
        public bool escalarBOOL(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            return Conv.ABool(escalar(sentenciaSql, transaccion, lanzarExcepcion));
        }

        /// <summary>
        /// Retorna un valor escalar de tipo Fecha
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <param name="lanzarExcepcion"></param>
        /// <returns></returns>
        public DateTime escalarFecha(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            return Conv.AFecha(escalar(sentenciaSql, transaccion, lanzarExcepcion));
        }

        /// <summary>
        /// Función que permite ejecutar comandos SQL
        /// sobre la base de datos
        /// </summary>
        /// <param name="sentenciaSql"></param>
        /// <param name="transaccion"></param>
        /// <returns></returns>

        public bool ejecutar(string sentenciaSql, SqlTransaction transaccion = null, bool lanzarExcepcion = true)
        {
            bool resultado = false;

            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                SqlCommand com = new SqlCommand(sentenciaSql, conexion);
                com.Transaction = transaccion;
                com.CommandTimeout = 3600 * 4;
                //com.Connection.Open();
                com.ExecuteNonQuery();
                resultado = true;

            }
            catch (Exception ex)
            {
                mensaje_Error = ex.Message;

                if (ex.InnerException != null)
                {
                    mensaje_Error = ex.InnerException.Message;
                }
                if (lanzarExcepcion) throw new Exception(ex.Message, ex);
            }

            return resultado;
        }

        /// <summary>
        /// Función que retorna la fecha que tiene el
        /// servidor SQL
        /// </summary>
        /// <returns></returns>

        public DateTime fecha_ServidorSQL()
        {
            DateTime fechaServidor = DateTime.Now;
            try
            {
                //                SqlCommand com = new SqlCommand("SELECT CURRENT_TIMESTAMP", conexion);
                fechaServidor = escalarFecha("SELECT CURRENT_TIMESTAMP");
                //                com.Dispose();

            }
            catch (Exception ex)
            {
                fechaServidor = DateTime.Now;
                mensaje_Error = ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje_Error = ex.Message;
                }
            }
            return fechaServidor;
        }


    }
}

