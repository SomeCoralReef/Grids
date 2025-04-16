using UnityEngine;

public class PlayerTimelineUnit : TimelineUnit
{
    private Player playerscript;

    private void Start()
    {
        isPlayerControlled = true;
        playerscript = GetComponent<Player>();
    }

    protected override void OnPrepare()
    {
        TimelineManager timelineManager = FindObjectOfType<TimelineManager>();
        timelineManager.isPaused = true;

        Debug.Log("Game paused for player to select ability.");

        // Show your ability + tile targeting UI here
    }

    protected override void OnExecute()
    {
        Debug.Log("Player is executing an action.");
        playerscript.ExecuteAttack();
    }

    void Update()
    {
        if (state == TimelineState.Preparing && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player confirmed action. Unpausing timeline.");
            TimelineManager timelineManager = FindObjectOfType<TimelineManager>();
            timelineManager.isPaused = false;

            BeginExecution(); // Player's icon now continues to 100%
        }
    }
}
