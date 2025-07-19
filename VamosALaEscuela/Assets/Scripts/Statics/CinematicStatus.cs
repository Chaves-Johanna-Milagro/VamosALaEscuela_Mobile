using System.Collections.Generic;
using UnityEngine;

public static class CinematicStatus
{
    //Verifica si se toco en una cinematica
    public static bool TouchedInCinematic()
    {
        Vector3 screenPos = Vector3.zero;
        bool inputDetected = false;

        // Detecta click de mouse
        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Input.mousePosition;
            inputDetected = true;
        }
        // Detecta toque en pantalla
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            screenPos = Input.GetTouch(0).position;
            inputDetected = true;
        }

        if (inputDetected)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPos);
            Vector2 rayOrigin = new Vector2(worldPoint.x, worldPoint.y);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
            return hit.collider != null && hit.collider.CompareTag("Cinematic");
        }

        return false;
    }

    // Clase interna que guarda el estado completo del padre y de cada hijo
    private class ObjectState
    {
        public bool isActive;                       // Si el padre est� activo
        public List<bool> childActiveStates;        // Estados activos de los hijos
        public List<Vector3> childPositions;        // Posiciones locales de los hijos
    }

    // Diccionario donde se guarda el estado con clave �nica por escena + nombre
    private static Dictionary<string, ObjectState> _savedStates = new();

    /// <summary>
    /// Guarda el estado del padre (activo) y de sus hijos (estado y posici�n local)
    /// </summary>
    public static void SaveState(GameObject obj)
    {
        string id = GetUniqueId(obj);

        ObjectState state = new ObjectState
        {
            isActive = obj.activeSelf,
            childActiveStates = new List<bool>(),
            childPositions = new List<Vector3>()
        };

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            state.childActiveStates.Add(child.gameObject.activeSelf);          // Guardar si est� activo
            state.childPositions.Add(child.localPosition);                     // Guardar posici�n local
        }

        _savedStates[id] = state;
    }

    /// <summary>
    /// Restaura el estado activo del padre y el estado + posici�n de los hijos
    /// </summary>
    public static void LoadState(GameObject obj)
    {
        string id = GetUniqueId(obj);
        if (!_savedStates.ContainsKey(id)) return;

        ObjectState state = _savedStates[id];

        obj.SetActive(state.isActive); // Restaurar estado del padre

        for (int i = 0; i < obj.transform.childCount &&
                        i < state.childActiveStates.Count &&
                        i < state.childPositions.Count; i++)
        {
            Transform child = obj.transform.GetChild(i);
            child.gameObject.SetActive(state.childActiveStates[i]);       // Restaurar si est� activo
            child.localPosition = state.childPositions[i];                // Restaurar posici�n local
        }
    }

    /// <summary>
    /// Verifica si ya se guard� un estado para este objeto
    /// </summary>
    public static bool HasState(GameObject obj)
    {
        return _savedStates.ContainsKey(GetUniqueId(obj));
    }

    /// <summary>
    /// Devuelve una clave �nica para el objeto: escena + nombre
    /// </summary>
    private static string GetUniqueId(GameObject obj)
    {
        return obj.scene.name + "_" + obj.name;
    }

}
