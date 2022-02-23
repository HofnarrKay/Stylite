using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerData
{
    public InhabitantData Inhabitant;
    public JobData Job;

    public WorkerData(InhabitantData inhabitant, JobData job)
    {
        Inhabitant = inhabitant;
        Job = job;
    }
}

public class Worker
{
    public Action<Worker, Job> GotJob;
    public Action<Worker> LeftJob;

    private Inhabitant inhabitant;
    private Job job;

    public Job Job
    {
        get => job;
    }

    public Worker(Inhabitant inhabitant)
    {
        this.inhabitant = inhabitant;
    }

    public void SetJob(Job job)
    {
        this.job = job;
        GotJob?.Invoke(this, job);
    }

    public void RemoveJob()
    {
        job = null;
        LeftJob?.Invoke(this);
    }

    public bool HasJob() => job == null;

    public void AddExperience(Experience experience) => inhabitant.AddExperience(experience);

    public Experience GetExperience(string key) => inhabitant.GetExperience(key);

    public string GetName() => inhabitant.Name;

    public WorkerData GetData(bool withJob) => new WorkerData(inhabitant.GetData(), withJob ? job.GetData() : null);
}
