using UnityEngine;
using System.Collections;

public class PlayerAniControl: MonoBehaviour
{
    public PlayerInput pInput;

    public Transform center;

    public Rigidbody playerRigid;
    public Transform player;

    public Animator playerAni;
    public SpriteRenderer sprite;
    public float x;
    public float y;
    Vector3 tempPos;
    public float speed;
    public float amplitude;
    public Sprite[] IdleSprites;
    public float minimum = -1.0F;
    public float maximum = 1.0F;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerAni = GetComponentInChildren<Animator>();
        pInput = GetComponent<PlayerInput>();

    }
    IEnumerator Switch()
    {
        float tempval;
        tempval = minimum;
        minimum = maximum;
        maximum = tempval;
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        sprite.transform.localPosition = new Vector3(0, Mathf.PingPong(Time.time * amplitude, 0.01f), sprite.transform.position.z);


    }
    public void SpriteAngle(float angle)
    {
        if(angle <= 22.5f && angle >= 0 || angle >= -22.25 & angle <=0)
        {
            x = 1;
            y = 0;
            playerAni.SetFloat("Right", x);
            playerAni.SetFloat("Up", y);
            sprite.sprite = IdleSprites[12];
        }
        if (angle > 11.25 && angle <= 33.75)
        {
            //Debug.Log("RightTop1");
            x = 1;
            y = 0.5f;
            playerAni.SetFloat("Right", x);
            playerAni.SetFloat("Up", y);
        }
        if (angle > 33.75 && angle <= 56.25)
        {
            //Debug.Log("RightTop2");
            x = 1;
            y = 1;
            playerAni.SetFloat("Right", x);
            playerAni.SetFloat("Up", y);
        }
        if (angle > 56.25 && angle <= 78.75)
        {
            //Debug.Log("RightTop3");
            x = 0.5f;
            y = 1;
            playerAni.SetFloat("Right", x);
            playerAni.SetFloat("Up", y);
        }
        if (angle > 78.75 && angle <= 101.25)
        {
            //Debug.Log("Top");
            playerAni.SetFloat("Right", 0);
            playerAni.SetFloat("Up", 1);
            sprite.sprite = IdleSprites[8];
        }
        if (angle > 101.25 && angle <= 123.75)
        {
            //Debug.Log("LeftTop3");
            playerAni.SetFloat("Right", -0.5f);
            playerAni.SetFloat("Up", 1);
        }
        if (angle > 123.75 && angle <= 146.25)
        {
            //Debug.Log("LeftTop2");
            playerAni.SetFloat("Right", -1f);
            playerAni.SetFloat("Up", 1);
        }
        if (angle > 146.25 && angle <= 168.75)
        {
            //Debug.Log("LeftTop1");
            playerAni.SetFloat("Right", -1f);
            playerAni.SetFloat("Up", 0.5f);
        }
        if (angle > 168.75 && angle <= 191.25)
        {
            //Debug.Log("Left");
            playerAni.SetFloat("Right", -1f);
            playerAni.SetFloat("Up", 0f);
            sprite.sprite = IdleSprites[4];
        }
        if (angle > 191.25 && angle <= 213.75)
        {
            //Debug.Log("LeftBot1");
            playerAni.SetFloat("Right", -1f);
            playerAni.SetFloat("Up", -0.5f);
        }
        if (angle > 213.75 && angle <= 236.25)
        {
            //Debug.Log("LeftBot2");
            playerAni.SetFloat("Right", -1f);
            playerAni.SetFloat("Up", -1f);
        }
        if (angle > 236.25 && angle <= 258.75)
        {
            //Debug.Log("LeftBot3");
            playerAni.SetFloat("Right", -0.5f);
            playerAni.SetFloat("Up", -1f);
        }
        if (angle < -78.75 && angle >= -101.25)
        {
            //Debug.Log("Bot");
            playerAni.SetFloat("Right", 0);
            playerAni.SetFloat("Up", -1);
            sprite.sprite = IdleSprites[0];
        }
        if (angle > 281.25 && angle <= 303.75)
        {
            //Debug.Log("RightBot3");
            playerAni.SetFloat("Right", 0.5f);
            playerAni.SetFloat("Up", -1f);
        }
        if (angle > 303.75 && angle <= 326.25)
        {
            //Debug.Log("RightBot2");
            playerAni.SetFloat("Right", 1f);
            playerAni.SetFloat("Up", -1f);
        }
        if (angle > 326.25 && angle <= 348.75)
        {
            //Debug.Log("RightBot1");
            playerAni.SetFloat("Right", 1f);
            playerAni.SetFloat("Up", -0.5f);
        }
    }
}
