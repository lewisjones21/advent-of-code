using System;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : CalculatorBase<Tuple<Calculator.Operation, int>, long>
{
    public enum Operation
    {
        NoOperation = 0,
        Accumulate,
        Jump
    }

    protected override Tuple<Operation, int> ParseInputLine(string inputLine)
    {
        string[] operationAndArgument = inputLine.Split(' ');
        return new Tuple<Operation, int>(operationAndArgument[0] == "acc" ? Operation.Accumulate
                                         : (operationAndArgument[0] == "jmp" ? Operation.Jump
                                            : Operation.NoOperation),
                                         int.Parse(operationAndArgument[1]));
    }

    protected override long CalculateOutput()
    {
        Operation operation;
        for (int changeIndex = _input.Count - 1; changeIndex >= 0; changeIndex--)
        {
            // Skip iterations where the target operation to change is not under consideration
            if (_input[changeIndex].Item1 == Operation.Accumulate)
            {
                continue;
            }
            // Process the input instructions
            int instruction = 0;
            int accumulator = 0;
            List<int> visitedInstructions = new List<int>();
            while (!visitedInstructions.Contains(instruction))
            {
                if (instruction == _input.Count)
                {
                    Debug.LogFormat(
                        "Program terminated correctly when instruction {0} was changed",
                        changeIndex);
                    return accumulator;
                }
                else if (instruction > _input.Count)
                {
                    Debug.LogErrorFormat(
                        "Program terminated incorrectly when instruction {0} was changed; " +
                        "ended {1} instructions out of range",
                        changeIndex, instruction - _input.Count);
                    break;
                }
                visitedInstructions.Add(instruction);
                operation = _input[instruction].Item1;
                if (instruction == changeIndex)
                {
                    if (operation == Operation.NoOperation)
                    {
                        operation = Operation.Jump;
                    }
                    else
                    {
                        operation = Operation.NoOperation;
                    }
                }
                switch (operation)
                {
                    case Operation.NoOperation:
                        instruction++;
                        break;
                    case Operation.Accumulate:
                        accumulator += _input[instruction].Item2;
                        instruction++;
                        break;
                    case Operation.Jump:
                        instruction += _input[instruction].Item2;
                        break;
                }
            }
        }

        Debug.LogError("No solution was found");
        return 0;
    }
}
