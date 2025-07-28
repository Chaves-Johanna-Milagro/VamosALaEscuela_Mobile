using UnityEngine;

public static class KindnessStatus//version mobile
{
    private static float _nowBarY = -520f; // valor inicial (centro)

    public static float GetNowBarY()
    {
        return _nowBarY;
    }

    public static void SetNowBarY(float y)
    {
        _nowBarY = y;
    }


    public static float GetKindnessPercent()
    {
        // Mapear la Y entre el rango [-720, -270] como 0 a 1
        float normalized = Mathf.InverseLerp(-720f, -270f, _nowBarY);
        return normalized; // 0 = malas acciones 1 = buenas acciones
    }

    // Resetea la posición de la barra a su valor inicial
    public static void ResetKindness()
    {
        _nowBarY = -520f;
    }
}
