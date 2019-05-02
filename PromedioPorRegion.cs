using System;
using System.Collections.Generic;
using System.Linq;

namespace laboratorio7
{
    public class PromedioPorRegion : Estadistica
    {
        public PromedioPorRegion() : base("Promedio PSU y NEM por región")
        {
        }

        public override void MostrarEstadistica(List<Fila> filas)
        {
            var grupos = (
                from fila in filas
                group fila by fila.region
            );

            foreach(var grupo in grupos)
            {
                Console.WriteLine($"\nRegion: {grupo.Key}");
                PromedioNemPsu promedio = new PromedioNemPsu();
                promedio.MostrarEstadistica(grupo.ToList());
            }
        }
    }
}
