using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Reporting;
public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI_Manager ui_Manager;
    //[SerializeField]private TextMeshProUGUI standTimerText;
    [SerializeField]private GameObject bustPanel;
    [SerializeField]private GameObject bjPanel;
    
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
    // private IEnumerator TimerRunOut()
    // {
    //     for(int i = 5; i>0; i--)
    //     {
    //         standTimerText.text = $"auto selecting in {i} seconds";
    //         yield return new WaitForSeconds(1f);
    //     }
    // }
    public void BustUI()
    {
        bustPanel.SetActive(true);
    }
    public void BlackJackUI()
    {
        bjPanel.SetActive(true);
    }
}
