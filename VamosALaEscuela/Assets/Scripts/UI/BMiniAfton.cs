using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BMiniAfton : MonoBehaviour//version mobile
{
    private GameObject[] _childs;
    private int _count;

    private Button _button;

    private bool _isActive = false;

    private string _scene;

    private TextMeshProUGUI _textComp;

    private string[] _randomTextNvl1 = new string[]
    {
        "QUIEN NO AMA UNA CAMA BIEN HECHA",
        "UNA SONRISA LIMPIA ES TU MEJOR ARMADURA",
        "HORA DE VESTIRSE",
        "ORDENAR TU ESPACIO TAMBIEN ORDENA TU CABEZA",
        "UN CUARTO LIMPIO DICE ESTOY LISTO PARA TODO",
        "TODO HEROE EMPIEZA SU DIA CON BUENOS HABITOS",
        "CAMA ORDENADA MENTE ENFOCADA",
        "HASTA LOS DIENTES QUIEREN LLEGAR PROLIJOS A CLASE"
    };

    private string[] _randomTextNvl2 = new string[]
    {
        "NO OLVIDES NADA",
        "DESAYUNAR ES COMO CARGAR COMBUSTIBLE PARA TU CEREBRO",
        "UNA MOCHILA ORDENADA ES UN SUPERPODER ESCOLAR",
        "YA PUSISTE TU CUADERNO NO LO OLVIDES",
        "TODO SABE MEJOR CUANDO ESTAS PREPARADO",
        "NO TE VAYAS SIN DESPEDIRTE",
        "SI TE VAS SIN DESPEDIRTE ALGUIEN TE VA A EXTRAÑAR"
    };

    private string[] _randomGoodFeedback = new string[]
{
        "EXCELENTE TRABAJO",
        "LO HICISTE MUY BIEN",
        "SUPER",
        "GENIAL SIGUE ASI",
        "LO ESTÁS HACIENDO MUY BIEN",
        "MUY BUENA ELECCION",
        "FANTASTICO",
        "BRAVO",
        "MUY BIEN HECHO",
        "INCREIBLE",
        "ME IMPRESIONAS",
        "EXCELENTE ELECCION"
};

    private string[] _randomBadFeedback = new string[]
    {
        "NO PASA NADA INTENTALO OTRA VEZ",
        "CASI SIGUE PROBANDO",
        "MUY CERCA TU PUEDES LOGRARLO",
        "LO IMPORTANTE ES SEGUIR INTENTANDO",
        "POCO A POCO LO VAS A CONSEGUIR",
        "LOS ERRORES NOS AYUDAN A MEJORAR",
        "PRACTICANDO SE APRENDE SIGUE ADELANTE",
        "AUNQUE TE EQUIVOQUES ESTAS APRENDIENDO"
    };

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

        _textComp = _childs[1].GetComponent<TextMeshProUGUI>();

        _scene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
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

        if (_isActive)
        {
            ShowByScene(); // Solo muestra consejo si se activó
        }
    }

    private void ShowByScene()
    {
        if (_scene == "Level1") ConcejosNvl1();
        if (_scene == "Level2") ConcejosNvl2();
    }

    private void Active(bool active)
    {
        for (int i = 0; i < _count; i++)
        {
            _childs[i].SetActive(active);
        }

        // Si se desactivó, detener el sonido actual
        if (!active)
        {
            AftonDialogStatus.StopAll(); // O StopLastSound() si lo preferís
        }
    }

    private void ConcejosNvl1()
    {
        if (_randomTextNvl1.Length > 0)  // Selecciona un concejo aleatorio
        {
            string randomLine = _randomTextNvl1[Random.Range(0, _randomTextNvl1.Length)];
            _textComp.text = "¡" + randomLine + "!";
            AftonDialogStatus.PlaySound(randomLine);//pa que suene el audio que corresponda
        }
    }

    private void ConcejosNvl2()
    {
        if (_randomTextNvl2.Length > 0)  // Selecciona un concejo aleatorio
        {
            string randomLine = _randomTextNvl2[Random.Range(0, _randomTextNvl2.Length)];
            _textComp.text = "¡" + randomLine + "!";
            AftonDialogStatus.PlaySound(randomLine);
        }
    }

    public void GoodFeedback() //metodo para cuando en jugador complete o realize buenas acciones
    {
        _isActive = true;

        Active(_isActive); //activar el globo y texto

        if (_randomGoodFeedback.Length > 0)  // Selecciona un felicitaciones aleatorias
        {
            string randomLine = _randomGoodFeedback[Random.Range(0, _randomGoodFeedback.Length)];
            _textComp.text = "¡" + randomLine + "!";
            AftonDialogStatus.PlaySound(randomLine);
        }
        StartCoroutine(Delay());
    }

    public void BadFeedback() //metodo para cuando en jugador se equivoque
    {
        _isActive = true;

        Active(_isActive); //activar el globo y texto

        if (_randomBadFeedback.Length > 0)  // Selecciona una motivacion aleatoria
        {
            string randomLine = _randomBadFeedback[Random.Range(0, _randomBadFeedback.Length)];
            _textComp.text = "¡" + randomLine + "!";
            AftonDialogStatus.PlaySound(randomLine);
        }
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);

        _isActive = false;
        Active(_isActive);
    }
}
