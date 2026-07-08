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
    public void WinBet(float profitMultiplier)
    {                 //베팅했을때 차감했던 베텡금액 + profitMultiplier 곱한 베팅금액을 더해주는거
        totalMoney += currentBet + (int)(currentBet * profitMultiplier);
        currentBet = 0;
        OnMoneyChanged?.Invoke();
    }
    public void LoseBet()
    {
        currentBet = 0;
        OnMoneyChanged?.Invoke();
    }
    public void PushBet()
    {
        totalMoney += currentBet;// 베팅금액 환불
        currentBet = 0; //리셋
        OnMoneyChanged?.Invoke();
    }
}
