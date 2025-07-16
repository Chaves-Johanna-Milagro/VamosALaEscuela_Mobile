using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaling : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _scaleMultiplier = 1.2f;
    private float _scaleSpeed = 5f;

    private RectTransform _rectTransform;

    private Vector3 _originalScale;

    private bool _isScaling = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalScale = _rectTransform.localScale;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isScaling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isScaling = false;
    }

    private void Update()
    {
        Vector3 desiredScale = _isScaling ? _originalScale * _scaleMultiplier : _originalScale;
        _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, desiredScale, Time.deltaTime * _scaleSpeed);
    }
}
