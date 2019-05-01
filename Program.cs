using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace laboratorio7
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // -- Lectura de datos --
            Console.WriteLine("Importando datos...");

            StreamReader reader = new StreamReader("data.csv");
            reader.ReadLine(); // La primera linea es el encabezado
            string row = reader.ReadLine();

            List<Fila> datos = new List<Fila>();
            
            while (row != null)
            {
                datos.Add(new Fila(row));
                row = reader.ReadLine();
            }
            reader.Close();

            Console.WriteLine($"Se han importado {datos.Count} datos");
        }
    }
}
