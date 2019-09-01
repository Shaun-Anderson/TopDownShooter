using UnityEngine;
using System.Collections;

public class SoulFood : Item
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Pickup(PlayerStats PStats)
    {
        base.Pickup(PStats);
        PStats.maxHealth += 100;
    }
}
