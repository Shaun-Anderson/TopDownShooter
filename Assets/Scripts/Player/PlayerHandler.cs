using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHandler : MonoBehaviour
{
    public int playerNum;

    //References
    public PlayerStats pStats;
    public PlayerGui pGUI;
    public PlayerAniControl pAni;
    public Camera pCam;

    //Perks
    public List<Perk> Perks = new List<Perk>();

    public Transform rotatetransform;
    public float angle;
    public Transform center;
    public bool canFire = true;
    public bool canFire1 = true;

    public Transform OBJ;
    public bool interact;

    public SpriteRenderer spriteRend;

    public Rigidbody2D bulletPrefab;
    public float speed;

    public float x;
    public float y;

    public bool pause;

    public Radius radius;

    public int amountToBlastAmmo;
    public int curAmountToBlastAmmo;
    public int blastAmmo;

    [SerializeField]
    float radi;
    public float Radi
    {
        get { return radi; }
        set {
            radi = value;
            radius.ChangeRadi(radi);
        }
    }
    

    public AudioClip shotSound;
    public AudioClip blastSound;

    [Header("Perk Checks")]
    //Perk Checks
    public bool flameDash;
    public bool perk_ReRoll;
    public bool perk_PerkUp;

    void Start()
    {
        canFire = true;
        pStats = GetComponent<PlayerStats>();
        pGUI = GetComponent<PlayerGui>();
        pAni = GetComponent<PlayerAniControl>();
        radius = GetComponentInChildren<Radius>();
        // AddItem(OBJ);
        GameMaster.instance.pHand = this;
    }

    public void Dash(Vector3 targetVelocity)
    {
        GetComponent<Rigidbody2D>().AddForce( new Vector2(targetVelocity.normalized.x, targetVelocity.normalized.y) * (pStats.Agility*6),ForceMode2D.Impulse);
        Debug.Log("hi2");
    }

    IEnumerator SpawnFire()
    {

        yield return new WaitForSeconds(0.1f);
        yield break;
    }

    //Movement
    public void Movement (Vector3 targetVelocity)
    {
        //targetVelocity = transform.TransformDirection(targetVelocity);
        //targetVelocity *= pStats.Agility;
        //GetComponent<Rigidbody2D>().velocity = targetVelocity;
        GetComponent<Rigidbody2D>().AddForce ( new Vector2(targetVelocity.x,targetVelocity.y) * 1,ForceMode2D.Impulse);
    }


    //Aim
    public Vector3 PcAim(Vector3 mousePos)
    {
        if (!pause)
        {
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            rotatetransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
           // pAni.SpriteAngle(angle);
        }
     return mousePos;
    }
    public void ControllerAim(float RVx, float RHx, float Vx, float Hx)
        {
            #region ControllerControl

            if (RVx >= 0.1f || RVx <= -0.1f || RHx >= 0.1f || RHx <= -0.1f)
            {
                rotatetransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(Input.GetAxis("RVertical"), Input.GetAxis("RHorizontal")) * 180 / Mathf.PI -90);
            }
            else if (Vx >= 0.1f || Vx <= -0.1f || Hx >= 0.1f || Hx <= -0.1f)
            {
                rotatetransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI-90);
            }
            angle = Mathf.Atan2(RVx, RHx) * Mathf.Rad2Deg;
        #endregion
    }

    //Perk Checks
    public void Check_Tick()
    {
        for (int i = 0; i < GameMaster.instance.perks.Count; i++)
        {
            if (GameMaster.instance.perks[i].triggerType == TriggerType.Passive)
            {
                switch (GameMaster.instance.perks[i].perkType)
                {
                    case PerkType.Buff:
                        GameMaster.instance.perks[i].Trigger();
                        break;
                }
            }
        }
    }
    
    public void Check_OnHit()
    {
        for(int i = 0; i<GameMaster.instance.perks.Count; i++)
        {
            if (GameMaster.instance.perks[i].triggerType == TriggerType.OnHit)
            {
                switch (GameMaster.instance.perks[i].perkType)
                {
                    case PerkType.Buff:
                        GameMaster.instance.perks[i].Trigger();
                        break;
                }
            }
        }
        print("OnHitCheck");
    }

    public void Check_OnKill()
    {
        GameMaster.instance.kills += 1;
        for (int i = 0; i < GameMaster.instance.perks.Count; i++)
        {
            if (GameMaster.instance.perks[i].triggerType == TriggerType.OnKill)
            {
                switch (GameMaster.instance.perks[i].perkType)
                {
                    case PerkType.Buff:
                        GameMaster.instance.perks[i].Trigger();
                        break;
                }
            }
        }
        print("OnKillCheck");
    }

    //Attacks
    public void FireSpell (int i)
    {
        if (canFire == true && !pause)
        {
            Rigidbody2D newBullet = Instantiate(bulletPrefab, transform.position, rotatetransform.transform.rotation);
            newBullet.GetComponent<Bullet>().owner = this;
            newBullet.transform.Rotate(rotatetransform.transform.rotation.x, rotatetransform.transform.rotation.x, Random.Range(1f, -1f));
            newBullet.GetComponent<Bullet>().speed = Random.Range(5 + 0.5f, 5 - 0.5f);
            GameMaster.instance.Play(shotSound, 0.2f, 0.1f * GameMaster.instance.saveState.soundVolume);
            StartCoroutine(Shake(0.1f));
            StartCoroutine(FirerateWait(0.05f, 1));

            curAmountToBlastAmmo += 1;
            if (curAmountToBlastAmmo >= amountToBlastAmmo)
            {
                blastAmmo += 1;
                curAmountToBlastAmmo = 0;
                ChangeBlastUI();
            }
        }
    }

    public void FireSpell2()
    {
        if(canFire1 == true & !pause & blastAmmo > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Rigidbody2D newBullet = Instantiate(bulletPrefab, transform.position, rotatetransform.transform.rotation);
                newBullet.GetComponent<Bullet>().owner = this;
                newBullet.transform.Rotate(rotatetransform.transform.rotation.x, rotatetransform.transform.rotation.x, Random.Range(10.0f, -10.0f));
                newBullet.GetComponent<Bullet>().speed = Random.Range(4 + 0.5f, 4 - 0.5f);
            }
            GameMaster.instance.Play(blastSound, 0.5f, 0.5f * GameMaster.instance.saveState.soundVolume);
            StartCoroutine(Shake(0.4f));
            StartCoroutine(FirerateWait(0.5f, 2));
            blastAmmo -= 1;
            ChangeBlastUI();
        }
    }

    public void Hit()
    {
        GameMaster.instance.EndGame();
    }

    public void ChangeBlastUI()
    {
        foreach (Transform child in pGUI.blastUI)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < blastAmmo; i++)
        {
            Instantiate(pGUI.blastBulletUI, pGUI.blastUI, false);
        }
    }

    IEnumerator FirerateWait(float firerate, int weaponNum)
    {
        if (weaponNum == 1)
        {
            canFire = false;
            yield return new WaitForSeconds(firerate);
            canFire = true;
        }
        else
        {
            canFire1 = false;
            Debug.Log("wait" + firerate);
            yield return new WaitForSeconds(firerate);
            canFire1 = true;
        }
    }

    IEnumerator Shake(float mag)
    {

        float elapsed = 0.0f;
        float duration = 0.1f;
        Vector3 originalCamPos = pCam.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 2.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            x = pCam.transform.position.x + Random.Range(-0.1f, 0.1f) * mag;          
            y = pCam.transform.position.y + Random.Range(-0.1f, 0.1f) * mag;

            Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

       // Camera.main.transform.position = originalCamPos;
    }

}