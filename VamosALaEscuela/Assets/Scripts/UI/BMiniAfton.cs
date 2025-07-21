using UnityEngine;
using UnityEngine.UI;

public class BMiniAfton : MonoBehaviour
{
    private GameObject[] _childs;
    private int _count;

    private Button _button;

    private bool _isActive = false;

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

    private void Active(bool active)
    {
        for (int i = 0; i < _count; i++)
        {
            _childs[i].SetActive(active);
        }
    }
}
