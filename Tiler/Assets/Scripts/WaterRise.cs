using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    [SerializeField] float risingSpeed = 1f;


    private void Update()
    {
        Vector3 newPos = new Vector3(0, risingSpeed * Time.deltaTime, 0);
        transform.position += newPos; 
    }
}
