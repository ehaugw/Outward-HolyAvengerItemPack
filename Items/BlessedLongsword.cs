using System.Collections.Generic;

namespace HolyAvengerItemPack
{
    using SideLoader;
    using InstanceIDs;

    public class BlessedLongsword
    {
        public const string SubfolderName = "BlessedLongsword";
        public const string ItemName = "Blessed Longsword";

        public static Item MakeItem()
        {
            float[] ha = new[] { 4.0f, 25f, 1.1f, 35f };
            var myitem = new SL_Weapon()
            {
                Name = ItemName,
                Target_ItemID = IDs.ironSwordID,
                New_ItemID = IDs.blessedLongswordID,
                Description = "",

                Tags = HolyAvengerItemPack.GetSafeTags(new string[]
                {
                    IDs.FinesseTag,
                    IDs.HolyTag,
                    "Blade",
                    "Weapon",
                    "Item"
                }),
                StatsHolder = new SL_WeaponStats()
                {
                    BaseValue = 1000,
                    RawWeight = 3.5f,
                    MaxDurability = 500,
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
                ItemVisuals = new SL_ItemVisual() {Prefab_Name = "keenlongsword_Prefab", Prefab_AssetBundle = "keenlongsword", Prefab_SLPack = HolyAvengerItemPack.sideloaderFolder },
                SLPackName = HolyAvengerItemPack.sideloaderFolder,
                SubfolderName = SubfolderName,
                SwingSound = SwingSoundWeapon.Weapon_2H,
            };
            myitem.Apply();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
        }

        public static Item MakeRecipes()
        {
            string newUID = HolyAvengerItemPack.GUID + "." + SubfolderName.ToLower() + "recipe";

            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Survival,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.blessedLongswordID },
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.tsarSwordID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.ancientRelicID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.blacksmithsVintageHammerID },
               },
                UID = newUID,
            }.ApplyRecipe();

            var myitem = new SL_RecipeItem()
            {
                Name = "Crafting: " + ItemName,
                Target_ItemID = IDs.arbitrarySurvivalRecipeID,
                New_ItemID = IDs.blessedLongswordRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.Apply();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
        }
    }
}
