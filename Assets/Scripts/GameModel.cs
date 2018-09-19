public static class GameModel
{
    public static float timeDelayReplayButton = .5f;

    public static bool canCollectCoins;
    public static int numAttempts;

    private static int goldCoinCount;
    private static int pinkCoinCount;

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


    //pink coin functions
    public static int GetPinkCoinCount()
    {
        return pinkCoinCount;
    }
    public static void SetPinkCoinCount(int pinkSet)
    {
        pinkCoinCount = pinkSet;
    }
    public static int AddPinkCoins(int pinkAdd)
    {
        pinkCoinCount += pinkAdd;
        return pinkCoinCount;
    }
    public static int SubtractPinkCoins(int pinkSubtract)
    {
        pinkCoinCount -= pinkSubtract;
        return pinkCoinCount;
    }
}