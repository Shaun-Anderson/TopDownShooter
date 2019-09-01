using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState {

    public List<int> unlockedPerks = new List<int>();
    public int highscore;
    public int mostKills;

    //Options
    public int soundVolume;
    public int musicVolume;

}
