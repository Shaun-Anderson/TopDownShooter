using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGui : MonoBehaviour
{
    public PlayerStats pStats;
    //public GameObject healthBar;
    //public GameObject healthText;

    public Image XPBar;

    public GameObject Fire1Icon;
    public Sprite Fire1IconSprite;
    float calc_health;

    public Text scoreText;
    public int displayScore;

    public GameObject pauseMenu;

    public float timer;
    public Text timerText;

    public Text countDown;

    public Transform blastUI;
    public Transform blastBulletUI;

    void Start()
    {
        pStats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        if (!GameMaster.instance.paused)
        {
            timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
            timerText.text = timer.ToString("00:00.0");

            if(displayScore != GameMaster.instance.score)
            {
                if(scoreText.fontSize != 20)
                {
                    scoreText.color = Color.red;
                    scoreText.fontSize++;
                }
                displayScore += 10;
                scoreText.text = displayScore.ToString("00000000");
            }
            else if (scoreText.fontSize != 14)
            {
                scoreText.color = Color.white;
                scoreText.fontSize--;
            }
        }
    }

}
