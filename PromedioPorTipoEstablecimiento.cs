using System;
using System.Collections.Generic;
using System.Linq;

namespace laboratorio7
{
    public class PromedioPorTipoEstablecimiento : Estadistica
    {
        public PromedioPorTipoEstablecimiento() : base("Promedio PSU y NEM por tipo de establecimiento")
        {
        }

        public override void MostrarEstadistica(List<Fila> filas)
        {
            var grupos = (
                from fila in filas
                group fila by fila.administration
            );

            foreach (var grupo in grupos)
            {
                Console.WriteLine($"\nTipo de Establecimiento: {grupo.Key}");
                PromedioNemPsu promedio = new PromedioNemPsu();
                promedio.MostrarEstadistica(grupo.ToList());
            }
        }
    }
}
