using UnityEngine;

public class BedTuto : MonoBehaviour
{
    private GameObject _incomp; //cama si hacer
    private GameObject _comp;  // cama hecha

    private AudioSource _sound;

    private BNotesT _bNotes;
    private BKindnessT _bKind;

    private bool _maked = false; //pa controla que la toca una vez
    private void Start()
    {
        _incomp = transform.Find("Incomplete").gameObject;
        _comp = transform.Find("Complete").gameObject;

        _incomp.SetActive(true);//activamos la incompleta
        _comp.SetActive(false);

        _sound = GetComponent<AudioSource>();

        _bNotes = Object.FindFirstObjectByType<BNotesT>();
        _bKind = Object.FindFirstObjectByType<BKindnessT>();
    }

    public void Update()
    {
        if (_maked) return;

        if (Input.GetMouseButtonDown(0) && !(TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile())) // click o toque
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("cama hecha");
                _incomp.SetActive(false);
                _comp.SetActive(true);
                if (_sound != null) _sound.Play();

                _bNotes.ActiveCheckTuto();
                _bKind.UpBarKindTuto();

                _maked = true;
            }
        }
    }
}
