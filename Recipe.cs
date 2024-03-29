﻿namespace Terraria
{
    using System;

    public class Recipe
    {
        public Item createItem = new Item();
        public static int maxRecipes = 300;
        public static int maxRequirements = 10;
        public bool needWater;
        private static Recipe newRecipe = new Recipe();
        public static int numRecipes = 0;
        public Item[] requiredItem = new Item[maxRequirements];
        public int[] requiredTile = new int[maxRequirements];

        public Recipe()
        {
            for (int i = 0; i < maxRequirements; i++)
            {
                this.requiredItem[i] = new Item();
                this.requiredTile[i] = -1;
            }
        }

        private static void addRecipe()
        {
            Main.recipe[numRecipes] = newRecipe;
            newRecipe = new Recipe();
            numRecipes++;
        }

        public void Create()
        {
            for (int i = 0; i < maxRequirements; i++)
            {
                if (this.requiredItem[i].type == 0)
                {
                    break;
                }
                int stack = this.requiredItem[i].stack;
                for (int j = 0; j < 0x2c; j++)
                {
                    if (Main.player[Main.myPlayer].inventory[j].IsTheSameAs(this.requiredItem[i]))
                    {
                        if (Main.player[Main.myPlayer].inventory[j].stack > stack)
                        {
                            Item item1 = Main.player[Main.myPlayer].inventory[j];
                            item1.stack -= stack;
                            stack = 0;
                        }
                        else
                        {
                            stack -= Main.player[Main.myPlayer].inventory[j].stack;
                            Main.player[Main.myPlayer].inventory[j] = new Item();
                        }
                    }
                    if (stack <= 0)
                    {
                        break;
                    }
                }
            }
            FindRecipes();
        }

        public static void FindRecipes()
        {
            int num = Main.availableRecipe[Main.focusRecipe];
            float num2 = Main.availableRecipeY[Main.focusRecipe];
            for (int i = 0; i < maxRecipes; i++)
            {
                Main.availableRecipe[i] = 0;
            }
            Main.numAvailableRecipes = 0;
            for (int j = 0; j < maxRecipes; j++)
            {
                if (Main.recipe[j].createItem.type == 0)
                {
                    break;
                }
                bool flag = true;
                bool flag2 = false;
                if (((Main.guideItem.type > 0) && (Main.guideItem.stack > 0)) && (Main.guideItem.name != ""))
                {
                    flag2 = true;
                }
                if (flag2)
                {
                    for (int num5 = 0; num5 < maxRequirements; num5++)
                    {
                        if (Main.recipe[j].requiredItem[num5].type == 0)
                        {
                            break;
                        }
                        if (Main.guideItem.IsTheSameAs(Main.recipe[j].requiredItem[num5]))
                        {
                            Main.availableRecipe[Main.numAvailableRecipes] = j;
                            Main.numAvailableRecipes++;
                            break;
                        }
                    }
                    continue;
                }
                for (int n = 0; n < maxRequirements; n++)
                {
                    if (Main.recipe[j].requiredItem[n].type == 0)
                    {
                        break;
                    }
                    int stack = Main.recipe[j].requiredItem[n].stack;
                    for (int num8 = 0; num8 < 0x2c; num8++)
                    {
                        if (Main.player[Main.myPlayer].inventory[num8].IsTheSameAs(Main.recipe[j].requiredItem[n]))
                        {
                            stack -= Main.player[Main.myPlayer].inventory[num8].stack;
                        }
                        if (stack <= 0)
                        {
                            break;
                        }
                    }
                    if (stack > 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    bool flag3 = true;
                    for (int num9 = 0; num9 < maxRequirements; num9++)
                    {
                        if (Main.recipe[j].requiredTile[num9] == -1)
                        {
                            break;
                        }
                        if (!Main.player[Main.myPlayer].adjTile[Main.recipe[j].requiredTile[num9]])
                        {
                            flag3 = false;
                            break;
                        }
                    }
                    if (flag3 && (!Main.recipe[j].needWater || (Main.recipe[j].needWater == Main.player[Main.myPlayer].adjWater)))
                    {
                        Main.availableRecipe[Main.numAvailableRecipes] = j;
                        Main.numAvailableRecipes++;
                    }
                }
            }
            for (int k = 0; k < Main.numAvailableRecipes; k++)
            {
                if (num == Main.availableRecipe[k])
                {
                    Main.focusRecipe = k;
                    break;
                }
            }
            if (Main.focusRecipe >= Main.numAvailableRecipes)
            {
                Main.focusRecipe = Main.numAvailableRecipes - 1;
            }
            if (Main.focusRecipe < 0)
            {
                Main.focusRecipe = 0;
            }
            float num11 = Main.availableRecipeY[Main.focusRecipe] - num2;
            for (int m = 0; m < maxRecipes; m++)
            {
                Main.availableRecipeY[m] -= num11;
            }
        }

        public static void SetupRecipes()
        {
            newRecipe.createItem.SetDefaults("Bottle");
            newRecipe.createItem.stack = 2;
            newRecipe.requiredItem[0].SetDefaults("Glass");
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Bottled Water");
            newRecipe.requiredItem[0].SetDefaults("Bottle");
            newRecipe.needWater = true;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x1c, false);
            newRecipe.createItem.stack = 2;
            newRecipe.requiredItem[0].SetDefaults(5, false);
            newRecipe.requiredItem[1].SetDefaults(0x17, false);
            newRecipe.requiredItem[1].stack = 2;
            newRecipe.requiredItem[2].SetDefaults(0x1f, false);
            newRecipe.requiredItem[2].stack = 2;
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults("Healing Potion");
            newRecipe.requiredItem[0].SetDefaults(0x1c, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredItem[1].SetDefaults(0xb7, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(110, false);
            newRecipe.createItem.stack = 2;
            newRecipe.requiredItem[0].SetDefaults(0x4b, false);
            newRecipe.requiredItem[1].SetDefaults(0x17, false);
            newRecipe.requiredItem[1].stack = 2;
            newRecipe.requiredItem[2].SetDefaults(0x1f, false);
            newRecipe.requiredItem[2].stack = 2;
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults("Mana Potion");
            newRecipe.requiredItem[0].SetDefaults(110, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredItem[1].SetDefaults(0xb7, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe2, false);
            newRecipe.requiredItem[0].SetDefaults(0x1c, false);
            newRecipe.requiredItem[1].SetDefaults(110, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe3, false);
            newRecipe.requiredItem[0].SetDefaults("Healing Potion");
            newRecipe.requiredItem[1].SetDefaults("Mana Potion");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x120, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13e, false);
            newRecipe.requiredItem[2].SetDefaults(0x13d, false);
            newRecipe.requiredItem[3].SetDefaults("Obsidian");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x121, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults("Mushroom");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(290, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13b, false);
            newRecipe.requiredItem[2].SetDefaults("Cactus");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x123, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13d, false);
            newRecipe.requiredItem[2].SetDefaults("Coral");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x124, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults("Iron Ore");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x125, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13a, false);
            newRecipe.requiredItem[2].SetDefaults(0x139, false);
            newRecipe.requiredItem[3].SetDefaults("Fallen Star");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x126, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13a, false);
            newRecipe.requiredItem[2].SetDefaults(0x13c, false);
            newRecipe.requiredItem[3].SetDefaults("Fallen Star");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x127, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults(0x13b, false);
            newRecipe.requiredItem[3].SetDefaults("Feather");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x128, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13b, false);
            newRecipe.requiredItem[2].SetDefaults(0x13a, false);
            newRecipe.requiredItem[3].SetDefaults("Gold Ore");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x129, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13b, false);
            newRecipe.requiredItem[2].SetDefaults(0x13a, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x12a, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults("Glowing Mushroom");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x12b, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults(0x13b, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(300, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13c, false);
            newRecipe.requiredItem[2].SetDefaults("Rotten Chunk");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x12d, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13c, false);
            newRecipe.requiredItem[2].SetDefaults("Cactus");
            newRecipe.requiredItem[3].SetDefaults("Worm Teeth");
            newRecipe.requiredItem[4].SetDefaults("Stinger");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x12e, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13d, false);
            newRecipe.requiredItem[2].SetDefaults(0x13f, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x12f, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults("Lens");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x130, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x139, false);
            newRecipe.requiredItem[2].SetDefaults(0x13b, false);
            newRecipe.requiredItem[3].SetDefaults(0x13f, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x131, false);
            newRecipe.requiredItem[0].SetDefaults(0x7e, false);
            newRecipe.requiredItem[1].SetDefaults(0x13e, false);
            newRecipe.requiredItem[2].SetDefaults(0x13c, false);
            newRecipe.requiredItem[3].SetDefaults(0x13b, false);
            newRecipe.requiredItem[4].SetDefaults("Feather");
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xff, false);
            newRecipe.requiredItem[0].SetDefaults(0x1f, false);
            newRecipe.requiredItem[1].SetDefaults(0xc3, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x43, false);
            newRecipe.createItem.stack = 5;
            newRecipe.requiredItem[0].SetDefaults(60, false);
            newRecipe.requiredTile[0] = 13;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x11f, false);
            newRecipe.createItem.stack = 20;
            newRecipe.requiredItem[0].SetDefaults(0x117, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0x43, false);
            addRecipe();
            newRecipe.createItem.SetDefaults(8, false);
            newRecipe.createItem.stack = 3;
            newRecipe.requiredItem[0].SetDefaults(0x17, false);
            newRecipe.requiredItem[0].stack = 1;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            addRecipe();
            newRecipe.createItem.SetDefaults(0xeb, false);
            newRecipe.requiredItem[0].SetDefaults(0xa6, false);
            newRecipe.requiredItem[1].SetDefaults(0x17, false);
            newRecipe.requiredItem[1].stack = 5;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x11e, false);
            newRecipe.requiredItem[0].SetDefaults(0x11a, false);
            newRecipe.requiredItem[1].SetDefaults(0x17, false);
            addRecipe();
            newRecipe.createItem.SetDefaults("Glass");
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults(0xa9, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Clay Pot");
            newRecipe.requiredItem[0].SetDefaults(0x85, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gray Brick");
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gray Brick Wall");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults("Gray Brick");
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Red Brick");
            newRecipe.requiredItem[0].SetDefaults(0x85, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Red Brick Wall");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults("Red Brick");
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Brick");
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredItem[0].stack = 1;
            newRecipe.requiredItem[1].SetDefaults("Copper Ore");
            newRecipe.requiredItem[1].stack = 1;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Brick Wall");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults("Copper Brick");
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Brick Wall");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults("Silver Brick");
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Brick");
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredItem[0].stack = 1;
            newRecipe.requiredItem[1].SetDefaults("Silver Ore");
            newRecipe.requiredItem[1].stack = 1;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Brick Wall");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults("Gold Brick");
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Brick");
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredItem[0].stack = 1;
            newRecipe.requiredItem[1].SetDefaults("Gold Ore");
            newRecipe.requiredItem[1].stack = 1;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Hellstone Brick");
            newRecipe.requiredItem[0].SetDefaults(0xae, false);
            newRecipe.requiredItem[1].SetDefaults(3, false);
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc0, false);
            newRecipe.requiredItem[0].SetDefaults(0xad, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults(30, false);
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults(2, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x1a, false);
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x5d, false);
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x5e, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            addRecipe();
            newRecipe.createItem.SetDefaults(0x19, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x22, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Sign");
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x30, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredItem[1].SetDefaults(0x16, false);
            newRecipe.requiredItem[1].stack = 2;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x20, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x24, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x18, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc4, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(40, false);
            newRecipe.createItem.stack = 3;
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[1].SetDefaults(3, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x27, false);
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Bed");
            newRecipe.requiredItem[0].SetDefaults(9, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredItem[1].SetDefaults("Silk");
            newRecipe.requiredItem[1].stack = 5;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silk");
            newRecipe.requiredItem[0].SetDefaults(150, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xf7, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xff, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xf8, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xff, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xf9, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xff, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(240, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xfe, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xf1, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xfe, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x106, false);
            newRecipe.requiredItem[0].SetDefaults("Silk");
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredItem[1].SetDefaults(0xb2, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x103, false);
            newRecipe.requiredItem[0].SetDefaults(0x44, false);
            newRecipe.requiredItem[0].stack = 5;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xfc, false);
            newRecipe.requiredItem[0].SetDefaults(0x103, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xfd, false);
            newRecipe.requiredItem[0].SetDefaults(0x103, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(250, false);
            newRecipe.requiredItem[0].SetDefaults(0x105, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredItem[1].SetDefaults(0x1f, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults("Flaming Arrow");
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults(40, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredItem[1].SetDefaults(8, false);
            addRecipe();
            newRecipe.createItem.SetDefaults(0x21, false);
            newRecipe.requiredItem[0].SetDefaults(3, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredItem[2].SetDefaults(8, false);
            newRecipe.requiredItem[2].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(20, false);
            newRecipe.requiredItem[0].SetDefaults(12, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Axe");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Hammer");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Broadsword");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Shortsword");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Bow");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Helmet");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Chainmail");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Greaves");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Watch");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 14;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Chandelier");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredItem[1].SetDefaults(8, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredItem[2].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x16, false);
            newRecipe.requiredItem[0].SetDefaults(11, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x23, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 5;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xcd, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(1, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(10, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(7, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(4, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(6, false);
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Iron Bow");
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Iron Helmet");
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Iron Chainmail");
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Iron Greaves");
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Iron Chain");
            newRecipe.requiredItem[0].SetDefaults(0x16, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x15, false);
            newRecipe.requiredItem[0].SetDefaults(14, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Axe");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Hammer");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Broadsword");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Shortsword");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Bow");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Helmet");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Chainmail");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Greaves");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Watch");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 14;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Chandelier");
            newRecipe.requiredItem[0].SetDefaults(0x15, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredItem[1].SetDefaults(8, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredItem[2].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x13, false);
            newRecipe.requiredItem[0].SetDefaults(13, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Axe");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Hammer");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9, false);
            newRecipe.requiredItem[1].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Broadsword");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Shortsword");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Bow");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 7;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Helmet");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Chainmail");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Greaves");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Watch");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 14;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Crown");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredItem[1].SetDefaults("Ruby");
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Chandelier");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredItem[1].SetDefaults(8, false);
            newRecipe.requiredItem[1].stack = 4;
            newRecipe.requiredItem[2].SetDefaults(0x55, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Candle");
            newRecipe.requiredItem[0].SetDefaults(0x13, false);
            newRecipe.requiredItem[1].SetDefaults(8, false);
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x39, false);
            newRecipe.requiredItem[0].SetDefaults(0x38, false);
            newRecipe.requiredItem[0].stack = 4;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x2c, false);
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 8;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Unholy Arrow");
            newRecipe.createItem.stack = 3;
            newRecipe.requiredItem[0].SetDefaults(40, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredItem[1].SetDefaults(0x45, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x2d, false);
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x2e, false);
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Shadow Helmet");
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredItem[1].SetDefaults(0x56, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Shadow Scalemail");
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredItem[1].SetDefaults(0x56, false);
            newRecipe.requiredItem[1].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Shadow Greaves");
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0x56, false);
            newRecipe.requiredItem[1].stack = 15;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Nightmare Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(0x56, false);
            newRecipe.requiredItem[1].stack = 6;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("The Breaker");
            newRecipe.requiredItem[0].SetDefaults(0x39, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(0x56, false);
            newRecipe.requiredItem[1].stack = 5;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Grappling Hook");
            newRecipe.requiredItem[0].SetDefaults(0x55, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredItem[1].SetDefaults(0x76, false);
            newRecipe.requiredItem[1].stack = 1;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x75, false);
            newRecipe.requiredItem[0].SetDefaults(0x74, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc6, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xb1, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc7, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xb2, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(200, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xb3, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc9, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xb5, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xca, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(0xb6, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xcb, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults(180, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xcc, false);
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x7f, false);
            newRecipe.requiredItem[0].SetDefaults(0x5f, false);
            newRecipe.requiredItem[1].SetDefaults(0x75, false);
            newRecipe.requiredItem[1].stack = 30;
            newRecipe.requiredItem[2].SetDefaults(0x4b, false);
            newRecipe.requiredItem[2].stack = 10;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc5, false);
            newRecipe.requiredItem[0].SetDefaults(0x62, false);
            newRecipe.requiredItem[1].SetDefaults(0x75, false);
            newRecipe.requiredItem[1].stack = 20;
            newRecipe.requiredItem[2].SetDefaults(0x4b, false);
            newRecipe.requiredItem[2].stack = 5;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Meteor Helmet");
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Meteor Suit");
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Meteor Leggings");
            newRecipe.requiredItem[0].SetDefaults(0x75, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults("Meteor Shot");
            newRecipe.createItem.stack = 0x19;
            newRecipe.requiredItem[0].SetDefaults("Musket Ball");
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredItem[1].SetDefaults(0x75, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x97, false);
            newRecipe.requiredItem[0].SetDefaults(0x9a, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredItem[1].SetDefaults(150, false);
            newRecipe.requiredItem[1].stack = 40;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x98, false);
            newRecipe.requiredItem[0].SetDefaults(0x9a, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredItem[1].SetDefaults(150, false);
            newRecipe.requiredItem[1].stack = 50;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x99, false);
            newRecipe.requiredItem[0].SetDefaults(0x9a, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredItem[1].SetDefaults(150, false);
            newRecipe.requiredItem[1].stack = 0x2d;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();
            newRecipe.createItem.SetDefaults(190, false);
            newRecipe.requiredItem[0].SetDefaults("Silver Broadsword");
            newRecipe.requiredItem[1].SetDefaults(0xd0, false);
            newRecipe.requiredItem[1].stack = 40;
            newRecipe.requiredItem[2].SetDefaults(0xd1, false);
            newRecipe.requiredItem[2].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xbf, false);
            newRecipe.requiredItem[0].SetDefaults(0xd0, false);
            newRecipe.requiredItem[0].stack = 40;
            newRecipe.requiredItem[1].SetDefaults(0xd1, false);
            newRecipe.requiredItem[1].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xb9, false);
            newRecipe.requiredItem[0].SetDefaults(0x54, false);
            newRecipe.requiredItem[1].SetDefaults(0xd0, false);
            newRecipe.requiredItem[1].stack = 30;
            newRecipe.requiredItem[2].SetDefaults(210, false);
            newRecipe.requiredItem[2].stack = 3;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe4, false);
            newRecipe.requiredItem[0].SetDefaults(0x5b, false);
            newRecipe.requiredItem[1].SetDefaults(0xb3, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredItem[2].SetDefaults(0xb1, false);
            newRecipe.requiredItem[2].stack = 10;
            newRecipe.requiredItem[3].SetDefaults(0xd0, false);
            newRecipe.requiredItem[3].stack = 20;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe5, false);
            newRecipe.requiredItem[0].SetDefaults(0x52, false);
            newRecipe.requiredItem[1].SetDefaults(0xb2, false);
            newRecipe.requiredItem[1].stack = 6;
            newRecipe.requiredItem[2].SetDefaults(0xb6, false);
            newRecipe.requiredItem[2].stack = 6;
            newRecipe.requiredItem[3].SetDefaults(0xd0, false);
            newRecipe.requiredItem[3].stack = 40;
            newRecipe.requiredItem[4].SetDefaults(0xd1, false);
            newRecipe.requiredItem[4].stack = 12;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(230, false);
            newRecipe.requiredItem[0].SetDefaults(0x4e, false);
            newRecipe.requiredItem[1].SetDefaults(0xb5, false);
            newRecipe.requiredItem[1].stack = 8;
            newRecipe.requiredItem[2].SetDefaults(180, false);
            newRecipe.requiredItem[2].stack = 8;
            newRecipe.requiredItem[3].SetDefaults(0xd0, false);
            newRecipe.requiredItem[3].stack = 30;
            newRecipe.requiredItem[4].SetDefaults(210, false);
            newRecipe.requiredItem[4].stack = 4;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].SetDefaults(0xae, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredItem[1].SetDefaults(0xad, false);
            newRecipe.requiredItem[1].stack = 2;
            newRecipe.requiredTile[0] = 0x4d;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x77, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 15;
            newRecipe.requiredItem[1].SetDefaults(0x37, false);
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(120, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 0x19;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x79, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x7a, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xd9, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xdb, false);
            newRecipe.requiredItem[0].SetDefaults(0xaf, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredItem[1].SetDefaults("Handgun");
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe7, false);
            newRecipe.requiredItem[0].SetDefaults(0x5c, false);
            newRecipe.requiredItem[1].SetDefaults(0xaf, false);
            newRecipe.requiredItem[1].stack = 30;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe8, false);
            newRecipe.requiredItem[0].SetDefaults(0x53, false);
            newRecipe.requiredItem[1].SetDefaults(0xaf, false);
            newRecipe.requiredItem[1].stack = 40;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xe9, false);
            newRecipe.requiredItem[0].SetDefaults(0x4f, false);
            newRecipe.requiredItem[1].SetDefaults(0xaf, false);
            newRecipe.requiredItem[1].stack = 0x23;
            newRecipe.requiredTile[0] = 0x10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x111, false);
            newRecipe.requiredItem[0].SetDefaults(0x2e, false);
            newRecipe.requiredItem[1].SetDefaults(0x9b, false);
            newRecipe.requiredItem[2].SetDefaults(190, false);
            newRecipe.requiredItem[3].SetDefaults(0x79, false);
            newRecipe.requiredTile[0] = 0x1a;
            addRecipe();
            newRecipe.createItem.SetDefaults("Depth Meter");
            newRecipe.requiredItem[0].SetDefaults(20, false);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(0x15, false);
            newRecipe.requiredItem[1].stack = 8;
            newRecipe.requiredItem[2].SetDefaults(0x13, false);
            newRecipe.requiredItem[2].stack = 6;
            newRecipe.requiredTile[0] = 14;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults(0xc1, false);
            newRecipe.requiredItem[0].SetDefaults(0xad, false);
            newRecipe.requiredItem[0].stack = 20;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Goggles");
            newRecipe.requiredItem[0].SetDefaults(0x26, false);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x12;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x10a, false);
            newRecipe.requiredItem[0].SetDefaults(0x144, false);
            newRecipe.requiredItem[1].SetDefaults(0x143, false);
            newRecipe.requiredItem[1].stack = 10;
            newRecipe.requiredItem[2].SetDefaults(180, false);
            newRecipe.requiredItem[2].stack = 5;
            newRecipe.requiredTile[0] = 0x11;
            addRecipe();
            newRecipe.createItem.SetDefaults("Sunglasses");
            newRecipe.requiredItem[0].SetDefaults("Black Lens");
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredTile[0] = 0x12;
            newRecipe.requiredTile[1] = 15;
            addRecipe();
            newRecipe.createItem.SetDefaults("Mana Crystal");
            newRecipe.requiredItem[0].SetDefaults(0x4b, false);
            newRecipe.requiredItem[0].stack = 10;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x2b, false);
            newRecipe.requiredItem[0].SetDefaults(0x26, false);
            newRecipe.requiredItem[0].stack = 6;
            newRecipe.requiredTile[0] = 0x1a;
            addRecipe();
            newRecipe.createItem.SetDefaults(70, false);
            newRecipe.requiredItem[0].SetDefaults(0x43, false);
            newRecipe.requiredItem[0].stack = 30;
            newRecipe.requiredItem[1].SetDefaults(0x44, false);
            newRecipe.requiredItem[1].stack = 15;
            newRecipe.requiredTile[0] = 0x1a;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x47, false);
            newRecipe.createItem.stack = 100;
            newRecipe.requiredItem[0].SetDefaults(0x48, false);
            newRecipe.requiredItem[0].stack = 1;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x48, false);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults(0x47, false);
            newRecipe.requiredItem[0].stack = 100;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x48, false);
            newRecipe.createItem.stack = 100;
            newRecipe.requiredItem[0].SetDefaults(0x49, false);
            newRecipe.requiredItem[0].stack = 1;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x49, false);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults(0x48, false);
            newRecipe.requiredItem[0].stack = 100;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x49, false);
            newRecipe.createItem.stack = 100;
            newRecipe.requiredItem[0].SetDefaults(0x4a, false);
            newRecipe.requiredItem[0].stack = 1;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x4a, false);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults(0x49, false);
            newRecipe.requiredItem[0].stack = 100;
            addRecipe();

            //Mod: portal recipe
            newRecipe.createItem.SetDefaults(4095);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults("Glass");
            newRecipe.requiredItem[1].SetDefaults("Fallen Star");
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();

            //Dynamite recipe
            newRecipe.createItem.SetDefaults(167, false);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults(166, false);
            newRecipe.requiredItem[0].stack = 3;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();

            //Enchanted Dynamite
            newRecipe.createItem.SetDefaults(4094);
            newRecipe.createItem.stack = 1;
            newRecipe.requiredItem[0].SetDefaults("Dynamite");
            newRecipe.requiredItem[1].SetDefaults("Fallen Star");
            newRecipe.requiredItem[1].stack = 2;
            newRecipe.requiredTile[0] = 0x12;
            addRecipe();

            for (int i = 0; i < numRecipes; i++)
            {
                for (int j = 0; Main.recipe[i].requiredItem[j].type > 0; j++)
                {
                    Main.recipe[i].requiredItem[j].checkMat();
                }
                Main.recipe[i].createItem.checkMat();
            }
        }
    }
}

