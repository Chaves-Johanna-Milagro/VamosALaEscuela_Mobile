using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Button _bPlay;
    private Button _bExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bPlay = transform.Find("BPlay").GetComponent<Button>();
        _bExit = transform.Find("BExit").GetComponent<Button>();

        _bPlay.onClick.AddListener(StartGame);
        _bExit.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        Debug.Log("Iniciando el juego...");
        SceneManager.LoadScene("SetUpGame");
    }
    private void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); //cierra el ejecutable
    }
}
