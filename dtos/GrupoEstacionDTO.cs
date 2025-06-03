using PPAI2025.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.dtos
{
    public class GrupoEstacionDTO
    {
        public string NombreEstacion { get; set; }
        public List<SerieTemporal> SeriesDeEstaEstacion { get; set; }
    }
}
