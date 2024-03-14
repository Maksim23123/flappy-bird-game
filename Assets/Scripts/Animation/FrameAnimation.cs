using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FrameAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject[] frames;

    [SerializeField]
    private float transitionTime = 0.1f;
    [SerializeField]
    private bool loop = true, playReverse = true;

    private bool IsReverseNow { get; set; }

    private bool IsPlaying { get; set; }

    private int currentFrame = 0;

    private void Start()
    {
        InitAnimation();
    }

    protected void InitAnimation()
    {
        foreach (var frame in frames)
        {
            frame.SetActive(false); 
        }
        PerformTransition();
    }

    private void PerformTransition()
    {
        int previousFrame = currentFrame;

        if (IsReverseNow)
            currentFrame--;
        else
            currentFrame++;

        if (currentFrame >= frames.Length && playReverse)
        {
            IsReverseNow = true;
            currentFrame -= 2;
        }
        else if (currentFrame < 0 && loop)
        {
            IsReverseNow = false;
            currentFrame += 2;
        }

        if (currentFrame >= 0 && currentFrame < frames.Length)
        {
            IsPlaying = true;
            Invoke("PerformTransition", transitionTime);
        }
        else
        {
            IsPlaying = false;
        }

        if (currentFrame >= 0 && currentFrame < frames.Length)
        {
            if (previousFrame >= 0 && previousFrame < frames.Length)
                frames[previousFrame].SetActive(false);
            frames[currentFrame].SetActive(true);
        }
        else if (previousFrame >= 0 && previousFrame < frames.Length)
        {
            currentFrame = previousFrame;
            frames[currentFrame].SetActive(true);
        }
    }

    protected void StartAnimation()
    {
        if (!IsPlaying)
        {
            IsReverseNow = false;
            currentFrame = -1;
            PerformTransition();
        }
    }
}
