using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action Caught;

    protected virtual void OnCaught()
    {
        if (Caught != null) Caught();
    }

}