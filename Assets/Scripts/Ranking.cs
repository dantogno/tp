using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ranking
{
    public static int RankPoints = 0;
    public static int TotalPossiblePoints = 0;

    public static string GetRankText()
    {
        float rank = RankPoints / TotalPossiblePoints;
        string toReturn = "";
        if (rank == 1)
            toReturn = "S";
        else if (rank >= 0.7f)
            toReturn = "A";
        else if (rank >= 0.5f)
            toReturn = "B";
        else if (rank >= 0.3f)
            toReturn = "C";
        else if (rank >= 0.1f)
            toReturn = "D";
        else
            toReturn = "F";

        return toReturn;
    }
}
