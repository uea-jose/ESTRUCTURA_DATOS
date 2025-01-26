+

    def calcular_area(self):
        # calcular_area es una función que devuelve el área del círculo
        # El área de un círculo se calcula con la fórmula: A = π * radio^2
        return math.pi * (self.radio ** 2)

    def calcular_perimetro(self):
        # calcular_perimetro es una función que devuelve el perímetro (circunferencia) del círculo
        # El perímetro de un círculo se calcula con la fórmula: P = 2 * π * radio
        return 2 * math.pi * self.radio

# Clase Rectángulo
class Rectangulo:
    def __init__(self, largo, ancho):
        # largo y ancho son las dimensiones del rectángulo
        self.largo = largo
        self.ancho = ancho

    def calcular_area(self):
        # calcular_area es una función que devuelve el área del rectángulo
        # El área de un rectángulo se calcula con la fórmula: A = largo * ancho
        return self.largo * self.ancho

    def calcular_perimetro(self):
        # calcular_perimetro es una función que devuelve el perímetro del rectángulo
        # El perímetro de un rectángulo se calcula con la fórmula: P = 2 * (largo + ancho)
        return 2 * (self.largo + self.ancho)

# Función principal para ejecutar el programa
def main():
    # Pedir al usuario el valor del radio para el círculo
    radio = float(input("Ingresa el radio del círculo: "))
    # Crear un objeto de la clase Circulo
    circulo = Circulo(radio)

    # Mostrar el área y perímetro del círculo
    print("\nCírculo:")
    print(f"Área: {circulo.calcular_area():.2f} unidades cuadradas")
    print(f"Perímetro: {circulo.calcular_perimetro():.2f} unidades")

    # Pedir al usuario las dimensiones del rectángulo
    largo = float(input("\nIngresa el largo del rectángulo: "))
    ancho = float(input("Ingresa el ancho del rectángulo: "))
    # Crear un objeto de la clase Rectangulo
    rectangulo = Rectangulo(largo, ancho)

    # Mostrar el área y perímetro del rectángulo
    print("\nRectángulo:")
    print(f"Área: {rectangulo.calcular_area()} unidades cuadradas")
    print(f"Perímetro: {rectangulo.calcular_perimetro()} unidades")

# Ejecutar la función principal
if __name__ == "__main__":
    main()
