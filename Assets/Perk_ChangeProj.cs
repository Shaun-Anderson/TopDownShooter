using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Perk/ChangeProj")]
public class Perk_ChangeProj : Perk
{
    public string abilityName;
   

    public override void Initialize(GameObject obj)
    {
        triggerType = TriggerType.OneTime;
        perkType = PerkType.Passive;
    }

    public override void Add(PlayerHandler pHand)
    {
        Debug.Log("ADDED PERK: " + perkName);
        GameMaster.instance.perks.Add(this);
        Trigger();
    }

    public override void Trigger()
    {
        Debug.Log("PASSIVE: " + GameMaster.instance.pHand.bulletPrefab.GetComponent<Bullet>().GetType().GetField(abilityName));
        GameMaster.instance.pHand.bulletPrefab.GetComponent<Bullet>().GetType().GetField(abilityName).SetValue(GameMaster.instance.pHand.bulletPrefab.GetComponent<Bullet>(), true);
    }


}