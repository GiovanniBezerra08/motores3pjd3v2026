using System;

public static class PlayerObserverManager
{
    public static Action<int, int> OnCoinCollected;
    public static Action<int> OnGameOver;

    public static void NotifyCoinCollected(int playerId, int totalCoins)
    {
        OnCoinCollected?.Invoke(playerId, totalCoins);
    }

    public static void NotifyGameOver(int winningPlayerId)
    {
        OnGameOver?.Invoke(winningPlayerId);
    }
}