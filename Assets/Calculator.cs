using System.Collections.Generic;

public class Calculator : CalculatorBase<List<char>, long>
{
    protected override List<char> ParseInputLine(string inputLine)
    {
        return new List<char>(inputLine.ToCharArray());
    }

    protected override long CalculateOutput()
    {
        long accumulator = 0;
        int[] slopeXValues = { 1, 3, 5, 7, 1 };
        int[] slopeYValues = { 1, 1, 1, 1, 2 };
        for (int s = 0; s < slopeXValues.Length; s++)
        {
            int treeHits = 0;
            for (int yPos = 0, xPos = 0;
                 yPos < _input.Count;
                 yPos += slopeYValues[s], xPos += slopeXValues[s])
            {
                if (_input[yPos][xPos % _input[yPos].Count] == '#')
                {
                    treeHits++;
                }
            }
            if (accumulator == 0)
            {
                accumulator = treeHits;
            }
            else
            {
                accumulator *= treeHits;
            }
        }
        return accumulator;
    }
}
