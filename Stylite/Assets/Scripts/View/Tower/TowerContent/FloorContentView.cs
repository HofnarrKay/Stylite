using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorContentView : MonoBehaviour
{
    public Action<JobView> CreatedJobView;
    public Action<JobView> DestroyedJobView;
    private Action<TowerData, Position> currentFocus;

    [SerializeField]
    private GameObject jobViewPrefab;

    [SerializeField]
    private TowerContentView towerView;

    [SerializeField]
    private Transform jobSocket;

    private Position currentPosition;

    private Position lastFramePosition;

    private List<JobView> visualizedJobs = new List<JobView>();

    private void Start()
    {
        currentPosition = new Position(-1);
        lastFramePosition = new Position(-2);
        towerView.CreatedFloor += OnFloorCreation;
        towerView.DestroyedFloor += OnFloorDestruction;
    }

    private void OnDestroy()
    {
        towerView.CreatedFloor -= OnFloorCreation;
        towerView.DestroyedFloor -= OnFloorDestruction;
    }

    public void Visualize(TowerData towerData) => currentFocus?.Invoke(towerData, currentPosition);

    public void VisualizeJobsInFloor(Position position)
    {
        currentPosition = position;
        currentFocus = OnChangedJobsVisualization;

        if (position.Equals(lastFramePosition))
        {
            currentPosition = new Position(-1);
            currentFocus = OnClosedVisualization;
            return;
        }
        else
        {
            if(!lastFramePosition.Equals(new Position(-2))) currentFocus = OnChangedJobsVisualization;
        }
    }

    public void OnVisualizeJobsInFloor(TowerData tower, Position position)
    {
        foreach (JobView jobView in visualizedJobs)
        {
            //TODO: modular system for Jobs and their recognition
            jobView.Visualize(((ResourceProductionFloorData)tower.Floors[position]).Jobs[jobView.JobIndex]);
        }

        if(position.Equals(lastFramePosition)) return;

        if (tower.Floors[position].HasJobs())
        {
            foreach (var job in ((ResourceProductionFloorData)tower.Floors[position]).Jobs)
            {
                GameObject createdObject = Instantiate(jobViewPrefab, jobSocket);
                JobView jobView = createdObject.GetComponent<JobView>();
                if (jobView)
                {
                    jobView.Setup(job, job.Index, position);

                    visualizedJobs.Add(jobView);
                    CreatedJobView?.Invoke(jobView);
                }
                else
                {
                    Destroy(jobView);
                }
            }
        }

        lastFramePosition = position;
    }

    private void OnChangedJobsVisualization(TowerData tower, Position position)
    {
        OnClosedVisualization(tower, position);
        //i know its weird, but without the instant call, there would be a frame without a view, and without the event i feel bad
        OnVisualizeJobsInFloor(tower, position);
        currentFocus = OnVisualizeJobsInFloor;
    }

    private void OnClosedVisualization(TowerData tower, Position position)
    {
        foreach (JobView jobView in visualizedJobs)
        {
            DestroyedJobView?.Invoke(jobView);
            jobView.Destroy();
        }
        visualizedJobs.Clear();
        lastFramePosition = new Position(-2);
    }

    public void OnFloorInteraction(Interactable interactable) => interactable.ViewFloorContentInteraction(this);

    public void OnFloorCreation(FloorView floorView)
    {
        Interactable interactable = floorView.gameObject.GetComponent<Interactable>();
        if (interactable) interactable.Interaction += OnFloorInteraction;
    }

    public void OnFloorDestruction(FloorView floorView)
    {
        Interactable interactable = floorView.gameObject.GetComponent<Interactable>();
        if (interactable) interactable.Interaction -= OnFloorInteraction;
    }
}
