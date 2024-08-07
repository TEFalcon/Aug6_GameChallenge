using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public enum Sounds
    {
        CLICK,
        PICK_UP,
        GAMEOVER,
        COUNTDOWN
    }
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip[] audios;

    [SerializeField,Range(0f,1f)] private float volume = 1f;
    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.Instance.OnTouchPumpkin += PlayerScript_OnTouchPumpkin;
        PlayerScript.Instance.OnTouchEnemy += PlayerScript_OnTouchEnemy;

        GameManager.Instance.OnCountdownStart += GameManager_OnCountdownChange;
    }

    private void GameManager_OnCountdownChange(object sender, System.EventArgs e)
    {
        PlaySound(Sounds.COUNTDOWN, Vector3.zero);
    }

    private void PlayerScript_OnTouchEnemy(object sender, PlayerScript.OnObjecttouchEventArgs e)
    {
        PlaySound(Sounds.GAMEOVER, Vector3.zero);
    }

    private void PlayerScript_OnTouchPumpkin(object sender, PlayerScript.OnObjecttouchEventArgs e)
    {
        PlaySound(Sounds.PICK_UP, PlayerScript.Instance.transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound(Sounds.CLICK, Vector3.zero);
        }
    }


    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
            volume = 0f;


        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    private void PlaySound(Sounds sound, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audios[(int)sound], position, volumeMultiplier * volume);
    }
    //private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    //{
    //    PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    //}
    public float GetVolume() { return volume; }
}
