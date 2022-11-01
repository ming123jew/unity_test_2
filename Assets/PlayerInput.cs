using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Variable
    [Header("===== Key settings =====")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";

    public string KeyA = "Left Shift";
    public string KeyB = "j";
    public string KeyC = "k";
    public string KeyD = "l";

    [Header("===== Output signals =====")]
    //
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    //pressing sinal
    public bool Run;
    //trigger once sinnal
    public bool Jump;
    private bool LastJump;
    //double trigger

    [Header("===== Others =====")]
    public bool InputEnable = true;

    private float TargetDup;
    private float TargetDright;
    private float VelocityDup;
    private float VelocityDright;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //if (!Input.GetKeyDown(KeyUp))
        //{
        //    return;
        //}
        //print("keyUp is press.");

        TargetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        TargetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        if (!InputEnable)
        {
            TargetDright = 0;
            TargetDup = 0;
        }

        //SmoothDamp平滑阻阻尼
        Dup = Mathf.SmoothDamp(Dup, TargetDup, ref VelocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, TargetDright, ref VelocityDright, 0.1f);

        Vector2 tempDVec = SquqreToCricle(new Vector2(Dright, Dup));
        float Dup2 = tempDVec.y;
        float Dright2 = tempDVec.x;

        //距离
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        //向量
        Dvec = Dup2 * transform.forward + Dright2 * transform.right;

        Run = Input.GetKey(KeyA);

        bool newJump = Input.GetKey(KeyB);
        if(LastJump != newJump && newJump==true)
        {
            Jump = true;
        }else{
            Jump = false;
        }
        LastJump = newJump;
    }

    private Vector2 SquqreToCricle(Vector2 _input)
    {
        Vector2 output = new Vector2();
        output.x = _input.x * Mathf.Sqrt(1 - (_input.y * _input.y) /2.0f);
        output.y = _input.y * Mathf.Sqrt(1 - (_input.x * _input.x) / 2.0f);
        return output;
    }
}
