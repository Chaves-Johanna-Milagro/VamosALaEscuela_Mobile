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
                SetTargetPos(touch.position);
            }
        }

        //  Click en PC para testear en el editor
        if (Input.GetMouseButtonDown(0) && !ClickInUIStatus.IsPointerOverUI_PC())
        {
            SetTargetPos(Input.mousePosition);
        }

        // Movimiento suave hacia el target
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

            // Parar si llegó o esta en la ui
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

    public Vector3 NextPos() {  return _targetPos;  }
    public bool IsMoving() {  return _isMoving;  }
}
