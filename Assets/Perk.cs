using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Perk: ScriptableObject {

    [HideInInspector]
    PlayerHandler pHand;

    [Header("Perk")]
    public string perkName;
    [TextArea(3,10)]
    public string desc;
    public Sprite aSprite;
    public AudioClip aSound;
    public TriggerType triggerType;
    public PerkType perkType;
    public float aBaseCoolDown = 1f;

    public abstract void Initialize(GameObject obj);

    //TriggerTypes
    public abstract void Trigger();

    public abstract void Add(PlayerHandler phand);
}

public enum TriggerType
{
    OnHit,
    OnKill,
    Passive,
    OneTime
}

public enum PerkType
{
    Buff,
    LaunchProjectile,
    Passive
}
