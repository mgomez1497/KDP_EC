using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infraestructure.DBContexto.Conversiones
{
        public class Conv
        {
            static string mensaje;

            /// <summary>
            /// Dado un objeto lo retorna como booleano
            /// </summary>
            /// <param name="campo"></param>
            /// <returns></returns>
            /// 



            public static bool ABool(Object campo)
            {
                bool aBoolean = false;

                try
                {
                    // Verificar si el campo es null o DBNull
                    if (campo == null || System.Convert.IsDBNull(campo))
                        return aBoolean;

                    // Convertir el campo a string para evaluar valores como "1", "0", "true", "false"
                    string campoStr = System.Convert.ToString(campo).Trim();

                    // Si el campo es una cadena vacía o solo espacios, devolvemos false
                    if (string.IsNullOrEmpty(campoStr))
                        return aBoolean;

                    // Convertir valores específicos ("1", "0") a booleanos
                    if (campoStr == "1")
                        return true;
                    if (campoStr == "0")
                        return false;

                    // Intentar convertir cualquier otro valor a booleano usando Convert
                    aBoolean = System.Convert.ToBoolean(campoStr);
                }
                catch (FormatException)
                {
                    // Manejar casos en los que el valor no puede convertirse a booleano
                    // Si ocurre una excepción de formato, devolvemos false
                }
                catch (Exception ex)
                {
                    // Cualquier otra excepción se captura y podrías manejarla o registrar el error
                    mensaje = ex.Message; // Si tienes una variable 'mensaje' definida globalmente
                }

                return aBoolean;
            }

            /// <summary>
            /// Dado un objeto lo retorna como una cadena
            /// </summary>
            /// <param name="campo"></param>
            /// <returns></returns>

            public static string AStr(Object campo)
            {
                string aString = "";
                try
                {
                    if (campo == null)
                        return aString;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return aString;
                    }
                    else
                    {
                        aString = System.Convert.ToString(campo);
                        return aString;
                    }
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;  // System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return aString;
            }
        /// <summary>
        /// Dado un objeto lo retorna como un dato numérico
        /// </summary>
        /// <param name="campo"></param>
        /// <returns></returns>

        public static Guid AGuid(object campo)
        {
            // Devuelve un Guid vacío si el campo es nulo o DBNull
            if (campo == null || System.Convert.IsDBNull(campo))
            {
                return Guid.Empty; // O Guid.NewGuid() si prefieres un nuevo Guid
            }

            try
            {
                // Intenta convertir el campo a Guid
                return Guid.Parse(campo.ToString());
            }
            catch (FormatException)
            {
                // Manejo de errores: el formato no es válido
                mensaje = "El formato del GUID no es válido.";
                return Guid.Empty; // O lanza una excepción si lo prefieres
            }
            catch (Exception ex)
            {
                
                mensaje = ex.Message;
                return Guid.Empty; // O lanza una excepción si lo prefieres
            }
        }
        public static double ANum(Object campo)
            {
                double aNumerico = 0;

                try
                {
                    if (campo == null)
                        return aNumerico;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return aNumerico;
                    }

                    //if (campo.GetType() == typeof(string))
                    //    if (double.TryParse(campo.ToString(), out aNumerico))
                    //        return aNumerico;

                    aNumerico = System.Convert.ToDouble(campo);
                    return aNumerico;



                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;  //  System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return aNumerico;


            }

            /// <summary>
            /// Dado un objeto lo retorna como un dato numérico
            /// </summary>
            /// <param name="campo"></param>
            /// <returns></returns>

            public static decimal ADec(Object campo)
            {
                decimal aNumerico = 0;

                try
                {
                    if (campo == null)
                        return aNumerico;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return aNumerico;
                    }

                    //if (campo.GetType() == typeof(string))
                    //    if (double.TryParse(campo.ToString(), out aNumerico))
                    //        return aNumerico;

                    aNumerico = System.Convert.ToDecimal(campo);
                    return aNumerico;

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;  //  System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return aNumerico;


            }

            public static Int32 AEntero(Object campo)
            {
                int aNumerico = 0;

                try
                {
                    if (campo == null)
                        return aNumerico;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return aNumerico;
                    }

                    aNumerico = System.Convert.ToInt32(campo);
                    return aNumerico;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;  //  System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return aNumerico;
            }

            public static Int64 ALong(Object campo)
            {
                long aNumerico = 0;

                try
                {
                    if (campo == null)
                        return aNumerico;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return aNumerico;
                    }

                    aNumerico = System.Convert.ToInt64(campo);
                    return aNumerico;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return aNumerico;
            }

            /// <summary>
            /// Dado un objeto lo retorna como una fecha, o sólo hora si está vacío
            /// </summary>
            /// <param name="campo"></param>
            /// <returns></returns>

            public static DateTime AFecha(Object campo)
            {
                DateTime hora = new DateTime(1900, 1, 1);

                try
                {
                    if (campo == null)
                        return hora;

                    if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return hora;
                    }
                    hora = System.Convert.ToDateTime(campo);
                    return hora;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message; // System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return hora;

            }


            public static DateTime? AFechaNullable(object campo)
            {
                try
                {
                    if (campo == null || System.Convert.IsDBNull(campo) || campo.Equals(null))
                    {
                        return null; // Retorna null si el campo es nulo o DBNull
                    }

                    return System.Convert.ToDateTime(campo); // Convierte el valor a DateTime
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (puedes registrar o manejar el mensaje si lo necesitas)
                    mensaje = ex.Message; // Opcional: registrar el mensaje de error
                }

                return null; // Retorna null si ocurre una excepción
            }
        }
}
