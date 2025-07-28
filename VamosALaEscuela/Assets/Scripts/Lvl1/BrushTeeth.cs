using UnityEngine;

public class BrushTeeth : MonoBehaviour//version mobile
{
    //script manejado por Mouth en el mj de cepillarse los dientes
    private float _timer = 0f;
    private float _requiredTime = 2f;

    private bool _isTouching = false;
    private bool _completed = false;

    private MGBathroom _mg;

    private AudioSource _sound;
    void Start()
    {
        _mg = GetComponentInParent<MGBathroom>();
        _sound = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_completed || !_isTouching) return;

        _timer += Time.deltaTime;

        if (_timer >= _requiredTime)
        {
            StartCoroutine(_mg.Complete());
            _completed = true;
            Debug.Log("dientes lavados");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_completed) return;

        if (collision.gameObject.name == "CEPILLO")
        {
            _sound?.Play();
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_completed) return;

        if (other.gameObject.name == "CEPILLO")
        {
            _isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "CEPILLO")
        {
            _isTouching = false;
            _timer = 0f;
            _sound.Stop();
        }
    }
}
