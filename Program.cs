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

            var estadisticas = new Dictionary<string, Estadistica>();
            estadisticas.Add("1", new PromedioPorRegion());
            estadisticas.Add("2", new PromedioPorTipoEstablecimiento());
            estadisticas.Add("3", new MejoresPsu());
            estadisticas.Add("4", new MejoresNem());

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine(" -- Menú --");
                Console.WriteLine("[0] Salir del programa");
                Console.WriteLine("[1] Consultar información de comuna");
                Console.WriteLine("[2] Estadísticas");
                Console.WriteLine("[3] Calcular Coeficiente de Pearson");
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

                        PromedioNemPsu promedio = new PromedioNemPsu();
                        promedio.MostrarEstadistica(filasDeComuna.ToList());
                    }
                    else
                    {
                        Console.WriteLine("No se encontró información para la comuna ingresada.");
                    }
                }
                else if (opcion == "2")
                {
                    while(true) 
                    {
                        // Imprimimos el menú de estadísticas
                        Console.WriteLine("\nIngresa la estadística que quieres:");

                        foreach (var estadistica in estadisticas)
                        {
                            Console.WriteLine($"[{estadistica.Key}] {estadistica.Value.GetNombre()}");
                        }
                            
                        Console.WriteLine("[Cualquier otra cosa] Volver al menú principal\n");

                        // Solicitamos una opción
                        string eleccionEstadistica = Console.ReadLine();

                        // Si existe, mostramos la estadistica
                        if (estadisticas.ContainsKey(eleccionEstadistica))
                        {
                            estadisticas[eleccionEstadistica].MostrarEstadistica(datos);
                        }
                        else
                        {
                            Console.WriteLine("Volviendo al menú principal...");
                            break;
                        }
                    }
                }
                else if (opcion == "3")
                {
                    // x = nem, y = psu

                    int n = datos.Count;

                    // Calculo del numerador
                    double sumXiYi = (from fila in datos select fila.nem * fila.psu).Sum();
                    double sumXi = (from fila in datos select fila.nem).Sum();
                    double sumYi = (from fila in datos select fila.psu).Sum();
                    double numerador = n * sumXiYi - sumXi * sumYi;

                    // Calculo del denominador
                    double sumXiXi = (from fila in datos select fila.nem * fila.nem).Sum();
                    double sumYiYi = (from fila in datos select fila.psu * fila.psu).Sum();
                    double denominador = Math.Sqrt(n * sumXiXi - sumXi * sumXi) * Math.Sqrt(n * sumYiYi - sumYi * sumYi);

                    double rxy = numerador / denominador;

                    Console.WriteLine($"El coeficiente de correlación de Pearson es: {rxy}");



                    if (rxy >= 0) {
                        Console.WriteLine("Existe una correlación positiva");
                    } else {
                        Console.WriteLine("Existe una correlación negativa");
                    }

                    // Aquí considero que si es mayor de 0.9, es correlación perfecta
                    if (Math.Abs(rxy) - 0.1 >= 0.9) {
                        Console.WriteLine("Que además es perfecta!");
                    } else if (Math.Abs(rxy) < 0.5) {
                        Console.WriteLine("El resultado NEM no explica tanto el resultado PSU");
                    } else if (Math.Abs(rxy) > 0.5)
                    {
                        Console.WriteLine("La tendencia es que a mejor NEM, mejor PSU");
                    }
                }
            }
        }
    }
}
