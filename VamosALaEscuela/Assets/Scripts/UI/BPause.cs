using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BPause : MonoBehaviour//version mobile
{
    private GameObject _panel;

    private Button _bPause;

    private Button _bContinuar;
    private Button _bMenu;
    private Button _bSalir;


    void Start()
    {
        _panel = transform.Find("Img").gameObject;

        _bContinuar = transform.Find("BContinue").GetComponent<Button>();
        _bMenu = transform.Find("BMenu").GetComponent<Button>();
        _bSalir = transform.Find("BExit").GetComponent<Button>();

        _bPause = GetComponent<Button>();

        ActivePanel(false);

        _bPause.onClick.AddListener(PauseGame);
        _bContinuar.onClick.AddListener(ResumeGame);
        _bMenu.onClick.AddListener(ReturnMenu);
        _bSalir.onClick.AddListener(ExitGame);
    }
    private void Update()
    {
        bool isPaused = PauseStatus.IsPaused();

        ActivePanel(isPaused);
    }
    private void PauseGame()
    {
        if (!PauseStatus.IsPaused())
        {
            PauseStatus.SetPaused(true);
        }
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");

        PlayerNameStatus.ResetName(); //resetiar todo
        LevelGameStatus.ResetLevel();
        PauseStatus.ResetPause();


        Debug.Log("volviendo al menu");
    }
    private void ResumeGame()
    {
        if (PauseStatus.IsPaused())
        {
            PauseStatus.SetPaused(false);
        }
    }
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); //cierra el ejecutable
    }
    private void ActivePanel(bool active)
    {
        int count = transform.childCount;
        GameObject[] child = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
            child[i].SetActive(active);
        }
    }
}
