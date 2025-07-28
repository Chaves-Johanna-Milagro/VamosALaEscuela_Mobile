using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorRoom : MonoBehaviour
{
    private string _room;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _room = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;

        if (CinematicStatus.IsActiveCinematic()) return;

        if (MiniGameStatus.IsActiveMiniGame()) return;

        bool touched = Input.GetMouseButtonDown(0) ||
                       (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        if (touched)
        {
            Vector2 screenPoint = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            if (TouchObjectStatus.TouchedThisObject(screenPoint, gameObject))
            {
                PlaySound("puerta");
                if(_room == "Level1VM") SceneManager.LoadScene("Level2VM");
                if(_room == "Level2VM") SceneManager.LoadScene("Level1VM");
            }
        }
    }
    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }

    public void StopSound()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound != null) sound.Stop();
        }
    }
}
