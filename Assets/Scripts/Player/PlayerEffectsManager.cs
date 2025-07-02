using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour {
    [SerializeField] private GameObject magnetObject;

    private int currentMagnets = 0;

    public void ActivateMangetObject(float timerTime)
    {

        StartCoroutine(HandleEffectObject(timerTime, magnetObject, () =>
        {

        }));
    }

    private IEnumerator HandleEffectObject(float timerTime, GameObject objectToActivate, Action OnObjectDeactivated)
    {
        currentMagnets++;
        objectToActivate.SetActive(true);
        yield return new WaitForSeconds(timerTime);
        currentMagnets--;
        if (currentMagnets == 0)
        {
            objectToActivate.SetActive(false);
        }

        OnObjectDeactivated?.Invoke();
    }
}
