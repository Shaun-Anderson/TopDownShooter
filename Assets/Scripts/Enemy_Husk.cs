using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Husk : MonoBehaviour {

    public Enemy enemyStat;
    public GameObject target;

    private PolyNavAgent _agent;
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    void Awake()
    {
        enemyStat = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        agent.SetDestination(target.transform.position);
    }

    void Update()
    {
        if (GameMaster.instance.paused)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
            agent.SetDestination(target.transform.position);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHandler>().Hit();
        }
    }
}
