using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Usuario
    {
        private string usuario;
        private string clave;
        private bool habilitado;

        public string NombreDeUsuario { get => this.usuario; set => this.usuario = value; }
        public string Password { get => clave; set => clave = value; }
        public bool Habilitar { get => habilitado; set => habilitado = true; }

        public Usuario(string nombreUsuario, string password, bool habilitado)
        {
            this.usuario = nombreUsuario;
            this.clave = password;
            this.habilitado = habilitado;
        }

        public bool getASLogueado(string nombreUsuario)
        {
            usuario = "admin";
            clave = "admin";
            if (nombreUsuario.Equals(usuario))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
