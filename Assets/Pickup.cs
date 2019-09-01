using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public GameMaster GM;
    public Transform player;
    public bool active;
    public AudioClip pickupsound;
    public float audio;


    private SpriteRenderer sRend;
	// Use this for initialization
	void Start () {
        sRend = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (active && !GameMaster.instance.paused)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<PlayerStats>().pickupcombo == false)
            {
                sRend.enabled = false;
                GetComponent<SpriteRenderer>();
                col.GetComponent<PlayerStats>().initialpitch = 1;
                col.GetComponent<PlayerStats>().pitch = col.GetComponent<PlayerStats>().initialpitch;
                GameMaster.instance.Play(pickupsound, col.GetComponent<PlayerStats>().pitch, audio * GameMaster.instance.saveState.soundVolume);
                col.GetComponent<PlayerStats>().pickupcombo = true;

                //remove
                GameMaster.instance.ExpGain(1);
                Destroy(gameObject);
            }
            else
            {
                sRend.enabled = false;
                GetComponent<AudioSource>().pitch = col.GetComponent<PlayerStats>().pitch;
                col.GetComponent<PlayerStats>().pitch = GetComponent<AudioSource>().pitch + 0.1f;
                if (col.GetComponent<PlayerStats>().pitch >= 1.6)
                {
                    col.GetComponent<PlayerStats>().pitch = 1.5f;
                }
                GameMaster.instance.Play(pickupsound, col.GetComponent<PlayerStats>().pitch, audio * GameMaster.instance.saveState.soundVolume);
                col.GetComponent<PlayerStats>().pickupTimer = 3;

                GameMaster.instance.ExpGain(1);
                Destroy(gameObject);
            }
        }
    }

    }
