using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Sismografo
    {
        private int id;
        private DateTime fechaAdquisicion;
        private string nroSerie;
        private EstacionSismologica estacionSismologica;

        public int Id { get => id; set => id = value; }
        public DateTime FechaAdquisicion { get => fechaAdquisicion; set => fechaAdquisicion = value; }
        public string NroSerie { get => nroSerie; set => nroSerie = value; }
        public EstacionSismologica EstacionSismologica
        { get => estacionSismologica; set => estacionSismologica = value; }

        public Sismografo(int id, DateTime fechaAdquisicion, string nroSerie, EstacionSismologica estacionSismologica)
        {
            this.id = id;
            this.fechaAdquisicion = fechaAdquisicion;
            this.nroSerie = nroSerie;
            this.estacionSismologica = estacionSismologica;
        }

        public Sismografo()
        {
        }

        public EstacionSismologica getEstacionSismologica()
        {
            return this.EstacionSismologica.getNombre();
        }

    }
}
