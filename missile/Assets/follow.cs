using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position - target.transform.forward * 1.9f - target.transform.up * 16, 0.5f);
        transform.LookAt(target.transform.position + target.transform.up * 50f);
    }
}
