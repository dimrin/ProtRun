using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerItemPicker : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.TryGetComponent<IPickable>(out IPickable pickable))
        {
            pickable.Picked();
        }
        
    }
}
