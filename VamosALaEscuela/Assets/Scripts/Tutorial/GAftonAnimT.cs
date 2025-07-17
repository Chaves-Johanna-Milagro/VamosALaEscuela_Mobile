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
            if (_gAfton.GetCurrentAnim() == 3) _anim.SetBool("G_Buttons",true);
        }
    }
}
