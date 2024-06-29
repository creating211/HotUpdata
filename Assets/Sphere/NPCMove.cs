using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    private float MoveSpeed = 0.8f; // ÒÆ¶¯ËÙ¶È
    private float MoveDistance = 0;

    public Canvas canvas;

    void Update()
    {
        MoveDistance += MoveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, 0, 1 * MoveSpeed) * Time.deltaTime);

        if (MoveDistance > 6)
        {
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, -eulerRotation.y, eulerRotation.z);

            MoveDistance = 0;
        }

    }
}
