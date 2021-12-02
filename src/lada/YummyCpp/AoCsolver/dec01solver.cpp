#include "dec01solver.h"
#include <iostream>
#include <fstream>
#include <numeric>
#include <algorithm>

Dec01Solver::Dec01Solver()
{

}

void Dec01Solver::read_input_data()
{
    std::fstream fin("../../data/Dec01.txt", std::ios_base::in);

    int input_number;
    while (fin >> input_number)
    {
        data_frame.push_back(input_number);
    }
}

// TODO: comming soon...
int Dec01Solver::count_positive_increments(const std::vector<int> &vec)
{
    std::vector<int> diff(vec.size() - 1);

    // next() - we need to skip first element of input vector, because out[0] = in[0] in adjacent_diff
    std::adjacent_difference(std::next(vec.begin(), 1), vec.end(), diff.begin());
    return std::count_if(diff.begin(), diff.end(), [](int i){return i > 0;});
}

int Dec01Solver::solve_part1()
{
    return count_positive_increments(data_frame);
}

int Dec01Solver::solve_part2()
{
    int window_size = 3;
    std::vector<int> result;

    // C called from 80s and wanted its solution back
    for(unsigned long i = 0; i < data_frame.size() - window_size + 1; i++)
    {
        int sum = 0;
        for(auto j = 0; j < window_size; j++)
            sum += data_frame[i + j];

        result.push_back(sum);
    }

    return count_positive_increments(result);
}
