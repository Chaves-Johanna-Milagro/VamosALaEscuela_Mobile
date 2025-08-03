using System.Collections;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    //script manejado por Mouth en el minijuego del desayuno

    private float _timer = 0f;
    private float _requiredTime = 1.2f;

    private bool _isTouching = false;
    private bool _completed = false;

    private MGBreakfast _mg;

    private string[] _food= new string[] { "PANES", "GALLETAS", "SANWIS" };
    private string[] _drink = new string[] { "TE", "MATE", "BATIDO" };

    private Vector3 _posDrink;

    private bool _wasEating = false; 
    private bool _wasTaking = false; 


    void Start()
    {
        _mg = GetComponentInParent<MGBreakfast>();

        for (int i = 0; i < _drink.Length; i++)//setea la posicion inicial de las bebidas
        {
            _posDrink = transform.parent.gameObject.transform.Find(_drink[i]).position;
        }
    }

    void Update()
    {
        if (PauseStatus.IsPaused() || _completed || !_isTouching) return;

        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_completed) return;

        string name = other.gameObject.name;
        if (IsFood(name) || IsDrink(name))
        {
            _isTouching = true;
            _timer = 0f;

            SetMouthState("open");
        }
        if (name == "SERVILLETA")
        {
            _isTouching = true;
            _timer = 0f;

            SetMouthState("close");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_completed) return;

        string name = other.gameObject.name;

        if (_timer >= _requiredTime)
        {
            Debug.Log("comiendose");

            if (IsFood(name))
            {
                StartCoroutine(EatMouth(other.gameObject));
                Debug.Log("alimento comido");
            }

            if (IsDrink(name))
            {
                StartCoroutine(TakeMouth(other.gameObject));
                Debug.Log("bebida tomada");
            }

            if (name == "SERVILLETA" && _wasEating && _wasTaking)
            {
                StartCoroutine(ClearMouth(other.gameObject));
                Debug.Log("boca limpiada");
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string name = other.gameObject.name;
        if ((IsFood(name) || IsDrink(name) || name == "SERVILLETA") && _timer < _requiredTime)
        {
            _isTouching = false;
            _timer = 0f;

            SetMouthState("default");
        }
    }

    private IEnumerator EatMouth(GameObject other)
    {
        PlaySound("eatingSound");
        other.SetActive(false);
        SetMouthState("close");
        yield return new WaitForSeconds(1f);
        SetMouthState("default");
        _wasEating = true;
    }

    private IEnumerator TakeMouth(GameObject other)
    {
        PlaySound("sorbo_taza");
        other.transform.position = _posDrink;
        if(other.gameObject.name == "BATIDO")
        {
            GameObject bat = transform.parent.transform.Find("BATIDO1").gameObject;
            bat.SetActive(true);
            other.SetActive(false);
        }
        SetMouthState("close");
        yield return new WaitForSeconds(1f);
        SetMouthState("default");
        _wasTaking = true;
    }
    private IEnumerator ClearMouth(GameObject other)
    {
        SetMouthState("close");
        yield return new WaitForSeconds(1f);
        SetMouthState("default");
        _completed = true;

        StartCoroutine(_mg.Complete());
    }

    private void SetMouthState(string state)
    {
        _mg.MDefault().SetActive(state == "default");
        _mg.MOpen().SetActive(state == "open");
        _mg.MClose().SetActive(state == "close");
    }

    private bool IsFood(string name)
    {
        foreach (var f in _food)
        {
            if (name == f) return true;
        }
        return false;
    }

    private bool IsDrink(string name)
    {
        foreach (var d in _drink)
        {
            if (name == d) return true;
        }
        return false;
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
