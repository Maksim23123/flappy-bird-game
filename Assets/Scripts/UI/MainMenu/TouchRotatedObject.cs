using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotatedObject : MonoBehaviour
{
    [SerializeField]
    private Vector2 rotarionZonePosition;
    [SerializeField]
    private Vector2 rotarionZoneLeftTopCornerScalar;
    [SerializeField]
    private Vector2 rotarionZoneRightBottomCornerScalar;
    [SerializeField]
    private float touchSensitivity = 0.4f;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform horizontalAxis;
    [SerializeField] 
    private Transform verticalAxis;
    private float verticalAngularVelocity = 0;
    private float horizontalAngularVelocity = 0;
    private float velocityBurningSpeed = 40f;

    // Update is called once per frame
    void Update()
    {
        verticalAngularVelocity = Mathf.MoveTowards(verticalAngularVelocity, 0, velocityBurningSpeed 
            * Mathf.Sqrt(Mathf.Abs(verticalAngularVelocity)) * Time.deltaTime);
        horizontalAngularVelocity = Mathf.MoveTowards(horizontalAngularVelocity, 0, velocityBurningSpeed
            * Mathf.Sqrt(Mathf.Abs(horizontalAngularVelocity)) * Time.deltaTime);
        float verticalAngVelocityMultiplier = 1;
        float horizontalAngVelocityMultiplier = 1;
        if (verticalAngularVelocity < 0)
            verticalAngVelocityMultiplier = -1;
        if (horizontalAngularVelocity < 0)
            horizontalAngVelocityMultiplier = -1;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            float horizontalVelue = Screen.width / 2;
            float verticalVelue = Screen.height / 2;

            Vector2 rotationZoneCenter = new Vector2(horizontalVelue + horizontalVelue * rotarionZonePosition.x
                , verticalVelue + verticalVelue * rotarionZonePosition.y);

            Vector2 rotationZoneTopLeftCorner = new Vector2(horizontalVelue - horizontalVelue * rotarionZoneLeftTopCornerScalar.x
                , verticalVelue + verticalVelue * rotarionZoneLeftTopCornerScalar.y);

            Vector2 rotationZoneBottomRightCorner = new Vector2(horizontalVelue + horizontalVelue * rotarionZoneRightBottomCornerScalar.x
                , verticalVelue - verticalVelue * rotarionZoneRightBottomCornerScalar.y);


            if (touch.position.x < rotationZoneBottomRightCorner.x && touch.position.y > rotationZoneBottomRightCorner.y &&
                touch.position.x > rotationZoneTopLeftCorner.x && touch.position.y < rotationZoneTopLeftCorner.y)
            {
                verticalAngularVelocity += touch.deltaPosition.y * touchSensitivity;
                horizontalAngularVelocity += touch.deltaPosition.x * touchSensitivity;
            }
            
        }

        verticalAxis.localEulerAngles = new Vector3(verticalAxis.localEulerAngles.x
                , verticalAxis.localEulerAngles.y - Mathf.Sqrt(9.81f * Mathf.Abs(verticalAngularVelocity)) * verticalAngVelocityMultiplier * rotationSpeed
                , verticalAxis.localEulerAngles.z);
        horizontalAxis.localEulerAngles = new Vector3(horizontalAxis.localEulerAngles.x
                , horizontalAxis.localEulerAngles.y - Mathf.Sqrt(9.81f * Mathf.Abs(horizontalAngularVelocity)) * horizontalAngVelocityMultiplier * rotationSpeed
                , horizontalAxis.localEulerAngles.z);
    }
}
