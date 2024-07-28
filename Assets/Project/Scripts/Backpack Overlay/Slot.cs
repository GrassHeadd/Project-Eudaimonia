using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemInSlot;
    public Image slotImage;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

    // Update is called once per frame
    private void onTriggerStay(Collider other)
    {
        if(itemInSlot != null) {return;}
        GameObject obj = other.gameObject;
        if(!IsItem(obj)) return;
        if(true/*controller command for grabbing the object*/) {
            InsertItem(obj);
        }
    }

    public bool IsItem(GameObject obj) {
        return obj.GetComponent<Item>();
    }

    public void InsertItem(GameObject obj) {
        obj.GetComponent<Item>();
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.eulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        itemInSlot = obj;
        slotImage.color = Color.gray;
    }

    public void ResetColor() {
        slotImage.color = originalColor;
    }

}
