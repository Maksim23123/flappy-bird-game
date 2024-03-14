using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrameAnimation : FrameAnimation
{
    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        InitAnimation();
        player.JumpAction += OnJump;
    }

    void OnJump()
    {
        StartAnimation();
    }
}
