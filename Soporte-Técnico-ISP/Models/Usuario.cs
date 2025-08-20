using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Soporte_Técnico_ISP.Models
{
    public class Usuario
    {
        public int id { get; set; }

        public string nombre { get; set; }
        public string email { get; set; }

        public string rol { get; set; }

        public string passwordHash { get; set; }

        public ICollection<Caso> Casos { get; set; }
    }
}
