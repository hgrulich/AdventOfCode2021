import pandas as pd


class Solver:
    def __init__(self):
        self.df = pd.read_csv('data/Dec01.txt', header=None)

    def solve_part1(self):
        increases = self.df.diff() > 0
        return increases.sum().values[0]

    def solve_part2(self):
        pass
