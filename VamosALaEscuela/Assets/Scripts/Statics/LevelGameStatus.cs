using UnityEngine;

public static class LevelGameStatus
{
    private static string _levelGame = "";

    public static void SetLevel(string level) //pa setear el nivel del juego
    {
        _levelGame = level;
    }
    public static string GetLevel()
    {
        return _levelGame;
    }
    public static void ResetLevel()
    {
        _levelGame = "";
    } 
}
