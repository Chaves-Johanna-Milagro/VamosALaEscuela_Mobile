using UnityEngine;

public class SaveUseful : MonoBehaviour//version mobile
{
    //lo tiene backpack del minijuego de la mochila

    private string[] _incorrects = new string[] { "CEPILLO", "MEDIA", "JOYSTICK" };

    private bool _isTouching = false;
    private bool _completed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.IsPaused() || _completed || !_isTouching) return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < _incorrects.Length; i++)
        {
            if (other.gameObject.name != _incorrects[i])
            {
                other.gameObject.SetActive(false);
                Debug.Log("Util guardado");
            }
            if (other.gameObject.name == _incorrects[i])
            {
                //other.gameObject.SetActive(false);
                Debug.Log("Util no valido");
            }
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
}
