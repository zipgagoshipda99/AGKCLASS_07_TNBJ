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

    public int moneyAmount = 0;
    public string AnimStateName;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        chipButton = GetComponent<Button>();
    }
    public void PlayAnim()
    {
        animator.Play(AnimStateName);
    }
}
