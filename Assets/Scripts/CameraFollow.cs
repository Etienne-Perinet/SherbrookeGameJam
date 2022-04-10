using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] Camera cam;
    [SerializeField] float threshold;
    void Update () 
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (target.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + target.position.x, threshold + target.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + target.position.y, threshold + target.position.y);
        targetPos.z = -15;

        this.transform.position = targetPos; 
    }
}
