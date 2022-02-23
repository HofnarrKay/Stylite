using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intaract : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                if(interactable)
                {
                    interactable.interact();
                }
            }
        }
    }
}

