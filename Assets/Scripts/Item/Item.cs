using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public CircleCollider2D col;
    public string itemName;
    public string itemDesc;
    public string pickupText;
    public Sprite Icon;
    public int itemCost;

    public PlayerStats pStats;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Pickup(PlayerStats PStats)
    {
        pStats = PStats;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<PlayerHandler>().interact = true;
        //other.GetComponent<PlayerHandler>().itemOBJ = transform;
        Debug.Log("E to pickup" + itemName);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //other.GetComponent<PlayerHandler>().itemOBJ = null;
        other.GetComponent<PlayerHandler>().interact = false;
    }
}
