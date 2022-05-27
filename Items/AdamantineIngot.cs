using System.Collections.Generic;

namespace HolyAvengerItemPack
{
    using InstanceIDs;
    using SideLoader;

    public class AdamantineIngot
    {
        public const string SubfolderName = "AdamantineIngot";
        public const string ItemName = "Adamantine Ingot";

        public static Item MakeItem()
        {
            var myitem = new SL_Item()
            {
                Name = ItemName,
                Target_ItemID = IDs.elattsRelicID,
                New_ItemID = IDs.adamantineID,
                EffectBehaviour = EditBehaviours.Override,
                Description = "A sizable ignot made of pure adamantine.",
                SLPackName = HolyAvengerItemPack.sideloaderFolder,
                SubfolderName = SubfolderName,

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
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.adamantineID },
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.tsarStoneID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.purifyingQuartzID},
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.palladiumScrapsID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.palladiumScrapsID }
                },
                UID = newUID,
            }.ApplyRecipe();

            var myitem = new SL_RecipeItem()
            {
                Name = "Crafting: " + ItemName,
                Target_ItemID = IDs.arbitrarySurvivalRecipeID,
                New_ItemID = IDs.adamantineRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.Apply();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
        }
    }
}
