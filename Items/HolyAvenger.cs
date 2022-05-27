using System.Collections.Generic;

namespace HolyAvengerItemPack
{
    using SideLoader;
    using InstanceIDs;
    using TinyHelper;
    using System.Linq;
    using UnityEngine;

    public class HolyAvenger
    {
        public const string SubfolderName = "HolyAvenger";
        public const string ItemName = "Crusader's Bastard Sword";

        //private static Item[] testItem;
        
        /// <summary>
        /// Stamina Cost, Knockback, Attack Speed, Damage, Stamina Cost Multiplier, Knockback Multiplier
        /// </summary>
        static float[] weaponData = new[] { 4.8f, 25f, 0.9f, 48f };

        //public static Item MakeItemNew()
        //{

        //    new CustomWeapon(out testItem)
        //    {
        //        //------------ CUSTOM ITEM PARAMS ------------//

        //        //General
        //        Name = ItemName,
        //        Target_ItemID = IDs.ironSwordID,
        //        New_ItemID = IDs.holyAvengerID,
        //        Description = "Augments lightning damage infusions that are applied to it and inflicts Doomed on enemies.",

        //        //Tags
        //        TagBehaviour = TinyHelper.Customs.Behaviour.Add,
        //        Tags = new List<string>
        //        {
        //            IDs.BastardTag,
        //            IDs.HolyTag,
        //        },

        //        //Graphics related
        //        PathPrefix = @"HolyAvengerItemPack\SideLoaderSafe\",
        //        IconPath = @"Items\HolyAvenger\Textures\icon.png",
        //        AssetBundlePath = @"AssetBundles\holyavenger",
        //        ItemVisuals_PrefabName = "holyavenger_Prefab",

        //        //Common Stats
        //        BaseValue = 2000,
        //        RawWeight = 4.5f,
        //        MaxDurability = -1,

        //        //------------ CUSTOM WEAPON PARAMS ----------//
        //        SwingSound = SwingSoundWeapon.Weapon_2H,
        //        //AttackSpeed = weaponData[2],





        //        //StatsHolder = new SL_WeaponStats()
        //        //{
        //        //    BaseValue = 2000,
        //        //    RawWeight = 4.5f,
        //        //    MaxDurability = -1,
        //        //    AttackSpeed = weaponData[2],
        //        //    BaseDamage = new List<SL_Damage>() { new SL_Damage() { Damage = weaponData[3], Type = DamageType.Types.Physical } },
        //        //    Impact = weaponData[1],
        //        //    Attacks = new[] {
        //        //            HolyAvengerItemPack.MakeAttackData(weaponData[0], weaponData[1], weaponData[2],         weaponData[3], 1,       1),
        //        //            HolyAvengerItemPack.MakeAttackData(weaponData[0], weaponData[1], weaponData[2],         weaponData[3], 1,       1),
        //        //            HolyAvengerItemPack.MakeAttackData(weaponData[0], weaponData[1], weaponData[2] + 0.1f,  weaponData[3], 1.2f,    1.3f),
        //        //            HolyAvengerItemPack.MakeAttackData(weaponData[0], weaponData[1], weaponData[2] + 0.1f,  weaponData[3], 1.1f,    1.1f),
        //        //            HolyAvengerItemPack.MakeAttackData(weaponData[0], weaponData[1], weaponData[2] + 0.1f,  weaponData[3], 1.1f,    1.1f)
        //        //        }
        //        //},
        //        //EffectTransforms = new List<SL_EffectTransform>() {
        //        //    new SL_EffectTransform() {
        //        //        TransformName = "HitEffects",
        //        //        Effects = new List<SL_Effect>() {
        //        //            new SL_AddStatusEffectBuildUp() { StatusEffect = "Doom", Buildup = 60f },
        //        //            //new SL_AddStatusEffect() {StatusEffect = "Radiating", ChanceToContract = 100},
        //        //            //new SL_AddStatusEffectBuildUp() { StatusEffect = "Burning", Buildup = 60f },
        //        //            //new SL_AddStatusEffectBuildUp() {StatusEffect = "Radiating", Buildup = 100f},
        //        //        }
        //        //    }
        //        //},
        //    };

        //    CustomItem.ApplyCustomItems();

        //    return null;

        //}

