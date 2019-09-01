using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour {

    public Transform spriteToMove;
    public float amplitude;
    public float length;
    public Light light;
	
	// Update is called once per frame
	void Update () {
        spriteToMove.localPosition = new Vector3(0, Mathf.PingPong(Time.time * amplitude, length), transform.position.z);
        if (light != null)
        {

            light.spotAngle = Mathf.PingPong(Time.time * amplitude, length) * 75;
        }

    }
}
