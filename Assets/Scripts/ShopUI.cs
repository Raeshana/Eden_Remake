using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Tooltip("Reference the shop item prefab here")]
    [SerializeField] GameObject shopItemPrefab;

    [Tooltip("Insert scriptable objects for all shop items here")]
    [SerializeField] ShopItem[] shopItems;

    [Tooltip("Reference the shop item container in the scene here")]
    [SerializeField] Transform shopItemContainer;
    private Vector2 itemOffset;

    [Tooltip("Number of columns in the shop")]
    [SerializeField] int cols;
    private int itemNum = 0;

    private bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(ShopItem shopItem in shopItems)
        {
            // Create shop item prefab
            GameObject temp = Instantiate(shopItemPrefab, shopItemContainer);

            // Populate shop item prefab using shop item SO
            ShopItemController shopItemController = temp.GetComponent<ShopItemController>();
            shopItemController.shopItem = shopItem;
            shopItemController.populateShopItem();

            // Calculate item position based on item number and max cols
            itemOffset.y = (itemNum/cols) * 250; // Calculate the y offset using div
            itemOffset.x = (itemNum%cols) * 250; // Calculate the x offset using mod
            temp.transform.position = new Vector3(shopItemContainer.position.x + itemOffset.x, 
                                                shopItemContainer.position.y + itemOffset.y,
                                                shopItemContainer.position.z);
            
            // Increment item number
            itemNum++;
        }

        // Hide shop when game starts
        isPaused = true;
        toggleShop();
    }

    public void toggleShop()
    {
        isPaused = !isPaused;
        shopItemContainer.gameObject.SetActive(isPaused);
        if (isPaused == true) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}
