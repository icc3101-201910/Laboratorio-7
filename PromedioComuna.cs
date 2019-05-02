using System;
namespace laboratorio7
{
    // Esta clase se utiliza sólo para poder hacer selecciones inteligentes en las clases "MejoresPsu" y "MejoresNem"
    public class PromedioComuna
    {
        public double promedio;
        public string comuna;

        public PromedioComuna(string comuna, double promedio)
        {
            this.promedio = promedio;
            this.comuna = comuna;
        }
    }
}
