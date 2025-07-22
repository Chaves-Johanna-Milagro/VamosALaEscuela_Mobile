using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BNotes : MonoBehaviour//version mobile
{
    private GameObject _panel;
    private GameObject _text;
    private GameObject _checks;
    private GameObject _oLevel;

    private Button _bNotes;

    private string _scene;

    private bool _isActive = false;

  /*  private bool _isCheck1 = false;
    private bool _isCheck2 = false;
    private bool _isCheck3 = false;*/
    void Start()
    {
        _panel = transform.Find("Img").gameObject;
        _text = transform.Find("Text").gameObject;
        _checks = transform.Find("Checks").gameObject;

        _bNotes = GetComponent<Button>();

        _scene = SceneManager.GetActiveScene().name;
        Active(false);

        _bNotes.onClick.AddListener(Toggle);

        if (CheckStatus.HasState(_checks))
        {
            CheckStatus.LoadState(_checks);
            //_isTouched = true;
            Debug.Log("restaurando estado");
            Active(false);
        }
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
    }

    private void Active(bool activated)
    {
        _panel.SetActive(activated);
        _text.SetActive(activated);
        _checks.SetActive(activated);

        if (_scene == "Level1")
        {
            _oLevel = transform.Find("Lvl1").gameObject;
            _oLevel.SetActive(activated);
        }
    }


    public void ActiveCheck1()
    {
        GameObject check = _checks.transform.Find("Obj1").gameObject;
        check.SetActive(true);
        CheckStatus.SaveState(_checks);
        Debug.Log("Check 1 activado");
    }

    public void ActiveCheck2()
    {
        GameObject check = _checks.transform.Find("Obj2").gameObject;
        check.SetActive(true);
        CheckStatus.SaveState(_checks);
        Debug.Log("Check 2 activado");
    }

    public void ActiveCheck3()
    {
        GameObject check = _checks.transform.Find("Obj3").gameObject;
        check.SetActive(true);
        CheckStatus.SaveState(_checks);
        Debug.Log("Check 3 activado");
    }

}
