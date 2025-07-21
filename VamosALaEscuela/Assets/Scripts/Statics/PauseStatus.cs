using UnityEngine;

public static class PauseStatus//version mobile
{
    private static bool _isPaused = false;

    public static void SetPaused(bool pause)
    {
        _isPaused = pause;
        Time.timeScale = pause ? 0f : 1f;
    }

    public static void ResetPause()
    {
        _isPaused = false;
        Time.timeScale = 1f; // Asegura que el tiempo vuelve a correr
    }
    public static bool IsPaused() { return _isPaused; }
}
