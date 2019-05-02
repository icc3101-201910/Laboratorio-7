using System;
using System.Collections.Generic;
using System.Linq;

namespace laboratorio7
{
    public class PromedioNemPsu : Estadistica
    {
        public PromedioNemPsu() : base("Promedio PSU y NEM de un conjunto arbitrario de datos")
        {

        }

        public double CalcularPromedioPsu(List<Fila> filas)
        {
            double totalNumeroDatos = filas.Select(fila => fila.numeroDatos).Sum();
            double sumaPsu = filas.Select(fila => fila.psu * fila.numeroDatos).Sum();
            return sumaPsu / totalNumeroDatos;
        }

        public double CalcularPromedioNem(List<Fila> filas)
        {
            double totalNumeroDatos = filas.Select(fila => fila.numeroDatos).Sum();
            double sumaNem = filas.Select(fila => fila.nem * fila.numeroDatos).Sum();
            return sumaNem / totalNumeroDatos;
        }

        public override void MostrarEstadistica(List<Fila> filas)
        {
            Console.WriteLine($"Promedio PSU: {CalcularPromedioPsu(filas)}");
            Console.WriteLine($"Promedio NEM: {CalcularPromedioNem(filas)}");
        }
    }
}
