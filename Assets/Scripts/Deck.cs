using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
[System.Serializable]
public struct Card
{
    public Sprite sprite;
    public int cardValue;
    
}
[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "ScriptableObjects/Deck")]

public class Deck : ScriptableObject //mono behaiver 가 있어야 유니티 inspector 에 뜨고 스크립트가 오브젝트에 attach 하도록함
{
    
    public Card[] deck;
    public Card GetRandomCards()
    {
        int i = Random.Range(0, deck.Length);
        return deck[i]; //Card를 여러개 담는 배열 (deck)에서 랜덤 index에 있는 Card 하나를 (sprite + cardValue 둘 다 들어있음) 호출한 쪽에 리턴함
    }
}