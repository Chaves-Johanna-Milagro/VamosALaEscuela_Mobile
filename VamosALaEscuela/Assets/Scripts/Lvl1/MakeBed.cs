using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MakeBed : MonoBehaviour//version mobile
{
    private GameObject _incomp; //cama si hacer
    private GameObject _comp;  // cama hecha

    private GameObject _cPJ;  // cinematica con pijama
    private GameObject _cRP;  // cinematica cambiado

    private BNotes _notes;
    private BKindness _kind;

    private void Start()
    {
        _incomp = transform.Find("Incomplete").gameObject;
        _comp = transform.Find("Complete").gameObject;

        _cPJ = transform.Find("CinematicPJ").gameObject;
        _cRP = transform.Find("CinematicRP").gameObject;

        /*_incomp.SetActive(true);//activamos la incompleta
        _comp.SetActive(false);*/

        if (CinematicStatus.HasState(gameObject))
        {
            CinematicStatus.LoadState(gameObject);
            Debug.Log("restaurando estado");
        }

        _notes = Object.FindFirstObjectByType<BNotes>();
        _kind = Object.FindFirstObjectByType<BKindness>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_notes.IsActiveCheck1()) return;

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
        bool useRP = CheckStatus.IsCheckActive("Level1VM", 1);

        if(useRP)_cRP.SetActive(true);
        else _cPJ.SetActive(true);

        Debug.Log("cama hecha");
        _incomp.SetActive(false);
        _comp.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (useRP) _cRP.SetActive(false);
        else _cPJ.SetActive(false);

        _notes.ActiveCheck1();//activamos el check
        _kind.GoodDecision();//subimos la barrita

        CinematicStatus.SaveState(gameObject);
        Debug.Log("Estado guardado");

    }
}
