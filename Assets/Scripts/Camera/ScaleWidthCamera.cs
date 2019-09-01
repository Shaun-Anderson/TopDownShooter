using UnityEngine;
using System.Collections;

public class ScaleWidthCamera : MonoBehaviour {
    public int targetWidth = 640;
    public int pixelsToUnits = 100;
    public float zoom = 4; //determines amount of zoom capable. Larger number means further zoomed in
    public float smooth = 5; //smooth determines speed of transition between zoomed in and default state
                             // Use this for initialization

    public float up;
    public float down;
    public Camera cam;
    void Start () {
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            zoom = 2;
            up = zoom + 0.02f;
            down = zoom - 0.02f;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            zoom = 3;
            up = zoom + 0.02f;
            down = zoom - 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            zoom = 4;
            up = zoom + 0.02f;
            down = zoom - 0.02f;
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, smooth * Time.deltaTime);

        if(cam.orthographicSize <= (up) && cam.orthographicSize >= (down))
        {
            cam.orthographicSize = zoom;
        }
    }
}
