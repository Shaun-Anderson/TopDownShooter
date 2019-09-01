using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LvlController : MonoBehaviour {

    public float time;
    public float timeToSpawn;

    public GameObject[] spawn;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public Sprite mouse;

    public List<GameObject> Spawns = new List<GameObject>();

    //Level Up
    public Transform levelupGUI;
    public Transform levelUpList;
    public Text GUIName;
    public Text GUIDesc;

    public GameObject perkInfoList;
    public GameObject perkInfoUI;

    public GameObject scoreUI;
    public GameObject leaderboardUIList;

    public Button pauseStart;

    public Transform EndScreen;
    public Button endScreenStart;
    public Text scoreNumUI;
    public Text killsNumUI;
    public Text timeNumUI;

    void Start()
    {
        GameMaster.instance.lvlMan = this;
    }

    // Use this for initialization
    void Spawn () {
        float x = Random.Range(minX,maxX);
        float y = Random.Range(minY, maxY);

        int rand = Random.Range(0, spawn.Length);
        GameObject newObj = Instantiate(spawn[rand], new Vector2(x, y), Quaternion.identity);

        Spawns.Add(newObj);
        
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameMaster.instance.paused)
        {
            time += Time.deltaTime;

            if (time >= timeToSpawn)
            {
                Spawn();
                time = 0;
            }
        }
	}

    public void Restart()
    {
        GameMaster.instance.paused = true;

        SceneManager.LoadScene("Game");
    }

    public void MoveToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Hover(int num)
    {
        GUIName.text = GameMaster.instance.curItem[num].perkName;
        GUIDesc.text = GameMaster.instance.curItem[num].desc;
    }
    public void Click(int num)
    {
        GameMaster.instance.curItem[num].Add(GameMaster.instance.pHand);
        levelupGUI.gameObject.SetActive(false);
        StartCoroutine(GameMaster.instance.CountDown(3));

        //clear
        GameMaster.instance.curItem.Clear();

        GameMaster.instance.CurXP = GameMaster.instance.CurXP - GameMaster.instance.MaxXP;
        GameMaster.instance.MaxXP = GameMaster.instance.MaxXP * 2;
        GameMaster.instance.pHand.pGUI.XPBar.transform.localScale = new Vector3(0, GameMaster.instance.pHand.pGUI.XPBar.transform.localScale.y, GameMaster.instance.pHand.pGUI.XPBar.transform.localScale.z);
    }
}
