using System;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : CalculatorBase<Tuple<string, List<Tuple<int, string>>>, long>
{
    protected override Tuple<string, List<Tuple<int, string>>> ParseInputLine(string inputLine)
    {
        string[] keyAndValues = inputLine.Split(new string[] { " bags contain " },
                                                StringSplitOptions.RemoveEmptyEntries);
        string[] countsAndValues = keyAndValues[1].Split(
            new string[] { " bag, ", " bags, ", " bag.", " bags." },
            StringSplitOptions.RemoveEmptyEntries);
        if (countsAndValues.Length == 0 || countsAndValues[0] == "no other")
        {
            return new Tuple<string, List<Tuple<int, string>>>(keyAndValues[0],
                                                               new List<Tuple<int, string>>());
        }
        Tuple<int, string>[] structuredValues = new Tuple<int, string>[countsAndValues.Length];
        for (int v = 0; v < countsAndValues.Length; v++)
        {
            structuredValues[v] = new Tuple<int, string>(
                int.Parse(countsAndValues[v].Substring(0, 1)), countsAndValues[v].Substring(2));
        }
        return new Tuple<string, List<Tuple<int, string>>>(
            keyAndValues[0], new List<Tuple<int, string>>(structuredValues));
    }

    protected override long CalculateOutput()
    {
        string target = "shiny gold";
        // Determine whether each type of bag contains the target bag
        // 0 - undetermined, 1 - yes, 2 - no
        List<int> containsTarget = new List<int>(new int[_input.Count]);
        for (int i = 0; i < _input.Count; i++)
        {
            containsTarget[i] = ContainsTarget(target, _input, containsTarget, i) ? 1 : 2;
        }
        // Count the number of different bag types which could contain the target
        // (Do not count the target itself)
        int sum = 0;
        for (int c = 0; c < containsTarget.Count; c++)
        {
            if (containsTarget[c] == 1 && _input[c].Item1 != target)
            {
                sum++;
            }
        }

        return sum;
    }

    private bool ContainsTarget(string target,
                                List<Tuple<string, List<Tuple<int, string>>>> inputs,
                                List<int> inputContainsTarget,
                                int currentIndex,
                                int recursionCount = 0)
    {
        if (recursionCount > 100)
        {
            Debug.LogError("Recursion level has reached 100; aborting calculation; " +
                           "results may be invalid");
            return false;
        }
        if (inputContainsTarget[currentIndex] == 1 || inputs[currentIndex].Item1 == target)
        {
            return true;
        }
        else if (inputContainsTarget[currentIndex] == 2)
        {
            return false;
        }
        for (int i = 0; i < inputs[currentIndex].Item2.Count; i++)
        {
            int newIndex = inputs.FindIndex((Tuple<string, List<Tuple<int, string>>> obj)
                                            => obj.Item1 == inputs[currentIndex].Item2[i].Item2);
            if (ContainsTarget(
                target, inputs, inputContainsTarget, newIndex, recursionCount + 1))
            {

                inputContainsTarget[currentIndex] = 1;
                return true;
            }
        }
        inputContainsTarget[currentIndex] = 2;
        return false;
    }
}
