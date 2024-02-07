using UnityEngine;

public class MainMenuBootsTrap : BootsTrap
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private ListLevelDisplay _listLevelDisplays;
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

        InitializeShop();

        InitializeMusicVolume();
    }

    private void InitializeWallet()
    {
        _wallet = new(_persistentPlayerData);

        _walletView.Initialize(_wallet);
    }

    private void InitializeLevels()
    {
        _openLevels = new(_persistentPlayerData);
        _starsLevels = new(_persistentPlayerData);

        _listLevelDisplays.Initialize(_openLevels, _starsLevels);
    }

    private void InitializeShop()
    {
        OpenSkinsChecker openSkinsChecker = new(_persistentPlayerData);
        SelectedSkinChecker selectedSkinChecker = new(_persistentPlayerData);
        SkinSelector skinSelector = new(_persistentPlayerData);
        SkinUnlocker skinUnlocker = new(_persistentPlayerData);

        _shop.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
    }

    private void InitializeMusicVolume()
    {
        _musicVolume = new(_persistentPlayerData);
        _musicSwitch.Initialize(_musicVolume, _dataProvider);
    }
}