using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class SerieTemporalInfo
    {
        public string CondicionAlarma { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public DateTime FechaHoraInicioMuestras { get; set; }
        public float FrecuenciaMuestras { get; set; }
        public List<MuestraSismicaInfo> MuestrasSismicas { get; set; }
    }
}
