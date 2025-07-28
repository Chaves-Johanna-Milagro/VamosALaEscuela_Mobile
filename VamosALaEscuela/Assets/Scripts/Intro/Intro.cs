using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
    private GameObject _cImg1;
    private GameObject _cImg2;
    private GameObject _cImg3;

    private TMP_Text _cText1;
    private TMP_Text _cText2;
    private TMP_Text _cText3;

    private AudioSource _aImg1;
    private AudioSource _aImg2;
    private AudioSource _aImg3;

    void Start()
    {
        _cImg1 = transform.Find("Img1").gameObject;
        _cImg2 = transform.Find("Img2").gameObject;
        _cImg3 = transform.Find("Img3").gameObject;

        _cText1 = _cImg1.transform.Find("Text").GetComponent<TMP_Text>();
        _cText2 = _cImg2.transform.Find("Text").GetComponent<TMP_Text>();
        _cText3 = _cImg3.transform.Find("Text").GetComponent<TMP_Text>();

        _aImg1 = _cImg1.GetComponent<AudioSource>();
        _aImg2 = _cImg2.GetComponent<AudioSource>();
        _aImg3 = _cImg3.GetComponent<AudioSource>();

        string name = PlayerNameStatus.GetName();

        if (name == "" || string.IsNullOrEmpty(name)) _cText1.text = "AFTON:" + "\n   BUENOS DIAS FREDDY" + "! \n   YA ES HORA DE LEVANTARSE!!!";
        else _cText1.text = "AFTON:" + "\n   BUENOS DIAS " + PlayerNameStatus.GetName() + "! \n   YA ES HORA DE LEVANTARSE!!!";

        _cText2.text = "AFTON:" + "\n   VAMOS! ARRIBA! ARRIBA! " + "\n   QUE LLEGARAS TARDE";

        _cText3.text = "AFTON:" + "\n   HAY QUE PREPARARNOS PARA IR A LA ESCUELA" + "\n  ¿RECUERDAS QUE DEBES HACER EN LA" + "\n  MAÑANA?";

        StartCoroutine(PlayIntroSequence());
    }
    private IEnumerator PlayIntroSequence()
    {
        yield return StartCoroutine(FadeSequence(_cImg1, _aImg1));
        yield return StartCoroutine(FadeSequence(_cImg2, _aImg2));
        yield return StartCoroutine(FadeSequence(_cImg3, _aImg3));
        SceneManager.LoadScene("TutorialVM");
    }

    private IEnumerator FadeSequence(GameObject root, AudioSource audio)
    {
        float duration = 1f;
        float stayTime = 6f;

        root.SetActive(true);

        // Obtener todos los Image y TMP_Text del objeto y sus hijos
        List<Graphic> graphics = new List<Graphic>();
        graphics.AddRange(root.GetComponentsInChildren<Image>(true));
        graphics.AddRange(root.GetComponentsInChildren<TMP_Text>(true));

        // Guardar sus colores originales
        Dictionary<Graphic, Color> originalColors = new Dictionary<Graphic, Color>();
        foreach (Graphic g in graphics)
        {
            originalColors[g] = g.color;
            g.color = Color.black; // los dejamos negros para el inicio
        }

        // Fade in (negro a su color original)
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float lerp = t / duration;
            foreach (Graphic g in graphics)
                g.color = Color.Lerp(Color.black, originalColors[g], lerp);
            yield return null;
        }
        foreach (Graphic g in graphics)
            g.color = originalColors[g];

        // Reproducir sonido cuando ya se ve todo
        if (audio != null) audio.Play();

        yield return new WaitForSeconds(stayTime);

        // Detener sonido justo antes del fade out
        if (audio != null) audio.Stop();

        // Fade out (de su color original a negro)
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float lerp = t / duration;
            foreach (Graphic g in graphics)
                g.color = Color.Lerp(originalColors[g], Color.black, lerp);
            yield return null;
        }
        foreach (Graphic g in graphics)
            g.color = Color.black;

        root.SetActive(false);
    }
}
