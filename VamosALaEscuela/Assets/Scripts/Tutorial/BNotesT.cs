using UnityEngine;
using UnityEngine.UI;

public class BNotesT : MonoBehaviour //pa la libreta del tutorial
{
    private GameObject[] _childs;
    private int _count;

    private bool _isActive = false;

    private Button _bNotes;

    private GameObject _check1; // pa mostra q se marca un check al cumplir on objetivo

    //private GuiaAftonT _gAftonT;
    void Start()
    {
        _count = transform.childCount;
        _childs = new GameObject[_count];

        for (int i = 0; i < _count; i++) // desativar al inicio
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }

        _bNotes = GetComponent<Button>();
        _bNotes.onClick.AddListener(Toggle);

        Transform childCheck = transform.Find("Check1");
        _check1 = childCheck.transform.Find("Image").gameObject;
        _check1.SetActive(false);

        //_gAftonT = Object.FindFirstObjectByType<GuiaAftonT>();
    }
    private void Update()
    {
        /*if (_gAftonT.IsActiveGuia() && _isActive)
        {
            _isActive = false;
            StopSound("ButtonNotes");
            Objts(false);
        }*/
    }
    private void Toggle()
    {
        _isActive = !_isActive;

        if (_isActive) PlaySound("ButtonNotes");
        if (_isActive == false) StopSound("ButtonNotes");
        Objts(_isActive);
    }

    private void Objts(bool active)
    {
        for (int i = 0; i < _count; i++) // desativar al clikear
        {
            _childs[i].SetActive(active);
        }
    }
    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }
    public void StopSound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Stop();
        }
    }


    public void ActiveCheckTuto()
    {
        if (_check1 != null) _check1.SetActive(true);
    }
}
