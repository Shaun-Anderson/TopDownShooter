using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {

    public Transform currentEnemy;
    public float turnSpeed = 1.0f;
    public Transform turrent;
    public Transform bulletPrefab;
    public bool canFire;
    public Vector3 dir;
    public float bulletSpeed;
	public Transform bulletSpawnPos;

	public List<GameObject> targets = new List<GameObject>();

	public CircleCollider2D radiusCollider;
 
    void Update()
    {
        if (currentEnemy)
        { // enemy alive and at sight: aim at him!
            dir = currentEnemy.position - transform.position; // find direction
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			turrent.transform.rotation = Quaternion.Slerp(turrent.transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), turnSpeed * Time.deltaTime);
			if (canFire == true)
            {
                Fire();
            }
        }
        else
        {
            // no enemy or enemy dead: find the nearest
            // victim and assign it to currentEnemy
			if(currentEnemy == null && targets.Count > 0)
			{
				currentEnemy = targets[0].transform;
			}
        }
    }

    void Fire()
    {
        Transform bullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, turrent.transform.rotation)as Transform;
        bullet.GetComponent<Rigidbody2D>().velocity = turrent.transform.up * bulletSpeed;
        //StartCoroutine(Kickback());
        StartCoroutine(FirerateWait(1f));
    }

    IEnumerator Kickback()
    {
        turrent.transform.position = -dir*0.01f;
        yield return new WaitForSeconds(0.9f);
        turrent.transform.position = dir*0.01f;
        yield break;
    }

    IEnumerator FirerateWait(float firerate)
    {
        canFire = false;
        yield return new WaitForSeconds(firerate);
        canFire = true;
    }
}
