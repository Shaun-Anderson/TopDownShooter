using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int health = 100;
    public float maxHealth = 100;
    public float healthBarLength;
    public GameObject healthbar;

    public Rigidbody2D rigid;
    public SpriteRenderer spriteRend;
    public Material flashMat;
    public Material normalMat;
    public GameObject gem;

    public int scoreWorth;

    public PlayerHandler lastHitPlayer;

    public bool canAtk = true;
	// Use this for initialization
	void Start ()
    {
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
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

    public void Hit(int damage, Vector3 knockbackDir, float force)
    {
        StartCoroutine(IDamage(damage, knockbackDir, force));
    }

    public virtual void Death()
    {
        GameMaster.instance.AddScore(100);
        spriteRend.sortingLayerName = "Environment";
        Instantiate(gem, transform.position, transform.rotation);

        lastHitPlayer.Check_OnKill();
        this.enabled = false;

        if(gameObject.GetComponent<Enemy_Husk>())
        {
            gameObject.GetComponent<Enemy_Husk>().enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        if (gameObject.GetComponent<EnemyMovement>())
        {
            gameObject.GetComponent<EnemyMovement>().enabled = false;
        }
        gameObject.GetComponent<PolyNavAgent>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void DeathKnock(Vector3 knockbackDir, float force)
    {
        rigid.AddForce(knockbackDir.normalized * force * 2);
    }

    public virtual IEnumerator IDamage (int damage, Vector3 knockbackDir, float force)
    {
        if (rigid != null)
        {
            rigid.AddForce(knockbackDir.normalized * force);
        }
        spriteRend.material = flashMat;
        health -= damage;
        yield return new WaitForSeconds(0.1f);
        spriteRend.material = normalMat;
        if (health <= 0)
        {
            DeathKnock(knockbackDir, force);
        }
        Debug.Log("Damage");
        yield break;
    }

    public IEnumerator FirerateWait(float firerate)
    {
        canAtk = false;
        yield return new WaitForSeconds(firerate);
        canAtk = true;
        yield break;
    }
}
