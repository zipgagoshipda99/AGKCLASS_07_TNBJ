using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using System;
using Unity.VisualScripting;
public class UI_Manager : MonoBehaviour
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
    [SerializeField] public CanvasGroup UI_buttons;
    [SerializeField]public TextMeshProUGUI timerText;
    [SerializeField]private GameObject[] ResultPanelsArray;
    private bool timerOn = false;
    
    void Awake()
    {
        if (ui_Manager != null)
        {
            Destroy(gameObject);
        }
        else{ui_Manager = this;}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BustUI()=>bustPanel.SetActive(true);
    
    public void DealerBustUI()=> dealerBustPanel.SetActive(true);
    
    public void Push()=> pushPanel.SetActive(true);
    
    
    public void BlackJackUI()=>bjPanel.SetActive(true);
    public void DealerBlackJackUI()=>dealerBjPanel.SetActive(true);
    
    public void NaturalWinUI()=>winPanel.SetActive(true);
    
    public void DealerWinUI()=>dealerWinPanel.SetActive(true);
    
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
}