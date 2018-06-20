using UnityEngine;

namespace TheWarehouse
{
    public class Item
    {
        #region Private Variables
        private int _idNum;
        private string _name;
        private int _value;
        private int _amount;
        private string _description;
        private Texture2D _icon;
        private GameObject _mesh;
        private ItemType _type;
        #endregion
        #region Constructors
        public void ItemConstructor(int itemId, string itemName, Texture2D itemIcon, ItemType itemType)
        {
            _idNum = itemId;
            _name = itemName;
            _icon = itemIcon;
            _type = itemType;
        }
        #endregion
        #region Public Variables
        public int ID
        {
            get { return _idNum; }
            set { _idNum = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public Texture2D Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        public GameObject MeshName
        {
            get { return _mesh; }
            set { _mesh = value; }
        }
        public ItemType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion
    }
}
#region Enums
public enum ItemType
{
    Food,
    Weapon,
    Apparel,
    Crafting,
    Quest,
    Reale,
    Ingredients,
    Potions,
    Scrolls
}
#endregion
