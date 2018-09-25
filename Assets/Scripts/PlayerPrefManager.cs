using UnityEngine;

public static class PlayerPrefManager
{
    public static void SetPinkCount( int pinkCount)
    {
        PlayerPrefs.SetInt("pinkCoinCount", pinkCount);
    }

    public static int GetPinkCount()
    {
        return PlayerPrefs.GetInt("pinkCoinCount", 0);
    }

    public static void SetBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt("bestScore", 0);
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void AddPinkCoins(int pinkAdd)
    {
        PlayerPrefs.SetInt("pinkCoinCount", GetPinkCount() + pinkAdd);
    }
    public static void SubtractPinkCoins(int pinkSub)
    {
        PlayerPrefs.SetInt("pinkCoinCount", GetPinkCount() - pinkSub);
    }
}