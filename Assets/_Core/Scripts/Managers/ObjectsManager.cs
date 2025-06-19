using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance;
    [SerializeField]
    private List<Harvestable> _harvestables;
    public List<Harvestable> Harvestables => _harvestables;

   

    [SerializeField]
    private Dictionary<byte, List<Unit>> _units = new Dictionary<byte, List<Unit>>();
    public Dictionary<byte, List<Unit>> Units => _units;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    public void AddHarvestable(Harvestable harvestable)
    {
        _harvestables.Add(harvestable);
    }
    public void RemoveHarvestable(Harvestable harvestable)
    {
        if (!_harvestables.Contains(harvestable))
        {
            return;
        }
        _harvestables.Remove(harvestable);
    }

    public void AddUnit(Unit unit)
    {
        if (!_units.ContainsKey(unit.Side))
        {
            _units.Add(unit.Side, new List<Unit>());
        }
        _units[unit.Side].Add(unit);
    }
    public void RemoveUnit(Unit unit)
    {
        if (!_units[unit.Side].Contains(unit))
        {
            return;
        }
        _units[unit.Side].Remove(unit);
    }
}
