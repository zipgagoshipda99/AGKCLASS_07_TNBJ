using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Concurrent;

public class HandController : MonoBehaviour
{
    [SerializeField]private SpriteRenderer cardPrefab;
    //private SpriteRenderer chosenCardSprite;
    [SerializeField]private Button hitButton;
    [SerializeField]private Button standButton;
    [SerializeField]private Button dealButton;
    [SerializeField]private Deck deckStructure;
    private BJ_GameState gameState = BJ_GameState.playerTurn;
    
    private List<GameObject> handCards = new List<GameObject>();
    private int playerNum = 0;
    private int dealerNum = 0;
    private int cardCount = 0;
    private int dealerCardCount = 0;



    // Start is called before the first frame update
    void Start()
    {
       //chosenCardSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnEnable()
    {
        hitButton.onClick.AddListener(Hit);
        standButton.onClick.AddListener(Stand);
        dealButton.onClick.AddListener(Deal);
    }
    void OnDisable()
    {
        hitButton.onClick.RemoveListener(Hit); 
        standButton.onClick.RemoveListener(Stand); 
        //버튼 이벤트 끄기
    }
    private void Deal()
    {
        Hit();
        Hit();
        DealerDraw();
        DealerDraw();
    }
    private void Hit()
    {
        Card randomCard = deckStructure.GetRandomCards();
        SpriteRenderer createdCard = Instantiate(cardPrefab);
        handCards.Add(createdCard.gameObject);
        createdCard.sprite = randomCard.sprite;
        createdCard.transform.position += new Vector3(0.75f*cardCount,0,0);
        createdCard.transform.rotation = Quaternion.Euler(0,0,3f *cardCount);
    //istantiate가 새로운 drawn card를 hit가 눌를때마다 만드므로 그걸 누적하는게 
        // 필요 그리고 그 누적한것을 곱해야 x 값이 눌를때마다 1.5씩 증가
        //createdCard에 기본 포지션 0 = (0,0,0) + (1.5f * cardCount,0,0)
        playerNum += randomCard.cardValue;
        cardCount++;        
        Debug.Log($"Drawn Card {randomCard.sprite.name}, {randomCard.cardValue}, TOTAL player value : {playerNum} ");
        if(playerNum > 21)
        {
            UI_Manager.ui_Manager.BustUI();
            clearHand();
        }
        else if(playerNum == 21)
        {
            UI_Manager.ui_Manager.BlackJackUI();
            clearHand();
        }
        
    }
    private void DealerDraw()
    {
        Card randomCard = deckStructure.GetRandomCards();
        SpriteRenderer createdCard = Instantiate(cardPrefab);
        handCards.Add(createdCard.gameObject);
        createdCard.sprite = randomCard.sprite;
        createdCard.transform.position = new Vector3(0.75f * dealerCardCount, -4f, 0); // -2 = dealer's row
        createdCard.transform.rotation = Quaternion.Euler(0, 0, 3f * dealerCardCount);
        dealerNum += randomCard.cardValue; 
        dealerCardCount++;
    }
    
    private IEnumerator DealerHit()
    {
        while (dealerNum < 17)
        {
            DealerDraw();
            yield return new WaitForSeconds(1.5f);
        }
    }
    public void clearHand()
    {
        foreach(GameObject card in handCards)
        {
            Destroy(card);
            
        }
        handCards.Clear();
        playerNum = 0;
        dealerNum = 0;
        cardCount = 0;
    }
    private void Stand()
    {
        gameState = BJ_GameState.dealerTurn;
        StartCoroutine(DealerHit());
    }
    public enum BJ_GameState
    {
        Deal,
        playerTurn,
        dealerTurn,
        roundOver
        

    }
}
