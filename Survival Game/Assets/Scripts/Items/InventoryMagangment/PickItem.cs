using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickItem : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] TextMeshProUGUI pickUpText;

    private Ray ray;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = GameObject.Find("Player").GetComponent<InputManager>();
    }

    private void Update()
    {
        RayCastInteractionable();
    }
    

    private void RayCastInteractionable()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer))
        {
            SetUpText();
            if (inputManager.Interactive)
            {
                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                interactable?.OnInteract();
            }
        }
        else DistableText();
    }
    private void SetUpText()
    {
        pickUpText.gameObject.SetActive(true);
        pickUpText.text = string.Format($"<b> Press <{inputManager.CurrentPathInput}F> to pick item </b>");
    }

    private void DistableText()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray);
    }

}

public interface IInteractable
{
    void OnInteract();
}
