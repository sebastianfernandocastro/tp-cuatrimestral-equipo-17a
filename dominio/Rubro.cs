﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Rubro
    {
        public int Id {  get; set; }    
        public string Nombre { get; set; }    
        public string Descripcion { get; set; }
        public Imagen imagen { get; set; }
        public int Estado { get; set; } 
    }
}
