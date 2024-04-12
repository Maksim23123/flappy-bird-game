using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecutor : MonoBehaviour
{
    public Player player;

    public float PlayerSpeed
    {
        get => player.Speed;

        set => player.Speed = value;
    }

    public void ResetPlayerSpeed() => player.ResetSpeed(); 

    // Start is called before the first frame update
    void Start()
    {
        Services.PlayerExecutor = this;
    }

    public void SetPlayerState(PlayerStates playerState)
    {
        player.PlayerState = playerState;
    }

    
}
