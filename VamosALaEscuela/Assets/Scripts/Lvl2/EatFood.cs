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

    void Start()
    {
        _mg = GetComponentInParent<MGBreakfast>();
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
        if (IsFood(name))
        {
            _isTouching = true;
            _timer = 0f;

            SetMouthState("open");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_completed) return;

        string name = other.gameObject.name;
        if (IsFood(name) && _timer >= _requiredTime)
        {
            Debug.Log("comiendose");
            _completed = true;
            Debug.Log("alimento comido");
            StartCoroutine(ChangeMouth(other.gameObject));

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string name = other.gameObject.name;
        if (IsFood(name) && _timer < _requiredTime)
        {
            _isTouching = false;
            _timer = 0f;

            SetMouthState("default");
        }
    }

    private IEnumerator ChangeMouth(GameObject other)
    {
        other.SetActive(false);
        SetMouthState("close");
        yield return new WaitForSeconds(1f);
        SetMouthState("default");
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
