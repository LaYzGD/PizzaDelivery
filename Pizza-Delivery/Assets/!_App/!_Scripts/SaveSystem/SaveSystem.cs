using UnityEngine;

public static class SaveSystem
{
    private const string _MONEY = "Money";

    public static void AddMoney(int amount)
    {
        var currentMoney = LoadMoney();
        var newMoneyAmout = currentMoney + amount;
        SaveMoney(newMoneyAmout);
    }

    public static int LoadMoney()
    {
        return PlayerPrefs.GetInt(_MONEY, 0);
    }

    public static void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt(_MONEY, amount);
    }
}
