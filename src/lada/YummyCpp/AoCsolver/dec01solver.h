#ifndef DEC01SOLVER_H
#define DEC01SOLVER_H

#include <vector>

class GenericSolver
{
public:
    virtual int solve_part1() = 0;
    virtual int solve_part2() = 0;
};


class Dec01Solver : public GenericSolver
{
private:
    std::vector<int> data_frame;
    int count_positive_increments(const std::vector<int> &vec);
public:
    Dec01Solver();
    void read_input_data();
    int solve_part1() override;
    int solve_part2() override;
};

#endif // DEC01SOLVER_H
