import time

class TorreHanoi:
    def __init__(self, num_discos):
        self.num_discos = num_discos
        self.torres = [['#' * i for i in range(num_discos, 0, -1)], [], []]
        self.pasos = []

    def imprimir_torres(self):
        """Imprime el estado actual de las torres de Hanoi con caracteres visuales."""
        max_disco = self.num_discos
        for i in range(max_disco, 0, -1):
            for torre in self.torres:
                if len(torre) >= i:
                    print(f"{torre[i-1]:<6}", end="   ")
                else:
                    print("      ", end="   ")
            print()
        print("\nA        B        C\n")

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

    def jugar(self, pausar=True):
        """Muestra los pasos de la solución de Torre de Hanoi uno por uno."""
        self.imprimir_torres()
        self.resolver(self.num_discos, 0, 2, 1)

        for paso in self.pasos:
            if pausar:
                input("Presiona Enter para ver el siguiente paso...")
            self.torres = paso
            self.imprimir_torres()
            time.sleep(0.5)  # Añadir un pequeño retraso entre pasos si quieres automatizar

# Ejecutar el juego con 3 discos
if __name__ == "__main__":
    juego = TorreHanoi(3)
    juego.jugar(pausar=False)  # Puedes cambiar a `True` si prefieres la interacción paso a paso
