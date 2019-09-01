using UnityEngine;
using System.Collections;

public class _Projectile : MonoBehaviour {

    public int damage;
    public float lifetime;
    public Transform colEffect;
    public float knockbackForce;
    public Vector2 knockbackDir;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        knockbackDir = transform.up;

        lifetime -= 1 +Time.deltaTime * 2;
        if(lifetime <=0)
        {
            Destroy(gameObject,lifetime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy hi = other.GetComponent<Enemy>();
            Instantiate(colEffect, transform.position, Quaternion.identity);
            hi.Hit(damage, knockbackDir, knockbackForce);
            Destroy(gameObject);
        }
        else if (other.tag == "Terrain")
        {
            Instantiate(colEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
