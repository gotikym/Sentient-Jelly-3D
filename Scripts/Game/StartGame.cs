using UnityEngine;

public class StartGame : MonoBehaviour
{
    private const string UnlockedMapsKey = "unlockedMaps";
    private const int FirstMapIndex = 1;

    private void Start()
    {
        if (PlayerPrefs.GetInt(UnlockedMapsKey) < FirstMapIndex)
            PlayerPrefs.SetInt(UnlockedMapsKey, FirstMapIndex);
    }
}
