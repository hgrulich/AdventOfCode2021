from itertools import product


class Solver:
    def __init__(self):
        self.df = None

        self.path = '../../data/Dec11.txt'
        self.arr = []

        self.N = None

    def read_input_data(self):
        with open(self.path) as file:
            self.arr = [[int(x) for x in line.strip()] for line in file.readlines()]

        self.N = len(self.arr)

    def get_neighbours(self, x, y):
        neighbours = list(product(range(max(x - 1, 0), min(x + 2, self.N)), range(max(y - 1, 0), min(y + 2, self.N))))
        neighbours.remove((x, y))

        return neighbours

    def solve_part1(self):
        self.read_input_data()
        flashes = 0

        total_iterations = 100
        for i in range(total_iterations):
            to_increment_list = list(product(range(self.N), repeat=2))
            flashed = []
            while to_increment_list:
                x, y = to_increment_list.pop()
                if (x, y) not in flashed:
                    self.arr[x][y] += 1
                if self.arr[x][y] > 9:
                    self.arr[x][y] = 0
                    flashes += 1
                    flashed.append((x, y))
                    to_increment_list += self.get_neighbours(x, y)

        return flashes

    def solve_part2(self):
        self.read_input_data()
        flashes = 0

        iterations = 0
        flashed = []

        while True:
            iterations += 1
            to_increment_list = list(product(range(self.N), repeat=2))
            flashed = []
            while to_increment_list:
                x, y = to_increment_list.pop()
                if (x, y) not in flashed:
                    self.arr[x][y] += 1
                if self.arr[x][y] > 9:
                    self.arr[x][y] = 0
                    flashes += 1
                    flashed.append((x, y))
                    to_increment_list += self.get_neighbours(x, y)

            if len(flashed) == self.N * self.N:
                break

        return iterations
