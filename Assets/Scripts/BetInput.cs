using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BetInput : MonoBehaviour
{
    [SerializeField]private GameObject chipButtonsPanel;
    [SerializeField]private Wallet wallet;
    [SerializeField]private Transform tableAnchor;
    
    private bool betLock= false;
    [SerializeField]Stack<ICommand> _commandList = new Stack<ICommand>();
    public void OnChipClicked(int money, GameObject prefab)
    {
        ICommand command = new BetCommand(wallet,tableAnchor,prefab,money);
        if (command.Execute())
        {
            _commandList.Push(command);
        }
    }
    public void OnUndoClick()
    {
        if(betLock) return;
        if (_commandList.Count > 0) 
            _commandList.Pop().Undo();
            
    }

    public void LockBets()
    {
        betLock = true;
        _commandList.Clear();
        chipButtonsPanel.SetActive(false);
        UI_Manager.ui_Manager.undoButton.gameObject.SetActive(false);
    }

    public void ResetTableChips4NewRound()
    {       // index를 거꾸로 돌면서 지워야함. 왜냐하면 index가 0부터 시작하기 때문에, 0번째를 지우면 1번째가 0번째로 바뀌기 때문.
        for (int i = tableAnchor.childCount - 1; i >= 0; i--)
        {
            Destroy(tableAnchor.GetChild(i).gameObject);
        }
        betLock = false; 
        chipButtonsPanel.SetActive(true);
        UI_Manager.ui_Manager.undoButton.gameObject.SetActive(true);

    } 
}
