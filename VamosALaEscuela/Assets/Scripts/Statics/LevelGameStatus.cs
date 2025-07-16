using UnityEngine;

public static class LevelGameStatus
{
    private static string _levelGame = "";

    public static void SetLevel(string level) //pa setear el nombre del jugador 
    {
        _levelGame = level;
    }
    public static string GetLevel()
    {
        return _levelGame;
    }
}
