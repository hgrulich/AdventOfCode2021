import pandas as pd
from abc import ABC, abstractmethod


class SubmarineCmdProcessor(ABC):
    def __init__(self):
        self.aim = 0
        self.depth = 0
        self.horizontal = 0

    @abstractmethod
    def _process_forward(self, steps):
        pass

    @abstractmethod
    def _process_down(self, steps):
        pass

    @abstractmethod
    def _process_up(self, steps):
        pass

    def compute_aim_pos(self, in_row):
        process_direction = getattr(self, '_process_' + in_row['Direction'])
        process_direction(in_row['Steps'])


class SubmarineCmdProcessorPart1(SubmarineCmdProcessor):
    def _process_forward(self, steps):
        self.horizontal += steps

    def _process_down(self, steps):
        self.depth += steps

    def _process_up(self, steps):
        self.depth -= steps


class SubmarineCmdProcessorPart2(SubmarineCmdProcessor):
    def _process_forward(self, steps):
        self.horizontal += steps
        self.depth += self.aim * steps

    def _process_down(self, steps):
        self.aim += steps

    def _process_up(self, steps):
        self.aim -= steps


class Solver:
    def __init__(self):
        self.df = pd.read_csv('../../data/Dec02.txt', header=None, names=['Direction', 'Steps'], sep='\s+')

    def _solve(self, cmd_processor):
        self.df.apply(cmd_processor.compute_aim_pos, axis=1)

    def solve_part1(self):
        cmd_proc = SubmarineCmdProcessorPart1()
        self._solve(cmd_proc)

        return cmd_proc.horizontal * cmd_proc.depth

    def solve_part2(self):
        cmd_proc = SubmarineCmdProcessorPart2()
        self._solve(cmd_proc)

        return cmd_proc.horizontal * cmd_proc.depth
