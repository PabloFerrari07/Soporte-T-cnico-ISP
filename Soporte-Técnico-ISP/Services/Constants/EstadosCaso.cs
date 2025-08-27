namespace Soporte_Técnico_ISP.Services.Constants
{
    public class EstadosCaso
    {
        public const string Pendiente = "pendiente";
        public const string EnProgreso = "en_progreso";
        public const string Resuelto = "resuelto";

        public static readonly HashSet<string> Validos = new()
    { Pendiente, EnProgreso, Resuelto };
    }
}
