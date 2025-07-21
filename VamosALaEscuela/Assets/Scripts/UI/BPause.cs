using UnityEngine;
using UnityEngine.UI;

public class BPause : MonoBehaviour//version mobile
{
    private GameObject _panel;
    private GameObject _bResume;

    private Button _bPause;

    private Button _bContinuar;


    void Start()
    {
        _panel = transform.Find("Img").gameObject;
        _bResume = transform.Find("BContinue").gameObject;

        _bContinuar = _bResume.GetComponent<Button>();

        _bPause = GetComponent<Button>();

        ActivePanel(false);

        _bPause.onClick.AddListener(PauseGame);
        _bContinuar.onClick.AddListener(ResumeGame);
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

    private void ResumeGame()
    {
        if (PauseStatus.IsPaused())
        {
            PauseStatus.SetPaused(false);
        }
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
