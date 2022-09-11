using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position = new Vector3(GetComponentInParent<Transform>().position.x, GetComponentInParent<Transform>().position.y, GetComponentInParent<Transform>().position.z);
    }
}
