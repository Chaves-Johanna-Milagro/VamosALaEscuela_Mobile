using UnityEngine;

public class PlayerAnim1 : MonoBehaviour
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

        if (newPos.x > transform.position.x)
        {
            _anim.SetBool("R_Walk_PJ", true);
        }
        else if (newPos.x < transform.position.x)
        {
            _anim.SetBool("L_Walk_PJ", true);
        }
    }
    private void IdleAnim()
    {
        ResetAnim();

        if (!_pMove.IsMoving()) _anim.SetBool("Idle_PJ", true);
    }

    private void ResetAnim()
    {
        _anim.SetBool("Idle_PJ", false);
        _anim.SetBool("R_Walk_PJ", false);
        _anim.SetBool("L_Walk_PJ", false);
    }
}
