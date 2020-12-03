using System.Collections.Generic;

public class Calculator : CalculatorBase<List<char>, int>
{
    protected override List<char> ParseInputLine(string inputLine)
    {
        return new List<char>(inputLine.ToCharArray());
    }

    protected override int CalculateOutput()
    {
        int treeHits = 0;
        for (int i = 0, xPos = 0; i < _input.Count; i++, xPos += 3)
        {
            if (_input[i][xPos % _input[i].Count] == '#')
            {
                treeHits++;
            }
        }
        return treeHits;
    }
}
