using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public int damage;
    public float speed;
    public float knockbackForce;
    public Transform collisionEffect;

    //Perk based abilities
    public bool bounce, pierce;
    public PlayerHandler owner;

    public LayerMask mask;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * Time.deltaTime * speed);

        Debug.DrawRay(transform.position, transform.up * 0.05f, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.05f, mask);

        if (hit)
        {

            Vector3 Dir = Vector3.Reflect(transform.up, hit.normal);
            //transform.rotation = Quaternion.LookRotation(Dir);
            float rot = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
           // print(Dir);
            transform.eulerAngles = new Vector3(0, 0, rot);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            Instantiate(collisionEffect, transform.position, transform.rotation);
            if (col.GetComponent<Enemy>() != null && owner != null)
            {
                col.GetComponent<Enemy>().lastHitPlayer = owner;
                col.GetComponent<Enemy>().Hit(damage, transform.up, knockbackForce);
                owner.Check_OnHit();
            }

            Destroy(gameObject);
        }
        if(col.tag == "Terrain")
        {
            if (!bounce)
            {
                Instantiate(collisionEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
