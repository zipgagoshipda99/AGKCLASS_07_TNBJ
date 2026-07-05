using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    // Start is called before the first frame update
    int totalMoney = 100;
    int currentBet = 0;
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
}
