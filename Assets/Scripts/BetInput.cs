using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BetInput : MonoBehaviour
{
    [SerializeField]private Button bet5;
    [SerializeField]private Button bet10;
    [SerializeField]private Button bet100;
    [SerializeField]private Button bet1000;
    [SerializeField]private Button bet5000;
    [SerializeField]private Button bet10000;
    public Chip chip;
    
    public void OnEnable()
    {
       bet5.onClick.AddListener(chip.PlayAnim);
    }



}
