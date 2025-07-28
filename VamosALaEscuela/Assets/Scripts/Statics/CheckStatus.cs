using System.Collections.Generic;
using UnityEngine;

public static class CheckStatus//version mobile
{
    // Guarda el estado de los checks para cada escena
    // Clave = nombre de la escena, Valor = array de 3 bools para los 3 checks
    private static Dictionary<string, bool[]> _sceneChecks = new Dictionary<string, bool[]>();

    // Obtiene el array de estados para una escena
    public static bool[] GetChecksForScene(string sceneName)
    {
        if (!_sceneChecks.ContainsKey(sceneName))
        {
            // Si no existe aún, la inicializa con todos los checks en false
            _sceneChecks[sceneName] = new bool[3];
        }

        return _sceneChecks[sceneName];
    }

    // Activa un check en una escena específica
    public static void SetCheckActive(string sceneName, int index)
    {
        if (index < 0 || index > 2) return;

        GetChecksForScene(sceneName)[index] = true;
    }


    //devuelve la cantidad de checks inactivos en escenas especificas
    public static int GetInactiveChecksFromScenes(params string[] sceneNames)
    {
        int total = 0;
        foreach (string scene in sceneNames)
        {
            if (_sceneChecks.ContainsKey(scene))
            {
                bool[] checks = _sceneChecks[scene];
                foreach (bool check in checks)
                {
                    if (!check)
                        total++;
                }
            }
        }
        return total;
    }



    // Verifica si un check específico está activo
    public static bool IsCheckActive(string sceneName, int index)
    {
        if (index < 0 || index > 2)
            return false;

        bool[] checks = GetChecksForScene(sceneName);
        return checks[index];
    }


    public static void ClearAllStates()
    {
        _sceneChecks.Clear();
    }
}
