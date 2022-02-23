using System;
public class TowerFloorData
{
    public string Key;

    public TowerFloorData(string key)
    {
        Key = key;
    }

    public virtual bool HasJobs() => false;
}

public class TowerFloor : GameComponent
{
    protected string key = "ResourceFloor";
    public string Key
    {
        get => key;
        set
        {
            key = value;
            dirty = true;
        }
    }

    bool dirty = true;

    TowerFloorData towerFloorData;
    
    public TowerFloor(string key)
    {
        this.key = key;
    }

    public virtual void ConnectToTower(Tower tower) { }

    public virtual bool EqualsData(TowerFloorData towerFloor)
    {
        return key == towerFloor.Key;
    }

    public virtual Job[] GetJobs() => null;

    public virtual bool AssignWorker(Worker worker, string jobKey)
    {
        return false;
    }
    public virtual TowerFloorData GetData()
    {
        if (dirty)
        {
            towerFloorData = new TowerFloorData(key);
            dirty = false;
        }

        return towerFloorData;
    }

    public virtual TowerFloor Duplicate()
    {
        return new TowerFloor(key);
    }
}
