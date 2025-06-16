using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        other.gameObject.transform.position = Vector3.zero;
        other.gameObject.SetActive(true);
        Debug.Log("1");
    }


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = Vector3.zero;
        Debug.Log("11");
    }


}
