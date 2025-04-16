using UnityEngine;

public class TimelineUnit : MonoBehaviour
{
    public float speed = 0.2f; // units per second along timeline
    public float timelineProgress = 0f; // 0 to 1
    public TimelineState state = TimelineState.Idle;
    protected bool isPlayerControlled = false;

    public virtual void UpdateTimeline()
    {
        if (TimelineManagerIsPaused()) return;

        switch (state)
        {
            case TimelineState.Idle:
                timelineProgress += speed * Time.deltaTime;
                if (timelineProgress >= 0.75f)
                {
                    timelineProgress = 0.75f;
                    state = TimelineState.Preparing;
                    OnPrepare();
                }
                break;

            case TimelineState.Executing:
                timelineProgress += speed * Time.deltaTime;
                if (timelineProgress >= 1.0f)
                {
                    timelineProgress = 1.0f;
                    OnExecute();
                    ResetTimeline();
                }
                break;
        }
    }

    private bool TimelineManagerIsPaused()
    {
        return FindObjectOfType<TimelineManager>().isPaused;
    }
    protected virtual void OnPrepare()
    {
        // Player or enemy behavior on reaching 75%
    }

    protected virtual void OnExecute()
    {
        // Final action when timeline completes
    }

    protected void ResetTimeline()
    {
        timelineProgress = 0f;
        state = TimelineState.Idle;
    }

    public void BeginExecution()
    {
        state = TimelineState.Executing;
    }
}
