namespace Soporte_Técnico_ISP.Dtos
{
    public class CasoDto
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }          // pendiente | en_progreso | resuelto
        public string prioridad { get; set; }       // baja | media | alta
        public int UsuarioId { get; set; }

        // Ayuda para UI (no se guarda en DB)
        public string colorPrioridad { get; set; }  // "verde" | "amarillo" | "rojo"
    }
}
