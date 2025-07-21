using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class MakeBed : MonoBehaviour//version mobile
{
    private GameObject _incomp; //cama si hacer
    private GameObject _comp;  // cama hecha

    private GameObject _cPJ;  // cinematica con pijama
    private GameObject _cRP;  // cinematica cambiado

    private bool _isCliked = false;

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
            _isCliked=true;
            Debug.Log("restaurando estado");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isCliked) return;

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
        _isCliked = true;

        _cPJ.SetActive(true);
        Debug.Log("cama hecha");
        _incomp.SetActive(false);
        _comp.SetActive(true);
        yield return new WaitForSeconds(2f);
        _cPJ.SetActive(false);

        CinematicStatus.SaveState(gameObject);
        Debug.Log("Estado guardado");

    }
}
