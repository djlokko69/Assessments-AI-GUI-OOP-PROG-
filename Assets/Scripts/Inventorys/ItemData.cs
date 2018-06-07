using UnityEngine;

public static class ItemData
{
public static Item CreateItem(int itemId)
    {
        Item temp = new Item();
        string name = "";
        string description = "";
        int value = 0;
        int amount = 0;
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;

        switch (itemId)
        {
            #region Food 0-99
            case 0:
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 5;
                amount = 1;
                icon = "Apple_Icon";
                mesh = "Apple_Mesh";
                type = ItemType.Food;
                break;
            case 1:
                name = "Bread";
                description = "Taste doughy";
                value = 10;
                amount = 1;
                icon = "bread";
                mesh = "Bread_Mesh";
                type = ItemType.Food;
                break;
            case 2:
                name = "Cheese";
                description = "Smells Weird";
                value = 15;
                amount = 1;
                icon = "cheese";
                mesh = "Cheese_Mesh";
                type = ItemType.Food;
                break;
            #endregion
            #region Armour 100-199
            case 100:
                name = "Iron Helm";
                description = "Heavy Hat";
                value = 20;
                amount = 1;
                icon = "helmets";
                mesh = "IronHelm_Mesh";
                type = ItemType.Apparel;
                break;
            case 101:
                name = "Iron Gloves";
                description = "Iron Gauntlet";
                value = 25;
                amount = 1;
                icon = "gloves";
                mesh = "IronGloves_Mesh";
                type = ItemType.Apparel;
                break;
            case 102:
                name = "Iron Chest Plate";
                description = "Iron Armour";
                value = 50;
                amount = 1;
                icon = "armor";
                mesh = "IronChestPlate_Mesh";
                type = ItemType.Apparel;
                break;
            #endregion
            #region Weapon 200-299
            case 200:
                name = "Rough Tree Branch";
                description = "Branch off the ground";
                value = 0;
                amount = 1;
                icon = "Branch_Icon";
                mesh = "Branch_Mesh";
                type = ItemType.Weapon;
                break;
            case 201:
                name = "Iron Sword";
                description = "Iron Blade";
                value = 30;
                amount = 1;
                icon = "sword";
                mesh = "IronSword_Mesh";
                type = ItemType.Weapon;
                break;
            case 202:
                name = "Steel Axe";
                description = "Slightly Fancy Axe";
                value = 75;
                amount = 1;
                icon = "SteelAxe_Icon";
                mesh = "SteelAxe_Mesh";
                type = ItemType.Weapon;
                break;
            #endregion
            #region Crafting 300-399
            case 300:
                name = "Oak Branch";
                description = "Thick Branch of Oak";
                value = 3;
                amount = 1;
                icon = "OakBranch_Icon";
                mesh = "OakBranch_Mesh";
                type = ItemType.Crafting;
                break;
            case 301:
                name = "Iron Ore";
                description = "Iron in its Raw form";
                value = 5;
                amount = 1;
                icon = "IronOre_Icon";
                mesh = "IronOre_Mesh";
                type = ItemType.Crafting;
                break;
            case 302:
                name = "Iron Ingot";
                description = "a bar of solid iron";
                value = 15;
                amount = 1;
                icon = "ingots";
                mesh = "IronIngot_Mesh";
                type = ItemType.Crafting;
                break;
            #endregion
            #region Ingredients 400-499
            case 400:
                name = "Mandrake Root";
                description = "Root of a screaming plant";
                value = 20;
                amount = 1;
                icon = "MandrakeRoot_Icon";
                mesh = "MandrakeRoot_Mesh";
                type = ItemType.Ingredients;
                break;
            case 401:
                name = "FireFly";
                description = "Small Glowing Bug";
                value = 3;
                amount = 1;
                icon = "FireFly_Icon";
                mesh = "FireFly_Mesh";
                type = ItemType.Ingredients;
                break;
            case 402:
                name = "Silver Dust";
                description = "The Dust thats made of Silver";
                value = 50;
                amount = 1;
                icon = "SilverDust_Icon";
                mesh = "SilverDust_Icon";
                type = ItemType.Ingredients;
                break;
            #endregion
            #region Potions 500-599
            case 500:
                name = "Health";
                description = "Heals you";
                value = 75;
                amount = 1;
                icon = "Health_Icon";
                mesh = "Health_Mesh";
                type = ItemType.Potions;
                break;
            case 501:
                name = "Mana";
                description = "Blue stuff that makes you more rad";
                value = 75;
                amount = 1;
                icon = "Mana_Icon";
                mesh = "Mana_Mesh";
                type = ItemType.Potions;
                break;
            case 502:
                name = "Stamina";
                description = "Makes you run faster ";
                value = 75;
                amount = 1;
                icon = "Stamina_Icon";
                mesh = "Stamina_Mesh";
                type = ItemType.Potions;
                break;
            #endregion
            #region Scroll 600-699
            case 600:
                name = "FireBall";
                description = "";
                value = 150;
                amount = 1;
                icon = "FireBallScroll_Icon";
                mesh = "FireBallScroll_Mesh";
                type = ItemType.Scrolls;
                break;
            case 601:
                name = "Levitation";
                description = "";
                value = 150;
                amount = 1;
                icon = "LevitationScroll_Icon";
                mesh = "LevitationScroll_Mesh";
                type = ItemType.Scrolls;
                break;
            case 602:
                name = "Invisibility";
                description = "";
                value = 150;
                amount = 1;
                icon = "InvisibilityScroll_Icon";
                mesh = "InvisibilityScroll_Mesh";
                type = ItemType.Scrolls;
                break;
            #endregion
            #region Misc 700-799
            //Quest
            //Money and Gems
            
            case 700:
                name = "Coin";
                description = "Money";
                value = 1;
                amount = 1;
                icon = "Coin_Icon";
                mesh = "Coin_Mesh";
                type = ItemType.Reale;
                break;
            case 701:
                name = "Statue De JayRay";
                description = "Statue of the God JayRay from the Gay Way religion";
                value = -1;
                amount = 1;
                icon = "StatueJayRay_Icon";
                mesh = "StatueJayRay_Mesh";
                type = ItemType.Quest;
                break;
            case 702:
                name = "Vape";
                description = "The magic flavoured smoke machine";
                value = -1;
                amount = 1;
                icon = "Vape_Icon";
                mesh = "Vape_Mesh";
                type = ItemType.Quest;
                break;
            #endregion
            default:
                itemId = 0;
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 5;
                amount = 1;
                icon = "apple";
                mesh = "Apple_Mesh";
                type = ItemType.Food;
                break;
        }
        temp.Name = name;
        temp.ID = itemId;
        temp.Description = description;
        temp.Value = value;
        temp.Amount = amount;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.MeshName = Resources.Load("Prefabs/" + mesh) as GameObject;
        temp.Type = type;

        return temp;
    }
}
