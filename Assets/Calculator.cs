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
        // -1 - undetermined, n - contains n bags
        List<int> containmentCounts = new List<int>(new int[_input.Count]);
        for (int c = 0; c < containmentCounts.Count; c++)
        {
            containmentCounts[c] = -1;
        }
        // Find how many bags the target bag type contains
        int targetIndex = _input.FindIndex((Tuple<string, List<Tuple<int, string>>> obj)
                                           => obj.Item1 == target);
        return ContainmentCount(_input, containmentCounts, targetIndex);
    }

    private int ContainmentCount(List<Tuple<string, List<Tuple<int, string>>>> schema,
                                 List<int> containmentCounts,
                                 int currentIndex,
                                 int recursionCount = 0)
    {
        if (containmentCounts[currentIndex] >= 0)
        {
            return containmentCounts[currentIndex];
        }
        if (recursionCount > 100)
        {
            Debug.LogError("Recursion level has reached 100; aborting calculation; " +
                           "results may be invalid");
            return 0;
        }
        int numContainedInThis = 0;
        for (int i = 0; i < schema[currentIndex].Item2.Count; i++)
        {
            // Find the index of the contained bag in the schema
            int newIndex = schema.FindIndex((Tuple<string, List<Tuple<int, string>>> obj)
                                            => obj.Item1 == schema[currentIndex].Item2[i].Item2);
            // Find how many bags the contained bag contains, and multiply it by the number of
            // instances of that contained bag, before adding to the total for the current bag
            numContainedInThis += schema[currentIndex].Item2[i].Item1 * (1 + ContainmentCount(
                schema, containmentCounts, newIndex, recursionCount + 1));
        }
        containmentCounts[currentIndex] = numContainedInThis;
        return numContainedInThis;
    }
}
