#Ejercicio 2
#Escribir un programa que almacene las asignaturas de un curso en una lista y la muestre con el mensaje Yo estudio <asignatura>, donde <asignatura> es cada una de las asignaturas de la lista.
# Clase para gestionar las asignaturas de un curso

# Clase para gestionar las asignaturas de un curso
# Clase para gestionar las asignaturas de un curso
# Clase para gestionar las asignaturas de un curso
# Clase para gestionar las asignaturas de un curso
# Clase para gestionar las asignaturas de un curso
class GestorDeAsignaturas:
    def __init__(self):
        # Lista para almacenar las asignaturas
        self.asignaturas = []

    # Función para agregar una asignatura
    def agregar_asignatura(self, asignatura):
        if asignatura:
            self.asignaturas.append(asignatura)
        else:
            print("La asignatura no puede estar vacía.")

    # Función para mostrar las asignaturas con el mensaje "Yo estudio <asignatura>"
    def mostrar_asignaturas(self):
        if not self.asignaturas:
            print("No hay asignaturas para mostrar.")
        else:
            for asignatura in self.asignaturas:
                print(f"Yo estudio {asignatura}")


# Función principal
def main():
    gestor_asignaturas = GestorDeAsignaturas()

    # Ingreso de asignaturas por teclado
    while True:
        asignatura = input("Introduce una asignatura \nMatematicas\n"
                           "Fisica\n"
                           "Quimica\n"
                           "Lenggua\n ('0' para salir): ")
        if asignatura.lower() == '0':
            break
        else:
            gestor_asignaturas.agregar_asignatura(asignatura)

    # Mostrar las asignaturas
    gestor_asignaturas.mostrar_asignaturas()


# Llamada a la función principal
if __name__ == "__main__":
    main()
