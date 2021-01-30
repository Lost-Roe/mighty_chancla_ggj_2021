using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 cameraOffest;
    [Range(0.1f, 1f)]
    [SerializeField] private float smoothFactor = 0;

    [SerializeField] private bool lookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffest = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = playerTransform.position + cameraOffest;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (lookAtPlayer)
            transform.LookAt(playerTransform);
    }
}
