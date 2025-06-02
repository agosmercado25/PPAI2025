﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class ClasificacionSismo
    {
        private int id;
        private float kmProfundidadDesde;
        private float kmProfundidadHasta;
        private string nombre;

        public int Id { get => id; set => id = value; }
        public float KmProfundidadDesde { get => kmProfundidadDesde; set => kmProfundidadDesde = value; }
        public float KmProfundidadHasta { get => kmProfundidadHasta; set => kmProfundidadHasta = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public ClasificacionSismo(int id, float kmProfundidadDesde, float kmProfundidadHasta, string nombre)
        {
            this.id = id;
            this.kmProfundidadDesde = kmProfundidadDesde;
            this.kmProfundidadHasta = kmProfundidadHasta;
            this.nombre = nombre;
        }

        public ClasificacionSismo()
        {
        }

        public String getNombre()
        {
            return Nombre.ToString();
        }
    }
}
