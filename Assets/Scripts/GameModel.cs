public static class GameModel
{
    public static float timeDelayReplayButton = .5f;

    public static bool canCollectCoins;
    public static int numAttempts;

    private static int goldCoinCount;

    //gold coin functions
    public static int GetGoldCoinCount()
    {
        return goldCoinCount;
    }
    public static void SetGoldCoinCount(int goldSet)
    {
        goldCoinCount = goldSet;
    }
    public static int AddGoldCoins(int goldAdd)
    {
        goldCoinCount += goldAdd;
        return goldCoinCount;
    }
    public static int SubtractGoldCoins(int goldSubtract)
    {
        goldCoinCount -= goldSubtract;
        return goldCoinCount;
    }
}