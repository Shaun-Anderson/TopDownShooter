using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Perk/SpawnObj")]
public class Perk_SpawnObj : Perk
{
    public float percentChance;
    public GameObject target;
    public GameObject ObjToSpawn;
    public SpawnType spawnType;
    PlayerHandler curPlayer;
     
    public override void Initialize(GameObject obj)
    {
        triggerType = TriggerType.Passive;
        perkType = PerkType.Passive;
    }

    public override void Add(PlayerHandler pHand)
    {
        Debug.Log("ADDED PERK: " + perkName);
        curPlayer = pHand;
        GameMaster.instance.perks.Add(this);

        if (spawnType == SpawnType.OnPlayer)
        {
            target = GameMaster.instance.pHand.gameObject;
        }

        if(triggerType == TriggerType.OneTime)
        {
            Trigger();
        }
    }

    public override void Trigger()
    {
        float rand = UnityEngine.Random.Range(0, 100);
        
        if (rand <= percentChance)
        {
            switch (spawnType)
            {
                //Random
                case SpawnType.Random:
                    LvlController lvl = GameMaster.instance.lvlMan;
                    float x = UnityEngine.Random.Range(lvl.minX, lvl.maxX);
                    float y = UnityEngine.Random.Range(lvl.minY, lvl.maxY);
                    GameObject randomSpawn = Instantiate(ObjToSpawn, new Vector2(x, y), Quaternion.identity);
                    break;

                //OnPlayer
                case SpawnType.OnPlayer:
                    GameObject onPlayerSpawn = Instantiate(ObjToSpawn, target.transform.position, Quaternion.identity);
                    break;
            }

            Debug.Log(perkName + ": TRIGGERED");
        }

        if(triggerType == TriggerType.OneTime)
        {
            switch (spawnType)
            {
                //Random
                case SpawnType.Random:
                    LvlController lvl = GameMaster.instance.lvlMan;
                    float x = UnityEngine.Random.Range(lvl.minX, lvl.maxX);
                    float y = UnityEngine.Random.Range(lvl.minY, lvl.maxY);
                    GameObject randomSpawn = Instantiate(ObjToSpawn, new Vector2(x, y), Quaternion.identity);
                    break;

                //OnPlayer
                case SpawnType.OnPlayer:
                    Debug.Log("SPAWN ON PLAYER: " + curPlayer);
                    GameObject onPlayerSpawn = Instantiate(ObjToSpawn, target.transform.position, Quaternion.identity);
                    onPlayerSpawn.GetComponent<SpawnableObject>().phand = curPlayer;
                    break;
            }
        }

    }


}

public enum SpawnType
{
    Random,
    OnEnemy,
    OnPlayer
}