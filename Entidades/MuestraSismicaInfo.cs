using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class MuestraSismicaInfo
    {
        public DateTime FechaHoraMuestra { get; set; }
        public List<DetalleMuestraSismicaInfo> DetallesMuestraSismica { get; set; }
    }
}
