using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using System;
using Unity.VisualScripting;
public class UI_Manager : MonoBehaviour, IObserver
{
    // Start is called before the first frame update
    public static UI_Manager ui_Manager;
    //[SerializeField]private TextMeshProUGUI standTimerText;
    [SerializeField]private GameObject bustPanel;
    [SerializeField]private GameObject dealerBustPanel;
    [SerializeField]private GameObject bjPanel;
    [SerializeField]private GameObject dealerBjPanel;
    [SerializeField]private GameObject pushPanel;
    [SerializeField]private GameObject winPanel;
    [SerializeField]private GameObject dealerWinPanel;
    [SerializeField]public GameObject hitButtonObj;
    [SerializeField]public GameObject standButtonObj;
     [SerializeField] public GameObject dealStartButtonObj;
    [SerializeField] public CanvasGroup UI_buttons;
    [SerializeField]public TextMeshProUGUI timerText;
    [SerializeField]private GameObject[] ResultPanelsArray;
    [SerializeField]private Subject _handSubject;
    [SerializeField]private TextMeshProUGUI totalMoneyText;
    [SerializeField]private TextMeshProUGUI betAmountText;
    [SerializeField]public Button undoButton;
    [SerializeField]private Wallet wallet;
    [SerializeField]private BetInput betInput;
     
    private bool timerOn = false;
    void Start()
    {
        hitButtonObj.SetActive(false);
        standButtonObj.SetActive(false);
    }
    void Awake()
    {
        if (ui_Manager != null)
        {
            Destroy(gameObject);
        }
        else{ui_Manager = this;}
    }
    public void OnNotify(GameResult gameResult)
    {
        switch (gameResult)
        {
            case GameResult.PlayerBust: bustPanel.SetActive(true); break;
            case GameResult.DealerBust: dealerBustPanel.SetActive(true); break;
            case GameResult.PlayerWin: winPanel.SetActive(true); break;
            case GameResult.DealerWin: dealerWinPanel.SetActive(true); break;
            case GameResult.Push: pushPanel.SetActive(true); break;
            case GameResult.PlayerBJ: bjPanel.SetActive(true); break;
            case GameResult.DealerBJ: dealerBjPanel.SetActive(true); break;
        }
        
        
    }

    // Update is called once per frame
    public void OnEnable()
    {
        _handSubject.AddObserver(this);
        undoButton.onClick.AddListener(betInput.OnUndoClick);
    }
    void OnDisable()
    {
        _handSubject.RemoveObserver(this);
       undoButton.onClick.RemoveListener(betInput.OnUndoClick);
    }
    public void UndoClicked()
    {
        betInput.OnUndoClick(); 
    }
    public IEnumerator HideAllResultPanels()
    {
        yield return new WaitForSeconds(2f);
        foreach (GameObject panel in ResultPanelsArray)
        {
            panel.SetActive(false);
        }
    }

    public IEnumerator FadeOut()
    {
        float duration = 0.7f;
        float timeCounter = 1f;
        while (timeCounter > 0f)
        {
             timeCounter -= Time.deltaTime;
             UI_buttons.alpha = Mathf.Lerp(0f, 1f, timeCounter / duration);
            yield return null;
        }
    }
    public IEnumerator FadeIn()
    {
        float duration = 0.7f;
        float timeCounter = 0f;
        while (timeCounter < 1f)
        {
            timeCounter += Time.deltaTime;
            UI_buttons.alpha = Mathf.Lerp(0f, 1f, timeCounter / duration);
            yield return null;
        }
    }
    public void RefreshMoneyTexts()
    {
        totalMoneyText.text = $"Total Money: {wallet.totalMoney}";
        betAmountText.text = $"Bet Amount: {wallet.currentBet}";
    }
    public void Update()
    {
        RefreshMoneyTexts();
        
    }
}