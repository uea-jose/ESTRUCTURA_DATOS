//
//
using System;
using System.Collections.Generic;
using System.Linq;

class Tarea_Semana10
{
    static List<string> nombresBase = new List<string>
    {
        "Sofía", "Valentina", "María José", "Camila", "Lucía", "Renata", "Valeria", "Josefa", "Ana", "Mía",
        "Gabriela", "María Fernanda", "Violeta", "Elena", "Isabella", "Jimena", "Martina", "Ariana", "Sara",
        "Emilia", "Carla", "Julieta", "Jose", "Nicolas", "Antonia", "Catalina", "Camila", "JOel", "Samantha",
        "Rodrigo", "Juan Sebastián", "Santiago", "Tomás", "Joaquín", "José Antonio", "Luis Felipe", "Sebastián",
        "Francisco", "Sergio", "MIkaela", "Alejandro", "Andrés", "Carlos Andrés", "Felipe", "David", "Miguel",
        "Diego", "Raúl", "Eduardo", "Luis Eduardo", "Estefania", "Fernando", "Ricardo", "Anahi", "Carlos Javier"
    };

    static List<string> apellidosBase = new List<string>
    {
        "Romero", "Martínez", "Pérez", "Rodríguez", "Jiménez", "Díaz", "Ramírez", "Torres", "Reyes", "García",
        "Rojas", "Sánchez", "Morales", "Mendoza", "Avila", "Chávez", "López", "Castro", "Fernández", "Cuenca",
        "Gutiérrez", "Luna", "Vásquez", "Paredes", "Serrano", "Ruiz", "González", "Torres", "Salazar", "Rivera",
        "Fuentes", "Vega", "San Martin", "Alvarado", "Mora", "Zambrano", "Puga", "Valarezo", "Sotomayor", "Armijos"
    };

    static void Main()
    {
        Console.Write("¿Cuántas personas deseas generar? (Por defecto: 500): ");
        string entrada = Console.ReadLine();
        int cantidad = string.IsNullOrWhiteSpace(entrada) ? 500 : int.TryParse(entrada, out int num) && num > 0 ? num : 500;

        var personas = GenerarPersonas(cantidad);
        MostrarPersonas(personas);
    }

    static List<Tuple<string, string, int>> GenerarPersonas(int cantidad)
    {
        var personas = new List<Tuple<string, string, int>>();
        Random rand = new Random();

        // Crear listas separadas
        var listaPfizer = new List<Tuple<string, string, int>>();
        var listaAstraZeneca = new List<Tuple<string, string, int>>();
        var listaSinVacuna = new List<Tuple<string, string, int>>();

        // Agregar 75 con Pfizer
        for (int i = 0; i < 75; i++)
        {
            string nombre = $"{nombresBase[rand.Next(nombresBase.Count)]} {apellidosBase[rand.Next(apellidosBase.Count)]}";
            listaPfizer.Add(new Tuple<string, string, int>(nombre, "Pfizer", 2));
        }

        // Agregar 75 con AstraZeneca
        for (int i = 0; i < 75; i++)
        {
            string nombre = $"{nombresBase[rand.Next(nombresBase.Count)]} {apellidosBase[rand.Next(apellidosBase.Count)]}";
            listaAstraZeneca.Add(new Tuple<string, string, int>(nombre, "AstraZeneca", 2));
        }

        // Agregar el resto sin vacunar
        for (int i = 150; i < cantidad; i++)
        {
            string nombre = $"{nombresBase[rand.Next(nombresBase.Count)]} {apellidosBase[rand.Next(apellidosBase.Count)]}";
            listaSinVacuna.Add(new Tuple<string, string, int>(nombre, "Sin Vacuna", 0));
        }

        // Mezclar las listas
        personas.AddRange(listaPfizer);
        personas.AddRange(listaAstraZeneca);
        personas.AddRange(listaSinVacuna);
        personas = personas.OrderBy(x => rand.Next()).ToList();

        return personas;
    }

    static void MostrarPersonas(List<Tuple<string, string, int>> personas)
    {
        Console.WriteLine("{0,-30} {1,-20} {2,-10}", "Nombre", "Tipo Vacuna", "No. Dosis");
        Console.WriteLine("--------------------------------------------------------------");

        // Contadores
        int totalPfizer = 0;
        int totalAstraZeneca = 0;
        int totalSinVacuna = 0;

        foreach (var persona in personas)
        {
            string color = persona.Item2 switch
            {
                "Pfizer" => "Azul",
                "AstraZeneca" => "Amarillo",
                "Sin Vacuna" => "Gris",
                _ => "Sin color"
            };

            Console.ForegroundColor = color switch
            {
                "Azul" => ConsoleColor.Cyan,
                "Amarillo" => ConsoleColor.Yellow,
                "Gris" => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };

            Console.WriteLine("{0,-30} {1,-20} {2,-10}", persona.Item1, persona.Item2, persona.Item3);

            // Contadores para los totales
            if (persona.Item2 == "Pfizer") totalPfizer++;
            else if (persona.Item2 == "AstraZeneca") totalAstraZeneca++;
            else if (persona.Item2 == "Sin Vacuna") totalSinVacuna++;
        }

        Console.ResetColor();

        // Mostrar totales
        Console.WriteLine("--------------------------------------------------------------");
        
        // Pfizer en color Cyan
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Total vacunados Pfizer (2 dosis): {totalPfizer}");
        
        // AstraZeneca en color Amarillo
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total vacunados AstraZeneca (2 dosis): {totalAstraZeneca}");
        
        // Sin Vacuna en color Gris
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Total Sin vacunar: {totalSinVacuna}");

        Console.ResetColor();
    }
}
