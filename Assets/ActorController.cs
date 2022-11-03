using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float WalkSpeed = 2.0f;
    public float RunSpeed = 2.0f;
    public float JumpSpeed = 3.0f; 

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 PlanarVec;
    private Vector3 ThrustVec;

    private bool LockPlanar = false;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //常用于摄像机追随玩家 实现平滑跟踪
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.Run ? 2.0f : 1.0f), 0.5f));
        if (pi.Jump)
        {
            anim.SetTrigger("jump");
        }

        if (pi.Dmag > 0.1f)
        {
            //Vector3.Slerp用于平滑转向
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
        }

        //起跳锁住平移，防止乱转
        if (false == LockPlanar)
        {
            PlanarVec = (pi.Run ? RunSpeed : 1.0f) * pi.Dmag * WalkSpeed * model.transform.forward;
        }
        
    }

    private void FixedUpdate()
    {
        //rigid.position += PlanarVec * Time.fixedDeltaTime + ThrustVec;
        rigid.velocity = new Vector3(PlanarVec.x, rigid.velocity.y, PlanarVec.z) + ThrustVec;
        ThrustVec = Vector3.zero;

    }

    //message block
    public void OnJumpEnter(Animator animator)
    {
        anim.ResetTrigger("jump");
        pi.InputEnable = false;
        LockPlanar = true;
        ThrustVec = new Vector3(0, JumpSpeed, 0);
    }

    public void OnGroundEnter()
    {
        pi.InputEnable = true;
        LockPlanar = false;
    }

    public void OnFallEnter()
    {
        pi.InputEnable = true;
        LockPlanar = false;
    }

    public void IsGround()
    {
        //print("is ground");
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        print("is not ground");
        anim.SetBool("isGround", false);
    }
}
