#include <fstream>
#include <algorithm>
#include "dec02solver.h"

Dec02Solver::Dec02Solver()
{

}

void Dec02Solver::read_input_data()
{
    std::fstream fin("../../data/Dec02.txt", std::ios_base::in);

    sub_command scmd;
    while (fin >> scmd.direction >> scmd.steps)
    {
        data_frame.push_back(scmd);
    }
}

int Dec02Solver::solve(GenericCmdProcessor &CmdProc)
{
    std::for_each(data_frame.begin(), data_frame.end(), [&CmdProc] (sub_command &cmd) {CmdProc.process_directions(cmd);});

    return CmdProc.get_depth() * CmdProc.get_horizontal();
}

int Dec02Solver::solve_part1()
{
    Dec02CmdProcessorPart1 CmdProc;

    return(solve(CmdProc));
}

int Dec02Solver::solve_part2()
{
    Dec02CmdProcessorPart2 CmdProc;

    return(solve(CmdProc));
}
