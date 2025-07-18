using UnityEngine;

public class TouchScaling : MonoBehaviour
{
    private float _scaleMultiplier = 1.1f;
    private float _scaleSpeed = 5f;

    private Vector3 _originalScale;
    private Vector3 _targetScale;

    private bool _isScaling = false;

    private AudioSource _hoverButton;

    private void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale * _scaleMultiplier;

        _hoverButton = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _isScaling = false;

        // Para PC / Editor
        if (Input.GetMouseButton(0) && !TouchInUIStatus.IsPointerOverUI_PC())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastCheck(ray);
        }

        // Para Mobile
        if (Input.touchCount > 0 && !TouchInUIStatus.IsPointerOverUI_Mobile())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastCheck(ray);
            }
        }

        // Escalado suave
        Vector3 targetScaling = _isScaling ? _targetScale : _originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScaling, Time.deltaTime * _scaleSpeed);
    }

    private void RaycastCheck(Ray ray)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!_isScaling && _hoverButton != null && !_hoverButton.isPlaying)
            {
                //_hoverButton.Play();
                _hoverButton.volume = 0.3f;
            }
            _isScaling = true;
        }
    }
}
