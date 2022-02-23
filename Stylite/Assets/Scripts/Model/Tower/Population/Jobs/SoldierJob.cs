using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierJobData : JobData
{
    public SoldierJobData(float importance, float complexity, string key, int index, WorkerData worker, ResourcesData resources) : base(importance, complexity, key, index, worker, resources)
    {

    }
}

public class SoldierJob : Job
{
    public SoldierJob(Resources resources, string key, float importance, int index) :base(resources, key, importance, index)
    {
        
    }


    public override bool AssignWorker(Worker worker)
    {
        if(base.AssignWorker(worker))
        {
            if(worker.GetExperience("Soldier") == null)
            {
                worker.AddExperience(new SoldierExperience("Soldier"));
            }

            return true;
        }
        return false;
    }

    public override Job Duplicate()
    {
        return new SoldierJob(resources.Duplicate(), key, importance, index);
    }

    public override void AddedJobToTower(Tower tower)
    {
        tower.AddedSoldierJob(this);
    }
}
