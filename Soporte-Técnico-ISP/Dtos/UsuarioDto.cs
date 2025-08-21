namespace Soporte_Técnico_ISP.Dtos
{
    public class UsuarioDto
    {
        public string nombre { get; set; }
        public string email { get; set; }

        public string rol { get; set; }

        public string passwordHash { get; set; }

        public string? Token { get; set; }
    }
}
