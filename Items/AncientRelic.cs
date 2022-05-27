using System.Collections.Generic;

namespace HolyAvengerItemPack
{
    using InstanceIDs;
    using NodeCanvas.BehaviourTrees;
    using NodeCanvas.Framework;
    using NodeCanvas.Tasks.Actions;
    using SideLoader;
    using System.Linq;
    using UnityEngine;

    public class AncientRelic
    {
        public const string SubfolderName = "AncientRelic";
        public const string ItemName = "Ancient Relic";

        public static Item MakeItem()
        {
            var myitem = new SL_Item()
            {
                Name = ItemName,
                Target_ItemID = IDs.elattsRelicID,
                New_ItemID = IDs.ancientRelicID,
                EffectBehaviour = EditBehaviours.Override,
                Description = "Legends has it that a relic like this was lost to a golden lich long time ago.",
                SLPackName = HolyAvengerItemPack.sideloaderFolder,
                SubfolderName = SubfolderName,

            };
            myitem.Apply();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
        }

        public static void AddToLightmenderLoot()
        {
            if (GameObject.Find("/AISquadManagerStructure/_Statics/LichGold (Passive)_BczAklXU9EebdVVCCn4yHg") is var goldLichPostBattle && goldLichPostBattle)
            {
                if (goldLichPostBattle.transform.Find("DialogueTemplate/NPC/DialogueTree")?.GetComponent<NodeCanvas.DialogueTrees.DialogueTreeController>() is var dialogueTreeController && dialogueTreeController)
                {
                    if (dialogueTreeController?.graph is var graph && graph)
                    {
                        if (graph.allNodes != null)
                        {
                            var nodes = graph.allNodes;

                            foreach (var giveReward in nodes.Where(n => (n is ActionNode an) && (an.action is GiveReward reward)).Select(n => ((GiveReward)((ActionNode)n).action)))
                            {
                                if (giveReward.ItemReward.Count(x => new int[] { 3000210, 3000212, 3000211, 2130060 }.Contains(x.Item.value.ItemID)) == GetExpectedLightmenderDrops())
                                {
                                    var relicReward = new ItemQuantity();
                                    var prayerRef = new ItemReference();
                                    prayerRef.ItemID = HolyAvengerItemPack.Instance.lightMendersRelicInstance.ItemID;
                                    relicReward.Item = new BBParameter<ItemReference>(prayerRef);
                                    relicReward.Quantity = 1;
                                    giveReward.ItemReward.Add(relicReward);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int GetExpectedLightmenderDrops()
        {
            return 4;
        }
    }
}
