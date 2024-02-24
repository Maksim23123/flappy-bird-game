using System;
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
    [SerializeField]
    private GameObject playerMesh;
    [SerializeField]
    private GameObject dieParticleSystem;
    [SerializeField]
    private LevelManager levelManager;


    // Internal parameters

    [SerializeField]
    private float speed = 5f;
    private float jumpHeight = 5f;
    public event Action onGameOverEvent;
    private float lookAtPointDistance = 5f;
    private float lookAtPointRangeScale = 0.1f;
    private string scoreTriggerTag = "ScoreTrigger";

    // Buffers
    private bool jump;
    private Rigidbody rb;

    void Start()
    {
        levelManager.onChangeGameState += OnChangeGameState;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);


        GetPlayerInput();

        if (jump)
        {
            rb.velocity = Vector3.up * jumpHeight;
            jump = false;
        }

        UpdateLookAtPoint();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerMesh.SetActive(false);
        dieParticleSystem.SetActive(true);
        dieParticleSystem.GetComponent<ParticleSystem>().Play();
        onGameOverEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == scoreTriggerTag)
        {
            RoundStats.ImproveScore();
        }
    }

    private void UpdateLookAtPoint()
    {
        lookAtPoint.position = transform.position + Vector3.up * rb.velocity.y * Mathf.Abs(rb.velocity.y) * lookAtPointRangeScale 
            + Vector3.right * lookAtPointDistance;
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

    private void OnChangeGameState(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            playerMesh.SetActive(true);
            dieParticleSystem.SetActive(false);
            dieParticleSystem.GetComponent<ParticleSystem>().Stop();
        }
        
    }
}
