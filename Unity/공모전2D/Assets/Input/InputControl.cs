// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6fcbd285-93d9-4ef3-a3c2-37a6b76e8a62"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""3a1f83a2-65a0-459c-acba-9c5cadd7fe5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sight"",
                    ""type"": ""Button"",
                    ""id"": ""412824bb-c22b-4bd3-8ba3-1f271eb325f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""688e740c-3b5b-40f3-afa5-8817d647bc61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rope"",
                    ""type"": ""Button"",
                    ""id"": ""fb85e416-58e3-4fbc-be1f-0df74de0b4a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""22ea4370-6a00-4152-b73e-d50ab7960e48"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e91fcbd0-96cf-4bc8-b1fc-e79b82e5ca51"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""de0101f8-ec8b-4d08-a2ce-a84cb4351ff6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""temp"",
                    ""id"": ""68aec510-5c69-4695-82a8-813c5f189461"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""436f2e9b-05cb-4963-89c6-b431f9fc002c"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""78833b63-d45f-4506-93c0-19c765d721dd"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""UpDown"",
                    ""id"": ""c1fde3d4-6a20-4b68-a5af-c61f6cdcfd24"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sight"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7966b94d-2b92-4e9a-bc30-b9cca4bc7f73"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8565598b-fe3a-4cc7-85d3-c55f53a86de7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""736d14d7-85a8-40d6-aa96-5999e4b983ef"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a8e8666-3202-4aae-b837-6230ada7cfc4"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Objects"",
            ""id"": ""2d98312b-f9ee-484f-a92f-b4f0a4035561"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""7d2984e1-edab-493d-8a63-5bb1bf063450"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gravity"",
                    ""type"": ""Button"",
                    ""id"": ""72c7a68e-aa59-4b23-89be-6987c71bedd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gravity"",
                    ""id"": ""c9b645b0-6ff2-46f4-86c0-df20967a1430"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b5c634d2-9e03-4c9f-a7ab-f99dd6f84c0f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a59aeea6-fef1-496f-a2c4-b4d3f9bf780a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""UpDown"",
                    ""id"": ""0d0716d3-50cf-42c3-bb69-4d44b17f1006"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gravity"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0d54d07b-0240-4f0e-948d-54d9d6ab1c6f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Gravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5193365a-c477-44e4-9db2-f5b32683a520"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Gravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Sight = m_Player.FindAction("Sight", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Rope = m_Player.FindAction("Rope", throwIfNotFound: true);
        // Objects
        m_Objects = asset.FindActionMap("Objects", throwIfNotFound: true);
        m_Objects_Rotate = m_Objects.FindAction("Rotate", throwIfNotFound: true);
        m_Objects_Gravity = m_Objects.FindAction("Gravity", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Sight;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Rope;
    public struct PlayerActions
    {
        private @InputControl m_Wrapper;
        public PlayerActions(@InputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Sight => m_Wrapper.m_Player_Sight;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Rope => m_Wrapper.m_Player_Rope;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Sight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSight;
                @Sight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSight;
                @Sight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSight;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Rope.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRope;
                @Rope.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRope;
                @Rope.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRope;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sight.started += instance.OnSight;
                @Sight.performed += instance.OnSight;
                @Sight.canceled += instance.OnSight;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Rope.started += instance.OnRope;
                @Rope.performed += instance.OnRope;
                @Rope.canceled += instance.OnRope;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Objects
    private readonly InputActionMap m_Objects;
    private IObjectsActions m_ObjectsActionsCallbackInterface;
    private readonly InputAction m_Objects_Rotate;
    private readonly InputAction m_Objects_Gravity;
    public struct ObjectsActions
    {
        private @InputControl m_Wrapper;
        public ObjectsActions(@InputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Objects_Rotate;
        public InputAction @Gravity => m_Wrapper.m_Objects_Gravity;
        public InputActionMap Get() { return m_Wrapper.m_Objects; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ObjectsActions set) { return set.Get(); }
        public void SetCallbacks(IObjectsActions instance)
        {
            if (m_Wrapper.m_ObjectsActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnRotate;
                @Gravity.started -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnGravity;
                @Gravity.performed -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnGravity;
                @Gravity.canceled -= m_Wrapper.m_ObjectsActionsCallbackInterface.OnGravity;
            }
            m_Wrapper.m_ObjectsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Gravity.started += instance.OnGravity;
                @Gravity.performed += instance.OnGravity;
                @Gravity.canceled += instance.OnGravity;
            }
        }
    }
    public ObjectsActions @Objects => new ObjectsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRope(InputAction.CallbackContext context);
    }
    public interface IObjectsActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnGravity(InputAction.CallbackContext context);
    }
}
