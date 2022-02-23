using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobData
{
    public float Importance;
    public float Complexity;
    public string Key;
    public int Index;
    public WorkerData Worker;
    public ResourcesData Resources;

    public JobData(float importance, float complexity, string key, int index, WorkerData worker, ResourcesData resources)
    {
        Importance = importance;
        Complexity = complexity;
        Key = key;
        Index = index;
        Worker = worker;
        if(worker != null) worker.Job = this;
        this.Resources = resources;
    }
}


public class Job
{
    public Action<Resource, ProductionEnvironment> Production;
    public Action<Job, float> changedImportance;

    public Action<Job> addedWorker;
    public Action<Job, Worker> removedWorker;

    protected float importance = 0f;
    protected float complexity = 0f;
    protected string key;
    protected int index;

    protected Worker worker;
    protected Resources resources;

    public float Importance => importance;
    public float Complexity => complexity;
    public string Key => key;
    public int Index => Index;

    public Worker Worker => worker;
    public Resources Resources => resources;

    public Job(Resources resources, string key, float importance, int index)
    {
        this.resources = resources;
        this.key = key;
        this.importance = importance;
        this.index = index;
    }

    virtual public bool AssignWorker(Worker worker)
    {
        if (this.worker != null) return false;
        this.worker = worker;

        if (worker == null) return false;
        worker.SetJob(this);

        addedWorker?.Invoke(this);
        return true;
    }

    virtual public void Work(float deltaTime)
    {
        if (worker == null) return;
        foreach (var resource in resources.ResourceList)
        {
            Production?.Invoke(new Resource(resource.Key, resource.Amount * deltaTime), new ProductionEnvironment());
        }
    }

    virtual public Worker RemoveWorker()
    {
        Worker removedWorker = worker;
        worker = null;
        this.removedWorker?.Invoke(this, Worker);
        removedWorker?.RemoveJob();
        return removedWorker;
    }

    public void ChangeImportance(float newValue)
    {
        float oldValue = importance; 
        importance = newValue;
        changedImportance?.Invoke(this, oldValue);
    }

    public bool HasWorkerAssigned() => Worker != null;

    public virtual Job Duplicate()
    {
        return new Job(resources.Duplicate(), key, importance, index);
    }

    public virtual void AddedJobToTower(Tower tower)
    {
    }

    public JobData GetData() => new JobData(importance, complexity, key, index, worker != null ? worker.GetData(false) : null, resources.GetData());
}
