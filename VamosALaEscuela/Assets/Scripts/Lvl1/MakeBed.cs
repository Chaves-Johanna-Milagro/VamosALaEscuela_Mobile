using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class MakeBed : MonoBehaviour
{
    private GameObject _incomp; //cama si hacer
    private GameObject _comp;  // cama hecha

    private void Start()
    {
        _incomp = transform.Find("Incomplete").gameObject;
        _comp = transform.Find("Complete").gameObject;

        /*_incomp.SetActive(true);//activamos la incompleta
        _comp.SetActive(false);*/
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile())) // click o toque
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("cama hecha");
                _incomp.SetActive(false);
                _comp.SetActive(true);
            }
        }
    }
}
