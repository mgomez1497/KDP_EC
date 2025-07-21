namespace Helpdesk2._0.Data
{
    public static class Fechas
    {
        /// <summary>
        /// Dada una fecha como parámetro, retornar una cadena
        /// en formato yyyyMMdd
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public static string FechaBaseDatos(DateTime fecha)
        {
            string fechaNueva = null;
            DateTime dt = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            fechaNueva = String.Format("{0:yyyyMMdd}", dt);
            return fechaNueva;

        }

        /// <summary>
        /// Dada una fecha como parámetro, retornar una cadena
        /// en formato yyyyMMdd HH:mm
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public static string FechaHoraBaseDatos(DateTime fecha)
        {
            string fechaHoraNueva = null;
            DateTimeOffset dt = new DateTimeOffset(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, fecha.Second, TimeZoneInfo.Local.GetUtcOffset(fecha));
            fechaHoraNueva = String.Format("{0:yyyyMMdd HH:mm}", dt);
            return fechaHoraNueva;
        }

        /// <summary>
        /// Dada una fecha como parámetro, retornar una cadena
        /// en formato yyyyMMdd HH:mm:ss
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public static string FechaHoraSegBaseDatos(DateTime fecha)
        {
            string fechaHoraSegNueva = null;
            DateTimeOffset dt = new DateTimeOffset(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, fecha.Second, TimeZoneInfo.Local.GetUtcOffset(fecha));
            fechaHoraSegNueva = String.Format("{0:yyyyMMdd HH:mm:ss}", dt);
            return fechaHoraSegNueva;
        }


        /// <summary>
        /// Metodo que recibe un datetime y devuelve una cadena con formato  yyyyMMdd HH:mm:ss
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Fecha_Hora_Seg_Base_Datos(DateTime fecha)
        {

            string fechaHoraSegNueva = null;
            // DateTimeOffset dt;

            if (fecha.Year < 2000)
            {

                fecha = fecha.AddYears(2000);
                //dt = new DateTimeOffset(2000, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, fecha.Second, TimeZoneInfo.Local.GetUtcOffset(fecha));

            }
            else
            {
                //dt = new DateTimeOffset(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, fecha.Second, TimeZoneInfo.Local.GetUtcOffset(fecha));
            }
            fechaHoraSegNueva = String.Format("{0:yyyyMMdd HH:mm:ss}", fecha);
            return fechaHoraSegNueva;
        }

        /// <summary>
        /// Dada una fecha cualquiera, retornar la fecha
        /// correspondiente al primer día del mes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public static DateTime FechaInicioMes(DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }


        /// <summary>
        /// fecha mekano
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Fecha_Mekano(DateTime fecha)
        {
            string fechaNueva = null;
            DateTime dt = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            fechaNueva = String.Format("{0:MM/dd/yyyy}", dt);
            return fechaNueva;
        }


        /// <summary>
        /// fecha isolucion
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Fecha_Isolucion(DateTime fecha)
        {
            string fechaNueva = null;
            DateTime dt = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            fechaNueva = String.Format("{0:yyyy/MM/dd}", dt);
            return fechaNueva;
        }

        /// <summary>
        /// fecha isolucion
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Fecha_RNDC(DateTime fecha)
        {
            string fechaNueva = null;
            DateTime dt = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            fechaNueva = String.Format("{0:dd/mm/yyyy}", dt);
            return fechaNueva;
        }

        /// <summary>
        /// fecha isolucion
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Fecha_Isolucion_Completa(DateTime fecha)
        {
            string fechaNueva = null;
            DateTime dt = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            fechaNueva = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt);
            return fechaNueva;
        }

        /// <summary>
        /// Dada una fecha cualquiera, retornar la fecha
        /// correspondiente al último día del mes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public static DateTime FechaFinMes(DateTime fecha)
        {
            DateTime primerDiaMes = new DateTime(fecha.Year, fecha.Month, 1);
            return primerDiaMes.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Valida si el dato pasado es una fecha válida
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool EsFecha(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
