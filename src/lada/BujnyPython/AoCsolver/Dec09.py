import numpy as np


class Solver:
    def __init__(self):
        self.df = None

        self.path = '../../data/Dec09.txt'
        self.arr = None

        # coord differences to get from a point to its neighbours of interest
        self.neighbour_directions = np.array([(1, 0), (-1, 0), (0, 1), (0, -1)])
        self.current_basin = []
        self.basins = []

    def read_input_data(self):
        with open(self.path) as file:
            self.arr = np.array([[int(x) for x in line.rstrip()] for line in file])

    def get_neighbours_of_interest(self, row, col):
        return np.array([self.arr[row + row_diff, col + col_diff] for row_diff, col_diff in self.neighbour_directions])

    def get_low_spots(self):
        pass

    def solve_part1(self):
        self.read_input_data()

        # Dirty little trick - add 9s all around the matrix so we don't have to care array edges
        self.arr = np.pad(self.arr, 1, mode='constant', constant_values=9)

        low_spots = []

        rows, cols = self.arr.shape
        for row in range(1, rows - 1):
            for col in range(1, cols - 1):
                neighbours = self.get_neighbours_of_interest(row, col)

                if self.arr[row, col] < min(neighbours):
                    low_spots.append(self.arr[row, col] + 1)

        return np.array(low_spots).sum()

    def build_basin(self, r, c):
        # 9s are boundaries of basins
        if self.arr[r, c] == 9:
            return

        if (r, c) not in self.current_basin:
            self.current_basin.append((r, c))
        else:
            # we already visited this place, don't pursue this road
            return

        neighbours = self.get_neighbours_of_interest(r, c)
        for x, y in self.neighbour_directions[neighbours > self.arr[r, c]]:
            self.build_basin(r + x, c + y)

        return

    def solve_part2(self):
        self.read_input_data()

        # Dirty little trick - add 9s all around the matrix so we don't have to care array edges
        self.arr = np.pad(self.arr, 1, mode='constant', constant_values=9)

        rows, cols = self.arr.shape

        for row in range(1, rows - 1):
            for col in range(1, cols - 1):
                neighbours = self.get_neighbours_of_interest(row, col)
                if self.arr[row, col] < min(neighbours):
                    # we found a low spot, lets examine its basin
                    self.current_basin = []
                    self.build_basin(row, col)

                    self.basins.append(self.current_basin)

        basin_lens = np.sort(np.array([len(x) for x in self.basins]))

        # multiply three highest basin lengths - i.e. last three numbers of our list
        return np.prod(basin_lens[-3:])
