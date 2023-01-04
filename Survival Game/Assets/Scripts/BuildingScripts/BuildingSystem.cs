using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask buildLayerMask;
    [SerializeField] private LayerMask cantBuildMask;
    [SerializeField] private Transform buildInUse;
    [SerializeField] private Material possitiveBuild;
    [SerializeField] private Material negativeBuild;

    private Material backup;
    private bool originalMaterialStored = false;
    private float _rotationY = 0;
    

    private static BuildingSystem instance;
    public bool IsExecuting { get; set; }
    public static BuildingSystem Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (IsBuildInUse())
        {
            BuildObject(buildInUse.GetChild(0).gameObject);
        }
    }
    private bool IsRayHitting(LayerMask mask, out RaycastHit hitInfo)
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        return Physics.Raycast(ray, out hitInfo, rayDistance, mask);
    }

    public void BuildObject(GameObject build)
    {
        if (build == null) return;
        IsExecuting = true;
        if (!originalMaterialStored)
        {
            backup = build.GetComponent<Renderer>().material;
            originalMaterialStored = true;
        }
        if (IsRayHitting(buildLayerMask, out RaycastHit hitInfo))
        {
           
            if (InputSystem.GetDevice<Keyboard>().rKey.wasPressedThisFrame)
            {
                _rotationY += 15;
            }
            else if (InputSystem.GetDevice<Keyboard>().tKey.wasPressedThisFrame)
            {
                _rotationY -= 15;
            }

            build.transform.position = hitInfo.point;
            build.transform.rotation = Quaternion.LookRotation(Vector3.right, hitInfo.normal);
            build.transform.rotation = Quaternion.Euler(build.transform.rotation.eulerAngles + new Vector3(0, _rotationY, 0));
            build.GetComponent<Renderer>().material = possitiveBuild;
            if (Physics.CheckBox(hitInfo.point, build.GetComponent<MeshCollider>().bounds.size / 2, build.transform.rotation, cantBuildMask))
            {
                build.GetComponent<Renderer>().material = negativeBuild;
                return;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var _build = Instantiate(build, hitInfo.point, build.transform.rotation);
                _build.GetComponent<Renderer>().material = backup;
                _build.layer = 11; //Buildable mask
                Destroy(buildInUse.GetChild(0).gameObject);
                DestroyAllChild(buildInUse);
                IsExecuting = false;
                originalMaterialStored = false;
            }
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Instantiate(build.GetComponent<ItemInUse>().Data.Prefab, Camera.main.transform.position, Quaternion.identity);
            Destroy(buildInUse.GetChild(0).gameObject);
            DestroyAllChild(buildInUse);
            IsExecuting = false;
            originalMaterialStored = false;
        }
    }

    private void DestroyAllChild(Transform _object)
    {
        if (_object.childCount > 0)
        {
            foreach(Transform child in _object)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private bool IsBuildInUse()
    {
        if (buildInUse.childCount == 0) return false;
        else return true;
    }

}
