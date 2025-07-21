using UnityEngine;

public static class TouchObjectStatus //version mobile
{
    // Verifica si el GameObject fue tocado o clickeado, según la posición en pantalla.
    public static bool TouchedThisObject(Vector3 screenPos, GameObject obj)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 rayOrigin = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        return hit.collider != null && hit.collider.gameObject == obj;
    }

    // Verifica si se tocó o clickeó un objeto con un tag específico.

    /*public static bool TouchedTaggedObject(Vector3 screenPos, string tag)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 rayOrigin = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        return hit.collider != null && hit.collider.CompareTag(tag);
    }*/
}
