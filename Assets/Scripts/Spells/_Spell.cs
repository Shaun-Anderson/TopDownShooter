using UnityEngine;
using System.Collections;

public class _Spell : MonoBehaviour {

    public string spellName;
    public float cooldown;
    public int manaCost;
    public int damage;
	// Update is called once per frame
	/*public virtual void Fire (float angle) {
	
	}
    */
    public enum SpellType
    {
        Proj,
        ChargedProj,
        Aoe,
        SelfAoe,
        Stream,
        CloseQuaters,
        Buff
    }
    public enum MagicType
    {
        Void,
        Arcane,
        Fire,
        Earth,
        Ice,
        Wind
    }
}