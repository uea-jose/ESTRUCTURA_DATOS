#Ejercicio 4
#Escribir un programa que pregunte al usuario los números ganadores de la lotería primitiva, los almacene en una lista y los muestre por pantalla ordenados de menor a mayor.
# Clase para gestionar los números de la lotería
# Clase para gestionar los números de la lotería
class Loteria:
    def __init__(self):
        # Lista para almacenar los números ganadores
        self.numeros_ganadores = []

    # Método para agregar números ganadores
    def agregar_numeros(self):
        while True:
            try:
                # Solicitar al usuario los números ganadores, separados por espacio
                numeros = input("Introduce los números ganadores separados por espacio: ").split()

                # Convertir los números a enteros
                self.numeros_ganadores = [int(num) for num in numeros]

                # Verificar que los números estén en el rango permitido (por ejemplo, entre 1 y 100)
                if all(1 <= num <= 100 for num in self.numeros_ganadores):
                    break
                else:
                    print("Los números deben estar entre 1 y 100. Intenta de nuevo.")
            except ValueError:
                print("Por favor, ingresa solo números válidos.")

    # Método para mostrar los números ganadores ordenados
    def mostrar_numeros_ordenados(self):
        # Ordenar los números de menor a mayor
        self.numeros_ganadores.sort()

        # Usamos un bucle for para mostrar los números ordenados
        print("Números ganadores ordenados:")
        for i, num in enumerate(self.numeros_ganadores):
            if i == len(self.numeros_ganadores) - 1:  # Para evitar la coma al final
                print(num)
            else:
                print(num, end=", ")


# Función principal
def main():
    # Crear el objeto Loteria
    loteria = Loteria()

    # Agregar los números ganadores
    loteria.agregar_numeros()

    # Mostrar los números ordenados
    loteria.mostrar_numeros_ordenados()


# Llamada a la función principal
if __name__ == "__main__":
    main()
