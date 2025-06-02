using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class MuestraSismica
    {
        private int id;
        private DateTime fechaHoraMuestra;
        private List<DetalleMuestraSismica> detalleMuestraSismica;
        private int idSerieTemporal;

        public int Id { get => id; set => id = value; }
        public DateTime FechaHoraMuestra { get => fechaHoraMuestra; set => fechaHoraMuestra = value; }
        public List<DetalleMuestraSismica> DetalleMuestraSismica { get => detalleMuestraSismica; set => detalleMuestraSismica = value; }
        public int IdSerieTemporal { get => idSerieTemporal; set => idSerieTemporal = value; }

        public MuestraSismica(int id, DateTime fechaHoraMuestra, List<DetalleMuestraSismica> detalleMuestraSismica, int idSerieTemporal)
        {
            this.id = id;
            this.fechaHoraMuestra = fechaHoraMuestra;
            this.detalleMuestraSismica = detalleMuestraSismica;
            this.idSerieTemporal = idSerieTemporal;
        }

        public MuestraSismica()
        {
        }

        public object getDatos()
        {
            return new
            {
                id = this.Id,
                fechaMuestra = this.FechaHoraMuestra.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                detalles = this.DetalleMuestraSismica?.Select(d => d.getDatos()).ToList()
            };
        }
        /*
        public MuestraSismicaInfo getDatos() // Renamed to follow naming conventions
        {
            var muestraInfo = new MuestraSismicaInfo
            {
                FechaHoraMuestra = FechaHoraMuestra, 
                DetallesMuestraSismica = ObtenerDetallesDeMuestra()
            };
            return muestraInfo; // Ensure a value is returned
        }
        */
    }
}

