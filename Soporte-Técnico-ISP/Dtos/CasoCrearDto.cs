namespace Soporte_Técnico_ISP.Dtos
{
    public class CasoCrearDto
    {
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string? estado { get; set; }      // opcional; default "pendiente"
        public string? prioridad { get; set; }   // opcional; default "baja"
    }
}
