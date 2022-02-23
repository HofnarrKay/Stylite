using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobAssignment
{
    Dictionary<bool, Dictionary<float, List<Job>>> existingJobs;

    List<Job> closedJobs;

    List<Worker> jobless;

    public void Setup()
    {
        closedJobs = new List<Job>();
        jobless = new List<Worker>();

        existingJobs = new Dictionary<bool, Dictionary<float, List<Job>>>();
        existingJobs.Add(true, new Dictionary<float, List<Job>>());
        existingJobs.Add(false, new Dictionary<float, List<Job>>());
    }

    public void Shutdown()
    {

    }

    public void AddJob(Job job)
    {
        JoblessAssignment(job);
        AddJobToDictionary(job);
        CalculateNewPossibleJob(job);

        job.changedImportance += OnJobChangedImportance;
    }

    private void AddJobToDictionary(Job job)
    {
        if (!existingJobs[job.HasWorkerAssigned()].ContainsKey(job.Importance))
        {
            existingJobs[job.HasWorkerAssigned()].Add(job.Importance, new List<Job>());
        }

        existingJobs[job.HasWorkerAssigned()][job.Importance].Add(job);
    }

    private void RemoveJobFromDictionary(Job job)
    {
        existingJobs[job.HasWorkerAssigned()][job.Importance].Remove(job);
        if (existingJobs[job.HasWorkerAssigned()][job.Importance].Count == 0) existingJobs[job.HasWorkerAssigned()].Remove(job.Importance);
    }

    public void RemoveJob(Job job)
    {
        if(closedJobs.Contains(job))
        {
            closedJobs.Remove(job);
            return;
        }

        RemoveJobFromDictionary(job);
        job.changedImportance -= OnJobChangedImportance;
    }

    public bool AssignWorker(Worker worker)
    {
        if(existingJobs[false].Count > 0)
        {
            List<Job> maxImportance = existingJobs[false].Aggregate((l, r) => l.Key > r.Key ? l : r).Value;
            if(maxImportance.Count > 0)
            {
                Job chosenJob = maxImportance[0];
                chosenJob.AssignWorker(worker);
                existingJobs[false][chosenJob.Importance].Remove(chosenJob);
                AddJobToDictionary(chosenJob);
                return true;
            }
        }

        jobless.Add(worker);
        return false;
    }


    public void OnJobChangedImportance(Job job, float oldImportance)
    {
        existingJobs[job.HasWorkerAssigned()][oldImportance].Remove(job);
        AddJobToDictionary(job);

        CalculateNewPossibleJob(job);
    }

    public void ChangeJobState(Job job, bool isOpen)
    {
        if(existingJobs[job.HasWorkerAssigned()].ContainsKey(job.Importance))
        {
            if(existingJobs[job.HasWorkerAssigned()][job.Importance].Contains(job))
            {
                if(!isOpen)
                {
                    existingJobs[job.HasWorkerAssigned()][job.Importance].Remove(job);
                    if(job.HasWorkerAssigned())
                    {
                        Worker worker = job.RemoveWorker();
                        if(worker != null) jobless.Add(worker);
                    }
                    closedJobs.Add(job);
                }
            }
        }

        if(closedJobs.Contains(job))
        {
            if(isOpen)
            {
                closedJobs.Remove(job);
                AddJobToDictionary(job);
                JoblessAssignment(job);
                CalculateNewPossibleJob(job);
            }
        }
    }

    public void CalculateNewPossibleJob(Job job)
    {
        if(existingJobs[!job.HasWorkerAssigned()].Count > 0)
        {
            List<Job> minImportanceWithWorkers = existingJobs[true].Aggregate((l, r) => l.Key < r.Key ? l : r).Value;
            List<Job> maxImportanceWithoutWorkers = existingJobs[false].Aggregate((l, r) => l.Key > r.Key ? l : r).Value;

            if (!(minImportanceWithWorkers.Count > 0 && maxImportanceWithoutWorkers.Count > 0)) return;

            Job chosenWorkedJob = minImportanceWithWorkers[0];
            Job chosenFreeJob = maxImportanceWithoutWorkers[0];

            if (chosenWorkedJob.Importance < chosenFreeJob.Importance )
            {
                RemoveJobFromDictionary(chosenFreeJob);
                RemoveJobFromDictionary(chosenWorkedJob);
                chosenFreeJob.AssignWorker(chosenWorkedJob.RemoveWorker());
                
                AddJobToDictionary(chosenFreeJob);
                AddJobToDictionary(chosenWorkedJob);
            }
        }
    }

    public void JoblessAssignment(Job job)
    {
        if(jobless.Count > 0)
        {
            job.AssignWorker(jobless[0]);
            jobless.RemoveAt(0);
            AddJobToDictionary(job);
        }
    }
}
