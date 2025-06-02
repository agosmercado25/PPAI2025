using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Sesion
    {
        private DateTime fechaHoraFin;
        private DateTime fechaHoraInicio;
        private Usuario usuarioSeleccionado;

        public DateTime FechaHoraInicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        public DateTime FechaHoraFin { get => this.fechaHoraFin; set => this.fechaHoraFin = value; }
        public Usuario UsuarioSeleccionado { get => this.usuarioSeleccionado; set => this.usuarioSeleccionado = value; }

        public Sesion(DateTime fechaHoraFin, DateTime fechaHoraInicio, Usuario usuarioSeleccionado)
        {
            this.fechaHoraFin = fechaHoraFin;
            this.fechaHoraInicio = fechaHoraInicio;
            this.usuarioSeleccionado = usuarioSeleccionado;
        }

        public Usuario conocerSesion(string nombreUsuario, string contraseña)
        {
            Usuario logueado = new Usuario(nombreUsuario, contraseña, true);
            this.usuarioSeleccionado = logueado;
            bool usu = logueado.getASLogueado(nombreUsuario);
            if (usu)
            {
                return logueado;
            }
            else
            {
                return null;
            }
        }
    }
}
