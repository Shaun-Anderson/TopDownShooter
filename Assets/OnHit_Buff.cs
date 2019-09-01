using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Perk/OnHit_Buff")]
public class OnHit_Buff: Perk
{
    //This class is used when a perk will just change a variables value (change a bool to unlock a move or stat values)

    [Header("Variable Changer")]
    public VariableToChange variableToChange;
    public string variableName;

    public float percentChance;
    public float amount;
    public string stringBoolAmount;

    public bool property;

    public override void Initialize(GameObject obj)
    {
        triggerType = TriggerType.OnHit;
    }

    public override void Trigger()
    {
        float rand = UnityEngine.Random.Range(0, 100);

        if (rand <= percentChance)
        {
            GameMaster.instance.pHand.GetType().GetField(variableName).SetValue(GameMaster.instance.pHand, amount);
            Debug.Log(perkName + ": TRIGGERED");
        }

    }

    //If the 
    public override void Add(PlayerHandler pHand)
    {
        Debug.Log("ADDED PERK: " + perkName);
        GameMaster.instance.perks.Add(this);

        if(triggerType == TriggerType.OneTime)
        {
            switch(variableToChange)
            {
                case VariableToChange.Bool:
                    GameMaster.instance.pHand.GetType().GetField(variableName).SetValue(GameMaster.instance.pHand, Convert.ToBoolean(stringBoolAmount));
                    break;

                case VariableToChange.Float:
                    if(property)
                    {
                        GameMaster.instance.pHand.GetType().GetProperty(variableName).SetValue(GameMaster.instance.pHand, amount, null);
                    }
                    else
                    {
                        GameMaster.instance.pHand.GetType().GetField(variableName).SetValue(GameMaster.instance.pHand, amount);
                    }
                    break;
            }
        }
    }


}

public enum VariableToChange
{
    Int,
    Float,
    Bool,
    String
}
