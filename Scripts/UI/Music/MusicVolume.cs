public class MusicVolume
{
    private IPersistentData _persistentData;

    public MusicVolume(IPersistentData persistentData) => _persistentData = persistentData;

    public int GetCurrentMusicVolume() => _persistentData.PlayerData.MusicVolume;

    public void SetVolumeOn(int volume)
    {
        _persistentData.PlayerData.MusicVolume = volume;
    }

    public void SetVolumeOff(int volume)
    {
        _persistentData.PlayerData.MusicVolume = volume;
    }
}