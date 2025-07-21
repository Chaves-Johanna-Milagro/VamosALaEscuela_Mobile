using System.Collections.Generic;
using UnityEngine;

public static class CheckStatus
{
    // Estructura para almacenar solo los estados activos de los hijos
    private class ObjectState
    {
        public bool isActive;                    // Estado activo del objeto padre
        public List<bool> childActiveStates;     // Estados activos de los hijos
    }

    // Diccionario donde se guarda el estado con clave única por escena + nombre
    private static Dictionary<string, ObjectState> _savedStates = new();

    /// <summary>
    /// Guarda el estado del padre (activo) y el estado activo de sus hijos
    /// </summary>
    public static void SaveState(GameObject obj)
    {
        string id = GetUniqueId(obj);

        ObjectState state = new ObjectState
        {
            isActive = obj.activeSelf,
            childActiveStates = new List<bool>()
        };

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            state.childActiveStates.Add(child.gameObject.activeSelf);
        }

        _savedStates[id] = state;
    }

    /// <summary>
    /// Restaura el estado activo del padre y de sus hijos
    /// </summary>
    public static void LoadState(GameObject obj)
    {
        string id = GetUniqueId(obj);
        if (!_savedStates.ContainsKey(id)) return;

        ObjectState state = _savedStates[id];

        obj.SetActive(state.isActive);

        for (int i = 0; i < obj.transform.childCount &&
                        i < state.childActiveStates.Count; i++)
        {
            Transform child = obj.transform.GetChild(i);
            child.gameObject.SetActive(state.childActiveStates[i]);
        }
    }

    /// <summary>
    /// Verifica si ya se guardó un estado para este objeto
    /// </summary>
    public static bool HasState(GameObject obj)
    {
        return _savedStates.ContainsKey(GetUniqueId(obj));
    }

    /// <summary>
    /// Devuelve una clave única para el objeto: escena + nombre
    /// </summary>
    private static string GetUniqueId(GameObject obj)
    {
        return obj.scene.name + "_" + obj.name;
    }

    public static void ClearAllStates()
    {
        _savedStates.Clear();
    }
}
