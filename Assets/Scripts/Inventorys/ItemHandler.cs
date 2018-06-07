using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    #region Variables
    public int idNum;
    public string itemName;
    public string description;
    public int value;
    public int amount;
    public string icon;
    public string mesh;
    public ItemType type;
    #endregion
    public void OnCollection()
    {
        Item temp = new Item();
        temp.ID = idNum;
        temp.Name = itemName;
        temp.Description = description;
        temp.Value = value;
        temp.Amount = amount;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.MeshName = Resources.Load("Prefabs/" + mesh) as GameObject;
        temp.Type = type;

        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (temp.Type == ItemType.Reale) { inventory.reale += temp.Amount; }
        else if(temp.Type== ItemType.Crafting|| temp.Type == ItemType.Food|| temp.Type == ItemType.Ingredients)
        {
            int found = 0;
            int addIndex = 0;
            for (int i = 0; i < inventory.inv.Count; i++)
            {
                if(temp.ID == inventory.inv[i].ID) {
                    found = 1;
                    addIndex = i;
                }
            }
            if (found == 1)
            {
                inventory.inv[addIndex].Amount += temp.Amount;
            }
            else
            {
                inventory.inv.Add(temp);
            }
        }
        else
        {
            inventory.inv.Add(temp);
        }
        Destroy(this.gameObject);
    }
}
