

using UnityEngine;

public class BetCommand : ICommand
{
    // Start is called before the first frame update
    
    private readonly Wallet _wallet;
    private readonly GameObject _prefab;
    private readonly Transform _anchor;
    private GameObject _spawnedChip;
    private readonly int _money;
    public BetCommand(Wallet wallet, Transform anchor, GameObject prefab, int money) //constructor a method called at instatiation of a class.
    {
        _wallet = wallet;
        _anchor = anchor;
        _prefab = prefab;
        _money = money;
    }
    public bool Execute()
    {
        if (!_wallet.PlaceBet(_money))return false;
        _spawnedChip = Object.Instantiate(_prefab, _anchor);
        
        return true;

    }

    // Update is called once per frame
    public void Undo()
    {
        _wallet.RefundBet(_money);
        Object.Destroy(_spawnedChip);
    }
}
