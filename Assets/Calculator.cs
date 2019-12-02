using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    private List<int> _inputs = new List<int>()
    {
        0
    };

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Output is: " + Calculate().ToString());
    }

    // Update is called once per frame
    private int Calculate()
    {
        return 0;
    }
}
