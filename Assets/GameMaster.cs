using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMaster : MonoBehaviour
{

    public static GameMaster instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [Header("Menu References")]
    public List<Perk> perkPool = new List<Perk>();
    public SaveState saveState;


    public bool paused = true;

    public List<Perk> perks = new List<Perk>();

    public int numbr;

    [Header("Game References")]
    public List<Perk> curItem = new List<Perk>();

    public Transform player;

    public int score;
    public int kills;

    public float CurXP;
    public float MaxXP;

    public PlayerHandler pHand;
    public Databases DB;
    public SoundManager soundMan;
    public LvlController lvlMan;

    public bool windowed;

    //LEADERBOARD
    public string playerName = "ANON";
    public dreamloLeaderBoard leaderBoard;
    public GameObject UIPlayerScore;
    public List<dreamloLeaderBoard.Score> scoreList = new List<dreamloLeaderBoard.Score>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        soundMan = GetComponent<SoundManager>();
        DB = GetComponent<Databases>();

        if (File.Exists(Application.persistentDataPath + "/data.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.gd", FileMode.Open);
            instance.saveState = (SaveState)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            //Set save inital values
            instance.saveState.highscore = 0;
            instance.Save();
        }
        leaderBoard = GetComponent<dreamloLeaderBoard>();
        SceneManager.activeSceneChanged += NewScene;
    }

    public IEnumerator UploadScore()
    {
        leaderBoard.AddScore(playerName, score, (int)pHand.pGUI.timer ,kills.ToString());
        yield break;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.gd");
        bf.Serialize(file, instance.saveState);
        file.Close();

        print("SAVE");
    }

    //Audio
    public void Play(AudioClip clip, float pitch, float volume)
    {
        soundMan.Play(clip, pitch, volume);
    }
    public void PlayMenuSound(AudioClip clip)
    {
        soundMan.Play(clip, 3, 3);
    }

    //Game Changes (Pause & End)
    public void Pause()
    {

        if(!paused)
        {
            paused = true;
            pHand.pGUI.pauseMenu.SetActive(true);
            lvlMan.pauseStart.Select();
            for (int i = 0; i < perks.Count; i++)
            {
                GameObject newInfo = Instantiate(lvlMan.perkInfoUI, transform.position, Quaternion.identity);
                newInfo.GetComponentInChildren<Text>().text = perks[i].perkName;
                newInfo.transform.SetParent(lvlMan.perkInfoList.transform, false);
            }

        }
        else
        {
            pHand.pGUI.pauseMenu.SetActive(false);

            foreach (Transform child in lvlMan.perkInfoList.transform)
            {
                Destroy(child.gameObject);
            }

            StartCoroutine(CountDown(3));

        }
    }
    public IEnumerator CountDown(float amount)
    {
        pHand.pGUI.countDown.gameObject.SetActive(true);
        float currCountdownValue = amount;
        while (currCountdownValue > 0)
        {
            pHand.pGUI.countDown.text = currCountdownValue.ToString("0");
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        pHand.pGUI.countDown.gameObject.SetActive(false);
        paused = false;
    }

    public void EndGame()
    {
        instance.paused = true;

        //Set stats on UI
        lvlMan.scoreNumUI.text = score.ToString("00000000");
        lvlMan.killsNumUI.text = kills.ToString();
        lvlMan.timeNumUI.text = pHand.pGUI.timer.ToString("00:00.0");

        lvlMan.endScreenStart.Select();
        lvlMan.EndScreen.gameObject.SetActive(true);

        StartCoroutine(UploadScore());
        if (score > instance.saveState.highscore)
        {
            
            saveState.highscore = score;
            instance.Save();
            print("New Highscore");

        }
    }

    public void LoadHighscore()
    {
        scoreList = leaderBoard.ToListHighToLow();
        print(scoreList.Count);

        int maxToDisplay = 20;
        int count = 0;
        foreach (dreamloLeaderBoard.Score currentScore in scoreList)
        {

            print(currentScore.score);
            count++;
            GameObject newScore = Instantiate(lvlMan.scoreUI, transform.position, Quaternion.identity);
            newScore.GetComponent<UI_Score>().player_Name.text = currentScore.playerName;
            newScore.GetComponent<UI_Score>().player_Score.text = currentScore.score.ToString();
            newScore.GetComponent<UI_Score>().player_Time.text = currentScore.seconds.ToString();
            newScore.GetComponent<UI_Score>().player_Kills.text = currentScore.shortText;
            newScore.transform.SetParent(lvlMan.leaderboardUIList.transform, false);

            if (count >= maxToDisplay) break;
        }

    }


    public void AddScore(int amount)
    {
        instance.score += amount;
        //pHand.pGUI.scoreText.text = score.ToString("00000000");
    }

    //Update is called every frame.
    public void ExpGain(int amount)
    {
        CurXP += amount;
        float calcXP = CurXP / MaxXP;

        pHand.pGUI.XPBar.transform.localScale = new Vector3(calcXP, pHand.pGUI.XPBar.transform.localScale.y, pHand.pGUI.XPBar.transform.localScale.z);

        if(CurXP >= MaxXP)
        {
            LevelUp();
        }
    }

    //LevelUp
    public void LevelUp()
    {
        instance.paused = true;


        List<Transform> GUIStuff = new List<Transform>();

        foreach (Transform child in lvlMan.levelUpList)
        {
            if(pHand.perk_PerkUp)
            {
                child.gameObject.SetActive(true);
            }

            numbr = Random.Range(0, DB.perkDB.Count);

            if(curItem.Count == 0)
            {
                curItem.Add(DB.perkDB[numbr]);
            }

            if (curItem.Count == 1)
            {
                while (DB.perkDB[numbr] == curItem[0])
                {
                    numbr = Random.Range(0, DB.perkDB.Count);
                }
                curItem.Add(DB.perkDB[numbr]);
            }

            if (curItem.Count == 2)
            {
                while (DB.perkDB[numbr] == curItem[0] || DB.perkDB[numbr] == curItem[1])
                {
                    numbr = Random.Range(0, DB.perkDB.Count);
                }
                curItem.Add(DB.perkDB[numbr]);
            }

            if (curItem.Count == 3)
            {
                while (DB.perkDB[numbr] == curItem[0] || DB.perkDB[numbr] == curItem[1] || DB.perkDB[numbr] == curItem[2])
                {
                    numbr = Random.Range(0, DB.perkDB.Count);
                }
                curItem.Add(DB.perkDB[numbr]);
            }

            GUIStuff.Add(child);

        }
        lvlMan.levelupGUI.gameObject.SetActive(true);

        if (Input.GetJoystickNames().Length != 0)
        {
            if (Input.GetJoystickNames()[0] != "")
            {
                GUIStuff[0].GetComponent<Button>().Select();
                lvlMan.GUIName.text = curItem[0].perkName;
                lvlMan.GUIDesc.text = curItem[0].desc;
            }
        }
        
    }

    void NewScene(Scene previousScene, Scene newScene)
    {
        Scene scene = SceneManager.GetActiveScene();
        print(scene.name);
        switch(scene.name)
        {
            case "Menu":
                break;

            case "Game":
                StartCoroutine(InitGame());
                break;
        }
    }

    public IEnumerator InitGame()
    {
        Debug.Log("hihihih");
        yield return new WaitForSeconds(1.0f);
        instance.paused = false;
    }

}
