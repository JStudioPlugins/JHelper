using Rocket.Core.Utils;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHelper.JPlayers
{
    public static class ItemPlayerExtensions
    {
        public static int GetItemCount(this Player player)
        {
            int itemCt = 0;
            for (int i = 0; i < PlayerInventory.STORAGE; ++i)
                itemCt += player.inventory.getItemCount((byte)i);
            return itemCt;
        }

        public static uint GetCurrencyValue(this Player player, Guid guid)
        {
            Asset asset = (ItemCurrencyAsset)Assets.find(guid);
            if (asset is ItemCurrencyAsset currency)
            {
                return currency.getInventoryValue(player);
            }
            else
            {
                JHelper.AdvancedLog("Attempted to get a currency value using an invalid ItemCurrencyAsset GUID!");
                return 0;
            }
        }

        public static bool CanSpendCurrency(this Player player, Guid guid, uint value)
        {
            Asset asset = (ItemCurrencyAsset)Assets.find(guid);
            if (asset is ItemCurrencyAsset currency)
            {
                return currency.canAfford(player, value);
            }
            else
            {
                JHelper.AdvancedLog("Attempted to check if afford using an invalid ItemCurrencyAsset GUID!");
                return false;
            }
        }

        public static bool SpendCurrency(this Player player, Guid guid, uint value)
        {
            Asset asset = (ItemCurrencyAsset)Assets.find(guid);
            if (asset is ItemCurrencyAsset currency)
            {
                return currency.spendValue(player, value);
            }
            else
            {
                JHelper.AdvancedLog("Attempted to spend value using an invalid ItemCurrencyAsset GUID!");
                return false;
            }
        }

        public static void GrantCurrency(this Player player, Guid guid, uint value)
        {
            Asset asset = (ItemCurrencyAsset)Assets.find(guid);
            if (asset is ItemCurrencyAsset currency)
            {
                currency.grantValue(player, value);
                return;
            }
            else
            {
                JHelper.AdvancedLog("Attempted to spend value using an invalid ItemCurrencyAsset GUID!");
                return;
            }
        }

        public static void ClearInventory(this Player player)
        {
            TaskDispatcher.QueueOnMainThread(() =>
            {
                byte[] EMPTY_BYTE_ARRAY = new byte[0];

                PlayerInventory inv = player.inventory;

                inv.player.equipment.sendSlot(0);
                inv.player.equipment.sendSlot(1);


                for (byte page = 0; page < PlayerInventory.PAGES; page++)
                {
                    if (page == PlayerInventory.AREA)
                        continue;

                    var count = inv.getItemCount(page);

                    for (byte index = 0; index < count; index++)
                    {
                        inv.removeItem(page, 0);
                    }
                }

                System.Action removeUnequipped = () =>
                {
                    for (byte i = 0; i < inv.getItemCount(2); i++)
                    {
                        inv.removeItem(2, 0);
                    }
                };

                inv.player.clothing.askWearBackpack(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearGlasses(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearHat(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearPants(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearMask(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearShirt(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();

                inv.player.clothing.askWearVest(0, 0, EMPTY_BYTE_ARRAY, true);
                removeUnequipped();
            });
        }
    }
}
