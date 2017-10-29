using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public Transform Target;
    public float Smoothing = 5f;

    Vector3 Offset;

    void Start()
    {
        Offset = transform.position - Target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = Target.position + Offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
    }
}
