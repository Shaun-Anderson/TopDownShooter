using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Enemy {

    public int spawnAmount;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        canAtk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }


    public override void Death()
    {
        spriteRend.sortingLayerName = "Environment";

        for (int i = 0; i <= spawnAmount; i++)
        {
            GameObject newCoin = Instantiate(gem, transform.position, transform.rotation);
            //newCoin.GetComponent<Pickup>().Spawn();
            newCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), ForceMode2D.Impulse);
            if (i == spawnAmount)
            {
                Destroy(gameObject);
            }
        }
    }

public override IEnumerator IDamage(int damage, Vector3 knockbackDir, float force)
    {
        spriteRend.material = flashMat;
        health -= damage;
        yield return new WaitForSeconds(0.1f);
        spriteRend.material = normalMat;
        yield break;
    }
}
