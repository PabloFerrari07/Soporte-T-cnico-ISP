namespace Soporte_Técnico_ISP.Models
{
    public class Caso
    {
        public int id { get; set; }

        public string titulo { get; set; }

        public string descripcion { get; set; }

        public string estado { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
