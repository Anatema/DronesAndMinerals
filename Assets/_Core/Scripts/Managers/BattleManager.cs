using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    private Dictionary<byte, int> _resources = new Dictionary<byte, int>();
    public Dictionary<byte, int> Resoutces => _resources;

    [HideInInspector]
    public UnityEvent<byte, int> OnResourceValueChanged = new UnityEvent<byte, int>();

    public static BattleManager Instance;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

   
    public void AddResource(byte side, int amount)
    {
        if (!_resources.ContainsKey(side))
        {
            _resources.Add(side, 0);
        }
        _resources[side] += amount;
        OnResourceValueChanged?.Invoke(side, _resources[side]);
    }

    public void ChangeNumberOfDrones(int number)
    {
        Dictionary<byte, List<Unit>> units = ObjectsManager.Instance?.Units;

       
        foreach (byte key in units.Keys)
        {
            List<Harvester> harvesters = new List<Harvester>();

            foreach(Unit unit in units[key])
            {
                if (unit.TryGetComponent(out Harvester harvester))
                {
                    harvesters.Add(harvester);
                }
            }
            for (int i = 0; i < harvesters.Count; i++)
            {

                if(i < number)
                {
                    harvesters[i].gameObject.SetActive(true);
                    continue;
                }
                harvesters[i].gameObject.SetActive(false);
            }
        }
    }
}
