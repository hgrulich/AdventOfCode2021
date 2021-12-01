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

int Dec01Solver::solve_part1()
{
    std::vector<int> diff(data_frame.size() - 1);

    std::adjacent_difference(data_frame.begin(), data_frame.end(), diff.begin());
    return std::count_if(diff.begin(), diff.end(), [](int i){return i > 0;});
}

int Dec01Solver::solve_part2()
{
    return 0;
}
