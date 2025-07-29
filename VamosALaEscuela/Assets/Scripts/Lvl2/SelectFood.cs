using UnityEditor;
using UnityEngine;

public class SelectFood : MonoBehaviour
{
    //pa la comida del menu

    private float _scaleMultiplier = 1.1f;
    private float _scaleSpeed = 5f;

    private Vector3 _originalScale;
    private Vector3 _targetScale;

    private bool _isScaling = false;

    private bool isTouched = false;

    private Vector3 screenPosition;
    private GUIStyle labelStyle;

    private MGBreakfast _mg;

    private string _name;

    private void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale * _scaleMultiplier;

        // Estilo para la etiqueta
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 40;
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.padding = new RectOffset(8, 8, 4, 4);

        Texture2D bgTexture = new Texture2D(1, 1);
        bgTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.6f));
        bgTexture.Apply();
        labelStyle.normal.background = bgTexture;

        GameObject par = transform.parent.gameObject;

        _mg = par.GetComponentInParent<MGBreakfast>();
        _name = gameObject.name;
    }

    private void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;


        _isScaling = false;

        isTouched = false;

        // Detectar toque o clic válido
        bool touched = Input.GetMouseButton(0) ||
                       (Input.touchCount > 0 &&
                       Input.GetTouch(0).phase == TouchPhase.Began); // || //began para el primer toque
                                                                     // Input.GetTouch(0).phase == TouchPhase.Stationary)); //stationary cuando se lo mantiene tocado

        if (touched)
        {

            Vector2 screenPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            if (TouchObjectStatus.TouchedThisObject(screenPos, gameObject))
            {
                screenPosition = Camera.main.WorldToScreenPoint(transform.position);

                _isScaling = true;
                isTouched = true;

                if (_name == "TE" || _name == "MATE" ||  _name == "BATIDO") _mg.SetDrink(_name);
                if (_name == "PANES" || _name == "GALLETAS" ||  _name == "SANWIS") _mg.SetFood(_name);

                if (_mg.IsSelectFood() && _mg.IsSelectDrink()) transform.parent.gameObject.SetActive(false);
            }
        }

        // Escalado suave
        Vector3 targetScaling = _isScaling ? _targetScale : _originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScaling, Time.deltaTime * _scaleSpeed);
    }

    private void OnGUI()
    {
        if (isTouched)
        {
            Vector2 textSize = labelStyle.CalcSize(new GUIContent(gameObject.name));
            Vector2 labelPosition = new Vector2(screenPosition.x, Screen.height - screenPosition.y - 30);
            Rect labelRect = new Rect(labelPosition.x - textSize.x / 2f, labelPosition.y, textSize.x + 1f, textSize.y);

            GUI.Label(labelRect, gameObject.name, labelStyle);
        }
    }
}
