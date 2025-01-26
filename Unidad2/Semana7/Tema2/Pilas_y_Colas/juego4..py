class TorreHanoi:
    def __init__(self, num_discos):
        self.num_discos = num_discos
        self.torres = [[i for i in range(num_discos, 0, -1)], [], []]  # Torre A tiene los discos iniciales

    def imprimir_torres(self):
        """Imprime el estado actual de las torres de Hanoi con los discos claramente separados por columnas."""
        max_disco = self.num_discos
        ancho_torre = max_disco * 2 + 2  # Ancho máximo de una torre (incluye espacio adicional para centrar)
        vacio = " " * ancho_torre  # Espacio vacío para torres sin discos

        # Crear la representación visual
        niveles = []
        for i in range(max_disco, 0, -1):  # De arriba hacia abajo
            nivel = []
            for torre in self.torres:
                if len(torre) >= i:
                    disco = torre[i - 1]
                    # Disco centrado con su número
                    relleno_izq = " " * (max_disco - disco + 1)
                    relleno_der = " " * (max_disco - disco)
                    nivel.append(f"{relleno_izq}#{disco}{'#' * (disco - 1)}{relleno_der}")
                else:
                    nivel.append(vacio)
            niveles.append(nivel)

        # Imprimir cada nivel línea por línea
        for nivel in niveles:
            print(" ".join(nivel))  # Agregar espacios entre las torres

        # Imprimir etiquetas centradas para las torres
        etiquetas = ["A", "B", "C"]
        print("".join(f"{etiqueta:^{ancho_torre}}" for etiqueta in etiquetas))
        print()

    def es_valido(self, origen, destino):
        if not self.torres[origen]:  # Si la torre de origen está vacía
            return False
        if not self.torres[destino]:  # Si la torre de destino está vacía
            return True
        if self.torres[origen][-1] > self.torres[destino][-1]:  # No se puede mover un disco más grande sobre uno más pequeño
            return False
        return True

    def mover_disco(self, origen, destino):
        if self.es_valido(origen, destino):
            disco = self.torres[origen].pop()
            self.torres[destino].append(disco)
            return True
        return False

    def jugar(self):
        while not self.juego_completo():
            self.imprimir_torres()

            try:
                disco = int(input(f"Escoge un número de disco (1-{self.num_discos}): "))
                if disco < 1 or disco > self.num_discos:
                    print("Número de disco inválido, intenta de nuevo.")
                    continue
            except ValueError:
                print("Entrada no válida, intenta de nuevo.")
                continue

            encontrado = False
            for i, torre in enumerate(self.torres):
                if disco in torre:
                    origen = ["A", "B", "C"][i]
                    encontrado = True
                    break
            if not encontrado:
                print(f"El disco #{disco} no está en ninguna torre. Intenta otro.")
                continue

            destino = input(f"Escoge la torre de destino para mover el disco #{disco} (A, B, C): ").upper()
            if destino not in ["A", "B", "C"]:
                print("Opción de torre inválida, intenta de nuevo.")
                continue

            origen_index = {"A": 0, "B": 1, "C": 2}[origen]
            destino_index = {"A": 0, "B": 1, "C": 2}[destino]

            if self.mover_disco(origen_index, destino_index):
                print(f"Disco #{disco} movido de la torre {origen} a la torre {destino}.")
            else:
                print(f"Movimiento inválido. No se puede mover el disco #{disco} allí.")

        self.imprimir_torres()
        print("¡GANASTE! El juego terminó.")

    def juego_completo(self):
        return len(self.torres[2]) == self.num_discos and self.torres[2] == list(range(self.num_discos, 0, -1))


if __name__ == "__main__":
    juego = TorreHanoi(3)
    juego.jugar()
