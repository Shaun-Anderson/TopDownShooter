using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class EnemyMovement : MonoBehaviour
{

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

    public Enemy enemyStat;
    public GameObject target;
    public Transform atk;
    public State currentState;
    public float angle;
    public Transform rotatetransform;
	public float turnSpeed;

    
    public bool canAtk;
    public float AtkRange;

    void Awake()
    {
        enemyStat = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!GameMaster.instance.paused)
        {
            canAtk = enemyStat.canAtk;
            if (Vector3.Distance(target.transform.position, transform.position) <= AtkRange)
            {
                currentState = State.attack;
            }
            else
            {
                currentState = State.chase;
            }

            switch (currentState)
            {
                case State.chase:
                    agent.enabled = true;
                    agent.SetDestination(target.transform.position);

					Vector3 dir = target.transform.position - transform.position;
					dir = target.transform.InverseTransformDirection(dir);
					angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
					rotatetransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
					break;
                case State.attack:
                    agent.enabled = false;
                    Rigidbody2D rigid = GetComponent<Rigidbody2D>();

                    rigid.velocity = Vector3.zero;
                    rigid.angularVelocity = 0;
                    if (canAtk == true)
                        Atk();
                    break;
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    public void Atk()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = target.transform.InverseTransformDirection(dir);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rotatetransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        Instantiate(atk, transform.position, rotatetransform.rotation);
        enemyStat.StartCoroutine(enemyStat.FirerateWait(3f));
        
    }

    public enum State
    {
        chase,
        attack
    }

}
