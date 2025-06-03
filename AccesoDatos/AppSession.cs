using PPAI2025.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.AccesoDatos
{
    public static class AppSession
    {
        public static Sesion SesionActual { get; private set; } = new Sesion();

        public static void InicializarSesionAdmin()
        {
            Usuario adminUsuario = new Usuario(999, "Admin"); 
            SesionActual.EstablecerUsuario(adminUsuario); 
        }

        public static Usuario GetUsuarioLogueado()
        {
            return SesionActual.conocerUsuario();
        }

    }
}
