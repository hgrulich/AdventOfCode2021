#ifndef GENERICSOLVER_H
#define GENERICSOLVER_H


class GenericSolver
{
public:
    virtual int solve_part1() = 0;
    virtual int solve_part2() = 0;
    virtual void read_input_data() = 0;
};

#endif // GENERICSOLVER_H
