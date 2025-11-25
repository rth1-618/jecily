using UnityEngine;

public class Timer : MonoBehaviour
{

    public float stateTimer;
    public float stateDuration = 10;
    void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    [ContextMenu("Set Timer")]
    public void setTimer()
    {
        stateTimer = stateDuration;
    }
}
