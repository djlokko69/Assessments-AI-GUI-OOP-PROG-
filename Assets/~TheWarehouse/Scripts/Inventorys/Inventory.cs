using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        public List<Item> inv = new List<Item>();
        public bool showInv, inShop, inChest;
        private Item selectedItem;
        public Vector2 scrollPos = Vector2.zero;
        float sW = Screen.width / 16;
        float sH = Screen.height / 9;

        public int reale;
        public string sortType;
        public Handler charH;
        public Shop shop;
        public Chest chest;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(102));
            inv.Add(ItemData.CreateItem(201));
            inv.Add(ItemData.CreateItem(302));
            charH = GetComponent<Handler>();
        }
        #endregion
        #region Update
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleInv();
            }
        }
        #endregion
        #region Toggle
        public bool ToggleInv()
        {
            if (showInv)
            {
                showInv = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;

                return false;
            }
            else
            {
                showInv = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                return true;
            }

        }
        #endregion
        #region GUI
        void OnGUI()
        {
            if (showInv)
            {
                sW = Screen.width / 16;
                sH = Screen.height / 9;

                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                /*for (int i = 0; i < inv.Count; i++)
                {
                   if (GUI.Button(new Rect(0.5f*sW,0.25f*sH+i*(0.25f*sH),3*sW,0.25f*sH),inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                   }
                }*/
                if (GUI.Button(new Rect(5.5f * sW, 0.25f * sH, 1 * sW, 0.25f * sH), "All"))
                {
                    sortType = "All";
                }
                if (GUI.Button(new Rect(6.5f * sW, 0.25f * sH, 1 * sW, 0.25f * sH), "Food")) { sortType = "Food"; }
                DisplayInv(sortType);

            }
            if (selectedItem != null)
            {

                GUI.DrawTexture(new Rect(11 * sW, 1.5f * sH, 2 * sW, 2 * sH), selectedItem.Icon);
                GUI.Box(new Rect(11 * sW, 3.5f * sH, 2 * sW, 2 * sH), selectedItem.Amount.ToString());
                GUI.Box(new Rect(11 * sW, 5.5f * sH, 2 * sW, 2 * sH), selectedItem.Description.ToString());

                #region Not Shop or Chest
                if (!(inShop || inChest))
                {
                    if (selectedItem.Type == ItemType.Food)
                    {
                        if (GUI.Button(new Rect(13 * sW, 6f * sH, sW, 0.25f * sH), "Eat"))
                        {
                            Debug.Log("OMG Yum, I like what I just ate.. what is it" + selectedItem.Name + "good to know");
                            if (selectedItem.Amount > 1)
                            {
                                selectedItem.Amount--;
                            }
                            else
                            {
                                inv.Remove(selectedItem);
                                selectedItem = null;
                            }
                        }
                    }
                    else if (selectedItem.Type == ItemType.Weapon)
                    {
                        if (GUI.Button(new Rect(15 * sW, 8.75f * sH, sW, 0.25f * sH), "Equip"))
                        {
                            Debug.Log("You equip the " + selectedItem.Name);
                        }
                    }
                }
#endregion
                #region Shop
                if (inShop)
                {
                    if(GUI.Button(new Rect(13 * sW, 6f * sH, sW, 0.25f * sH), "Sell"))
                    {
                        Debug.Log("Selling");
                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                            reale += selectedItem.Value;
                            shop.inv.Add(selectedItem);
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            reale += selectedItem.Value;
                            shop.inv.Add(selectedItem);
                            selectedItem = null;
                        }
                    }
                }
                #endregion
                #region Chest
                if (inChest&&!inShop)
                {
                    if(GUI.Button(new Rect(13 * sW, 6f * sH, sW, 0.25f * sH), "Store"))
                    {
                        Debug.Log("Storing");
                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                            chest.inv.Add(selectedItem);
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            chest.inv.Add(selectedItem);
                            selectedItem = null;
                        }
                    }
                }
                #endregion
            }
        }
        #endregion
        #region Display
        void DisplayInv(string sortType)
        {
            if (!(sortType == "All" || sortType == ""))
            {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
                int h = 0;
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        if (h <= 35)
                        {
                            // 35 or less show buttons
                            if (GUI.Button(new Rect(0.5f * sW, 0.25f * sH + h * (0.25f * sH), 3 * sW, 0.25f * sH), inv[i].Name))
                            {
                                selectedItem = inv[i];
                            }
                            h++;
                        }
                    }
                    else
                    {
                        // more than 35 show buttons in scroll view
                        scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * sH, 5 * sW, 8.75f * sH), scrollPos, new Rect(0, 0, 0, 8.75f * sH + ((inv.Count - 35) * 0.25f * sH)), false, true);
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * sW, 0f * sH + h * (0.25f * sH), 3 * sW, 0.25f * sH), inv[i].Name))
                            {
                                selectedItem = inv[i];
                                Debug.Log(selectedItem.Name);
                            }
                            h++;
                        }
                        GUI.EndScrollView();
                    }


                }
            }
            else
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv.Count <= 35)
                    {
                        // 35 or less show buttons
                        if (GUI.Button(new Rect(0.5f * sW, 0.25f * sH + i * (0.25f * sH), 3 * sW, 0.25f * sH), inv[i].Name))
                        {
                            selectedItem = inv[i];
                            Debug.Log(selectedItem.Name);
                        }
                    }
                    else
                    {
                        // more than 35 show buttons in scroll view
                        scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * sH, 5 * sW, 8.75f * sH), scrollPos, new Rect(0, 0, 0, 8.75f * sH + ((inv.Count - 35) * 0.25f * sH)), false, true);
                        if (GUI.Button(new Rect(0.5f * sW, 0f * sH + i * (0.25f * sH), 3 * sW, 0.25f * sH), inv[i].Name))
                        {
                            selectedItem = inv[i];
                            Debug.Log(selectedItem.Name);
                        }
                        GUI.EndScrollView();
                    }


                }
            }
        }
        #endregion
    }
}