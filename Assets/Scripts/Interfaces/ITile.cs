using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    public void OnSpawn();
    public void OnRecycle();
    public GameObject GameObject { get; }
}
