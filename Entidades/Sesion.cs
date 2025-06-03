using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Sesion
    {
        public Usuario UsuarioActual { get; private set; }

        public Sesion()
        {
            UsuarioActual = null; 
        }

        public bool EstablecerUsuario(Usuario usuario) 
        {
            if (usuario != null)
            {
                UsuarioActual = usuario;
                Console.WriteLine($"[Sesion] Usuario '{usuario.NombreUsuario}' establecido en esta sesión.");
                return true;
            }
            else
            {
                Console.WriteLine("[Sesion] Intento de establecer un usuario nulo en la sesión.");
                return false;
            }
        }

        public Usuario conocerUsuario() 
        {
            if (UsuarioActual != null)
            {
                Console.WriteLine($"[Sesion] La sesión actual pertenece a: {UsuarioActual.getASLogueado()}");
                return UsuarioActual;
            }
            else
            {
                Console.WriteLine("[Sesion] No hay ningún usuario activo en esta sesión.");
                return null;
            }
        }
    }
}
