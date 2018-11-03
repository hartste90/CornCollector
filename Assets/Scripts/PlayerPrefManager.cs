using System;
using System.Collections;
using System.Collections.Generic;
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

    public static bool GetMuteState()
    {
        return PlayerPrefs.GetInt("muteState", 0) != 0;
    }

    public static void SetMuteState(bool muteSet)
    {
        PlayerPrefs.SetInt("muteState", muteSet ? 1 : 0);
    }

    public static void IncrementNumLogins()
    {
        PlayerPrefs.SetInt("numLogins", GetNumLogins() + 1);
    }

    public static int GetNumLogins()
    {
        return PlayerPrefs.GetInt("numLogins", 0);
    }

    public static void SetFirstLoginDate(DateTime date)
    {
        PlayerPrefs.SetInt("firstLoginYear", date.Year);
        PlayerPrefs.SetInt("firstLoginDay", date.DayOfYear);
    }

    public static Hashtable GetFirstLoginDate()
    {
        Hashtable date = new Hashtable
        {
            { "year", PlayerPrefs.GetInt("firstLoginYear", 0) },
            { "day", PlayerPrefs.GetInt("firstLoginDay", 0) }
        };
        return date;
    }

}