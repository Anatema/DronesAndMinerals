using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MapDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourceText;
    private Dictionary<byte, int> _resources;
    // Start is called before the first frame update
    void Start()
    {
        _resources = BattleManager.Instance.Resoutces;
        BattleManager.Instance.OnResourceValueChanged.AddListener(OnResourcevalueChanged);
        RefreshText();
    }
    private void OnResourcevalueChanged(byte side, int amount)
    {
        if (!_resources.ContainsKey(side))
        {
            _resources.Add(side, 0);
        }
        _resources[side] = amount;
        RefreshText();
    }
    private void RefreshText()
    {
        _resourceText.text = string.Empty;
        foreach(byte player in _resources.Keys)
        {
            _resourceText.text += $"Player {player}: {_resources[player]} minerals\n";
        }
    }
    private void OnDestroy()
    {

        BattleManager.Instance.OnResourceValueChanged.RemoveListener(OnResourcevalueChanged);
    }
}
