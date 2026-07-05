using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HandController : Subject
{
    [SerializeField]private SpriteRenderer cardPrefab;
    [SerializeField]private SpriteRenderer hiddenCardRenderer;
    [SerializeField]private Sprite hiddenCardFace;
    //private SpriteRenderer chosenCardSprite;
    [SerializeField]private Button hitButton;
    [SerializeField]private Button standButton;
    [SerializeField]private Button dealButton;
    [SerializeField]private Deck deckStructure;
    [SerializeField]private Sprite cardCover;
    private Card plrAceCard;
    private Card dealerAceCard;
    private BJ_GameState gameState = BJ_GameState.playerTurn;
    
    private List<GameObject> allHandCards = new List<GameObject>();
    private List<GameObject> dealerHandCards = new List<GameObject>();
    private Animator animator;
    private int playerNum = 0;
    private int dealerNum = 0;
    private int plrCardCount = 0;
    private int dealerCardCount = 0;
    bool showDealerCard = false;
    private bool timerEnabled = false;
    private bool SoftHand = false;
    private bool HardHand = false;
    private Coroutine timerCoroutine;
    public enum BJ_GameState
    {
        Deal,
        playerTurn,
        dealerTurn,
        roundOver
    }


    void Awake()
    {
        UI_Manager.ui_Manager.standButtonObj.SetActive(false);
        UI_Manager.ui_Manager.hitButtonObj.SetActive(false);
    }

    // Update is called once per frame
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
        dealButton.onClick.RemoveListener(Deal); 
        //버튼 이벤트 끄기
    }
    private void Deal()
    {
        gameState = BJ_GameState.Deal;
        Hit();
        Hit();
        DealerDraw();
        DealerDraw();
        // timerEnabled = true;
        if (playerNum == 21 && dealerNum < 21)
        {
            RevealDealerCard();
            NotifyObservers(GameResult.PlayerBJ);  // instant blackjack condition.
            ClearHand();
            return;
        }
        if (dealerNum == 21)
        {
            NotifyObservers(GameResult.DealerBJ);
        }
        StartTimer();
        UI_Manager.ui_Manager.StartCoroutine(UI_Manager.ui_Manager.FadeIn());
        UI_Manager.ui_Manager.standButtonObj.SetActive(true);
        UI_Manager.ui_Manager.hitButtonObj.SetActive(true);
        gameState = BJ_GameState.playerTurn;
        
    }
    private void Hit()
    {
        // timerEnabled = false;
        Card randomCard = deckStructure.GetRandomCards();
        // if(randomCard.cardValue ==1)
        // {
        //     randomCard = plrAceCard;
        // }
        SpriteRenderer createdCard = Instantiate(cardPrefab);
        animator = createdCard.GetComponent<Animator>();
        animator.SetBool("wasCreated", true);
        Debug.Log($"card -> {createdCard}, animation -> {animator}");
        allHandCards.Add(createdCard.gameObject);
        createdCard.sprite = randomCard.sprite;
        createdCard.transform.position += new Vector3(0.75f*plrCardCount,0,0);
        createdCard.transform.rotation = Quaternion.Euler(0,0,3f *plrCardCount);
      //istantiate가 새로운 drawn card를 hit가 눌를때마다 만드므로 그걸 누적하는게 
        // 필요 그리고 그 누적한것을 곱해야 x 값이 눌를때마다 1.5씩 증가
        //createdCard에 기본 포지션 0 = (0,0,0) + (1.5f * cardCount,0,0)
        plrCardCount++;        
        playerNum += randomCard.cardValue;
        Debug.Log($"Drawn Card {randomCard.sprite.name}, {randomCard.cardValue}, TOTAL player value : {playerNum} ");
        if (gameState != BJ_GameState.playerTurn) return; //player 턴 아니면 여기서 함수 끝 player턴 이면 이후로 넘어감
        RevealDealerCard();
        StopTimer();
        switch (playerNum)
        {
            case 21:
                Stand();
                return;
            case > 21:
                NotifyObservers(GameResult.PlayerBust);
                UI_Manager.ui_Manager.StartCoroutine(UI_Manager.ui_Manager.FadeOut());
                return;
        }   
        // if(playerNum == 21)
        // {
        //     Stand();
        //     return; b 
        // } 
        // else if(playerNum > 21) 
        // {
        //     {UI_Manager.ui_Manager.BustUI();}
        //     StopTimer();
        //     return;
        // }
        StartTimer(); //player 턴일때 hit를 해도 두가지 case에 안 닿을때 다시 타이머를 시작(자동 선택하게)
    }
    private void DealerDraw()
    {
        Card randomCard = deckStructure.GetRandomCards();
        // if (randomCard.cardValue == 1)
        // {
        //     randomCard = dealerAceCard;
        // }
        SpriteRenderer createdCard = Instantiate(cardPrefab); //even if card prefab references sprite renderer instatnitate makes a copy of the object with that sprite renderer component.
        //and created card only references the spriterenderer component.
        allHandCards.Add(createdCard.gameObject);
        dealerHandCards.Add(createdCard.gameObject);
        createdCard.sprite = randomCard.sprite;
        createdCard.transform.position = new Vector3(0.75f * dealerCardCount, 4f, 0); // -2 = dealer's row
        createdCard.transform.rotation = Quaternion.Euler(0, 0, 3f * dealerCardCount);
        dealerNum += randomCard.cardValue;
        dealerCardCount++;
        Debug.Log($"dealer drawn {randomCard.sprite.name}, {randomCard.cardValue}, TOTAL dealer value : {dealerNum} ");
        if (dealerCardCount == 2)
        {
            hiddenCardFace = randomCard.sprite;
            hiddenCardRenderer = createdCard; // dealer 카드 2번째의 스프라이트 렌더러를 기억하도록 저장.
            hiddenCardRenderer.sprite = cardCover;
        }
    }
    private void Stand()
    {
        StopTimer();
        UI_Manager.ui_Manager.StartCoroutine(UI_Manager.ui_Manager.FadeOut());
        gameState = BJ_GameState.dealerTurn;
        StartCoroutine(DealerHit()); 
    }

    private void WinDecider(int playerScore, int dealerScore) //win decider는 deal 이후 hit / stand 할때 판단하는 메소드
    {
        if (dealerScore > 21)
        {
            NotifyObservers(GameResult.DealerBust);
        }
        else if (playerScore == dealerScore)// push condition.
        {
            NotifyObservers(GameResult.Push);
        }
        else if (playerScore == 21 && dealerScore < 21)
        {
            NotifyObservers(GameResult.PlayerBJ); // plr blackjack condition.
        }
        else if(dealerScore == 21 && playerScore < 21)
        {
            NotifyObservers(GameResult.DealerBJ); // dealer blackjack condition.
        }
        else if (playerScore > dealerScore)
        {
            NotifyObservers(GameResult.PlayerWin); // plr win condition.
        }
        else if (dealerScore > playerScore)
        {
            NotifyObservers(GameResult.DealerWin); //dealer win condition
        }
        ClearHand();
    }
    private IEnumerator TimerLimit()
    {
        yield return new WaitForSeconds(5f);
        for(int i = 5; i>0; i--)
        {
            // if (!timerEnabled)
            // {
            //     UI_Manager.ui_Manager.timerText.text = "";
            //     yield break;
            // }
            UI_Manager.ui_Manager.timerText.text = $"auto selecting in {i} seconds";
            yield return new WaitForSeconds(1f);
        }
        timerCoroutine = null;
        int pickDealOrStand = Random.Range(1,3);
        if (pickDealOrStand == 1)
        {
            Hit();
        }
        else if(pickDealOrStand ==2)
        {
            Stand();
        }
        UI_Manager.ui_Manager.timerText.text = $"";
        
    }
    private void StartTimer()
    {
        StopTimer();
        timerCoroutine = StartCoroutine(TimerLimit()); //코루틴 시작 함수가 실행되어 리턴 값을 주고 그것을 코루틴 참조값을 저장하는 timerCoroutine 변수가 저장한다는 의미.
        //대충 코루틴이 시작하므로 그 타이머가 띄워진다.
    } 
    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
            UI_Manager.ui_Manager.timerText.text = "";
        }
    }

    private void RevealDealerCard()
    {
        if(hiddenCardRenderer != null)
        {
            hiddenCardRenderer.sprite = hiddenCardFace;
        }
    }
    private IEnumerator DealerHit()
    {
        RevealDealerCard();
        while (dealerNum < 17)
        {
            DealerDraw();
            yield return new WaitForSeconds(1f);
        }
        gameState = BJ_GameState.roundOver;

        WinDecider(playerNum, dealerNum);
    }
    public void ClearHand()
    {
        foreach(GameObject card in allHandCards)
        {
            Destroy(card);
        }
        StopTimer();
        allHandCards.Clear();
        dealerHandCards.Clear();
        UI_Manager.ui_Manager.StartCoroutine("HideAllResultPanels");
        playerNum = 0;
        dealerNum = 0;
        plrCardCount = 0;
        dealerCardCount = 0;
    }
    public bool CheckSoftHand()
    {
        
        if(playerNum + 11 < 21)
        {
            plrAceCard.cardValue = 11;
            return SoftHand;
        }
        return SoftHand == false;
    }
    public bool CheckHardHand()
    {
        if(playerNum + 11 > 21)
        {
            return HardHand;
        }
        return HardHand == false;
    }
    
}