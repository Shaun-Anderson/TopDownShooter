using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour {

    public int damage;
    public PlayerHandler pHand;

	// Use this for initialization
	void Start () {
        pHand = GetComponentInParent<PlayerHandler>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().lastHitPlayer = pHand;
            col.GetComponent<Enemy>().Hit(damage, Vector3.zero, 0);

        }
    }
}
