using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Objects/ShopItem")]
public class ShopItem : ScriptableObject
{
    [Tooltip("Name of the item")]
    public string itemName;

    [Tooltip("Sprite image of the item")]
    public Image itemImage;

    [Tooltip("Cost of the item")]
    public float itemCost;

    [Tooltip("Describe item including buff")]
    public string toolTip;

    [Tooltip("Is this item an upgrade? Will be used to change item display")]
    public bool isUpgrade;

    [Tooltip("Multiple by which food saturation increases")]
    public float saturationModifier;

    [Tooltip("Additive by which satisfaction increases")]
    public float satisfactionModifier;
}
