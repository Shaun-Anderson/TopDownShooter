using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public Axis rotateAxis;
    public float speed;
	// Update is called once per frame
	void Update () {

        switch(rotateAxis)
        {
            case Axis.Z:
                transform.Rotate(Vector3.forward * speed * 0.1f);
                break;

            case Axis.X:
                transform.Rotate(Vector3.right * speed * 0.1f);
                break;

            case Axis.Y:
                transform.Rotate(Vector3.up * speed * 0.1f);
                break;
        }
    }
}

public enum Axis
{
    X,
    Y,
    Z
}