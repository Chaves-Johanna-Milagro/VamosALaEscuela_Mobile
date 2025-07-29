using UnityEngine;

public class NameTouching : MonoBehaviour//version mobile
{
    //para mostrar el nombre del objeto en los minijuegos

    private bool isTouched = false;
    private Vector3 screenPosition;
    private GUIStyle labelStyle;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (PauseStatus.IsPaused()) return;
        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;
        if (CinematicStatus.IsActiveCinematic()) return;

        isTouched = false;

        bool touched = Input.GetMouseButton(0) ||
                      (Input.touchCount > 0 &&
                      Input.GetTouch(0).phase == TouchPhase.Began);

        if (touched)
        {
            Vector2 screenPos = Input.touchCount > 0 ?
                                Input.GetTouch(0).position :
                                (Vector2)Input.mousePosition;

            if (TouchObjectStatus.TouchedThisObject(screenPos, gameObject))
            {
                screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                isTouched = true;
            }
        }
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
