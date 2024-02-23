using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // External parameters
    [SerializeField]
    private Transform lookAtPoint;


    // Internal parameters

    [SerializeField]
    private float jumpHeight = 3f;
    public event EventHandler raise;
    private float startGravityDelay = 0.1f;
    private float lookAtPointDistance = 5f;
    private float lookAtPointRangeScale = 0.1f;
    private string scoreTriggerTag = "ScoreTrigger";

    // Debug parameters

    [SerializeField]
    private TextMeshProUGUI gravity;
    [SerializeField]
    private TextMeshProUGUI timeScale;

    // Buffers
    private bool jump;
    private Rigidbody rb;
    private bool startGravityDelayPassed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(RoundStats.GameScore);

        GetPlayerInput();


        if (Wall.WallNearPlayer && startGravityDelayPassed)
        {
            rb.useGravity = true;
        }
        else if (Wall.WallNearPlayer && !startGravityDelayPassed)
        {
            StartCoroutine(StartGravityDelay());
        }
        else
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }


        if (jump)
        {
            rb.velocity = Vector3.up * jumpHeight;
            jump = false;
        }

        UpdateLookAtPoint();
    }

    private void UpdateLookAtPoint()
    {
        lookAtPoint.position = transform.position + Vector3.up * rb.velocity.y * Mathf.Abs(rb.velocity.y) * lookAtPointRangeScale 
            + Vector3.right * lookAtPointDistance;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        raise?.Invoke(gameObject, new ChangeTimeArgs(0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == scoreTriggerTag)
        {
            RoundStats.ImproveScore();
        }
    }

    void GetPlayerInput()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            jump = true;
        }

        if (Input.touchCount > 0)
        {
            jump = true;
        }
    }
    
    IEnumerator StartGravityDelay()
    {
        yield return new WaitForSeconds(startGravityDelay);
        startGravityDelayPassed = true;
    }
}
