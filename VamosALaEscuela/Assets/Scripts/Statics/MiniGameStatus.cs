using UnityEngine;

public static class MiniGameStatus
{
    public static bool IsActiveMiniGame()
    {
        GameObject[] miniGames = GameObject.FindGameObjectsWithTag("MiniGame");

        foreach (GameObject mg in miniGames)
        {
            if (mg.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
}
