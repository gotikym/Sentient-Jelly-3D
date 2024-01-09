using UnityEngine;

public class ResetLevels : MonoBehaviour
{
    private const string UnlockedMapsKey = "unlockedMaps";
    private const string PointsKey = "Points";

    public void ResetLockOnRevels()
    {
        PlayerPrefs.DeleteKey(UnlockedMapsKey);
        PlayerPrefs.DeleteKey(PointsKey);
        PlayerPrefs.DeleteKey("Level1");
        PlayerPrefs.DeleteKey("Level2");
    }
}