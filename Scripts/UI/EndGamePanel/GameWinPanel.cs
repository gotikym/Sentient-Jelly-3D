using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinPanel : EndGamePanel
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Animator _animator;

    private const string animationName = "RotateWinPanel";

    protected override void OnEnable()
    {
        Victory.LevelFinised += OpenPanel;
    }

    protected override void OnDisable()
    {
        Victory.LevelFinised -= OpenPanel;
    }

    protected override void OpenPanel()
    {
        base.OpenPanel();

        Time.timeScale = RunningTimeScale;
        
        _animator.Play(animationName);
        
        StartCoroutine(WaitEndAnimation());
    }

    private IEnumerator WaitEndAnimation()
    {
        _timer.ResetTime();
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        Time.timeScale = StoppedTimeScale;
    }
}