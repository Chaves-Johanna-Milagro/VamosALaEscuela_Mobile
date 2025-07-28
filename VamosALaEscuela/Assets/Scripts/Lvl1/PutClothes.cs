using UnityEngine;
using System.Collections;

public class PutClothes : MonoBehaviour//version mobile
{
    private GameObject _cRP;
    private GameObject _cPJ;

    private BNotes _notes;
    private BKindness _kind;

    void Start()
    {
        _cRP = transform.Find("CinematicRP").gameObject;
        _cPJ = transform.Find("CinematicPJ").gameObject;

        if (CinematicStatus.HasState(gameObject))
        {
            CinematicStatus.LoadState(gameObject);
            Debug.Log("restaurando estado");
        }
        _notes = Object.FindFirstObjectByType<BNotes>();
        _kind = Object.FindFirstObjectByType<BKindness>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_notes.IsActiveCheck2()) return;

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
                StartCoroutine(DelayCinematic());
            }
        }
    }

    private IEnumerator DelayCinematic()
    {
        PlaySound("ropa");
        _cRP.SetActive(true);
        Debug.Log("ropa puesta");
        yield return new WaitForSeconds(2f);
        _cRP.SetActive(false);
        StopSound();

        _notes.ActiveCheck2();//activamos el check
        _kind.GoodDecision();//subimos la barrita

        CinematicStatus.SaveState(gameObject);
        Debug.Log("Estado guardado");

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
