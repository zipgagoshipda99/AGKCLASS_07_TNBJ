using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class HandController : MonoBehaviour
{
    [SerializeField]private SpriteRenderer cardPrefab;
    //private SpriteRenderer chosenCardSprite;
    [SerializeField]private Button hitButton;
    [SerializeField]private Button standButton;
    [SerializeField]private Deck deckStructure;
    private BJ_GameState gameState = BJ_GameState.playerTurn;
    
    private List<GameObject> handCards = new List<GameObject>();
    private int playerNum = 0;
    private int dealerNum = 0;
    private int cardCount = 0;



    // Start is called before the first frame update
    void Start()
    {
       //chosenCardSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNum > 21)
        {
            UI_Manager.ui_Manager.BustUI();
            clearHand();
            
        }
    }
    void OnEnable()
    {
        hitButton.onClick.AddListener(Hit);
        standButton.onClick.AddListener(Stand);
    }
    void OnDisable()
    {
        hitButton.onClick.RemoveListener(Hit); 
        standButton.onClick.RemoveListener(Stand); 
        //버튼 이벤트 끄기
    }
    private void Hit()
    {
        Card drawn = deckStructure.GetRandomCards();
        SpriteRenderer drawnCard = Instantiate(cardPrefab);
        handCards.Add(drawnCard.gameObject);
        drawnCard.sprite = drawn.sprite;
        drawnCard.transform.position += new Vector3(0.75f*cardCount,0,0);
        drawnCard.transform.rotation = Quaternion.Euler(0,0,3f *cardCount);
    //istantiate가 새로운 drawn card를 hit가 눌를때마다 만드므로 그걸 누적하는게 
        // 필요 그리고 그 누적한것을 곱해야 x 값이 눌를때마다 1.5씩 증가
        //drawnCard에 기본 포지션 0 = (0,0,0) + (1.5f * cardCount,0,0)
        playerNum += drawn.cardValue;
        cardCount++;        Debug.Log($"Drawn Card {drawn.sprite.name}, {drawn.cardValue}, TOTAL player value : {playerNum} ");
        
    }
    public void clearHand()
    {
        foreach(GameObject card in handCards)
        {
            Destroy(card);
            
        }
        handCards.Clear();
    }
    private void Stand()
    {
        gameState = BJ_GameState.dealerTurn;
    }
    public enum BJ_GameState
    {
        playerTurn,
        dealerTurn,
        roundOver
        

    }
}
