//
//
using System;
using System.Collections.Generic;
using System.Linq;

class Tarea_Semana10
{
    // Listas de nombres y apellidos comunes en Ecuador
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

    // Lista de tipos de vacuna disponibles
    static List<string> vacunas = new List<string> { "Pfizer", "AstraZeneca", "Sin Vacuna" };

    static void Main()
    {
        // Solicita al usuario la cantidad de personas a generar
        Console.WriteLine("¿Cuántas personas deseas generar?");
        int cantidad;
        
        // Verifica si la entrada es un número válido y mayor que 0
        if (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Por favor, ingresa un número válido y mayor que 0.");
            return;
        }

        // Genera la lista de personas y las muestra
        var personas = GenerarPersonas(cantidad);
        MostrarPersonas(personas);
    }

    // Función para generar una lista de personas con nombre, tipo de vacuna y dosis
    static List<Tuple<string, string, int>> GenerarPersonas(int cantidad)
    {
        var personas = new List<Tuple<string, string, int>>();
        Random rand = new Random();

        for (int i = 0; i < cantidad; i++)
        {
            // Selecciona aleatoriamente un nombre y un apellido
            string nombre = $"{nombresBase[rand.Next(nombresBase.Count)]} {apellidosBase[rand.Next(apellidosBase.Count)]}";
            
            // Selecciona aleatoriamente un tipo de vacuna
            string tipoVacuna = vacunas[rand.Next(vacunas.Count)];
            
            // Determina el número de dosis según la vacuna
            int dosis = tipoVacuna == "Sin Vacuna" ? 0 : 2;

            // Agrega la persona generada a la lista
            personas.Add(new Tuple<string, string, int>(nombre, tipoVacuna, dosis));
        }

        return personas;
    }

    // Función para mostrar la lista de personas generadas con formato
    static void MostrarPersonas(List<Tuple<string, string, int>> personas)
    {
        // Encabezado de la tabla
        Console.WriteLine("{0,-30} {1,-20} {2,-10}", "Nombre", "Tipo Vacuna", "No. Dosis");
        Console.WriteLine("--------------------------------------------------------------");

        // Contadores para cada tipo de vacuna
        int totalPfizer = 0;
        int totalAstraZeneca = 0;
        int totalSinVacuna = 0;

        foreach (var persona in personas)
        {
            // Determina el color de la línea según el tipo de vacuna
            string color = persona.Item2 switch
            {
                "Pfizer" => "Azul",
                "AstraZeneca" => "Amarillo",
                "Sin Vacuna" => "Gris",
                _ => "Sin color"
            };

            // Aplica el color correspondiente
            Console.ForegroundColor = color switch
            {
                "Azul" => ConsoleColor.Cyan,
                "Amarillo" => ConsoleColor.Yellow,
                "Gris" => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };

            // Imprime los datos de la persona
            Console.WriteLine("{0,-30} {1,-20} {2,-10}", persona.Item1, persona.Item2, persona.Item3);

            // Incrementa el contador de cada tipo de vacuna
            if (persona.Item2 == "Pfizer") totalPfizer++;
            else if (persona.Item2 == "AstraZeneca") totalAstraZeneca++;
            else if (persona.Item2 == "Sin Vacuna") totalSinVacuna++;
        }

        Console.ResetColor(); // Restablece el color de la consola

        // Muestra los totales por cada tipo de vacuna
        Console.WriteLine("--------------------------------------------------------------");
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Total vacunados Pfizer (2 dosis): {totalPfizer}");
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total vacunados AstraZeneca (2 dosis): {totalAstraZeneca}");
        
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Total Sin vacunar: {totalSinVacuna}");

        Console.ResetColor(); // Restablece el color al final
    }
}
