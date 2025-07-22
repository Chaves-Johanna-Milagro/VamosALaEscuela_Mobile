using System.Collections.Generic;
using UnityEngine;

public static class AftonDialogStatus
{
    private static Dictionary<string, AudioClip> _clips = new();
    private static Dictionary<string, AudioSource> _sources = new();
    private static GameObject _audioHost;

    static AftonDialogStatus()
    {
        // Cargar todos los clips de Resources/Audio
        AudioClip[] loaded = Resources.LoadAll<AudioClip>("Afton");
        foreach (var clip in loaded)
        {
            _clips[clip.name] = clip;
        }

        // Crear objeto host
        _audioHost = new GameObject("AudioManager_Host");
        Object.DontDestroyOnLoad(_audioHost);
    }

    public static void PlaySound(string name, float volume = 1f)
    {
        if (!_clips.ContainsKey(name))
        {
            Debug.LogWarning("AudioManager: Clip no encontrado -> " + name);
            return;
        }

        if (!_sources.ContainsKey(name))
        {
            // Crear un AudioSource nuevo para ese clip
            AudioSource source = _audioHost.AddComponent<AudioSource>();
            source.clip = _clips[name];
            source.playOnAwake = false;
            _sources[name] = source;
        }

        AudioSource targetSource = _sources[name];
        targetSource.volume = volume;
        targetSource.Play();
    }

    public static void StopSound(string name)
    {
        if (_sources.TryGetValue(name, out var source))
        {
            if (source.isPlaying)
                source.Stop();
        }
        else
        {
            Debug.LogWarning("AudioManager: No se encontró AudioSource para -> " + name);
        }
    }
    public static void StopAll()
    {
        foreach (var source in _sources.Values)
        {
            if (source.isPlaying)
                source.Stop();
        }
    }
}
