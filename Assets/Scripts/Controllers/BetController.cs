using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    public int currentBet = 0;
    public int totalMoney = 1000;
    
    public void SetBetAmount(int betAmount)
    {
        if (betAmount > totalMoney)
        {
            Debug.Log("betcontrol line 16 잔액 부족");
            return;
        }
        currentBet = betAmount;
        Debug.Log($"현재 베팅금액 변경 -> {currentBet}원");
    }


}
