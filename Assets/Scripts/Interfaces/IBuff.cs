using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBuff
{
    void Apply(int value);
    void Update(Action OnBuffEnded); // Optional - for time-based or active behavior
    bool IsActive { get; }
}
