#include <iostream>
#include "dec02solver.h"

using namespace std;

int main()
{
    Dec02Solver Solver;

    Solver.read_input_data();

    cout << Solver.solve_part1() << endl;
    cout << Solver.solve_part2() << endl;
    return 0;
}
