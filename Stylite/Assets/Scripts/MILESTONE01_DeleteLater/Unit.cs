using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{

}

public class Unit : MonoBehaviour
{
    [SerializeField] private int movementPoints = 20;
    
    public int MovementPoints { get => movementPoints; }

    [SerializeField] private float movementDuration = 1f, rotationDuration = 0.3f;

    private GlowHighlight glowHighlight;
    private Queue<Vector3> pathPositions = new Queue<Vector3>();

    public event Action<Unit> MovementFinished;

    private void Awake()
    {
        glowHighlight = GetComponent<GlowHighlight>();
    }
    internal void Deselect()
    {
        glowHighlight.ToggleGlow(false);
    }
    public void Select()
    {
        glowHighlight.ToggleGlow();
    }
    public void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPositions = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = pathPositions.Dequeue();
        StartCoroutine(RotationCoroutine(firstTarget, rotationDuration, true));
    }
    private IEnumerator RotationCoroutine(Vector3 endPosition, float rotationDuration, bool firstRotation = false)
    {
        Quaternion startRotation = transform.rotation;
        endPosition.y = transform.position.y;
        Vector3 direction = endPosition - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        if(Mathf.Approximately(Mathf.Abs(Quaternion.Dot(startRotation, endRotation)), 1.0f) == false)
        {
            float timeElapsed = 0;
            while(timeElapsed < rotationDuration)
            {
                timeElapsed += Time.deltaTime;
                float lerpStep = timeElapsed / rotationDuration;
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerpStep);
                yield return null;
            }
            transform.rotation = endRotation;
        }
        StartCoroutine(MovementCoroutine(endPosition));
    }

    private IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        Vector3 startPostion = transform.position;
        endPosition.y = startPostion.y;
        float timeElapsed = 0f;

        while(timeElapsed < movementDuration)
        {
            timeElapsed += Time.deltaTime;
            float lerpStep = timeElapsed / movementDuration;
            transform.position = Vector3.Lerp(startPostion, endPosition, lerpStep);
            yield return null;
        }
        transform.position = endPosition;

        if(pathPositions.Count > 0)
        {
            Debug.Log("Selecting the next position!");
            StartCoroutine(RotationCoroutine(pathPositions.Dequeue(), rotationDuration));
        }
        else
        {
            Debug.Log("Movement finished!");
            MovementFinished?.Invoke(this);
        }
    }

    public UnitData GetData() => new UnitData();
}
