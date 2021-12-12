import pandas as pd


class Solver:
    def __init__(self):
        # read line by line and convert strings to list of chars
        with open('../../data/Dec03.txt') as f:
            lines = [list(line.rstrip()) for line in f]

        self.df = pd.DataFrame(lines)
        self.df = self.df.astype('int')
        self.df = self.df.astype(bool)

    def _bool_data_frame_to_int(self, bdf):
        # convert True/False to 1/0 ints
        bdf = bdf.astype('int')

        # and now to '1'/'0' string
        bdf = bdf.astype('string')

        # join all individual '1'/'0' strings and covert the result to int
        return int(bdf.agg(''.join, axis=1).iloc[0], base=2)

    def solve_part1(self):
        # most common value for each column -> modus
        most_common = self.df.mode()
        least_common = ~most_common

        return self._bool_data_frame_to_int(most_common) * self._bool_data_frame_to_int(least_common)

    def find_common_index(self, find_least_common=False):
        filtered_df = self.df
        for bit_position in range(self.df.shape[1]):  # Iterate through columns
            common_bit_value = filtered_df[bit_position].mode()

            # if 1s and 0s are equally common (more than one common value returned), pick 1
            if len(common_bit_value) > 1:
                common_bit_value = pd.Series([True])

            # if we have more common bit values than one at this point, something is terribly wrong
            assert(len(common_bit_value) == 1)

            if find_least_common:
                common_bit_value = ~common_bit_value

            # filter dataframe - keep only rows with most common bit value at bit bit_position
            filtered_df = filtered_df[filtered_df[bit_position] == common_bit_value[0]]

            # if only one item is left in filtered dataframe, it's our winner
            if len(filtered_df.index) == 1:
                break

            # if check of bit nr 12 (index 11) didn't bring winner, we're in trouble
            assert(bit_position != 11)

        return filtered_df.index

    def solve_part2(self):
        most_common_index = self.find_common_index()
        least_common_index = self.find_common_index(find_least_common=True)

        return self._bool_data_frame_to_int(self.df.iloc[most_common_index, :]) * \
               self._bool_data_frame_to_int(self.df.iloc[least_common_index, :])
