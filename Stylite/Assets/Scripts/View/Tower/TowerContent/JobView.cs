using System;
using UnityEngine;
using UnityEngine.UI;

public class JobView : MonoBehaviour
{
    public Action<Position, int, float> ChangedJobImportance;
    public Action<Position, int, bool> ClosedJob;

    [SerializeField]
    private Text inhabitantName;

    [SerializeField]
    private Text jobName;

    [SerializeField]
    private Text resourceText;

    [SerializeField]
    private Text importanceText;

    private Position position;

    private float importance = 0;

    private int jobIndex = 0;

    public int JobIndex
    {
        get => jobIndex;
    }

    public void Setup(JobData job, int jobIndex, Position position)
    {
        this.jobIndex = jobIndex;
        this.position = position;

        Visualize(job);
    }

    public void Visualize(JobData job)
    {
        this.importance = job.Importance;
        jobName.text = job.Key;
        inhabitantName.text = job.Worker != null ? job.Worker.Inhabitant.Name : "empty";

        string message = "";
        foreach (var resource in job.Resources.ResourceList)
        {
            message += resource.key + ": " + resource.amount + "\n";
        }

        importanceText.text = importance.ToString();
        resourceText.text = message;
    }

    public void Destroy()
    {
       Destroy(gameObject);
    }

    public void ChangeJobImportance(float value)
    {
        ChangedJobImportance?.Invoke(position, jobIndex, importance + value);
    }


    public void ChangeJobState(bool isOpen) => ClosedJob?.Invoke(position, jobIndex, isOpen);
}
