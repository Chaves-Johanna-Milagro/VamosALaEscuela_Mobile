using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 20f;

    private Vector3 _targetPos;
    private bool _isMoving = false;

    private void Start()
    {
        _targetPos = transform.position;
    }

    private void Update()
    {
        // TOUCH en móvil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved &&
            !ClickInUIStatus.IsPointerOverUI_Mobile())
            {
                if (!TouchedInteractiveObject(touch.position))
                {
                    SetTargetPos(touch.position);
                }
            }
        }

        //  Click en PC para testear en el editor
        if (Input.GetMouseButtonDown(0) && !ClickInUIStatus.IsPointerOverUI_PC())
        {
            if (!TouchedInteractiveObject(Input.mousePosition))
            {
                SetTargetPos(Input.mousePosition);
            }
        }

        // Movimiento suave hacia el target
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

            // Parar si llego
            if (Vector3.Distance(transform.position, _targetPos) < 0.05f) _isMoving = false;
         
        }
    }
    private void SetTargetPos(Vector3 screenPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = transform.position.z;

        // Limitar el movimiento
        worldPos.x = Mathf.Clamp(worldPos.x, -20f, 20f);
        worldPos.y = Mathf.Clamp(worldPos.y, -8f, 8f);

        _targetPos = worldPos;
        _isMoving = true;
    }

    //Verifica si se toco un obj interactivo
    private bool TouchedInteractiveObject(Vector3 screenPos)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 rayOrigin = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero); // Vector2.zero para un punto exacto

        if (hit.collider != null && hit.collider.CompareTag("InteractiveObj"))
        {
            return true; // No mover si se toco un obj interactivo
        }

        return false;
    }

    public Vector3 NextPos() {  return _targetPos;  }
    public bool IsMoving() {  return _isMoving;  }

}
