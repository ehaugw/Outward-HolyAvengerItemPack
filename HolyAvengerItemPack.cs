namespace HolyAvengerItemPack
{
    using Templar;
    using System.Collections.Generic;
    using SideLoader;
    using HarmonyLib;
    using BepInEx;
    using System;
    using InstanceIDs;
    using UnityEngine;
    using System.Linq;
    using UnityEngine.Rendering;
    using CustomWeaponBehaviour;
    using UnityEngine.SceneManagement;
    using NodeCanvas.Tasks.Actions;
    using NodeCanvas.Framework;
    using NodeCanvas.DialogueTrees;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(Templar.GUID, Templar.VERSION)]
    [BepInDependency(TinyHelper.TinyHelper.GUID, TinyHelper.TinyHelper.VERSION)]
    [BepInDependency(CustomWeaponBehaviour.GUID, CustomWeaponBehaviour.VERSION)]

    public class HolyAvengerItemPack : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.holyavengeritempack";
        public const string VERSION = "3.0.0";
        public const string NAME = "Holy Avenger Item Pack";

        public const string sideloaderFolder = "HolyAvengerItemPack";
        public Item divineLongswordInstance;
        public Item blessedLongswordInstance;
        public Item holyAvengerInstance;
        public Item lightMendersRelicInstance;
        public Item adamantineIngotInstance;

        public Item divineLongswordRecipeInstance;
        public Item blessedLongswordRecipeInstance;
        public Item holyAvengerRecipeInstance;
        public Item adamantineIngotRecipeInstance;

        public GameObject gameObj;

        public static HolyAvengerItemPack Instance;
        
        internal void Awake()
        {
            Instance = this;
            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public static String[] GetSafeTags(String[] tags)
        {
            List<string> newTags = new List<string>();

            foreach (string tag in tags)
            {
                if (TagSourceManager.Instance.DbTags.FirstOrDefault(x => x.TagName == tag || x.UID == tag) != null)
                {
                    newTags.Add(tag);
                }
            }
            return newTags.ToArray();
        }

        private void OnSceneLoaded()
        {
            //AncientRelic.AddToLightmenderLoot();

            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>()) {
                // Add holy sword recipes to Vyzyrinthrix
                if (obj.name == "HumanSNPC_Blacksmith" && (obj.GetComponentInChildren<Merchant>()?.ShopName ?? "") == "Vyzyrinthrix the Blacksmith")
                {
                    if (obj.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipes)
                    {
                        if (At.GetField<GuaranteedDrop>(recipes, "m_itemDrops") is List<BasicItemDrop> drops)
                        {
                            foreach (var item in new Item[] { holyAvengerRecipeInstance, divineLongswordRecipeInstance, adamantineIngotRecipeInstance})
                            {
                                //Used to say DroppedItem = item
                                drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
                            }
                        }
                    }
                }
            }
        }

        private void OnPackLoaded()
        {
            blessedLongswordInstance = BlessedLongsword.MakeItem();
            divineLongswordInstance = DivineLongsword.MakeItem();
            holyAvengerInstance = HolyAvenger.MakeItem();
            lightMendersRelicInstance = AncientRelic.MakeItem();
            adamantineIngotInstance = AdamantineIngot.MakeItem();

            divineLongswordRecipeInstance = DivineLongsword.MakeRecipes();
            //blessedLongswordRecipeInstance = BlessedLongsword.MakeRecipes();
            holyAvengerRecipeInstance = HolyAvenger.MakeRecipes();
            //lightMendersRelicRecipeInstance = ArcaneTalisman.MakeRecipes();
            adamantineIngotRecipeInstance = AdamantineIngot.MakeRecipes();
        }

        public static WeaponStats.AttackData MakeAttackData(float stamCost, float knockback, float attackSpeed, float damage, float stamCostMult, float knockbackMult)
        {
            return new WeaponStats.AttackData() { StamCost = stamCost * stamCostMult, Knockback = knockback * knockbackMult, AttackSpeed = attackSpeed, Damage = new List<float>() { damage * stamCostMult } };
        }

        [HarmonyPatch(typeof(GiveReward), "OnExecute")]
        public class GiveReward_OnExecute
        {
            [HarmonyPrefix]
            public static void Prefix(GiveReward __instance)
            {
                //Debug.Log("SHOULD TRY TO GIVE");
                if (__instance.ItemReward.Count(x => new int[] { 3000210, 3000212, 3000211, 2130060 }.Contains(x.Item.value.ItemID)) >= 4)
                {
                    //Debug.Log("Tried");
                    var relicReward = new ItemQuantity();
                    var relicRef = new ItemReference();
                    relicRef.ItemID = HolyAvengerItemPack.Instance.lightMendersRelicInstance.ItemID;
                    relicReward.Item = new BBParameter<ItemReference>(relicRef);
                    relicReward.Quantity = 1;
                    __instance.ItemReward.Add(relicReward);
                }
            }
        }
    }
}