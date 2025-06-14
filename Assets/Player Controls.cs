//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Player Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerTouchActions"",
            ""id"": ""f503be55-d7c4-4f3b-8485-1a2a14cdf38c"",
            ""actions"": [
                {
                    ""name"": ""TouchMovement"",
                    ""type"": ""Value"",
                    ""id"": ""af2ea9b7-da2c-4fab-9112-49869ce86624"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""afc2ccfb-97c4-44bc-ae6c-1576edbc79d6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f3930aac-39e3-4816-965c-ac37b60f09ee"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""931abf17-1ab3-49bf-ae1f-021310b9d84e"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerTouchActions
        m_PlayerTouchActions = asset.FindActionMap("PlayerTouchActions", throwIfNotFound: true);
        m_PlayerTouchActions_TouchMovement = m_PlayerTouchActions.FindAction("TouchMovement", throwIfNotFound: true);
        m_PlayerTouchActions_TouchPress = m_PlayerTouchActions.FindAction("TouchPress", throwIfNotFound: true);
    }

    ~@PlayerControls()
    {
        UnityEngine.Debug.Assert(!m_PlayerTouchActions.enabled, "This will cause a leak and performance issues, PlayerControls.PlayerTouchActions.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerTouchActions
    private readonly InputActionMap m_PlayerTouchActions;
    private List<IPlayerTouchActionsActions> m_PlayerTouchActionsActionsCallbackInterfaces = new List<IPlayerTouchActionsActions>();
    private readonly InputAction m_PlayerTouchActions_TouchMovement;
    private readonly InputAction m_PlayerTouchActions_TouchPress;
    public struct PlayerTouchActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerTouchActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchMovement => m_Wrapper.m_PlayerTouchActions_TouchMovement;
        public InputAction @TouchPress => m_Wrapper.m_PlayerTouchActions_TouchPress;
        public InputActionMap Get() { return m_Wrapper.m_PlayerTouchActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerTouchActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerTouchActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerTouchActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerTouchActionsActionsCallbackInterfaces.Add(instance);
            @TouchMovement.started += instance.OnTouchMovement;
            @TouchMovement.performed += instance.OnTouchMovement;
            @TouchMovement.canceled += instance.OnTouchMovement;
            @TouchPress.started += instance.OnTouchPress;
            @TouchPress.performed += instance.OnTouchPress;
            @TouchPress.canceled += instance.OnTouchPress;
        }

        private void UnregisterCallbacks(IPlayerTouchActionsActions instance)
        {
            @TouchMovement.started -= instance.OnTouchMovement;
            @TouchMovement.performed -= instance.OnTouchMovement;
            @TouchMovement.canceled -= instance.OnTouchMovement;
            @TouchPress.started -= instance.OnTouchPress;
            @TouchPress.performed -= instance.OnTouchPress;
            @TouchPress.canceled -= instance.OnTouchPress;
        }

        public void RemoveCallbacks(IPlayerTouchActionsActions instance)
        {
            if (m_Wrapper.m_PlayerTouchActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerTouchActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerTouchActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerTouchActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerTouchActionsActions @PlayerTouchActions => new PlayerTouchActionsActions(this);
    public interface IPlayerTouchActionsActions
    {
        void OnTouchMovement(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
    }
}
