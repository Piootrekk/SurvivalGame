using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float checkrate = 0.04f;
    [SerializeField] private float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;

    private float lastCheckTime;
    private GameObject currentIteractGameObject;
    private IInteractable currentinteractable;

}

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}