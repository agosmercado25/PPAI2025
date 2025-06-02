using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class TipoDeDato
    {
        private int id;
        private string denominacion;
        private string nombreUnidadMedida;
        private float valorUmbral;

        public int Id { get => id; set => id = value; }
        public string Denominacion { get => denominacion; set => denominacion = value; }
        public string NombreUnidadMedida { get => nombreUnidadMedida; set => nombreUnidadMedida = value; }
        public float ValorUmbral { get => valorUmbral; set => valorUmbral = value; }

        public TipoDeDato(int id, string denominacion, string nombreUnidadMedida, float valorUmbral)
        {
            this.id = id;
            this.denominacion = denominacion;
            this.nombreUnidadMedida = nombreUnidadMedida;
            this.valorUmbral = valorUmbral;
        }

        public TipoDeDato()
        {
        }

        public string getDenominacion()
        {
            return this.Denominacion.ToString();
        }
    }
}
