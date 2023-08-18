using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Scripts.Characters.Player;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public event Action OnFinishEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Actor>();

        if (player != null )
        {
            player.OnWin();
            OnFinishEvent?.Invoke();
        }
    }
}
