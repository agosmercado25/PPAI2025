using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }

        public Usuario(int idUsuario, string nombreUsuario)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
        }

        public string getASLogueado()
        {
            return $"ID: {IdUsuario}, Nombre: {NombreUsuario}";
        }
    }
}
