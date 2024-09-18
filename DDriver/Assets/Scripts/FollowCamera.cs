using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] GameObject toFollow;

    // This position should be the same as the car position

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, transform.position.z);
    }
}
