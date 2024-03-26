using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}
