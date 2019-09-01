using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterTime : MonoBehaviour {
    public float timeToSpawn;
    float curTime;
    public GameObject ObjToSpawn;
    public bool destroyOnSpawn;
    public Animator ani;
	
   void Start()
    {
        curTime = timeToSpawn;
    }

	// Update is called once per frame
	void Update () {
        if (!GameMaster.instance.paused)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 3 && ani != null)
            {
                ani.SetBool("Close", true);
            }

            if (curTime <= 0)
            {
                Spawn();
            }
        }
	}

    void Spawn()
    {
        GameObject newObj = Instantiate(ObjToSpawn, transform.position, transform.rotation);

        if (destroyOnSpawn)
        {
            Destroy(gameObject);
        }
        curTime = timeToSpawn;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameMaster.instance.AddScore(50);
            Destroy(gameObject);
        }
    }

}
