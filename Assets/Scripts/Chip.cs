using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Button chipButton;
    [SerializeField]private GameObject chipPrefab;
    [SerializeField]private BetInput betInput;
    

    public int moneyAmount;
    void OnEnable()
    {                                 //괄호 없, 메서드 자체를 보관
        chipButton.onClick.AddListener(ChipClicked); //add lisenter stores the method it self. it must have no parameters and have a return type of void.
        
    }
    void OnDisable()
    {                                 //괄호 없, 메서드 자체를 보관
        chipButton.onClick.RemoveListener(ChipClicked); //add lisenter stores the method it self. it must have no parameters and have a return type of void.
        //add lisenter stores the method it self. it must have no parameters and have a return type of void.
        
    }
    public void ChipClicked()
    {
        betInput.OnChipClicked(moneyAmount, chipPrefab);
        // AnimateButton();
         
    }

    // void Start()
    // {
    //     animator = GetComponent<Animator>();
    // }
    // private void AnimateButton()
    // {
    //     animator.Play(AnimStateName);
    // }
}
