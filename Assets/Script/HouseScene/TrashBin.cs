using UnityEngine;

public class TrashBin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InventoryManager.BinType binType;

    // ฟังก์ชันนี้ขยะจะเป็นคนเรียกใช้ เพื่อถามว่าตัวเองทิ้งลงถังนี้ได้ไหม
    public bool IsCorrectTrash(InventoryManager.ItemType itemType)
    {
        switch (binType)
        {
            case InventoryManager.BinType.Organic:
                // ถังขยะเปียก (สีเขียว)
                return itemType == InventoryManager.ItemType.Banana || itemType == InventoryManager.ItemType.Apple;

            case InventoryManager.BinType.Recycle:
                // ถังรีไซเคิล (สีเหลือง)
                return itemType == InventoryManager.ItemType.Plastic || itemType == InventoryManager.ItemType.Bottle;

            case InventoryManager.BinType.General:
                // ถังทั่วไป (สีน้ำเงิน)
                return itemType == InventoryManager.ItemType.Bag || itemType == InventoryManager.ItemType.Straw;

            case InventoryManager.BinType.Hazardous:
                // ถังอันตราย (สีแดง)
                return itemType == InventoryManager.ItemType.Bulb || itemType == InventoryManager.ItemType.Glass;

            default:
                return false;
        }
    }
}
