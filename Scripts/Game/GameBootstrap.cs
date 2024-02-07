using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : BootsTrap
{
    [SerializeField] private Victory _victory;
    [SerializeField] private BordersMaterial _bordersMaterial;
    [SerializeField] private MusicSwitch _musicSwitch;

    private Wallet _wallet;
    private OpenLevels _openLevels;
    private StarsLevels _starsLevels;
    private MusicVolume _musicVolume;

    protected override void Awake()
    {
        base.Awake();

        InitializeWallet();

        InitializeLevels();

        InitializeVictory();

        InitializeBorders();

        InitializeMusicVolume();
    }

    private void InitializeWallet()
    {
        _wallet = new (_persistentPlayerData);
    }

    private void InitializeLevels()
    {
        _openLevels = new (_persistentPlayerData);
        _starsLevels = new (_persistentPlayerData);
    }

    private void InitializeVictory()
    {
        _victory.Initialize(_dataProvider, _wallet, _openLevels, _starsLevels);
    }

    private void InitializeBorders()
    {
        BorderSkins borderSkins = _persistentPlayerData.PlayerData.SelectedBorderSkin;

        _bordersMaterial.Initialize(borderSkins);
    }

    private void InitializeMusicVolume()
    {
        _musicVolume = new(_persistentPlayerData);
        _musicSwitch.Initialize(_musicVolume, _dataProvider);
    }
}