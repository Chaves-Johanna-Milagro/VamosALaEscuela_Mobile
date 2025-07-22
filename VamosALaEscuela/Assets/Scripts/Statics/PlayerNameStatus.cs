using UnityEngine;

public static class PlayerNameStatus
{
    private static string _playerName = "";

    public static void SetName(string name) //pa setear el nombre del jugador 
    {
        _playerName = name;
    }
    public static string GetName()
    {
        return _playerName;
    }
    public static void ResetName()
    {
        _playerName = "";
    }
}
