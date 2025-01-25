import time

# Clase que representa el juego de la Torre de Hanoi
class TorreHanoi:
    def __init__(self, num_discos):
        # Inicializa las torres y el número de discos
        self.num_discos = num_discos
        # Los discos se representan con '#' y el más grande tiene más caracteres
        self.torres = [['#' * i for i in range(num_discos, 0, -1)], [], []]
        self.pasos = []

    def imprimir_torres(self):
        """Imprime el estado actual de las torres de Hanoi con caracteres visuales."""
        max_disco = self.num_discos
        # Muestra los discos de las tres torres
        for i in range(max_disco, 0, -1):
            for torre in self.torres:
                if len(torre) >= i:
                    # Discos visibles representados por '#' o '/' según el tamaño
                    print(f"{torre[i-1]:<6}", end="   ")
                else:
                    print("      ", end="   ")  # Espacio adicional para las torres vacías
            print()
        print("\nA        B        C\n")  # Alinea las torres con más espacio entre ellas

    def resolver(self, n, origen, destino, auxiliar):
        """Resuelve el problema de la Torre de Hanoi de forma recursiva."""
        if n == 1:
            self.mover_disco(origen, destino)
        else:
            self.resolver(n-1, origen, auxiliar, destino)
            self.mover_disco(origen, destino)
            self.resolver(n-1, auxiliar, destino, origen)

    def mover_disco(self, origen, destino):
        """Mueve un disco de una torre a otra y registra el paso."""
        disco = self.torres[origen].pop()
        self.torres[destino].append(disco)
        self.pasos.append([list(self.torres[0]), list(self.torres[1]), list(self.torres[2])])

    def jugar(self):
        """Muestra los pasos de la solución de Torre de Hanoi uno por uno."""
        # Mostrar el estado inicial
        self.imprimir_torres()
        # Resolver el juego
        self.resolver(self.num_discos, 0, 2, 1)

        # Mostrar los pasos#
        for paso in self.pasos:
            input("Presiona Enter para ver el siguiente paso...")
            self.torres = paso  # Actualizar el estado de las torres en cada paso
            self.imprimir_torres()

# Ejecutar el juego con 3 discos
if __name__ == "__main__":
    juego = TorreHanoi(3)
    juego.jugar()
