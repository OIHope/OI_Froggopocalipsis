using System;
using Core.Progression;

public class GameEventsBase
{
    public static Action OnPlotItemPickUp;

    public static Action OnEnemyDeath;
    public static Action OnEnemyHit;

    public static Action OnPlayerHit;
    public static Action OnPlayerDeath;
    public static Action OnPlayerIsStuckAndNeedsHelp;

    public static Action IsUsingGamepad;
    public static Action IsUsingKeybord;

    public static Action OnLanguageChanged;

    public static Action<GameStage> OnReachNewGameStage;
    public static Action OnGameReset;
    public static Action OnBackToMaunMenu;
    public static Action OnResumeGameplay;
}
