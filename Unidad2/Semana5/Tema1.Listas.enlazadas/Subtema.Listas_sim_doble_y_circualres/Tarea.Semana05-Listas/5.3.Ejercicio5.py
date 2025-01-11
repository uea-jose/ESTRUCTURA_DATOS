#Ejercicio 5
#Escribir un programa que almacene en una lista los números del 1 al 10 y los muestre por pantalla en orden inverso separados por comas.
# Clase para manejar los números
# Clase para manejar los números
class GestorDeNumeros:
    def __init__(self, inicio, fin):
        # Crear una lista de números desde inicio hasta fin
        self.numeros = list(range(inicio, fin + 1))

    # Método para mostrar los números en orden normal
    def mostrar_normal(self):
        print("Orden normal:")
        for numero in self.numeros:
            print(numero, end=", " if numero != self.numeros[-1] else "")
        print()  # Para saltar a la siguiente línea

    # Método para mostrar los números en orden inverso
    def mostrar_invertidos(self):
        print("Orden inverso:")
        for numero in reversed(self.numeros):
            print(numero, end=", " if numero != self.numeros[0] else "")
        print()  # Para saltar a la siguiente línea


# Función principal
def main():
    # Crear el objeto gestor con números del 1 al 10
    gestor = GestorDeNumeros(1, 10)

    # Mostrar los números en orden normal
    gestor.mostrar_normal()

    # Mostrar los números en orden inverso
    gestor.mostrar_invertidos()


# Llamada a la función principal
if __name__ == "__main__":
    main()
