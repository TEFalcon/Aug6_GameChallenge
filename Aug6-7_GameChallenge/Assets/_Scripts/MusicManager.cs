using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Owned by EyalK
public class MusicManager : MonoBehaviour
{

    //Varriables:
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    [SerializeField,Range(0f,0.5f)] private float volume =0.3f;

    private void Awake()
    {
        Instance= this;

        audioSource= GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume;
    }
    public void ChangeVolume() {
        volume += 0.1f;
        if(volume > 0.5f) volume = 0f;

        audioSource.volume = volume;


        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()  { return volume;  }


    public void ChangeState(bool state)
    {
        audioSource.enabled= state;
    }
}
