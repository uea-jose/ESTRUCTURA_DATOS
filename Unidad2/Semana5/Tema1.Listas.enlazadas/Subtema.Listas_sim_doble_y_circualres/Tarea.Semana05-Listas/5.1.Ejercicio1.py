#Ejercicio 1
#Escribir un programa que almacene las asignaturas de un curso (por ejemplo Matemáticas, Física, Química, Historia y Lengua) en una lista y la muestre por pantalla.
# Clase para gestionar las asignaturas de un curso
class GestorDeAsignaturas:
    def __init__(self):
        # Lista para almacenar las asignaturas
        self.asignaturas = []

    # Función para agregar una asignatura
    def agregar_asignatura(self, asignatura):
        if asignatura and asignatura not in self.asignaturas:
            self.asignaturas.append(asignatura)
        else:
            print("La asignatura ya existe o está vacía.")

    # Función para mostrar las asignaturas
    def mostrar_asignaturas(self):
        if not self.asignaturas:
            print("No hay asignaturas para mostrar.")
        else:
            print("Asignaturas del curso:")
            for asignatura in self.asignaturas:
                print(f"- {asignatura}")

# Crear una instancia de la clase GestorDeAsignaturas
gestor_asignaturas = GestorDeAsignaturas()

# Agregar# asignaturas
gestor_asignaturas.agregar_asignatura("Matemáticas")
gestor_asignaturas.agregar_asignatura("Física")
gestor_asignaturas.agregar_asignatura("Química")
gestor_asignaturas.agregar_asignatura("Historia")
gestor_asignaturas.agregar_asignatura("Lengua")

# Mostrar las asignaturas
gestor_asignaturas.mostrar_asignaturas()
