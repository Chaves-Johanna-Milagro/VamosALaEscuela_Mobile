using UnityEngine;
using UnityEngine.EventSystems;

public static class ClickInUIStatus  //pa detecta si el cursor esta sobre la ui en pc o se toco en mobile
{
    // Método para PC / Editor
    public static bool IsPointerOverUI_PC()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    // Método para Mobile (Android/iOS)
    public static bool IsPointerOverUI_Mobile()
    {
        if (EventSystem.current == null)
            return false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                return true;
        }
        return false;
    }
}
