using System.Collections.Generic;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _focusImage;
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private int _level;

    private const string UnlockedMapsKey = "unlockedMaps";
    private const string NameLevel = "Level";

    private string _nameLevel;
    private int _starsCount;

    public int Level => _level;

    private void OnEnable()
    {
        StartSettingsDisplay();
    }

    private void DisplayLevel()
    {
        if (PlayerPrefs.GetInt(UnlockedMapsKey) < _level)
        {
            _lockImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(UnlockedMapsKey) >= _level)
        {
            _lockImage.SetActive(false);
            _focusImage.SetActive(true);
        }
    }

    private void SetStars()
    {
        if (_starsCount >= 0)
            for (int i = 0; i < _starsCount; i++)
                _stars[i].SetActive(true);
    }

    private void OffAllStars()
    {
        foreach (var star in _stars)
            star.SetActive(false);
    }

    private void StartSettingsDisplay()
    {
        OffAllStars();

        _nameLevel = NameLevel + _level;
        _focusImage.SetActive(false);

        DisplayLevel();

        _starsCount = PlayerPrefs.GetInt(_nameLevel);

        SetStars();
    }
}