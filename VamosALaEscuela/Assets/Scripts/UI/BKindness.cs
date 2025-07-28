using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BKindness : MonoBehaviour//version mobile
{
    private GameObject[] _childs;
    private int _count;

    private Button _button;

    private bool _isActive = false;

    private RectTransform _now;
    private float _maxY = -240;
    private float _minY = -790;
    private float _amount = 20f;

    void Start()
    {
        _count = transform.childCount;
        _childs = new GameObject[_count];

        for (int i = 0; i < _count; i++)
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }

        _button = GetComponent<Button>();

        _button.onClick.AddListener(Toggle);

        _now = transform.Find("Now").GetComponent<RectTransform>();
        
        float savedY = Mathf.Clamp(KindnessStatus.GetNowBarY(), _minY, _maxY); // Cargar la posicion guardada
        Vector2 newPos = _now.anchoredPosition;
        newPos.y = savedY;
        _now.anchoredPosition = newPos;
    }

    void Update()
    {
        if (PauseStatus.IsPaused() && _isActive)
        {
            _isActive = false;
            Active(false);
        }
    }

    private void Toggle()
    {
        _isActive = !_isActive;

        Active(_isActive);
        if (_isActive) PlaySound("ButtonKindness");
        if (!_isActive) StopSound();
    }

    private void Active(bool active)
    {
        for (int i = 0; i < _count; i++)
        {
            _childs[i].SetActive(active);
        }
    }
    public void GoodDecision()
    {
        MoveBar(_amount);
    }

    public void BadDevision()
    {
        MoveBar(-_amount);
    }

    private void MoveBar(float deltaY)
    {
        Vector2 newPos = _now.anchoredPosition + new Vector2(0f, deltaY);
        newPos.y = Mathf.Clamp(newPos.y, _minY, _maxY);
        _now.anchoredPosition = newPos;

        KindnessStatus.SetNowBarY(newPos.y); // Guardar la nueva posición

        // Si llega al límite inferior, cargar GameOver
        /*if (Mathf.Approximately(newPos.y, _minY))
        {
            GameOverStatus.MotiveDownBar();
            Debug.Log("GameOver...");
            SceneManager.LoadScene("GameOver");
        }*/
    }

    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }

    public void StopSound()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound != null) sound.Stop();
        }
    }
}
