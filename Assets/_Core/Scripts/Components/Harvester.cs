using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Harvester : MonoBehaviour
{
    [SerializeField]
    private int _harvestCapasity;
    public int HarvestCapasity => _harvestCapasity;
    [SerializeField]
    private int _harvested;
    public int Harvested => _harvested;
    [SerializeField]
    private float _harvestCooldown;
    public float HarvestCooldown => _harvestCooldown;
    [SerializeField]
    private int _amountPerHarvest;
    private Unit _unit;

    [SerializeField]
    private GameObject _harvestingIndicator;
    [SerializeField]
    private ParticleSystem _miningParticles;
    [SerializeField]
    private ParticleSystem _uploadParticles;
    // Start is called before the first frame update
    void Awake()
    {
        _unit = GetComponent<Unit>();

    }
    private void Start()
    {
        _harvestingIndicator.SetActive(false);
    }
    public void StartHarvest()
    {
        UnitState state = new SearchHarvestableState(_unit, this);
        _unit.SetState(state);

    }
    public void Unload(Cargo cargo)
    {
        _uploadParticles.Play();
        cargo.LoadCargo(_harvested);
        _harvested = 0;
        _harvestingIndicator.SetActive(false);
    }
    public bool SearchForCargo(out Cargo returnCargo)
    {
        
        List<Unit> units = ObjectsManager.Instance.Units[_unit.Side];
        List<Cargo> cargos = new List<Cargo>();

        foreach(Unit unit in units)
        {
            if(unit.TryGetComponent(out Cargo cargo))
            {
                cargos.Add(cargo);
            }
        }

        returnCargo = null;
        if (cargos.Count <= 0)
        {
            return false;
        }

        float minDistance = Mathf.Infinity;

        foreach (Cargo cargo in cargos)
        {
            if (minDistance > Vector3.Distance(cargo.transform.position, _unit.transform.position))
            {
                minDistance = Vector3.Distance(cargo.transform.position, _unit.transform.position);
                returnCargo = cargo;
            }

        }
        if (!returnCargo)
        {
            return false;
        }
        return true;
    }

    public void Stopharvesting()
    {
        _miningParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
    public void Harvest(Harvestable harvestable, bool isFirst = false)
    {
        int harvestAmount = _amountPerHarvest;
        if (isFirst)
        {
            harvestAmount = 0;
        }
        if (harvestable == null || (harvestable.IsHarvested && harvestable.HarvestedBy != _unit))
        {
            _unit.SetState(new SearchHarvestableState(_unit, this));
            return;
        }
        _miningParticles.Play();
        if ((_harvestCapasity - (_harvested + harvestAmount)) < 0)
        {
            harvestAmount = (_harvested + harvestAmount) - _harvestCapasity;
        }
        harvestable.Harvest(_unit, harvestAmount, out int harvested);

        _harvested += harvested; 
        if(_harvested > 0)
        {
            _harvestingIndicator.SetActive(true);
        }
        
    }
    public bool SearchForHarvestable(out Harvestable returnHarvestable)
    {
        returnHarvestable = null;
        List<Harvestable> harvestables = ObjectsManager.Instance.Harvestables;
        if (harvestables.Count <= 0)
        {
            return false;
        }

        float minDistance = Mathf.Infinity;

        foreach (Harvestable harvestable in harvestables)
        {
            if (harvestable.IsHarvested)
            {
                continue;
            }
            if (minDistance > Vector3.Distance(harvestable.transform.position, _unit.transform.position))
            {
                minDistance = Vector3.Distance(harvestable.transform.position, _unit.transform.position);
                returnHarvestable = harvestable;
            }

        }
        if (!returnHarvestable)
        {
            return false;
        }

        return true;


        
    }
}
