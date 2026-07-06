using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BetInput : MonoBehaviour
{
    [SerializeField]private Wallet wallet;
    [SerializeField]private Transform tableAnchor;
    
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
        if (_commandList.Count > 0) 
            _commandList.Pop().Undo();
    }

    public void LockBets()
    {
        _commandList.Clear();
    }


}