        public static Item MakeItem()
        {
            float[] ha = new[] { 4.8f, 30f, 0.9f, 48f };
            var myitem = new SL_Weapon()
            {
                Name = ItemName,
                Target_ItemID = IDs.ironSwordID,
                New_ItemID = IDs.holyAvengerID,
                Description = "Has a small chance to inflict and even spread Impending Doom among your foes.",

                Tags = HolyAvengerItemPack.GetSafeTags(new string[]
                {
                    "SpecialLeap",
                    IDs.BastardTag,
                    IDs.HolyTag,
                    "Blade",
                    "Weapon",
                }),
                StatsHolder = new SL_WeaponStats()
                {
                    BaseValue = 2000,
                    RawWeight = 4.5f,
                    MaxDurability = 650,
                    AttackSpeed = ha[2],
                    BaseDamage = new List<SL_Damage>() { new SL_Damage() { Damage = ha[3], Type = DamageType.Types.Physical } },
                    Impact = ha[1],
                    AutoGenerateAttackData = true,
                    //Attacks = new[] {
                    //        HolyAvengerItemPack.MakeAttackData(ha[0], ha[1], ha[2],         ha[3], 1,       1),
                    //        HolyAvengerItemPack.MakeAttackData(ha[0], ha[1], ha[2],         ha[3], 1,       1),
                    //        HolyAvengerItemPack.MakeAttackData(ha[0], ha[1], ha[2] + 0.1f,  ha[3], 1.2f,    1.3f),
                    //        HolyAvengerItemPack.MakeAttackData(ha[0], ha[1], ha[2] + 0.1f,  ha[3], 1.1f,    1.1f),
                    //        HolyAvengerItemPack.MakeAttackData(ha[0], ha[1], ha[2] + 0.1f,  ha[3], 1.1f,    1.1f)
                    //    }
                },
                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "HitEffects",
                        Effects = new SL_Effect[] {
                            //new SL_AddStatusEffectBuildUp() { StatusEffect = "Doom", Buildup = 60f },
                            //new SL_AddStatusEffect() {StatusEffect = "Radiating", ChanceToContract = 100},
                            //new SL_AddStatusEffectBuildUp() { StatusEffect = "Burning", Buildup = 60f },
                            //new SL_AddStatusEffectBuildUp() {StatusEffect = "Radiating", Buildup = 100f},
                        }
                    }
                },
                SwingSound = SwingSoundWeapon.Weapon_2H,
                
                SLPackName = HolyAvengerItemPack.sideloaderFolder,
                SubfolderName = SubfolderName,

                ItemVisuals = new SL_ItemVisual() { Prefab_Name = "holyavenger_Prefab", Prefab_AssetBundle = "holyavenger", Prefab_SLPack = HolyAvengerItemPack.sideloaderFolder, PositionOffset = new UnityEngine.Vector3(-0.03f, 0,0)},
            };

            myitem.Apply();
            Item item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            var bonusDamage = TinyEffectManager.GetOrMake(item.transform, "Effects", true, true).gameObject.AddComponent<AffectStat>();

            var tagSelectorList = new TagSourceSelector[] {new TagSourceSelector(TagSourceManager.Instance.GetTag("359"))};
            bonusDamage.AffectedStat = new TagSourceSelector(TagSourceManager.Instance.GetTag("96"));

            bonusDamage.Tags = tagSelectorList;
            bonusDamage.IsModifier = true;
            bonusDamage.Value = 25;

            var addAndSpread = TinyEffectManager.GetOrMake(item.transform, "HitEffects", true, true).gameObject.AddComponent<AddThenSpreadStatus>();
            addAndSpread.Status = Templar.Templar.Instance.impendingDoomInstance;
            addAndSpread.Range = 3;
            addAndSpread.BaseChancesToContract = 20;
            return item;
        }
        public static Item MakeRecipes()
        {
            string newUID = HolyAvengerItemPack.GUID + "." + SubfolderName.ToLower() + "recipe";
            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Survival,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.holyAvengerID },
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.ancientRelicID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.goldIngotID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.adamantineID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.adamantineID}
                },
                UID = newUID,
            }.ApplyRecipe();

            var myitem = new SL_RecipeItem()
            {
                Name = "Crafting: " + ItemName,
                Target_ItemID = IDs.arbitrarySurvivalRecipeID,
                New_ItemID = IDs.holyAvengerRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.Apply();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            //var prefab = SL.GetSLPack("Templar").AssetBundles["divinesmite"].LoadAsset<GameObject>("divineinfusion_Prefab").transform;
            //UnityEngine.Object.DontDestroyOnLoad(prefab.gameObject);
            //prefab.parent = item.gameObject.transform;

            return item;
        }
    }
}
