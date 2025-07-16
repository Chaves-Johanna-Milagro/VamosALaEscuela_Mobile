using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 20f;

    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        // TOUCH en móvil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = transform.position.z; // mantener la profundidad del personaje
                _targetPosition = touchPos;
                _isMoving = true;
            }
        }

        //  Click en PC para testear en el editor
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            _targetPosition = mousePos;
            _isMoving = true;
        }

        // Movimiento suave hacia el target
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

            // Parar si llegó
            if (Vector3.Distance(transform.position, _targetPosition) < 0.05f)
            {
                _isMoving = false;
            }
        }
    }
}
