using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndingTrigger : MonoBehaviour
{
    public static event Action<WhatHappened> EndReached;

    [SerializeField] private Player player;

    private void Start()
    {
        player = GameObject.Find("GameLogic").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.layer == player.Character.layer)) return;
        OnEndReached(WhatHappened.EndReached);
    }

    protected virtual void OnEndReached(WhatHappened whatHappened)
    {
        if (EndReached != null) EndReached(whatHappened);
    }
}
