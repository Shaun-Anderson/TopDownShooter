using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour {
    public PlayerHandler phand;

    void Start()
    {
        if(GetComponent<DamageEnemies>())
        {
            GetComponent<DamageEnemies>().pHand = phand;
        }
    }
}
