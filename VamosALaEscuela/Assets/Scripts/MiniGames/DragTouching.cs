using UnityEngine;

public class DragTouching : MonoBehaviour//version mobile
{
    //para objetos arrastrables de los minijuegos
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;

        if (CinematicStatus.IsActiveCinematic()) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        // Detección con mouse para PC
        if (Input.GetMouseButtonDown(0))
        {
            if (TouchObjectStatus.TouchedThisObject(Input.mousePosition, gameObject))
            {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldMousePos.z = 0;
                offset = transform.position - worldMousePos;
                isDragging = true;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            LimitMove(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        // Detección con touch para mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (TouchObjectStatus.TouchedThisObject(touchPos, gameObject))
                    {
                        Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(touchPos);
                        worldTouchPos.z = 0;
                        offset = transform.position - worldTouchPos;
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        LimitMove(touchPos);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
#endif
    }
    void LimitMove(Vector3 inputPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(inputPos) + offset;
        worldPos.z = 0;

        // limita dentro del rango permitido
        float limitX = Mathf.Clamp(worldPos.x, -16f, 16f);
        float limitY = Mathf.Clamp(worldPos.y, -10f, 7.5f);

        transform.position = new Vector3(limitX, limitY, 0);
    }

}
