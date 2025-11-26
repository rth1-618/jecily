using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        //Debug.Log("Attack Over");
        player.CallAnimationTrigger();

    }

   
}
