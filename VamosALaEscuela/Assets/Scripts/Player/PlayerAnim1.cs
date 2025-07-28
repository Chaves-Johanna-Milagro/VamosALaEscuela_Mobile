using UnityEngine;

public class PlayerAnim1 : MonoBehaviour//version mobile
{
    //script para la animacion del personaje en el nivel 1 y 2

    private PlayerMove _pMove;

    private Animator _anim;

    private void Start()
    {
        _pMove = GetComponent<PlayerMove>();

        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PauseStatus.IsPaused()) return;

        if (_pMove.IsMoving())
        {
            Vector3 targetPos = _pMove.NextPos();
            WalkAnim(targetPos);
        }

    }

    private void LateUpdate()
    {
        IdleAnim();
    }

    private void WalkAnim(Vector3 newPos)
    {
        ResetAnim();

        bool useRP = CheckStatus.IsCheckActive("Level1", 1);

        if (newPos.x > transform.position.x)
        {
           if(useRP) _anim.SetBool("R_Walk_RP", true);
            else _anim.SetBool("R_Walk_PJ", true);
        }
        else if (newPos.x < transform.position.x)
        {
            if(useRP) _anim.SetBool("L_Walk_RP", true);
            else _anim.SetBool("L_Walk_PJ", true);
        }
    }
    private void IdleAnim()
    {
        ResetAnim();
        bool useRP = CheckStatus.IsCheckActive("Level1",1);

        if (!_pMove.IsMoving())
        {
            if(useRP)_anim.SetBool("Idle_RP", true);
            else _anim.SetBool("Idle_PJ", true);
        }
    }

    private void ResetAnim()
    {
        _anim.SetBool("Idle_PJ", false);
        _anim.SetBool("R_Walk_PJ", false);
        _anim.SetBool("L_Walk_PJ", false);

        _anim.SetBool("Idle_RP", false);
        _anim.SetBool("R_Walk_RP", false);
        _anim.SetBool("L_Walk_RP", false);
    }
}
