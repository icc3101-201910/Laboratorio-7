using System;
using System.Collections.Generic;
using System.Linq;

namespace laboratorio7
{
    public class MejoresPsu : Estadistica
    {
        public MejoresPsu() : base("Las 10 comunas con mejor promedio PSU")
        {
        }

        public override void MostrarEstadistica(List<Fila> filas)
        {
            var promedio = new PromedioNemPsu();

            var promediosPorComuna =
                            from fila in filas
                            group fila by fila.comuna into datosComuna
                            select new PromedioComuna(datosComuna.Key, promedio.CalcularPromedioPsu(datosComuna.ToList()));

            var promediosOrdenados = promediosPorComuna
                .OrderByDescending(resultado => resultado.promedio)
                .Take(10)
                .ToList();

            for (var i = 0; i < promediosOrdenados.Count; i++)
            {
                Console.WriteLine($"({i + 1}): {promediosOrdenados[i].comuna} - PSU: {promediosOrdenados[i].promedio}");
            }
        }
    }
}
