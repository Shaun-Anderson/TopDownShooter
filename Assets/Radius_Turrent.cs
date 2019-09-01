using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius_Turrent : MonoBehaviour {

	public TagToCheck tagCheck;
	public NewBehaviourScript newBehaviourScript;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == tagCheck.ToString())
		{
			Debug.Log("Enter: " + col.gameObject.name);
			newBehaviourScript.targets.Add(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == tagCheck.ToString())
		{
			Debug.Log("Exit: " + col.gameObject.name);
			newBehaviourScript.targets.Remove(col.gameObject);
		}
	}
}


public enum TagToCheck
{
	Enemy,
	Player
}
