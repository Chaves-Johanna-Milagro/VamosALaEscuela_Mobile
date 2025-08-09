using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MGBackpack : MonoBehaviour
{
    private GameObject[] _childs;
    private int _count;

    private BNotes _notes;
    private BKindness _kind;

    private GAfton _gAfton;

    private bool _completed = false;
    
    void Start()
    {
        _count = transform.childCount;
        _childs = new GameObject[_count];

        for (int i = 0; i < _count; i++)
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }

        _notes = Object.FindFirstObjectByType<BNotes>();
        _kind = Object.FindFirstObjectByType<BKindness>();

        _gAfton = Object.FindFirstObjectByType<GAfton>();

        if (MiniGameStatus.HasState(gameObject))
        {
            MiniGameStatus.LoadState(gameObject);
            Debug.Log("restaurando estado");
        }
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
                for (int i = 0; i < _count; i++)
                {
                    _childs[i].SetActive(true);
                }

                PlaySound("BackpackOpening");

                _gAfton.GBreakfast();//active las indicaciones del mg

            }
        }

    }
    public IEnumerator Complete()
    {
        if (_completed) yield break;
        _completed = true;

        _notes.ActiveCheck2();//activamos el check
        _kind.GoodDecision();//subimos la barrita

        yield return new WaitForSeconds(1.5f);

        int count = transform.childCount;
        GameObject[] child = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
            child[i].SetActive(false);
        }

        MiniGameStatus.SaveState(gameObject);
    }
    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }
}
