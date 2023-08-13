using DemoJWTNetCore.Models;

namespace DemoJWTNetCore.Models
{
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public string rol { get; set; }

        public static List<Usuario> DB()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    idUsuario = "1",
                    nombre = "Esteban",
                    password = "3",
                    rol = "empleado",
                },
                new Usuario
                {
                    idUsuario = "2",
                    nombre = "Sara",
                    password = "3",
                    rol = "empleado",
                },
                new Usuario
                {
                    idUsuario = "3",
                    nombre = "Nieves",
                    password = "3",
                    rol = "asesor",
                },
                new Usuario
                {
                    idUsuario = "4",
                    nombre = "Maritza",
                    password = "3",
                    rol = "administrador",
                },
            };
            return list;
        }
    }
}
                 