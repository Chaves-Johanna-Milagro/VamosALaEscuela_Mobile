using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GAfton : MonoBehaviour//version mobile
{
    private GameObject[] _childs;
    private int _count;

    private Button _bOK;

    private TextMeshProUGUI _textComp;

    void Start()
    {
        _count = transform.childCount;
        _childs = new GameObject[_count];

        for (int i = 0; i < _count; i++)
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }

        _textComp = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        _bOK = transform.Find("BOK").GetComponent<Button>();

        _bOK.onClick.AddListener(Desactive);
    }

    public void GBrushTeeth()
    {
        for (int i = 0; i < _count; i++)
        {
            _childs[i].SetActive(true);
            PlaySound("afton_tutorial_cepillo");
        }

        _textComp.text = "¡AGARRA Y ARRASTRA EL CEPILLO \nPARA LIMPIAR TUS DIENTES!";
    }
    private void Desactive()
    {
        for (int i = 0; i < _count; i++)
        {
            _childs[i].SetActive(false);
            StopSound();
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

    public void StopSound()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound != null) sound.Stop();
        }
    }
}
