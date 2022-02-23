using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : MonoBehaviour
{
    [SerializeField]
    FloorContentView floorContentView;

    private void Start()
    {
        if(!floorContentView)
        {
            Destroy(this);
            Debug.LogError("JobController has unfilled field 'floorContentView' -> Shutdown of Feature.");
        }
        floorContentView.CreatedJobView += OnJobViewCreation;
        floorContentView.DestroyedJobView += OnJobViewDestruction;
    }

    private void OnDestroy()
    {
        floorContentView.CreatedJobView -= OnJobViewCreation;
        floorContentView.DestroyedJobView -= OnJobViewDestruction;
    }

    public void OnJobViewCreation(JobView jobView)
    {
        jobView.ChangedJobImportance += OnChangedJobImportance;
        jobView.ClosedJob += OnClosedJob;
    }

    public void OnJobViewDestruction(JobView jobView)
    {
        jobView.ChangedJobImportance -= OnChangedJobImportance;
        jobView.ClosedJob -= OnClosedJob;
    }

    public void OnChangedJobImportance(Position position, int jobIndex, float newValue) => Model.Instance.ChangeJobImportance(position, jobIndex, newValue);

    public void OnClosedJob(Position position, int jobIndex, bool isOpen) => Model.Instance.ChangeJobState(position, jobIndex, isOpen);
}
