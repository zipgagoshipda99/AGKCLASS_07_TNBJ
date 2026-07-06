using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalMoney = 100;
    public int currentBet = 0;
    event System.Action OnMoneyChanged;
    public bool PlaceBet(int amount)
    {
        if (totalMoney >= amount)
        {
            totalMoney -= amount;
            currentBet += amount;
            return true;
        }
                
        else return false;
    }
    public void RefundBet(int amount) //place bet 후 refund 하는거이므로 부호 반대
    {
        totalMoney += amount;
        currentBet -= amount;
    }
}
