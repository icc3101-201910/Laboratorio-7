using System.Collections.Generic;

namespace laboratorio7
{
    public abstract class Estadistica
    {
        private string nombre;

        protected Estadistica(string nombre)
        {
            this.nombre = nombre;
        }

        public abstract void MostrarEstadistica(List<Fila> filas);

        public string GetNombre() {
            return nombre;
        }
    }
}
