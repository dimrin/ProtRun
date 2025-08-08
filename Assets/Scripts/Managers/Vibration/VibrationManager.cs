using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour {
    private bool isVibrating = false;

    public void SetVibration(bool state)
    {
        isVibrating = state;
    }

    public void Vibrate()
    {
        if (isVibrating)
        {
            Handheld.Vibrate();
            Debug.Log("Vibr");
        } else
        {
            Debug.Log("no Vibr");
        }

    }
}
