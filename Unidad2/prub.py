import time

class TorreHanoi:
    def __init__(self, num_discos):
        self.num_discos = num_discos
        # Los discos están representados con números para facilidad visual.
        self.torres = [[i for i in range(num_discos, 0, -1)], [], []]  # Torre A tiene los discos ordenados

    def imprimir_torres(self):
        """Imprime el estado actual de las torres de Hanoi con los discos numerados."""
        max_disco = self.num_discos
        # Para cada nivel de disco (de mayor a menor)
        for i in range(max_disco, 0, -1):
            for torre in self.torres:
                if len(torre) >= i:
                    # Muestra el disco con su número entre paréntesis
                    print(f"{' #' * torre[i-1]} ({torre[i-1]:<2})", end="    ")
                else:
                    print("      ", end="    ")  # Espacio para las torres vacías
            print()
        print("\n   A              B              C\n")  # Espacio extra entre las torres

    def es_valido(self, origen, destino):
        """Verifica si el movimiento es válido. No puede poner un disco más grande sobre uno más pequeño."""
        if not self.torres[origen]:
            return False  # La torre de origen está vacía
        if not self.torres[destino]:
            return True  # La torre de destino está vacía, por lo que el movimiento es válido
        # Si el disco de origen es mayor que el disco de destino, no es válido
        if self.torres[origen][-1] > self.torres[destino][-1]:
            return False
        return True

    def mover_disco(self, origen, destino):
        """Mueve un disco de una torre a otra si el movimiento es válido."""
        if self.es_valido(origen, destino):
            disco = self.torres[origen].pop()
            self.torres[destino].append(disco)
            return True
        return False

    def jugar(self):
        """Ejecuta el juego, permitiendo al usuario mover los discos hasta completar el juego."""
        while not self.juego_completo():
            self.imprimir_torres()

            try:
                # Pedir al jugador que elija el disco
                disco = int(input(f"Escoge un número de disco (1-{self.num_discos}): "))
                if disco < 1 or disco > self.num_discos:
                    print("Número de disco inválido, intenta de nuevo.")
                    continue
            except ValueError:
                print("Entrada no válida, intenta de nuevo.")
                continue

            # Verificar en qué torre está el disco seleccionado
            encontrado = False
            for i, torre in enumerate(self.torres):
                if disco in torre:
                    origen = ["A", "B", "C"][i]
                    encontrado = True
                    break
            if not encontrado:
                print(f"El disco #{disco} no está en ninguna torre. Intenta otro.")
                continue

            # Pedir al jugador que elija la torre de destino
            destino = input(f"Escoge la torre de destino para mover el disco #{disco} (A, B, C): ").upper()
            if destino not in ["A", "B", "C"]:
                print("Opción de torre inválida, intenta de nuevo.")
                continue

            # Mapear las torres A, B, C a los índices correspondientes (0, 1, 2)
            origen_index = {"A": 0, "B": 1, "C": 2}[origen]
            destino_index = {"A": 0, "B": 1, "C": 2}[destino]

            # Intentar mover el disco
            if self.mover_disco(origen_index, destino_index):
                print(f"Disco #{disco} movido de la torre {origen} a la torre {destino}.")
            else:
                print(f"Movimiento inválido. No se puede mover el disco #{disco} allí.")

        # Mostrar el estado final
        self.imprimir_torres()
        print("¡Juego finalizado! Todos los discos están correctamente apilados en la torre C.")

    def juego_completo(self):
        """Verifica si el juego ha terminado (todos los discos en la torre C)."""
        return len(self.torres[2]) == self.num_discos and self.torres[2] == list(range(1, self.num_discos + 1))

# Ejecutar el juego con 3 discos
if __name__ == "__main__":
    juego = TorreHanoi(3)
    juego.jugar()
