using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BMiniAftonT : MonoBehaviour // pa el boton mini afton del tutorial
{
    private GameObject[] _childs;
    private int _count;

    private bool _isActive = false;

    private Button _bNotes;

    private TextMeshProUGUI _textComp;

    private GuiaAftonT _gAftonT; 
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

        _textComp = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _textComp.text = "TE DARE UNOS CONSEJOS CUANDO QUIERAS";

        _gAftonT = Object.FindFirstObjectByType<GuiaAftonT>();
    }
    private void Update()
    {
        if (_gAftonT.IsActiveGuia() && _isActive)
        {
            _isActive = false;
            StopSound("Afton");
            Objts(false);
        }
    }
    private void Toggle()
    {
        _isActive = !_isActive;

        if (_isActive) PlaySound("Afton");
        if (_isActive == false) StopSound("Afton");
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
}
