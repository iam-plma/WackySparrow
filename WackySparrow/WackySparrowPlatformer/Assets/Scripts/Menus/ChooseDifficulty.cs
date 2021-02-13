using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChooseDifficulty
{
    private static Difficulties difficulty;

    public static Difficulties Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
}
