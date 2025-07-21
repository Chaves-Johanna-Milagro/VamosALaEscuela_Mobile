using UnityEngine;

public class GAftonAnimT : MonoBehaviour
{
    private GuiaAftonT _gAfton;
    private Animator _anim;

    private void Start()
    {
        _gAfton = GetComponent<GuiaAftonT>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gAfton.IsActiveGuia())
        {
            if (_gAfton.GetCurrentAnim() == 1) _anim.SetBool("G_Walk", true);
            if (_gAfton .GetCurrentAnim() == 2) _anim.SetBool("G_Interact", true);
            if (_gAfton.GetCurrentAnim() == 3) _anim.SetBool("G_Buttons", true);
            if (_gAfton.GetCurrentAnim() == 4) _anim.SetBool("G_BPause", true);
            if (_gAfton.GetCurrentAnim() == 5) _anim.SetBool("G_BNotes", true);
            if (_gAfton.GetCurrentAnim() == 6) _anim.SetBool("G_BKind", true);
            if (_gAfton.GetCurrentAnim() == 7) _anim.SetBool("G_BMiniA", true);
        }
        if (!_gAfton.IsActiveGuia()) ResetsAnims();
    }
    private void ResetsAnims()
    {
        _anim.SetBool("G_Walk", false);
        _anim.SetBool("G_Interact", false);
        //_anim.SetBool("G_Buttons", false);
        _anim.SetBool("G_BPause", false);
        _anim.SetBool("G_BNotes", false);
        _anim.SetBool("G_BKind", false);
        _anim.SetBool("G_BMiniA", false);
    }
}
