using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    PlayerHandler pHand;

    public Vector3 mousePos;
    Vector3 targetVelocity;

    public string[] controllers;
    public float RVx;
    public float RHx;
    public float Vx;
    public float Hx;

    public float atk1;
    public float LeftTrigger;

    public bool tigger1;
    public bool forward;
    public int turn;
    public PlayerAniControl pAni;
    // Use this for initialization
    void Start ()
    {
        pHand = GetComponent<PlayerHandler>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        controllers = Input.GetJoystickNames();
        if (!GameMaster.instance.paused)
        {
            if (atk1 == 0)
            {
                atk1 = Input.GetAxis("Right_Trigger");
            }
            if (atk1 == 1)
            {
                pHand.FireSpell(0);
                atk1 = 0;
            }

            if (LeftTrigger == 0)
            {
                LeftTrigger = Input.GetAxis("Left_Trigger");
            }
            if (LeftTrigger == 1)
            {
                pHand.FireSpell2();
                LeftTrigger = 0;
            }

            if (Input.GetButtonDown("Xbox_LB"))
            {
                pHand.Dash(targetVelocity);
            }
            //rotation



            if (Input.GetButtonDown("Dash"))
            {
                Debug.Log("hi");
                pHand.Dash(targetVelocity);
            }




            #region AIM
            if (controllers.Length != 0)
            {
                if (controllers[0] == "")
                {
                    mousePos = Input.mousePosition;
                    mousePos.z = 5.23f;
                    Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                    mousePos.x = mousePos.x - objectPos.x;
                    mousePos.y = mousePos.y - objectPos.y;
                    pHand.PcAim(mousePos);

                }
                else
                {
                    RVx = Input.GetAxis("RVertical");
                    RHx = Input.GetAxis("RHorizontal");
                    Vx = Input.GetAxis("Vertical");
                    Hx = Input.GetAxis("Horizontal");
                    pHand.ControllerAim(RVx, RHx, Vx, Hx);
                   // pHand.ControllerMove(Vx, Hx);
                }
            }
            else
            {
                mousePos = Input.mousePosition;
                mousePos.z = 5.23f;
                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;
                pHand.PcAim(mousePos);
            }
            #endregion


            if (Input.GetButton("Fire1") && pHand.canFire == true)
            {
                pHand.FireSpell(0);
            }

            if (Input.GetButton("Fire2") && pHand.canFire1 == true)
            {
                pHand.FireSpell2();
            }
             
        }
        //Input that staysactive even on pause
        if (Input.GetButtonDown("Pause"))
        {
            GameMaster.instance.Pause();
            print("PAUSE");
        }

    }

    void FixedUpdate()
    {
		if (!GameMaster.instance.paused)
        {
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            pHand.Movement(targetVelocity);
        }
    }

}
