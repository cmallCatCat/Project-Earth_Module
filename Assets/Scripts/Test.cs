using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Command;
using Core.Inventory_And_Item.Data;
using Core.Save_And_Load.Command;
using QFramework;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public int inventoryKey;
    public ItemIdentification itemIdentification;
    public InventoryHolderExample inventoryHolderExample;

    private void OnGUI()
    {
        if (GUILayout.Button("创建库存持有者"))
        {
            inventoryHolderExample = new GameObject("InventoryHolder").AddComponent<InventoryHolderExample>();
        }

        if (GUILayout.Button("创建并给予库存"))
        {
            if (inventoryHolderExample != null)
                inventoryHolderExample.RegisterInventory();
        }

        if (GUILayout.Button("加载"))
        {
            this.SendCommand<LoadCommand>();
        }

        if (GUILayout.Button("保存"))
        {
            this.SendCommand<SaveCommand>();
        }

        if (GUILayout.Button("给予物品"))
        {
            ItemStack itemStack = new ItemStack(itemIdentification, 1);
            this.SendCommand(new GetItemCommand(inventoryKey, itemStack));
        }
    }

    public IArchitecture GetArchitecture()
    {
        return InGame.Interface;
    }
}