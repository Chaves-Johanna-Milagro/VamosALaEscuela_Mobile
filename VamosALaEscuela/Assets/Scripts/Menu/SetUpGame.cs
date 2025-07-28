using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SetUpGame : MonoBehaviour
{
    private TMP_InputField _inputText; // pa que ponga su nombre

    private Button _lvlEasy; //botones para elegir la dificultad
    private Button _lvlMedium;
    private Button _lvlHard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputText = transform.Find("SetName").GetComponent<TMP_InputField>();

        _lvlEasy = transform.Find("BEasy").GetComponent<Button>();
        _lvlMedium = transform.Find("BMedium").GetComponent<Button>();
        _lvlHard = transform.Find("BHard").GetComponent<Button>();

        SetupLevelButtons();
    }

    public void SaveName()
    {
        string name = _inputText.text.ToUpper(); //pa convertirle en mayusculas

        PlayerNameStatus.SetName(name);

        Debug.Log("nombre elegido " + name);

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("IntroVM"); //la intro en version masculina
        SaveName();
    }

    void SetupLevelButtons()
    {
        _lvlEasy.onClick.AddListener(() => SetLevelAndStart("Facil")); // numeros del 1 al 10
        _lvlMedium.onClick.AddListener(() => SetLevelAndStart("Normal")); // numeros del 10 al 100
        _lvlHard.onClick.AddListener(() => SetLevelAndStart("Dificil"));  // numeros del 100 al 1000
    }

    void SetLevelAndStart(string level)
    {
        LevelGameStatus.SetLevel(level);
        Debug.Log("Nivel seleccionado: " + LevelGameStatus.GetLevel()); // Para verificar en consola
        PlayGame(); // Cargar la siguiente escena
    }
}
