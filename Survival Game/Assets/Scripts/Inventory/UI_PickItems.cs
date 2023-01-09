using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PickItems : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] TextMeshProUGUI pickUpText;

    private Ray ray;
    private InputManager inputManager;

    private float pickUpTimer = 0f;
    private float pickUpInterval = 0.15f;

    private void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }

    private void Update()
    {
        RayCastInteractionable();
    }


    private void RayCastInteractionable()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer) && !inputManager.Inventory)
        {
            var item = hitInfo.collider.gameObject;
            if (item.GetComponent<ItemObjectInGame>() != null)
            {
                SetUpText("with "+item.GetComponent<ItemObjectInGame>().InstanceInInventory.GetComponent<UI_ItemData>().ItemData.NameItem);
            }
            else if (item.GetComponent<BuildInteractable>() != null)
            {
                SetUpText("");
            }
            if (inputManager.Interactive && pickUpTimer >= pickUpInterval)
            {
                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                interactable?.OnInteract();
                pickUpTimer = 0f;
            }
        }
        else DistableText();
        pickUpTimer += Time.deltaTime;
    }
    private void SetUpText(string name)
    {
        pickUpText.gameObject.SetActive(true);
        pickUpText.text = string.Format($"<b> Press <F> to interact {name} </b>");
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

