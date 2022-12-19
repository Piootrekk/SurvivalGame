using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PickItems : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] TextMeshProUGUI pickUpText;

    private UI_InventoryManager inventoryHandler;
    private Ray ray;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = GameObject.Find("Player").GetComponent<InputManager>();
        inventoryHandler = gameObject.GetComponent<UI_InventoryManager>();
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
            var item = hitInfo.collider.gameObject;
            SetUpText(item.GetComponent<ItemObjectInGame>().InstanceInInventory.GetComponent<UI_ItemData>().ItemData.NameItem);
            if (inputManager.Interactive)
            {
                if (!inventoryHandler) { return; }
                if (inventoryHandler.ItemAdd(item))
                {
                    IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                    interactable?.OnInteract();
                }
            }
        }
        else DistableText();
    }
    private void SetUpText(string name)
    {
        pickUpText.gameObject.SetActive(true);
        pickUpText.text = string.Format($"<b> Press <F> to pick {name}  </b>");
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

