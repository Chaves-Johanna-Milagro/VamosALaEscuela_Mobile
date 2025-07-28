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

        UpdateVisualChecks();
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
        // Actualizar visibilidad de los checks según su estado
        UpdateVisualChecks();
    }

    private void UpdateVisualChecks()
    {
        bool[] current = CheckStatus.GetChecksForScene(_scene);

        GameObject check1 = _checks.transform.Find("Obj1")?.gameObject;
        GameObject check2 = _checks.transform.Find("Obj2")?.gameObject;
        GameObject check3 = _checks.transform.Find("Obj3")?.gameObject;

        if (check1 != null) check1.SetActive(current[0]);
        if (check2 != null) check2.SetActive(current[1]);
        if (check3 != null) check3.SetActive(current[2]);
    }

    public void ActiveCheck1()
    {
        CheckStatus.SetCheckActive(_scene, 0);
        UpdateVisualChecks();
        Debug.Log("Check 1 activado");
    }

    public void ActiveCheck2()
    {
        CheckStatus.SetCheckActive(_scene, 1);
        UpdateVisualChecks();
        Debug.Log("Check 2 activado");
    }

    public void ActiveCheck3()
    {
        CheckStatus.SetCheckActive(_scene, 2);
        UpdateVisualChecks();
        Debug.Log("Check 3 activado");
    }

    public bool IsActiveCheck1()
    {
        return CheckStatus.IsCheckActive(_scene, 0);
    }

    public bool IsActiveCheck2()
    {
        return CheckStatus.IsCheckActive(_scene, 1);
    }

    public bool IsActiveCheck3()
    {
        return CheckStatus.IsCheckActive(_scene, 2);
    }
}
