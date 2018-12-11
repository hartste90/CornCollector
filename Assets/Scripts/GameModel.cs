public static class GameModel
{
    public static float timeDelayReplayButton = 2f;

    public static bool canCollectCoins;
    public static int numAttempts;
    public static int numReplays;
    public static int numSafes;
    public static int userLevel;
    public static bool shouldReplaceSafes;

    public static bool hasJustContinued = false;

    private static int goldCoinCount;
    private static bool allowSwiping;

    private static bool purchasedNoAds;

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

    public static void EnableShipInput()
    {
        allowSwiping = true;
    }

    public static void DisableShipInput()
    {
        allowSwiping = false;
    }

    public static bool IsShipInputAllowed()
    {
        return allowSwiping;
    }

    public static void AddSafe()
    {
        numSafes++;
    }

    public static void ResetSafes()
    {
        numSafes = 1;
    }

    public static bool GetNoAds()
    {
        return purchasedNoAds;
    }

    public static void SetNoAds(bool hasPurchasedAds)
    {
        purchasedNoAds = hasPurchasedAds;
    }


}