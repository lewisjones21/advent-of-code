using System;
using System.Collections.Generic;

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
        // Process the input instructions
        int instruction = 0;
        int accumulator = 0;
        List<int> visitedInstructions = new List<int>();
        while (!visitedInstructions.Contains(instruction))
        {
            visitedInstructions.Add(instruction);
            switch (_input[instruction].Item1)
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
        // Return the accumulator value when an instruction is first revisited
        return accumulator;
    }
}
