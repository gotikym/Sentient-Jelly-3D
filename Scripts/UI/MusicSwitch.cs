using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _imageMusicOn;
    [SerializeField] private GameObject _imageMusicOff;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioSource _audioSource;
    //[SerializeField] private Image _musicButtonImage;
    //[SerializeField] private AudioMixerGroup _mixer;
    //[SerializeField] private Slider _musicSlider;
    //[SerializeField] private Slider _sfxSlider;

    private const string NameKeyMusic = "music";
    //private const string NameMixerGroupMusic = "MusicVolume";
    //private const string NameMixerGroupEffect = "EffectVolume";
    //private const float MinVolumeMixer = -80f;
    //private const float MaxVolumeMixer = 0f;
    private const int ValueMusicOn = 1;
    private const int ValueMusicOff = 0;

    private int newTrackDelay = 0;

    private bool _isMusicOn;

    private void Start()
    {
        _isMusicOn = true;
        StartCoroutine(PlayBackgroudMusic());
        //_musicSlider.value = PlayerPrefs.GetFloat(NameMixerGroupMusic);
        //_sfxSlider.value = PlayerPrefs.GetFloat(NameMixerGroupEffect);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(NameKeyMusic) == ValueMusicOff)
        {
            _imageMusicOn.SetActive(false);
            _imageMusicOff.SetActive(true);
            AudioListener.volume = ValueMusicOff;
            _isMusicOn = true;
        }
        else if (PlayerPrefs.GetInt(NameKeyMusic) == ValueMusicOn)
        {
            _imageMusicOff.SetActive(false);
            _imageMusicOn.SetActive(true);
            AudioListener.volume = ValueMusicOn;
            _isMusicOn = false;
        }
    }

    public void SwitchMusicPlaying()
    {
        if (_isMusicOn)
            PlayerPrefs.SetInt(NameKeyMusic, ValueMusicOn);
        else if (_isMusicOn == false)
            PlayerPrefs.SetInt(NameKeyMusic, ValueMusicOff);
    }

    /*public void ChangeVolumeMusic(float volume)
    {
        _mixer.audioMixer.SetFloat(NameMixerGroupMusic, Mathf.Lerp(MinVolumeMixer, MaxVolumeMixer, volume));

        PlayerPrefs.SetFloat(NameMixerGroupMusic, volume);
    }

    public void ChangeVolumeSFX(float volume)
    {
        _mixer.audioMixer.SetFloat(NameMixerGroupEffect, Mathf.Lerp(MinVolumeMixer, MaxVolumeMixer, volume));

        PlayerPrefs.SetFloat(NameMixerGroupEffect, volume);
    }*/

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