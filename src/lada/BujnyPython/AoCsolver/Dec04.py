
class BingoBoardCell:
    def __init__(self, value=0, drawn=False):
        self.value = value
        self.drawn = drawn


class BingoBoard:
    def __init__(self, board_size=5):
        self.cells = []
        self.row_sums = [0] * board_size
        self.cell_sums = [0] * board_size
        self.winner = False
        self.board_size = board_size

    def add_row(self, row):
        row = [BingoBoardCell(int(value)) for value in row.split()]
        self.cells.append(row)

    def __str__(self):
        output = ''
        for rows in self.cells:
            for cell in rows:
                if cell.drawn:
                    output += '*'
                output += str(cell.value)
                output += ' '

            output += '\n'

        return output

    def draw_value(self, value):
        for row_idx, row in enumerate(self.cells):
            for cell_idx, cell in enumerate(row):
                if cell.value == value:
                    cell.drawn = True
                    self.row_sums[row_idx] += 1
                    self.cell_sums[cell_idx] += 1

                    # as soon as the count of selected numbers in one row/column equals board size, this is a winner
                    if self.row_sums[row_idx] == self.board_size or self.cell_sums[cell_idx] == self.board_size:
                        self.winner = True

    def get_unmarked_sum(self):
        """Counts sum of all board values that have not been drawn yet"""
        return sum([sum([x.value for x in rows if not x.drawn]) for rows in self.cells])


class Solver:
    def __init__(self):
        self.Boards = []
        self.winner_boards = 0
        self.last_draw_idx = 0
        self.draws = []

        self.read_input_data('../../data/Dec04.txt')

    def read_input_data(self, path):
        with open(path) as f:
            self.draws = [int(value) for value in f.readline().split(',')]

            # each board has size of 5x5 numbers
            board_size = 5
            row_counter = 0
            brd = None
            for line in f:
                line = line.strip()
                if line == '':
                    continue

                # after each five rows create a new board
                if not row_counter % board_size:
                    brd = BingoBoard(board_size)
                    self.Boards.append(brd)

                brd.add_row(line)
                row_counter += 1

    def solve_part1(self):
        for self.last_draw_idx, draw in enumerate(self.draws):
            for brd in self.Boards:
                brd.draw_value(draw)
                if brd.winner:
                    self.winner_boards += 1
                    return brd.get_unmarked_sum() * draw

        return 0

    def solve_part2(self):
        for draw in self.draws[self.last_draw_idx + 1:]:
            for brd in self.Boards:
                if brd.winner:
                    continue

                brd.draw_value(draw)
                if brd.winner:
                    self.winner_boards += 1
                    # is this the last board to win?
                    if self.winner_boards == len(self.Boards):
                        return brd.get_unmarked_sum() * draw

        return 0
