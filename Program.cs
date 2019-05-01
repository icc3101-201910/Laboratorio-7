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

            // -- Menú --

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine(" -- Menú --");
                Console.WriteLine("[0] Salir del programa");
                Console.WriteLine("[1] Consultar información de comuna");
                Console.WriteLine("");

                string opcion = Console.ReadLine();

                if (opcion == "0")
                {
                    Console.WriteLine("Cerrando programa...");
                    break;
                }

                if (opcion == "1")
                {
                    Console.WriteLine("Ingresa el ID de la comuna:");

                    int comunaID;

                    while (true)
                    {
                        try
                        {
                            comunaID = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ingresó un valor no válido. Vuelve a ingresar el ID de la comuna:");
                        }
                    }

                    Console.WriteLine($"Buscando información de la comuna con ID = {comunaID} ...");

                    var filasDeComuna = (
                        from fila in datos
                        where fila.comunaID == comunaID
                        select fila
                    );

                    if (filasDeComuna.Any())
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"=== Información de {filasDeComuna.First().comuna} ===");

                        foreach (Fila fila in filasDeComuna) Console.WriteLine(fila);

                        double datosTotales = filasDeComuna.Select(fila => fila.numeroDatos).Sum();
                        double sumaPsu = filasDeComuna.Select(fila => fila.psu * fila.numeroDatos).Sum();
                        double sumaNem = filasDeComuna.Select(fila => fila.nem * fila.numeroDatos).Sum();

                        Console.WriteLine($"Promedio PSU: {sumaPsu / datosTotales}");
                        Console.WriteLine($"Promedio NEM: {sumaNem / datosTotales}");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró información para la comuna ingresada.");
                    }
                }
            }
        }
    }
}
