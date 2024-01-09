using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private List<Block> _allBlock;

    private bool _isFinish;
    private float _timeForOneStart;
    private float _timeForTwoStart;
    private string _mapMame;
    private int _currentStarsCount;
    private int _pointsForLevel;
    private int _previosStarsCount;

    private const string UnlockedMapsKey = "unlockedMaps";

    private const int OneStar = 1;
    private const int TwoStar = 2;
    private const int ThreeStar = 3;

    private const int FirstMapIndex = 1;
    private const int multiplierTwo = 2;
    private const int multiplierThree = 3;

    private const int PointsForOneStar = 500;
    private const int PointsForTwoStar = 1000;
    private const int PointsForThreeStar = 1500;

    public static event Action LevelFinised;
    public event Action<int> StarsCalculated;

    private void Start()
    {
        _mapMame = SceneManager.GetActiveScene().name;
        Jelly.JellyFilled += JellyFill;
    }

    private void OnDestroy()
    {
        Jelly.JellyFilled -= JellyFill;
    }

    private void JellyFill()
    {
        for (int i = 0; i < _allBlock.Count; i++)
        {
            if (_allBlock[i].IsJelly == false)
            {
                _isFinish = false;
                break;
            }
            else
            {
                _isFinish = true;
            }
        }

        if (_isFinish)
            Finish();
    }

    private int CalculatePoints()
    {
        _timeForOneStart = _timer.TimeStart / multiplierThree;
        _timeForTwoStart = _timeForOneStart * multiplierTwo;

        if (_timer.CurrentTime <= _timeForOneStart)
            return PointsForOneStar;
        else if (_timer.CurrentTime <= _timeForTwoStart)
            return PointsForTwoStar;
        else if (_timer.CurrentTime > _timeForTwoStart)
            return PointsForThreeStar;
        else
            return 0;        
    }

    private void CalculateStars()
    {
        _previosStarsCount = PlayerPrefs.GetInt(_mapMame);
        _pointsForLevel = CalculatePoints();

        switch (_pointsForLevel)
        {
            case PointsForOneStar: _currentStarsCount = OneStar; break;
            case PointsForTwoStar: _currentStarsCount = TwoStar; break;
            case PointsForThreeStar: _currentStarsCount = ThreeStar; break;
        }
    }

    private void Finish()
    {
        LevelFinised?.Invoke();
        TrySaveStars();
        TrySavePoints();
        StarsCalculated?.Invoke(_currentStarsCount);
        PlayerPrefs.SetInt(UnlockedMapsKey, SceneManager.GetActiveScene().buildIndex + FirstMapIndex);
    }

    private void TrySavePoints()
    {
        if (_currentStarsCount - _previosStarsCount > 0)
        {
            int bousPoints = PointsForOneStar * (_currentStarsCount - _previosStarsCount);
            _player.AddPoints(bousPoints);
        }
    }

    private void TrySaveStars()
    {
        CalculateStars();

        if (_currentStarsCount > PlayerPrefs.GetInt(_mapMame))
            PlayerPrefs.SetInt(_mapMame, _currentStarsCount);
    }
}