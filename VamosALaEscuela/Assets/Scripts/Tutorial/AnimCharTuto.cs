using UnityEngine;

public class AnimCharTuto : MonoBehaviour
{
    private PlayerMove _pMove;

    private Animator _anim;

    private void Start()
    {
        _pMove = GetComponent<PlayerMove>();

        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_pMove.IsMoving())
        {
            Vector3 targetPos = _pMove.NextPos();
            WalkAnim(targetPos);
        }

    }

    private void  LateUpdate()
    {
        IdleAnim();
    }

    private void WalkAnim(Vector3 newPos)
    {
        ResetAnim();

        if (newPos.x > transform.position.x)
        {
            _anim.SetBool("R_Walk_RP", true);
        }
        else if (newPos.x < transform.position.x)
        {
            _anim.SetBool("L_Walk_RP", true);
        }
    }
    private void IdleAnim()
    {
        ResetAnim();

        if (!_pMove.IsMoving()) _anim.SetBool("Idle_RP",true);
    }

    private void ResetAnim()
    {
        _anim.SetBool("Idle_RP",false);
        _anim.SetBool("R_Walk_RP",false);
        _anim.SetBool("L_Walk_RP",false);
    }
}
