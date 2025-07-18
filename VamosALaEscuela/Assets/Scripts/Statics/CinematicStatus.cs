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
}
