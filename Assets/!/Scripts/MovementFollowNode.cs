using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementFollowNode : MonoBehaviour
{

    [SerializeField] Transform[] Points;

    [SerializeField] private float moveSpeed;

    private int pointsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsIndex <= Points.Length - 1)
        {
            // Move towards point
            Vector3 targetPos = Points[pointsIndex].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Smooth rotation towards point
            //Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed/2 * Time.deltaTime);
            gameObject.transform.LookAt(targetPos);

            // Model flip
            transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, transform.eulerAngles.z);

            if (transform.position == Points[pointsIndex].transform.position )
            {
                pointsIndex++;
            }

            if (pointsIndex >= Points.Length)
            {
                pointsIndex = 0;
            }
        }
    }
}
