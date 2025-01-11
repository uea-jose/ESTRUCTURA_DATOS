#Ejercicio 10
#Escribir un programa que almacene en una lista los siguientes precios, 50, 75, 46, 22, 80, 65, 8, y muestre por pantalla el menor y el mayor de los precios.
# Clase para gestionar los precios
# Clase para gestionar los precios
class GestorDePrecios:
    def __init__(self):
        # Lista para almacenar los precios
        self.precios = [50, 75, 46, 22, 80, 65, 8]

    # Método para obtener el precio más bajo
    def obtener_precio_minimo(self):
        return min(self.precios)

    # Método para obtener el precio más alto
    def obtener_precio_maximo(self):
        return max(self.precios)

    # Método para mostrar los precios, la lista original, y los precios más bajo y más alto
    def mostrar_precios(self):
        # Mostrar la lista original
        print(f"Lista original de precios: {self.precios}")

        # Obtener el precio más bajo y el más alto
        precio_minimo = self.obtener_precio_minimo()
        precio_maximo = self.obtener_precio_maximo()

        # Mostrar los resultados
        print(f"El precio más bajo es: {precio_minimo}")
        print(f"El precio más alto es: {precio_maximo}")
#

# Función principal
def main():
    # Crear el objeto GestorDePrecios
    gestor_precios = GestorDePrecios()

    # Mostrar la lista original de precios y los precios más bajo y más alto
    gestor_precios.mostrar_precios()#


# Llamada a la función principal
if __name__ == "__main__":
    main()
