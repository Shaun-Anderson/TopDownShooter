﻿using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
