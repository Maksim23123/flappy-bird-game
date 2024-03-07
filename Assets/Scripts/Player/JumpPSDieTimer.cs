using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPSDieTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DieTimer());
    }

    IEnumerator DieTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
