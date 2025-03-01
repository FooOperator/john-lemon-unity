using UnityEngine;
using System;

public class GameEndingTrigger : MonoBehaviour
{
    public static event Action<WhatHappened> EndReached;

    private void OnTriggerEnter(Collider other)
    {
        // if (!(other.gameObject.layer == player.Character.layer)) return;
        // OnEndReached(WhatHappened.EndReached);
    }

    protected virtual void OnEndReached(WhatHappened whatHappened)
    {
        if (EndReached != null) EndReached(whatHappened);
    }
}