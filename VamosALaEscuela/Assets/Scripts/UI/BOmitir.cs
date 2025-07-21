using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BOmitir : MonoBehaviour
{
    private string _scene;

    private Button _button;

    void Start()
    {
        _scene = SceneManager.GetActiveScene().name;

        _button = GetComponent<Button>();

        _button.onClick.AddListener(Omitir);
    }

    private void Omitir()
    {
        if (_scene == "Tutorial") SceneManager.LoadScene("Level1");
    }


}
