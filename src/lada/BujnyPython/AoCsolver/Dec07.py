import pandas as pd
from scipy.optimize import minimize_scalar


class Solver:
    def __init__(self):
        self.df = None
        self.line_strings = []

        self.read_input_data('../../data/Dec07.txt')

    def read_input_data(self, path):
        # read data from csv and transpose them
        self.df = pd.read_csv(path, header=None).T

    def lateral_distance_linear(self, x):
        return abs(self.df - x).sum()[0]

    def lateral_distance_triangular(self, x):
        diff = abs(self.df - x)
        return ((diff**2 + diff) // 2).sum()[0]

    def _solve(self, fun):
        res = minimize_scalar(fun)
        # round the x that minimizes fun to int and compute fun
        return fun(round(res.x))

    def solve_part1(self):
        return self._solve(self.lateral_distance_linear)

    def solve_part2(self):
        return self._solve(self.lateral_distance_triangular)
