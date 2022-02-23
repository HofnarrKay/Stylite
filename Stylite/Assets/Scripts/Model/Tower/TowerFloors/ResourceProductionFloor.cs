using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProductionFloorData : TowerFloorData
{
    public JobData[] Jobs;
    
    public ResourceProductionFloorData(string key, Job[] jobs) : base(key)
    {
        Jobs = new JobData[jobs.Length];
        for (int i = 0; i < jobs.Length; i++)
        {
            Jobs[i] = jobs[i].GetData();
        }
    }

    public override bool HasJobs() => true;
}

public class ResourceProductionFloor : TowerFloor
{

    public Action<Resource, ProductionEnvironment> Production;

    private Job[] jobs;

    public ResourceProductionFloor(string key, Job[] jobs) : base(key)
    {
        this.jobs = jobs;

        foreach (var job in jobs)
        {
            job.Production += OnProduction;
        }
    }

    public override void Process(float deltaTime)
    {
        if(jobs != null)
        {
            foreach (var job in jobs)
            {
                job.Work(deltaTime);
            }
        }
    }

    public override bool AssignWorker(Worker worker, string jobKey)
    {
        foreach (var job in jobs)
        {
            if(job.Key == jobKey)
            {
                if (job.AssignWorker(worker)) return true;
            }
        }

        return false;
    }

    public override void ConnectToTower(Tower tower)
    {
        tower.ConnectResourceProductionFloorToTower(this);
    }

    public override TowerFloor Duplicate()
    {
        Job[] jobs = new Job[this.jobs.Length];
        for (int i = 0; i < this.jobs.Length; i++)
        {
            jobs[i] = this.jobs[i].Duplicate();
        }

        return new ResourceProductionFloor(key, jobs);
    }

    public override TowerFloorData GetData() => new ResourceProductionFloorData(key, jobs);

    public override bool EqualsData(TowerFloorData towerFloor)
    {
        if(base.EqualsData(towerFloor))
        {
        //TODO: Resources were checked before, job check should be implemented here.
            return true;
        }
        return false;
    }

    public override Job[] GetJobs()
    {
        return jobs;
    }

    public void OnProduction(Resource resource, ProductionEnvironment productionEnvironment)
    {
        Production?.Invoke(new Resource(resource.Key, resource.Amount), new ProductionEnvironment());
    }

}
