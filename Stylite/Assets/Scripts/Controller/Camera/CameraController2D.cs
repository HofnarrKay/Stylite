using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float speed = 4;

    [SerializeField]
    float maxSpeedMultiplier = 3;

    [SerializeField]
    float maxHeight = 10000;

    [SerializeField]
    float minHeight = 0;

    float oldInputDirection = 0;

    float continousSpeed = 1;

    private void Update()
    {
        if (cameraTransform) cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + (Input.GetAxis("Vertical") * speed * continousSpeed * Time.deltaTime), cameraTransform.position.z);

        if (cameraTransform.position.y < minHeight) cameraTransform.position = new Vector3(cameraTransform.position.x, minHeight, cameraTransform.position.z);

        if (oldInputDirection == Input.GetAxis("Vertical") && Input.GetAxis("Vertical") != 0)
        {
            if (continousSpeed < maxSpeedMultiplier)
            {
                continousSpeed *= 1.01f;
            }
            else
            {
                continousSpeed = maxSpeedMultiplier;
            }
        }
        else
        {
            continousSpeed = 1;
        }

        oldInputDirection = Input.GetAxis("Vertical");
    }
}
