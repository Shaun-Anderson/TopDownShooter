using UnityEngine;
using System.Collections;

public class Spell : _Spell
{
    public Transform projectilePrefab;
    public Transform collisionEffect;
    public Transform projectileSpawn;
    public float projectileSpeed;

    public float projectileRotation;
    public int projLifetime;
    public SpellType spellType;
    public MagicType magicType;

	// Use this for initialization
	public void Fire (float angle, Transform spawn, bool canFire)
    {
        if (canFire == true)
        {
            if (spellType == SpellType.Proj)
            {
                //Debug.Log("Active? " + gameObject.activeInHierarchy);
                //Debug.Log("fire");
                Transform bulletInstance = (Transform)Instantiate(projectilePrefab, spawn.position, Quaternion.Euler(new Vector3(0, 0, angle - projectileRotation)));   //instantiate the bullet at the correct position and set rotation to current rotation
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * projectileSpeed);
                bulletInstance.GetComponent<_Projectile>().damage = damage;
                bulletInstance.GetComponent<_Projectile>().lifetime = projLifetime;
                bulletInstance.GetComponent<_Projectile>().colEffect = collisionEffect;
                //canFire = false;

            }
        }
    }

}