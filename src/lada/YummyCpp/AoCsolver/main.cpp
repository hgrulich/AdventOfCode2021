#include <iostream>
#include "dec01solver.h"

using namespace std;

int main()
{
    Dec01Solver Solver;

    Solver.read_input_data();

    cout << Solver.solve_part1() << endl;
    cout << Solver.solve_part2() << endl;
    return 0;
}
