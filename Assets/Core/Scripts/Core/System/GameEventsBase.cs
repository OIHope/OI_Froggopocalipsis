using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Progression;

public class GameEventsBase
{
    public static Action OnEnemyDeath;
    public static Action OnEnemyHit;

    public static Action OnPlayerHit;
    public static Action OnPlayerDeath;


    public static Action<GameStage> OnReachNewGameStage;
}
