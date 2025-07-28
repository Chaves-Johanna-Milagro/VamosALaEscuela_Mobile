using UnityEngine;
using System.Collections;

public class PutClothes : MonoBehaviour//version mobile
{
    private GameObject _cRP;
    private GameObject _cPJ;

    private bool _isTouched = false;

    private BNotes _notes;

    void Start()
    {
        _cRP = transform.Find("CinematicRP").gameObject;
        _cPJ = transform.Find("CinematicPJ").gameObject;

        if (CinematicStatus.HasState(gameObject))
        {
            CinematicStatus.LoadState(gameObject);
            _isTouched = true;
            Debug.Log("restaurando estado");
        }
        _notes = Object.FindFirstObjectByType<BNotes>();
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
        _isTouched = true;

        _cRP.SetActive(true);
        Debug.Log("ropa puesta");
        yield return new WaitForSeconds(2f);
        _cRP.SetActive(false);

        _notes.ActiveCheck2();//activamos el check

        CinematicStatus.SaveState(gameObject);
        Debug.Log("Estado guardado");

    }
}
