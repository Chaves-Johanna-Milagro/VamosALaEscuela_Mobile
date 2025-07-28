using System.Collections;
using UnityEngine;

public class MGBathroom : MonoBehaviour//version mobile
{
    private GameObject _back;

    private GameObject _mouth;
    private GameObject _mouthD;
    private GameObject _mouthC;

    private GameObject _brush;

    private bool _isCompleted = false;

    private BNotes _notes;
    void Start()
    {
        _back = transform.Find("Background").gameObject;

        _mouth = transform.Find("Mouth").gameObject;
        _mouthD = transform.Find("MouthDirty").gameObject;
        _mouthC = transform.Find("MouthClean").gameObject;

        _brush = transform.Find("CEPILLO").gameObject;

        _notes = Object.FindFirstObjectByType<BNotes>();

        if (MiniGameStatus.HasState(gameObject))
        {
            MiniGameStatus.LoadState(gameObject);
            _isCompleted = true;
            Debug.Log("restaurando estado");
        }
    }


    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_notes.IsActiveCheck3()) return;

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
                _back.SetActive(true);
                _mouth.SetActive(true);
                _mouthD.SetActive(true);
                _brush.SetActive(true);
                
            }
        }
    }

    public IEnumerator Complete()
    {
        //_isCompleted = true;
        _notes.ActiveCheck3();//activamos el check

        _mouthC.SetActive(true);
        _mouthD.SetActive(false);
        yield return new WaitForSeconds(2f);



        int count = transform.childCount;
        GameObject[] child = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
            child[i].SetActive(false);
        }

        MiniGameStatus.SaveState(gameObject);
    }
}
