using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioProperties
{
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    public static float MasterVolume
    {
        get { return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f); }
        set
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
            PlayerPrefs.Save();



        }
    }

    public static float MusicVolume
    {
        get { return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f); }
        set
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
            PlayerPrefs.Save();


        }
    }
}
