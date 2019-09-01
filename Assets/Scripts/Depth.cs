using UnityEngine;
using System.Collections;

public class Depth : MonoBehaviour {

    public bool notMove;
	float Xposition;
    float Yposition;
	float Zposition;
	
    void Start()
    {
        if (notMove)
        {
            Xposition = transform.position.x;
            Yposition = transform.position.y;
            transform.position = new Vector3(Xposition, Yposition, Yposition + Zposition);
        }
    }

	// Update is called once per frame
	void Update () {
        if (!notMove)
        {
            Xposition = transform.position.x;
            Yposition = transform.position.y;
            transform.position = new Vector3(Xposition, Yposition, Yposition + Zposition);
        }
	}
}