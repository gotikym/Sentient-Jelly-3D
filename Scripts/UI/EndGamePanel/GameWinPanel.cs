using UnityEngine;

public class GameWinPanel : EndGamePanel
{
    protected override void OnEnable()
    {
        Victory.LevelFinised += OpenPanel;
    }

    protected override void OnDisable()
    {
        Victory.LevelFinised -= OpenPanel;
    }
}