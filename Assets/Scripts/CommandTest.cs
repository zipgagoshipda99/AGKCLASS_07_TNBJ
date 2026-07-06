using UnityEngine;

public class CommandTest : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private Transform tableAnchor;

    private BetCommand lastCmd;   // 임시 — 진짜 Stack은 BetInput에서

    void Update()   // 임시 입력 테스트라 Update 허용 — 게임 로직 아님
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            var cmd = new BetCommand(wallet, tableAnchor, chipPrefab, 30);
            bool ok = cmd.Execute();
            if (ok) lastCmd = cmd;
            Debug.Log($"베팅: {ok}, 잔액: {wallet.totalMoney}");
        }

        if (Input.GetKeyDown(KeyCode.U) && lastCmd != null)
        {
            lastCmd.Undo();
            lastCmd = null;
            Debug.Log($"언두, 잔액: {wallet.totalMoney}");
        }
    }
}