import pandas as pd


class Solver:
    def __init__(self):
        self.df = pd.read_csv('../../data/Dec01.txt', header=None)

    def solve_part1(self):
        increases = self.df.diff() > 0
        return increases.sum().values[0]

    def solve_part2(self):
        window_size = 3
        increases = self.df.rolling(window_size).sum().diff() > 0
        return increases.sum().values[0]
