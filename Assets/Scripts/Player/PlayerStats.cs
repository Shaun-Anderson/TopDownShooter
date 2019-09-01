using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;
    public int maxHealth;

    public int level;

    public float gold;
    public float maxGold;

	public float Agility;
    public int TurnSpeed;

    public float radius;
    public float pickupTimer = 2;
    public float initialpitch;
    public float pitch;
    public bool pickupcombo;

    public float maxVelocityChange = 10.0f;

    void Start ()
    {
        level = 1;
	}
	
	// Update is called once per frame
	void Update () {
	    if(pickupcombo)
        {
            pickupTimer -= Time.deltaTime;
            if(pickupTimer <= 0)
            {
                //reset
                pitch = initialpitch;
                pickupTimer = 3;
                pickupcombo = false;
            }
        }
        
        if(gold >= maxGold)
        {
            Levelup();
        }
	}

    void Levelup()
    {
        GameMaster.instance.LevelUp();
        gold = 0;
        maxGold = maxGold * 1.5f;
        level += 1;
    }
}
