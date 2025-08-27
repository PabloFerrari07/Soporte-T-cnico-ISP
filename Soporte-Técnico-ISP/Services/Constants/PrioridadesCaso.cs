namespace Soporte_Técnico_ISP.Services.Constants
{
    public class PrioridadesCaso
    {
        public const string Baja = "baja";     // verde
        public const string Media = "media";   // amarillo
        public const string Alta = "alta";     // rojo

        public static readonly HashSet<string> Validas = new()
    { Baja, Media, Alta };

        public static string Color(string prioridad) => prioridad switch
        {
            Alta => "rojo",
            Media => "amarillo",
            _ => "verde"
        };

        // Para ordenar: Alta primero
        public static int Peso(string prioridad) => prioridad switch
        {
            Alta => 3,
            Media => 2,
            _ => 1
        };
    }
}
