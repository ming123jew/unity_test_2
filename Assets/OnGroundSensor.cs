using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//侦测是否撞击地面 https://www.jb51.net/article/213749.htm  22 26
public class OnGroundSensor : MonoBehaviour
{

    private Vector3 Point1;
    private Vector3 Point2;
    private float Radius;

    public CapsuleCollider Capcol;
    public float Offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Radius = Capcol.radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Point1 = transform.position + transform.up * (Radius - Offset);
        Point2 = transform.position + transform.up * (Capcol.height - Offset) - transform.up * Radius;

        Collider[] cols = Physics.OverlapCapsule(Point1, Point2, Radius, LayerMask.GetMask("Ground"));
  
        if (cols.Length != 0)
        {

            //foreach (var item in cols)
            //{
            //    print(item.name);
            //}
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
       
    }
}
