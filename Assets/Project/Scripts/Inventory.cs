using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{
    private InputActionReference secondaryActionButton;
    [SerializeField] private ActionBasedController leftController;
    [SerializeField] private InputHelpers.Button buttonToTestFor;
    [SerializeField] private GameObject prefab;

    private void Start()
    {
    }

    private void Update()
    {
        // Debug.Log(leftController.activateAction.action.ReadValue<float>());
        // (buttonToTestFor, out bool pressed);
        if (leftController.activateAction.action.ReadValue<float>() > 0.5f) prefab.SetActive(!prefab.activeSelf);
    }
    


    public void Foo()
    {
        Instantiate(prefab, new Vector3(-2.5f, 1.5f, 0), Quaternion.identity);
    }
}