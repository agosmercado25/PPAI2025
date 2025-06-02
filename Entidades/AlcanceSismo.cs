﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class AlcanceSismo
    {
        private int id;
        private string descripcion;
        private string nombre;

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public AlcanceSismo(int id, string descripcion, string nombre)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.nombre = nombre;
        }

        public AlcanceSismo()
        {
        }

        public String getNombre()
        {
            return Nombre.ToString();
        }
    }
}
