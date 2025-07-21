using System.Linq.Expressions;

namespace Helpdesk2._0.Data
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
                if (campo == null)
                    return aBoolean;

                if (System.Convert.IsDBNull(campo) || campo.Equals(null))
                {
                    return aBoolean;
                }
                else
                {
                    if (System.Convert.ToString(campo).Equals(""))
                    {
                        return aBoolean;

                    }

                }
                aBoolean = System.Convert.ToBoolean(campo);
                return aBoolean;

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
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
                // Captura cualquier otra excepción
                mensaje = ex.Message;
                return Guid.Empty; // O lanza una excepción si lo prefieres
            }
        }
        /// <summary>
        /// Dado un objeto lo retorna como un dato numérico
        /// </summary>
        /// <param name="campo"></param>
        /// <returns></returns>

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
            DateTime hora = new DateTime();

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
    }
}

