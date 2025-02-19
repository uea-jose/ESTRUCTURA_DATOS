//TAREA SEMAN10
using System;
using System.Collections.Generic;
using System.Linq;

class Tarea_Semana10
{
    // Conjunto definido con HashSet para almacenar nombres únicos
    static HashSet<string> nombresBase = new HashSet<string>
    {
        "Sofía", "Valentina", "María José", "Camila", "Lucía", "Renata", "Valeria", "Josefa", "Ana", "Mía",
        "Gabriela", "María Fernanda", "Violeta", "Elena", "Isabella", "Jimena", "Martina", "Ariana", "Sara",
        "Emilia", "Carla", "Julieta", "Jose", "Nicolas", "Antonia", "Catalina", "Camila", "Joel", "Samantha",
        "Rodrigo", "Juan Sebastián", "Santiago", "Tomás", "Joaquín", "José Antonio", "Luis Felipe", "Sebastián",
        "Francisco", "Sergio", "Mikaela", "Alejandro", "Andrés", "Carlos Andrés", "Felipe", "David", "Miguel",
        "Diego", "Raúl", "Eduardo", "Luis Eduardo", "Estefania", "Fernando", "Ricardo", "Anahi", "Carlos Javier"
    };

    // Conjunto definido con HashSet para almacenar apellidos únicos
    static HashSet<string> apellidosBase = new HashSet<string>
    {
        "Romero", "Martínez", "Pérez", "Rodríguez", "Jiménez", "Díaz", "Ramírez", "Torres", "Reyes", "García",
        "Rojas", "Sánchez", "Morales", "Mendoza", "Avila", "Chávez", "López", "Castro", "Fernández", "Cuenca",
        "Gutiérrez", "Luna", "Vásquez", "Paredes", "Serrano", "Ruiz", "González", "Torres", "Salazar", "Rivera",
        "Fuentes", "Vega", "San Martin", "Alvarado", "Mora", "Zambrano", "Puga", "Valarezo", "Sotomayor", "Armijos"
    };

    static void Main()
    {
        // Definir la cantidad de ciudadanos a generar
        int cantidad = 500;
        
        // Generar una lista de personas con nombres aleatorios
        var personas = GenerarPersonas(cantidad);
        
        // Crear los conjuntos de vacunados con diferentes vacunas
        var vacunadosPfizer = personas.Take(75).ToList(); // Primeros 75 vacunados con Pfizer
        var vacunadosAstraZeneca = personas.Skip(75).Take(75).ToList(); // Siguientes 75 vacunados con AstraZeneca
        var noVacunados = personas.Skip(150).ToList(); // El resto no están vacunados

        // Operaciones de conjunto para obtener los diferentes listados
        var vacunadosDosDosis = vacunadosPfizer.Concat(vacunadosAstraZeneca).ToHashSet(); // Personas con dos dosis
        var soloPfizer = vacunadosPfizer.Except(vacunadosAstraZeneca).ToHashSet(); // Personas vacunadas solo con Pfizer
        var soloAstraZeneca = vacunadosAstraZeneca.Except(vacunadosPfizer).ToHashSet(); // Personas vacunadas solo con AstraZeneca
        
        // Mostrar los resultados de los diferentes grupos de vacunados con LINQ
        MostrarListados("Ciudadanos NO vacunados", noVacunados.ToList()); // Convertir el HashSet a List para mostrar
        MostrarListados("Ciudadanos con dos dosis", vacunadosDosDosis.ToList()); // Convertir el HashSet a List para mostrar
        MostrarListados("Ciudadanos vacunados solo con Pfizer", soloPfizer.ToList()); // Convertir el HashSet a List para mostrar
        MostrarListados("Ciudadanos vacunados solo con AstraZeneca", soloAstraZeneca.ToList()); // Convertir el HashSet a List para mostrar

        // Aplicar LINQ para obtener más información sobre los ciudadanos vacunados con Pfizer o AstraZeneca
        var ciudadanosConPfizer = personas.Where(p => p.Vacuna == "Pfizer").ToList();
        var ciudadanosConAstraZeneca = personas.Where(p => p.Vacuna == "AstraZeneca").ToList();
        Console.WriteLine($"\nTotal de ciudadanos vacunados con Pfizer: {ciudadanosConPfizer.Count}");
        Console.WriteLine($"Total de ciudadanos vacunados con AstraZeneca: {ciudadanosConAstraZeneca.Count}");
    }

    // Función para generar personas con nombres aleatorios y asignar vacuna y dosis
    static List<(string Nombre, string Vacuna, int Dosis)> GenerarPersonas(int cantidad)
    {
        Random rand = new Random();
        
        // Generar nombres completos combinando nombres y apellidos aleatorios
        var nombresCompletos = nombresBase.SelectMany(nombre => apellidosBase,
            (nombre, apellido) => $"{nombre} {apellido}").OrderBy(_ => rand.Next()).Take(cantidad).ToList();

        // Asignar vacuna y dosis dependiendo de la persona
        var personas = nombresCompletos.Select((nombre, index) =>
        {
            string vacuna = index < 75 ? "Pfizer" : index < 150 ? "AstraZeneca" : "Sin Vacuna";
            int dosis = vacuna == "Sin Vacuna" ? 0 : 2; // 0 dosis si no está vacunado, 2 dosis si está vacunado
            return (nombre, vacuna, dosis);
        }).ToList();

        return personas;
    }

    // Función para mostrar los grupos generados con colores específicos por tipo de vacuna
    static void MostrarListados(string titulo, List<(string Nombre, string Vacuna, int Dosis)> grupo)
    {
        Console.ForegroundColor = ConsoleColor.Yellow; // Color de título en amarillo
        Console.WriteLine($"\n=== {titulo} ({grupo.Count}) ===\n");
        Console.ForegroundColor = ConsoleColor.Cyan; // Color de las cabeceras en cyan
        Console.WriteLine("{0,-30} {1,-20} {2,-10}", "Nombre", "Tipo Vacuna", "No. Dosis");
        Console.WriteLine(new string('-', 60));

        // Mostrar los datos con colores según el tipo de vacuna
        foreach (var persona in grupo)
        {
            // Cambiar color según la vacuna
            if (persona.Vacuna == "Pfizer")
            {
                Console.ForegroundColor = ConsoleColor.Blue; // Pfizer en azul
            }
            else if (persona.Vacuna == "AstraZeneca")
            {
                Console.ForegroundColor = ConsoleColor.Green; // AstraZeneca en verde
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray; // Sin Vacuna en gris
            }

            // Mostrar los datos de cada persona con los colores aplicados
            Console.WriteLine("{0,-30} {1,-20} {2,-10}", persona.Nombre, persona.Vacuna, persona.Dosis);
        }

        // Resetear el color de la consola para evitar que afecte otras salidas
        Console.ResetColor();
    }
}
