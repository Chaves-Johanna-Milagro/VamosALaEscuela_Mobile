using UnityEngine;

public class TouchScaling : MonoBehaviour //version mobile
{
    private float _scaleMultiplier = 1.1f;
    private float _scaleSpeed = 5f;

    private Vector3 _originalScale;
    private Vector3 _targetScale;

    private bool _isScaling = false;

    private bool _isPlaying = false; //pa que no estre en bucle el sonido
    private void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale * _scaleMultiplier;

    }

    private void Update()
    {
        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;

        if (CinematicStatus.IsActiveCinematic()) return;

        if (MiniGameStatus.IsActiveMiniGame()) return;

        _isScaling = false;

        // Detectar toque o clic válido
        bool touched = Input.GetMouseButton(0) ||
                       (Input.touchCount > 0 &&
                       Input.GetTouch(0).phase == TouchPhase.Began); // || //began para el primer toque
                       // Input.GetTouch(0).phase == TouchPhase.Stationary)); //stationary cuando se lo mantiene tocado

        if (touched)
        {

            Vector2 screenPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            if (TouchObjectStatus.TouchedThisObject(screenPos, gameObject))
            {
                
                if (!_isPlaying)
                {
                    PlaySound("MenuButton");
                    _isPlaying = true;
                }

                _isScaling = true;
            }
        }
        else if (!touched) _isPlaying = false;  

        // Escalado suave
        Vector3 targetScaling = _isScaling ? _targetScale : _originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScaling, Time.deltaTime * _scaleSpeed);
    }

    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }
}
