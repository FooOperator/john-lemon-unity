using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<Vector2> WalkPerformed;
  
    public GameObject Character;
    public PlayerControls PlayerControls;
//    public PlayerEvents PlayerEvents;

    private void Awake()
    {
        PlayerControls = new PlayerControls();
    //    PlayerEvents = new PlayerEvents();

        PlayerControls.InGame.Walk.performed += _ => Walk_performed(_.ReadValue<Vector2>());
    }

    private void Start()
    {
        Character = GameObject.FindWithTag("Player");
    }
    private void OnEnable()
    {
        PlayerControls.Enable();
    }
    private void OnDisable()
    {
        PlayerControls.Disable();
    }
    
    protected virtual void Walk_performed(Vector2 directionsVector)
    {
        if (WalkPerformed != null)
        {
            WalkPerformed(directionsVector);
        }
    }
}


