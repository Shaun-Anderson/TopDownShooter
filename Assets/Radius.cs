using UnityEngine;
using System.Collections;

public class Radius : MonoBehaviour {

    public float radi;

    public CircleCollider2D pCol; 


    void Start()
    {
        pCol = GetComponent<CircleCollider2D>();
        radi = 0.15f;
        pCol.radius = radi;
    }

    public void ChangeRadi(float amount)
    {
        radi = amount;
        pCol.radius = radi;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
        {
            if (col.GetComponent<Pickup>() != null)
            {
                if (col.GetComponent<Pickup>().active == false & col.tag == "Pickup")
                {
                    col.GetComponent<Pickup>().active = true;
                    col.GetComponent<Pickup>().player = transform;
                }
            }
        }
    }
}
