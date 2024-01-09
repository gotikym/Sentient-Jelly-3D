using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelDisplay))]
public class ChooseLvl : MonoBehaviour
{
    [SerializeField] private LevelDisplay _levelDislay;

    private const string UnlockedMapsKey = "unlockedMaps";

    private int _runningTimeScale = 1;

    private int _mapIndex;

    private void OnValidate()
    {
        _levelDislay ??= GetComponent<LevelDisplay>();
    }

    private void Start()
    {
        _mapIndex = _levelDislay.Level;
    }

    public void OpenLevel()
    {
        if (PlayerPrefs.GetInt(UnlockedMapsKey) >= _mapIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _mapIndex);
            Time.timeScale = _runningTimeScale;
        }
    }
}