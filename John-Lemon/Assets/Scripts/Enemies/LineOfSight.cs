using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public static event Action<WhatHappened> CaughtPlayer;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float lineOfSightDistance = 6f;

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, lineOfSightDistance, playerMask))
        {
            if (Physics.Linecast(ray.origin, hit.point, ~playerMask)) return;
            OnCaughtPlayer(WhatHappened.PlayerCaught);
        }    
    }

    protected virtual void OnCaughtPlayer(WhatHappened whatHappened)
    {
        if (CaughtPlayer != null)
        {
            CaughtPlayer(whatHappened);
        }
    }
}
