#ifndef DEC02SOLVER_H
#define DEC02SOLVER_H

#include <string>
#include <vector>
#include "genericsolver.h"

struct sub_command
{
    std::string direction;
    int steps;
};

class GenericCmdProcessor
{
    virtual void process_forward(int steps) = 0;
    virtual void process_up(int steps) = 0;
    virtual void process_down(int steps) = 0;

protected:
    int horizontal = 0;
    int depth = 0;
    int aim = 0;

public:
    void process_directions(const sub_command &cmd)
    {
        if (cmd.direction == "forward")
        {
            process_forward(cmd.steps);
        }
        else if (cmd.direction == "up")
        {
            process_up(cmd.steps);
        }
        else if (cmd.direction == "down")
        {
           process_down(cmd.steps);
        }
    }

    int get_horizontal() { return horizontal; }
    int get_depth() {return depth; }
};

class Dec02CmdProcessorPart1 : public GenericCmdProcessor
{
    void process_forward(int steps) override
    {
        horizontal += steps;
    }

    void process_up(int steps) override
    {
        depth -= steps;
    }

    void process_down(int steps) override
    {
        depth += steps;
    }
};

class Dec02CmdProcessorPart2 : public GenericCmdProcessor
{
    void process_forward(int steps) override
    {
        horizontal += steps;
        depth += aim * steps;
    }

    void process_up(int steps) override
    {
        aim -= steps;
    }

    void process_down(int steps) override
    {
        aim += steps;
    }
};


class Dec02Solver : public GenericSolver
{
     std::vector<sub_command> data_frame;

public:
    Dec02Solver();
    void read_input_data() override;
    int solve(GenericCmdProcessor &CmdProc);
    int solve_part1() override;
    int solve_part2() override;
};

#endif // DEC02SOLVER_H
