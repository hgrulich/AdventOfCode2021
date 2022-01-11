
class Solver:
    def __init__(self):
        self.df = None

        self.path = '../../data/Dec10.txt'
        self.input_lines = []

    def read_input_data(self):
        with open(self.path) as file:
            self.input_lines = file.readlines()

    def solve_part1(self):
        self.read_input_data()

        opening_to_closing = {'(': ')',
                              '<': '>',
                              '[': ']',
                              '{': '}'}

        char_to_value = {')': 3,
                         ']': 57,
                         '}': 1197,
                         '>': 25137}

        ratings = []
        for line in self.input_lines:
            expected_closings = []
            for char in line.strip():
                # opening or closing char?
                if char in opening_to_closing:
                    expected_closings.append(opening_to_closing[char])
                else:  # must be a closing char
                    if char != expected_closings.pop():
                        ratings.append(char_to_value[char])
                        break

        return sum(ratings)

    def evaluate_missing(self, line):
        char_to_value = {')': 1,
                         ']': 2,
                         '}': 3,
                         '>': 4}
        score = 0
        for char in line[::-1]:
            score = 5 * score + char_to_value[char]

        return score

    def solve_part2(self):
        self.read_input_data()

        opening_to_closing = {'(': ')',
                              '<': '>',
                              '[': ']',
                              '{': '}'}

        all_scores = []
        for line in self.input_lines:
            expected_endings = []
            valid_line = True
            for char in line.strip():
                # opening or closing char?
                if char in opening_to_closing:
                    expected_endings.append(opening_to_closing[char])
                else:  # must be a closing char
                    if char != expected_endings.pop():
                        valid_line = False
                        break
            # end of line and nothing illegal found -> store expected endings
            if valid_line:
                all_scores.append(self.evaluate_missing(expected_endings))

        all_scores.sort()
        return all_scores[(len(all_scores) - 1) // 2]
