﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2025.Entidades
{
    public class CambioEstado
    {
        private int id;
        private DateTime? fechaHoraFin;
        private DateTime? fechaHoraInicio;
        private Estado estado;
        private int idEvento;

        public int Id { get => id; set => id = value; }
        public DateTime? FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public DateTime? FechaHoraInicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        public Estado EstadoActual { get => estado; set => estado = value; }
        public int IdEvento { get => idEvento; set => idEvento = value; }

        public CambioEstado(int id, DateTime? fechaHoraInicio, DateTime? fechaHoraFin, Estado estado, int idEvento)
        {
            this.id = id;
            this.fechaHoraInicio = fechaHoraInicio;
            this.estado = estado;
            this.fechaHoraFin = fechaHoraFin;
            this.idEvento = idEvento;
        }

        public CambioEstado()
        {
        }

        public bool esEstadoActual()
        {
            if (fechaHoraFin == null)
            {
                return true;
            }
            return false;
        }

        public bool esNoRevisado()
        {
            bool estadoEsAmbitoEventoSismico = this.EstadoActual.esAmbitoEventoSismico();
            bool estadoEsNoRevisado = this.EstadoActual.esNoRevisado();

            return estadoEsNoRevisado && estadoEsAmbitoEventoSismico;
        }

        public void setFechaHoraFin(DateTime fechaHoraActual)
        {
            this.FechaHoraFin = fechaHoraActual;
        }
    }
}
