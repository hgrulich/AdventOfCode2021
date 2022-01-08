import pandas as pd


class Patterns:
    #  AAAA
    # B    C
    # B    C
    #  DDDD
    # E    F
    # E    F
    #  GGGG

    def __init__(self, pattern_samples):
        for p in pattern_samples:
            p = set(p)
            match len(p):
                case 4:
                    four = p
                case 2:
                    one = p
                case 7:
                    eight = p

        self.BD = four - one
        self.AEG = eight - four
        self.CF = one


class Solver:
    def __init__(self):
        self.df = None
        self.line_strings = []

        self.path = '../../data/Dec08.txt'

    def translate_digits(self, digits, patterns):
        translated = ''
        for value in digits:
            value = set(value)
            match len(value):
                case 2:
                    digit = 1
                case 3:
                    digit = 7
                case 4:
                    digit = 4
                case 5:
                    if patterns.AEG.issubset(value):
                        digit = 2
                    elif patterns.BD.issubset(value):
                        digit = 5
                    else:
                        digit = 3
                case 6:
                    if not patterns.BD.issubset(value):
                        digit = 0
                    elif patterns.CF.issubset(value):
                        digit = 9
                    else:
                        digit = 6
                case 7:
                    digit = 8

            translated += str(digit)

        return int(translated)

    def solve_part1(self):
        df = pd.read_csv(self.path, header=None, sep=' ')
        lens = pd.DataFrame([df[col].str.len() for col in [11, 12, 13, 14]])
        return lens[(lens == 2) | (lens == 3) | (lens == 4) | (lens == 7)].count().sum()

    def solve_part2(self):
        translated_value_sum = 0
        with open(self.path) as file:
            for line in file:
                pattern_samples, out_values = line.split('|')
                pattern_samples = pattern_samples.split()
                out_values = out_values.split()

                translated_value_sum += self.translate_digits(out_values, Patterns(pattern_samples))

        return translated_value_sum
