using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuiaAftonT : MonoBehaviour
{
    private GameObject _img; //pa la pantalla oscura

    private GameObject _imgAfton; //pa el afton
    private GameObject _imgGlobo; //pa el globo
    private GameObject _imgGText; //pa el texto del globo

    private TextMeshProUGUI _gText;

    private string[] _guiasText = new string[]
    {
        "HOLA, SOY AFTON Y TE VOY A GUIAR EN ESTA AVENTURA!",//audio tuto6

        "PARA MOVERTE TOCA A DONDE QUIERAS IR!",  //audio tuto1
        "PARA INTERACTUAR CON ALGO TOCA EL OBJETO!",//audio tuto2

        "¡VEAMOS PARA QUE SIRVEN LOS BOTONES!",//audio tuto7

        "SI QUIERES DESCANZAR UN RATO PUEDES TOCAR AQUI",//audio tuto8
        "SI NO SABES QUÉ HACER ÉSTA LIBRETA TE AYUDARÁ!",//audio tuto3
        "ESTE MEDIDOR MOSTRARÁ QUE TAN BIEN TE COMPORTAS. COMPLETA LOS OBJETIVOS Y  HAZ BUENAS ACCIONES PARA QUE AUMENTE!",//audio tuto5
        "YO TAMBIEN ESTOY AQUI SI LO NECESITAS",//audio tuto4
        
        "¡ESO ES TODO, BUENA SUERTE!"//audio tuto9
    };

    private AudioSource[] _aGuias; //audios guia

    private bool _isGuiaActive = true; //pa que los botones desactiven sus hijos cuando se activen las explicaciones

    private int _countAnims = 0; // para ver en cual animacion debe estar
    void Start()
    {
        _img = transform.Find("Background").gameObject;

        _imgAfton = transform.Find("Afton").gameObject;
        _imgGlobo = transform.Find("Globo").gameObject;
        _imgGText = transform.Find("GText").gameObject;

        _gText = _imgGText.GetComponent<TextMeshProUGUI>();

        // Activar todos al inicio
        _img.SetActive(true);
        _imgAfton.SetActive(true);
        _imgGlobo.SetActive(true);
        _imgGText.SetActive(true);

        _gText.text = _guiasText[0];//pa mostra la presentacion de afton

        _aGuias = GetComponents<AudioSource>();// pa los audios

        StartCoroutine(DelayGuia());
    }

    private IEnumerator DelayGuia()
    {
        _aGuias[0].Play();//reproducir audio de presentacion
        yield return new WaitForSeconds(3f);

        for (int i = 1; i < 3; i++) // guia pal movimiento
        {
            _countAnims++; //pa setear el numero de animaciones 
            _isGuiaActive = true; //que se desactiven los hijos de los bototnes UI

            _img.SetActive(true); // Mostrar fondo
            _gText.text = _guiasText[i]; // Mostrar texto
            _aGuias[i].Play(); //reproducir audio

            yield return new WaitForSeconds(7f); // Tiempo de lectura

            _isGuiaActive = false; //que se puedan activer los hijos de los bototnes UI

            _img.SetActive(false); // Ocultar fondo unos segundos (como respiro)
            yield return new WaitForSeconds(4f); // Pausa entre frases
        }

        yield return new WaitForSeconds(2f);

        _countAnims++; //pa setear el numero de animaciones
        _isGuiaActive = true; //que se desactiven los hijos de los bototnes UI

        _img.SetActive(true);
        _gText.text = _guiasText[3];//pa mostra el mnsaje de como funcionan los botones
        _aGuias[3].Play(); //reproducir audio

        yield return new WaitForSeconds(3f);

        for (int i = 4; i < _guiasText.Length; i++) //continuar con la guia de los botones y terminar con la despedida
        {
            _countAnims++; //pa setear el numero de animaciones 
            _isGuiaActive = true; //que se desactiven los hijos de los bototnes UI

            _img.SetActive(true); // Mostrar fondo
            _gText.text = _guiasText[i]; // Mostrar texto
            _aGuias[i].Play(); //reproducir audio

            yield return new WaitForSeconds(7f); // Tiempo de lectura

            _isGuiaActive = false; //que se puedan activer los hijos de los bototnes UI

            _img.SetActive(false); // Ocultar fondo unos segundos (como respiro)
            yield return new WaitForSeconds(4f); // Pausa entre frases
        }

        // Al terminar la guía, cambiar de escena
        SceneManager.LoadScene("Level1VM");
    }

    public bool IsActiveGuia() { return _isGuiaActive; } //
    public int GetCurrentAnim() { return _countAnims; } //
}
