using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _imageMusicOn;
    [SerializeField] private GameObject _imageMusicOff;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioSource _audioSource;

    private const int ValueMusicOn = 1;
    private const int ValueMusicOff = 0;

    private int newTrackDelay = 0;

    private bool _isMusicOn;
    private MusicVolume _musicVolume;
    private IDataProvider _dataProvider;

    private void Start()
    {
        DontDestroyOnLoad(this);
        _isMusicOn = true;
        StartCoroutine(PlayBackgroudMusic());
    }

    public void Initialize(MusicVolume musicVolume, IDataProvider dataProvider)
    {
        _musicVolume = musicVolume;
        _dataProvider = dataProvider;
    }

    private void Update()
    {
        //сделать отдельный класс с статическими евентами, и по ним запускать методы, а не ифами в апдейте
        if (_musicVolume.GetCurrentMusicVolume() == ValueMusicOff)
        {
            OnMusicVolumeOff();
        }
        else if (_musicVolume.GetCurrentMusicVolume() == ValueMusicOn)
        {
            OnMusicVolumeOn();
        }
    }

    private void OnMusicVolumeOn()
    {
        _imageMusicOff.SetActive(false);
        _imageMusicOn.SetActive(true);
        AudioListener.volume = ValueMusicOn;
        _isMusicOn = false;
    }

    private void OnMusicVolumeOff()
    {
        _imageMusicOn.SetActive(false);
        _imageMusicOff.SetActive(true);
        AudioListener.volume = ValueMusicOff;
        _isMusicOn = true;
    }

    public void SwitchMusicPlaying()
    {
        if (_isMusicOn)
            _musicVolume.SetVolumeOn(ValueMusicOn);
        else if (_isMusicOn == false)
            _musicVolume.SetVolumeOff(ValueMusicOff);

        _dataProvider.Save();
    }

    IEnumerator PlayBackgroudMusic()
    {
        int musicIndex = 0;

        while (_audioClips.Count > 0)
        {
            float waitTime = _audioClips[musicIndex].length + newTrackDelay;

            _audioSource.PlayOneShot(_audioClips[musicIndex]);

            musicIndex++;

            if (musicIndex >= _audioClips.Count)
                musicIndex = 0;

            yield return new WaitForSeconds(waitTime);
        }
    }
}