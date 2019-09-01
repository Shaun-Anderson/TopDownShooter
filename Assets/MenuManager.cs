using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject optionMenu;
    public Transform startingButton;

    public Text highscoreText;
    public Text mostTimeText;

    public Toggle toggle_Windowed;
    public Dropdown drop_Resolution;

	// Use this for initialization
	void Start () {
        if (Input.GetJoystickNames().Length != 0)
        {
            if (Input.GetJoystickNames()[0] != "")
            {
                Button button = startingButton.GetComponent<Button>();
                button.Select();
            }
        }

        highscoreText.text = GameMaster.instance.saveState.highscore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        optionMenu.SetActive(true);
        toggle_Windowed.isOn = GameMaster.instance.windowed;
        toggle_Windowed.onValueChanged.AddListener(delegate { Windowed(); });
    }

    public void SwitchResolution( Dropdown drop)
    {
        int width = int.Parse(drop.captionText.text.Split(new char[] { 'X' }, System.StringSplitOptions.None)[0]);
        print(width);
        int height = int.Parse(drop.captionText.text.Split(new char[] { 'X' }, System.StringSplitOptions.None)[1]);
        print(height);
        Screen.SetResolution(width, height, GameMaster.instance.windowed);
    }

    public void Windowed()
    {
        GameMaster.instance.windowed = !GameMaster.instance.windowed;
        Screen.SetResolution(Screen.width, Screen.height, GameMaster.instance.windowed);
    }
}
