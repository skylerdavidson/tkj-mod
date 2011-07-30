namespace Terraria
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public class Player
    {
        public int accDepthMeter;
        public bool accFlipper;
        public int accWatch;
        public bool active;
        public float activeNPCs;
        public bool[] adjTile = new bool[0x56];
        public bool adjWater;
        public bool archer;
        public Item[] armor = new Item[11];
        public int attackCD;
        public Item[] bank = new Item[Chest.maxItems];
        public int body = -1;
        public Rectangle bodyFrame;
        public double bodyFrameCounter;
        public Vector2 bodyPosition;
        public float bodyRotation;
        public Vector2 bodyVelocity;
        public bool boneArmor;
        public int breath = 200;
        public int breathCD;
        public int breathMax = 200;
        public int[] buffTime = new int[10];
        public int[] buffType = new int[10];
        public bool canRocket;
        public int changeItem = -1;
        public bool channel;
        public int chatShowTime;
        public string chatText = "";
        public int chest = -1;
        public int chestX;
        public int chestY;
        public bool controlDown;
        public bool controlInv;
        public bool controlJump;
        public bool controlLeft;
        public bool controlRight;
        public bool controlThrow;
        public bool controlUp;
        public bool controlUseItem;
        public bool controlUseTile;
        public bool dead;
        public static bool deadForGood = false;
        public bool delayUseItem;
        public bool detectCreature;
        public int direction = 1;
        public bool doubleJump;
        public bool enemySpawns;
        public Color eyeColor = new Color(0x69, 90, 0x4b);
        public int fallStart;
        public bool findTreasure;
        public bool fireWalk;
        public bool gills;
        public int grapCount;
        public int[] grappling = new int[20];
        public bool gravControl;
        public float gravDir = 1f;
        public int hair;
        public Color hairColor = new Color(0xd7, 90, 0x37);
        public Rectangle hairFrame;
        public bool hardCore;
        public int head = -1;
        public Rectangle headFrame;
        public double headFrameCounter;
        public Vector2 headPosition;
        public float headRotation;
        public Vector2 headVelocity;
        public int height = 0x2a;
        public int heldProj = -1;
        public int hitTile;
        public int hitTileX;
        public int hitTileY;
        public bool hostile;
        public bool immune;
        public int immuneAlpha;
        public int immuneAlphaDirection;
        public int immuneTime;
        public Item[] inventory = new Item[0x2c];
        public bool invis;
        public int itemAnimation;
        public int itemAnimationMax;
        private static int itemGrabRange = 0x26;
        private static float itemGrabSpeed = 0.45f;
        private static float itemGrabSpeedMax = 4f;
        public int itemHeight;
        public Vector2 itemLocation;
        public float itemRotation;
        public int itemTime;
        public int itemWidth;
        public int jump;
        public bool jumpAgain;
        public bool jumpBoost;
        private static int jumpHeight = 15;
        private static float jumpSpeed = 5.01f;
        public bool killGuide;
        public bool lavaImmune;
        public bool lavaWet;
        public Rectangle legFrame;
        public double legFrameCounter;
        public Vector2 legPosition;
        public float legRotation;
        public int legs = -1;
        public Vector2 legVelocity;
        public int lifeRegen;
        public int lifeRegenCount;
        public float magicBoost = 1f;
        public float manaCost = 1f;
        public int manaRegen;
        public int manaRegenCount;
        public int manaRegenDelay;
        public const int maxBuffs = 10;
        public float meleeDamage = 1f;
        public float meleeSpeed = 1f;
        public bool mouseInterface;
        public string name = "";
        public static int nameLen = 20;
        public bool nightVision;
        public bool noFallDmg;
        public bool noKnockback;
        public bool[] oldAdjTile = new bool[0x56];
        public bool oldAdjWater;
        public Vector2 oldVelocity;
        public Color pantsColor = new Color(0xff, 230, 0xaf);
        public Vector2 position;
        public int potionDelay;
        public bool pvpDeath;
        public bool releaseInventory;
        public bool releaseJump;
        public bool releaseUseItem;
        public bool releaseUseTile;
        public int respawnTimer;
        public bool rocketBoots;
        public int rocketDelay;
        public int rocketDelay2;
        public bool rocketFrame;
        public bool rocketRelease;
        public int rocketTime;
        public int rocketTimeMax = 7;
        public int runSoundDelay;
        public int selectedItem;
        public string setBonus = "";
        public float shadow;
        public int shadowCount;
        public Vector2[] shadowPos = new Vector2[3];
        public Color shirtColor = new Color(0xaf, 0xa5, 140);
        public Color shoeColor = new Color(160, 0x69, 60);
        public bool showItemIcon;
        public int showItemIcon2;
        public int sign = -1;
        public Color skinColor = new Color(0xff, 0x7d, 90);
        public int slowCount;
        public bool slowFall;
        public bool socialShadow;
        public bool spaceGun;
        public bool spawnMax;
        public int SpawnX = -1;
        public int SpawnY = -1;
        public int[] spI = new int[200];
        public string[] spN = new string[200];
        public int[] spX = new int[200];
        public int[] spY = new int[200];
        public int statAttack;
        public int statDefense;
        public int statLife = 100;
        public int statLifeMax = 100;
        public int statMana;
        public int statManaMax;
        public int step = -1;
        public int swimTime;
        public int talkNPC = -1;
        public int team;
        public bool thorns;
        public static int tileRangeX = 5;
        public static int tileRangeY = 4;
        private static int tileTargetX;
        private static int tileTargetY;
        public float townNPCs;
        public Color underShirtColor = new Color(160, 180, 0xd7);
        public Vector2 velocity;
        public bool waterWalk;
        public bool wet;
        public byte wetCount;
        public int whoAmi;
        public int width = 20;
        public bool zoneDungeon;
        public bool zoneEvil;
        public bool zoneJungle;
        public bool zoneMeteor;

        //Mod
        public bool admin = false;
        public int lastPortalMadeX = -1;
        public int lastPortalMadeY = -1;

        public Player()
        {
            for (int i = 0; i < 0x2c; i++)
            {
                if (i < 11)
                {
                    this.armor[i] = new Item();
                    this.armor[i].name = "";
                }
                this.inventory[i] = new Item();
                this.inventory[i].name = "";
            }
            for (int j = 0; j < Chest.maxItems; j++)
            {
                this.bank[j] = new Item();
                this.bank[j].name = "";
            }
            this.grappling[0] = -1;
            this.inventory[0].SetDefaults("Copper Shortsword");
            this.inventory[1].SetDefaults("Copper Pickaxe");
            this.inventory[2].SetDefaults("Copper Axe");
            this.inventory[3].SetDefaults(4095);
            this.inventory[3].stack = 250;
           
            for (int k = 0; k < 0x56; k++)
            {
                this.adjTile[k] = false;
                this.oldAdjTile[k] = false;
            }
        }

        public void AddBuff(int type, int time)
        {
            int index = -1;
            for (int i = 0; i < 10; i++)
            {
                if (this.buffType[i] == type)
                {
                    if (this.buffTime[i] < time)
                    {
                        this.buffTime[i] = time;
                    }
                    return;
                }
            }
            while (index == -1)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.buffType[j] == 0)
                    {
                        index = j;
                        break;
                    }
                }
                if (index == -1)
                {
                    this.DelBuff(0);
                }
            }
            this.buffType[index] = type;
            this.buffTime[index] = time;
        }

        public void AdjTiles()
        {
            int num = 4;
            int num2 = 3;
            for (int i = 0; i < 0x56; i++)
            {
                this.oldAdjTile[i] = this.adjTile[i];
                this.adjTile[i] = false;
            }
            this.oldAdjWater = this.adjWater;
            this.adjWater = false;
            int num4 = (int) ((this.position.X + (this.width / 2)) / 16f);
            int num5 = (int) ((this.position.Y + this.height) / 16f);
            for (int j = num4 - num; j <= (num4 + num); j++)
            {
                for (int k = num5 - num2; k < (num5 + num2); k++)
                {
                    if (Main.tile[j, k].active)
                    {
                        this.adjTile[Main.tile[j, k].type] = true;
                        if (Main.tile[j, k].type == 0x4d)
                        {
                            this.adjTile[0x11] = true;
                        }
                    }
                    if (Main.tile[j, k].liquid > 200)
                    {
                        this.adjWater = true;
                    }
                }
            }
            if (Main.playerInventory)
            {
                bool flag = false;
                for (int m = 0; m < 0x56; m++)
                {
                    if (this.oldAdjTile[m] != this.adjTile[m])
                    {
                        flag = true;
                        break;
                    }
                }
                if (this.adjWater != this.oldAdjWater)
                {
                    flag = true;
                }
                if (flag)
                {
                    Recipe.FindRecipes();
                }
            }
        }

        public bool BuyItem(int price)
        {
            if (price == 0)
            {
                return false;
            }
            int num = 0;
            int num2 = price;
            Item[] itemArray = new Item[0x2c];
            for (int i = 0; i < 0x2c; i++)
            {
                itemArray[i] = new Item();
                itemArray[i] = (Item) this.inventory[i].Clone();
                if (this.inventory[i].type == 0x47)
                {
                    num += this.inventory[i].stack;
                }
                if (this.inventory[i].type == 0x48)
                {
                    num += this.inventory[i].stack * 100;
                }
                if (this.inventory[i].type == 0x49)
                {
                    num += this.inventory[i].stack * 0x2710;
                }
                if (this.inventory[i].type == 0x4a)
                {
                    num += this.inventory[i].stack * 0xf4240;
                }
            }
            if (num < price)
            {
                return false;
            }
            num2 = price;
            while (num2 > 0)
            {
                if (num2 >= 0xf4240)
                {
                    for (int j = 0; j < 0x2c; j++)
                    {
                        if (this.inventory[j].type == 0x4a)
                        {
                            while ((this.inventory[j].stack > 0) && (num2 >= 0xf4240))
                            {
                                num2 -= 0xf4240;
                                Item item1 = this.inventory[j];
                                item1.stack--;
                                if (this.inventory[j].stack == 0)
                                {
                                    this.inventory[j].type = 0;
                                }
                            }
                        }
                    }
                }
                if (num2 >= 0x2710)
                {
                    for (int k = 0; k < 0x2c; k++)
                    {
                        if (this.inventory[k].type == 0x49)
                        {
                            while ((this.inventory[k].stack > 0) && (num2 >= 0x2710))
                            {
                                num2 -= 0x2710;
                                Item item2 = this.inventory[k];
                                item2.stack--;
                                if (this.inventory[k].stack == 0)
                                {
                                    this.inventory[k].type = 0;
                                }
                            }
                        }
                    }
                }
                if (num2 >= 100)
                {
                    for (int m = 0; m < 0x2c; m++)
                    {
                        if (this.inventory[m].type == 0x48)
                        {
                            while ((this.inventory[m].stack > 0) && (num2 >= 100))
                            {
                                num2 -= 100;
                                Item item3 = this.inventory[m];
                                item3.stack--;
                                if (this.inventory[m].stack == 0)
                                {
                                    this.inventory[m].type = 0;
                                }
                            }
                        }
                    }
                }
                if (num2 >= 1)
                {
                    for (int n = 0; n < 0x2c; n++)
                    {
                        if (this.inventory[n].type == 0x47)
                        {
                            while ((this.inventory[n].stack > 0) && (num2 >= 1))
                            {
                                num2--;
                                Item item4 = this.inventory[n];
                                item4.stack--;
                                if (this.inventory[n].stack == 0)
                                {
                                    this.inventory[n].type = 0;
                                }
                            }
                        }
                    }
                }
                if (num2 > 0)
                {
                    int index = -1;
                    for (int num9 = 0x2b; num9 >= 0; num9--)
                    {
                        if ((this.inventory[num9].type == 0) || (this.inventory[num9].stack == 0))
                        {
                            index = num9;
                            break;
                        }
                    }
                    if (index >= 0)
                    {
                        bool flag = true;
                        if (num2 >= 0x2710)
                        {
                            for (int num10 = 0; num10 < 0x2c; num10++)
                            {
                                if ((this.inventory[num10].type == 0x4a) && (this.inventory[num10].stack >= 1))
                                {
                                    Item item5 = this.inventory[num10];
                                    item5.stack--;
                                    if (this.inventory[num10].stack == 0)
                                    {
                                        this.inventory[num10].type = 0;
                                    }
                                    this.inventory[index].SetDefaults(0x49, false);
                                    this.inventory[index].stack = 100;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else if (num2 >= 100)
                        {
                            for (int num11 = 0; num11 < 0x2c; num11++)
                            {
                                if ((this.inventory[num11].type == 0x49) && (this.inventory[num11].stack >= 1))
                                {
                                    Item item6 = this.inventory[num11];
                                    item6.stack--;
                                    if (this.inventory[num11].stack == 0)
                                    {
                                        this.inventory[num11].type = 0;
                                    }
                                    this.inventory[index].SetDefaults(0x48, false);
                                    this.inventory[index].stack = 100;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else if (num2 >= 1)
                        {
                            for (int num12 = 0; num12 < 0x2c; num12++)
                            {
                                if ((this.inventory[num12].type == 0x48) && (this.inventory[num12].stack >= 1))
                                {
                                    Item item7 = this.inventory[num12];
                                    item7.stack--;
                                    if (this.inventory[num12].stack == 0)
                                    {
                                        this.inventory[num12].type = 0;
                                    }
                                    this.inventory[index].SetDefaults(0x47, false);
                                    this.inventory[index].stack = 100;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (!flag)
                        {
                            continue;
                        }
                        if (num2 < 0x2710)
                        {
                            for (int num13 = 0; num13 < 0x2c; num13++)
                            {
                                if ((this.inventory[num13].type == 0x49) && (this.inventory[num13].stack >= 1))
                                {
                                    Item item8 = this.inventory[num13];
                                    item8.stack--;
                                    if (this.inventory[num13].stack == 0)
                                    {
                                        this.inventory[num13].type = 0;
                                    }
                                    this.inventory[index].SetDefaults(0x48, false);
                                    this.inventory[index].stack = 100;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag && (num2 < 0xf4240))
                        {
                            for (int num14 = 0; num14 < 0x2c; num14++)
                            {
                                if ((this.inventory[num14].type == 0x4a) && (this.inventory[num14].stack >= 1))
                                {
                                    Item item9 = this.inventory[num14];
                                    item9.stack--;
                                    if (this.inventory[num14].stack == 0)
                                    {
                                        this.inventory[num14].type = 0;
                                    }
                                    this.inventory[index].SetDefaults(0x49, false);
                                    this.inventory[index].stack = 100;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        continue;
                    }
                    for (int num15 = 0; num15 < 0x2c; num15++)
                    {
                        this.inventory[num15] = (Item) itemArray[num15].Clone();
                    }
                    return false;
                }
            }
            return true;
        }

        public void ChangeSpawn(int x, int y)
        {
            for (int i = 0; i < 200; i++)
            {
                if (this.spN[i] == null)
                {
                    break;
                }
                if ((this.spN[i] == Main.worldName) && (this.spI[i] == Main.worldID))
                {
                    for (int k = i; k > 0; k--)
                    {
                        this.spN[k] = this.spN[k - 1];
                        this.spI[k] = this.spI[k - 1];
                        this.spX[k] = this.spX[k - 1];
                        this.spY[k] = this.spY[k - 1];
                    }
                    this.spN[0] = Main.worldName;
                    this.spI[0] = Main.worldID;
                    this.spX[0] = x;
                    this.spY[0] = y;
                    return;
                }
            }
            for (int j = 0xc7; j > 0; j--)
            {
                if (this.spN[j - 1] != null)
                {
                    this.spN[j] = this.spN[j - 1];
                    this.spI[j] = this.spI[j - 1];
                    this.spX[j] = this.spX[j - 1];
                    this.spY[j] = this.spY[j - 1];
                }
            }
            this.spN[0] = Main.worldName;
            this.spI[0] = Main.worldID;
            this.spX[0] = x;
            this.spY[0] = y;
        }

        public void checkArmor()
        {
        }

        public static bool CheckSpawn(int x, int y)
        {
            if (((x < 10) || (x > (Main.maxTilesX - 10))) || ((y < 10) || (y > (Main.maxTilesX - 10))))
            {
                return false;
            }
            if (Main.tile[x, y - 1] == null)
            {
                return false;
            }
            if (!Main.tile[x, y - 1].active || (Main.tile[x, y - 1].type != 0x4f))
            {
                return false;
            }
            for (int i = x - 1; i <= (x + 1); i++)
            {
                for (int j = y - 3; j < y; j++)
                {
                    if (Main.tile[i, j] == null)
                    {
                        return false;
                    }
                    if ((Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type]) && !Main.tileSolidTop[Main.tile[i, j].type])
                    {
                        return false;
                    }
                }
            }
            if (!WorldGen.StartRoomCheck(x, y - 1))
            {
                return false;
            }
            return true;
        }

        public object clientClone()
        {
            Player player = new Player {
                zoneEvil = this.zoneEvil,
                zoneMeteor = this.zoneMeteor,
                zoneDungeon = this.zoneDungeon,
                zoneJungle = this.zoneJungle,
                direction = this.direction,
                selectedItem = this.selectedItem,
                controlUp = this.controlUp,
                controlDown = this.controlDown,
                controlLeft = this.controlLeft,
                controlRight = this.controlRight,
                controlJump = this.controlJump,
                controlUseItem = this.controlUseItem,
                statLife = this.statLife,
                statLifeMax = this.statLifeMax,
                statMana = this.statMana,
                statManaMax = this.statManaMax
            };
            player.position.X = this.position.X;
            player.chest = this.chest;
            player.talkNPC = this.talkNPC;
            for (int i = 0; i < 0x2c; i++)
            {
                player.inventory[i] = (Item) this.inventory[i].Clone();
                if (i < 11)
                {
                    player.armor[i] = (Item) this.armor[i].Clone();
                }
            }
            for (int j = 0; j < 10; j++)
            {
                player.buffType[j] = this.buffType[j];
                player.buffTime[j] = this.buffTime[j];
            }
            return player;
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public int countBuffs()
        {
            int index = 0;
            for (int i = 0; i < 10; i++)
            {
                if (this.buffType[index] > 0)
                {
                    index++;
                }
            }
            return index;
        }

        private static bool DecryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            byte[] bytes = new UnicodeEncoding().GetBytes(s);
            FileStream stream = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            FileStream stream3 = new FileStream(outputFile, FileMode.Create);
            try
            {
                int num;
                while ((num = stream2.ReadByte()) != -1)
                {
                    stream3.WriteByte((byte) num);
                }
                stream3.Close();
                stream2.Close();
                stream.Close();
            }
            catch
            {
                stream3.Close();
                stream.Close();
                File.Delete(outputFile);
                return true;
            }
            return false;
        }

        public void DelBuff(int b)
        {
            this.buffTime[b] = 0;
            this.buffType[b] = 0;
            for (int i = 0; i < 9; i++)
            {
                if ((this.buffTime[i] == 0) || (this.buffType[i] == 0))
                {
                    for (int j = i + 1; j < 10; j++)
                    {
                        this.buffTime[j - 1] = this.buffTime[j];
                        this.buffType[j - 1] = this.buffType[j];
                        this.buffTime[j] = 0;
                        this.buffType[j] = 0;
                    }
                }
            }
        }

        public void DoCoins(int i)
        {
            if ((this.inventory[i].stack == 100) && (((this.inventory[i].type == 0x47) || (this.inventory[i].type == 0x48)) || (this.inventory[i].type == 0x49)))
            {
                this.inventory[i].SetDefaults(this.inventory[i].type + 1, false);
                for (int j = 0; j < 0x2c; j++)
                {
                    if ((this.inventory[j].IsTheSameAs(this.inventory[i]) && (j != i)) && (this.inventory[j].stack < this.inventory[j].maxStack))
                    {
                        Item item1 = this.inventory[j];
                        item1.stack++;
                        this.inventory[i].SetDefaults("");
                        this.inventory[i].active = false;
                        this.inventory[i].name = "";
                        this.inventory[i].type = 0;
                        this.inventory[i].stack = 0;
                        this.DoCoins(j);
                    }
                }
            }
        }

        public void DropCoins()
        {
            for (int i = 0; i < 0x2c; i++)
            {
                if ((this.inventory[i].type >= 0x47) && (this.inventory[i].type <= 0x4a))
                {
                    int index = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.inventory[i].type, 1, false);
                    int num3 = this.inventory[i].stack / 2;
                    num3 = this.inventory[i].stack - num3;
                    Item item1 = this.inventory[i];
                    item1.stack -= num3;
                    if (this.inventory[i].stack <= 0)
                    {
                        this.inventory[i] = new Item();
                    }
                    Main.item[index].stack = num3;
                    Main.item[index].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                    Main.item[index].velocity.X = Main.rand.Next(-20, 0x15) * 0.2f;
                    Main.item[index].noGrabDelay = 100;
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(0x15, -1, -1, "", index, 0f, 0f, 0f, 0);
                    }
                }
            }
        }

        public void dropItemCheck()
        {
            if (!Main.craftGuide && (Main.guideItem.type > 0))
            {
                int index = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, Main.guideItem.type, 1, false);
                Main.guideItem.position = Main.item[index].position;
                Main.item[index] = Main.guideItem;
                Main.guideItem = new Item();
                if (Main.netMode == 0)
                {
                    Main.item[index].noGrabDelay = 100;
                }
                Main.item[index].velocity.Y = -2f;
                Main.item[index].velocity.X = (4 * this.direction) + this.velocity.X;
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(0x15, -1, -1, "", index, 0f, 0f, 0f, 0);
                }
            }
            if (((this.controlThrow && (this.inventory[this.selectedItem].type > 0)) && !Main.chatMode) || (((((Main.mouseState.LeftButton == ButtonState.Pressed) && !this.mouseInterface) && Main.mouseLeftRelease) || !Main.playerInventory) && (Main.mouseItem.type > 0)))
            {
                Item item = new Item();
                bool flag = false;
                if (((((Main.mouseState.LeftButton == ButtonState.Pressed) && !this.mouseInterface) && Main.mouseLeftRelease) || !Main.playerInventory) && (Main.mouseItem.type > 0))
                {
                    item = this.inventory[this.selectedItem];
                    this.inventory[this.selectedItem] = Main.mouseItem;
                    this.delayUseItem = true;
                    this.controlUseItem = false;
                    flag = true;
                }
                int num2 = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.inventory[this.selectedItem].type, 1, false);
                if ((!flag && (this.inventory[this.selectedItem].type == 8)) && (this.inventory[this.selectedItem].stack > 1))
                {
                    Item item1 = this.inventory[this.selectedItem];
                    item1.stack--;
                }
                else
                {
                    this.inventory[this.selectedItem].position = Main.item[num2].position;
                    Main.item[num2] = this.inventory[this.selectedItem];
                    this.inventory[this.selectedItem] = new Item();
                }
                if (Main.netMode == 0)
                {
                    Main.item[num2].noGrabDelay = 100;
                }
                Main.item[num2].velocity.Y = -2f;
                Main.item[num2].velocity.X = (4 * this.direction) + this.velocity.X;
                if ((((Main.mouseState.LeftButton == ButtonState.Pressed) && !this.mouseInterface) || !Main.playerInventory) && (Main.mouseItem.type > 0))
                {
                    this.inventory[this.selectedItem] = item;
                    Main.mouseItem = new Item();
                }
                else
                {
                    this.itemAnimation = 10;
                    this.itemAnimationMax = 10;
                }
                Recipe.FindRecipes();
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(0x15, -1, -1, "", num2, 0f, 0f, 0f, 0);
                }
            }
        }

        public void DropItems()
        {
            for (int i = 0; i < 0x2c; i++)
            {
                if (((this.inventory[i].stack > 0) && (this.inventory[i].name != "Copper Pickaxe")) && ((this.inventory[i].name != "Copper Axe") && (this.inventory[i].name != "Copper Shortsword")))
                {
                    int index = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.inventory[i].type, 1, false);
                    Main.item[index].SetDefaults(this.inventory[i].name);
                    Main.item[index].stack = this.inventory[i].stack;
                    Main.item[index].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                    Main.item[index].velocity.X = Main.rand.Next(-20, 0x15) * 0.2f;
                    Main.item[index].noGrabDelay = 100;
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(0x15, -1, -1, "", index, 0f, 0f, 0f, 0);
                    }
                }
                this.inventory[i] = new Item();
                if (i < 11)
                {
                    if (this.armor[i].stack > 0)
                    {
                        int num3 = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.armor[i].type, 1, false);
                        Main.item[num3].SetDefaults(this.armor[i].name);
                        Main.item[num3].stack = this.armor[i].stack;
                        Main.item[num3].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num3].velocity.X = Main.rand.Next(-20, 0x15) * 0.2f;
                        Main.item[num3].noGrabDelay = 100;
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(0x15, -1, -1, "", num3, 0f, 0f, 0f, 0);
                        }
                    }
                    this.armor[i] = new Item();
                }
            }
            this.inventory[0].SetDefaults("Copper Shortsword");
            this.inventory[1].SetDefaults("Copper Pickaxe");
            this.inventory[2].SetDefaults("Copper Axe");
        }

        private static void EncryptFile(string inputFile, string outputFile)
        {
            int num;
            string s = "h3y_gUyZ";
            byte[] bytes = new UnicodeEncoding().GetBytes(s);
            string path = outputFile;
            FileStream stream = new FileStream(path, FileMode.Create);
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            FileStream stream3 = new FileStream(inputFile, FileMode.Open);
            while ((num = stream3.ReadByte()) != -1)
            {
                stream2.WriteByte((byte) num);
            }
            stream3.Close();
            stream2.Close();
            stream.Close();
        }

        public static byte FindClosest(Vector2 Position, int Width, int Height)
        {
            byte num = 0;
            for (int i = 0; i < 0xff; i++)
            {
                if (Main.player[i].active)
                {
                    num = (byte) i;
                    break;
                }
            }
            float num3 = -1f;
            for (int j = 0; j < 0xff; j++)
            {
                if ((Main.player[j].active && !Main.player[j].dead) && ((num3 == -1f) || ((Math.Abs((float) (((Main.player[j].position.X + (Main.player[j].width / 2)) - Position.X) + (Width / 2))) + Math.Abs((float) (((Main.player[j].position.Y + (Main.player[j].height / 2)) - Position.Y) + (Height / 2)))) < num3)))
                {
                    num3 = Math.Abs((float) (((Main.player[j].position.X + (Main.player[j].width / 2)) - Position.X) + (Width / 2))) + Math.Abs((float) (((Main.player[j].position.Y + (Main.player[j].height / 2)) - Position.Y) + (Height / 2)));
                    num = (byte) j;
                }
            }
            return num;
        }

        public void FindSpawn()
        {
            for (int i = 0; i < 200; i++)
            {
                if (this.spN[i] == null)
                {
                    this.SpawnX = -1;
                    this.SpawnY = -1;
                    return;
                }
                if ((this.spN[i] == Main.worldName) && (this.spI[i] == Main.worldID))
                {
                    this.SpawnX = this.spX[i];
                    this.SpawnY = this.spY[i];
                    return;
                }
            }
        }

        public Color GetDeathAlpha(Color newColor)
        {
            int r = newColor.R + ((int) (this.immuneAlpha * 0.9));
            int g = newColor.G + ((int) (this.immuneAlpha * 0.5));
            int b = newColor.B + ((int) (this.immuneAlpha * 0.5));
            int a = newColor.A + ((int) (this.immuneAlpha * 0.4));
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public static string getDeathMessage(int plr = -1, int npc = -1, int proj = -1, int other = -1)
        {
            string str = "";
            int num = Main.rand.Next(11);
            string str2 = "";
            switch (num)
            {
                case 0:
                    str2 = " was slain";
                    break;

                case 1:
                    str2 = " was eviscerated";
                    break;

                case 2:
                    str2 = " was murdered";
                    break;

                case 3:
                    str2 = "'s face was torn off";
                    break;

                case 4:
                    str2 = "'s entrails were ripped out";
                    break;

                case 5:
                    str2 = " was destroyed";
                    break;

                case 6:
                    str2 = "'s skull was crushed";
                    break;

                case 7:
                    str2 = " got massacred";
                    break;

                case 8:
                    str2 = " got impaled";
                    break;

                case 9:
                    str2 = " was torn in half";
                    break;

                case 10:
                    str2 = " was decapitated";
                    break;
            }
            if ((plr >= 0) && (plr < 0xff))
            {
                if (proj >= 0)
                {
                    return (str2 + " by " + Main.player[plr].name + "'s " + Main.projectile[proj].name + ".");
                }
                return (str2 + " by " + Main.player[plr].name + "'s " + Main.player[plr].inventory[Main.player[plr].selectedItem].name + ".");
            }
            if (npc >= 0)
            {
                return (str2 + " by " + Main.npc[npc].name + ".");
            }
            if (proj >= 0)
            {
                return (str2 + " by " + Main.projectile[proj].name + ".");
            }
            if (other >= 0)
            {
                if (other == 0)
                {
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            return " fell to their death.";

                        case 1:
                            return " faceplanted the ground.";

                        case 2:
                            return " fell victim to gravity.";

                        case 3:
                            return " left a small crater.";

                        case 4:
                            str = " didn't bounce.";
                            break;
                    }
                    return str;
                }
                if (other == 1)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            return " forgot to breathe.";

                        case 1:
                            return " is sleeping with the fish.";

                        case 2:
                            return " drowned.";

                        case 3:
                            str = " is shark food.";
                            break;
                    }
                    return str;
                }
                if (other == 2)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            return " got melted.";

                        case 1:
                            return " was incinerated.";

                        case 2:
                            return " tried to swim in lava.";

                        case 3:
                            str = " likes to play in magma.";
                            break;
                    }
                    return str;
                }
                if (other == 3)
                {
                    str = str2 + ".";
                }
            }
            return str;
        }

        public Color GetImmuneAlpha(Color newColor)
        {
            float num = ((float) (0xff - this.immuneAlpha)) / 255f;
            if (this.shadow > 0f)
            {
                num *= 1f - this.shadow;
            }
            int r = (int) (newColor.R * num);
            int g = (int) (newColor.G * num);
            int b = (int) (newColor.B * num);
            int a = (int) (newColor.A * num);
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public Item GetItem(int plr, Item newItem)
        {
            Item item = newItem;
            if (newItem.noGrabDelay <= 0)
            {
                int num = 0;
                if (((newItem.type == 0x47) || (newItem.type == 0x48)) || ((newItem.type == 0x49) || (newItem.type == 0x4a)))
                {
                    num = -4;
                }
                for (int i = num; i < 40; i++)
                {
                    int index = i;
                    if (index < 0)
                    {
                        index = 0x2c + i;
                    }
                    if (((this.inventory[index].type > 0) && (this.inventory[index].stack < this.inventory[index].maxStack)) && item.IsTheSameAs(this.inventory[index]))
                    {
                        Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                        if ((item.stack + this.inventory[index].stack) <= this.inventory[index].maxStack)
                        {
                            Item item1 = this.inventory[index];
                            item1.stack += item.stack;
                            ItemText.NewText(newItem, item.stack);
                            this.DoCoins(index);
                            if (plr == Main.myPlayer)
                            {
                                Recipe.FindRecipes();
                            }
                            return new Item();
                        }
                        item.stack -= this.inventory[index].maxStack - this.inventory[index].stack;
                        ItemText.NewText(newItem, this.inventory[index].maxStack - this.inventory[index].stack);
                        this.inventory[index].stack = this.inventory[index].maxStack;
                        this.DoCoins(index);
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                    }
                }
                for (int j = num; j < 40; j++)
                {
                    int num5 = j;
                    if (num5 < 0)
                    {
                        num5 = 0x2c + j;
                    }
                    if (this.inventory[num5].type == 0)
                    {
                        this.inventory[num5] = item;
                        ItemText.NewText(newItem, newItem.stack);
                        this.DoCoins(num5);
                        Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                        return new Item();
                    }
                }
            }
            return item;
        }

        public bool HasItem(int type)
        {
            for (int i = 0; i < 0x2c; i++)
            {
                if (type == this.inventory[i].type)
                {
                    return true;
                }
            }
            return false;
        }

        public void HealEffect(int healAmount)
        {
            CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(100, 100, 0xff, 0xff), healAmount.ToString());
            if ((Main.netMode == 1) && (this.whoAmi == Main.myPlayer))
            {
                NetMessage.SendData(0x23, -1, -1, "", this.whoAmi, (float) healAmount, 0f, 0f, 0);
            }
        }

        public double Hurt(int Damage, int hitDirection, bool pvp = false, bool quiet = false, string deathText = " was slain...")
        {
            if (this.immune)
            {
                return 0.0;
            }
            int damage = Damage;
            if (pvp)
            {
                damage *= 2;
            }
            double dmg = Main.CalculateDamage(damage, this.statDefense);
            if (dmg >= 1.0)
            {
                if (((Main.netMode == 1) && (this.whoAmi == Main.myPlayer)) && !quiet)
                {
                    int num3 = 0;
                    if (pvp)
                    {
                        num3 = 1;
                    }
                    NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
                    NetMessage.SendData(0x10, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
                    NetMessage.SendData(0x1a, -1, -1, "", this.whoAmi, (float) hitDirection, (float) Damage, (float) num3, 0);
                }
                CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(0xff, 80, 90, 0xff), ((int) dmg).ToString());
                this.statLife -= (int) dmg;
                this.immune = true;
                this.immuneTime = 40;
                if (pvp)
                {
                    this.immuneTime = 8;
                }
                if (!this.noKnockback && (hitDirection != 0))
                {
                    this.velocity.X = 4.5f * hitDirection;
                    this.velocity.Y = -3.5f;
                }
                if (this.boneArmor)
                {
                    Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, 2);
                }
                else if (((this.hair == 5) || (this.hair == 6)) || ((this.hair == 9) || (this.hair == 11)))
                {
                    Main.PlaySound(20, (int) this.position.X, (int) this.position.Y, 1);
                }
                else
                {
                    Main.PlaySound(1, (int) this.position.X, (int) this.position.Y, 1);
                }
                if (this.statLife > 0)
                {
                    for (int i = 0; i < ((dmg / ((double) this.statLifeMax)) * 100.0); i++)
                    {
                        if (this.boneArmor)
                        {
                            Color newColor = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) (2 * hitDirection), -2f, 0, newColor, 1f);
                        }
                        else
                        {
                            Color color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                    }
                }
                else
                {
                    this.statLife = 0;
                    if (this.whoAmi == Main.myPlayer)
                    {
                        this.KillMe(dmg, hitDirection, pvp, deathText);
                    }
                }
            }
            if (pvp)
            {
                dmg = Main.CalculateDamage(damage, this.statDefense);
            }
            return dmg;
        }

        public void ItemCheck(int i)
        {
            Color color;
            if (this.inventory[this.selectedItem].autoReuse)
            {
                this.releaseUseItem = true;
                if ((this.itemAnimation == 1) && (this.inventory[this.selectedItem].stack > 0))
                {
                    this.itemAnimation = 0;
                }
            }
            if ((this.controlUseItem && (this.itemAnimation == 0)) && (this.releaseUseItem && (this.inventory[this.selectedItem].useStyle > 0)))
            {
                bool flag = true;
                if (((this.inventory[this.selectedItem].shoot == 6) || (this.inventory[this.selectedItem].shoot == 0x13)) || ((this.inventory[this.selectedItem].shoot == 0x21) || (this.inventory[this.selectedItem].shoot == 0x34)))
                {
                    for (int j = 0; j < 0x3e8; j++)
                    {
                        if ((Main.projectile[j].active && (Main.projectile[j].owner == Main.myPlayer)) && (Main.projectile[j].type == this.inventory[this.selectedItem].shoot))
                        {
                            flag = false;
                        }
                    }
                }
                if (this.inventory[this.selectedItem].potion)
                {
                    if (this.potionDelay <= 0)
                    {
                        this.potionDelay = Item.potionDelay;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if ((this.inventory[this.selectedItem].mana > 0) && ((this.inventory[this.selectedItem].type != 0x7f) || !this.spaceGun))
                {
                    if (this.statMana >= ((int) (this.inventory[this.selectedItem].mana * this.manaCost)))
                    {
                        this.statMana -= (int) (this.inventory[this.selectedItem].mana * this.manaCost);
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if ((this.inventory[this.selectedItem].type == 0x2b) && Main.dayTime)
                {
                    flag = false;
                }
                if ((this.inventory[this.selectedItem].type == 70) && !this.zoneEvil)
                {
                    flag = false;
                }
                if (((this.inventory[this.selectedItem].shoot == 0x11) && flag) && (i == Main.myPlayer))
                {
                    int num2 = (Main.mouseState.X + ((int) Main.screenPosition.X)) / 0x10;
                    int num3 = (Main.mouseState.Y + ((int) Main.screenPosition.Y)) / 0x10;
                    if (Main.tile[num2, num3].active && (((Main.tile[num2, num3].type == 0) || (Main.tile[num2, num3].type == 2)) || (Main.tile[num2, num3].type == 0x17)))
                    {
                        WorldGen.KillTile(num2, num3, false, false, true);
                        if (!Main.tile[num2, num3].active)
                        {
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(0x11, -1, -1, "", 4, (float) num2, (float) num3, 0f, 0);
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag && (this.inventory[this.selectedItem].useAmmo > 0))
                {
                    flag = false;
                    for (int k = 0; k < 0x2c; k++)
                    {
                        if ((this.inventory[k].ammo == this.inventory[this.selectedItem].useAmmo) && (this.inventory[k].stack > 0))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    if (this.grappling[0] > -1)
                    {
                        if (this.controlRight)
                        {
                            this.direction = 1;
                        }
                        else if (this.controlLeft)
                        {
                            this.direction = -1;
                        }
                    }
                    this.channel = this.inventory[this.selectedItem].channel;
                    this.attackCD = 0;
                    if ((this.inventory[this.selectedItem].shoot > 0) || (this.inventory[this.selectedItem].damage == 0))
                    {
                        this.meleeSpeed = 1f;
                    }
                    this.itemAnimation = (int) (this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
                    this.itemAnimationMax = (int) (this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
                    if (this.inventory[this.selectedItem].useSound > 0)
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, this.inventory[this.selectedItem].useSound);
                    }
                }
                if (flag && (this.inventory[this.selectedItem].shoot == 0x12))
                {
                    for (int m = 0; m < 0x3e8; m++)
                    {
                        if ((Main.projectile[m].active && (Main.projectile[m].owner == i)) && (Main.projectile[m].type == this.inventory[this.selectedItem].shoot))
                        {
                            Main.projectile[m].Kill();
                        }
                    }
                }
            }
            if (!this.controlUseItem)
            {
                this.channel = false;
            }
            if (this.itemAnimation > 0)
            {
                if (this.inventory[this.selectedItem].mana > 0)
                {
                    this.manaRegenDelay = 180;
                }
                if (Main.dedServ)
                {
                    this.itemHeight = this.inventory[this.selectedItem].height;
                    this.itemWidth = this.inventory[this.selectedItem].width;
                }
                else
                {
                    this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
                    this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
                }
                this.itemAnimation--;
                if (!Main.dedServ)
                {
                    if (this.inventory[this.selectedItem].useStyle == 1)
                    {
                        if (this.itemAnimation < (this.itemAnimationMax * 0.333))
                        {
                            float num6 = 10f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 0x20)
                            {
                                num6 = 14f;
                            }
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - num6) * this.direction);
                            this.itemLocation.Y = this.position.Y + 24f;
                        }
                        else if (this.itemAnimation < (this.itemAnimationMax * 0.666))
                        {
                            float num7 = 10f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 0x20)
                            {
                                num7 = 18f;
                            }
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - num7) * this.direction);
                            num7 = 10f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 0x20)
                            {
                                num7 = 8f;
                            }
                            this.itemLocation.Y = this.position.Y + num7;
                        }
                        else
                        {
                            float num8 = 6f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 0x20)
                            {
                                num8 = 14f;
                            }
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) - (((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - num8) * this.direction);
                            num8 = 10f;
                            if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 0x20)
                            {
                                num8 = 10f;
                            }
                            this.itemLocation.Y = this.position.Y + num8;
                        }
                        this.itemRotation = ((((((float) this.itemAnimation) / ((float) this.itemAnimationMax)) - 0.5f) * -this.direction) * 3.5f) - (this.direction * 0.3f);
                        if (this.gravDir == -1f)
                        {
                            this.itemRotation = -this.itemRotation;
                            this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                        }
                    }
                    else if (this.inventory[this.selectedItem].useStyle == 2)
                    {
                        this.itemRotation = (((((float) this.itemAnimation) / ((float) this.itemAnimationMax)) * this.direction) * 2f) + (-1.4f * this.direction);
                        if (this.itemAnimation < (this.itemAnimationMax * 0.5))
                        {
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + ((((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - 9f) - ((this.itemRotation * 12f) * this.direction)) * this.direction);
                            this.itemLocation.Y = (this.position.Y + 38f) + ((this.itemRotation * this.direction) * 4f);
                        }
                        else
                        {
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + ((((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - 9f) - ((this.itemRotation * 16f) * this.direction)) * this.direction);
                            this.itemLocation.Y = (this.position.Y + 38f) + (this.itemRotation * this.direction);
                        }
                        if (this.gravDir == -1f)
                        {
                            this.itemRotation = -this.itemRotation;
                            this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                        }
                    }
                    else if (this.inventory[this.selectedItem].useStyle == 3)
                    {
                        if (this.itemAnimation > (this.itemAnimationMax * 0.666))
                        {
                            this.itemLocation.X = -1000f;
                            this.itemLocation.Y = -1000f;
                            this.itemRotation = -1.3f * this.direction;
                        }
                        else
                        {
                            this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - 4f) * this.direction);
                            this.itemLocation.Y = this.position.Y + 24f;
                            float num9 = (((((((float) this.itemAnimation) / ((float) this.itemAnimationMax)) * Main.itemTexture[this.inventory[this.selectedItem].type].Width) * this.direction) * this.inventory[this.selectedItem].scale) * 1.2f) - (10 * this.direction);
                            if ((num9 > -4f) && (this.direction == -1))
                            {
                                num9 = -8f;
                            }
                            if ((num9 < 4f) && (this.direction == 1))
                            {
                                num9 = 8f;
                            }
                            this.itemLocation.X -= num9;
                            this.itemRotation = 0.8f * this.direction;
                        }
                        if (this.gravDir == -1f)
                        {
                            this.itemRotation = -this.itemRotation;
                            this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                        }
                    }
                    else if (this.inventory[this.selectedItem].useStyle == 4)
                    {
                        this.itemRotation = 0f;
                        this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + ((((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) - 9f) - ((this.itemRotation * 14f) * this.direction)) * this.direction);
                        this.itemLocation.Y = this.position.Y + (Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f);
                        if (this.gravDir == -1f)
                        {
                            this.itemRotation = -this.itemRotation;
                            this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                        }
                    }
                    else if (this.inventory[this.selectedItem].useStyle == 5)
                    {
                        this.itemLocation.X = ((this.position.X + (this.width * 0.5f)) - (Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f)) - (this.direction * 2);
                        this.itemLocation.Y = (this.position.Y + (this.height * 0.5f)) - (Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f);
                    }
                }
            }
            else if (this.inventory[this.selectedItem].holdStyle == 1)
            {
                if (Main.dedServ)
                {
                    this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (20f * this.direction);
                }
                else
                {
                    this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (((Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f) + 4f) * this.direction);
                    if ((this.inventory[this.selectedItem].type == 0x11a) || (this.inventory[this.selectedItem].type == 0x11e))
                    {
                        this.itemLocation.X -= this.direction * 4;
                        this.itemLocation.Y += 4f;
                    }
                }
                this.itemLocation.Y = this.position.Y + 24f;
                this.itemRotation = 0f;
                if (this.gravDir == -1f)
                {
                    this.itemRotation = -this.itemRotation;
                    this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                }
            }
            else if (this.inventory[this.selectedItem].holdStyle == 2)
            {
                this.itemLocation.X = (this.position.X + (this.width * 0.5f)) + (6 * this.direction);
                this.itemLocation.Y = this.position.Y + 16f;
                this.itemRotation = 0.79f * -this.direction;
                if (this.gravDir == -1f)
                {
                    this.itemRotation = -this.itemRotation;
                    this.itemLocation.Y = (this.position.Y + this.height) + (this.position.Y - this.itemLocation.Y);
                }
            }
            if ((this.inventory[this.selectedItem].type == 8) && !this.wet)
            {
                int maxValue = 20;
                if (this.itemAnimation > 0)
                {
                    maxValue = 7;
                }
                if (this.direction == -1)
                {
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X - 16f, this.itemLocation.Y - (14f * this.gravDir)), 4, 4, 6, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X - 16f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
                else
                {
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X + 6f, this.itemLocation.Y - (14f * this.gravDir)), 4, 4, 6, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X + 6f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
            }
            else if ((this.inventory[this.selectedItem].type == 0x69) && !this.wet)
            {
                int num11 = 20;
                if (this.itemAnimation > 0)
                {
                    num11 = 7;
                }
                if (this.direction == -1)
                {
                    if (Main.rand.Next(num11) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - (20f * this.gravDir)), 4, 4, 6, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X - 16f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
                else
                {
                    if (Main.rand.Next(num11) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - (20f * this.gravDir)), 4, 4, 6, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X + 6f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
            }
            else if ((this.inventory[this.selectedItem].type == 0x94) && !this.wet)
            {
                int num12 = 10;
                if (this.itemAnimation > 0)
                {
                    num12 = 7;
                }
                if (this.direction == -1)
                {
                    if (Main.rand.Next(num12) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - (20f * this.gravDir)), 4, 4, 0x1d, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X - 16f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
                else
                {
                    if (Main.rand.Next(num12) == 0)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - (20f * this.gravDir)), 4, 4, 0x1d, 0f, 0f, 100, color, 1f);
                    }
                    Lighting.addLight((int) (((this.itemLocation.X + 6f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 1f);
                }
            }
            if ((this.inventory[this.selectedItem].type == 0x11a) || (this.inventory[this.selectedItem].type == 0x11e))
            {
                if (this.direction == -1)
                {
                    Lighting.addLight((int) (((this.itemLocation.X - 16f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 0.8f);
                }
                else
                {
                    Lighting.addLight((int) (((this.itemLocation.X + 6f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 0.8f);
                }
            }
            if (this.controlUseItem)
            {
                this.releaseUseItem = false;
            }
            else
            {
                this.releaseUseItem = true;
            }
            if (this.itemTime > 0)
            {
                this.itemTime--;
            }
            if (i == Main.myPlayer)
            {
                if (((this.inventory[this.selectedItem].shoot > 0) && (this.itemAnimation > 0)) && (this.itemTime == 0))
                {
                    int shoot = this.inventory[this.selectedItem].shoot;
                    float shootSpeed = this.inventory[this.selectedItem].shootSpeed;
                    bool flag2 = false;
                    int damage = this.inventory[this.selectedItem].damage;
                    float knockBack = this.inventory[this.selectedItem].knockBack;
                    switch (shoot)
                    {
                        case 13:
                        case 0x20:
                            this.grappling[0] = -1;
                            this.grapCount = 0;
                            for (int n = 0; n < 0x3e8; n++)
                            {
                                if ((Main.projectile[n].active && (Main.projectile[n].owner == i)) && (Main.projectile[n].type == 13))
                                {
                                    Main.projectile[n].Kill();
                                }
                            }
                            break;
                    }
                    if (this.inventory[this.selectedItem].useAmmo > 0)
                    {
                        for (int num18 = 0; num18 < 0x2c; num18++)
                        {
                            if ((this.inventory[num18].ammo == this.inventory[this.selectedItem].useAmmo) && (this.inventory[num18].stack > 0))
                            {
                                if (this.inventory[num18].shoot > 0)
                                {
                                    shoot = this.inventory[num18].shoot;
                                }
                                shootSpeed += this.inventory[num18].shootSpeed;
                                damage += this.inventory[num18].damage;
                                knockBack += this.inventory[num18].knockBack;
                                Item item1 = this.inventory[num18];
                                item1.stack--;
                                if (this.inventory[num18].stack <= 0)
                                {
                                    this.inventory[num18].active = false;
                                    this.inventory[num18].name = "";
                                    this.inventory[num18].type = 0;
                                }
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                    }
                    if ((shoot == 9) && (this.position.Y > ((Main.worldSurface * 16.0) + (Main.screenHeight / 2))))
                    {
                        flag2 = false;
                    }
                    if (!flag2)
                    {
                        if (this.inventory[this.selectedItem].useStyle == 5)
                        {
                            this.itemRotation = 0f;
                            NetMessage.SendData(0x29, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
                        }
                    }
                    else
                    {
                        if (this.inventory[this.selectedItem].mana > 0)
                        {
                            damage = (int) Math.Round((double) (damage * this.magicBoost));
                        }
                        if (this.inventory[this.selectedItem].melee)
                        {
                            damage = (int) Math.Round((double) (damage * this.meleeDamage));
                        }
                        if ((this.inventory[this.selectedItem].useAmmo == 1) && this.archer)
                        {
                            damage = (int) (damage * 1.15f);
                            if (shootSpeed < 16f)
                            {
                                shootSpeed *= 1.15f;
                                if (shootSpeed > 16f)
                                {
                                    shootSpeed = 16f;
                                }
                            }
                        }
                        if ((shoot == 1) && (this.inventory[this.selectedItem].type == 120))
                        {
                            shoot = 2;
                        }
                        this.itemTime = this.inventory[this.selectedItem].useTime;
                        if ((Main.mouseState.X + Main.screenPosition.X) > (this.position.X + (this.width * 0.5f)))
                        {
                            this.direction = 1;
                        }
                        else
                        {
                            this.direction = -1;
                        }
                        Vector2 vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        switch (shoot)
                        {
                            case 9:
                                vector = new Vector2((this.position.X + (this.width * 0.5f)) + (Main.rand.Next(0x259) * -this.direction), ((this.position.Y + (this.height * 0.5f)) - 300f) - Main.rand.Next(100));
                                knockBack = 0f;
                                break;

                            case 0x33:
                                vector.Y -= 6f * this.gravDir;
                                break;
                        }
                        float speedX = (Main.mouseState.X + Main.screenPosition.X) - vector.X;
                        float speedY = (Main.mouseState.Y + Main.screenPosition.Y) - vector.Y;
                        float num21 = (float) Math.Sqrt((double) ((speedX * speedX) + (speedY * speedY)));
                        num21 = shootSpeed / num21;
                        speedX *= num21;
                        speedY *= num21;
                        if (shoot == 12)
                        {
                            vector.X += speedX * 3f;
                            vector.Y += speedY * 3f;
                        }
                        if (this.inventory[this.selectedItem].useStyle == 5)
                        {
                            this.itemRotation = (float) Math.Atan2((double) (speedY * this.direction), (double) (speedX * this.direction));
                            NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
                            NetMessage.SendData(0x29, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
                        }
                        if (shoot == 0x11)
                        {
                            vector.X = Main.mouseState.X + Main.screenPosition.X;
                            vector.Y = Main.mouseState.Y + Main.screenPosition.Y;
                        }
                        Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, shoot, damage, knockBack, i);
                    }
                }
                if ((((this.inventory[this.selectedItem].type >= 0xcd) && (this.inventory[this.selectedItem].type <= 0xcf)) && (((((this.position.X / 16f) - tileRangeX) - this.inventory[this.selectedItem].tileBoost) <= tileTargetX) && ((((((this.position.X + this.width) / 16f) + tileRangeX) + this.inventory[this.selectedItem].tileBoost) - 1f) >= tileTargetX))) && (((((this.position.Y / 16f) - tileRangeY) - this.inventory[this.selectedItem].tileBoost) <= tileTargetY) && ((((((this.position.Y + this.height) / 16f) + tileRangeY) + this.inventory[this.selectedItem].tileBoost) - 2f) >= tileTargetY)))
                {
                    this.showItemIcon = true;
                    if (((this.itemTime == 0) && (this.itemAnimation > 0)) && this.controlUseItem)
                    {
                        if (this.inventory[this.selectedItem].type == 0xcd)
                        {
                            bool lava = Main.tile[tileTargetX, tileTargetY].lava;
                            int num22 = 0;
                            for (int num23 = tileTargetX - 1; num23 <= (tileTargetX + 1); num23++)
                            {
                                for (int num24 = tileTargetY - 1; num24 <= (tileTargetY + 1); num24++)
                                {
                                    if (Main.tile[num23, num24].lava == lava)
                                    {
                                        num22 += Main.tile[num23, num24].liquid;
                                    }
                                }
                            }
                            if ((Main.tile[tileTargetX, tileTargetY].liquid > 0) && (num22 > 100))
                            {
                                bool flag4 = Main.tile[tileTargetX, tileTargetY].lava;
                                if (!Main.tile[tileTargetX, tileTargetY].lava)
                                {
                                    this.inventory[this.selectedItem].SetDefaults(0xce, false);
                                }
                                else
                                {
                                    this.inventory[this.selectedItem].SetDefaults(0xcf, false);
                                }
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                                int liquid = Main.tile[tileTargetX, tileTargetY].liquid;
                                Main.tile[tileTargetX, tileTargetY].liquid = 0;
                                Main.tile[tileTargetX, tileTargetY].lava = false;
                                WorldGen.SquareTileFrame(tileTargetX, tileTargetY, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(tileTargetX, tileTargetY);
                                }
                                else
                                {
                                    Liquid.AddWater(tileTargetX, tileTargetY);
                                }
                                for (int num26 = tileTargetX - 1; num26 <= (tileTargetX + 1); num26++)
                                {
                                    for (int num27 = tileTargetY - 1; num27 <= (tileTargetY + 1); num27++)
                                    {
                                        if ((liquid < 0x100) && (Main.tile[num26, num27].lava == lava))
                                        {
                                            int num28 = Main.tile[num26, num27].liquid;
                                            if ((num28 + liquid) > 0xff)
                                            {
                                                num28 = 0xff - liquid;
                                            }
                                            liquid += num28;
                                            Tile tile1 = Main.tile[num26, num27];
                                            tile1.liquid = (byte) (tile1.liquid - ((byte) num28));
                                            Main.tile[num26, num27].lava = flag4;
                                            if (Main.tile[num26, num27].liquid == 0)
                                            {
                                                Main.tile[num26, num27].lava = false;
                                            }
                                            WorldGen.SquareTileFrame(num26, num27, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.sendWater(num26, num27);
                                            }
                                            else
                                            {
                                                Liquid.AddWater(num26, num27);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if ((Main.tile[tileTargetX, tileTargetY].liquid < 200) && ((!Main.tile[tileTargetX, tileTargetY].active || !Main.tileSolid[Main.tile[tileTargetX, tileTargetY].type]) || Main.tileSolidTop[Main.tile[tileTargetX, tileTargetY].type]))
                        {
                            if (this.inventory[this.selectedItem].type == 0xcf)
                            {
                                if ((Main.tile[tileTargetX, tileTargetY].liquid == 0) || Main.tile[tileTargetX, tileTargetY].lava)
                                {
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                    Main.tile[tileTargetX, tileTargetY].lava = true;
                                    Main.tile[tileTargetX, tileTargetY].liquid = 0xff;
                                    WorldGen.SquareTileFrame(tileTargetX, tileTargetY, true);
                                    this.inventory[this.selectedItem].SetDefaults(0xcd, false);
                                    this.itemTime = this.inventory[this.selectedItem].useTime;
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.sendWater(tileTargetX, tileTargetY);
                                    }
                                }
                            }
                            else if ((Main.tile[tileTargetX, tileTargetY].liquid == 0) || !Main.tile[tileTargetX, tileTargetY].lava)
                            {
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                Main.tile[tileTargetX, tileTargetY].lava = false;
                                Main.tile[tileTargetX, tileTargetY].liquid = 0xff;
                                WorldGen.SquareTileFrame(tileTargetX, tileTargetY, true);
                                this.inventory[this.selectedItem].SetDefaults(0xcd, false);
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(tileTargetX, tileTargetY);
                                }
                            }
                        }
                    }
                }
                if ((((this.inventory[this.selectedItem].pick > 0) || (this.inventory[this.selectedItem].axe > 0)) || (this.inventory[this.selectedItem].hammer > 0)) && ((((((this.position.X / 16f) - tileRangeX) - this.inventory[this.selectedItem].tileBoost) <= tileTargetX) && ((((((this.position.X + this.width) / 16f) + tileRangeX) + this.inventory[this.selectedItem].tileBoost) - 1f) >= tileTargetX)) && (((((this.position.Y / 16f) - tileRangeY) - this.inventory[this.selectedItem].tileBoost) <= tileTargetY) && ((((((this.position.Y + this.height) / 16f) + tileRangeY) + this.inventory[this.selectedItem].tileBoost) - 2f) >= tileTargetY))))
                {
                    this.showItemIcon = true;
                    if ((Main.tile[tileTargetX, tileTargetY].active && (this.itemTime == 0)) && ((this.itemAnimation > 0) && this.controlUseItem))
                    {
                        if ((this.hitTileX != tileTargetX) || (this.hitTileY != tileTargetY))
                        {
                            this.hitTile = 0;
                            this.hitTileX = tileTargetX;
                            this.hitTileY = tileTargetY;
                        }
                        if (Main.tileNoFail[Main.tile[tileTargetX, tileTargetY].type])
                        {
                            this.hitTile = 100;
                        }
                        if (Main.tile[tileTargetX, tileTargetY].type != 0x1b)
                        {
                            if ((((((Main.tile[tileTargetX, tileTargetY].type == 4) || (Main.tile[tileTargetX, tileTargetY].type == 10)) || ((Main.tile[tileTargetX, tileTargetY].type == 11) || (Main.tile[tileTargetX, tileTargetY].type == 12))) || (((Main.tile[tileTargetX, tileTargetY].type == 13) || (Main.tile[tileTargetX, tileTargetY].type == 14)) || ((Main.tile[tileTargetX, tileTargetY].type == 15) || (Main.tile[tileTargetX, tileTargetY].type == 0x10)))) || ((((Main.tile[tileTargetX, tileTargetY].type == 0x11) || (Main.tile[tileTargetX, tileTargetY].type == 0x12)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x13) || (Main.tile[tileTargetX, tileTargetY].type == 0x15))) || (((Main.tile[tileTargetX, tileTargetY].type == 0x1a) || (Main.tile[tileTargetX, tileTargetY].type == 0x1c)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x1d) || (Main.tile[tileTargetX, tileTargetY].type == 0x1f))))) || (((((Main.tile[tileTargetX, tileTargetY].type == 0x21) || (Main.tile[tileTargetX, tileTargetY].type == 0x22)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x23) || (Main.tile[tileTargetX, tileTargetY].type == 0x24))) || (((Main.tile[tileTargetX, tileTargetY].type == 0x2a) || (Main.tile[tileTargetX, tileTargetY].type == 0x30)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x31) || (Main.tile[tileTargetX, tileTargetY].type == 50)))) || ((((Main.tile[tileTargetX, tileTargetY].type == 0x36) || (Main.tile[tileTargetX, tileTargetY].type == 0x37)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x4d) || (Main.tile[tileTargetX, tileTargetY].type == 0x4e))) || (((Main.tile[tileTargetX, tileTargetY].type == 0x4f) || (Main.tile[tileTargetX, tileTargetY].type == 0x51)) || (Main.tile[tileTargetX, tileTargetY].type == 0x55)))))
                            {
                                if (Main.tile[tileTargetX, tileTargetY].type == 0x30)
                                {
                                    this.hitTile += this.inventory[this.selectedItem].hammer / 3;
                                }
                                else
                                {
                                    this.hitTile += this.inventory[this.selectedItem].hammer;
                                }
                                if (((tileTargetY > Main.rockLayer) && (Main.tile[tileTargetX, tileTargetY].type == 0x4d)) && (this.inventory[this.selectedItem].hammer < 60))
                                {
                                    this.hitTile = 0;
                                }
                                if (this.inventory[this.selectedItem].hammer > 0)
                                {
                                    if (Main.tile[tileTargetX, tileTargetY].type == 0x1a)
                                    {
                                        this.Hurt(this.statLife / 2, -this.direction, false, false, getDeathMessage(-1, -1, -1, 4));
                                        WorldGen.KillTile(tileTargetX, tileTargetY, true, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                        }
                                    }
                                    else if (this.hitTile >= 100)
                                    {
                                        if ((Main.netMode == 1) && (Main.tile[tileTargetX, tileTargetY].type == 0x15))
                                        {
                                            WorldGen.KillTile(tileTargetX, tileTargetY, true, false, false);
                                            NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                            NetMessage.SendData(0x22, -1, -1, "", tileTargetX, (float) tileTargetY, 0f, 0f, 0);
                                        }
                                        else
                                        {
                                            this.hitTile = 0;
                                            WorldGen.KillTile(tileTargetX, tileTargetY, false, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 0f, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        WorldGen.KillTile(tileTargetX, tileTargetY, true, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                        }
                                    }
                                    this.itemTime = this.inventory[this.selectedItem].useTime;
                                }
                            }
                            else if (((Main.tile[tileTargetX, tileTargetY].type == 5) || (Main.tile[tileTargetX, tileTargetY].type == 30)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x48) || (Main.tile[tileTargetX, tileTargetY].type == 80)))
                            {
                                if (Main.tile[tileTargetX, tileTargetY].type == 30)
                                {
                                    this.hitTile += this.inventory[this.selectedItem].axe * 5;
                                }
                                else if (Main.tile[tileTargetX, tileTargetY].type == 80)
                                {
                                    this.hitTile += this.inventory[this.selectedItem].axe * 3;
                                }
                                else
                                {
                                    this.hitTile += this.inventory[this.selectedItem].axe;
                                }
                                if (this.inventory[this.selectedItem].axe > 0)
                                {
                                    if (this.hitTile >= 100)
                                    {
                                        this.hitTile = 0;
                                        WorldGen.KillTile(tileTargetX, tileTargetY, false, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 0f, 0);
                                        }
                                    }
                                    else
                                    {
                                        WorldGen.KillTile(tileTargetX, tileTargetY, true, false, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                        }
                                    }
                                    this.itemTime = this.inventory[this.selectedItem].useTime;
                                }
                            }
                            else if (this.inventory[this.selectedItem].pick > 0)
                            {
                                if ((Main.tileDungeon[Main.tile[tileTargetX, tileTargetY].type] || (Main.tile[tileTargetX, tileTargetY].type == 0x25)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x19) || (Main.tile[tileTargetX, tileTargetY].type == 0x3a)))
                                {
                                    this.hitTile += this.inventory[this.selectedItem].pick / 2;
                                }
                                else
                                {
                                    this.hitTile += this.inventory[this.selectedItem].pick;
                                }
                                if ((Main.tile[tileTargetX, tileTargetY].type == 0x19) && (this.inventory[this.selectedItem].pick < 0x41))
                                {
                                    this.hitTile = 0;
                                }
                                else if ((Main.tile[tileTargetX, tileTargetY].type == 0x25) && (this.inventory[this.selectedItem].pick < 0x37))
                                {
                                    this.hitTile = 0;
                                }
                                else if (((Main.tile[tileTargetX, tileTargetY].type == 0x16) && (tileTargetY > Main.worldSurface)) && (this.inventory[this.selectedItem].pick < 0x37))
                                {
                                    this.hitTile = 0;
                                }
                                else if ((Main.tile[tileTargetX, tileTargetY].type == 0x38) && (this.inventory[this.selectedItem].pick < 0x41))
                                {
                                    this.hitTile = 0;
                                }
                                else if ((Main.tile[tileTargetX, tileTargetY].type == 0x3a) && (this.inventory[this.selectedItem].pick < 0x41))
                                {
                                    this.hitTile = 0;
                                }
                                else if ((Main.tileDungeon[Main.tile[tileTargetX, tileTargetY].type] && (this.inventory[this.selectedItem].pick < 0x41)) && ((tileTargetX < (Main.maxTilesX * 0.25)) || (tileTargetX > (Main.maxTilesX * 0.75))))
                                {
                                    this.hitTile = 0;
                                }
                                if (((Main.tile[tileTargetX, tileTargetY].type == 0) || (Main.tile[tileTargetX, tileTargetY].type == 40)) || ((Main.tile[tileTargetX, tileTargetY].type == 0x35) || (Main.tile[tileTargetX, tileTargetY].type == 0x3b)))
                                {
                                    this.hitTile += this.inventory[this.selectedItem].pick;
                                }
                                if (this.hitTile >= 100)
                                {
                                    this.hitTile = 0;
                                    WorldGen.KillTile(tileTargetX, tileTargetY, false, false, false);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 0f, 0);
                                    }
                                }
                                else
                                {
                                    WorldGen.KillTile(tileTargetX, tileTargetY, true, false, false);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(0x11, -1, -1, "", 0, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                    }
                                }
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                            }
                        }
                    }
                    if ((((Main.tile[tileTargetX, tileTargetY].wall > 0) && (this.itemTime == 0)) && ((this.itemAnimation > 0) && this.controlUseItem)) && (this.inventory[this.selectedItem].hammer > 0))
                    {
                        bool flag5 = true;
                        if (!Main.wallHouse[Main.tile[tileTargetX, tileTargetY].wall])
                        {
                            flag5 = false;
                            for (int num29 = tileTargetX - 1; num29 < (tileTargetX + 2); num29++)
                            {
                                for (int num30 = tileTargetY - 1; num30 < (tileTargetY + 2); num30++)
                                {
                                    if (Main.tile[num29, num30].wall != Main.tile[tileTargetX, tileTargetY].wall)
                                    {
                                        flag5 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag5)
                        {
                            if ((this.hitTileX != tileTargetX) || (this.hitTileY != tileTargetY))
                            {
                                this.hitTile = 0;
                                this.hitTileX = tileTargetX;
                                this.hitTileY = tileTargetY;
                            }
                            this.hitTile += this.inventory[this.selectedItem].hammer;
                            if (this.hitTile >= 100)
                            {
                                this.hitTile = 0;
                                WorldGen.KillWall(tileTargetX, tileTargetY, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 2, (float) tileTargetX, (float) tileTargetY, 0f, 0);
                                }
                            }
                            else
                            {
                                WorldGen.KillWall(tileTargetX, tileTargetY, true);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 2, (float) tileTargetX, (float) tileTargetY, 1f, 0);
                                }
                            }
                            this.itemTime = this.inventory[this.selectedItem].useTime;
                        }
                    }
                }
                if (((this.inventory[this.selectedItem].type == 0x1d) && (this.itemAnimation > 0)) && ((this.statLifeMax < 400) && (this.itemTime == 0)))
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    this.statLifeMax += 20;
                    this.statLife += 20;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.HealEffect(20);
                    }
                }
                if (((this.inventory[this.selectedItem].type == 0x6d) && (this.itemAnimation > 0)) && ((this.statManaMax < 200) && (this.itemTime == 0)))
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    this.statManaMax += 20;
                    this.statMana += 20;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.ManaEffect(20);
                    }
                }
                if ((((this.inventory[this.selectedItem].createTile >= 0) && ((((this.position.X / 16f) - tileRangeX) - this.inventory[this.selectedItem].tileBoost) <= tileTargetX)) && (((((((this.position.X + this.width) / 16f) + tileRangeX) + this.inventory[this.selectedItem].tileBoost) - 1f) >= tileTargetX) && ((((this.position.Y / 16f) - tileRangeY) - this.inventory[this.selectedItem].tileBoost) <= tileTargetY))) && ((((((this.position.Y + this.height) / 16f) + tileRangeY) + this.inventory[this.selectedItem].tileBoost) - 2f) >= tileTargetY))
                {
                    this.showItemIcon = true;
                    if (((!Main.tile[tileTargetX, tileTargetY].active || (this.inventory[this.selectedItem].createTile == 0x17)) || (((this.inventory[this.selectedItem].createTile == 2) || (this.inventory[this.selectedItem].createTile == 60)) || (this.inventory[this.selectedItem].createTile == 70))) && (((this.itemTime == 0) && (this.itemAnimation > 0)) && this.controlUseItem))
                    {
                        bool flag6 = false;
                        if ((this.inventory[this.selectedItem].createTile == 0x17) || (this.inventory[this.selectedItem].createTile == 2))
                        {
                            if (Main.tile[tileTargetX, tileTargetY].active && (Main.tile[tileTargetX, tileTargetY].type == 0))
                            {
                                flag6 = true;
                            }
                        }
                        else if ((this.inventory[this.selectedItem].createTile == 60) || (this.inventory[this.selectedItem].createTile == 70))
                        {
                            if (Main.tile[tileTargetX, tileTargetY].active && (Main.tile[tileTargetX, tileTargetY].type == 0x3b))
                            {
                                flag6 = true;
                            }
                        }
                        else if (this.inventory[this.selectedItem].createTile == 4)
                        {
                            int type = Main.tile[tileTargetX, tileTargetY + 1].type;
                            int index = Main.tile[tileTargetX - 1, tileTargetY].type;
                            int num33 = Main.tile[tileTargetX + 1, tileTargetY].type;
                            int num34 = Main.tile[tileTargetX - 1, tileTargetY - 1].type;
                            int num35 = Main.tile[tileTargetX + 1, tileTargetY - 1].type;
                            int num36 = Main.tile[tileTargetX - 1, tileTargetY - 1].type;
                            int num37 = Main.tile[tileTargetX + 1, tileTargetY + 1].type;
                            if (!Main.tile[tileTargetX, tileTargetY + 1].active)
                            {
                                type = -1;
                            }
                            if (!Main.tile[tileTargetX - 1, tileTargetY].active)
                            {
                                index = -1;
                            }
                            if (!Main.tile[tileTargetX + 1, tileTargetY].active)
                            {
                                num33 = -1;
                            }
                            if (!Main.tile[tileTargetX - 1, tileTargetY - 1].active)
                            {
                                num34 = -1;
                            }
                            if (!Main.tile[tileTargetX + 1, tileTargetY - 1].active)
                            {
                                num35 = -1;
                            }
                            if (!Main.tile[tileTargetX - 1, tileTargetY + 1].active)
                            {
                                num36 = -1;
                            }
                            if (!Main.tile[tileTargetX + 1, tileTargetY + 1].active)
                            {
                                num37 = -1;
                            }
                            if (((type >= 0) && Main.tileSolid[type]) && !Main.tileNoAttach[type])
                            {
                                flag6 = true;
                            }
                            else if ((((index >= 0) && Main.tileSolid[index]) && !Main.tileNoAttach[index]) || (((index == 5) && (num34 == 5)) && (num36 == 5)))
                            {
                                flag6 = true;
                            }
                            else if ((((num33 >= 0) && Main.tileSolid[num33]) && !Main.tileNoAttach[num33]) || (((num33 == 5) && (num35 == 5)) && (num37 == 5)))
                            {
                                flag6 = true;
                            }
                        }
                        else if (this.inventory[this.selectedItem].createTile == 0x4e)
                        {
                            if (Main.tile[tileTargetX, tileTargetY + 1].active && (Main.tileSolid[Main.tile[tileTargetX, tileTargetY + 1].type] || Main.tileTable[Main.tile[tileTargetX, tileTargetY + 1].type]))
                            {
                                flag6 = true;
                            }
                        }
                        else if (((this.inventory[this.selectedItem].createTile == 13) || (this.inventory[this.selectedItem].createTile == 0x1d)) || ((this.inventory[this.selectedItem].createTile == 0x21) || (this.inventory[this.selectedItem].createTile == 0x31)))
                        {
                            if (Main.tile[tileTargetX, tileTargetY + 1].active && Main.tileTable[Main.tile[tileTargetX, tileTargetY + 1].type])
                            {
                                flag6 = true;
                            }
                        }
                        else if (this.inventory[this.selectedItem].createTile == 0x33)
                        {
                            if (((Main.tile[tileTargetX + 1, tileTargetY].active || (Main.tile[tileTargetX + 1, tileTargetY].wall > 0)) || (Main.tile[tileTargetX - 1, tileTargetY].active || (Main.tile[tileTargetX - 1, tileTargetY].wall > 0))) || ((Main.tile[tileTargetX, tileTargetY + 1].active || (Main.tile[tileTargetX, tileTargetY + 1].wall > 0)) || (Main.tile[tileTargetX, tileTargetY - 1].active || (Main.tile[tileTargetX, tileTargetY - 1].wall > 0))))
                            {
                                flag6 = true;
                            }
                        }
                        else if (((Main.tile[tileTargetX + 1, tileTargetY].active && Main.tileSolid[Main.tile[tileTargetX + 1, tileTargetY].type]) || ((Main.tile[tileTargetX + 1, tileTargetY].wall > 0) || (Main.tile[tileTargetX - 1, tileTargetY].active && Main.tileSolid[Main.tile[tileTargetX - 1, tileTargetY].type]))) || ((((Main.tile[tileTargetX - 1, tileTargetY].wall > 0) || (Main.tile[tileTargetX, tileTargetY + 1].active && Main.tileSolid[Main.tile[tileTargetX, tileTargetY + 1].type])) || ((Main.tile[tileTargetX, tileTargetY + 1].wall > 0) || (Main.tile[tileTargetX, tileTargetY - 1].active && Main.tileSolid[Main.tile[tileTargetX, tileTargetY - 1].type]))) || (Main.tile[tileTargetX, tileTargetY - 1].wall > 0)))
                        {
                            flag6 = true;
                        }
                        if (Main.tileAlch[this.inventory[this.selectedItem].createTile])
                        {
                            flag6 = true;
                        }
                        if (flag6 && WorldGen.PlaceTile(tileTargetX, tileTargetY, this.inventory[this.selectedItem].createTile, false, false, this.whoAmi, this.inventory[this.selectedItem].placeStyle))
                        {
                            this.itemTime = this.inventory[this.selectedItem].useTime;
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(0x11, -1, -1, "", 1, (float) tileTargetX, (float) tileTargetY, (float) this.inventory[this.selectedItem].createTile, this.inventory[this.selectedItem].placeStyle);
                            }
                            if (this.inventory[this.selectedItem].createTile == 15)
                            {
                                if (this.direction == 1)
                                {
                                    Tile tile2 = Main.tile[tileTargetX, tileTargetY];
                                    tile2.frameX = (short) (tile2.frameX + 0x12);
                                    Tile tile3 = Main.tile[tileTargetX, tileTargetY - 1];
                                    tile3.frameX = (short) (tile3.frameX + 0x12);
                                }
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, tileTargetX - 1, tileTargetY - 1, 3);
                                }
                            }
                            else if ((this.inventory[this.selectedItem].createTile == 0x4f) && (Main.netMode == 1))
                            {
                                NetMessage.SendTileSquare(-1, tileTargetX, tileTargetY, 5);
                            }
                        }
                    }
                }
                if (this.inventory[this.selectedItem].createWall >= 0)
                {
                    tileTargetX = (int) ((Main.mouseState.X + Main.screenPosition.X) / 16f);
                    tileTargetY = (int) ((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    if ((((((this.position.X / 16f) - tileRangeX) - this.inventory[this.selectedItem].tileBoost) <= tileTargetX) && ((((((this.position.X + this.width) / 16f) + tileRangeX) + this.inventory[this.selectedItem].tileBoost) - 1f) >= tileTargetX)) && (((((this.position.Y / 16f) - tileRangeY) - this.inventory[this.selectedItem].tileBoost) <= tileTargetY) && ((((((this.position.Y + this.height) / 16f) + tileRangeY) + this.inventory[this.selectedItem].tileBoost) - 2f) >= tileTargetY)))
                    {
                        this.showItemIcon = true;
                        if (((((this.itemTime == 0) && (this.itemAnimation > 0)) && this.controlUseItem) && (((Main.tile[tileTargetX + 1, tileTargetY].active || (Main.tile[tileTargetX + 1, tileTargetY].wall > 0)) || (Main.tile[tileTargetX - 1, tileTargetY].active || (Main.tile[tileTargetX - 1, tileTargetY].wall > 0))) || ((Main.tile[tileTargetX, tileTargetY + 1].active || (Main.tile[tileTargetX, tileTargetY + 1].wall > 0)) || (Main.tile[tileTargetX, tileTargetY - 1].active || (Main.tile[tileTargetX, tileTargetY - 1].wall > 0))))) && (Main.tile[tileTargetX, tileTargetY].wall != this.inventory[this.selectedItem].createWall))
                        {
                            WorldGen.PlaceWall(tileTargetX, tileTargetY, this.inventory[this.selectedItem].createWall, false);
                            if (Main.tile[tileTargetX, tileTargetY].wall == this.inventory[this.selectedItem].createWall)
                            {
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 3, (float) tileTargetX, (float) tileTargetY, (float) this.inventory[this.selectedItem].createWall, 0);
                                }
                            }
                        }
                    }
                }
            }
            if (((this.inventory[this.selectedItem].damage >= 0) && (this.inventory[this.selectedItem].type > 0)) && (!this.inventory[this.selectedItem].noMelee && (this.itemAnimation > 0)))
            {
                bool flag7 = false;
                Rectangle rectangle = new Rectangle((int) this.itemLocation.X, (int) this.itemLocation.Y, 0x20, 0x20);
                if (!Main.dedServ)
                {
                    rectangle = new Rectangle((int) this.itemLocation.X, (int) this.itemLocation.Y, Main.itemTexture[this.inventory[this.selectedItem].type].Width, Main.itemTexture[this.inventory[this.selectedItem].type].Height);
                }
                rectangle.Width = (int) (rectangle.Width * this.inventory[this.selectedItem].scale);
                rectangle.Height = (int) (rectangle.Height * this.inventory[this.selectedItem].scale);
                if (this.direction == -1)
                {
                    rectangle.X -= rectangle.Width;
                }
                if (this.gravDir == 1f)
                {
                    rectangle.Y -= rectangle.Height;
                }
                if (this.inventory[this.selectedItem].useStyle == 1)
                {
                    if (this.itemAnimation < (this.itemAnimationMax * 0.333))
                    {
                        if (this.direction == -1)
                        {
                            rectangle.X -= ((int) (rectangle.Width * 1.4)) - rectangle.Width;
                        }
                        rectangle.Width = (int) (rectangle.Width * 1.4);
                        rectangle.Y += (int) ((rectangle.Height * 0.5) * this.gravDir);
                        rectangle.Height = (int) (rectangle.Height * 1.1);
                    }
                    else if (this.itemAnimation >= (this.itemAnimationMax * 0.666))
                    {
                        if (this.direction == 1)
                        {
                            rectangle.X -= (int) (rectangle.Width * 1.2);
                        }
                        rectangle.Width *= 2;
                        rectangle.Y -= (int) (((rectangle.Height * 1.4) - rectangle.Height) * this.gravDir);
                        rectangle.Height = (int) (rectangle.Height * 1.4);
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 3)
                {
                    if (this.itemAnimation > (this.itemAnimationMax * 0.666))
                    {
                        flag7 = true;
                    }
                    else
                    {
                        if (this.direction == -1)
                        {
                            rectangle.X -= ((int) (rectangle.Width * 1.4)) - rectangle.Width;
                        }
                        rectangle.Width = (int) (rectangle.Width * 1.4);
                        rectangle.Y += (int) (rectangle.Height * 0.6);
                        rectangle.Height = (int) (rectangle.Height * 0.6);
                    }
                }
                float gravDir = this.gravDir;
                if (!flag7)
                {
                    if ((((this.inventory[this.selectedItem].type == 0x2c) || (this.inventory[this.selectedItem].type == 0x2d)) || (((this.inventory[this.selectedItem].type == 0x2e) || (this.inventory[this.selectedItem].type == 0x67)) || (this.inventory[this.selectedItem].type == 0x68))) && (Main.rand.Next(15) == 0))
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 14, (float) (this.direction * 2), 0f, 150, color, 1.3f);
                    }
                    if (this.inventory[this.selectedItem].type == 0x111)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 14, (float) (this.direction * 2), 0f, 150, color, 1.4f);
                        }
                        color = new Color();
                        int num38 = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 0x1b, (this.velocity.X * 0.2f) + (this.direction * 3), this.velocity.Y * 0.2f, 100, color, 1.2f);
                        Main.dust[num38].noGravity = true;
                        Main.dust[num38].velocity.X /= 2f;
                        Main.dust[num38].velocity.Y /= 2f;
                    }
                    if (this.inventory[this.selectedItem].type == 0x41)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 15, 0f, 0f, 150, color, 1.2f);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            Gore.NewGore(new Vector2((float) rectangle.X, (float) rectangle.Y), new Vector2(), Main.rand.Next(0x10, 0x12));
                        }
                    }
                    if ((this.inventory[this.selectedItem].type == 190) || (this.inventory[this.selectedItem].type == 0xd5))
                    {
                        color = new Color();
                        int num39 = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 40, (this.velocity.X * 0.2f) + (this.direction * 3), this.velocity.Y * 0.2f, 0, color, 1.2f);
                        Main.dust[num39].noGravity = true;
                    }
                    if (this.inventory[this.selectedItem].type == 0x79)
                    {
                        for (int num40 = 0; num40 < 2; num40++)
                        {
                            color = new Color();
                            int num41 = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 6, (this.velocity.X * 0.2f) + (this.direction * 3), this.velocity.Y * 0.2f, 100, color, 2.5f);
                            Main.dust[num41].noGravity = true;
                            Main.dust[num41].velocity.X *= 2f;
                            Main.dust[num41].velocity.Y *= 2f;
                        }
                    }
                    if ((this.inventory[this.selectedItem].type == 0x7a) || (this.inventory[this.selectedItem].type == 0xd9))
                    {
                        color = new Color();
                        int num42 = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 6, (this.velocity.X * 0.2f) + (this.direction * 3), this.velocity.Y * 0.2f, 100, color, 1.9f);
                        Main.dust[num42].noGravity = true;
                    }
                    if (this.inventory[this.selectedItem].type == 0x9b)
                    {
                        color = new Color();
                        int num43 = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 0x1d, (this.velocity.X * 0.2f) + (this.direction * 3), this.velocity.Y * 0.2f, 100, color, 2f);
                        Main.dust[num43].noGravity = true;
                        Main.dust[num43].velocity.X /= 2f;
                        Main.dust[num43].velocity.Y /= 2f;
                    }
                    if ((this.inventory[this.selectedItem].type >= 0xc6) && (this.inventory[this.selectedItem].type <= 0xcb))
                    {
                        Lighting.addLight((int) (((this.itemLocation.X + 6f) + this.velocity.X) / 16f), (int) ((this.itemLocation.Y - 14f) / 16f), 0.5f);
                    }
                    if (Main.myPlayer == i)
                    {
                        int num44 = (int) (this.inventory[this.selectedItem].damage * this.meleeDamage);
                        float num45 = this.inventory[this.selectedItem].knockBack;
                        int num46 = rectangle.X / 0x10;
                        int num47 = ((rectangle.X + rectangle.Width) / 0x10) + 1;
                        int num48 = rectangle.Y / 0x10;
                        int num49 = ((rectangle.Y + rectangle.Height) / 0x10) + 1;
                        for (int num50 = num46; num50 < num47; num50++)
                        {
                            for (int num51 = num48; num51 < num49; num51++)
                            {
                                if (((Main.tile[num50, num51] != null) && Main.tileCut[Main.tile[num50, num51].type]) && ((Main.tile[num50, num51 + 1] != null) && (Main.tile[num50, num51 + 1].type != 0x4e)))
                                {
                                    WorldGen.KillTile(num50, num51, false, false, false);
                                    if (Main.netMode == 1)
                                    {
                                        NetMessage.SendData(0x11, -1, -1, "", 0, (float) num50, (float) num51, 0f, 0);
                                    }
                                }
                            }
                        }
                        for (int num52 = 0; num52 < 0x3e8; num52++)
                        {
                            if (((Main.npc[num52].active && (Main.npc[num52].immune[i] == 0)) && (this.attackCD == 0)) && (!Main.npc[num52].friendly || ((Main.npc[num52].type == 0x16) && this.killGuide)))
                            {
                                Rectangle rectangle2 = new Rectangle((int) Main.npc[num52].position.X, (int) Main.npc[num52].position.Y, Main.npc[num52].width, Main.npc[num52].height);
                                if (rectangle.Intersects(rectangle2) && (Main.npc[num52].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[num52].position, Main.npc[num52].width, Main.npc[num52].height)))
                                {
                                    Main.npc[num52].StrikeNPC(num44, num45, this.direction);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(0x1c, -1, -1, "", num52, (float) num44, num45, (float) this.direction, 0);
                                    }
                                    Main.npc[num52].immune[i] = this.itemAnimation;
                                    this.attackCD = (int) (this.itemAnimationMax * 0.33);
                                }
                            }
                        }
                        if (this.hostile)
                        {
                            for (int num53 = 0; num53 < 0xff; num53++)
                            {
                                if ((((num53 != i) && Main.player[num53].active) && (Main.player[num53].hostile && !Main.player[num53].immune)) && (!Main.player[num53].dead && ((Main.player[i].team == 0) || (Main.player[i].team != Main.player[num53].team))))
                                {
                                    Rectangle rectangle3 = new Rectangle((int) Main.player[num53].position.X, (int) Main.player[num53].position.Y, Main.player[num53].width, Main.player[num53].height);
                                    if (rectangle.Intersects(rectangle3) && Collision.CanHit(this.position, this.width, this.height, Main.player[num53].position, Main.player[num53].width, Main.player[num53].height))
                                    {
                                        Main.player[num53].Hurt(num44, this.direction, true, false, " was slain...");
                                        if (Main.netMode != 0)
                                        {
                                            NetMessage.SendData(0x1a, -1, -1, getDeathMessage(this.whoAmi, -1, -1, -1), num53, (float) this.direction, (float) this.inventory[this.selectedItem].damage, 1f, 0);
                                        }
                                        this.attackCD = (int) (this.itemAnimationMax * 0.33);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if ((this.itemTime == 0) && (this.itemAnimation > 0))
            {
                if (this.inventory[this.selectedItem].healLife > 0)
                {
                    this.statLife += this.inventory[this.selectedItem].healLife;
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.HealEffect(this.inventory[this.selectedItem].healLife);
                    }
                }
                if (this.inventory[this.selectedItem].healMana > 0)
                {
                    this.statMana += this.inventory[this.selectedItem].healMana;
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.ManaEffect(this.inventory[this.selectedItem].healMana);
                    }
                }
                if (this.inventory[this.selectedItem].buffType > 0)
                {
                    if (this.whoAmi == Main.myPlayer)
                    {
                        this.AddBuff(this.inventory[this.selectedItem].buffType, this.inventory[this.selectedItem].buffTime);
                    }
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                }
            }
            if (((this.itemTime == 0) && (this.itemAnimation > 0)) && ((this.inventory[this.selectedItem].type == 0x2b) || (this.inventory[this.selectedItem].type == 70)))
            {
                this.itemTime = this.inventory[this.selectedItem].useTime;
                bool flag8 = false;
                int num54 = 4;
                if (this.inventory[this.selectedItem].type == 0x2b)
                {
                    num54 = 4;
                }
                else if (this.inventory[this.selectedItem].type == 70)
                {
                    num54 = 13;
                }
                for (int num55 = 0; num55 < 0x3e8; num55++)
                {
                    if (Main.npc[num55].active && (Main.npc[num55].type == num54))
                    {
                        flag8 = true;
                        break;
                    }
                }
                if (flag8)
                {
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.Hurt(this.statLife * (this.statDefense + 1), -this.direction, false, false, getDeathMessage(-1, -1, -1, 3));
                    }
                }
                else if (this.inventory[this.selectedItem].type == 0x2b)
                {
                    if (!Main.dayTime)
                    {
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        if (Main.netMode != 1)
                        {
                            NPC.SpawnOnPlayer(i, 4);
                        }
                    }
                }
                else if ((this.inventory[this.selectedItem].type == 70) && this.zoneEvil)
                {
                    Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                    if (Main.netMode != 1)
                    {
                        NPC.SpawnOnPlayer(i, 13);
                    }
                }
            }
            if ((this.inventory[this.selectedItem].type == 50) && (this.itemAnimation > 0))
            {
                if (Main.rand.Next(2) == 0)
                {
                    color = new Color();
                    Dust.NewDust(this.position, this.width, this.height, 15, 0f, 0f, 150, color, 1.1f);
                }
                if (this.itemTime == 0)
                {
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                }
                else if (this.itemTime == (this.inventory[this.selectedItem].useTime / 2))
                {
                    for (int num56 = 0; num56 < 70; num56++)
                    {
                        color = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, color, 1.5f);
                    }
                    this.grappling[0] = -1;
                    this.grapCount = 0;
                    for (int num57 = 0; num57 < 0x3e8; num57++)
                    {
                        if ((Main.projectile[num57].active && (Main.projectile[num57].owner == i)) && (Main.projectile[num57].aiStyle == 7))
                        {
                            Main.projectile[num57].Kill();
                        }
                    }
                    this.Spawn();
                    for (int num58 = 0; num58 < 70; num58++)
                    {
                        color = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 15, 0f, 0f, 150, color, 1.5f);
                    }
                }
            }
            if (i == Main.myPlayer)
            {
                if ((this.itemTime == this.inventory[this.selectedItem].useTime) && this.inventory[this.selectedItem].consumable)
                {
                    Item item2 = this.inventory[this.selectedItem];
                    item2.stack--;
                    if (this.inventory[this.selectedItem].stack <= 0)
                    {
                        this.itemTime = this.itemAnimation;
                    }
                }
                if ((this.inventory[this.selectedItem].stack <= 0) && (this.itemAnimation == 0))
                {
                    this.inventory[this.selectedItem] = new Item();
                }
            }
        }

        public bool ItemSpace(Item newItem)
        {
            if (newItem.type == 0x3a)
            {
                return true;
            }
            if (newItem.type == 0xb8)
            {
                return true;
            }
            int num = 40;
            if (((newItem.type == 0x47) || (newItem.type == 0x48)) || ((newItem.type == 0x49) || (newItem.type == 0x4a)))
            {
                num = 0x2c;
            }
            for (int i = 0; i < num; i++)
            {
                if (this.inventory[i].type == 0)
                {
                    return true;
                }
            }
            for (int j = 0; j < num; j++)
            {
                if (((this.inventory[j].type > 0) && (this.inventory[j].stack < this.inventory[j].maxStack)) && newItem.IsTheSameAs(this.inventory[j]))
                {
                    return true;
                }
            }
            return false;
        }

        public void KillMe(double dmg, int hitDirection, bool pvp = false, string deathText = " was slain...")
        {
            if (!this.dead)
            {
                if (pvp)
                {
                    this.pvpDeath = true;
                }
                if (this.hardCore)
                {
                    if (Main.netMode != 1)
                    {
                        float num = Main.rand.Next(-35, 0x24) * 0.1f;
                        while ((num < 2f) && (num > -2f))
                        {
                            num += Main.rand.Next(-30, 0x1f) * 0.1f;
                        }
                        int index = Projectile.NewProjectile(this.position.X + (this.width / 2), this.position.Y + (this.head / 2), ((Main.rand.Next(10, 30) * 0.1f) * hitDirection) + num, Main.rand.Next(-40, -20) * 0.1f, 0x2b, this.statLifeMax + this.statManaMax, 0f, Main.myPlayer);
                        Main.projectile[index].miscText = this.name + deathText;
                    }
                    if (Main.myPlayer == this.whoAmi)
                    {
                        this.statLifeMax = 100;
                        this.statManaMax = 0;
                        this.DropItems();
                    }
                }
                Main.PlaySound(5, (int) this.position.X, (int) this.position.Y, 1);
                this.headVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                this.bodyVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                this.legVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                this.headVelocity.X = (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                this.bodyVelocity.X = (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                this.legVelocity.X = (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                for (int i = 0; i < (20.0 + ((dmg / ((double) this.statLifeMax)) * 100.0)); i++)
                {
                    if (this.boneArmor)
                    {
                        Color newColor = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) (2 * hitDirection), -2f, 0, newColor, 1f);
                    }
                    else
                    {
                        Color color2 = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                    }
                }
                this.dead = true;
                this.respawnTimer = 600;
                this.immuneAlpha = 0;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, this.name + deathText, 0xff, 225f, 25f, 25f, 0);
                }
                else if (Main.netMode == 0)
                {
                    Main.NewText(this.name + deathText, 0xe1, 0x19, 0x19);
                }
                if ((Main.netMode == 1) && (this.whoAmi == Main.myPlayer))
                {
                    int num4 = 0;
                    if (pvp)
                    {
                        num4 = 1;
                    }
                    NetMessage.SendData(0x2c, -1, -1, deathText, this.whoAmi, (float) hitDirection, (float) ((int) dmg), (float) num4, 0);
                }
                if ((!pvp && (this.whoAmi == Main.myPlayer)) && !this.hardCore)
                {
                    this.DropCoins();
                }
                if (this.whoAmi == Main.myPlayer)
                {
                    try
                    {
                        WorldGen.saveToonWhilePlaying();
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void KillMeForGood()
        {
            if (File.Exists(Main.playerPathName))
            {
                File.Delete(Main.playerPathName);
            }
            if (File.Exists(Main.playerPathName + ".bak"))
            {
                File.Delete(Main.playerPathName + ".bak");
            }
            if (File.Exists(Main.playerPathName + ".dat"))
            {
                File.Delete(Main.playerPathName + ".dat");
            }
            Main.playerPathName = "";
        }

        public static Player LoadPlayer(string playerPath)
        {
            bool flag = false;
            if (Main.rand == null)
            {
                Main.rand = new Random((int) DateTime.Now.Ticks);
            }
            Player player = new Player();
            try
            {
                string outputFile = playerPath + ".dat";
                flag = DecryptFile(playerPath, outputFile);
                if (!flag)
                {
                    using (FileStream stream = new FileStream(outputFile, FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            int release = reader.ReadInt32();
                            player.name = reader.ReadString();
                            if (release >= 10)
                            {
                                player.hardCore = reader.ReadBoolean();
                            }
                            player.hair = reader.ReadInt32();
                            player.statLife = reader.ReadInt32();
                            player.statLifeMax = reader.ReadInt32();
                            if (player.statLife > player.statLifeMax)
                            {
                                player.statLife = player.statLifeMax;
                            }
                            player.statMana = reader.ReadInt32();
                            player.statManaMax = reader.ReadInt32();
                            if (player.statMana > player.statManaMax)
                            {
                                player.statMana = player.statManaMax;
                            }
                            player.hairColor.R = reader.ReadByte();
                            player.hairColor.G = reader.ReadByte();
                            player.hairColor.B = reader.ReadByte();
                            player.skinColor.R = reader.ReadByte();
                            player.skinColor.G = reader.ReadByte();
                            player.skinColor.B = reader.ReadByte();
                            player.eyeColor.R = reader.ReadByte();
                            player.eyeColor.G = reader.ReadByte();
                            player.eyeColor.B = reader.ReadByte();
                            player.shirtColor.R = reader.ReadByte();
                            player.shirtColor.G = reader.ReadByte();
                            player.shirtColor.B = reader.ReadByte();
                            player.underShirtColor.R = reader.ReadByte();
                            player.underShirtColor.G = reader.ReadByte();
                            player.underShirtColor.B = reader.ReadByte();
                            player.pantsColor.R = reader.ReadByte();
                            player.pantsColor.G = reader.ReadByte();
                            player.pantsColor.B = reader.ReadByte();
                            player.shoeColor.R = reader.ReadByte();
                            player.shoeColor.G = reader.ReadByte();
                            player.shoeColor.B = reader.ReadByte();
                            Main.player[Main.myPlayer].shirtColor = player.shirtColor;
                            Main.player[Main.myPlayer].pantsColor = player.pantsColor;
                            Main.player[Main.myPlayer].hairColor = player.hairColor;
                            for (int i = 0; i < 8; i++)
                            {
                                player.armor[i].SetDefaults(Item.VersionName(reader.ReadString(), release));
                            }
                            if (release >= 6)
                            {
                                for (int n = 8; n < 11; n++)
                                {
                                    player.armor[n].SetDefaults(Item.VersionName(reader.ReadString(), release));
                                }
                            }
                            for (int j = 0; j < 0x2c; j++)
                            {
                                player.inventory[j].SetDefaults(Item.VersionName(reader.ReadString(), release));
                                player.inventory[j].stack = reader.ReadInt32();
                            }
                            for (int k = 0; k < Chest.maxItems; k++)
                            {
                                player.bank[k].SetDefaults(Item.VersionName(reader.ReadString(), release));
                                player.bank[k].stack = reader.ReadInt32();
                            }
                            if (release >= 11)
                            {
                                for (int num6 = 0; num6 < 10; num6++)
                                {
                                    player.buffType[num6] = reader.ReadInt32();
                                    player.buffTime[num6] = reader.ReadInt32();
                                }
                            }
                            for (int m = 0; m < 200; m++)
                            {
                                int num8 = reader.ReadInt32();
                                if (num8 == -1)
                                {
                                    break;
                                }
                                player.spX[m] = num8;
                                player.spY[m] = reader.ReadInt32();
                                player.spI[m] = reader.ReadInt32();
                                player.spN[m] = reader.ReadString();
                            }
                            reader.Close();
                        }
                    }
                    player.PlayerFrame();
                    File.Delete(outputFile);
                    return player;
                }
            }
            catch
            {
                flag = true;
            }
            if (flag)
            {
                string path = playerPath + ".bak";
                if (File.Exists(path))
                {
                    File.Delete(playerPath);
                    File.Move(path, playerPath);
                    return LoadPlayer(playerPath);
                }
            }
            return new Player();
        }

        public void ManaEffect(int manaAmount)
        {
            CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(180, 50, 0xff, 0xff), manaAmount.ToString());
            if ((Main.netMode == 1) && (this.whoAmi == Main.myPlayer))
            {
                NetMessage.SendData(0x2b, -1, -1, "", this.whoAmi, (float) manaAmount, 0f, 0f, 0);
            }
        }

        public void PlayerFrame()
        {
            if (this.swimTime > 0)
            {
                this.swimTime--;
                if (!this.wet)
                {
                    this.swimTime = 0;
                }
            }
            this.head = this.armor[0].headSlot;
            this.body = this.armor[1].bodySlot;
            this.legs = this.armor[2].legSlot;
            if (!this.hostile)
            {
                if (this.armor[8].headSlot >= 0)
                {
                    this.head = this.armor[8].headSlot;
                }
                if (this.armor[9].bodySlot >= 0)
                {
                    this.body = this.armor[9].bodySlot;
                }
                if (this.armor[10].legSlot >= 0)
                {
                    this.legs = this.armor[10].legSlot;
                }
            }
            this.socialShadow = false;
            if (((this.head == 5) && (this.body == 5)) && (this.legs == 5))
            {
                this.socialShadow = true;
            }
            if (((this.head == 5) && (this.body == 5)) && ((this.legs == 5) && (Main.rand.Next(10) == 0)))
            {
                Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 200, new Color(), 1.2f);
            }
            if ((((this.head == 6) && (this.body == 6)) && ((this.legs == 6) && ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) > 1f))) && !this.rocketFrame)
            {
                for (int i = 0; i < 2; i++)
                {
                    Color newColor = new Color();
                    int index = Dust.NewDust(new Vector2(this.position.X - (this.velocity.X * 2f), (this.position.Y - 2f) - (this.velocity.Y * 2f)), this.width, this.height, 6, 0f, 0f, 100, newColor, 2f);
                    Main.dust[index].noGravity = true;
                    Main.dust[index].velocity.X -= this.velocity.X * 0.5f;
                    Main.dust[index].velocity.Y -= this.velocity.Y * 0.5f;
                }
            }
            if (((this.head == 7) && (this.body == 7)) && (this.legs == 7))
            {
                this.boneArmor = true;
            }
            if (((this.head == 8) && (this.body == 8)) && ((this.legs == 8) && ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) > 1f)))
            {
                int num3 = Dust.NewDust(new Vector2(this.position.X - (this.velocity.X * 2f), (this.position.Y - 2f) - (this.velocity.Y * 2f)), this.width, this.height, 40, 0f, 0f, 50, new Color(), 1.4f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].velocity.X = this.velocity.X * 0.25f;
                Main.dust[num3].velocity.Y = this.velocity.Y * 0.25f;
            }
            if ((((this.head == 9) && (this.body == 9)) && ((this.legs == 9) && ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) > 1f))) && !this.rocketFrame)
            {
                for (int j = 0; j < 2; j++)
                {
                    Color color4 = new Color();
                    int num5 = Dust.NewDust(new Vector2(this.position.X - (this.velocity.X * 2f), (this.position.Y - 2f) - (this.velocity.Y * 2f)), this.width, this.height, 6, 0f, 0f, 100, color4, 2f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity.X -= this.velocity.X * 0.5f;
                    Main.dust[num5].velocity.Y -= this.velocity.Y * 0.5f;
                }
            }
            this.bodyFrame.Width = 40;
            this.bodyFrame.Height = 0x38;
            this.legFrame.Width = 40;
            this.legFrame.Height = 0x38;
            this.bodyFrame.X = 0;
            this.legFrame.X = 0;
            if ((this.itemAnimation > 0) && (this.inventory[this.selectedItem].useStyle != 10))
            {
                if ((this.inventory[this.selectedItem].useStyle == 1) || (this.inventory[this.selectedItem].type == 0))
                {
                    if (this.itemAnimation < (this.itemAnimationMax * 0.333))
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                    }
                    else if (this.itemAnimation < (this.itemAnimationMax * 0.666))
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    }
                    else
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height;
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 2)
                {
                    if (this.itemAnimation > (this.itemAnimationMax * 0.5))
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                    }
                    else
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 3)
                {
                    if (this.itemAnimation > (this.itemAnimationMax * 0.666))
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                    }
                    else
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 4)
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 2;
                }
                else if (this.inventory[this.selectedItem].useStyle == 5)
                {
                    if (this.inventory[this.selectedItem].type == 0x119)
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    }
                    else
                    {
                        float num6 = this.itemRotation * this.direction;
                        this.bodyFrame.Y = this.bodyFrame.Height * 3;
                        if (num6 < -0.75)
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height * 2;
                            if (this.gravDir == -1f)
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 4;
                            }
                        }
                        if (num6 > 0.6)
                        {
                            this.bodyFrame.Y = this.bodyFrame.Height * 4;
                            if (this.gravDir == -1f)
                            {
                                this.bodyFrame.Y = this.bodyFrame.Height * 2;
                            }
                        }
                    }
                }
            }
            else if ((this.inventory[this.selectedItem].holdStyle == 1) && (!this.wet || !this.inventory[this.selectedItem].noWet))
            {
                this.bodyFrame.Y = this.bodyFrame.Height * 3;
            }
            else if ((this.inventory[this.selectedItem].holdStyle == 2) && (!this.wet || !this.inventory[this.selectedItem].noWet))
            {
                this.bodyFrame.Y = this.bodyFrame.Height * 2;
            }
            else if (this.grappling[0] >= 0)
            {
                Vector2 vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                float num7 = 0f;
                float num8 = 0f;
                for (int k = 0; k < this.grapCount; k++)
                {
                    num7 += Main.projectile[this.grappling[k]].position.X + (Main.projectile[this.grappling[k]].width / 2);
                    num8 += Main.projectile[this.grappling[k]].position.Y + (Main.projectile[this.grappling[k]].height / 2);
                }
                num7 /= (float) this.grapCount;
                num8 /= (float) this.grapCount;
                num7 -= vector.X;
                num8 -= vector.Y;
                if ((num8 < 0f) && (Math.Abs(num8) > Math.Abs(num7)))
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    if (this.gravDir == -1f)
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 4;
                    }
                }
                else if ((num8 > 0f) && (Math.Abs(num8) > Math.Abs(num7)))
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 4;
                    if (this.gravDir == -1f)
                    {
                        this.bodyFrame.Y = this.bodyFrame.Height * 2;
                    }
                }
                else
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 3;
                }
            }
            else if (this.swimTime > 0)
            {
                if (this.swimTime > 20)
                {
                    this.bodyFrame.Y = 0;
                }
                else if (this.swimTime > 10)
                {
                    this.bodyFrame.Y = this.bodyFrame.Height * 5;
                }
                else
                {
                    this.bodyFrame.Y = 0;
                }
            }
            else if (this.velocity.Y != 0f)
            {
                this.bodyFrameCounter = 0.0;
                this.bodyFrame.Y = this.bodyFrame.Height * 5;
            }
            else if (this.velocity.X != 0f)
            {
                this.bodyFrameCounter += Math.Abs(this.velocity.X) * 1.5;
                this.bodyFrame.Y = this.legFrame.Y;
            }
            else
            {
                this.bodyFrameCounter = 0.0;
                this.bodyFrame.Y = 0;
            }
            if (this.swimTime > 0)
            {
                this.legFrameCounter += 2.0;
                while (this.legFrameCounter > 8.0)
                {
                    this.legFrameCounter -= 8.0;
                    this.legFrame.Y += this.legFrame.Height;
                }
                if (this.legFrame.Y < (this.legFrame.Height * 7))
                {
                    this.legFrame.Y = this.legFrame.Height * 0x13;
                }
                else if (this.legFrame.Y > (this.legFrame.Height * 0x13))
                {
                    this.legFrame.Y = this.legFrame.Height * 7;
                }
            }
            else if ((this.velocity.Y != 0f) || (this.grappling[0] > -1))
            {
                this.legFrameCounter = 0.0;
                this.legFrame.Y = this.legFrame.Height * 5;
            }
            else if (this.velocity.X != 0f)
            {
                this.legFrameCounter += Math.Abs(this.velocity.X) * 1.3;
                while (this.legFrameCounter > 8.0)
                {
                    this.legFrameCounter -= 8.0;
                    this.legFrame.Y += this.legFrame.Height;
                }
                if (this.legFrame.Y < (this.legFrame.Height * 7))
                {
                    this.legFrame.Y = this.legFrame.Height * 0x13;
                }
                else if (this.legFrame.Y > (this.legFrame.Height * 0x13))
                {
                    this.legFrame.Y = this.legFrame.Height * 7;
                }
            }
            else
            {
                this.legFrameCounter = 0.0;
                this.legFrame.Y = 0;
            }
        }

        public void QuickBuff()
        {
            int style = 0;
            for (int i = 0; i < 0x2c; i++)
            {
                if (this.countBuffs() == 10)
                {
                    return;
                }
                if (((this.inventory[i].stack > 0) && (this.inventory[i].type > 0)) && (this.inventory[i].buffType > 0))
                {
                    bool flag = true;
                    for (int j = 0; j < 10; j++)
                    {
                        if (this.buffType[j] == this.inventory[i].buffType)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        style = this.inventory[i].useSound;
                        this.AddBuff(this.inventory[i].buffType, this.inventory[i].buffTime);
                        Item item1 = this.inventory[i];
                        item1.stack--;
                        if (this.inventory[i].stack <= 0)
                        {
                            this.inventory[i].type = 0;
                            this.inventory[i].name = "";
                        }
                    }
                }
            }
            if (style > 0)
            {
                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, style);
                Recipe.FindRecipes();
            }
        }

        public void QuickHeal()
        {
            if ((this.statLife != this.statLifeMax) && (this.potionDelay <= 0))
            {
                for (int i = 0; i < 0x2c; i++)
                {
                    if (((this.inventory[i].stack > 0) && (this.inventory[i].type > 0)) && (this.inventory[i].potion && (this.inventory[i].healLife > 0)))
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, this.inventory[i].useSound);
                        this.potionDelay = Item.potionDelay;
                        this.statLife += this.inventory[i].healLife;
                        if (this.statMana > this.statManaMax)
                        {
                            this.statMana = this.statManaMax;
                        }
                        if (Main.myPlayer == this.whoAmi)
                        {
                            this.HealEffect(this.inventory[i].healLife);
                        }
                        Item item1 = this.inventory[i];
                        item1.stack--;
                        if (this.inventory[i].stack <= 0)
                        {
                            this.inventory[i].type = 0;
                            this.inventory[i].name = "";
                        }
                        Recipe.FindRecipes();
                        return;
                    }
                }
            }
        }

        public void QuickMana()
        {
            if ((this.statMana != this.statManaMax) && (this.potionDelay <= 0))
            {
                for (int i = 0; i < 0x2c; i++)
                {
                    if (((this.inventory[i].stack > 0) && (this.inventory[i].type > 0)) && (this.inventory[i].potion && (this.inventory[i].healMana > 0)))
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, this.inventory[i].useSound);
                        this.potionDelay = Item.potionDelay;
                        this.statMana += this.inventory[i].healMana;
                        if (this.statMana > this.statManaMax)
                        {
                            this.statMana = this.statManaMax;
                        }
                        if (Main.myPlayer == this.whoAmi)
                        {
                            this.ManaEffect(this.inventory[i].healMana);
                        }
                        Item item1 = this.inventory[i];
                        item1.stack--;
                        if (this.inventory[i].stack <= 0)
                        {
                            this.inventory[i].type = 0;
                            this.inventory[i].name = "";
                        }
                        Recipe.FindRecipes();
                        return;
                    }
                }
            }
        }

        public static void SavePlayer(Player newPlayer, string playerPath)
        {
            try
            {
                Directory.CreateDirectory(Main.PlayerPath);
            }
            catch
            {
            }
            if ((playerPath != null) && (playerPath != ""))
            {
                string destFileName = playerPath + ".bak";
                if (File.Exists(playerPath))
                {
                    File.Copy(playerPath, destFileName, true);
                }
                string path = playerPath + ".dat";
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(Main.curRelease);
                        writer.Write(newPlayer.name);
                        writer.Write(newPlayer.hardCore);
                        writer.Write(newPlayer.hair);
                        writer.Write(newPlayer.statLife);
                        writer.Write(newPlayer.statLifeMax);
                        writer.Write(newPlayer.statMana);
                        writer.Write(newPlayer.statManaMax);
                        writer.Write(newPlayer.hairColor.R);
                        writer.Write(newPlayer.hairColor.G);
                        writer.Write(newPlayer.hairColor.B);
                        writer.Write(newPlayer.skinColor.R);
                        writer.Write(newPlayer.skinColor.G);
                        writer.Write(newPlayer.skinColor.B);
                        writer.Write(newPlayer.eyeColor.R);
                        writer.Write(newPlayer.eyeColor.G);
                        writer.Write(newPlayer.eyeColor.B);
                        writer.Write(newPlayer.shirtColor.R);
                        writer.Write(newPlayer.shirtColor.G);
                        writer.Write(newPlayer.shirtColor.B);
                        writer.Write(newPlayer.underShirtColor.R);
                        writer.Write(newPlayer.underShirtColor.G);
                        writer.Write(newPlayer.underShirtColor.B);
                        writer.Write(newPlayer.pantsColor.R);
                        writer.Write(newPlayer.pantsColor.G);
                        writer.Write(newPlayer.pantsColor.B);
                        writer.Write(newPlayer.shoeColor.R);
                        writer.Write(newPlayer.shoeColor.G);
                        writer.Write(newPlayer.shoeColor.B);
                        for (int i = 0; i < 11; i++)
                        {
                            if (newPlayer.armor[i].name == null)
                            {
                                newPlayer.armor[i].name = "";
                            }
                            writer.Write(newPlayer.armor[i].name);
                        }
                        for (int j = 0; j < 0x2c; j++)
                        {
                            if (newPlayer.inventory[j].name == null)
                            {
                                newPlayer.inventory[j].name = "";
                            }
                            writer.Write(newPlayer.inventory[j].name);
                            writer.Write(newPlayer.inventory[j].stack);
                        }
                        for (int k = 0; k < Chest.maxItems; k++)
                        {
                            if (newPlayer.bank[k].name == null)
                            {
                                newPlayer.bank[k].name = "";
                            }
                            writer.Write(newPlayer.bank[k].name);
                            writer.Write(newPlayer.bank[k].stack);
                        }
                        for (int m = 0; m < 10; m++)
                        {
                            writer.Write(newPlayer.buffType[m]);
                            writer.Write(newPlayer.buffTime[m]);
                        }
                        for (int n = 0; n < 200; n++)
                        {
                            if (newPlayer.spN[n] == null)
                            {
                                writer.Write(-1);
                                break;
                            }
                            writer.Write(newPlayer.spX[n]);
                            writer.Write(newPlayer.spY[n]);
                            writer.Write(newPlayer.spI[n]);
                            writer.Write(newPlayer.spN[n]);
                        }
                        writer.Close();
                    }
                }
                EncryptFile(path, playerPath);
                File.Delete(path);
            }
        }

        public bool SellItem(int price)
        {
            if (price > 0)
            {
                Item[] itemArray = new Item[0x2c];
                for (int i = 0; i < 0x2c; i++)
                {
                    itemArray[i] = new Item();
                    itemArray[i] = (Item) this.inventory[i].Clone();
                }
                int num2 = price / 5;
                if (num2 < 1)
                {
                    num2 = 1;
                }
                bool flag = false;
                while ((num2 >= 0xf4240) && !flag)
                {
                    int index = -1;
                    for (int k = 0x2b; k >= 0; k--)
                    {
                        if ((index == -1) && ((this.inventory[k].type == 0) || (this.inventory[k].stack == 0)))
                        {
                            index = k;
                        }
                        while (((this.inventory[k].type == 0x4a) && (this.inventory[k].stack < this.inventory[k].maxStack)) && (num2 >= 0xf4240))
                        {
                            Item item1 = this.inventory[k];
                            item1.stack++;
                            num2 -= 0xf4240;
                            this.DoCoins(k);
                            if ((this.inventory[k].stack == 0) && (index == -1))
                            {
                                index = k;
                            }
                        }
                    }
                    if (num2 >= 0xf4240)
                    {
                        if (index == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[index].SetDefaults(0x4a, false);
                            num2 -= 0xf4240;
                        }
                    }
                }
                while ((num2 >= 0x2710) && !flag)
                {
                    int num5 = -1;
                    for (int m = 0x2b; m >= 0; m--)
                    {
                        if ((num5 == -1) && ((this.inventory[m].type == 0) || (this.inventory[m].stack == 0)))
                        {
                            num5 = m;
                        }
                        while (((this.inventory[m].type == 0x49) && (this.inventory[m].stack < this.inventory[m].maxStack)) && (num2 >= 0x2710))
                        {
                            Item item2 = this.inventory[m];
                            item2.stack++;
                            num2 -= 0x2710;
                            this.DoCoins(m);
                            if ((this.inventory[m].stack == 0) && (num5 == -1))
                            {
                                num5 = m;
                            }
                        }
                    }
                    if (num2 >= 0x2710)
                    {
                        if (num5 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num5].SetDefaults(0x49, false);
                            num2 -= 0x2710;
                        }
                    }
                }
                while ((num2 >= 100) && !flag)
                {
                    int num7 = -1;
                    for (int n = 0x2b; n >= 0; n--)
                    {
                        if ((num7 == -1) && ((this.inventory[n].type == 0) || (this.inventory[n].stack == 0)))
                        {
                            num7 = n;
                        }
                        while (((this.inventory[n].type == 0x48) && (this.inventory[n].stack < this.inventory[n].maxStack)) && (num2 >= 100))
                        {
                            Item item3 = this.inventory[n];
                            item3.stack++;
                            num2 -= 100;
                            this.DoCoins(n);
                            if ((this.inventory[n].stack == 0) && (num7 == -1))
                            {
                                num7 = n;
                            }
                        }
                    }
                    if (num2 >= 100)
                    {
                        if (num7 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num7].SetDefaults(0x48, false);
                            num2 -= 100;
                        }
                    }
                }
                while ((num2 >= 1) && !flag)
                {
                    int num9 = -1;
                    for (int num10 = 0x2b; num10 >= 0; num10--)
                    {
                        if ((num9 == -1) && ((this.inventory[num10].type == 0) || (this.inventory[num10].stack == 0)))
                        {
                            num9 = num10;
                        }
                        while (((this.inventory[num10].type == 0x47) && (this.inventory[num10].stack < this.inventory[num10].maxStack)) && (num2 >= 1))
                        {
                            Item item4 = this.inventory[num10];
                            item4.stack++;
                            num2--;
                            this.DoCoins(num10);
                            if ((this.inventory[num10].stack == 0) && (num9 == -1))
                            {
                                num9 = num10;
                            }
                        }
                    }
                    if (num2 >= 1)
                    {
                        if (num9 == -1)
                        {
                            flag = true;
                        }
                        else
                        {
                            this.inventory[num9].SetDefaults(0x47, false);
                            num2--;
                        }
                    }
                }
                if (!flag)
                {
                    return true;
                }
                for (int j = 0; j < 0x2c; j++)
                {
                    this.inventory[j] = (Item) itemArray[j].Clone();
                }
            }
            return false;
        }

        public void Spawn()
        {
            if (this.whoAmi == Main.myPlayer)
            {
                this.FindSpawn();
                if (!CheckSpawn(this.SpawnX, this.SpawnY))
                {
                    this.SpawnX = -1;
                    this.SpawnY = -1;
                }
            }
            if ((Main.netMode == 1) && (this.whoAmi == Main.myPlayer))
            {
                NetMessage.SendData(12, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                Main.gameMenu = false;
            }
            this.headPosition = new Vector2();
            this.bodyPosition = new Vector2();
            this.legPosition = new Vector2();
            this.headRotation = 0f;
            this.bodyRotation = 0f;
            this.legRotation = 0f;
            if (this.statLife <= 0)
            {
                this.statLife = 100;
                this.breath = this.breathMax;
                if (this.spawnMax)
                {
                    this.statLife = this.statLifeMax;
                    this.statMana = this.statManaMax;
                }
            }
            this.immune = true;
            this.dead = false;
            this.immuneTime = 0;
            this.active = true;
            if ((this.SpawnX >= 0) && (this.SpawnY >= 0))
            {
                this.position.X = ((this.SpawnX * 0x10) + 8) - (this.width / 2);
                this.position.Y = (this.SpawnY * 0x10) - this.height;
            }
            else
            {
                this.position.X = ((Main.spawnTileX * 0x10) + 8) - (this.width / 2);
                this.position.Y = (Main.spawnTileY * 0x10) - this.height;
                for (int i = Main.spawnTileX - 1; i < (Main.spawnTileX + 2); i++)
                {
                    for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
                    {
                        if (Main.tileSolid[Main.tile[i, j].type] && !Main.tileSolidTop[Main.tile[i, j].type])
                        {
                            WorldGen.KillTile(i, j, false, false, false);
                        }
                        if (Main.tile[i, j].liquid > 0)
                        {
                            Main.tile[i, j].lava = false;
                            Main.tile[i, j].liquid = 0;
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                }
            }
            this.wet = false;
            this.wetCount = 0;
            this.lavaWet = false;
            this.fallStart = (int) (this.position.Y / 16f);
            this.velocity.X = 0f;
            this.velocity.Y = 0f;
            this.talkNPC = -1;
            if (this.pvpDeath)
            {
                this.pvpDeath = false;
                this.immuneTime = 300;
                this.statLife = this.statLifeMax;
            }
            else
            {
                this.immuneTime = 60;
            }
            if (this.whoAmi == Main.myPlayer)
            {
                Lighting.lightCounter = Lighting.lightSkip + 1;
                Main.screenPosition.X = (this.position.X + (this.width / 2)) - (Main.screenWidth / 2);
                Main.screenPosition.Y = (this.position.Y + (this.height / 2)) - (Main.screenHeight / 2);
            }
        }

        public void toggleInv()
        {
            if (this.talkNPC >= 0)
            {
                this.talkNPC = -1;
                Main.npcChatText = "";
                Main.PlaySound(11, -1, -1, 1);
            }
            else if (this.sign >= 0)
            {
                this.sign = -1;
                Main.editSign = false;
                Main.npcChatText = "";
                Main.PlaySound(11, -1, -1, 1);
            }
            else if (!Main.playerInventory)
            {
                Recipe.FindRecipes();
                Main.playerInventory = true;
                Main.PlaySound(10, -1, -1, 1);
            }
            else
            {
                Main.playerInventory = false;
                Main.PlaySound(11, -1, -1, 1);
            }
        }

        public void UpdatePlayer(int i)
        {
            Color color;
            float num = 10f;
            float num2 = 0.4f;
            jumpHeight = 15;
            jumpSpeed = 5.01f;
            if (this.wet)
            {
                num2 = 0.2f;
                num = 5f;
                jumpHeight = 30;
                jumpSpeed = 6.01f;
            }
            float num3 = 3f;
            float num4 = 0.08f;
            float num5 = 0.2f;
            float num6 = num3;
            this.heldProj = -1;
            if (!this.active)
            {
                return;
            }
            this.shadowCount++;
            if (this.shadowCount == 1)
            {
                this.shadowPos[2] = this.shadowPos[1];
            }
            else if (this.shadowCount == 2)
            {
                this.shadowPos[1] = this.shadowPos[0];
            }
            else if (this.shadowCount >= 3)
            {
                this.shadowCount = 0;
                this.shadowPos[0] = this.position;
            }
            this.whoAmi = i;
            if (this.runSoundDelay > 0)
            {
                this.runSoundDelay--;
            }
            if (this.attackCD > 0)
            {
                this.attackCD--;
            }
            if (this.itemAnimation == 0)
            {
                this.attackCD = 0;
            }
            if (this.chatShowTime > 0)
            {
                this.chatShowTime--;
            }
            if (this.potionDelay > 0)
            {
                this.potionDelay--;
            }
            if (this.dead)
            {
                this.gravDir = 1f;
                for (int num7 = 0; num7 < 10; num7++)
                {
                    this.buffTime[num7] = 0;
                    this.buffType[num7] = 0;
                }
                if (i == Main.myPlayer)
                {
                    Main.npcChatText = "";
                    Main.editSign = false;
                }
                this.sign = -1;
                this.talkNPC = -1;
                this.statLife = 0;
                this.channel = false;
                this.potionDelay = 0;
                this.chest = -1;
                this.changeItem = -1;
                this.itemAnimation = 0;
                this.immuneAlpha += 2;
                if (this.immuneAlpha > 0xff)
                {
                    this.immuneAlpha = 0xff;
                }
                this.headPosition += this.headVelocity;
                this.bodyPosition += this.bodyVelocity;
                this.legPosition += this.legVelocity;
                this.headRotation += this.headVelocity.X * 0.1f;
                this.bodyRotation += this.bodyVelocity.X * 0.1f;
                this.legRotation += this.legVelocity.X * 0.1f;
                this.headVelocity.Y += 0.1f;
                this.bodyVelocity.Y += 0.1f;
                this.legVelocity.Y += 0.1f;
                this.headVelocity.X *= 0.99f;
                this.bodyVelocity.X *= 0.99f;
                this.legVelocity.X *= 0.99f;
                this.respawnTimer--;
                if ((this.respawnTimer <= 0) && (Main.myPlayer == this.whoAmi))
                {
                    this.Spawn();
                    return;
                }
                return;
            }
            if (i == Main.myPlayer)
            {
                this.zoneEvil = false;
                if (Main.evilTiles >= 500)
                {
                    this.zoneEvil = true;
                }
                this.zoneMeteor = false;
                if (Main.meteorTiles >= 50)
                {
                    this.zoneMeteor = true;
                }
                this.zoneDungeon = false;
                if ((Main.dungeonTiles >= 250) && (this.position.Y > (Main.worldSurface * 16.0)))
                {
                    int num8 = ((int) this.position.X) / 0x10;
                    int num9 = ((int) this.position.Y) / 0x10;
                    if ((Main.tile[num8, num9].wall > 0) && !Main.wallHouse[Main.tile[num8, num9].wall])
                    {
                        this.zoneDungeon = true;
                    }
                }
                this.zoneJungle = false;
                if (Main.jungleTiles >= 150)
                {
                    this.zoneJungle = true;
                }
                this.controlUp = false;
                this.controlLeft = false;
                this.controlDown = false;
                this.controlRight = false;
                this.controlJump = false;
                this.controlUseItem = false;
                this.controlUseTile = false;
                this.controlThrow = false;
                this.controlInv = false;
                if (Main.hasFocus)
                {
                    if (!Main.chatMode && !Main.editSign)
                    {
                        Keys[] pressedKeys = Main.keyState.GetPressedKeys();
                        for (int num10 = 0; num10 < pressedKeys.Length; num10++)
                        {
                            string str = pressedKeys[num10].ToString();
                            if (str == Main.cUp)
                            {
                                this.controlUp = true;
                            }
                            if (str == Main.cLeft)
                            {
                                this.controlLeft = true;
                            }
                            if (str == Main.cDown)
                            {
                                this.controlDown = true;
                            }
                            if (str == Main.cRight)
                            {
                                this.controlRight = true;
                            }
                            if (str == Main.cJump)
                            {
                                this.controlJump = true;
                            }
                            if (str == Main.cThrowItem)
                            {
                                this.controlThrow = true;
                            }
                            if (str == Main.cInv)
                            {
                                this.controlInv = true;
                            }
                            if (str == Main.cBuff)
                            {
                                this.QuickBuff();
                            }
                            if (str == Main.cHeal)
                            {
                                this.QuickHeal();
                            }
                            if (str == Main.cMana)
                            {
                                this.QuickMana();
                            }
                        }
                        if (this.controlLeft && this.controlRight)
                        {
                            this.controlLeft = false;
                            this.controlRight = false;
                        }
                    }
                    if ((Main.mouseState.LeftButton == ButtonState.Pressed) && !this.mouseInterface)
                    {
                        this.controlUseItem = true;
                    }
                    if ((Main.mouseState.RightButton == ButtonState.Pressed) && !this.mouseInterface)
                    {
                        this.controlUseTile = true;
                    }
                    if (this.controlInv)
                    {
                        if (this.releaseInventory)
                        {
                            this.toggleInv();
                        }
                        this.releaseInventory = false;
                    }
                    else
                    {
                        this.releaseInventory = true;
                    }
                    if (this.delayUseItem)
                    {
                        if (!this.controlUseItem)
                        {
                            this.delayUseItem = false;
                        }
                        this.controlUseItem = false;
                    }
                    if ((this.itemAnimation == 0) && (this.itemTime == 0))
                    {
                        this.dropItemCheck();
                        if (!Main.playerInventory)
                        {
                            int selectedItem = this.selectedItem;
                            if (!Main.chatMode)
                            {
                                if (Main.keyState.IsKeyDown(Keys.D1))
                                {
                                    this.selectedItem = 0;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D2))
                                {
                                    this.selectedItem = 1;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D3))
                                {
                                    this.selectedItem = 2;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D4))
                                {
                                    this.selectedItem = 3;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D5))
                                {
                                    this.selectedItem = 4;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D6))
                                {
                                    this.selectedItem = 5;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D7))
                                {
                                    this.selectedItem = 6;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D8))
                                {
                                    this.selectedItem = 7;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D9))
                                {
                                    this.selectedItem = 8;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D0))
                                {
                                    this.selectedItem = 9;
                                }
                            }
                            if (selectedItem != this.selectedItem)
                            {
                                Main.PlaySound(12, -1, -1, 1);
                            }
                            int num12 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
                            while (num12 > 9)
                            {
                                num12 -= 10;
                            }
                            while (num12 < 0)
                            {
                                num12 += 10;
                            }
                            this.selectedItem -= num12;
                            if (num12 != 0)
                            {
                                Main.PlaySound(12, -1, -1, 1);
                            }
                            if (this.changeItem >= 0)
                            {
                                if (this.selectedItem != this.changeItem)
                                {
                                    Main.PlaySound(12, -1, -1, 1);
                                }
                                this.selectedItem = this.changeItem;
                                this.changeItem = -1;
                            }
                            while (this.selectedItem > 9)
                            {
                                this.selectedItem -= 10;
                            }
                            while (this.selectedItem < 0)
                            {
                                this.selectedItem += 10;
                            }
                        }
                        else
                        {
                            int num13 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
                            Main.focusRecipe += num13;
                            if (Main.focusRecipe > (Main.numAvailableRecipes - 1))
                            {
                                Main.focusRecipe = Main.numAvailableRecipes - 1;
                            }
                            if (Main.focusRecipe < 0)
                            {
                                Main.focusRecipe = 0;
                            }
                        }
                    }
                }
                if (Main.netMode == 1)
                {
                    bool flag = false;
                    if ((this.statLife != Main.clientPlayer.statLife) || (this.statLifeMax != Main.clientPlayer.statLifeMax))
                    {
                        NetMessage.SendData(0x10, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                        flag = true;
                    }
                    if ((this.statMana != Main.clientPlayer.statMana) || (this.statManaMax != Main.clientPlayer.statManaMax))
                    {
                        NetMessage.SendData(0x2a, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                        flag = true;
                    }
                    if (this.controlUp != Main.clientPlayer.controlUp)
                    {
                        flag = true;
                    }
                    if (this.controlDown != Main.clientPlayer.controlDown)
                    {
                        flag = true;
                    }
                    if (this.controlLeft != Main.clientPlayer.controlLeft)
                    {
                        flag = true;
                    }
                    if (this.controlRight != Main.clientPlayer.controlRight)
                    {
                        flag = true;
                    }
                    if (this.controlJump != Main.clientPlayer.controlJump)
                    {
                        flag = true;
                    }
                    if (this.controlUseItem != Main.clientPlayer.controlUseItem)
                    {
                        flag = true;
                    }
                    if (this.selectedItem != Main.clientPlayer.selectedItem)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                    }
                }
                if (Main.playerInventory)
                {
                    this.AdjTiles();
                }
                if (this.chest != -1)
                {
                    int num14 = (int) ((this.position.X + (this.width * 0.5)) / 16.0);
                    int num15 = (int) ((this.position.Y + (this.height * 0.5)) / 16.0);
                    if (((num14 < (this.chestX - 5)) || (num14 > (this.chestX + 6))) || ((num15 < (this.chestY - 4)) || (num15 > (this.chestY + 5))))
                    {
                        if (this.chest != -1)
                        {
                            Main.PlaySound(11, -1, -1, 1);
                        }
                        this.chest = -1;
                    }
                    if (!Main.tile[this.chestX, this.chestY].active)
                    {
                        Main.PlaySound(11, -1, -1, 1);
                        this.chest = -1;
                    }
                }
                if (this.velocity.Y == 0f)
                {
                    int num16 = ((int) (this.position.Y / 16f)) - this.fallStart;
                    if ((((this.gravDir == 1f) && (num16 > 0x19)) || ((this.gravDir == -1f) && (num16 < -25))) && !this.noFallDmg)
                    {
                        int damage = ((int) ((num16 * this.gravDir) - 25f)) * 10;
                        this.immune = false;
                        this.Hurt(damage, 0, false, false, getDeathMessage(-1, -1, -1, 0));
                    }
                    this.fallStart = (int) (this.position.Y / 16f);
                }
                if (((this.jump > 0) || (this.rocketDelay > 0)) || (this.wet || this.slowFall))
                {
                    this.fallStart = (int) (this.position.Y / 16f);
                }
            }
            if (this.mouseInterface)
            {
                this.delayUseItem = true;
            }
            Player.tileTargetX = (int) ((Main.mouseState.X + Main.screenPosition.X) / 16f);
            Player.tileTargetY = (int) ((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
            if (this.immune)
            {
                this.immuneTime--;
                if (this.immuneTime <= 0)
                {
                    this.immune = false;
                }
                this.immuneAlpha += this.immuneAlphaDirection * 50;
                if (this.immuneAlpha <= 50)
                {
                    this.immuneAlphaDirection = 1;
                }
                else if (this.immuneAlpha >= 0xcd)
                {
                    this.immuneAlphaDirection = -1;
                }
            }
            else
            {
                this.immuneAlpha = 0;
            }
            if (this.manaRegenDelay > 0)
            {
                this.manaRegenDelay--;
            }
            this.statDefense = 0;
            this.accWatch = 0;
            this.accDepthMeter = 0;
            this.lifeRegen = 0;
            this.manaCost = 1f;
            this.meleeSpeed = 1f;
            this.meleeDamage = 1f;
            this.boneArmor = false;
            this.rocketBoots = false;
            this.fireWalk = false;
            this.noKnockback = false;
            this.jumpBoost = false;
            this.noFallDmg = false;
            this.accFlipper = false;
            this.spawnMax = false;
            this.spaceGun = false;
            this.magicBoost = 1f;
            this.killGuide = false;
            this.lavaImmune = false;
            this.gills = false;
            this.slowFall = false;
            this.findTreasure = false;
            this.invis = false;
            this.nightVision = false;
            this.enemySpawns = false;
            this.thorns = false;
            this.waterWalk = false;
            this.archer = false;
            this.detectCreature = false;
            this.gravControl = false;

            //This little bit updates our buffers, or stat specials
            for (int j = 0; j < 10; j++)
            {
                if ((this.buffType[j] > 0) && (this.buffTime[j] > 0))
                {
                    if (this.whoAmi == Main.myPlayer)
                    {
                        this.buffTime[j]--;
                    }
                    if (this.buffType[j] == 1)
                    {
                        this.lavaImmune = true;
                        this.fireWalk = true;
                    }
                    else if (this.buffType[j] == 2)
                    {
                        this.lifeRegen += 4;
                    }
                    else if (this.buffType[j] == 3)
                    {
                        num4 *= 1.25f;
                        num3 *= 1.25f;
                    }
                    else if (this.buffType[j] == 4)
                    {
                        this.gills = true;
                    }
                    else if (this.buffType[j] == 5)
                    {
                        this.statDefense += 10;
                    }
                    else if (this.buffType[j] == 6)
                    {
                        this.manaRegen += 20;
                    }
                    else if (this.buffType[j] == 7)
                    {
                        this.magicBoost *= 1.2f;
                    }
                    else if (this.buffType[j] == 8)
                    {
                        this.slowFall = true;
                    }
                    else if (this.buffType[j] == 9)
                    {
                        this.findTreasure = true;
                    }
                    else if (this.buffType[j] == 10)
                    {
                        this.invis = true;
                    }
                    else if (this.buffType[j] == 11)
                    {
                        Lighting.addLight((((int) this.position.X) + (this.width / 2)) / 0x10, (((int) this.position.Y) + (this.height / 2)) / 0x10, 1f);
                    }
                    else if (this.buffType[j] == 12)
                    {
                        this.nightVision = true;
                    }
                    else if (this.buffType[j] == 13)
                    {
                        this.enemySpawns = true;
                    }
                    else if (this.buffType[j] == 14)
                    {
                        this.thorns = true;
                    }
                    else if (this.buffType[j] == 15)
                    {
                        this.waterWalk = true;
                    }
                    else if (this.buffType[j] == 0x10)
                    {
                        this.archer = true;
                    }
                    else if (this.buffType[j] == 0x11)
                    {
                        this.detectCreature = true;
                    }
                    else if (this.buffType[j] == 0x12)
                    {
                        this.gravControl = true;
                    }
                }
            }
            if (this.whoAmi == Main.myPlayer)
            {
                for (int num19 = 0; num19 < 10; num19++)
                {
                    if ((this.buffType[num19] > 0) && (this.buffTime[num19] <= 0))
                    {
                        this.DelBuff(num19);
                    }
                }
            }
            if (this.manaRegenDelay == 0)
            {
                this.manaRegen = (this.statManaMax / 30) + 1;
            }
            else
            {
                this.manaRegen = 0;
            }
            this.doubleJump = false;
            for (int k = 0; k < 8; k++)
            {
                this.statDefense += this.armor[k].defense;
                this.lifeRegen += this.armor[k].lifeRegen;
                this.manaRegen += this.armor[k].manaRegen;
                if (this.armor[k].type == 0xc1)
                {
                    this.fireWalk = true;
                }
                if (this.armor[k].type == 0xee)
                {
                    this.magicBoost *= 1.15f;
                }
            }
            this.head = this.armor[0].headSlot;
            this.body = this.armor[1].bodySlot;
            this.legs = this.armor[2].legSlot;
            for (int m = 3; m < 8; m++)
            {
                if ((this.armor[m].type == 15) && (this.accWatch < 1))
                {
                    this.accWatch = 1;
                }
                if ((this.armor[m].type == 0x10) && (this.accWatch < 2))
                {
                    this.accWatch = 2;
                }
                if ((this.armor[m].type == 0x11) && (this.accWatch < 3))
                {
                    this.accWatch = 3;
                }
                if ((this.armor[m].type == 0x12) && (this.accDepthMeter < 1))
                {
                    this.accDepthMeter = 1;
                }
                if (this.armor[m].type == 0x35)
                {
                    this.doubleJump = true;
                }
                if (this.armor[m].type == 0x36)
                {
                    num6 = 6f;
                }
                if (this.armor[m].type == 0x80)
                {
                    this.rocketBoots = true;
                }
                if (this.armor[m].type == 0x9c)
                {
                    this.noKnockback = true;
                }
                if (this.armor[m].type == 0x9e)
                {
                    this.noFallDmg = true;
                }
                if (this.armor[m].type == 0x9f)
                {
                    this.jumpBoost = true;
                }
                if (this.armor[m].type == 0xbb)
                {
                    this.accFlipper = true;
                }
                if (this.armor[m].type == 0xd3)
                {
                    this.meleeSpeed *= 0.9f;
                }
                if (this.armor[m].type == 0xdf)
                {
                    this.spawnMax = true;
                }
                if (this.armor[m].type == 0x11d)
                {
                    num4 *= 1.1f;
                    num3 *= 1.1f;
                }
                if (this.armor[m].type == 0xd4)
                {
                    num4 *= 1.05f;
                    num3 *= 1.05f;
                }
                if (this.armor[m].type == 0x10b)
                {
                    this.killGuide = true;
                }
            }
            this.lifeRegenCount += this.lifeRegen;
            while (this.lifeRegenCount >= 120)
            {
                this.lifeRegenCount -= 120;
                if (this.statLife < this.statLifeMax)
                {
                    this.statLife++;
                }
                if (this.statLife > this.statLifeMax)
                {
                    this.statLife = this.statLifeMax;
                }
            }
            this.manaRegenCount += this.manaRegen;
            while (this.manaRegenCount >= 120)
            {
                this.manaRegenCount -= 120;
                if (this.statMana < this.statManaMax)
                {
                    this.statMana++;
                }
                if (this.statMana > this.statManaMax)
                {
                    this.statMana = this.statManaMax;
                }
            }
            if (this.head == 11)
            {
                int num22 = ((((int) this.position.X) + (this.width / 2)) + (8 * this.direction)) / 0x10;
                int num23 = ((int) (this.position.Y + 2f)) / 0x10;
                Lighting.addLight(num22, num23, 0.8f);
            }
            if (this.jumpBoost)
            {
                jumpHeight = 20;
                jumpSpeed = 6.51f;
            }
            this.setBonus = "";
            if ((((this.head == 1) && (this.body == 1)) && (this.legs == 1)) || (((this.head == 2) && (this.body == 2)) && (this.legs == 2)))
            {
                this.setBonus = "2 defense";
                this.statDefense += 2;
            }
            if ((((this.head == 3) && (this.body == 3)) && (this.legs == 3)) || (((this.head == 4) && (this.body == 4)) && (this.legs == 4)))
            {
                this.setBonus = "3 defense";
                this.statDefense += 3;
            }
            if (((this.head == 5) && (this.body == 5)) && (this.legs == 5))
            {
                this.setBonus = "15 % increased melee speed";
                this.meleeSpeed *= 0.85f;
            }
            if (((this.head == 6) && (this.body == 6)) && (this.legs == 6))
            {
                this.setBonus = "Space Gun costs 0 mana";
                this.spaceGun = true;
            }
            if (((this.head == 7) && (this.body == 7)) && (this.legs == 7))
            {
                num4 *= 1.3f;
                num3 *= 1.3f;
                this.setBonus = "30% increased movement speed";
            }
            if (((this.head == 8) && (this.body == 8)) && (this.legs == 8))
            {
                this.setBonus = "25% reduced mana usage";
                this.manaCost *= 0.75f;
            }
            if (((this.head == 9) && (this.body == 9)) && (this.legs == 9))
            {
                this.setBonus = "15% extra melee damage";
                this.meleeDamage *= 1.15f;
            }
            if (!this.doubleJump)
            {
                this.jumpAgain = false;
            }
            else if (this.velocity.Y == 0f)
            {
                this.jumpAgain = true;
            }
            if (this.meleeSpeed < 0.7)
            {
                this.meleeSpeed = 0.7f;
            }
            if (this.grappling[0] == -1)
            {
                if (this.controlLeft && (this.velocity.X > -num3))
                {
                    if (this.velocity.X > num5)
                    {
                        this.velocity.X -= num5;
                    }
                    this.velocity.X -= num4;
                    if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                    {
                        this.direction = -1;
                    }
                }
                else if (this.controlRight && (this.velocity.X < num3))
                {
                    if (this.velocity.X < -num5)
                    {
                        this.velocity.X += num5;
                    }
                    this.velocity.X += num4;
                    if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                    {
                        this.direction = 1;
                    }
                }
                else if (this.controlLeft && (this.velocity.X > -num6))
                {
                    if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                    {
                        this.direction = -1;
                    }
                    if (this.velocity.Y == 0f)
                    {
                        if (this.velocity.X > num5)
                        {
                            this.velocity.X -= num5;
                        }
                        this.velocity.X -= num4 * 0.2f;
                    }
                    if ((this.velocity.X < (-(num6 + num3) / 2f)) && (this.velocity.Y == 0f))
                    {
                        int num24 = 0;
                        if (this.gravDir == -1f)
                        {
                            num24 -= this.height;
                        }
                        if ((this.runSoundDelay == 0) && (this.velocity.Y == 0f))
                        {
                            Main.PlaySound(0x11, (int) this.position.X, (int) this.position.Y, 1);
                            this.runSoundDelay = 9;
                        }
                        color = new Color();
                        int index = Dust.NewDust(new Vector2(this.position.X - 4f, (this.position.Y + this.height) + num24), this.width + 8, 4, 0x10, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, color, 1.5f);
                        Main.dust[index].velocity.X *= 0.2f;
                        Main.dust[index].velocity.Y *= 0.2f;
                    }
                }
                else if (this.controlRight && (this.velocity.X < num6))
                {
                    if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                    {
                        this.direction = 1;
                    }
                    if (this.velocity.Y == 0f)
                    {
                        if (this.velocity.X < -num5)
                        {
                            this.velocity.X += num5;
                        }
                        this.velocity.X += num4 * 0.2f;
                    }
                    if ((this.velocity.X > ((num6 + num3) / 2f)) && (this.velocity.Y == 0f))
                    {
                        int num26 = 0;
                        if (this.gravDir == -1f)
                        {
                            num26 -= this.height;
                        }
                        if ((this.runSoundDelay == 0) && (this.velocity.Y == 0f))
                        {
                            Main.PlaySound(0x11, (int) this.position.X, (int) this.position.Y, 1);
                            this.runSoundDelay = 9;
                        }
                        color = new Color();
                        int num27 = Dust.NewDust(new Vector2(this.position.X - 4f, (this.position.Y + this.height) + num26), this.width + 8, 4, 0x10, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, color, 1.5f);
                        Main.dust[num27].velocity.X *= 0.2f;
                        Main.dust[num27].velocity.Y *= 0.2f;
                    }
                }
                else if (this.velocity.Y == 0f)
                {
                    if (this.velocity.X > num5)
                    {
                        this.velocity.X -= num5;
                    }
                    else if (this.velocity.X < -num5)
                    {
                        this.velocity.X += num5;
                    }
                    else
                    {
                        this.velocity.X = 0f;
                    }
                }
                else if (this.velocity.X > (num5 * 0.5))
                {
                    this.velocity.X -= num5 * 0.5f;
                }
                else if (this.velocity.X < (-num5 * 0.5))
                {
                    this.velocity.X += num5 * 0.5f;
                }
                else
                {
                    this.velocity.X = 0f;
                }
                if (this.gravControl)
                {
                    if (this.controlUp && (this.gravDir == 1f))
                    {
                        this.gravDir = -1f;
                        this.fallStart = (int) (this.position.Y / 16f);
                        this.jump = 0;
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                    }
                    if (this.controlDown && (this.gravDir == -1f))
                    {
                        this.gravDir = 1f;
                        this.fallStart = (int) (this.position.Y / 16f);
                        this.jump = 0;
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                    }
                }
                else
                {
                    this.gravDir = 1f;
                }
                if (this.controlJump)
                {
                    if (this.jump > 0)
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.jump = 0;
                        }
                        else
                        {
                            this.velocity.Y = -jumpSpeed * this.gravDir;
                            this.jump--;
                        }
                    }
                    else if ((((this.velocity.Y == 0f) || this.jumpAgain) || (this.wet && this.accFlipper)) && this.releaseJump)
                    {
                        bool flag2 = false;
                        if (this.wet && this.accFlipper)
                        {
                            if (this.swimTime == 0)
                            {
                                this.swimTime = 30;
                            }
                            flag2 = true;
                        }
                        this.jumpAgain = false;
                        this.canRocket = false;
                        this.rocketRelease = false;
                        if ((this.velocity.Y == 0f) && this.doubleJump)
                        {
                            this.jumpAgain = true;
                        }
                        if ((this.velocity.Y == 0f) || flag2)
                        {
                            this.velocity.Y = -jumpSpeed * this.gravDir;
                            this.jump = jumpHeight;
                        }
                        else
                        {
                            int height = this.height;
                            if (this.gravDir == -1f)
                            {
                                height = 0;
                            }
                            Main.PlaySound(0x10, (int) this.position.X, (int) this.position.Y, 1);
                            this.velocity.Y = -jumpSpeed * this.gravDir;
                            this.jump = jumpHeight / 2;
                            for (int num29 = 0; num29 < 10; num29++)
                            {
                                color = new Color();
                                int num30 = Dust.NewDust(new Vector2(this.position.X - 34f, (this.position.Y + height) - 16f), 0x66, 0x20, 0x10, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, color, 1.5f);
                                Main.dust[num30].velocity.X = (Main.dust[num30].velocity.X * 0.5f) - (this.velocity.X * 0.1f);
                                Main.dust[num30].velocity.Y = (Main.dust[num30].velocity.Y * 0.5f) - (this.velocity.Y * 0.3f);
                            }
                            int num31 = Gore.NewGore(new Vector2((this.position.X + (this.width / 2)) - 16f, (this.position.Y + height) - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num31].velocity.X = (Main.gore[num31].velocity.X * 0.1f) - (this.velocity.X * 0.1f);
                            Main.gore[num31].velocity.Y = (Main.gore[num31].velocity.Y * 0.1f) - (this.velocity.Y * 0.05f);
                            num31 = Gore.NewGore(new Vector2(this.position.X - 36f, (this.position.Y + height) - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num31].velocity.X = (Main.gore[num31].velocity.X * 0.1f) - (this.velocity.X * 0.1f);
                            Main.gore[num31].velocity.Y = (Main.gore[num31].velocity.Y * 0.1f) - (this.velocity.Y * 0.05f);
                            num31 = Gore.NewGore(new Vector2((this.position.X + this.width) + 4f, (this.position.Y + height) - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num31].velocity.X = (Main.gore[num31].velocity.X * 0.1f) - (this.velocity.X * 0.1f);
                            Main.gore[num31].velocity.Y = (Main.gore[num31].velocity.Y * 0.1f) - (this.velocity.Y * 0.05f);
                        }
                    }
                    this.releaseJump = false;
                }
                else
                {
                    this.jump = 0;
                    this.releaseJump = true;
                    this.rocketRelease = true;
                }
                if (((this.doubleJump && !this.jumpAgain) && (((this.gravDir == 1f) && (this.velocity.Y < 0f)) || ((this.gravDir == -1f) && (this.velocity.Y > 0f)))) && (!this.rocketBoots && !this.accFlipper))
                {
                    int num32 = this.height;
                    if (this.gravDir == -1f)
                    {
                        num32 = -6;
                    }
                    color = new Color();
                    int num33 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + num32), this.width + 8, 4, 0x10, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, color, 1.5f);
                    Main.dust[num33].velocity.X = (Main.dust[num33].velocity.X * 0.5f) - (this.velocity.X * 0.1f);
                    Main.dust[num33].velocity.Y = (Main.dust[num33].velocity.Y * 0.5f) - (this.velocity.Y * 0.3f);
                }
                if ((((this.gravDir == 1f) && (this.velocity.Y > -jumpSpeed)) || ((this.gravDir == -1f) && (this.velocity.Y < jumpSpeed))) && (this.velocity.Y != 0f))
                {
                    this.canRocket = true;
                }
                if (this.velocity.Y == 0f)
                {
                    this.rocketTime = this.rocketTimeMax;
                }
                if (((this.rocketBoots && this.controlJump) && ((this.rocketDelay == 0) && this.canRocket)) && (this.rocketRelease && !this.jumpAgain))
                {
                    if (this.rocketTime > 0)
                    {
                        //this.rocketTime--; //let's have infinite rocket time
                        this.rocketDelay = 10;
                        if (this.rocketDelay2 <= 0)
                        {
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 13);
                            this.rocketDelay2 = 30;
                        }
                    }
                    else
                    {
                        this.canRocket = false;
                    }
                }
                if (this.rocketDelay2 > 0)
                {
                    this.rocketDelay2--;
                }
                if (this.rocketDelay == 0)
                {
                    this.rocketFrame = false;
                }
                if (this.rocketDelay > 0)
                {
                    int num34 = this.height;
                    if (this.gravDir == -1f)
                    {
                        num34 = 4;
                    }
                    this.rocketFrame = true;
                    for (int num35 = 0; num35 < 2; num35++)
                    {
                        int type = 6;
                        float scale = 2.5f;
                        if (this.socialShadow)
                        {
                            type = 0x1b;
                            scale = 1.5f;
                        }
                        if (num35 == 0)
                        {
                            color = new Color();
                            int num38 = Dust.NewDust(new Vector2(this.position.X - 4f, (this.position.Y + num34) - 10f), 8, 8, type, 0f, 0f, 100, color, scale);
                            Main.dust[num38].noGravity = true;
                            Main.dust[num38].velocity.X = ((Main.dust[num38].velocity.X * 1f) - 2f) - (this.velocity.X * 0.3f);
                            Main.dust[num38].velocity.Y = ((Main.dust[num38].velocity.Y * 1f) + (2f * this.gravDir)) - (this.velocity.Y * 0.3f);
                        }
                        else
                        {
                            color = new Color();
                            int num39 = Dust.NewDust(new Vector2((this.position.X + this.width) - 4f, (this.position.Y + num34) - 10f), 8, 8, type, 0f, 0f, 100, color, scale);
                            Main.dust[num39].noGravity = true;
                            Main.dust[num39].velocity.X = ((Main.dust[num39].velocity.X * 1f) + 2f) - (this.velocity.X * 0.3f);
                            Main.dust[num39].velocity.Y = ((Main.dust[num39].velocity.Y * 1f) + (2f * this.gravDir)) - (this.velocity.Y * 0.3f);
                        }
                    }
                    if (this.rocketDelay == 0)
                    {
                        this.releaseJump = true;
                    }
                    this.rocketDelay--;
                    this.velocity.Y -= 0.1f * this.gravDir;
                    if (this.gravDir == 1f)
                    {
                        if (this.velocity.Y > 0f)
                        {
                            this.velocity.Y -= 0.5f;
                        }
                        else if (this.velocity.Y > (-jumpSpeed * 0.5))
                        {
                            this.velocity.Y -= 0.1f;
                        }
                        if (this.velocity.Y < (-jumpSpeed * 1.5f))
                        {
                            this.velocity.Y = -jumpSpeed * 1.5f;
                        }
                    }
                    else
                    {
                        if (this.velocity.Y < 0f)
                        {
                            this.velocity.Y += 0.5f;
                        }
                        else if (this.velocity.Y < (jumpSpeed * 0.5))
                        {
                            this.velocity.Y += 0.1f;
                        }
                        if (this.velocity.Y > (jumpSpeed * 1.5f))
                        {
                            this.velocity.Y = jumpSpeed * 1.5f;
                        }
                    }
                }
                else if (this.slowFall && ((!this.controlDown && (this.gravDir == 1f)) || (!this.controlUp && (this.gravDir == -1f))))
                {
                    if ((this.controlUp && (this.gravDir == 1f)) || (this.controlDown && (this.gravDir == -1f)))
                    {
                        this.velocity.Y += (num2 / 10f) * this.gravDir;
                    }
                    else
                    {
                        this.velocity.Y += (num2 / 3f) * this.gravDir;
                    }
                }
                else
                {
                    this.velocity.Y += num2 * this.gravDir;
                }
                if (this.gravDir == 1f)
                {
                    if (this.velocity.Y > num)
                    {
                        this.velocity.Y = num;
                    }
                    if ((this.slowFall && (this.velocity.Y > (num / 3f))) && !this.controlDown)
                    {
                        this.velocity.Y = num / 3f;
                    }
                    if ((this.slowFall && (this.velocity.Y > (num / 5f))) && this.controlUp)
                    {
                        this.velocity.Y = num / 10f;
                    }
                }
                else
                {
                    if (this.velocity.Y < -num)
                    {
                        this.velocity.Y = -num;
                    }
                    if ((this.slowFall && (this.velocity.Y < (-num / 3f))) && !this.controlUp)
                    {
                        this.velocity.Y = -num / 3f;
                    }
                    if ((this.slowFall && (this.velocity.Y < (-num / 5f))) && this.controlDown)
                    {
                        this.velocity.Y = -num / 10f;
                    }
                }
            }
            for (int n = 0; n < 200; n++)
            {
                if ((Main.item[n].active && (Main.item[n].noGrabDelay == 0)) && (Main.item[n].owner == i))
                {
                    Rectangle rectangle6 = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                    if (rectangle6.Intersects(new Rectangle((int) Main.item[n].position.X, (int) Main.item[n].position.Y, Main.item[n].width, Main.item[n].height)))
                    {
                        if ((i == Main.myPlayer) && ((this.inventory[this.selectedItem].type != 0) || (this.itemAnimation <= 0)))
                        {
                            if (Main.item[n].type == 0x3a)
                            {
                                Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                                this.statLife += 20;
                                if (Main.myPlayer == this.whoAmi)
                                {
                                    this.HealEffect(20);
                                }
                                if (this.statLife > this.statLifeMax)
                                {
                                    this.statLife = this.statLifeMax;
                                }
                                Main.item[n] = new Item();
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x15, -1, -1, "", n, 0f, 0f, 0f, 0);
                                }
                            }
                            else if (Main.item[n].type == 0xb8)
                            {
                                Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                                this.statMana += 20;
                                if (Main.myPlayer == this.whoAmi)
                                {
                                    this.ManaEffect(20);
                                }
                                if (this.statMana > this.statManaMax)
                                {
                                    this.statMana = this.statManaMax;
                                }
                                Main.item[n] = new Item();
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x15, -1, -1, "", n, 0f, 0f, 0f, 0);
                                }
                            }
                            else
                            {
                                Main.item[n] = this.GetItem(i, Main.item[n]);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(0x15, -1, -1, "", n, 0f, 0f, 0f, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        rectangle6 = new Rectangle(((int) this.position.X) - itemGrabRange, ((int) this.position.Y) - itemGrabRange, this.width + (itemGrabRange * 2), this.height + (itemGrabRange * 2));
                        if (rectangle6.Intersects(new Rectangle((int) Main.item[n].position.X, (int) Main.item[n].position.Y, Main.item[n].width, Main.item[n].height)) && this.ItemSpace(Main.item[n]))
                        {
                            Main.item[n].beingGrabbed = true;
                            if ((this.position.X + (this.width * 0.5)) > (Main.item[n].position.X + (Main.item[n].width * 0.5)))
                            {
                                if (Main.item[n].velocity.X < (itemGrabSpeedMax + this.velocity.X))
                                {
                                    Main.item[n].velocity.X += itemGrabSpeed;
                                }
                                if (Main.item[n].velocity.X < 0f)
                                {
                                    Main.item[n].velocity.X += itemGrabSpeed * 0.75f;
                                }
                            }
                            else
                            {
                                if (Main.item[n].velocity.X > (-itemGrabSpeedMax + this.velocity.X))
                                {
                                    Main.item[n].velocity.X -= itemGrabSpeed;
                                }
                                if (Main.item[n].velocity.X > 0f)
                                {
                                    Main.item[n].velocity.X -= itemGrabSpeed * 0.75f;
                                }
                            }
                            if ((this.position.Y + (this.height * 0.5)) > (Main.item[n].position.Y + (Main.item[n].height * 0.5)))
                            {
                                if (Main.item[n].velocity.Y < itemGrabSpeedMax)
                                {
                                    Main.item[n].velocity.Y += itemGrabSpeed;
                                }
                                if (Main.item[n].velocity.Y < 0f)
                                {
                                    Main.item[n].velocity.Y += itemGrabSpeed * 0.75f;
                                }
                            }
                            else
                            {
                                if (Main.item[n].velocity.Y > -itemGrabSpeedMax)
                                {
                                    Main.item[n].velocity.Y -= itemGrabSpeed;
                                }
                                if (Main.item[n].velocity.Y > 0f)
                                {
                                    Main.item[n].velocity.Y -= itemGrabSpeed * 0.75f;
                                }
                            }
                        }
                    }
                }
            }
            if ((((((this.position.X / 16f) - tileRangeX) <= Player.tileTargetX) && (((((this.position.X + this.width) / 16f) + tileRangeX) - 1f) >= Player.tileTargetX)) && ((((this.position.Y / 16f) - tileRangeY) <= Player.tileTargetY) && (((((this.position.Y + this.height) / 16f) + tileRangeY) - 2f) >= Player.tileTargetY))) && Main.tile[Player.tileTargetX, Player.tileTargetY].active)
            {
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x4f)
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0xe0;
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x15)
                {
                    this.showItemIcon = true;
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 0x24)
                    {
                        this.showItemIcon2 = 0x132;
                    }
                    else
                    {
                        this.showItemIcon2 = 0x30;
                    }
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4)
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 8;
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)
                {
                    this.showItemIcon = true;
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 0x12)
                    {
                        this.showItemIcon2 = 0x1c;
                    }
                    else if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 0x24)
                    {
                        this.showItemIcon2 = 110;
                    }
                    else
                    {
                        this.showItemIcon2 = 0x1f;
                    }
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x1d)
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0x57;
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x21)
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0x69;
                }
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x31)
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0x94;
                }
                if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50) && (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90))
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0xa5;
                }
                if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x37) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x55))
                {
                    int num41 = Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 0x12;
                    int num42 = Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 0x12;
                    while (num41 > 1)
                    {
                        num41 -= 2;
                    }
                    int num43 = Player.tileTargetX - num41;
                    int num44 = Player.tileTargetY - num42;
                    Main.signBubble = true;
                    Main.signX = (num43 * 0x10) + 0x10;
                    Main.signY = num44 * 0x10;
                }
                if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11))
                {
                    this.showItemIcon = true;
                    this.showItemIcon2 = 0x19;
                }
                if (this.controlUseTile)
                {
                    if (this.releaseUseTile)
                    {
                        if (((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)) || (((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x21) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x31)) || ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50) && (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90))))
                        {
                            WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(0x11, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 0f, 0);
                            }
                        }
                        else if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x4f)
                        {
                            int tileTargetX = Player.tileTargetX;
                            int tileTargetY = Player.tileTargetY;
                            tileTargetX += (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 0x12) * -1;
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 0x48)
                            {
                                tileTargetX += 4;
                                tileTargetX++;
                            }
                            else
                            {
                                tileTargetX += 2;
                            }
                            tileTargetY += (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 0x12) * -1;
                            tileTargetY += 2;
                            if (CheckSpawn(tileTargetX, tileTargetY))
                            {
                                this.ChangeSpawn(tileTargetX, tileTargetY);
                                Main.NewText("Spawn point set!", 0xff, 240, 20);
                            }
                        }
                        else if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x37) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x55))
                        {
                            bool flag3 = true;
                            if ((this.sign >= 0) && (Sign.ReadSign(Player.tileTargetX, Player.tileTargetY) == this.sign))
                            {
                                this.sign = -1;
                                Main.npcChatText = "";
                                Main.editSign = false;
                                Main.PlaySound(11, -1, -1, 1);
                                flag3 = false;
                            }
                            if (flag3)
                            {
                                if (Main.netMode == 0)
                                {
                                    this.talkNPC = -1;
                                    Main.playerInventory = false;
                                    Main.editSign = false;
                                    Main.PlaySound(10, -1, -1, 1);
                                    int num48 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
                                    this.sign = num48;
                                    Main.npcChatText = Main.sign[num48].text;
                                }
                                else
                                {
                                    int num49 = Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 0x12;
                                    int num50 = Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 0x12;
                                    while (num49 > 1)
                                    {
                                        num49 -= 2;
                                    }
                                    int number = Player.tileTargetX - num49;
                                    int num52 = Player.tileTargetY - num50;
                                    if ((Main.tile[number, num52].type == 0x37) || (Main.tile[number, num52].type == 0x55))
                                    {
                                        NetMessage.SendData(0x2e, -1, -1, "", number, (float) num52, 0f, 0f, 0);
                                    }
                                }
                            }
                        }
                        else if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10)
                        {
                            WorldGen.OpenDoor(Player.tileTargetX, Player.tileTargetY, this.direction);
                            NetMessage.SendData(0x13, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.direction, 0);
                        }
                        else if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
                        {
                            if (WorldGen.CloseDoor(Player.tileTargetX, Player.tileTargetY, false))
                            {
                                NetMessage.SendData(0x13, -1, -1, "", 1, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.direction, 0);
                            }
                        }
                        else if (((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x15) || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x1d)) && (this.talkNPC == -1))
                        {
                            bool flag4 = false;
                            int num53 = Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 0x12;
                            while (num53 > 1)
                            {
                                num53 -= 2;
                            }
                            num53 = Player.tileTargetX - num53;
                            int y = Player.tileTargetY - (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 0x12);
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0x1d)
                            {
                                flag4 = true;
                            }
                            if ((Main.netMode == 1) && !flag4)
                            {
                                if (((num53 == this.chestX) && (y == this.chestY)) && (this.chest != -1))
                                {
                                    this.chest = -1;
                                    Main.PlaySound(11, -1, -1, 1);
                                }
                                else
                                {
                                    NetMessage.SendData(0x1f, -1, -1, "", num53, (float) y, 0f, 0f, 0);
                                }
                            }
                            else
                            {
                                int num55 = -1;
                                if (flag4)
                                {
                                    num55 = -2;
                                }
                                else
                                {
                                    num55 = Chest.FindChest(num53, y);
                                }
                                if (num55 != -1)
                                {
                                    if (num55 == this.chest)
                                    {
                                        this.chest = -1;
                                        Main.PlaySound(11, -1, -1, 1);
                                    }
                                    else if ((num55 != this.chest) && (this.chest == -1))
                                    {
                                        this.chest = num55;
                                        Main.playerInventory = true;
                                        Main.PlaySound(10, -1, -1, 1);
                                        this.chestX = num53;
                                        this.chestY = y;
                                    }
                                    else
                                    {
                                        this.chest = num55;
                                        Main.playerInventory = true;
                                        Main.PlaySound(12, -1, -1, 1);
                                        this.chestX = num53;
                                        this.chestY = y;
                                    }
                                }
                            }
                        }
                    }
                    this.releaseUseTile = false;
                }
                else
                {
                    this.releaseUseTile = true;
                }
            }
            if (Main.myPlayer == this.whoAmi)
            {
                if (this.talkNPC >= 0)
                {
                    Rectangle rectangle = new Rectangle((((int) this.position.X) + (this.width / 2)) - (tileRangeX * 0x10), (((int) this.position.Y) + (this.height / 2)) - (tileRangeY * 0x10), (tileRangeX * 0x10) * 2, (tileRangeY * 0x10) * 2);
                    Rectangle rectangle2 = new Rectangle((int) Main.npc[this.talkNPC].position.X, (int) Main.npc[this.talkNPC].position.Y, Main.npc[this.talkNPC].width, Main.npc[this.talkNPC].height);
                    if ((!rectangle.Intersects(rectangle2) || (this.chest != -1)) || !Main.npc[this.talkNPC].active)
                    {
                        if (this.chest == -1)
                        {
                            Main.PlaySound(11, -1, -1, 1);
                        }
                        this.talkNPC = -1;
                        Main.npcChatText = "";
                    }
                }
                if (this.sign >= 0)
                {
                    Rectangle rectangle3 = new Rectangle((((int) this.position.X) + (this.width / 2)) - (tileRangeX * 0x10), (((int) this.position.Y) + (this.height / 2)) - (tileRangeY * 0x10), (tileRangeX * 0x10) * 2, (tileRangeY * 0x10) * 2);
                    try
                    {
                        Rectangle rectangle4 = new Rectangle(Main.sign[this.sign].x * 0x10, Main.sign[this.sign].y * 0x10, 0x20, 0x20);
                        if (!rectangle3.Intersects(rectangle4))
                        {
                            Main.PlaySound(11, -1, -1, 1);
                            this.sign = -1;
                            Main.editSign = false;
                            Main.npcChatText = "";
                        }
                    }
                    catch
                    {
                        Main.PlaySound(11, -1, -1, 1);
                        this.sign = -1;
                        Main.editSign = false;
                        Main.npcChatText = "";
                    }
                }
                if (Main.editSign)
                {
                    if (this.sign == -1)
                    {
                        Main.editSign = false;
                    }
                    else
                    {
                        Main.npcChatText = Main.GetInputText(Main.npcChatText);
                        if (Main.inputTextEnter)
                        {
                            byte[] bytes = new byte[] { 10 };
                            Main.npcChatText = Main.npcChatText + Encoding.ASCII.GetString(bytes);
                        }
                    }
                }
                Rectangle rectangle5 = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                for (int num56 = 0; num56 < 0x3e8; num56++)
                {
                    if ((Main.npc[num56].active && !Main.npc[num56].friendly) && ((Main.npc[num56].damage > 0) && rectangle5.Intersects(new Rectangle((int) Main.npc[num56].position.X, (int) Main.npc[num56].position.Y, Main.npc[num56].width, Main.npc[num56].height))))
                    {
                        int hitDirection = -1;
                        if ((Main.npc[num56].position.X + (Main.npc[num56].width / 2)) < (this.position.X + (this.width / 2)))
                        {
                            hitDirection = 1;
                        }
                        if (((this.whoAmi == Main.myPlayer) && this.thorns) && !this.immune)
                        {
                            int num58 = Main.npc[num56].damage / 3;
                            int num59 = 10;
                            Main.npc[num56].StrikeNPC(num58, (float) num59, -hitDirection);
                            if (Main.netMode != 0)
                            {
                                NetMessage.SendData(0x1c, -1, -1, "", num56, (float) num58, (float) num59, (float) -hitDirection, 0);
                            }
                        }
                        this.Hurt(Main.npc[num56].damage, hitDirection, false, false, getDeathMessage(-1, num56, -1, -1));
                    }
                }
                Vector2 vector = Collision.HurtTiles(this.position, this.velocity, this.width, this.height, this.fireWalk);
                if (vector.Y != 0f)
                {
                    this.Hurt((int) vector.Y, (int) vector.X, false, false, getDeathMessage(-1, -1, -1, 3));
                }
            }
            if (this.grappling[0] >= 0)
            {
                this.rocketTime = this.rocketTimeMax;
                this.rocketDelay = 0;
                this.rocketFrame = false;
                this.canRocket = false;
                this.rocketRelease = false;
                this.fallStart = (int) (this.position.Y / 16f);
                float num60 = 0f;
                float num61 = 0f;
                for (int num62 = 0; num62 < this.grapCount; num62++)
                {
                    num60 += Main.projectile[this.grappling[num62]].position.X + (Main.projectile[this.grappling[num62]].width / 2);
                    num61 += Main.projectile[this.grappling[num62]].position.Y + (Main.projectile[this.grappling[num62]].height / 2);
                }
                num60 /= (float) this.grapCount;
                num61 /= (float) this.grapCount;
                Vector2 vector2 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                float num63 = num60 - vector2.X;
                float num64 = num61 - vector2.Y;
                float num65 = (float) Math.Sqrt((double) ((num63 * num63) + (num64 * num64)));
                float num66 = 11f;
                float num67 = num65;
                if (num65 > num66)
                {
                    num67 = num66 / num65;
                }
                else
                {
                    num67 = 1f;
                }
                num63 *= num67;
                num64 *= num67;
                this.velocity.X = num63;
                this.velocity.Y = num64;
                if (this.itemAnimation == 0)
                {
                    if (this.velocity.X > 0f)
                    {
                        this.direction = 1;
                    }
                    if (this.velocity.X < 0f)
                    {
                        this.direction = -1;
                    }
                }
                if (this.controlJump)
                {
                    if (this.releaseJump)
                    {
                        if ((this.velocity.Y == 0f) || ((this.wet && (this.velocity.Y > -0.02)) && (this.velocity.Y < 0.02)))
                        {
                            this.velocity.Y = -jumpSpeed;
                            this.jump = jumpHeight / 2;
                            this.releaseJump = false;
                        }
                        else
                        {
                            this.velocity.Y += 0.01f;
                            this.releaseJump = false;
                        }
                        if (this.doubleJump)
                        {
                            this.jumpAgain = true;
                        }
                        this.grappling[0] = 0;
                        this.grapCount = 0;
                        for (int num68 = 0; num68 < 0x3e8; num68++)
                        {
                            if ((Main.projectile[num68].active && (Main.projectile[num68].owner == i)) && (Main.projectile[num68].aiStyle == 7))
                            {
                                Main.projectile[num68].Kill();
                            }
                        }
                    }
                }
                else
                {
                    this.releaseJump = true;
                }
            }
            if (Collision.StickyTiles(this.position, this.velocity, this.width, this.height))
            {
                this.fallStart = (int) (this.position.Y / 16f);
                this.jump = 0;
                if (this.velocity.X > 1f)
                {
                    this.velocity.X = 1f;
                }
                if (this.velocity.X < -1f)
                {
                    this.velocity.X = -1f;
                }
                if (this.velocity.Y > 1f)
                {
                    this.velocity.Y = 1f;
                }
                if (this.velocity.Y < -5f)
                {
                    this.velocity.Y = -5f;
                }
                if ((this.velocity.X > 0.75) || (this.velocity.X < -0.75))
                {
                    this.velocity.X *= 0.85f;
                }
                else
                {
                    this.velocity.X *= 0.6f;
                }
                if (this.velocity.Y < 0f)
                {
                    this.velocity.Y *= 0.96f;
                }
                else
                {
                    this.velocity.Y *= 0.3f;
                }
            }
            bool flag5 = Collision.DrownCollision(this.position, this.width, this.height, this.gravDir);
            if (this.armor[0].type == 250)
            {
                flag5 = true;
            }
            if (this.inventory[this.selectedItem].type == 0xba)
            {
                try
                {
                    int num69 = (int) (((this.position.X + (this.width / 2)) + (6 * this.direction)) / 16f);
                    int num70 = 0;
                    if (this.gravDir == -1f)
                    {
                        num70 = this.height;
                    }
                    int num71 = (int) (((this.position.Y + num70) - (44f * this.gravDir)) / 16f);
                    if (Main.tile[num69, num71].liquid < 0x80)
                    {
                        if (Main.tile[num69, num71] == null)
                        {
                            Main.tile[num69, num71] = new Tile();
                        }
                        if ((!Main.tile[num69, num71].active || !Main.tileSolid[Main.tile[num69, num71].type]) || Main.tileSolidTop[Main.tile[num69, num71].type])
                        {
                            flag5 = false;
                        }
                    }
                }
                catch
                {
                }
            }
            if (this.gills)
            {
                if (flag5)
                {
                    flag5 = false;
                }
                else
                {
                    flag5 = true;
                }
            }
            if (Main.myPlayer == i)
            {
                if (flag5)
                {
                    this.breathCD++;
                    int num72 = 7;
                    if (this.inventory[this.selectedItem].type == 0xba)
                    {
                        num72 *= 2;
                    }
                    if (this.armor[0].type == 0x10c)
                    {
                        num72 *= 4;
                    }
                    if (this.breathCD >= num72)
                    {
                        this.breathCD = 0;
                        this.breath--;
                        if (this.breath <= 0)
                        {
                            this.breath = 0;
                            this.statLife -= 2;
                            if (this.statLife <= 0)
                            {
                                this.statLife = 0;
                                this.KillMe(10.0, 0, false, getDeathMessage(-1, -1, -1, 1));
                            }
                        }
                    }
                }
                else
                {
                    this.breath += 3;
                    if (this.breath > this.breathMax)
                    {
                        this.breath = this.breathMax;
                    }
                    this.breathCD = 0;
                }
            }
            if ((flag5 && (Main.rand.Next(20) == 0)) && !this.lavaWet)
            {
                int num73 = 0;
                if (this.gravDir == -1f)
                {
                    num73 += this.height - 12;
                }
                if (this.inventory[this.selectedItem].type == 0xba)
                {
                    color = new Color();
                    Dust.NewDust(new Vector2((this.position.X + (10 * this.direction)) + 4f, (this.position.Y + num73) - (54f * this.gravDir)), this.width - 8, 8, 0x22, 0f, 0f, 0, color, 1.2f);
                }
                else
                {
                    color = new Color();
                    Dust.NewDust(new Vector2(this.position.X + (12 * this.direction), (this.position.Y + num73) + (4f * this.gravDir)), this.width - 8, 8, 0x22, 0f, 0f, 0, color, 1.2f);
                }
            }

            //Mod: added some collisions
            int pIndex = -1;

            for (int p = 0; p < 0xff; p++) //get the player's index in the main class
            {
                if (Main.player[p].name == name)
                    pIndex = p;
            }

            if (pIndex != -1 && Collision.PortalCollision(this.position, this.width, this.height, 0, pIndex))
            {
                //TODO: some kind of particle effect
            }


            bool flag6 = Collision.LavaCollision(this.position, this.width, this.height);
            if (flag6)
            {
                if (!this.lavaImmune && (Main.myPlayer == i))
                {
                    this.Hurt(100, 0, false, false, getDeathMessage(-1, -1, -1, 2));
                }
                this.lavaWet = true;
            }
            if (Collision.WetCollision(this.position, this.width, this.height))
            {
                if (!this.wet)
                {
                    if (this.wetCount == 0)
                    {
                        this.wetCount = 10;
                        if (!flag6)
                        {
                            for (int num74 = 0; num74 < 50; num74++)
                            {
                                color = new Color();
                                int num75 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                                Main.dust[num75].velocity.Y -= 4f;
                                Main.dust[num75].velocity.X *= 2.5f;
                                Main.dust[num75].scale = 1.3f;
                                Main.dust[num75].alpha = 100;
                                Main.dust[num75].noGravity = true;
                            }
                            Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                        }
                        else
                        {
                            for (int num76 = 0; num76 < 20; num76++)
                            {
                                color = new Color();
                                int num77 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                                Main.dust[num77].velocity.Y -= 1.5f;
                                Main.dust[num77].velocity.X *= 2.5f;
                                Main.dust[num77].scale = 1.3f;
                                Main.dust[num77].alpha = 100;
                                Main.dust[num77].noGravity = true;
                            }
                            Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                        }
                    }
                    this.wet = true;
                }
            }
            else if (this.wet)
            {
                this.wet = false;
                if (this.jump > (jumpHeight / 5))
                {
                    this.jump = jumpHeight / 5;
                }
                if (this.wetCount == 0)
                {
                    this.wetCount = 10;
                    if (!this.lavaWet)
                    {
                        for (int num78 = 0; num78 < 50; num78++)
                        {
                            color = new Color();
                            int num79 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (this.height / 2)), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                            Main.dust[num79].velocity.Y -= 4f;
                            Main.dust[num79].velocity.X *= 2.5f;
                            Main.dust[num79].scale = 1.3f;
                            Main.dust[num79].alpha = 100;
                            Main.dust[num79].noGravity = true;
                        }
                        Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                    }
                    else
                    {
                        for (int num80 = 0; num80 < 20; num80++)
                        {
                            color = new Color();
                            int num81 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                            Main.dust[num81].velocity.Y -= 1.5f;
                            Main.dust[num81].velocity.X *= 2.5f;
                            Main.dust[num81].scale = 1.3f;
                            Main.dust[num81].alpha = 100;
                            Main.dust[num81].noGravity = true;
                        }
                        Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                    }
                }
            }
            if (!this.wet)
            {
                this.lavaWet = false;
            }
            if (this.wetCount > 0)
            {
                this.wetCount = (byte) (this.wetCount - 1);
            }
            if (this.wet)
            {
                Vector2 velocity = this.velocity;
                this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
                Vector2 vector4 = (Vector2) (this.velocity * 0.5f);
                if (this.velocity.X != velocity.X)
                {
                    vector4.X = this.velocity.X;
                }
                if (this.velocity.Y != velocity.Y)
                {
                    vector4.Y = this.velocity.Y;
                }
                this.position += vector4;
            }
            else
            {
                this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
                if (this.waterWalk)
                {
                    this.velocity = Collision.WaterCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
                }
                this.position += this.velocity;
            }
            if (this.velocity.Y == 0f)
            {
                if ((this.gravDir == 1f) && Collision.up)
                {
                    this.velocity.Y = 0.01f;
                    this.jump = 0;
                }
                else if ((this.gravDir == -1f) && Collision.down)
                {
                    this.velocity.Y = -0.01f;
                    this.jump = 0;
                }
            }
            if (this.position.X < ((Main.leftWorld + 336f) + 16f))
            {
                this.position.X = (Main.leftWorld + 336f) + 16f;
                this.velocity.X = 0f;
            }
            if ((this.position.X + this.width) > ((Main.rightWorld - 336f) - 32f))
            {
                this.position.X = ((Main.rightWorld - 336f) - 32f) - this.width;
                this.velocity.X = 0f;
            }
            if (this.position.Y < ((Main.topWorld + 336f) + 16f))
            {
                this.position.Y = (Main.topWorld + 336f) + 16f;
                if (this.velocity.Y < -0.1)
                {
                    this.velocity.Y = -0.1f;
                }
            }
            if (this.position.Y > (((Main.bottomWorld - 336f) - 32f) - this.height))
            {
                this.position.Y = ((Main.bottomWorld - 336f) - 32f) - this.height;
                this.velocity.Y = 0f;
            }
            if (Main.ignoreErrors)
            {
                try
                {
                    this.ItemCheck(i);
                    goto Label_4E43;
                }
                catch
                {
                    goto Label_4E43;
                }
            }
            this.ItemCheck(i);
        Label_4E43:
            this.PlayerFrame();
            if (this.statLife > this.statLifeMax)
            {
                this.statLife = this.statLifeMax;
            }
            this.grappling[0] = -1;
            this.grapCount = 0;
        }
    }
}

