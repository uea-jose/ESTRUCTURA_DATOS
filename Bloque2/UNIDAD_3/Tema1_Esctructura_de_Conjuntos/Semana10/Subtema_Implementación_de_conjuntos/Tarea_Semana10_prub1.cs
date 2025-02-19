//Tarea SEmana10
using System;
using System.Collections.Generic;
using System.Linq;

// Clase principal que ejecuta el programa
class Tarea_Semana10
{
    // Conjunto de nombres únicos para evitar duplicados
    // En esta línea se está aplicando la teoría de conjuntos.
    // Un conjunto (HashSet) garantiza que no haya nombres repetidos, es decir, cada elemento es único.
    static HashSet<string> nombresBase = new HashSet<string>
    {
        "Sofía", "Valentina", "María José", "Camila", "Lucía", "Renata", "Valeria", "Josefa", "Ana", "Mía",
        "Gabriela", "María Fernanda", "Violeta", "Elena", "Isabella", "Jimena", "Martina", "Ariana", "Sara",
        "Emilia", "Carla", "Julieta", "Jose", "Nicolas", "Antonia", "Catalina", "Camila", "JOel", "Samantha",
        "Rodrigo", "Juan Sebastián", "Santiago", "Tomás", "Joaquín", "José Antonio", "Luis Felipe", "Sebastián",
        "Francisco", "Sergio", "MIkaela", "Alejandro", "Andrés", "Carlos Andrés", "Felipe", "David", "Miguel",
        "Diego", "Raúl", "Eduardo", "Luis Eduardo", "Estefania", "Fernando", "Ricardo", "Anahi", "Carlos Javier"
    };

    // Conjunto de apellidos únicos para evitar duplicados
    // Similar al conjunto de nombres, el uso de un HashSet garantiza que los apellidos sean únicos.
    static HashSet<string> apellidosBase = new HashSet<string>
    {
        "Romero", "Martínez", "Pérez", "Rodríguez", "Jiménez", "Díaz", "Ramírez", "Torres", "Reyes", "García",
        "Rojas", "Sánchez", "Morales", "Mendoza", "Avila", "Chávez", "López", "Castro", "Fernández", "Cuenca",
        "Gutiérrez", "Luna", "Vásquez", "Paredes", "Serrano", "Ruiz", "González", "Torres", "Salazar", "Rivera",
        "Fuentes", "Vega", "San Martin", "Alvarado", "Mora", "Zambrano", "Puga", "Valarezo", "Sotomayor", "Armijos"
    };

    // Método principal que inicia el programa
    static void Main()
    {
        // Solicita al usuario cuántas personas generar, con un valor por defecto de 500
        Console.Write("¿Cuántas personas deseas generar? (Por defecto: 500): ");
        string entrada = Console.ReadLine();

        // Establece la cantidad de personas a generar, si no se introduce un valor válido, se usa 500
        int cantidad = string.IsNullOrWhiteSpace(entrada) ? 500 : int.TryParse(entrada, out int num) && num > 0 ? num : 500;

        // Generar y mostrar las personas
        var personas = GenerarPersonas(cantidad);
        MostrarPersonas(personas);
    }

    // Método que genera una lista de personas con nombres, tipo de vacuna y dosis
    static List<(string Nombre, string Vacuna, int Dosis)> GenerarPersonas(int cantidad)
    {
        Random rand = new Random();

        // Generar nombres aleatorios únicos combinando nombres y apellidos
        // Aquí utilizas un conjunto de nombres y apellidos (HashSet) y los combinas de manera aleatoria.
        // El uso de HashSet asegura que no haya duplicados, lo cual es un comportamiento propio de los conjuntos.
        var nombresCompletos = nombresBase.SelectMany(nombre => apellidosBase,
            (nombre, apellido) => $"{nombre} {apellido}").OrderBy(_ => rand.Next()).Take(cantidad).ToList();

        // Asignar vacunas usando LINQ y mezclar el resultado
        var personas = nombresCompletos.Select((nombre, index) =>
        {
            string vacuna = index < 75 ? "Pfizer" : index < 150 ? "AstraZeneca" : "Sin Vacuna";
            int dosis = vacuna == "Sin Vacuna" ? 0 : 2;
            return (nombre, vacuna, dosis);
        }).OrderBy(_ => rand.Next()).ToList(); // Mezclar aleatoriamente

        return personas;
    }

    // Método para mostrar las personas generadas en formato tabular en consola
    static void MostrarPersonas(List<(string Nombre, string Vacuna, int Dosis)> personas)
    {
        // Imprimir encabezados de la tabla
        Console.WriteLine("{0,-30} {1,-20} {2,-10}", "Nombre", "Tipo Vacuna", "No. Dosis");
        Console.WriteLine("--------------------------------------------------------------");

        // Iterar sobre cada persona y mostrar sus datos con colores según el tipo de vacuna
        personas.ForEach(persona =>
        {
            // Cambiar el color de la consola según el tipo de vacuna
            Console.ForegroundColor = persona.Vacuna switch
            {
                "Pfizer" => ConsoleColor.Cyan,      // Color cian para Pfizer
                "AstraZeneca" => ConsoleColor.Yellow, // Color amarillo para AstraZeneca
                "Sin Vacuna" => ConsoleColor.Gray,  // Color gris para Sin Vacuna
                _ => ConsoleColor.White
            };
            // Mostrar la información de la persona
            Console.WriteLine("{0,-30} {1,-20} {2,-10}", persona.Nombre, persona.Vacuna, persona.Dosis);
        });

        // Restablecer el color de la consola a su valor predeterminado
        Console.ResetColor(); 

        // Mostrar totales de cada tipo de vacuna
        Console.WriteLine("--------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Total vacunados Pfizer (2 dosis): {personas.Count(p => p.Vacuna == "Pfizer")}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total vacunados AstraZeneca (2 dosis): {personas.Count(p => p.Vacuna == "AstraZeneca")}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Total Sin vacunar: {personas.Count(p => p.Vacuna == "Sin Vacuna")}");
        Console.ResetColor(); // Restablecer color de la consola
    }
}
