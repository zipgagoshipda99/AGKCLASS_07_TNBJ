
using System.Windows.Input;

public class BetCommand : IAction
{
    // Start is called before the first frame update
    public Chip _chip;
    public Wallet _wallet;
    public BetCommand(Chip chip, Wallet wallet) //constructor a method called at instatiation of a class.
    {
        this._chip = chip;
        this._wallet = wallet;
    }
    public bool Execute()
    {
        _chip.PlayAnim();
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
