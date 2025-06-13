using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHittable {
    public void Hit()
    {
        Destroy(gameObject);
    }
}
