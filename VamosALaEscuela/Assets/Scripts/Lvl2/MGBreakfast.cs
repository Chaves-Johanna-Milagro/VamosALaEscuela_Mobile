using UnityEngine;
using System.Collections;
public class MGBreakfast : MonoBehaviour//version mobile
{
    //pa el activar el minijuego del desayuno

    private GameObject _back;

    private GameObject _mouth;
    private GameObject _mDefault;
    private GameObject _mClose;
    private GameObject _mOpen;

    private GameObject _napkin;

    private BNotes _notes;
    private BKindness _kind;

    private GAfton _gAfton;

    private GameObject _menu;

    private bool _food = false;
    private bool _drink = false;
   
    private bool _selectF = false;
    private bool _selectD = false;

    void Start()
    {
        _back = transform.Find("Background").gameObject;

        _menu = transform.Find("Menu").gameObject;

        _mouth = transform.Find("Mouth").gameObject;
        _mDefault = transform.Find("MouthDefault").gameObject;
        _mClose = transform.Find("MouthClose").gameObject;
        _mOpen = transform.Find("MouthOpen").gameObject;

        _napkin = transform.Find("SERVILLETA").gameObject;

        _notes = Object.FindFirstObjectByType<BNotes>();
        _kind = Object.FindFirstObjectByType<BKindness>();

        _gAfton = Object.FindFirstObjectByType<GAfton>();

        if (MiniGameStatus.HasState(gameObject))
        {
            MiniGameStatus.LoadState(gameObject);
            Debug.Log("restaurando estado");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_notes.IsActiveCheck1()) return;

        if (TouchInUIStatus.IsPointerOverUI_PC() || TouchInUIStatus.IsPointerOverUI_Mobile()) return;

        if (CinematicStatus.IsActiveCinematic()) return;

        if (MiniGameStatus.IsActiveMiniGame()) return;


        bool touched = Input.GetMouseButtonDown(0) ||
               (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        if (touched)
        {
            Vector2 screenPoint = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            if (TouchObjectStatus.TouchedThisObject(screenPoint, gameObject))
            {
                _back.SetActive(true);
                _menu.SetActive(true);
                _mouth.SetActive(true);
                _mDefault.SetActive(true);
                PlaySound("Plates");

                _gAfton.GBreakfast();//active las indicaciones del mg

            }
        }

       
    }

    public IEnumerator Complete()
    {

        _notes.ActiveCheck1();//activamos el check
        _kind.GoodDecision();//subimos la barrita

        yield return new WaitForSeconds(2f);

        int count = transform.childCount;
        GameObject[] child = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
            child[i].SetActive(false);
        }

        MiniGameStatus.SaveState(gameObject);
    }
    public void PlaySound(string name)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            if (sound.clip != null && sound.clip.name == name) sound.Play();
        }
    }

    public void SetFood(string name)
    {
        if(_selectF) return;

        _food = true;
        _selectF = true;
        Debug.Log(_food);

        GameObject food = transform.Find(name).gameObject;
        food.SetActive(true);

        _napkin.SetActive(true);
    }
    public void SetDrink(string name)
    {
        if (_selectD) return;

        _drink = true;
        _selectD = true;
        Debug.Log(_drink);

        GameObject drink = transform.Find(name).gameObject;
        drink.SetActive(true);

        _napkin.SetActive(true);
    }

    public IEnumerator ChangeMouth()
    {
        _mDefault.SetActive(true);
        yield return new WaitForSeconds(2f);
        _mOpen.SetActive(true);
        _mDefault.SetActive(false);
        yield return new WaitForSeconds(2f);
        _mClose.SetActive(true);
        _mOpen.SetActive(false);
        yield return new WaitForSeconds(2f);
        _mDefault.SetActive(true);
        _mClose.SetActive(false);
    }


    public bool IsSelectFood() { return _food; }
    public bool IsSelectDrink() { return _drink; }
    public GameObject MDefault() { return _mDefault; }
    public GameObject MOpen() { return _mOpen; }
    public GameObject MClose() { return _mClose; }
}
