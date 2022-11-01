using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{

    private Vector3 Point1;
    private Vector3 Point2;
    private float Radius;

    public CapsuleCollider Capcol;

    // Start is called before the first frame update
    void Start()
    {
        Radius = Capcol.radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Point1 = transform.position + transform.up * Radius;
        Point2 = transform.position + transform.up * Capcol.height - transform.up * Radius;

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
