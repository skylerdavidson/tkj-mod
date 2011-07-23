namespace Terraria
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using System;
    using System.Runtime.InteropServices;

    public class NPC
    {
        public bool active;
        public static int sWidth = 0x690;
        public static int sHeight = 0x41a;
        private static int activeRangeX = ((int) (sWidth * 1.7));
        private static int activeRangeY = ((int) (sHeight * 1.7));
        private static int activeTime = 750;
        public float[] ai = new float[maxAI];
        public int aiAction;
        public int aiStyle;
        public int alpha;
        public bool behindTiles;
        public bool boss;
        public bool closeDoor;
        public bool collideX;
        public bool collideY;
        public Color color;
        public int damage;
        private static int defaultMaxSpawns = 20;
        private static int defaultSpawnRate = 500;
        public int defense;
        public int direction = 1;
        public int directionY = 1;
        public int doorX;
        public int doorY;
        public static bool downedBoss1 = false;
        public static bool downedBoss2 = false;
        public static bool downedBoss3 = false;
        public Rectangle frame;
        public double frameCounter;
        public bool friendly;
        public int friendlyRegen;
        public int height;
        public bool homeless;
        public int homeTileX = -1;
        public int homeTileY = -1;
        public int[] immune = new int[0x100];
        public static int immuneTime = 20;
        public float knockBackResist = 1f;
        public bool lavaImmune;
        public bool lavaWet;
        public int life;
        public int lifeMax;
        public static int maxAI = 4;
        private static int maxSpawns = defaultMaxSpawns;
        public string name;
        public bool netUpdate;
        public bool noGravity;
        private static bool noSpawnCycle = false;
        public bool noTileCollide;
        private static float npcSlots = 1f;
        public int oldDirection;
        public int oldDirectionY;
        public Vector2 oldPosition;
        public int oldTarget;
        public Vector2 oldVelocity;
        public Vector2 position;
        public float rotation;
        public static int safeRangeX = ((int) ((sWidth / 0x10) * 0.52));
        public static int safeRangeY = ((int) ((sHeight / 0x10) * 0.52));
        public float scale = 1f;
        public int soundDelay;
        public int soundHit;
        public int soundKilled;
        private static int spawnRangeX = ((int) ((sWidth / 0x10) * 0.7));
        private static int spawnRangeY = ((int) ((sHeight / 0x10) * 0.7));
        private static int spawnRate = defaultSpawnRate;
        private static int spawnSpaceX = 3;
        private static int spawnSpaceY = 3;
        public int spriteDirection = -1;
        public int target = -1;
        public Rectangle targetRect;
        public int timeLeft;
        public bool townNPC;
        private static int townRangeX = sWidth;
        private static int townRangeY = sHeight;
        public int type;
        public float value;
        public Vector2 velocity;
        public bool wet;
        public byte wetCount;
        public int whoAmI;
        public int width;

        public void AI()
        {
            Color color;
            if (this.aiStyle == 0)
            {
                this.velocity.X *= 0.93f;
                if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                {
                    this.velocity.X = 0f;
                    return;
                }
                return;
            }
            if (this.aiStyle == 1)
            {
                if (this.type == 0x3b)
                {
                    int index = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 1.7f);
                    Main.dust[index].noGravity = true;
                }
                if (this.wet)
                {
                    if (this.type == 0x3b)
                    {
                        if (this.velocity.Y > 2f)
                        {
                            this.velocity.Y *= 0.9f;
                        }
                        else if (this.directionY < 0)
                        {
                            this.velocity.Y -= 0.8f;
                        }
                        this.velocity.Y -= 0.5f;
                        if (this.velocity.Y < -10f)
                        {
                            this.velocity.Y = -10f;
                        }
                    }
                    else
                    {
                        if (this.velocity.Y > 2f)
                        {
                            this.velocity.Y *= 0.9f;
                        }
                        this.velocity.Y -= 0.5f;
                        if (this.velocity.Y < -4f)
                        {
                            this.velocity.Y = -4f;
                        }
                    }
                    this.TargetClosest(true);
                }
                this.aiAction = 0;
                if (this.ai[2] == 0f)
                {
                    this.ai[0] = -100f;
                    this.ai[2] = 1f;
                    this.TargetClosest(true);
                }
                if (this.velocity.Y != 0f)
                {
                    if ((this.target < 0xff) && (((this.direction == 1) && (this.velocity.X < 3f)) || ((this.direction == -1) && (this.velocity.X > -3f))))
                    {
                        if (((this.direction == -1) && (this.velocity.X < 0.1)) || ((this.direction == 1) && (this.velocity.X > -0.1)))
                        {
                            this.velocity.X += 0.2f * this.direction;
                            return;
                        }
                        this.velocity.X *= 0.93f;
                        return;
                    }
                }
                else
                {
                    if (this.ai[3] == this.position.X)
                    {
                        this.direction *= -1;
                    }
                    this.ai[3] = 0f;
                    this.velocity.X *= 0.8f;
                    if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                    {
                        this.velocity.X = 0f;
                    }
                    if ((!Main.dayTime || (this.life != this.lifeMax)) || (this.position.Y > (Main.worldSurface * 16.0)))
                    {
                        this.ai[0]++;
                    }
                    this.ai[0]++;
                    if (this.type == 0x3b)
                    {
                        this.ai[0] += 2f;
                    }
                    if (this.ai[0] >= 0f)
                    {
                        this.netUpdate = true;
                        if ((!Main.dayTime || (this.life != this.lifeMax)) || (this.position.Y > (Main.worldSurface * 16.0)))
                        {
                            this.TargetClosest(true);
                        }
                        if (this.ai[1] == 2f)
                        {
                            this.velocity.Y = -8f;
                            if (this.type == 0x3b)
                            {
                                this.velocity.Y -= 2f;
                            }
                            this.velocity.X += 3 * this.direction;
                            if (this.type == 0x3b)
                            {
                                this.velocity.X += 0.5f * this.direction;
                            }
                            this.ai[0] = -200f;
                            this.ai[1] = 0f;
                            this.ai[3] = this.position.X;
                            return;
                        }
                        this.velocity.Y = -6f;
                        this.velocity.X += 2 * this.direction;
                        if (this.type == 0x3b)
                        {
                            this.velocity.X += 2 * this.direction;
                        }
                        this.ai[0] = -120f;
                        this.ai[1]++;
                        return;
                    }
                    if (this.ai[0] >= -30f)
                    {
                        this.aiAction = 1;
                        return;
                    }
                }
                return;
            }
            if (this.aiStyle == 2)
            {
                this.noGravity = true;
                if (this.collideX)
                {
                    this.velocity.X = this.oldVelocity.X * -0.5f;
                    if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                    {
                        this.velocity.X = 2f;
                    }
                    if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                    {
                        this.velocity.X = -2f;
                    }
                }
                if (this.collideY)
                {
                    this.velocity.Y = this.oldVelocity.Y * -0.5f;
                    if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                    {
                        this.velocity.Y = 1f;
                    }
                    if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                    {
                        this.velocity.Y = -1f;
                    }
                }
                if ((Main.dayTime && (this.position.Y <= (Main.worldSurface * 16.0))) && (this.type == 2))
                {
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                    }
                    this.directionY = -1;
                    if (this.velocity.Y > 0f)
                    {
                        this.direction = 1;
                    }
                    this.direction = -1;
                    if (this.velocity.X > 0f)
                    {
                        this.direction = 1;
                    }
                }
                else
                {
                    this.TargetClosest(true);
                }
                if ((this.direction == -1) && (this.velocity.X > -4f))
                {
                    this.velocity.X -= 0.1f;
                    if (this.velocity.X > 4f)
                    {
                        this.velocity.X -= 0.1f;
                    }
                    else if (this.velocity.X > 0f)
                    {
                        this.velocity.X += 0.05f;
                    }
                    if (this.velocity.X < -4f)
                    {
                        this.velocity.X = -4f;
                    }
                }
                else if ((this.direction == 1) && (this.velocity.X < 4f))
                {
                    this.velocity.X += 0.1f;
                    if (this.velocity.X < -4f)
                    {
                        this.velocity.X += 0.1f;
                    }
                    else if (this.velocity.X < 0f)
                    {
                        this.velocity.X -= 0.05f;
                    }
                    if (this.velocity.X > 4f)
                    {
                        this.velocity.X = 4f;
                    }
                }
                if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                {
                    this.velocity.Y -= 0.04f;
                    if (this.velocity.Y > 1.5)
                    {
                        this.velocity.Y -= 0.05f;
                    }
                    else if (this.velocity.Y > 0f)
                    {
                        this.velocity.Y += 0.03f;
                    }
                    if (this.velocity.Y < -1.5)
                    {
                        this.velocity.Y = -1.5f;
                    }
                }
                else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                {
                    this.velocity.Y += 0.04f;
                    if (this.velocity.Y < -1.5)
                    {
                        this.velocity.Y += 0.05f;
                    }
                    else if (this.velocity.Y < 0f)
                    {
                        this.velocity.Y -= 0.03f;
                    }
                    if (this.velocity.Y > 1.5)
                    {
                        this.velocity.Y = 1.5f;
                    }
                }
                if ((this.type == 2) && (Main.rand.Next(40) == 0))
                {
                    int num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, new Color(), 1f);
                    Main.dust[num2].velocity.X *= 0.5f;
                    Main.dust[num2].velocity.Y *= 0.1f;
                }
                if (this.wet)
                {
                    if (this.velocity.Y > 0f)
                    {
                        this.velocity.Y *= 0.95f;
                    }
                    this.velocity.Y -= 0.5f;
                    if (this.velocity.Y < -4f)
                    {
                        this.velocity.Y = -4f;
                    }
                    this.TargetClosest(true);
                    return;
                }
                return;
            }
            if (this.aiStyle == 3)
            {
                int num3 = 60;
                bool flag = false;
                if ((this.velocity.Y == 0f) && (((this.velocity.X > 0f) && (this.direction < 0)) || ((this.velocity.X < 0f) && (this.direction > 0))))
                {
                    flag = true;
                }
                if (((this.position.X == this.oldPosition.X) || (this.ai[3] >= num3)) || flag)
                {
                    this.ai[3]++;
                }
                else if ((Math.Abs(this.velocity.X) > 0.9) && (this.ai[3] > 0f))
                {
                    this.ai[3]--;
                }
                if (this.ai[3] > (num3 * 10))
                {
                    this.ai[3] = 0f;
                }
                if (this.ai[3] == num3)
                {
                    this.netUpdate = true;
                }
                if ((((!Main.dayTime || (this.position.Y > (Main.worldSurface * 16.0))) || ((this.type == 0x1a) || (this.type == 0x1b))) || (((this.type == 0x1c) || (this.type == 0x1f)) || ((this.type == 0x2f) || (this.type == 0x43)))) && (this.ai[3] < num3))
                {
                    if ((((this.type == 3) || (this.type == 0x15)) || (this.type == 0x1f)) && (Main.rand.Next(0x3e8) == 0))
                    {
                        Main.PlaySound(14, (int) this.position.X, (int) this.position.Y, 1);
                    }
                    this.TargetClosest(true);
                }
                else
                {
                    if ((Main.dayTime && ((this.position.Y / 16f) < Main.worldSurface)) && (this.timeLeft > 10))
                    {
                        this.timeLeft = 10;
                    }
                    if (this.velocity.X == 0f)
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.ai[0]++;
                            if (this.ai[0] >= 2f)
                            {
                                this.direction *= -1;
                                this.spriteDirection = this.direction;
                                this.ai[0] = 0f;
                            }
                        }
                    }
                    else
                    {
                        this.ai[0] = 0f;
                    }
                    if (this.direction == 0)
                    {
                        this.direction = 1;
                    }
                }
                if (this.type == 0x1b)
                {
                    if ((this.velocity.X < -2f) || (this.velocity.X > 2f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.8f);
                        }
                    }
                    else if ((this.velocity.X < 2f) && (this.direction == 1))
                    {
                        this.velocity.X += 0.07f;
                        if (this.velocity.X > 2f)
                        {
                            this.velocity.X = 2f;
                        }
                    }
                    else if ((this.velocity.X > -2f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.07f;
                        if (this.velocity.X < -2f)
                        {
                            this.velocity.X = -2f;
                        }
                    }
                }
                else if (((this.type == 0x15) || (this.type == 0x1a)) || ((this.type == 0x1f) || (this.type == 0x2f)))
                {
                    if ((this.velocity.X < -1.5f) || (this.velocity.X > 1.5f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.8f);
                        }
                    }
                    else if ((this.velocity.X < 1.5f) && (this.direction == 1))
                    {
                        this.velocity.X += 0.07f;
                        if (this.velocity.X > 1.5f)
                        {
                            this.velocity.X = 1.5f;
                        }
                    }
                    else if ((this.velocity.X > -1.5f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.07f;
                        if (this.velocity.X < -1.5f)
                        {
                            this.velocity.X = -1.5f;
                        }
                    }
                }
                else if (this.type == 0x43)
                {
                    if ((this.velocity.X < -0.5f) || (this.velocity.X > 0.5f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.7f);
                        }
                    }
                    else if ((this.velocity.X < 0.5f) && (this.direction == 1))
                    {
                        this.velocity.X += 0.03f;
                        if (this.velocity.X > 0.5f)
                        {
                            this.velocity.X = 0.5f;
                        }
                    }
                    else if ((this.velocity.X > -0.5f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.03f;
                        if (this.velocity.X < -0.5f)
                        {
                            this.velocity.X = -0.5f;
                        }
                    }
                }
                else if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                {
                    if (this.velocity.Y == 0f)
                    {
                        this.velocity = (Vector2) (this.velocity * 0.8f);
                    }
                }
                else if ((this.velocity.X < 1f) && (this.direction == 1))
                {
                    this.velocity.X += 0.07f;
                    if (this.velocity.X > 1f)
                    {
                        this.velocity.X = 1f;
                    }
                }
                else if ((this.velocity.X > -1f) && (this.direction == -1))
                {
                    this.velocity.X -= 0.07f;
                    if (this.velocity.X < -1f)
                    {
                        this.velocity.X = -1f;
                    }
                }
                if (this.velocity.Y != 0f)
                {
                    this.ai[1] = 0f;
                    this.ai[2] = 0f;
                    return;
                }
                int i = (int) (((this.position.X + (this.width / 2)) + (15 * this.direction)) / 16f);
                int j = (int) (((this.position.Y + this.height) - 15f) / 16f);
                if (Main.tile[i, j] == null)
                {
                    Main.tile[i, j] = new Tile();
                }
                if (Main.tile[i, j - 1] == null)
                {
                    Main.tile[i, j - 1] = new Tile();
                }
                if (Main.tile[i, j - 2] == null)
                {
                    Main.tile[i, j - 2] = new Tile();
                }
                if (Main.tile[i, j - 3] == null)
                {
                    Main.tile[i, j - 3] = new Tile();
                }
                if (Main.tile[i, j + 1] == null)
                {
                    Main.tile[i, j + 1] = new Tile();
                }
                if (Main.tile[i + this.direction, j - 1] == null)
                {
                    Main.tile[i + this.direction, j - 1] = new Tile();
                }
                if (Main.tile[i + this.direction, j + 1] == null)
                {
                    Main.tile[i + this.direction, j + 1] = new Tile();
                }
                bool flag2 = true;
                if ((this.type == 0x2f) || (this.type == 0x43))
                {
                    flag2 = false;
                }
                if ((!Main.tile[i, j - 1].active || (Main.tile[i, j - 1].type != 10)) || !flag2)
                {
                    if (((this.velocity.X < 0f) && (this.spriteDirection == -1)) || ((this.velocity.X > 0f) && (this.spriteDirection == 1)))
                    {
                        if (Main.tile[i, j - 2].active && Main.tileSolid[Main.tile[i, j - 2].type])
                        {
                            if (Main.tile[i, j - 3].active && Main.tileSolid[Main.tile[i, j - 3].type])
                            {
                                this.velocity.Y = -8f;
                                this.netUpdate = true;
                            }
                            else
                            {
                                this.velocity.Y = -7f;
                                this.netUpdate = true;
                            }
                        }
                        else if (Main.tile[i, j - 1].active && Main.tileSolid[Main.tile[i, j - 1].type])
                        {
                            this.velocity.Y = -6f;
                            this.netUpdate = true;
                        }
                        else if (Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type])
                        {
                            this.velocity.Y = -5f;
                            this.netUpdate = true;
                        }
                        else if ((((this.directionY < 0) && (this.type != 0x43)) && (!Main.tile[i, j + 1].active || !Main.tileSolid[Main.tile[i, j + 1].type])) && (!Main.tile[i + this.direction, j + 1].active || !Main.tileSolid[Main.tile[i + this.direction, j + 1].type]))
                        {
                            this.velocity.Y = -8f;
                            this.velocity.X *= 1.5f;
                            this.netUpdate = true;
                        }
                        else
                        {
                            this.ai[1] = 0f;
                            this.ai[2] = 0f;
                        }
                    }
                    if ((((this.type == 0x1f) || (this.type == 0x2f)) && (((this.velocity.Y == 0f) && (Math.Abs((float) ((this.position.X + (this.width / 2)) - (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))) < 100f)) && (Math.Abs((float) ((this.position.Y + (this.height / 2)) - (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)))) < 50f))) && (((this.direction > 0) && (this.velocity.X >= 1f)) || ((this.direction < 0) && (this.velocity.X <= -1f))))
                    {
                        this.velocity.X *= 2f;
                        if (this.velocity.X > 3f)
                        {
                            this.velocity.X = 3f;
                        }
                        if (this.velocity.X < -3f)
                        {
                            this.velocity.X = -3f;
                        }
                        this.velocity.Y = -4f;
                        this.netUpdate = true;
                        return;
                    }
                }
                else
                {
                    this.ai[2]++;
                    this.ai[3] = 0f;
                    if (this.ai[2] >= 60f)
                    {
                        if (!Main.bloodMoon && (this.type == 3))
                        {
                            this.ai[1] = 0f;
                        }
                        this.velocity.X = 0.5f * -this.direction;
                        this.ai[1]++;
                        if (this.type == 0x1b)
                        {
                            this.ai[1]++;
                        }
                        if (this.type == 0x1f)
                        {
                            this.ai[1] += 6f;
                        }
                        this.ai[2] = 0f;
                        bool flag3 = false;
                        if (this.ai[1] >= 10f)
                        {
                            flag3 = true;
                            this.ai[1] = 10f;
                        }
                        WorldGen.KillTile(i, j - 1, true, false, false);
                        if (((Main.netMode != 1) || !flag3) && (flag3 && (Main.netMode != 1)))
                        {
                            if (this.type != 0x1a)
                            {
                                bool flag4 = WorldGen.OpenDoor(i, j, this.direction);
                                if (!flag4)
                                {
                                    this.ai[3] = num3;
                                    this.netUpdate = true;
                                }
                                if ((Main.netMode == 2) && flag4)
                                {
                                    NetMessage.SendData(0x13, -1, -1, "", 0, (float) i, (float) j, (float) this.direction, 0);
                                    return;
                                }
                            }
                            else
                            {
                                WorldGen.KillTile(i, j - 1, false, false, false);
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 0, (float) i, (float) (j - 1), 0f, 0);
                                    return;
                                }
                            }
                        }
                    }
                }
                return;
            }
            if (this.aiStyle == 4)
            {
                if (((this.target < 0) || (this.target == 0xff)) || (Main.player[this.target].dead || !Main.player[this.target].active))
                {
                    this.TargetClosest(true);
                }
                bool dead = Main.player[this.target].dead;
                float num6 = ((this.position.X + (this.width / 2)) - Main.player[this.target].position.X) - (Main.player[this.target].width / 2);
                float num7 = (((this.position.Y + this.height) - 59f) - Main.player[this.target].position.Y) - (Main.player[this.target].height / 2);
                float num8 = ((float) Math.Atan2((double) num7, (double) num6)) + 1.57f;
                if (num8 < 0f)
                {
                    num8 += 6.283f;
                }
                else if (num8 > 6.283)
                {
                    num8 -= 6.283f;
                }
                float num9 = 0f;
                if ((this.ai[0] == 0f) && (this.ai[1] == 0f))
                {
                    num9 = 0.02f;
                }
                if (((this.ai[0] == 0f) && (this.ai[1] == 2f)) && (this.ai[2] > 40f))
                {
                    num9 = 0.05f;
                }
                if ((this.ai[0] == 3f) && (this.ai[1] == 0f))
                {
                    num9 = 0.05f;
                }
                if (((this.ai[0] == 3f) && (this.ai[1] == 2f)) && (this.ai[2] > 40f))
                {
                    num9 = 0.08f;
                }
                if (this.rotation < num8)
                {
                    if ((num8 - this.rotation) > 3.1415)
                    {
                        this.rotation -= num9;
                    }
                    else
                    {
                        this.rotation += num9;
                    }
                }
                else if (this.rotation > num8)
                {
                    if ((this.rotation - num8) > 3.1415)
                    {
                        this.rotation += num9;
                    }
                    else
                    {
                        this.rotation -= num9;
                    }
                }
                if ((this.rotation > (num8 - num9)) && (this.rotation < (num8 + num9)))
                {
                    this.rotation = num8;
                }
                if (this.rotation < 0f)
                {
                    this.rotation += 6.283f;
                }
                else if (this.rotation > 6.283)
                {
                    this.rotation -= 6.283f;
                }
                if ((this.rotation > (num8 - num9)) && (this.rotation < (num8 + num9)))
                {
                    this.rotation = num8;
                }
                if (Main.rand.Next(5) == 0)
                {
                    color = new Color();
                    int num10 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, color, 1f);
                    Main.dust[num10].velocity.X *= 0.5f;
                    Main.dust[num10].velocity.Y *= 0.1f;
                }
                if (!Main.dayTime && !dead)
                {
                    if (this.ai[0] != 0f)
                    {
                        if ((this.ai[0] != 1f) && (this.ai[0] != 2f))
                        {
                            this.damage = 30;
                            this.defense = 6;
                            if (this.ai[1] != 0f)
                            {
                                if (this.ai[1] == 1f)
                                {
                                    Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                                    this.rotation = num8;
                                    float num34 = 8f;
                                    Vector2 vector6 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    float num35 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector6.X;
                                    float num36 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector6.Y;
                                    float num37 = (float) Math.Sqrt((double) ((num35 * num35) + (num36 * num36)));
                                    num37 = num34 / num37;
                                    this.velocity.X = num35 * num37;
                                    this.velocity.Y = num36 * num37;
                                    this.ai[1] = 2f;
                                    return;
                                }
                                if (this.ai[1] == 2f)
                                {
                                    this.ai[2]++;
                                    if (this.ai[2] >= 40f)
                                    {
                                        this.velocity.X *= 0.97f;
                                        this.velocity.Y *= 0.97f;
                                        if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                                        {
                                            this.velocity.X = 0f;
                                        }
                                        if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                                        {
                                            this.velocity.Y = 0f;
                                        }
                                    }
                                    else
                                    {
                                        this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                                    }
                                    if (this.ai[2] >= 100f)
                                    {
                                        this.ai[3]++;
                                        this.ai[2] = 0f;
                                        this.target = 0xff;
                                        this.rotation = num8;
                                        if (this.ai[3] >= 3f)
                                        {
                                            this.ai[1] = 0f;
                                            this.ai[3] = 0f;
                                            return;
                                        }
                                        this.ai[1] = 1f;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                float num29 = 6f;
                                float num30 = 0.07f;
                                Vector2 vector5 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                float num31 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector5.X;
                                float num32 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - 120f) - vector5.Y;
                                float num33 = (float) Math.Sqrt((double) ((num31 * num31) + (num32 * num32)));
                                num33 = num29 / num33;
                                num31 *= num33;
                                num32 *= num33;
                                if (this.velocity.X < num31)
                                {
                                    this.velocity.X += num30;
                                    if ((this.velocity.X < 0f) && (num31 > 0f))
                                    {
                                        this.velocity.X += num30;
                                    }
                                }
                                else if (this.velocity.X > num31)
                                {
                                    this.velocity.X -= num30;
                                    if ((this.velocity.X > 0f) && (num31 < 0f))
                                    {
                                        this.velocity.X -= num30;
                                    }
                                }
                                if (this.velocity.Y < num32)
                                {
                                    this.velocity.Y += num30;
                                    if ((this.velocity.Y < 0f) && (num32 > 0f))
                                    {
                                        this.velocity.Y += num30;
                                    }
                                }
                                else if (this.velocity.Y > num32)
                                {
                                    this.velocity.Y -= num30;
                                    if ((this.velocity.Y > 0f) && (num32 < 0f))
                                    {
                                        this.velocity.Y -= num30;
                                    }
                                }
                                this.ai[2]++;
                                if (this.ai[2] >= 200f)
                                {
                                    this.ai[1] = 1f;
                                    this.ai[2] = 0f;
                                    this.ai[3] = 0f;
                                    this.target = 0xff;
                                    this.netUpdate = true;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (this.ai[0] == 1f)
                            {
                                this.ai[2] += 0.005f;
                                if (this.ai[2] > 0.5)
                                {
                                    this.ai[2] = 0.5f;
                                }
                            }
                            else
                            {
                                this.ai[2] -= 0.005f;
                                if (this.ai[2] < 0f)
                                {
                                    this.ai[2] = 0f;
                                }
                            }
                            this.rotation += this.ai[2];
                            this.ai[1]++;
                            if (this.ai[1] == 100f)
                            {
                                this.ai[0]++;
                                this.ai[1] = 0f;
                                if (this.ai[0] == 3f)
                                {
                                    this.ai[2] = 0f;
                                }
                                else
                                {
                                    Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, 1);
                                    for (int k = 0; k < 2; k++)
                                    {
                                        Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 8);
                                        Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 7);
                                        Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 6);
                                    }
                                    for (int m = 0; m < 20; m++)
                                    {
                                        color = new Color();
                                        Dust.NewDust(this.position, this.width, this.height, 5, Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f, 0, color, 1f);
                                    }
                                    Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                                }
                            }
                            Dust.NewDust(this.position, this.width, this.height, 5, Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f, 0, new Color(), 1f);
                            this.velocity.X *= 0.98f;
                            this.velocity.Y *= 0.98f;
                            if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                            {
                                this.velocity.X = 0f;
                            }
                            if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                            {
                                this.velocity.Y = 0f;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (this.ai[1] == 0f)
                        {
                            float num11 = 5f;
                            float num12 = 0.04f;
                            Vector2 vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num13 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                            float num14 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - 200f) - vector.Y;
                            float num15 = (float) Math.Sqrt((double) ((num13 * num13) + (num14 * num14)));
                            float num16 = num15;
                            num15 = num11 / num15;
                            num13 *= num15;
                            num14 *= num15;
                            if (this.velocity.X < num13)
                            {
                                this.velocity.X += num12;
                                if ((this.velocity.X < 0f) && (num13 > 0f))
                                {
                                    this.velocity.X += num12;
                                }
                            }
                            else if (this.velocity.X > num13)
                            {
                                this.velocity.X -= num12;
                                if ((this.velocity.X > 0f) && (num13 < 0f))
                                {
                                    this.velocity.X -= num12;
                                }
                            }
                            if (this.velocity.Y < num14)
                            {
                                this.velocity.Y += num12;
                                if ((this.velocity.Y < 0f) && (num14 > 0f))
                                {
                                    this.velocity.Y += num12;
                                }
                            }
                            else if (this.velocity.Y > num14)
                            {
                                this.velocity.Y -= num12;
                                if ((this.velocity.Y > 0f) && (num14 < 0f))
                                {
                                    this.velocity.Y -= num12;
                                }
                            }
                            this.ai[2]++;
                            if (this.ai[2] >= 600f)
                            {
                                this.ai[1] = 1f;
                                this.ai[2] = 0f;
                                this.ai[3] = 0f;
                                this.target = 0xff;
                                this.netUpdate = true;
                            }
                            else if (((this.position.Y + this.height) < Main.player[this.target].position.Y) && (num16 < 500f))
                            {
                                if (!Main.player[this.target].dead)
                                {
                                    this.ai[3]++;
                                }
                                if (this.ai[3] >= 90f)
                                {
                                    Vector2 vector3;
                                    this.ai[3] = 0f;
                                    this.rotation = num8;
                                    float num17 = 5f;
                                    float num18 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                                    float num19 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                                    float num20 = (float) Math.Sqrt((double) ((num18 * num18) + (num19 * num19)));
                                    num20 = num17 / num20;
                                    Vector2 position = vector;
                                    vector3.X = num18 * num20;
                                    vector3.Y = num19 * num20;
                                    position.X += vector3.X * 10f;
                                    position.Y += vector3.Y * 10f;
                                    if (Main.netMode != 1)
                                    {
                                        int num21 = NewNPC((int) position.X, (int) position.Y, 5, 0);
                                        Main.npc[num21].velocity.X = vector3.X;
                                        Main.npc[num21].velocity.Y = vector3.Y;
                                        if ((Main.netMode == 2) && (num21 < 0x3e8))
                                        {
                                            NetMessage.SendData(0x17, -1, -1, "", num21, 0f, 0f, 0f, 0);
                                        }
                                    }
                                    Main.PlaySound(3, (int) position.X, (int) position.Y, 1);
                                    for (int n = 0; n < 10; n++)
                                    {
                                        color = new Color();
                                        Dust.NewDust(position, 20, 20, 5, vector3.X * 0.4f, vector3.Y * 0.4f, 0, color, 1f);
                                    }
                                }
                            }
                        }
                        else if (this.ai[1] == 1f)
                        {
                            this.rotation = num8;
                            float num23 = 7f;
                            Vector2 vector4 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num24 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector4.X;
                            float num25 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector4.Y;
                            float num26 = (float) Math.Sqrt((double) ((num24 * num24) + (num25 * num25)));
                            num26 = num23 / num26;
                            this.velocity.X = num24 * num26;
                            this.velocity.Y = num25 * num26;
                            this.ai[1] = 2f;
                        }
                        else if (this.ai[1] == 2f)
                        {
                            this.ai[2]++;
                            if (this.ai[2] >= 40f)
                            {
                                this.velocity.X *= 0.98f;
                                this.velocity.Y *= 0.98f;
                                if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                                {
                                    this.velocity.X = 0f;
                                }
                                if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                                {
                                    this.velocity.Y = 0f;
                                }
                            }
                            else
                            {
                                this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                            }
                            if (this.ai[2] >= 120f)
                            {
                                this.ai[3]++;
                                this.ai[2] = 0f;
                                this.target = 0xff;
                                this.rotation = num8;
                                if (this.ai[3] >= 3f)
                                {
                                    this.ai[1] = 0f;
                                    this.ai[3] = 0f;
                                }
                                else
                                {
                                    this.ai[1] = 1f;
                                }
                            }
                        }
                        if (this.life < (this.lifeMax * 0.5))
                        {
                            this.ai[0] = 1f;
                            this.ai[1] = 0f;
                            this.ai[2] = 0f;
                            this.ai[3] = 0f;
                            this.netUpdate = true;
                            return;
                        }
                    }
                }
                else
                {
                    this.velocity.Y -= 0.04f;
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                        return;
                    }
                }
                return;
            }
            if (this.aiStyle == 5)
            {
                if (((this.target < 0) || (this.target == 0xff)) || Main.player[this.target].dead)
                {
                    this.TargetClosest(true);
                }
                float num38 = 6f;
                float num39 = 0.05f;
                if ((this.type == 6) || (this.type == 0x2a))
                {
                    num38 = 4f;
                    num39 = 0.02f;
                }
                else if (this.type == 0x17)
                {
                    num38 = 2f;
                    num39 = 0.03f;
                }
                Vector2 vector7 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                float num40 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector7.X;
                float num41 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector7.Y;
                float num42 = (float) Math.Sqrt((double) ((num40 * num40) + (num41 * num41)));
                float num43 = num42;
                num42 = num38 / num42;
                num40 *= num42;
                num41 *= num42;
                if ((this.type == 6) || (this.type == 0x2a))
                {
                    if (num43 > 100f)
                    {
                        this.ai[0]++;
                        if (this.ai[0] > 0f)
                        {
                            this.velocity.Y += 0.023f;
                        }
                        else
                        {
                            this.velocity.Y -= 0.023f;
                        }
                        if ((this.ai[0] < -100f) || (this.ai[0] > 100f))
                        {
                            this.velocity.X += 0.023f;
                        }
                        else
                        {
                            this.velocity.X -= 0.023f;
                        }
                        if (this.ai[0] > 200f)
                        {
                            this.ai[0] = -200f;
                        }
                    }
                    if (num43 < 150f)
                    {
                        this.velocity.X += num40 * 0.007f;
                        this.velocity.Y += num41 * 0.007f;
                    }
                }
                if (Main.player[this.target].dead)
                {
                    num40 = (this.direction * num38) / 2f;
                    num41 = -num38 / 2f;
                }
                if (this.velocity.X < num40)
                {
                    this.velocity.X += num39;
                    if (((this.type != 6) && (this.velocity.X < 0f)) && (num40 > 0f))
                    {
                        this.velocity.X += num39;
                    }
                }
                else if (this.velocity.X > num40)
                {
                    this.velocity.X -= num39;
                    if (((this.type != 6) && (this.velocity.X > 0f)) && (num40 < 0f))
                    {
                        this.velocity.X -= num39;
                    }
                }
                if (this.velocity.Y < num41)
                {
                    this.velocity.Y += num39;
                    if (((this.type != 6) && (this.velocity.Y < 0f)) && (num41 > 0f))
                    {
                        this.velocity.Y += num39;
                    }
                }
                else if (this.velocity.Y > num41)
                {
                    this.velocity.Y -= num39;
                    if (((this.type != 6) && (this.velocity.Y > 0f)) && (num41 < 0f))
                    {
                        this.velocity.Y -= num39;
                    }
                }
                if (this.type == 0x17)
                {
                    if (num40 > 0f)
                    {
                        this.spriteDirection = 1;
                        this.rotation = (float) Math.Atan2((double) num41, (double) num40);
                    }
                    else if (num40 < 0f)
                    {
                        this.spriteDirection = -1;
                        this.rotation = ((float) Math.Atan2((double) num41, (double) num40)) + 3.14f;
                    }
                }
                else if (this.type == 6)
                {
                    this.rotation = ((float) Math.Atan2((double) num41, (double) num40)) - 1.57f;
                }
                else if (this.type == 0x2a)
                {
                    if (this.velocity.X > 0f)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.velocity.X < 0f)
                    {
                        this.spriteDirection = -1;
                    }
                    this.rotation = this.velocity.X * 0.1f;
                }
                else
                {
                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                }
                if (((this.type == 6) || (this.type == 0x17)) || (this.type == 0x2a))
                {
                    float num44 = 0.7f;
                    if (this.type == 6)
                    {
                        num44 = 0.4f;
                    }
                    if (this.collideX)
                    {
                        this.netUpdate = true;
                        this.velocity.X = this.oldVelocity.X * -num44;
                        if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                        {
                            this.velocity.X = 2f;
                        }
                        if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                        {
                            this.velocity.X = -2f;
                        }
                        this.netUpdate = true;
                    }
                    if (this.collideY)
                    {
                        this.netUpdate = true;
                        this.velocity.Y = this.oldVelocity.Y * -num44;
                        if ((this.velocity.Y > 0f) && (this.velocity.Y < 1.5))
                        {
                            this.velocity.Y = 2f;
                        }
                        if ((this.velocity.Y < 0f) && (this.velocity.Y > -1.5))
                        {
                            this.velocity.Y = -2f;
                        }
                    }
                    if (this.type == 0x17)
                    {
                        int num45 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                        Main.dust[num45].noGravity = true;
                        Main.dust[num45].velocity.X *= 0.3f;
                        Main.dust[num45].velocity.Y *= 0.3f;
                    }
                    else if (Main.rand.Next(20) == 0)
                    {
                        int num46 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 0x12, this.velocity.X, 2f, this.alpha, this.color, this.scale);
                        Main.dust[num46].velocity.X *= 0.5f;
                        Main.dust[num46].velocity.Y *= 0.1f;
                    }
                }
                else if (Main.rand.Next(40) == 0)
                {
                    int num47 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, new Color(), 1f);
                    Main.dust[num47].velocity.X *= 0.5f;
                    Main.dust[num47].velocity.Y *= 0.1f;
                }
                if (((Main.dayTime && (this.type != 6)) && ((this.type != 0x17) && (this.type != 0x2a))) || Main.player[this.target].dead)
                {
                    this.velocity.Y -= num39 * 2f;
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                        return;
                    }
                }
                return;
            }
            if (this.aiStyle != 6)
            {
                if (this.aiStyle == 7)
                {
                    int num69 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                    int num70 = ((int) ((this.position.Y + this.height) + 1f)) / 0x10;
                    if ((Main.netMode == 1) || !this.townNPC)
                    {
                        this.homeTileX = num69;
                        this.homeTileY = num70;
                    }
                    if ((this.type == 0x2e) && (this.target == 0xff))
                    {
                        this.TargetClosest(true);
                    }
                    bool flag8 = false;
                    this.directionY = -1;
                    if (this.direction == 0)
                    {
                        this.direction = 1;
                    }
                    for (int num71 = 0; num71 < 0xff; num71++)
                    {
                        if (Main.player[num71].active && (Main.player[num71].talkNPC == this.whoAmI))
                        {
                            flag8 = true;
                            if (this.ai[0] != 0f)
                            {
                                this.netUpdate = true;
                            }
                            this.ai[0] = 0f;
                            this.ai[1] = 300f;
                            this.ai[2] = 100f;
                            if ((Main.player[num71].position.X + (Main.player[num71].width / 2)) < (this.position.X + (this.width / 2)))
                            {
                                this.direction = -1;
                            }
                            else
                            {
                                this.direction = 1;
                            }
                        }
                    }
                    if (this.ai[3] > 0f)
                    {
                        this.life = -1;
                        this.HitEffect(0, 10.0);
                        this.active = false;
                        if (this.type == 0x25)
                        {
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        }
                    }
                    if ((this.type == 0x25) && (Main.netMode != 1))
                    {
                        this.homeless = false;
                        this.homeTileX = Main.dungeonX;
                        this.homeTileY = Main.dungeonY;
                        if (downedBoss3)
                        {
                            this.ai[3] = 1f;
                            this.netUpdate = true;
                        }
                    }
                    if (((((Main.netMode != 1) && this.townNPC) && !Main.dayTime) && ((num69 != this.homeTileX) || (num70 != this.homeTileY))) && !this.homeless)
                    {
                        bool flag9 = true;
                        for (int num72 = 0; num72 < 2; num72++)
                        {
                            Rectangle rectangle = new Rectangle(((((int) this.position.X) + (this.width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) this.position.Y) + (this.height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                            if (num72 == 1)
                            {
                                rectangle = new Rectangle((((this.homeTileX * 0x10) + 8) - (sWidth / 2)) - safeRangeX, (((this.homeTileY * 0x10) + 8) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                            }
                            for (int num73 = 0; num73 < 0xff; num73++)
                            {
                                if (Main.player[num73].active)
                                {
                                    Rectangle rectangle2 = new Rectangle((int) Main.player[num73].position.X, (int) Main.player[num73].position.Y, Main.player[num73].width, Main.player[num73].height);
                                    if (rectangle2.Intersects(rectangle))
                                    {
                                        flag9 = false;
                                        break;
                                    }
                                }
                                if (!flag9)
                                {
                                    break;
                                }
                            }
                        }
                        if (flag9)
                        {
                            if ((this.type == 0x25) || !Collision.SolidTiles(this.homeTileX - 1, this.homeTileX + 1, this.homeTileY - 3, this.homeTileY - 1))
                            {
                                this.velocity.X = 0f;
                                this.velocity.Y = 0f;
                                this.position.X = ((this.homeTileX * 0x10) + 8) - (this.width / 2);
                                this.position.Y = ((this.homeTileY * 0x10) - this.height) - 0.1f;
                                this.netUpdate = true;
                            }
                            else
                            {
                                this.homeless = true;
                                WorldGen.QuickFindHome(this.whoAmI);
                            }
                        }
                    }
                    if (this.ai[0] != 0f)
                    {
                        if (this.ai[0] == 1f)
                        {
                            if (((Main.netMode != 1) && !Main.dayTime) && ((num69 == this.homeTileX) && (num70 == this.homeTileY)))
                            {
                                this.ai[0] = 0f;
                                this.ai[1] = 200 + Main.rand.Next(200);
                                this.ai[2] = 60f;
                                this.netUpdate = true;
                                return;
                            }
                            if (((Main.netMode != 1) && !this.homeless) && ((num69 < (this.homeTileX - 0x23)) || (num69 > (this.homeTileX + 0x23))))
                            {
                                if ((this.position.X < (this.homeTileX * 0x10)) && (this.direction == -1))
                                {
                                    this.direction = 1;
                                    this.velocity.X = 0.1f;
                                    this.netUpdate = true;
                                }
                                else if ((this.position.X > (this.homeTileX * 0x10)) && (this.direction == 1))
                                {
                                    this.direction = -1;
                                    this.velocity.X = -0.1f;
                                    this.netUpdate = true;
                                }
                            }
                            this.ai[1]--;
                            if (this.ai[1] <= 0f)
                            {
                                this.ai[0] = 0f;
                                this.ai[1] = 300 + Main.rand.Next(300);
                                if (this.type == 0x2e)
                                {
                                    this.ai[1] -= Main.rand.Next(100);
                                }
                                this.ai[2] = 60f;
                                this.netUpdate = true;
                            }
                            if (this.closeDoor && ((((this.position.X + (this.width / 2)) / 16f) > (this.doorX + 2)) || (((this.position.X + (this.width / 2)) / 16f) < (this.doorX - 2))))
                            {
                                if (WorldGen.CloseDoor(this.doorX, this.doorY, false))
                                {
                                    this.closeDoor = false;
                                    NetMessage.SendData(0x13, -1, -1, "", 1, (float) this.doorX, (float) this.doorY, (float) this.direction, 0);
                                }
                                if (((((this.position.X + (this.width / 2)) / 16f) > (this.doorX + 4)) || (((this.position.X + (this.width / 2)) / 16f) < (this.doorX - 4))) || ((((this.position.Y + (this.height / 2)) / 16f) > (this.doorY + 4)) || (((this.position.Y + (this.height / 2)) / 16f) < (this.doorY - 4))))
                                {
                                    this.closeDoor = false;
                                }
                            }
                            if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                            {
                                if (this.velocity.Y == 0f)
                                {
                                    this.velocity = (Vector2) (this.velocity * 0.8f);
                                }
                            }
                            else if ((this.velocity.X < 1.15) && (this.direction == 1))
                            {
                                this.velocity.X += 0.07f;
                                if (this.velocity.X > 1f)
                                {
                                    this.velocity.X = 1f;
                                }
                            }
                            else if ((this.velocity.X > -1f) && (this.direction == -1))
                            {
                                this.velocity.X -= 0.07f;
                                if (this.velocity.X > 1f)
                                {
                                    this.velocity.X = 1f;
                                }
                            }
                            if (this.velocity.Y == 0f)
                            {
                                if (this.position.X == this.ai[2])
                                {
                                    this.direction *= -1;
                                }
                                this.ai[2] = -1f;
                                int startX = (int) (((this.position.X + (this.width / 2)) + (15 * this.direction)) / 16f);
                                int num75 = (int) (((this.position.Y + this.height) - 16f) / 16f);
                                if (Main.tile[startX, num75] == null)
                                {
                                    Main.tile[startX, num75] = new Tile();
                                }
                                if (Main.tile[startX, num75 - 1] == null)
                                {
                                    Main.tile[startX, num75 - 1] = new Tile();
                                }
                                if (Main.tile[startX, num75 - 2] == null)
                                {
                                    Main.tile[startX, num75 - 2] = new Tile();
                                }
                                if (Main.tile[startX, num75 - 3] == null)
                                {
                                    Main.tile[startX, num75 - 3] = new Tile();
                                }
                                if (Main.tile[startX, num75 + 1] == null)
                                {
                                    Main.tile[startX, num75 + 1] = new Tile();
                                }
                                if (Main.tile[startX + this.direction, num75 - 1] == null)
                                {
                                    Main.tile[startX + this.direction, num75 - 1] = new Tile();
                                }
                                if (Main.tile[startX + this.direction, num75 + 1] == null)
                                {
                                    Main.tile[startX + this.direction, num75 + 1] = new Tile();
                                }
                                if (((!this.townNPC || !Main.tile[startX, num75 - 2].active) || (Main.tile[startX, num75 - 2].type != 10)) || ((Main.rand.Next(10) != 0) && Main.dayTime))
                                {
                                    if (((this.velocity.X < 0f) && (this.spriteDirection == -1)) || ((this.velocity.X > 0f) && (this.spriteDirection == 1)))
                                    {
                                        if ((Main.tile[startX, num75 - 2].active && Main.tileSolid[Main.tile[startX, num75 - 2].type]) && !Main.tileSolidTop[Main.tile[startX, num75 - 2].type])
                                        {
                                            if (((this.direction == 1) && !Collision.SolidTiles(startX - 2, startX - 1, num75 - 5, num75 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(startX + 1, startX + 2, num75 - 5, num75 - 1)))
                                            {
                                                if (!Collision.SolidTiles(startX, startX, num75 - 5, num75 - 3))
                                                {
                                                    this.velocity.Y = -6f;
                                                    this.netUpdate = true;
                                                }
                                                else
                                                {
                                                    this.direction *= -1;
                                                    this.netUpdate = true;
                                                }
                                            }
                                            else
                                            {
                                                this.direction *= -1;
                                                this.netUpdate = true;
                                            }
                                        }
                                        else if ((Main.tile[startX, num75 - 1].active && Main.tileSolid[Main.tile[startX, num75 - 1].type]) && !Main.tileSolidTop[Main.tile[startX, num75 - 1].type])
                                        {
                                            if (((this.direction == 1) && !Collision.SolidTiles(startX - 2, startX - 1, num75 - 4, num75 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(startX + 1, startX + 2, num75 - 4, num75 - 1)))
                                            {
                                                if (!Collision.SolidTiles(startX, startX, num75 - 4, num75 - 2))
                                                {
                                                    this.velocity.Y = -5f;
                                                    this.netUpdate = true;
                                                }
                                                else
                                                {
                                                    this.direction *= -1;
                                                    this.netUpdate = true;
                                                }
                                            }
                                            else
                                            {
                                                this.direction *= -1;
                                                this.netUpdate = true;
                                            }
                                        }
                                        else if ((Main.tile[startX, num75].active && Main.tileSolid[Main.tile[startX, num75].type]) && !Main.tileSolidTop[Main.tile[startX, num75].type])
                                        {
                                            if (((this.direction == 1) && !Collision.SolidTiles(startX - 2, startX, num75 - 3, num75 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(startX, startX + 2, num75 - 3, num75 - 1)))
                                            {
                                                this.velocity.Y = -3.6f;
                                                this.netUpdate = true;
                                            }
                                            else
                                            {
                                                this.direction *= -1;
                                                this.netUpdate = true;
                                            }
                                        }
                                        try
                                        {
                                            if (Main.tile[startX, num75 + 1] == null)
                                            {
                                                Main.tile[startX, num75 + 1] = new Tile();
                                            }
                                            if (Main.tile[startX - this.direction, num75 + 1] == null)
                                            {
                                                Main.tile[startX - this.direction, num75 + 1] = new Tile();
                                            }
                                            if (Main.tile[startX, num75 + 2] == null)
                                            {
                                                Main.tile[startX, num75 + 2] = new Tile();
                                            }
                                            if (Main.tile[startX - this.direction, num75 + 2] == null)
                                            {
                                                Main.tile[startX - this.direction, num75 + 2] = new Tile();
                                            }
                                            if (Main.tile[startX, num75 + 3] == null)
                                            {
                                                Main.tile[startX, num75 + 3] = new Tile();
                                            }
                                            if (Main.tile[startX - this.direction, num75 + 3] == null)
                                            {
                                                Main.tile[startX - this.direction, num75 + 3] = new Tile();
                                            }
                                            if (Main.tile[startX, num75 + 4] == null)
                                            {
                                                Main.tile[startX, num75 + 4] = new Tile();
                                            }
                                            if (Main.tile[startX - this.direction, num75 + 4] == null)
                                            {
                                                Main.tile[startX - this.direction, num75 + 4] = new Tile();
                                            }
                                            else if (((((((num69 >= (this.homeTileX - 0x23)) && (num69 <= (this.homeTileX + 0x23))) && (!Main.tile[startX, num75 + 1].active || !Main.tileSolid[Main.tile[startX, num75 + 1].type])) && (!Main.tile[startX - this.direction, num75 + 1].active || !Main.tileSolid[Main.tile[startX - this.direction, num75 + 1].type])) && ((!Main.tile[startX, num75 + 2].active || !Main.tileSolid[Main.tile[startX, num75 + 2].type]) && (!Main.tile[startX - this.direction, num75 + 2].active || !Main.tileSolid[Main.tile[startX - this.direction, num75 + 2].type]))) && (((!Main.tile[startX, num75 + 3].active || !Main.tileSolid[Main.tile[startX, num75 + 3].type]) && (!Main.tile[startX - this.direction, num75 + 3].active || !Main.tileSolid[Main.tile[startX - this.direction, num75 + 3].type])) && ((!Main.tile[startX, num75 + 4].active || !Main.tileSolid[Main.tile[startX, num75 + 4].type]) && (!Main.tile[startX - this.direction, num75 + 4].active || !Main.tileSolid[Main.tile[startX - this.direction, num75 + 4].type])))) && (this.type != 0x2e))
                                            {
                                                this.direction *= -1;
                                                this.velocity.X *= -1f;
                                                this.netUpdate = true;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        if (this.velocity.Y < 0f)
                                        {
                                            this.ai[2] = this.position.X;
                                        }
                                    }
                                    if ((this.velocity.Y < 0f) && this.wet)
                                    {
                                        this.velocity.Y *= 1.2f;
                                    }
                                    if ((this.velocity.Y < 0f) && (this.type == 0x2e))
                                    {
                                        this.velocity.Y *= 1.2f;
                                        return;
                                    }
                                }
                                else if (Main.netMode != 1)
                                {
                                    if (WorldGen.OpenDoor(startX, num75 - 2, this.direction))
                                    {
                                        this.closeDoor = true;
                                        this.doorX = startX;
                                        this.doorY = num75 - 2;
                                        NetMessage.SendData(0x13, -1, -1, "", 0, (float) startX, (float) (num75 - 2), (float) this.direction, 0);
                                        this.netUpdate = true;
                                        this.ai[1] += 80f;
                                        return;
                                    }
                                    if (WorldGen.OpenDoor(startX, num75 - 2, -this.direction))
                                    {
                                        this.closeDoor = true;
                                        this.doorX = startX;
                                        this.doorY = num75 - 2;
                                        NetMessage.SendData(0x13, -1, -1, "", 0, (float) startX, (float) (num75 - 2), (float) -this.direction, 0);
                                        this.netUpdate = true;
                                        this.ai[1] += 80f;
                                        return;
                                    }
                                    this.direction *= -1;
                                    this.netUpdate = true;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.ai[2] > 0f)
                        {
                            this.ai[2]--;
                        }
                        if (!Main.dayTime && !flag8)
                        {
                            if (Main.netMode != 1)
                            {
                                if ((num69 == this.homeTileX) && (num70 == this.homeTileY))
                                {
                                    if (this.velocity.X != 0f)
                                    {
                                        this.netUpdate = true;
                                    }
                                    if (this.velocity.X > 0.1)
                                    {
                                        this.velocity.X -= 0.1f;
                                    }
                                    else if (this.velocity.X < -0.1)
                                    {
                                        this.velocity.X += 0.1f;
                                    }
                                    else
                                    {
                                        this.velocity.X = 0f;
                                    }
                                }
                                else if (!flag8)
                                {
                                    if (num69 > this.homeTileX)
                                    {
                                        this.direction = -1;
                                    }
                                    else
                                    {
                                        this.direction = 1;
                                    }
                                    this.ai[0] = 1f;
                                    this.ai[1] = 200 + Main.rand.Next(200);
                                    this.ai[2] = 0f;
                                    this.netUpdate = true;
                                }
                            }
                        }
                        else
                        {
                            if (this.velocity.X > 0.1)
                            {
                                this.velocity.X -= 0.1f;
                            }
                            else if (this.velocity.X < -0.1)
                            {
                                this.velocity.X += 0.1f;
                            }
                            else
                            {
                                this.velocity.X = 0f;
                            }
                            if (Main.netMode != 1)
                            {
                                if (this.ai[1] > 0f)
                                {
                                    this.ai[1]--;
                                }
                                if (this.ai[1] <= 0f)
                                {
                                    this.ai[0] = 1f;
                                    this.ai[1] = 200 + Main.rand.Next(200);
                                    if (this.type == 0x2e)
                                    {
                                        this.ai[1] += Main.rand.Next(200, 400);
                                    }
                                    this.ai[2] = 0f;
                                    this.netUpdate = true;
                                }
                            }
                        }
                        if ((Main.netMode != 1) && (Main.dayTime || ((num69 == this.homeTileX) && (num70 == this.homeTileY))))
                        {
                            if ((num69 >= (this.homeTileX - 0x19)) && (num69 <= (this.homeTileX + 0x19)))
                            {
                                if ((Main.rand.Next(80) == 0) && (this.ai[2] == 0f))
                                {
                                    this.ai[2] = 200f;
                                    this.direction *= -1;
                                    this.netUpdate = true;
                                    return;
                                }
                            }
                            else if (this.ai[2] == 0f)
                            {
                                if ((num69 < (this.homeTileX - 50)) && (this.direction == -1))
                                {
                                    this.direction = 1;
                                    this.netUpdate = true;
                                    return;
                                }
                                if ((num69 > (this.homeTileX + 50)) && (this.direction == 1))
                                {
                                    this.direction = -1;
                                    this.netUpdate = true;
                                    return;
                                }
                            }
                        }
                    }
                    return;
                }
                if (this.aiStyle != 8)
                {
                    if (this.aiStyle == 9)
                    {
                        if (this.target == 0xff)
                        {
                            this.TargetClosest(true);
                            float num96 = 6f;
                            if (this.type == 30)
                            {
                                maxSpawns = 8;
                            }
                            Vector2 vector10 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num97 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector10.X;
                            float num98 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector10.Y;
                            float num99 = (float) Math.Sqrt((double) ((num97 * num97) + (num98 * num98)));
                            num99 = num96 / num99;
                            this.velocity.X = num97 * num99;
                            this.velocity.Y = num98 * num99;
                        }
                        if (this.timeLeft > 100)
                        {
                            this.timeLeft = 100;
                        }
                        for (int num100 = 0; num100 < 2; num100++)
                        {
                            if (this.type == 30)
                            {
                                color = new Color();
                                int num101 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1b, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num101].noGravity = true;
                                Dust dust7 = Main.dust[num101];
                                dust7.velocity = (Vector2) (dust7.velocity * 0.3f);
                                Main.dust[num101].velocity.X -= this.velocity.X * 0.2f;
                                Main.dust[num101].velocity.Y -= this.velocity.Y * 0.2f;
                            }
                            else if (this.type == 0x21)
                            {
                                color = new Color();
                                int num102 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1d, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num102].noGravity = true;
                                Main.dust[num102].velocity.X *= 0.3f;
                                Main.dust[num102].velocity.Y *= 0.3f;
                            }
                            else
                            {
                                color = new Color();
                                int num103 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num103].noGravity = true;
                                Main.dust[num103].velocity.X *= 0.3f;
                                Main.dust[num103].velocity.Y *= 0.3f;
                            }
                        }
                        this.rotation += 0.4f * this.direction;
                        return;
                    }
                    if (this.aiStyle == 10)
                    {
                        float num104 = 1f;
                        float num105 = 0.011f;
                        this.TargetClosest(true);
                        Vector2 vector11 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        float num106 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector11.X;
                        float num107 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector11.Y;
                        float num108 = (float) Math.Sqrt((double) ((num106 * num106) + (num107 * num107)));
                        float num109 = num108;
                        this.ai[1]++;
                        if (this.ai[1] > 1000f)
                        {
                            num105 *= 6f;
                            num104 = 5f;
                            if (this.ai[1] > 1030f)
                            {
                                this.ai[1] = 0f;
                            }
                        }
                        if (num109 < 300f)
                        {
                            this.ai[0] += 0.9f;
                            if (this.ai[0] > 0f)
                            {
                                this.velocity.Y += 0.019f;
                            }
                            else
                            {
                                this.velocity.Y -= 0.019f;
                            }
                            if ((this.ai[0] < -100f) || (this.ai[0] > 100f))
                            {
                                this.velocity.X += 0.019f;
                            }
                            else
                            {
                                this.velocity.X -= 0.019f;
                            }
                            if (this.ai[0] > 200f)
                            {
                                this.ai[0] = -200f;
                            }
                        }
                        else if (num109 < 350f)
                        {
                            num104 = 1.2f;
                            num105 = 0.04f;
                        }
                        else if (num109 < 400f)
                        {
                            num104 = 2.5f;
                            num105 = 0.04f;
                        }
                        else
                        {
                            num104 = 4f;
                            num105 = 0.06f;
                        }
                        num108 = num104 / num108;
                        num106 *= num108;
                        num107 *= num108;
                        if (Main.player[this.target].dead)
                        {
                            num106 = (this.direction * num104) / 2f;
                            num107 = -num104 / 2f;
                        }
                        if (this.velocity.X < num106)
                        {
                            this.velocity.X += num105;
                        }
                        else if (this.velocity.X > num106)
                        {
                            this.velocity.X -= num105;
                        }
                        if (this.velocity.Y < num107)
                        {
                            this.velocity.Y += num105;
                        }
                        else if (this.velocity.Y > num107)
                        {
                            this.velocity.Y -= num105;
                        }
                        if (num106 > 0f)
                        {
                            this.spriteDirection = -1;
                            this.rotation = (float) Math.Atan2((double) num107, (double) num106);
                        }
                        if (num106 < 0f)
                        {
                            this.spriteDirection = 1;
                            this.rotation = ((float) Math.Atan2((double) num107, (double) num106)) + 3.14f;
                            return;
                        }
                    }
                    else if (this.aiStyle == 11)
                    {
                        if ((this.ai[0] == 0f) && (Main.netMode != 1))
                        {
                            this.TargetClosest(true);
                            this.ai[0] = 1f;
                            if (this.type != 0x44)
                            {
                                int num110 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + (this.height / 2), 0x24, this.whoAmI);
                                Main.npc[num110].ai[0] = -1f;
                                Main.npc[num110].ai[1] = this.whoAmI;
                                Main.npc[num110].target = this.target;
                                Main.npc[num110].netUpdate = true;
                                num110 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + (this.height / 2), 0x24, this.whoAmI);
                                Main.npc[num110].ai[0] = 1f;
                                Main.npc[num110].ai[1] = this.whoAmI;
                                Main.npc[num110].ai[3] = 150f;
                                Main.npc[num110].target = this.target;
                                Main.npc[num110].netUpdate = true;
                            }
                        }
                        if (((this.type == 0x44) && (this.ai[1] != 3f)) && (this.ai[1] != 2f))
                        {
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                            this.ai[1] = 2f;
                        }
                        if ((Main.player[this.target].dead || (Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) > 2000f)) || (Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y)) > 2000f))
                        {
                            this.TargetClosest(true);
                            if ((Main.player[this.target].dead || (Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) > 2000f)) || (Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y)) > 2000f))
                            {
                                this.ai[1] = 3f;
                            }
                        }
                        if ((Main.dayTime && (this.ai[1] != 3f)) && (this.ai[1] != 2f))
                        {
                            this.ai[1] = 2f;
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        }
                        if (this.ai[1] == 0f)
                        {
                            this.ai[2]++;
                            if (this.ai[2] >= 800f)
                            {
                                this.ai[2] = 0f;
                                this.ai[1] = 1f;
                                this.TargetClosest(true);
                                this.netUpdate = true;
                            }
                            this.rotation = this.velocity.X / 15f;
                            if (this.position.Y > (Main.player[this.target].position.Y - 250f))
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y *= 0.98f;
                                }
                                this.velocity.Y -= 0.02f;
                                if (this.velocity.Y > 2f)
                                {
                                    this.velocity.Y = 2f;
                                }
                            }
                            else if (this.position.Y < (Main.player[this.target].position.Y - 250f))
                            {
                                if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y *= 0.98f;
                                }
                                this.velocity.Y += 0.02f;
                                if (this.velocity.Y < -2f)
                                {
                                    this.velocity.Y = -2f;
                                }
                            }
                            if ((this.position.X + (this.width / 2)) > (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))
                            {
                                if (this.velocity.X > 0f)
                                {
                                    this.velocity.X *= 0.98f;
                                }
                                this.velocity.X -= 0.05f;
                                if (this.velocity.X > 8f)
                                {
                                    this.velocity.X = 8f;
                                }
                            }
                            if ((this.position.X + (this.width / 2)) < (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))
                            {
                                if (this.velocity.X < 0f)
                                {
                                    this.velocity.X *= 0.98f;
                                }
                                this.velocity.X += 0.05f;
                                if (this.velocity.X < -8f)
                                {
                                    this.velocity.X = -8f;
                                }
                            }
                        }
                        else if (this.ai[1] == 1f)
                        {
                            this.ai[2]++;
                            if (this.ai[2] == 2f)
                            {
                                Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                            }
                            if (this.ai[2] >= 400f)
                            {
                                this.ai[2] = 0f;
                                this.ai[1] = 0f;
                            }
                            this.rotation += this.direction * 0.3f;
                            Vector2 vector12 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num111 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector12.X;
                            float num112 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector12.Y;
                            float num113 = (float) Math.Sqrt((double) ((num111 * num111) + (num112 * num112)));
                            num113 = 2f / num113;
                            this.velocity.X = num111 * num113;
                            this.velocity.Y = num112 * num113;
                        }
                        else if (this.ai[1] == 2f)
                        {
                            this.damage = 0x270f;
                            this.defense = 0x270f;
                            this.rotation += this.direction * 0.3f;
                            Vector2 vector13 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num114 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector13.X;
                            float num115 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector13.Y;
                            float num116 = (float) Math.Sqrt((double) ((num114 * num114) + (num115 * num115)));
                            num116 = 8f / num116;
                            this.velocity.X = num114 * num116;
                            this.velocity.Y = num115 * num116;
                        }
                        else if (this.ai[1] == 3f)
                        {
                            this.velocity.Y -= 0.1f;
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y *= 0.95f;
                            }
                            this.velocity.X *= 0.95f;
                            if (this.timeLeft > 50)
                            {
                                this.timeLeft = 50;
                            }
                        }
                        if (((this.ai[1] != 2f) && (this.ai[1] != 3f)) && (this.type != 0x44))
                        {
                            color = new Color();
                            int num117 = Dust.NewDust(new Vector2(((this.position.X + (this.width / 2)) - 15f) - (this.velocity.X * 5f), (this.position.Y + this.height) - 2f), 30, 10, 5, -this.velocity.X * 0.2f, 3f, 0, color, 2f);
                            Main.dust[num117].noGravity = true;
                            Main.dust[num117].velocity.X *= 1.3f;
                            Main.dust[num117].velocity.X += this.velocity.X * 0.4f;
                            Main.dust[num117].velocity.Y += 2f + this.velocity.Y;
                            for (int num118 = 0; num118 < 2; num118++)
                            {
                                color = new Color();
                                num117 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 120f), this.width, 60, 5, this.velocity.X, this.velocity.Y, 0, color, 2f);
                                Main.dust[num117].noGravity = true;
                                Dust dust8 = Main.dust[num117];
                                dust8.velocity -= this.velocity;
                                Main.dust[num117].velocity.Y += 5f;
                            }
                            return;
                        }
                    }
                    else if (this.aiStyle == 12)
                    {
                        this.spriteDirection = -((int) this.ai[0]);
                        if (!Main.npc[(int) this.ai[1]].active || (Main.npc[(int) this.ai[1]].aiStyle != 11))
                        {
                            this.ai[2] += 10f;
                            if ((this.ai[2] > 50f) || (Main.netMode != 2))
                            {
                                this.life = -1;
                                this.HitEffect(0, 10.0);
                                this.active = false;
                            }
                        }
                        if ((this.ai[2] == 0f) || (this.ai[2] == 3f))
                        {
                            if ((Main.npc[(int) this.ai[1]].ai[1] == 3f) && (this.timeLeft > 10))
                            {
                                this.timeLeft = 10;
                            }
                            if (Main.npc[(int) this.ai[1]].ai[1] != 0f)
                            {
                                if (this.position.Y > (Main.npc[(int) this.ai[1]].position.Y - 100f))
                                {
                                    if (this.velocity.Y > 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y -= 0.07f;
                                    if (this.velocity.Y > 6f)
                                    {
                                        this.velocity.Y = 6f;
                                    }
                                }
                                else if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y - 100f))
                                {
                                    if (this.velocity.Y < 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y += 0.07f;
                                    if (this.velocity.Y < -6f)
                                    {
                                        this.velocity.Y = -6f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (120f * this.ai[0])))
                                {
                                    if (this.velocity.X > 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X -= 0.1f;
                                    if (this.velocity.X > 8f)
                                    {
                                        this.velocity.X = 8f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (120f * this.ai[0])))
                                {
                                    if (this.velocity.X < 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X += 0.1f;
                                    if (this.velocity.X < -8f)
                                    {
                                        this.velocity.X = -8f;
                                    }
                                }
                            }
                            else
                            {
                                this.ai[3]++;
                                if (this.ai[3] >= 300f)
                                {
                                    this.ai[2]++;
                                    this.ai[3] = 0f;
                                    this.netUpdate = true;
                                }
                                if (this.position.Y > (Main.npc[(int) this.ai[1]].position.Y + 230f))
                                {
                                    if (this.velocity.Y > 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y -= 0.04f;
                                    if (this.velocity.Y > 3f)
                                    {
                                        this.velocity.Y = 3f;
                                    }
                                }
                                else if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y + 230f))
                                {
                                    if (this.velocity.Y < 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y += 0.04f;
                                    if (this.velocity.Y < -3f)
                                    {
                                        this.velocity.Y = -3f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])))
                                {
                                    if (this.velocity.X > 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X -= 0.07f;
                                    if (this.velocity.X > 8f)
                                    {
                                        this.velocity.X = 8f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])))
                                {
                                    if (this.velocity.X < 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X += 0.07f;
                                    if (this.velocity.X < -8f)
                                    {
                                        this.velocity.X = -8f;
                                    }
                                }
                            }
                            Vector2 vector14 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num119 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector14.X;
                            float num120 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector14.Y;
                            Math.Sqrt((double) ((num119 * num119) + (num120 * num120)));
                            this.rotation = ((float) Math.Atan2((double) num120, (double) num119)) + 1.57f;
                            return;
                        }
                        if (this.ai[2] != 1f)
                        {
                            if (this.ai[2] != 2f)
                            {
                                if (this.ai[2] != 4f)
                                {
                                    if ((this.ai[2] == 5f) && (((this.velocity.X > 0f) && ((this.position.X + (this.width / 2)) > (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))) || ((this.velocity.X < 0f) && ((this.position.X + (this.width / 2)) < (Main.player[this.target].position.X + (Main.player[this.target].width / 2))))))
                                    {
                                        this.ai[2] = 0f;
                                        return;
                                    }
                                }
                                else
                                {
                                    Vector2 vector16 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    float num124 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector16.X;
                                    float num125 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector16.Y;
                                    float num126 = (float) Math.Sqrt((double) ((num124 * num124) + (num125 * num125)));
                                    this.rotation = ((float) Math.Atan2((double) num125, (double) num124)) + 1.57f;
                                    this.velocity.Y *= 0.95f;
                                    this.velocity.X += 0.1f * -this.ai[0];
                                    if (this.velocity.X < -8f)
                                    {
                                        this.velocity.X = -8f;
                                    }
                                    if (this.velocity.X > 8f)
                                    {
                                        this.velocity.X = 8f;
                                    }
                                    if (((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - 500f)) || ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) + 500f)))
                                    {
                                        this.TargetClosest(true);
                                        this.ai[2] = 5f;
                                        vector16 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                        num124 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector16.X;
                                        num125 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector16.Y;
                                        num126 = (float) Math.Sqrt((double) ((num124 * num124) + (num125 * num125)));
                                        num126 = 20f / num126;
                                        this.velocity.X = num124 * num126;
                                        this.velocity.Y = num125 * num126;
                                        this.netUpdate = true;
                                        return;
                                    }
                                }
                            }
                            else if ((this.position.Y > Main.player[this.target].position.Y) || (this.velocity.Y < 0f))
                            {
                                this.ai[2] = 3f;
                                return;
                            }
                        }
                        else
                        {
                            Vector2 vector15 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            float num121 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector15.X;
                            float num122 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector15.Y;
                            float num123 = (float) Math.Sqrt((double) ((num121 * num121) + (num122 * num122)));
                            this.rotation = ((float) Math.Atan2((double) num122, (double) num121)) + 1.57f;
                            this.velocity.X *= 0.95f;
                            this.velocity.Y -= 0.1f;
                            if (this.velocity.Y < -8f)
                            {
                                this.velocity.Y = -8f;
                            }
                            if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y - 200f))
                            {
                                this.TargetClosest(true);
                                this.ai[2] = 2f;
                                vector15 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                num121 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector15.X;
                                num122 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector15.Y;
                                num123 = (float) Math.Sqrt((double) ((num121 * num121) + (num122 * num122)));
                                num123 = 20f / num123;
                                this.velocity.X = num121 * num123;
                                this.velocity.Y = num122 * num123;
                                this.netUpdate = true;
                                return;
                            }
                        }
                    }
                    else if (this.aiStyle == 13)
                    {
                        if (Main.tile[(int) this.ai[0], (int) this.ai[1]] == null)
                        {
                            Main.tile[(int) this.ai[0], (int) this.ai[1]] = new Tile();
                        }
                        if (!Main.tile[(int) this.ai[0], (int) this.ai[1]].active)
                        {
                            this.life = -1;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                            return;
                        }
                        this.TargetClosest(true);
                        float num127 = 0.05f;
                        float num128 = 150f;
                        if (this.type == 0x2b)
                        {
                            num128 = 200f;
                        }
                        Vector2 vector17 = new Vector2((this.ai[0] * 16f) + 8f, (this.ai[1] * 16f) + 8f);
                        float num129 = ((Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - (this.width / 2)) - vector17.X;
                        float num130 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - (this.height / 2)) - vector17.Y;
                        float num131 = (float) Math.Sqrt((double) ((num129 * num129) + (num130 * num130)));
                        if (num131 > num128)
                        {
                            num131 = num128 / num131;
                            num129 *= num131;
                            num130 *= num131;
                        }
                        if (this.position.X < (((this.ai[0] * 16f) + 8f) + num129))
                        {
                            this.velocity.X += num127;
                            if ((this.velocity.X < 0f) && (num129 > 0f))
                            {
                                this.velocity.X += num127 * 2f;
                            }
                        }
                        else if (this.position.X > (((this.ai[0] * 16f) + 8f) + num129))
                        {
                            this.velocity.X -= num127;
                            if ((this.velocity.X > 0f) && (num129 < 0f))
                            {
                                this.velocity.X -= num127 * 2f;
                            }
                        }
                        if (this.position.Y < (((this.ai[1] * 16f) + 8f) + num130))
                        {
                            this.velocity.Y += num127;
                            if ((this.velocity.Y < 0f) && (num130 > 0f))
                            {
                                this.velocity.Y += num127 * 2f;
                            }
                        }
                        else if (this.position.Y > (((this.ai[1] * 16f) + 8f) + num130))
                        {
                            this.velocity.Y -= num127;
                            if ((this.velocity.Y > 0f) && (num130 < 0f))
                            {
                                this.velocity.Y -= num127 * 2f;
                            }
                        }
                        if (this.type == 0x2b)
                        {
                            if (this.velocity.X > 3f)
                            {
                                this.velocity.X = 3f;
                            }
                            if (this.velocity.X < -3f)
                            {
                                this.velocity.X = -3f;
                            }
                            if (this.velocity.Y > 3f)
                            {
                                this.velocity.Y = 3f;
                            }
                            if (this.velocity.Y < -3f)
                            {
                                this.velocity.Y = -3f;
                            }
                        }
                        else
                        {
                            if (this.velocity.X > 2f)
                            {
                                this.velocity.X = 2f;
                            }
                            if (this.velocity.X < -2f)
                            {
                                this.velocity.X = -2f;
                            }
                            if (this.velocity.Y > 2f)
                            {
                                this.velocity.Y = 2f;
                            }
                            if (this.velocity.Y < -2f)
                            {
                                this.velocity.Y = -2f;
                            }
                        }
                        if (num129 > 0f)
                        {
                            this.spriteDirection = 1;
                            this.rotation = (float) Math.Atan2((double) num130, (double) num129);
                        }
                        if (num129 < 0f)
                        {
                            this.spriteDirection = -1;
                            this.rotation = ((float) Math.Atan2((double) num130, (double) num129)) + 3.14f;
                        }
                        if (this.collideX)
                        {
                            this.netUpdate = true;
                            this.velocity.X = this.oldVelocity.X * -0.7f;
                            if ((this.velocity.X > 0f) && (this.velocity.X < 2f))
                            {
                                this.velocity.X = 2f;
                            }
                            if ((this.velocity.X < 0f) && (this.velocity.X > -2f))
                            {
                                this.velocity.X = -2f;
                            }
                        }
                        if (this.collideY)
                        {
                            this.netUpdate = true;
                            this.velocity.Y = this.oldVelocity.Y * -0.7f;
                            if ((this.velocity.Y > 0f) && (this.velocity.Y < 2f))
                            {
                                this.velocity.Y = 2f;
                            }
                            if ((this.velocity.Y < 0f) && (this.velocity.Y > -2f))
                            {
                                this.velocity.Y = -2f;
                                return;
                            }
                        }
                    }
                    else if (this.aiStyle == 14)
                    {
                        if (this.type == 60)
                        {
                            int num132 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                            Main.dust[num132].noGravity = true;
                        }
                        this.noGravity = true;
                        if (this.collideX)
                        {
                            this.velocity.X = this.oldVelocity.X * -0.5f;
                            if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                            {
                                this.velocity.X = 2f;
                            }
                            if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                            {
                                this.velocity.X = -2f;
                            }
                        }
                        if (this.collideY)
                        {
                            this.velocity.Y = this.oldVelocity.Y * -0.5f;
                            if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                            {
                                this.velocity.Y = 1f;
                            }
                            if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                            {
                                this.velocity.Y = -1f;
                            }
                        }
                        this.TargetClosest(true);
                        if ((this.direction == -1) && (this.velocity.X > -4f))
                        {
                            this.velocity.X -= 0.1f;
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X -= 0.1f;
                            }
                            else if (this.velocity.X > 0f)
                            {
                                this.velocity.X += 0.05f;
                            }
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X = -4f;
                            }
                        }
                        else if ((this.direction == 1) && (this.velocity.X < 4f))
                        {
                            this.velocity.X += 0.1f;
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X += 0.1f;
                            }
                            else if (this.velocity.X < 0f)
                            {
                                this.velocity.X -= 0.05f;
                            }
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X = 4f;
                            }
                        }
                        if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                        {
                            this.velocity.Y -= 0.04f;
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y -= 0.05f;
                            }
                            else if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y += 0.03f;
                            }
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y = -1.5f;
                            }
                        }
                        else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                        {
                            this.velocity.Y += 0.04f;
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y += 0.05f;
                            }
                            else if (this.velocity.Y < 0f)
                            {
                                this.velocity.Y -= 0.03f;
                            }
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y = 1.5f;
                            }
                        }
                        if (((this.type == 0x31) || (this.type == 0x33)) || (this.type == 60))
                        {
                            if (this.wet)
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y *= 0.95f;
                                }
                                this.velocity.Y -= 0.5f;
                                if (this.velocity.Y < -4f)
                                {
                                    this.velocity.Y = -4f;
                                }
                                this.TargetClosest(true);
                            }
                            if ((this.direction == -1) && (this.velocity.X > -4f))
                            {
                                this.velocity.X -= 0.1f;
                                if (this.velocity.X > 4f)
                                {
                                    this.velocity.X -= 0.1f;
                                }
                                else if (this.velocity.X > 0f)
                                {
                                    this.velocity.X += 0.05f;
                                }
                                if (this.velocity.X < -4f)
                                {
                                    this.velocity.X = -4f;
                                }
                            }
                            else if ((this.direction == 1) && (this.velocity.X < 4f))
                            {
                                this.velocity.X += 0.1f;
                                if (this.velocity.X < -4f)
                                {
                                    this.velocity.X += 0.1f;
                                }
                                else if (this.velocity.X < 0f)
                                {
                                    this.velocity.X -= 0.05f;
                                }
                                if (this.velocity.X > 4f)
                                {
                                    this.velocity.X = 4f;
                                }
                            }
                            if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                            {
                                this.velocity.Y -= 0.04f;
                                if (this.velocity.Y > 1.5)
                                {
                                    this.velocity.Y -= 0.05f;
                                }
                                else if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y += 0.03f;
                                }
                                if (this.velocity.Y < -1.5)
                                {
                                    this.velocity.Y = -1.5f;
                                }
                            }
                            else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                            {
                                this.velocity.Y += 0.04f;
                                if (this.velocity.Y < -1.5)
                                {
                                    this.velocity.Y += 0.05f;
                                }
                                else if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y -= 0.03f;
                                }
                                if (this.velocity.Y > 1.5)
                                {
                                    this.velocity.Y = 1.5f;
                                }
                            }
                        }
                        if (Main.netMode != 1)
                        {
                            if (this.type == 0x30)
                            {
                                this.ai[0]++;
                                if (((this.ai[0] == 30f) || (this.ai[0] == 60f)) || (this.ai[0] == 90f))
                                {
                                    float num133 = 6f;
                                    Vector2 vector18 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    float speedX = ((Main.player[this.target].position.X + (Main.player[this.target].width * 0.5f)) - vector18.X) + Main.rand.Next(-100, 0x65);
                                    float speedY = ((Main.player[this.target].position.Y + (Main.player[this.target].height * 0.5f)) - vector18.Y) + Main.rand.Next(-100, 0x65);
                                    float num136 = (float) Math.Sqrt((double) ((speedX * speedX) + (speedY * speedY)));
                                    num136 = num133 / num136;
                                    speedX *= num136;
                                    speedY *= num136;
                                    int damage = 15;
                                    int type = 0x26;
                                    int num139 = Projectile.NewProjectile(vector18.X, vector18.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
                                    Main.projectile[num139].timeLeft = 300;
                                }
                                else if (this.ai[0] >= (400 + Main.rand.Next(400)))
                                {
                                    this.ai[0] = 0f;
                                }
                            }
                            if ((this.type == 0x3e) || (this.type == 0x42))
                            {
                                this.ai[0]++;
                                if (((this.ai[0] == 20f) || (this.ai[0] == 40f)) || ((this.ai[0] == 60f) || (this.ai[0] == 80f)))
                                {
                                    float num140 = 0.2f;
                                    Vector2 vector19 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    float num141 = ((Main.player[this.target].position.X + (Main.player[this.target].width * 0.5f)) - vector19.X) + Main.rand.Next(-100, 0x65);
                                    float num142 = ((Main.player[this.target].position.Y + (Main.player[this.target].height * 0.5f)) - vector19.Y) + Main.rand.Next(-100, 0x65);
                                    float num143 = (float) Math.Sqrt((double) ((num141 * num141) + (num142 * num142)));
                                    num143 = num140 / num143;
                                    num141 *= num143;
                                    num142 *= num143;
                                    int num144 = 0x19;
                                    int num145 = 0x2c;
                                    int num146 = Projectile.NewProjectile(vector19.X, vector19.Y, num141, num142, num145, num144, 0f, Main.myPlayer);
                                    Main.projectile[num146].timeLeft = 300;
                                    return;
                                }
                                if (this.ai[0] >= (300 + Main.rand.Next(300)))
                                {
                                    this.ai[0] = 0f;
                                    return;
                                }
                            }
                        }
                    }
                    else if (this.aiStyle == 15)
                    {
                        this.aiAction = 0;
                        if ((this.ai[3] == 0f) && (this.life > 0))
                        {
                            this.ai[3] = this.lifeMax;
                        }
                        if (this.ai[2] == 0f)
                        {
                            this.ai[0] = -100f;
                            this.ai[2] = 1f;
                            this.TargetClosest(true);
                        }
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity.X *= 0.8f;
                            if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                            {
                                this.velocity.X = 0f;
                            }
                            this.ai[0] += 2f;
                            if (this.life < (this.lifeMax * 0.8))
                            {
                                this.ai[0]++;
                            }
                            if (this.life < (this.lifeMax * 0.6))
                            {
                                this.ai[0]++;
                            }
                            if (this.life < (this.lifeMax * 0.4))
                            {
                                this.ai[0] += 2f;
                            }
                            if (this.life < (this.lifeMax * 0.2))
                            {
                                this.ai[0] += 3f;
                            }
                            if (this.life < (this.lifeMax * 0.1))
                            {
                                this.ai[0] += 4f;
                            }
                            if (this.ai[0] >= 0f)
                            {
                                this.netUpdate = true;
                                this.TargetClosest(true);
                                if (this.ai[1] == 3f)
                                {
                                    this.velocity.Y = -13f;
                                    this.velocity.X += 3.5f * this.direction;
                                    this.ai[0] = -200f;
                                    this.ai[1] = 0f;
                                }
                                else if (this.ai[1] == 2f)
                                {
                                    this.velocity.Y = -6f;
                                    this.velocity.X += 4.5f * this.direction;
                                    this.ai[0] = -120f;
                                    this.ai[1]++;
                                }
                                else
                                {
                                    this.velocity.Y = -8f;
                                    this.velocity.X += 4f * this.direction;
                                    this.ai[0] = -120f;
                                    this.ai[1]++;
                                }
                            }
                            else if (this.ai[0] >= -30f)
                            {
                                this.aiAction = 1;
                            }
                        }
                        else if ((this.target < 0xff) && (((this.direction == 1) && (this.velocity.X < 3f)) || ((this.direction == -1) && (this.velocity.X > -3f))))
                        {
                            if (((this.direction == -1) && (this.velocity.X < 0.1)) || ((this.direction == 1) && (this.velocity.X > -0.1)))
                            {
                                this.velocity.X += 0.2f * this.direction;
                            }
                            else
                            {
                                this.velocity.X *= 0.93f;
                            }
                        }
                        int num147 = Dust.NewDust(this.position, this.width, this.height, 4, this.velocity.X, this.velocity.Y, 0xff, new Color(0, 80, 0xff, 80), this.scale * 1.2f);
                        Main.dust[num147].noGravity = true;
                        Dust dust9 = Main.dust[num147];
                        dust9.velocity = (Vector2) (dust9.velocity * 0.5f);
                        if (this.life > 0)
                        {
                            float num148 = ((float) this.life) / ((float) this.lifeMax);
                            num148 = (num148 * 0.5f) + 0.75f;
                            if (num148 != this.scale)
                            {
                                this.position.X += this.width / 2;
                                this.position.Y += this.height;
                                this.scale = num148;
                                this.width = (int) (98f * this.scale);
                                this.height = (int) (92f * this.scale);
                                this.position.X -= this.width / 2;
                                this.position.Y -= this.height;
                            }
                            if (Main.netMode != 1)
                            {
                                int num149 = (int) (this.lifeMax * 0.05);
                                if ((this.life + num149) < this.ai[3])
                                {
                                    this.ai[3] = this.life;
                                    int num150 = Main.rand.Next(1, 4);
                                    for (int num151 = 0; num151 < num150; num151++)
                                    {
                                        int x = ((int) this.position.X) + Main.rand.Next(this.width - 0x20);
                                        int y = ((int) this.position.Y) + Main.rand.Next(this.height - 0x20);
                                        int num154 = NewNPC(x, y, 1, 0);
                                        Main.npc[num154].SetDefaults(1);
                                        Main.npc[num154].velocity.X = Main.rand.Next(-15, 0x10) * 0.1f;
                                        Main.npc[num154].velocity.Y = Main.rand.Next(-30, 1) * 0.1f;
                                        Main.npc[num154].ai[1] = Main.rand.Next(3);
                                        if ((Main.netMode == 2) && (num154 < 0x3e8))
                                        {
                                            NetMessage.SendData(0x17, -1, -1, "", num154, 0f, 0f, 0f, 0);
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    else if (this.aiStyle == 0x10)
                    {
                        if (this.direction == 0)
                        {
                            this.TargetClosest(true);
                        }
                        if (this.wet)
                        {
                            if (this.collideX)
                            {
                                this.velocity.X *= -1f;
                                this.direction *= -1;
                            }
                            if (this.collideY)
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y = Math.Abs(this.velocity.Y) * -1f;
                                    this.directionY = -1;
                                    this.ai[0] = -1f;
                                }
                                else if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y = Math.Abs(this.velocity.Y);
                                    this.directionY = 1;
                                    this.ai[0] = 1f;
                                }
                            }
                            bool flag14 = false;
                            if (!this.friendly)
                            {
                                this.TargetClosest(false);
                                if (Main.player[this.target].wet && !Main.player[this.target].dead)
                                {
                                    flag14 = true;
                                }
                            }
                            if (flag14)
                            {
                                this.TargetClosest(true);
                                if (this.type == 0x41)
                                {
                                    this.velocity.X += this.direction * 0.15f;
                                    this.velocity.Y += this.directionY * 0.15f;
                                    if (this.velocity.X > 5f)
                                    {
                                        this.velocity.X = 5f;
                                    }
                                    if (this.velocity.X < -5f)
                                    {
                                        this.velocity.X = -5f;
                                    }
                                    if (this.velocity.Y > 3f)
                                    {
                                        this.velocity.Y = 3f;
                                    }
                                    if (this.velocity.Y < -3f)
                                    {
                                        this.velocity.Y = -3f;
                                    }
                                }
                                else
                                {
                                    this.velocity.X += this.direction * 0.1f;
                                    this.velocity.Y += this.directionY * 0.1f;
                                    if (this.velocity.X > 3f)
                                    {
                                        this.velocity.X = 3f;
                                    }
                                    if (this.velocity.X < -3f)
                                    {
                                        this.velocity.X = -3f;
                                    }
                                    if (this.velocity.Y > 2f)
                                    {
                                        this.velocity.Y = 2f;
                                    }
                                    if (this.velocity.Y < -2f)
                                    {
                                        this.velocity.Y = -2f;
                                    }
                                }
                            }
                            else
                            {
                                this.velocity.X += this.direction * 0.1f;
                                if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                                {
                                    this.velocity.X *= 0.95f;
                                }
                                if (this.ai[0] == -1f)
                                {
                                    this.velocity.Y -= 0.01f;
                                    if (this.velocity.Y < -0.3)
                                    {
                                        this.ai[0] = 1f;
                                    }
                                }
                                else
                                {
                                    this.velocity.Y += 0.01f;
                                    if (this.velocity.Y > 0.3)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                }
                                int num155 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                                int num156 = (((int) this.position.Y) + (this.height / 2)) / 0x10;
                                if (Main.tile[num155, num156 - 1] == null)
                                {
                                    Main.tile[num155, num156 - 1] = new Tile();
                                }
                                if (Main.tile[num155, num156 + 1] == null)
                                {
                                    Main.tile[num155, num156 + 1] = new Tile();
                                }
                                if (Main.tile[num155, num156 + 2] == null)
                                {
                                    Main.tile[num155, num156 + 2] = new Tile();
                                }
                                if (Main.tile[num155, num156 - 1].liquid > 0x80)
                                {
                                    if (Main.tile[num155, num156 + 1].active)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                    else if (Main.tile[num155, num156 + 2].active)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                }
                                if ((this.velocity.Y > 0.4) || (this.velocity.Y < -0.4))
                                {
                                    this.velocity.Y *= 0.95f;
                                }
                            }
                        }
                        else
                        {
                            if (this.velocity.Y == 0f)
                            {
                                if (this.type == 0x41)
                                {
                                    this.velocity.X *= 0.94f;
                                    if ((this.velocity.X > -0.2) && (this.velocity.X < 0.2))
                                    {
                                        this.velocity.X = 0f;
                                    }
                                }
                                else if (Main.netMode != 1)
                                {
                                    this.velocity.Y = Main.rand.Next(-50, -20) * 0.1f;
                                    this.velocity.X = Main.rand.Next(-20, 20) * 0.1f;
                                    this.netUpdate = true;
                                }
                            }
                            this.velocity.Y += 0.3f;
                            if (this.velocity.Y > 10f)
                            {
                                this.velocity.Y = 10f;
                            }
                            this.ai[0] = 1f;
                        }
                        this.rotation = (this.velocity.Y * this.direction) * 0.1f;
                        if (this.rotation < -0.2)
                        {
                            this.rotation = -0.2f;
                        }
                        if (this.rotation > 0.2)
                        {
                            this.rotation = 0.2f;
                            return;
                        }
                    }
                    else if (this.aiStyle == 0x11)
                    {
                        this.noGravity = true;
                        if (this.ai[0] == 0f)
                        {
                            this.TargetClosest(true);
                            if (Main.netMode != 1)
                            {
                                if ((this.velocity.X != 0f) || (this.velocity.Y != 0f))
                                {
                                    this.ai[0] = 1f;
                                    this.netUpdate = true;
                                }
                                else
                                {
                                    Rectangle rectangle3 = new Rectangle((int) Main.player[this.target].position.X, (int) Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
                                    Rectangle rectangle4 = new Rectangle(((int) this.position.X) - 100, ((int) this.position.Y) - 100, this.width + 200, this.height + 200);
                                    if (rectangle4.Intersects(rectangle3) || (this.life < this.lifeMax))
                                    {
                                        this.ai[0] = 1f;
                                        this.velocity.Y -= 6f;
                                        this.netUpdate = true;
                                    }
                                }
                            }
                        }
                        else if (!Main.player[this.target].dead)
                        {
                            if (this.collideX)
                            {
                                this.velocity.X = this.oldVelocity.X * -0.5f;
                                if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                                {
                                    this.velocity.X = 2f;
                                }
                                if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                                {
                                    this.velocity.X = -2f;
                                }
                            }
                            if (this.collideY)
                            {
                                this.velocity.Y = this.oldVelocity.Y * -0.5f;
                                if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                                {
                                    this.velocity.Y = 1f;
                                }
                                if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                                {
                                    this.velocity.Y = -1f;
                                }
                            }
                            this.TargetClosest(true);
                            if ((this.direction == -1) && (this.velocity.X > -3f))
                            {
                                this.velocity.X -= 0.1f;
                                if (this.velocity.X > 3f)
                                {
                                    this.velocity.X -= 0.1f;
                                }
                                else if (this.velocity.X > 0f)
                                {
                                    this.velocity.X -= 0.05f;
                                }
                                if (this.velocity.X < -3f)
                                {
                                    this.velocity.X = -3f;
                                }
                            }
                            else if ((this.direction == 1) && (this.velocity.X < 3f))
                            {
                                this.velocity.X += 0.1f;
                                if (this.velocity.X < -3f)
                                {
                                    this.velocity.X += 0.1f;
                                }
                                else if (this.velocity.X < 0f)
                                {
                                    this.velocity.X += 0.05f;
                                }
                                if (this.velocity.X > 3f)
                                {
                                    this.velocity.X = 3f;
                                }
                            }
                            float num157 = Math.Abs((float) ((this.position.X + (this.width / 2)) - (Main.player[this.target].position.X + (Main.player[this.target].width / 2))));
                            float num158 = Main.player[this.target].position.Y - (this.height / 2);
                            if (num157 > 50f)
                            {
                                num158 -= 100f;
                            }
                            if (this.position.Y < num158)
                            {
                                this.velocity.Y += 0.05f;
                                if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y += 0.01f;
                                }
                            }
                            else
                            {
                                this.velocity.Y -= 0.05f;
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y -= 0.01f;
                                }
                            }
                            if (this.velocity.Y < -3f)
                            {
                                this.velocity.Y = -3f;
                            }
                            if (this.velocity.Y > 3f)
                            {
                                this.velocity.Y = 3f;
                            }
                        }
                        if (this.wet)
                        {
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y *= 0.95f;
                            }
                            this.velocity.Y -= 0.5f;
                            if (this.velocity.Y < -4f)
                            {
                                this.velocity.Y = -4f;
                            }
                            this.TargetClosest(true);
                            return;
                        }
                    }
                    else if (this.aiStyle == 0x12)
                    {
                        Lighting.addLight((((int) this.position.X) + (this.height / 2)) / 0x10, (((int) this.position.Y) + (this.height / 2)) / 0x10, 0.4f);
                        if (this.direction == 0)
                        {
                            this.TargetClosest(true);
                        }
                        if (!this.wet)
                        {
                            this.rotation += this.velocity.X * 0.1f;
                            if (this.velocity.Y == 0f)
                            {
                                this.velocity.X *= 0.98f;
                                if ((this.velocity.X > -0.01) && (this.velocity.X < 0.01))
                                {
                                    this.velocity.X = 0f;
                                }
                            }
                            this.velocity.Y += 0.2f;
                            if (this.velocity.Y > 10f)
                            {
                                this.velocity.Y = 10f;
                            }
                            this.ai[0] = 1f;
                            return;
                        }
                        if (this.collideX)
                        {
                            this.velocity.X *= -1f;
                            this.direction *= -1;
                        }
                        if (this.collideY)
                        {
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y = Math.Abs(this.velocity.Y) * -1f;
                                this.directionY = -1;
                                this.ai[0] = -1f;
                            }
                            else if (this.velocity.Y < 0f)
                            {
                                this.velocity.Y = Math.Abs(this.velocity.Y);
                                this.directionY = 1;
                                this.ai[0] = 1f;
                            }
                        }
                        bool flag15 = false;
                        if (!this.friendly)
                        {
                            this.TargetClosest(false);
                            if (Main.player[this.target].wet && !Main.player[this.target].dead)
                            {
                                flag15 = true;
                            }
                        }
                        if (!flag15)
                        {
                            this.velocity.X += this.direction * 0.02f;
                            this.rotation = this.velocity.X * 0.4f;
                            if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                            {
                                this.velocity.X *= 0.95f;
                            }
                            if (this.ai[0] == -1f)
                            {
                                this.velocity.Y -= 0.01f;
                                if (this.velocity.Y < -1f)
                                {
                                    this.ai[0] = 1f;
                                }
                            }
                            else
                            {
                                this.velocity.Y += 0.01f;
                                if (this.velocity.Y > 1f)
                                {
                                    this.ai[0] = -1f;
                                }
                            }
                            int num164 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                            int num165 = (((int) this.position.Y) + (this.height / 2)) / 0x10;
                            if (Main.tile[num164, num165 - 1] == null)
                            {
                                Main.tile[num164, num165 - 1] = new Tile();
                            }
                            if (Main.tile[num164, num165 + 1] == null)
                            {
                                Main.tile[num164, num165 + 1] = new Tile();
                            }
                            if (Main.tile[num164, num165 + 2] == null)
                            {
                                Main.tile[num164, num165 + 2] = new Tile();
                            }
                            if (Main.tile[num164, num165 - 1].liquid > 0x80)
                            {
                                if (Main.tile[num164, num165 + 1].active)
                                {
                                    this.ai[0] = -1f;
                                }
                                else if (Main.tile[num164, num165 + 2].active)
                                {
                                    this.ai[0] = -1f;
                                }
                            }
                            else
                            {
                                this.ai[0] = 1f;
                            }
                            if ((this.velocity.Y > 1.2) || (this.velocity.Y < -1.2))
                            {
                                this.velocity.Y *= 0.99f;
                                return;
                            }
                        }
                        else
                        {
                            this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                            this.velocity = (Vector2) (this.velocity * 0.98f);
                            float num159 = 0.2f;
                            if (((this.velocity.X > -num159) && (this.velocity.X < num159)) && ((this.velocity.Y > -num159) && (this.velocity.Y < num159)))
                            {
                                this.TargetClosest(true);
                                float num160 = 7f;
                                Vector2 vector20 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                float num161 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector20.X;
                                float num162 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector20.Y;
                                float num163 = (float) Math.Sqrt((double) ((num161 * num161) + (num162 * num162)));
                                num163 = num160 / num163;
                                num161 *= num163;
                                num162 *= num163;
                                this.velocity.X = num161;
                                this.velocity.Y = num162;
                                return;
                            }
                        }
                    }
                    else if (this.aiStyle == 0x13)
                    {
                        this.TargetClosest(true);
                        float num166 = 12f;
                        Vector2 vector21 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        float num167 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector21.X;
                        float num168 = Main.player[this.target].position.Y - vector21.Y;
                        float num169 = (float) Math.Sqrt((double) ((num167 * num167) + (num168 * num168)));
                        num169 = num166 / num169;
                        num167 *= num169;
                        num168 *= num169;
                        bool flag16 = false;
                        if (this.directionY < 0)
                        {
                            this.rotation = (float) (Math.Atan2((double) num168, (double) num167) + 1.57);
                            if ((this.rotation < -1.2) || (this.rotation > 1.2))
                            {
                                flag16 = false;
                            }
                            else
                            {
                                flag16 = true;
                            }
                            if (this.rotation < -0.8)
                            {
                                this.rotation = -0.8f;
                            }
                            else if (this.rotation > 0.8)
                            {
                                this.rotation = 0.8f;
                            }
                            if (this.velocity.X != 0f)
                            {
                                this.velocity.X *= 0.9f;
                                if ((this.velocity.X > -0.1) || (this.velocity.X < 0.1))
                                {
                                    this.netUpdate = true;
                                    this.velocity.X = 0f;
                                }
                            }
                        }
                        if (this.ai[0] > 0f)
                        {
                            if (this.ai[0] == 200f)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 5);
                            }
                            this.ai[0]--;
                        }
                        if (((Main.netMode != 1) && flag16) && ((this.ai[0] == 0f) && Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height)))
                        {
                            this.ai[0] = 200f;
                            int num170 = 10;
                            int num171 = 0x1f;
                            int num172 = Projectile.NewProjectile(vector21.X, vector21.Y, num167, num168, num171, num170, 0f, Main.myPlayer);
                            Main.projectile[num172].ai[0] = 2f;
                            Main.projectile[num172].timeLeft = 300;
                            Main.projectile[num172].friendly = false;
                            NetMessage.SendData(0x1b, -1, -1, "", num172, 0f, 0f, 0f, 0);
                            this.netUpdate = true;
                        }
                        try
                        {
                            int num173 = ((int) this.position.X) / 0x10;
                            int num174 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                            int num175 = (((int) this.position.X) + this.width) / 0x10;
                            int num176 = (((int) this.position.Y) + this.height) / 0x10;
                            bool flag17 = false;
                            if (Main.tile[num173, num176] == null)
                            {
                                Main.tile[num173, num176] = new Tile();
                            }
                            if (Main.tile[num174, num176] == null)
                            {
                                Main.tile[num173, num176] = new Tile();
                            }
                            if (Main.tile[num175, num176] == null)
                            {
                                Main.tile[num173, num176] = new Tile();
                            }
                            if (((Main.tile[num173, num176].active && Main.tileSolid[Main.tile[num173, num176].type]) || (Main.tile[num174, num176].active && Main.tileSolid[Main.tile[num174, num176].type])) || (Main.tile[num175, num176].active && Main.tileSolid[Main.tile[num175, num176].type]))
                            {
                                flag17 = true;
                            }
                            if (flag17)
                            {
                                this.noGravity = true;
                                this.noTileCollide = true;
                                this.velocity.Y = -0.2f;
                            }
                            else
                            {
                                this.noGravity = false;
                                this.noTileCollide = false;
                                if (Main.rand.Next(2) == 0)
                                {
                                    int num177 = Dust.NewDust(new Vector2(this.position.X - 4f, (this.position.Y + this.height) - 8f), this.width + 8, 0x18, 0x20, 0f, this.velocity.Y / 2f, 0, new Color(), 1f);
                                    Main.dust[num177].velocity.X *= 0.4f;
                                    Main.dust[num177].velocity.Y *= -1f;
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        Main.dust[num177].noGravity = true;
                                        Dust dust10 = Main.dust[num177];
                                        dust10.scale += 0.2f;
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    return;
                }
                this.TargetClosest(true);
                this.velocity.X *= 0.93f;
                if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                {
                    this.velocity.X = 0f;
                }
                if (this.ai[0] == 0f)
                {
                    this.ai[0] = 500f;
                }
                if ((this.ai[2] != 0f) && (this.ai[3] != 0f))
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                    for (int num76 = 0; num76 < 50; num76++)
                    {
                        if ((this.type == 0x1d) || (this.type == 0x2d))
                        {
                            color = new Color();
                            int num77 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, 0f, 0f, 100, color, (float) Main.rand.Next(1, 3));
                            Dust dust1 = Main.dust[num77];
                            dust1.velocity = (Vector2) (dust1.velocity * 3f);
                            if (Main.dust[num77].scale > 1f)
                            {
                                Main.dust[num77].noGravity = true;
                            }
                        }
                        else if (this.type == 0x20)
                        {
                            color = new Color();
                            int num78 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, 0f, 0f, 100, color, 2.5f);
                            Dust dust2 = Main.dust[num78];
                            dust2.velocity = (Vector2) (dust2.velocity * 3f);
                            Main.dust[num78].noGravity = true;
                        }
                        else
                        {
                            color = new Color();
                            int num79 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2.5f);
                            Dust dust3 = Main.dust[num79];
                            dust3.velocity = (Vector2) (dust3.velocity * 3f);
                            Main.dust[num79].noGravity = true;
                        }
                    }
                    this.position.X = ((this.ai[2] * 16f) - (this.width / 2)) + 8f;
                    this.position.Y = (this.ai[3] * 16f) - this.height;
                    this.velocity.X = 0f;
                    this.velocity.Y = 0f;
                    this.ai[2] = 0f;
                    this.ai[3] = 0f;
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                    for (int num80 = 0; num80 < 50; num80++)
                    {
                        if ((this.type == 0x1d) || (this.type == 0x2d))
                        {
                            color = new Color();
                            int num81 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, 0f, 0f, 100, color, (float) Main.rand.Next(1, 3));
                            Dust dust4 = Main.dust[num81];
                            dust4.velocity = (Vector2) (dust4.velocity * 3f);
                            if (Main.dust[num81].scale > 1f)
                            {
                                Main.dust[num81].noGravity = true;
                            }
                        }
                        else if (this.type == 0x20)
                        {
                            color = new Color();
                            int num82 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, 0f, 0f, 100, color, 2.5f);
                            Dust dust5 = Main.dust[num82];
                            dust5.velocity = (Vector2) (dust5.velocity * 3f);
                            Main.dust[num82].noGravity = true;
                        }
                        else
                        {
                            color = new Color();
                            int num83 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2.5f);
                            Dust dust6 = Main.dust[num83];
                            dust6.velocity = (Vector2) (dust6.velocity * 3f);
                            Main.dust[num83].noGravity = true;
                        }
                    }
                }
                this.ai[0]++;
                if (((this.ai[0] == 100f) || (this.ai[0] == 200f)) || (this.ai[0] == 300f))
                {
                    this.ai[1] = 30f;
                    this.netUpdate = true;
                    goto Label_6668;
                }
                if ((this.ai[0] < 650f) || (Main.netMode == 1))
                {
                    goto Label_6668;
                }
                this.ai[0] = 1f;
                int num84 = ((int) Main.player[this.target].position.X) / 0x10;
                int num85 = ((int) Main.player[this.target].position.Y) / 0x10;
                int num86 = ((int) this.position.X) / 0x10;
                int num87 = ((int) this.position.Y) / 0x10;
                int num88 = 20;
                int num89 = 0;
                bool flag12 = false;
                if ((Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) + Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y))) > 2000f)
                {
                    num89 = 100;
                    flag12 = true;
                }
            Label_6654:
                while (!flag12 && (num89 < 100))
                {
                    num89++;
                    int num90 = Main.rand.Next(num84 - num88, num84 + num88);
                    for (int num92 = Main.rand.Next(num85 - num88, num85 + num88); num92 < (num85 + num88); num92++)
                    {
                        if (((((num92 < (num85 - 4)) || (num92 > (num85 + 4))) || ((num90 < (num84 - 4)) || (num90 > (num84 + 4)))) && (((num92 < (num87 - 1)) || (num92 > (num87 + 1))) || ((num90 < (num86 - 1)) || (num90 > (num86 + 1))))) && Main.tile[num90, num92].active)
                        {
                            bool flag13 = true;
                            if ((this.type == 0x20) && (Main.tile[num90, num92 - 1].wall == 0))
                            {
                                flag13 = false;
                            }
                            else if (Main.tile[num90, num92 - 1].lava)
                            {
                                flag13 = false;
                            }
                            if ((flag13 && Main.tileSolid[Main.tile[num90, num92].type]) && !Collision.SolidTiles(num90 - 1, num90 + 1, num92 - 4, num92 - 1))
                            {
                                this.ai[1] = 20f;
                                this.ai[2] = num90;
                                this.ai[3] = num92;
                                flag12 = true;
                                goto Label_6654;
                            }
                        }
                    }
                }
                this.netUpdate = true;
            }
            else
            {
                if (((this.target < 0) || (this.target == 0xff)) || Main.player[this.target].dead)
                {
                    this.TargetClosest(true);
                }
                if (Main.player[this.target].dead && (this.timeLeft > 10))
                {
                    this.timeLeft = 10;
                }
                if (Main.netMode != 1)
                {
                    if (((((this.type == 7) || (this.type == 8)) || ((this.type == 10) || (this.type == 11))) || (((this.type == 13) || (this.type == 14)) || ((this.type == 0x27) || (this.type == 40)))) && (this.ai[0] == 0f))
                    {
                        if (((this.type == 7) || (this.type == 10)) || ((this.type == 13) || (this.type == 0x27)))
                        {
                            this.ai[2] = 10f;
                            if (this.type == 10)
                            {
                                this.ai[2] = 5f;
                            }
                            if (this.type == 13)
                            {
                                this.ai[2] = 50f;
                            }
                            if (this.type == 0x27)
                            {
                                this.ai[2] = 15f;
                            }
                            this.ai[0] = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + this.height, this.type + 1, this.whoAmI);
                        }
                        else if ((((this.type == 8) || (this.type == 11)) || ((this.type == 14) || (this.type == 40))) && (this.ai[2] > 0f))
                        {
                            this.ai[0] = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + this.height, this.type, this.whoAmI);
                        }
                        else
                        {
                            this.ai[0] = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + this.height, this.type + 1, this.whoAmI);
                        }
                        Main.npc[(int) this.ai[0]].ai[1] = this.whoAmI;
                        Main.npc[(int) this.ai[0]].ai[2] = this.ai[2] - 1f;
                        this.netUpdate = true;
                    }
                    if (((((this.type == 8) || (this.type == 9)) || ((this.type == 11) || (this.type == 12))) || ((this.type == 40) || (this.type == 0x29))) && (!Main.npc[(int) this.ai[1]].active || (Main.npc[(int) this.ai[1]].aiStyle != this.aiStyle)))
                    {
                        this.life = 0;
                        this.HitEffect(0, 10.0);
                        this.active = false;
                    }
                    if (((((this.type == 7) || (this.type == 8)) || ((this.type == 10) || (this.type == 11))) || ((this.type == 0x27) || (this.type == 40))) && !Main.npc[(int) this.ai[0]].active)
                    {
                        this.life = 0;
                        this.HitEffect(0, 10.0);
                        this.active = false;
                    }
                    if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                    {
                        if (!Main.npc[(int) this.ai[1]].active && !Main.npc[(int) this.ai[0]].active)
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if ((this.type == 13) && !Main.npc[(int) this.ai[0]].active)
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if ((this.type == 15) && !Main.npc[(int) this.ai[1]].active)
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if ((this.type == 14) && !Main.npc[(int) this.ai[1]].active)
                        {
                            this.type = 13;
                            int whoAmI = this.whoAmI;
                            int life = this.life;
                            float num50 = this.ai[0];
                            this.SetDefaults(this.type);
                            this.life = life;
                            if (this.life > this.lifeMax)
                            {
                                this.life = this.lifeMax;
                            }
                            this.ai[0] = num50;
                            this.TargetClosest(true);
                            this.netUpdate = true;
                            this.whoAmI = whoAmI;
                        }
                        if ((this.type == 14) && !Main.npc[(int) this.ai[0]].active)
                        {
                            int num51 = this.life;
                            int num52 = this.whoAmI;
                            float num53 = this.ai[1];
                            this.SetDefaults(this.type);
                            this.life = num51;
                            if (this.life > this.lifeMax)
                            {
                                this.life = this.lifeMax;
                            }
                            this.ai[1] = num53;
                            this.TargetClosest(true);
                            this.netUpdate = true;
                            this.whoAmI = num52;
                        }
                        if (this.life == 0)
                        {
                            bool flag6 = true;
                            for (int num54 = 0; num54 < 0x3e8; num54++)
                            {
                                if (Main.npc[num54].active && (((Main.npc[num54].type == 13) || (Main.npc[num54].type == 14)) || (Main.npc[num54].type == 15)))
                                {
                                    flag6 = false;
                                    break;
                                }
                            }
                            if (flag6)
                            {
                                this.boss = true;
                                this.NPCLoot();
                            }
                        }
                    }
                    if (!this.active && (Main.netMode == 2))
                    {
                        NetMessage.SendData(0x1c, -1, -1, "", this.whoAmI, -1f, 0f, 0f, 0);
                    }
                }
                int num55 = ((int) (this.position.X / 16f)) - 1;
                int maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                int num57 = ((int) (this.position.Y / 16f)) - 1;
                int maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                if (num55 < 0)
                {
                    num55 = 0;
                }
                if (maxTilesX > Main.maxTilesX)
                {
                    maxTilesX = Main.maxTilesX;
                }
                if (num57 < 0)
                {
                    num57 = 0;
                }
                if (maxTilesY > Main.maxTilesY)
                {
                    maxTilesY = Main.maxTilesY;
                }
                bool flag7 = false;
                for (int num59 = num55; num59 < maxTilesX; num59++)
                {
                    for (int num60 = num57; num60 < maxTilesY; num60++)
                    {
                        if ((Main.tile[num59, num60] != null) && ((Main.tile[num59, num60].active && (Main.tileSolid[Main.tile[num59, num60].type] || (Main.tileSolidTop[Main.tile[num59, num60].type] && (Main.tile[num59, num60].frameY == 0)))) || (Main.tile[num59, num60].liquid > 0x40)))
                        {
                            Vector2 vector8;
                            vector8.X = num59 * 0x10;
                            vector8.Y = num60 * 0x10;
                            if ((((this.position.X + this.width) > vector8.X) && (this.position.X < (vector8.X + 16f))) && (((this.position.Y + this.height) > vector8.Y) && (this.position.Y < (vector8.Y + 16f))))
                            {
                                flag7 = true;
                                if ((Main.rand.Next(40) == 0) && Main.tile[num59, num60].active)
                                {
                                    WorldGen.KillTile(num59, num60, true, true, false);
                                }
                                if ((Main.netMode != 1) && (Main.tile[num59, num60].type == 2))
                                {
                                    byte num1 = Main.tile[num59, num60 - 1].type;
                                }
                            }
                        }
                    }
                }
                float num61 = 8f;
                float num62 = 0.07f;
                if (this.type == 10)
                {
                    num61 = 6f;
                    num62 = 0.05f;
                }
                if (this.type == 13)
                {
                    num61 = 11f;
                    num62 = 0.08f;
                }
                Vector2 vector9 = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                float num63 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector9.X;
                float num64 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector9.Y;
                float num65 = (float) Math.Sqrt((double) ((num63 * num63) + (num64 * num64)));
                if (this.ai[1] > 0f)
                {
                    num63 = (Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - vector9.X;
                    num64 = (Main.npc[(int) this.ai[1]].position.Y + (Main.npc[(int) this.ai[1]].height / 2)) - vector9.Y;
                    this.rotation = ((float) Math.Atan2((double) num64, (double) num63)) + 1.57f;
                    num65 = (float) Math.Sqrt((double) ((num63 * num63) + (num64 * num64)));
                    num65 = (num65 - this.width) / num65;
                    num63 *= num65;
                    num64 *= num65;
                    this.velocity = new Vector2();
                    this.position.X += num63;
                    this.position.Y += num64;
                    return;
                }
                if (!flag7)
                {
                    this.TargetClosest(true);
                    this.velocity.Y += 0.11f;
                    if (this.velocity.Y > num61)
                    {
                        this.velocity.Y = num61;
                    }
                    if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num61 * 0.4))
                    {
                        if (this.velocity.X < 0f)
                        {
                            this.velocity.X -= num62 * 1.1f;
                        }
                        else
                        {
                            this.velocity.X += num62 * 1.1f;
                        }
                    }
                    else if (this.velocity.Y == num61)
                    {
                        if (this.velocity.X < num63)
                        {
                            this.velocity.X += num62;
                        }
                        else if (this.velocity.X > num63)
                        {
                            this.velocity.X -= num62;
                        }
                    }
                    else if (this.velocity.Y > 4f)
                    {
                        if (this.velocity.X < 0f)
                        {
                            this.velocity.X += num62 * 0.9f;
                        }
                        else
                        {
                            this.velocity.X -= num62 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (this.soundDelay == 0)
                    {
                        float num66 = num65 / 40f;
                        if (num66 < 10f)
                        {
                            num66 = 10f;
                        }
                        if (num66 > 20f)
                        {
                            num66 = 20f;
                        }
                        this.soundDelay = (int) num66;
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 1);
                    }
                    num65 = (float) Math.Sqrt((double) ((num63 * num63) + (num64 * num64)));
                    float num67 = Math.Abs(num63);
                    float num68 = Math.Abs(num64);
                    num65 = num61 / num65;
                    num63 *= num65;
                    num64 *= num65;
                    if ((this.type == 7) && !Main.player[this.target].zoneEvil) //Mod
                    {
                        if (((this.position.Y / 16f) > Main.rockLayer) && (this.timeLeft > 2))
                        {
                            this.timeLeft = 2;
                        }
                        num63 = 0f;
                        num64 = num61;
                    }
                    if ((((this.velocity.X > 0f) && (num63 > 0f)) || ((this.velocity.X < 0f) && (num63 < 0f))) || (((this.velocity.Y > 0f) && (num64 > 0f)) || ((this.velocity.Y < 0f) && (num64 < 0f))))
                    {
                        if (this.velocity.X < num63)
                        {
                            this.velocity.X += num62;
                        }
                        else if (this.velocity.X > num63)
                        {
                            this.velocity.X -= num62;
                        }
                        if (this.velocity.Y < num64)
                        {
                            this.velocity.Y += num62;
                        }
                        else if (this.velocity.Y > num64)
                        {
                            this.velocity.Y -= num62;
                        }
                    }
                    else if (num67 > num68)
                    {
                        if (this.velocity.X < num63)
                        {
                            this.velocity.X += num62 * 1.1f;
                        }
                        else if (this.velocity.X > num63)
                        {
                            this.velocity.X -= num62 * 1.1f;
                        }
                        if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num61 * 0.5))
                        {
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y += num62;
                            }
                            else
                            {
                                this.velocity.Y -= num62;
                            }
                        }
                    }
                    else
                    {
                        if (this.velocity.Y < num64)
                        {
                            this.velocity.Y += num62 * 1.1f;
                        }
                        else if (this.velocity.Y > num64)
                        {
                            this.velocity.Y -= num62 * 1.1f;
                        }
                        if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num61 * 0.5))
                        {
                            if (this.velocity.X > 0f)
                            {
                                this.velocity.X += num62;
                            }
                            else
                            {
                                this.velocity.X -= num62;
                            }
                        }
                    }
                }
                this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                return;
            }
        Label_6668:
            if (this.ai[1] > 0f)
            {
                this.ai[1]--;
                if (this.ai[1] == 25f)
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                    if (Main.netMode != 1)
                    {
                        if ((this.type == 0x1d) || (this.type == 0x2d))
                        {
                            NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) - 8, 30, 0);
                        }
                        else if (this.type == 0x20)
                        {
                            NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) - 8, 0x21, 0);
                        }
                        else
                        {
                            NewNPC((((int) this.position.X) + (this.width / 2)) + (this.direction * 8), ((int) this.position.Y) + 20, 0x19, 0);
                        }
                    }
                }
            }
            if ((this.type == 0x1d) || (this.type == 0x2d))
            {
                if (Main.rand.Next(5) == 0)
                {
                    int num93 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1b, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 1.5f);
                    Main.dust[num93].noGravity = true;
                    Main.dust[num93].velocity.X *= 0.5f;
                    Main.dust[num93].velocity.Y = -2f;
                }
            }
            else if (this.type == 0x20)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num94 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1d, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                    Main.dust[num94].noGravity = true;
                    Main.dust[num94].velocity.X *= 1f;
                    Main.dust[num94].velocity.Y *= 1f;
                }
            }
            else if (Main.rand.Next(2) == 0)
            {
                int num95 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                Main.dust[num95].noGravity = true;
                Main.dust[num95].velocity.X *= 1f;
                Main.dust[num95].velocity.Y *= 1f;
            }
        }

        public static bool AnyNPCs(int Type)
        {
            for (int i = 0; i < 0x3e8; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == Type))
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckActive()
        {
            if (this.active && ((((this.type != 8) && (this.type != 9)) && ((this.type != 11) && (this.type != 12))) && (((this.type != 14) && (this.type != 15)) && ((this.type != 40) && (this.type != 0x29)))))
            {
                if (this.townNPC)
                {
                    if (this.position.Y < (Main.worldSurface * 18.0))
                    {
                        Rectangle rectangle = new Rectangle((((int) this.position.X) + (this.width / 2)) - townRangeX, (((int) this.position.Y) + (this.height / 2)) - townRangeY, townRangeX * 2, townRangeY * 2);
                        for (int i = 0; i < 0xff; i++)
                        {
                            if (Main.player[i].active && rectangle.Intersects(new Rectangle((int) Main.player[i].position.X, (int) Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
                            {
                                Player player1 = Main.player[i];
                                player1.townNPCs += npcSlots;
                            }
                        }
                    }
                }
                else
                {
                    bool flag = false;
                    Rectangle rectangle2 = new Rectangle((((int) this.position.X) + (this.width / 2)) - activeRangeX, (((int) this.position.Y) + (this.height / 2)) - activeRangeY, activeRangeX * 2, activeRangeY * 2);
                    Rectangle rectangle3 = new Rectangle(((int) ((this.position.X + (this.width / 2)) - (sWidth * 0.5))) - this.width, ((int) ((this.position.Y + (this.height / 2)) - (sHeight * 0.5))) - this.height, sWidth + (this.width * 2), sHeight + (this.height * 2));
                    for (int j = 0; j < 0xff; j++)
                    {
                        if (Main.player[j].active)
                        {
                            if (rectangle2.Intersects(new Rectangle((int) Main.player[j].position.X, (int) Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
                            {
                                flag = true;
                                if (((this.type != 0x19) && (this.type != 30)) && (this.type != 0x21))
                                {
                                    Player player2 = Main.player[j];
                                    player2.activeNPCs += npcSlots;
                                }
                            }
                            if (rectangle3.Intersects(new Rectangle((int) Main.player[j].position.X, (int) Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
                            {
                                this.timeLeft = activeTime;
                            }
                            if (((this.type == 7) || (this.type == 10)) || ((this.type == 13) || (this.type == 0x27)))
                            {
                                flag = true;
                            }
                            if ((this.boss || (this.type == 0x23)) || (this.type == 0x24))
                            {
                                flag = true;
                            }
                        }
                    }
                    this.timeLeft--;
                    if (this.timeLeft <= 0)
                    {
                        flag = false;
                    }
                    if (!flag && (Main.netMode != 1))
                    {
                        noSpawnCycle = true;
                        this.active = false;
                        if (Main.netMode == 2)
                        {
                            this.life = 0;
                            NetMessage.SendData(0x17, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
                        }
                    }
                }
            }
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public void FindFrame()
        {
            int num = 1;
            if (!Main.dedServ)
            {
                num = Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type];
            }
            int num2 = 0;
            if (this.aiAction == 0)
            {
                if (this.velocity.Y < 0f)
                {
                    num2 = 2;
                }
                else if (this.velocity.Y > 0f)
                {
                    num2 = 3;
                }
                else if (this.velocity.X != 0f)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 0;
                }
            }
            else if (this.aiAction == 1)
            {
                num2 = 4;
            }
            if (((this.type == 1) || (this.type == 0x10)) || (this.type == 0x3b))
            {
                this.frameCounter++;
                if (num2 > 0)
                {
                    this.frameCounter++;
                }
                if (num2 == 4)
                {
                    this.frameCounter++;
                }
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 50)
            {
                if (this.velocity.Y != 0f)
                {
                    this.frame.Y = num * 4;
                }
                else
                {
                    this.frameCounter++;
                    if (num2 > 0)
                    {
                        this.frameCounter++;
                    }
                    if (num2 == 4)
                    {
                        this.frameCounter++;
                    }
                    if (this.frameCounter >= 8.0)
                    {
                        this.frame.Y += num;
                        this.frameCounter = 0.0;
                    }
                    if (this.frame.Y >= (num * 4))
                    {
                        this.frame.Y = 0;
                    }
                }
            }
            if (this.type == 0x3d)
            {
                this.spriteDirection = this.direction;
                this.rotation = this.velocity.X * 0.1f;
                if ((this.velocity.X == 0f) && (this.velocity.Y == 0f))
                {
                    this.frame.Y = 0;
                    this.frameCounter = 0.0;
                }
                else
                {
                    this.frameCounter++;
                    if (this.frameCounter < 4.0)
                    {
                        this.frame.Y = num;
                    }
                    else
                    {
                        this.frame.Y = num * 2;
                        if (this.frameCounter >= 7.0)
                        {
                            this.frameCounter = 0.0;
                        }
                    }
                }
            }
            if ((this.type == 0x3e) || (this.type == 0x42))
            {
                this.spriteDirection = this.direction;
                this.rotation = this.velocity.X * 0.1f;
                this.frameCounter++;
                if (this.frameCounter < 6.0)
                {
                    this.frame.Y = 0;
                }
                else
                {
                    this.frame.Y = num;
                    if (this.frameCounter >= 11.0)
                    {
                        this.frameCounter = 0.0;
                    }
                }
            }
            if ((this.type == 0x3f) || (this.type == 0x40))
            {
                this.frameCounter++;
                if (this.frameCounter < 6.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 18.0)
                {
                    this.frame.Y = num * 2;
                }
                else
                {
                    this.frame.Y = num * 3;
                    if (this.frameCounter >= 23.0)
                    {
                        this.frameCounter = 0.0;
                    }
                }
            }
            if ((this.type == 2) || (this.type == 0x17))
            {
                if (this.type == 2)
                {
                    if (this.velocity.X > 0f)
                    {
                        this.spriteDirection = 1;
                        this.rotation = (float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X);
                    }
                    if (this.velocity.X < 0f)
                    {
                        this.spriteDirection = -1;
                        this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 3.14f;
                    }
                }
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            if (((this.type == 0x37) || (this.type == 0x39)) || (this.type == 0x3a))
            {
                this.spriteDirection = this.direction;
                this.frameCounter++;
                if (this.wet)
                {
                    if (this.frameCounter < 6.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter < 12.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter < 18.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else if (this.frameCounter < 24.0)
                    {
                        this.frame.Y = num * 3;
                    }
                    else
                    {
                        this.frameCounter = 0.0;
                    }
                }
                else if (this.frameCounter < 6.0)
                {
                    this.frame.Y = num * 4;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num * 5;
                }
                else
                {
                    this.frameCounter = 0.0;
                }
            }
            if (this.type == 0x45)
            {
                if (this.ai[0] < 190f)
                {
                    this.frameCounter++;
                    if (this.frameCounter >= 6.0)
                    {
                        this.frameCounter = 0.0;
                        this.frame.Y += num;
                        if ((this.frame.Y / num) >= (Main.npcFrameCount[this.type] - 1))
                        {
                            this.frame.Y = 0;
                        }
                    }
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * (Main.npcFrameCount[this.type] - 1);
                }
            }
            if (this.type == 0x43)
            {
                if (this.velocity.Y == 0f)
                {
                    this.spriteDirection = this.direction;
                }
                this.frameCounter++;
                if (this.frameCounter >= 6.0)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y += num;
                    if ((this.frame.Y / num) >= Main.npcFrameCount[this.type])
                    {
                        this.frame.Y = 0;
                    }
                }
            }
            if (this.type == 0x41)
            {
                this.spriteDirection = this.direction;
                this.frameCounter++;
                if (this.wet)
                {
                    if (this.frameCounter < 6.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter < 12.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter < 18.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else if (this.frameCounter < 24.0)
                    {
                        this.frame.Y = num * 3;
                    }
                    else
                    {
                        this.frameCounter = 0.0;
                    }
                }
            }
            if (((this.type == 0x30) || (this.type == 0x31)) || ((this.type == 0x33) || (this.type == 60)))
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                }
                this.rotation = this.velocity.X * 0.1f;
                this.frameCounter++;
                if (this.frameCounter >= 6.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * 4))
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 0x2a)
            {
                this.frameCounter++;
                if (this.frameCounter < 2.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 4.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 6.0)
                {
                    this.frame.Y = num * 2;
                }
                else if (this.frameCounter < 8.0)
                {
                    this.frame.Y = num;
                }
                else
                {
                    this.frameCounter = 0.0;
                }
            }
            if ((this.type == 0x2b) || (this.type == 0x38))
            {
                this.frameCounter++;
                if (this.frameCounter < 6.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 18.0)
                {
                    this.frame.Y = num * 2;
                }
                else if (this.frameCounter < 24.0)
                {
                    this.frame.Y = num;
                }
                if (this.frameCounter == 23.0)
                {
                    this.frameCounter = 0.0;
                }
            }
            if (((((this.type == 0x11) || (this.type == 0x12)) || ((this.type == 0x13) || (this.type == 20))) || (((this.type == 0x16) || (this.type == 0x26)) || ((this.type == 0x1a) || (this.type == 0x1b)))) || ((((this.type == 0x1c) || (this.type == 0x1f)) || ((this.type == 0x15) || (this.type == 0x2c))) || ((this.type == 0x36) || (this.type == 0x25))))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                    if (this.velocity.X == 0f)
                    {
                        this.frame.Y = 0;
                        this.frameCounter = 0.0;
                    }
                    else
                    {
                        this.frameCounter += Math.Abs(this.velocity.X) * 2f;
                        this.frameCounter++;
                        if (this.frameCounter > 6.0)
                        {
                            this.frame.Y += num;
                            this.frameCounter = 0.0;
                        }
                        if ((this.frame.Y / num) >= Main.npcFrameCount[this.type])
                        {
                            this.frame.Y = num * 2;
                        }
                    }
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num;
                    if (((this.type == 0x15) || (this.type == 0x1f)) || (this.type == 0x2c))
                    {
                        this.frame.Y = 0;
                    }
                }
            }
            else if (((this.type == 3) || (this.type == 0x34)) || (this.type == 0x35))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                if (((this.velocity.Y != 0f) || ((this.direction == -1) && (this.velocity.X > 0f))) || ((this.direction == 1) && (this.velocity.X < 0f)))
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 2;
                }
                else if (this.velocity.X == 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = 0;
                }
                else
                {
                    this.frameCounter += Math.Abs(this.velocity.X);
                    if (this.frameCounter < 8.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter < 16.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter < 24.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else if (this.frameCounter < 32.0)
                    {
                        this.frame.Y = num;
                    }
                    else
                    {
                        this.frameCounter = 0.0;
                    }
                }
            }
            else if ((this.type == 0x2e) || (this.type == 0x2f))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                    if (this.velocity.X == 0f)
                    {
                        this.frame.Y = 0;
                        this.frameCounter = 0.0;
                    }
                    else
                    {
                        this.frameCounter += Math.Abs(this.velocity.X) * 1f;
                        this.frameCounter++;
                        if (this.frameCounter > 6.0)
                        {
                            this.frame.Y += num;
                            this.frameCounter = 0.0;
                        }
                        if ((this.frame.Y / num) >= Main.npcFrameCount[this.type])
                        {
                            this.frame.Y = 0;
                        }
                    }
                }
                else if (this.velocity.Y < 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 4;
                }
                else if (this.velocity.Y > 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 6;
                }
            }
            else if (this.type == 4)
            {
                this.frameCounter++;
                if (this.frameCounter < 7.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 14.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 21.0)
                {
                    this.frame.Y = num * 2;
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = 0;
                }
                if (this.ai[0] > 1f)
                {
                    this.frame.Y += num * 3;
                }
            }
            else if (this.type == 5)
            {
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            else if (this.type == 6)
            {
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            else if (this.type == 0x18)
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                if (this.ai[1] > 0f)
                {
                    if (this.frame.Y < 4)
                    {
                        this.frameCounter = 0.0;
                    }
                    this.frameCounter++;
                    if (this.frameCounter <= 4.0)
                    {
                        this.frame.Y = num * 4;
                    }
                    else if (this.frameCounter <= 8.0)
                    {
                        this.frame.Y = num * 5;
                    }
                    else if (this.frameCounter <= 12.0)
                    {
                        this.frame.Y = num * 6;
                    }
                    else if (this.frameCounter <= 16.0)
                    {
                        this.frame.Y = num * 7;
                    }
                    else if (this.frameCounter <= 20.0)
                    {
                        this.frame.Y = num * 8;
                    }
                    else
                    {
                        this.frame.Y = num * 9;
                        this.frameCounter = 100.0;
                    }
                }
                else
                {
                    this.frameCounter++;
                    if (this.frameCounter <= 4.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter <= 8.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter <= 12.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else
                    {
                        this.frame.Y = num * 3;
                        if (this.frameCounter >= 16.0)
                        {
                            this.frameCounter = 0.0;
                        }
                    }
                }
            }
            else if (((this.type == 0x1d) || (this.type == 0x20)) || (this.type == 0x2d))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                this.frame.Y = 0;
                if (this.velocity.Y != 0f)
                {
                    this.frame.Y += num;
                }
                else if (this.ai[1] > 0f)
                {
                    this.frame.Y += num * 2;
                }
            }
            if (this.type == 0x22)
            {
                this.frameCounter++;
                if (this.frameCounter >= 4.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
        }

        public Color GetAlpha(Color newColor)
        {
            int r = newColor.R - this.alpha;
            int g = newColor.G - this.alpha;
            int b = newColor.B - this.alpha;
            int a = newColor.A - this.alpha;
            if (((this.type == 0x19) || (this.type == 30)) || (this.type == 0x21))
            {
                r = newColor.R;
                g = newColor.G;
                b = newColor.B;
            }
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

        public string GetChat()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            for (int i = 0; i < 0x3e8; i++)
            {
                if (Main.npc[i].type == 0x11)
                {
                    flag = true;
                }
                else if (Main.npc[i].type == 0x12)
                {
                    flag2 = true;
                }
                else if (Main.npc[i].type == 0x13)
                {
                    flag3 = true;
                }
                else if (Main.npc[i].type == 20)
                {
                    flag4 = true;
                }
                else if (Main.npc[i].type == 0x25)
                {
                    flag5 = true;
                }
                else if (Main.npc[i].type == 0x26)
                {
                    flag6 = true;
                }
            }
            string str = "";
            if (this.type == 0x11)
            {
                if (Main.dayTime)
                {
                    if (Main.time < 16200.0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            return "Sword beats paper, get one today.";
                        }
                        return "Lovely morning, wouldn't you say? Was there something you needed?";
                    }
                    if (Main.time > 37800.0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            return "Night be upon us soon, friend. Make your choices while you can.";
                        }
                        return ("Ah, they will tell tales of " + Main.player[Main.myPlayer].name + " some day... good ones I'm sure.");
                    }
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            str = "Check out my dirt blocks, they are extra dirty.";
                            break;

                        case 1:
                            return "Boy, that sun is hot! I do have some perfectly ventilated armor.";
                    }
                    return "The sun is high, but my prices are not.";
                }
                if (Main.bloodMoon)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "Have you seen Chith...Shith.. Chat... The big eye?";
                    }
                    return "Keep your eye on the prize, buy a lense!";
                }
                if (Main.time < 9720.0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "Kosh, kapleck Mog. Oh sorry, that's klingon for 'Buy something or die.'";
                    }
                    return (Main.player[Main.myPlayer].name + " is it? I've heard good things, friend!");
                }
                if (Main.time > 22680.0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "I hear there's a secret treasure... oh never mind.";
                    }
                    return "Angel Statue you say? I'm sorry, I'm not a junk dealer.";
                }
                int num8 = Main.rand.Next(3);
                if (num8 == 0)
                {
                    str = "The last guy who was here left me some junk..er I mean.. treasures!";
                }
                if (num8 == 1)
                {
                    return "I wonder if the moon is made of cheese...huh, what? Oh yes, buy something!";
                }
                return "Did you say gold?  I'll take that off of ya'.";
            }
            if (this.type == 0x12)
            {
                if (flag6 && (Main.rand.Next(4) == 0))
                {
                    return "I wish that bomb maker would be more careful.  I'm getting tired of having to sew his limbs back on every day.";
                }
                if (Main.player[Main.myPlayer].statLife < (Main.player[Main.myPlayer].statLifeMax * 0.33))
                {
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            return "I think you look better this way.";

                        case 1:
                            return "Eww.. What happened to your face?";

                        case 2:
                            return "MY GOODNESS! I'm good but I'm not THAT good.";

                        case 3:
                            return "Dear friends we are gathered here today to bid farewell... oh, you'll be fine.";
                    }
                    return "You left your arm over there. Let me get that for you..";
                }
                if (Main.player[Main.myPlayer].statLife < (Main.player[Main.myPlayer].statLifeMax * 0.66))
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            return "Quit being such a baby! I've seen worse.";

                        case 1:
                            return "That's gonna need stitches!";

                        case 2:
                            return "Trouble with those bullies again?";
                    }
                    return "You look half digested. Have you been chasing slimes again?";
                }
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return "Turn your head and cough.";

                    case 1:
                        return "That's not the biggest I've ever seen... Yes, I've seen bigger wounds for sure.";
                }
                return "Show me where it hurts.";
            }
            if (this.type == 0x13)
            {
                if (flag2 && (Main.rand.Next(4) == 0))
                {
                    return "Make it quick! I've got a date with the nurse in an hour.";
                }
                if (flag4 && (Main.rand.Next(4) == 0))
                {
                    return "That dryad is a looker.  Too bad she's such a prude.";
                }
                if (flag6 && (Main.rand.Next(4) == 0))
                {
                    return "Don't bother with that firework vendor, I've got all you need right here.";
                }
                if (Main.bloodMoon)
                {
                    return "I love nights like tonight.  There is never a shortage of things to kill!";
                }
                switch (Main.rand.Next(2))
                {
                    case 0:
                        return "I see you're eyeballin' the Minishark.. You really don't want to know how it was made.";

                    case 1:
                        str = "Keep your hands off my gun, buddy!";
                        break;
                }
                return str;
            }
            if (this.type == 20)
            {
                if (flag3 && (Main.rand.Next(4) == 0))
                {
                    return "I wish that gun seller would stop talking to me. Doesn't he realize I'm 500 years old?";
                }
                if (flag && (Main.rand.Next(4) == 0))
                {
                    return "That merchant keeps trying to sell me an angel statue. Everyone knows that they don't do anything.";
                }
                if (flag5 && (Main.rand.Next(4) == 0))
                {
                    return "Have you seen the old man walking around the dungeon? He doesn't look well at all...";
                }
                if (Main.bloodMoon)
                {
                    return "It is an evil moon tonight. Be careful.";
                }
                int num13 = Main.rand.Next(6);
                if (num13 == 0)
                {
                    return "You must cleanse the world of this corruption.";
                }
                if (num13 != 1)
                {
                    if (num13 == 2)
                    {
                        return "The sands of time are flowing. And well, you are not aging very gracefully.";
                    }
                    if (num13 == 3)
                    {
                        return "What's this about me having more 'bark' than bite?";
                    }
                    if (num13 == 4)
                    {
                        return "So two goblins walk into a bar, and one says to the other, 'Want to get a Gobblet of beer?!'";
                    }
                }
                return "Be safe; Terraria needs you!";
            }
            if (this.type == 0x16)
            {
                if (Main.bloodMoon)
                {
                    return "You can tell a Blood Moon is out when the sky turns red. There is something about it that causes monsters to swarm.";
                }
                if (!Main.dayTime)
                {
                    return "You should stay indoors at night. It is very dangerous to be wandering around in the dark.";
                }
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return ("Greetings, " + Main.player[Main.myPlayer].name + ". Is there something I can help you with?");

                    case 1:
                        return "I am here to give you advice on what to do next.  It is recommended that you talk with me anytime you get stuck.";

                    case 2:
                        str = "They say there is a person who will tell you how to survive in this land... oh wait. That's me.";
                        break;
                }
                return str;
            }
            if (this.type == 0x25)
            {
                if (Main.dayTime)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "I cannot let you enter until you free me of my curse.";

                        case 1:
                            str = "Come back at night if you wish to enter.";
                            break;
                    }
                    return str;
                }
                if ((Main.player[Main.myPlayer].statLifeMax < 300) || (Main.player[Main.myPlayer].statDefense < 10))
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "You are far to weak to defeat my curse.  Come back when you aren't so worthless.";

                        case 1:
                            str = "You pathetic fool.  You cannot hope to face my master as you are now.";
                            break;
                    }
                    return str;
                }
                int num17 = Main.rand.Next(2);
                if (num17 == 0)
                {
                    return "You just might be strong enough to free me from my curse...";
                }
                if (num17 == 1)
                {
                    str = "Stranger, do you possess the strength to defeat my master?";
                }
                return str;
            }
            if (this.type == 0x26)
            {
                if (Main.bloodMoon)
                {
                    return "I've got something for them zombies alright!";
                }
                if (flag3 && (Main.rand.Next(4) == 0))
                {
                    return "Even the gun dealer wants what I'm selling!";
                }
                if (flag2 && (Main.rand.Next(4) == 0))
                {
                    return "I'm sure the nurse will help if you accidentally lose a limb to these.";
                }
                if (flag4 && (Main.rand.Next(4) == 0))
                {
                    return "Why purify the world when you can just blow it up?";
                }
                switch (Main.rand.Next(4))
                {
                    case 0:
                        return "Explosives are da' bomb these days.  Buy some now!";

                    case 1:
                        return "It's a good day to die!";

                    case 2:
                        return "I wonder what happens if I... (BOOM!)... Oh, sorry, did you need that leg?";

                    case 3:
                        return "Dynamite, my own special cure-all for what ails ya.";
                }
                return "Check out my goods; they have explosive prices!";
            }
            if (this.type != 0x36)
            {
                return str;
            }
            if (Main.bloodMoon)
            {
                return (Main.player[Main.myPlayer].name + "... we have a problem! Its a blood moon out there!");
            }
            if (flag2 && (Main.rand.Next(4) == 0))
            {
                return "T'were I younger, I would ask the nurse out. I used to be quite the lady killer.";
            }
            if (Main.player[Main.myPlayer].head == 0x18)
            {
                return "That Red Hat of yours looks familiar...";
            }
            int num19 = Main.rand.Next(4);
            if (num19 == 0)
            {
                return "Thanks again for freeing me from my curse. Felt like something jumped up and bit me";
            }
            if (num19 == 1)
            {
                return "Mama always said I would make a great tailor.";
            }
            if (num19 == 2)
            {
                return "Life's like a box of clothes, you never know what you are gonna wear!";
            }
            return "Being cursed was lonely, so I once made a friend out of leather. I named him Wilson.";
        }

        public Color GetColor(Color newColor)
        {
            int r = this.color.R - (0xff - newColor.R);
            int g = this.color.G - (0xff - newColor.G);
            int b = this.color.B - (0xff - newColor.B);
            int a = this.color.A - (0xff - newColor.A);
            if (r < 0)
            {
                r = 0;
            }
            if (r > 0xff)
            {
                r = 0xff;
            }
            if (g < 0)
            {
                g = 0;
            }
            if (g > 0xff)
            {
                g = 0xff;
            }
            if (b < 0)
            {
                b = 0;
            }
            if (b > 0xff)
            {
                b = 0xff;
            }
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

        public void HitEffect(int hitDirection = 0, double dmg = 10.0)
        {
            if ((this.type == 1) || (this.type == 0x10))
            {
                if (this.life > 0)
                {
                    for (int i = 0; i < ((dmg / ((double) this.lifeMax)) * 100.0); i++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) hitDirection, -1f, this.alpha, this.color, 1f);
                    }
                }
                else
                {
                    for (int j = 0; j < 50; j++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) (2 * hitDirection), -2f, this.alpha, this.color, 1f);
                    }
                    if ((Main.netMode != 1) && (this.type == 0x10))
                    {
                        int num3 = Main.rand.Next(2) + 2;
                        for (int k = 0; k < num3; k++)
                        {
                            int index = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + this.height, 1, 0);
                            Main.npc[index].SetDefaults("Baby Slime");
                            Main.npc[index].velocity.X = this.velocity.X * 2f;
                            Main.npc[index].velocity.Y = this.velocity.Y;
                            Main.npc[index].velocity.X += (Main.rand.Next(-20, 20) * 0.1f) + ((k * this.direction) * 0.3f);
                            Main.npc[index].velocity.Y -= (Main.rand.Next(0, 10) * 0.1f) + k;
                            Main.npc[index].ai[1] = k;
                            if ((Main.netMode == 2) && (index < 0x3e8))
                            {
                                NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f, 0);
                            }
                        }
                    }
                }
            }
            if ((this.type == 0x3f) || (this.type == 0x40))
            {
                Color newColor = new Color(50, 120, 0xff, 100);
                if (this.type == 0x40)
                {
                    newColor = new Color(0xe1, 70, 140, 100);
                }
                if (this.life > 0)
                {
                    for (int m = 0; m < ((dmg / ((double) this.lifeMax)) * 50.0); m++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) hitDirection, -1f, 0, newColor, 1f);
                    }
                }
                else
                {
                    for (int n = 0; n < 0x19; n++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) (2 * hitDirection), -2f, 0, newColor, 1f);
                    }
                }
            }
            else
            {
                Color color2;
                if ((this.type == 0x3b) || (this.type == 60))
                {
                    if (this.life > 0)
                    {
                        for (int num8 = 0; num8 < ((dmg / ((double) this.lifeMax)) * 80.0); num8++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 6, (float) (hitDirection * 2), -1f, this.alpha, color2, 1.5f);
                        }
                    }
                    else
                    {
                        for (int num9 = 0; num9 < 40; num9++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 6, (float) (hitDirection * 2), -1f, this.alpha, color2, 1.5f);
                        }
                        if ((this.type == 0x3b) && (Main.netMode != 1))
                        {
                            int num10 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                            int num11 = (((int) this.position.Y) + (this.height / 2)) / 0x10;
                            Main.tile[num10, num11].lava = true;
                            if (Main.tile[num10, num11].liquid < 200)
                            {
                                Main.tile[num10, num11].liquid = 200;
                            }
                            WorldGen.TileFrame(num10, num11, false, false);
                        }
                    }
                }
                else if (this.type == 50)
                {
                    if (this.life > 0)
                    {
                        for (int num12 = 0; num12 < ((dmg / ((double) this.lifeMax)) * 300.0); num12++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 4, (float) hitDirection, -1f, 0xaf, new Color(0, 80, 0xff, 100), 1f);
                        }
                    }
                    else
                    {
                        for (int num13 = 0; num13 < 200; num13++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 4, (float) (2 * hitDirection), -2f, 0xaf, new Color(0, 80, 0xff, 100), 1f);
                        }
                        if (Main.netMode != 1)
                        {
                            int num14 = Main.rand.Next(4) + 4;
                            for (int num15 = 0; num15 < num14; num15++)
                            {
                                int x = ((int) this.position.X) + Main.rand.Next(this.width - 0x20);
                                int y = ((int) this.position.Y) + Main.rand.Next(this.height - 0x20);
                                int num18 = NewNPC(x, y, 1, 0);
                                Main.npc[num18].SetDefaults(1);
                                Main.npc[num18].velocity.X = Main.rand.Next(-15, 0x10) * 0.1f;
                                Main.npc[num18].velocity.Y = Main.rand.Next(-30, 1) * 0.1f;
                                Main.npc[num18].ai[1] = Main.rand.Next(3);
                                if ((Main.netMode == 2) && (num18 < 0x3e8))
                                {
                                    NetMessage.SendData(0x17, -1, -1, "", num18, 0f, 0f, 0f, 0);
                                }
                            }
                        }
                    }
                }
                else if ((this.type == 0x31) || (this.type == 0x33))
                {
                    if (this.life > 0)
                    {
                        for (int num19 = 0; num19 < ((dmg / ((double) this.lifeMax)) * 30.0); num19++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num20 = 0; num20 < 15; num20++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        if (this.type == 0x33)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x53);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x52);
                        }
                    }
                }
                else if (((this.type == 0x2e) || (this.type == 0x37)) || (this.type == 0x43))
                {
                    if (this.life > 0)
                    {
                        for (int num21 = 0; num21 < ((dmg / ((double) this.lifeMax)) * 20.0); num21++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num22 = 0; num22 < 10; num22++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        if (this.type == 0x2e)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x4c);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x4d);
                        }
                        else if (this.type == 0x43)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x5f);
                            Gore.NewGore(this.position, this.velocity, 0x5f);
                            Gore.NewGore(this.position, this.velocity, 0x60);
                        }
                    }
                }
                else if (((this.type == 0x2f) || (this.type == 0x39)) || (this.type == 0x3a))
                {
                    if (this.life > 0)
                    {
                        for (int num23 = 0; num23 < ((dmg / ((double) this.lifeMax)) * 20.0); num23++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num24 = 0; num24 < 10; num24++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        if (this.type == 0x39)
                        {
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x54);
                        }
                        else if (this.type == 0x3a)
                        {
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x55);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x4e);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x4f);
                        }
                    }
                }
                else if (this.type == 2)
                {
                    if (this.life > 0)
                    {
                        for (int num25 = 0; num25 < ((dmg / ((double) this.lifeMax)) * 100.0); num25++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num26 = 0; num26 < 50; num26++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 1);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 2);
                    }
                }
                else if (this.type == 0x45)
                {
                    if (this.life > 0)
                    {
                        for (int num27 = 0; num27 < ((dmg / ((double) this.lifeMax)) * 100.0); num27++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num28 = 0; num28 < 50; num28++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x61);
                        Gore.NewGore(this.position, this.velocity, 0x62);
                    }
                }
                else if (this.type == 0x3d)
                {
                    if (this.life > 0)
                    {
                        for (int num29 = 0; num29 < ((dmg / ((double) this.lifeMax)) * 100.0); num29++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num30 = 0; num30 < 50; num30++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x56);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 0x57);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 0x58);
                    }
                }
                else if (this.type == 0x41)
                {
                    if (this.life > 0)
                    {
                        for (int num31 = 0; num31 < ((dmg / ((double) this.lifeMax)) * 150.0); num31++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num32 = 0; num32 < 0x4b; num32++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, (Vector2) (this.velocity * 0.8f), 0x59);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), (Vector2) (this.velocity * 0.8f), 90);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), (Vector2) (this.velocity * 0.8f), 0x5b);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), (Vector2) (this.velocity * 0.8f), 0x5c);
                    }
                }
                else if (((this.type == 3) || (this.type == 0x34)) || (this.type == 0x35))
                {
                    if (this.life > 0)
                    {
                        for (int num33 = 0; num33 < ((dmg / ((double) this.lifeMax)) * 100.0); num33++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num34 = 0; num34 < 50; num34++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 3);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                    }
                }
                else if (this.type == 4)
                {
                    if (this.life > 0)
                    {
                        for (int num35 = 0; num35 < ((dmg / ((double) this.lifeMax)) * 100.0); num35++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num36 = 0; num36 < 150; num36++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        for (int num37 = 0; num37 < 2; num37++)
                        {
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 2);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 7);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 9);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 10);
                        }
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                    }
                }
                else if (this.type == 5)
                {
                    if (this.life > 0)
                    {
                        for (int num38 = 0; num38 < ((dmg / ((double) this.lifeMax)) * 50.0); num38++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num39 = 0; num39 < 20; num39++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 6);
                        Gore.NewGore(this.position, this.velocity, 7);
                    }
                }
                else if (this.type == 6)
                {
                    if (this.life > 0)
                    {
                        for (int num40 = 0; num40 < ((dmg / ((double) this.lifeMax)) * 100.0); num40++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                        }
                    }
                    else
                    {
                        for (int num41 = 0; num41 < 50; num41++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                        }
                        int num42 = Gore.NewGore(this.position, this.velocity, 14);
                        Main.gore[num42].alpha = this.alpha;
                        num42 = Gore.NewGore(this.position, this.velocity, 15);
                        Main.gore[num42].alpha = this.alpha;
                    }
                }
                else if (((this.type == 7) || (this.type == 8)) || (this.type == 9))
                {
                    if (this.life > 0)
                    {
                        for (int num43 = 0; num43 < ((dmg / ((double) this.lifeMax)) * 100.0); num43++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                        }
                    }
                    else
                    {
                        for (int num44 = 0; num44 < 50; num44++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                        }
                        int num45 = Gore.NewGore(this.position, this.velocity, (this.type - 7) + 0x12);
                        Main.gore[num45].alpha = this.alpha;
                    }
                }
                else if (((this.type == 10) || (this.type == 11)) || (this.type == 12))
                {
                    if (this.life > 0)
                    {
                        for (int num46 = 0; num46 < ((dmg / ((double) this.lifeMax)) * 50.0); num46++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num47 = 0; num47 < 10; num47++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, (this.type - 7) + 0x12);
                    }
                }
                else if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                {
                    if (this.life > 0)
                    {
                        for (int num48 = 0; num48 < ((dmg / ((double) this.lifeMax)) * 100.0); num48++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                        }
                    }
                    else
                    {
                        for (int num49 = 0; num49 < 50; num49++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                        }
                        if (this.type == 13)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x18);
                            Gore.NewGore(this.position, this.velocity, 0x19);
                        }
                        else if (this.type == 14)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x1a);
                            Gore.NewGore(this.position, this.velocity, 0x1b);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x1c);
                            Gore.NewGore(this.position, this.velocity, 0x1d);
                        }
                    }
                }
                else if (this.type == 0x11)
                {
                    if (this.life > 0)
                    {
                        for (int num50 = 0; num50 < ((dmg / ((double) this.lifeMax)) * 100.0); num50++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num51 = 0; num51 < 50; num51++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 30);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x1f);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x1f);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x20);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x20);
                    }
                }
                else if (this.type == 0x16)
                {
                    if (this.life > 0)
                    {
                        for (int num52 = 0; num52 < ((dmg / ((double) this.lifeMax)) * 100.0); num52++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num53 = 0; num53 < 50; num53++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x49);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x4a);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x4a);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x4b);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x4b);
                    }
                }
                else if ((this.type == 0x25) || (this.type == 0x36))
                {
                    if (this.life > 0)
                    {
                        for (int num54 = 0; num54 < ((dmg / ((double) this.lifeMax)) * 100.0); num54++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num55 = 0; num55 < 50; num55++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x3a);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x3b);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x3b);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                    }
                }
                else if (this.type == 0x12)
                {
                    if (this.life > 0)
                    {
                        for (int num56 = 0; num56 < ((dmg / ((double) this.lifeMax)) * 100.0); num56++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num57 = 0; num57 < 50; num57++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x21);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x22);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x22);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x23);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x23);
                    }
                }
                else if (this.type == 0x13)
                {
                    if (this.life > 0)
                    {
                        for (int num58 = 0; num58 < ((dmg / ((double) this.lifeMax)) * 100.0); num58++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num59 = 0; num59 < 50; num59++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x24);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x25);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x25);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x26);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x26);
                    }
                }
                else if (this.type == 0x26)
                {
                    if (this.life > 0)
                    {
                        for (int num60 = 0; num60 < ((dmg / ((double) this.lifeMax)) * 100.0); num60++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num61 = 0; num61 < 50; num61++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x40);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x41);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x41);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x42);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x42);
                    }
                }
                else if (this.type == 20)
                {
                    if (this.life > 0)
                    {
                        for (int num62 = 0; num62 < ((dmg / ((double) this.lifeMax)) * 100.0); num62++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num63 = 0; num63 < 50; num63++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x27);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x29);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x29);
                    }
                }
                else if (((this.type == 0x15) || (this.type == 0x1f)) || (((this.type == 0x20) || (this.type == 0x2c)) || (this.type == 0x2d)))
                {
                    if (this.life > 0)
                    {
                        for (int num64 = 0; num64 < ((dmg / ((double) this.lifeMax)) * 50.0); num64++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num65 = 0; num65 < 20; num65++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x2a);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2b);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2b);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2c);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2c);
                    }
                }
                else if (((this.type == 0x27) || (this.type == 40)) || (this.type == 0x29))
                {
                    if (this.life > 0)
                    {
                        for (int num66 = 0; num66 < ((dmg / ((double) this.lifeMax)) * 50.0); num66++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num67 = 0; num67 < 20; num67++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, (this.type - 0x27) + 0x43);
                    }
                }
                else if (this.type == 0x22)
                {
                    if (this.life > 0)
                    {
                        for (int num68 = 0; num68 < ((dmg / ((double) this.lifeMax)) * 30.0); num68++)
                        {
                            color2 = new Color();
                            int num69 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 15, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 1.8f);
                            Main.dust[num69].noLight = true;
                            Main.dust[num69].noGravity = true;
                            Dust dust1 = Main.dust[num69];
                            dust1.velocity = (Vector2) (dust1.velocity * 1.3f);
                            color2 = new Color();
                            num69 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1a, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 0, color2, 0.9f);
                            Main.dust[num69].noLight = true;
                            Dust dust2 = Main.dust[num69];
                            dust2.velocity = (Vector2) (dust2.velocity * 1.3f);
                        }
                    }
                    else
                    {
                        for (int num70 = 0; num70 < 15; num70++)
                        {
                            color2 = new Color();
                            int num71 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 15, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 1.8f);
                            Main.dust[num71].noLight = true;
                            Main.dust[num71].noGravity = true;
                            Dust dust3 = Main.dust[num71];
                            dust3.velocity = (Vector2) (dust3.velocity * 1.3f);
                            color2 = new Color();
                            num71 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1a, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 0, color2, 0.9f);
                            Main.dust[num71].noLight = true;
                            Dust dust4 = Main.dust[num71];
                            dust4.velocity = (Vector2) (dust4.velocity * 1.3f);
                        }
                    }
                }
                else if ((this.type == 0x23) || (this.type == 0x24))
                {
                    if (this.life > 0)
                    {
                        for (int num72 = 0; num72 < ((dmg / ((double) this.lifeMax)) * 100.0); num72++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num73 = 0; num73 < 150; num73++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        if (this.type == 0x23)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x36);
                            Gore.NewGore(this.position, this.velocity, 0x37);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x38);
                            Gore.NewGore(this.position, this.velocity, 0x39);
                            Gore.NewGore(this.position, this.velocity, 0x39);
                            Gore.NewGore(this.position, this.velocity, 0x39);
                        }
                    }
                }
                else if (this.type == 0x17)
                {
                    if (this.life > 0)
                    {
                        for (int num74 = 0; num74 < ((dmg / ((double) this.lifeMax)) * 100.0); num74++)
                        {
                            int type = 0x19;
                            if (Main.rand.Next(2) == 0)
                            {
                                type = 6;
                            }
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, type, (float) hitDirection, -1f, 0, color2, 1f);
                            color2 = new Color();
                            int num76 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color2, 2f);
                            Main.dust[num76].noGravity = true;
                        }
                    }
                    else
                    {
                        for (int num77 = 0; num77 < 50; num77++)
                        {
                            int num78 = 0x19;
                            if (Main.rand.Next(2) == 0)
                            {
                                num78 = 6;
                            }
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, num78, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        for (int num79 = 0; num79 < 50; num79++)
                        {
                            color2 = new Color();
                            int num80 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color2, 2.5f);
                            Dust dust5 = Main.dust[num80];
                            dust5.velocity = (Vector2) (dust5.velocity * 6f);
                            Main.dust[num80].noGravity = true;
                        }
                    }
                }
                else if (this.type == 0x18)
                {
                    if (this.life > 0)
                    {
                        for (int num81 = 0; num81 < ((dmg / ((double) this.lifeMax)) * 100.0); num81++)
                        {
                            color2 = new Color();
                            int num82 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X, this.velocity.Y, 100, color2, 2.5f);
                            Main.dust[num82].noGravity = true;
                        }
                    }
                    else
                    {
                        for (int num83 = 0; num83 < 50; num83++)
                        {
                            color2 = new Color();
                            int num84 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X, this.velocity.Y, 100, color2, 2.5f);
                            Main.dust[num84].noGravity = true;
                            Dust dust6 = Main.dust[num84];
                            dust6.velocity = (Vector2) (dust6.velocity * 2f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x2d);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2e);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2e);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2f);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2f);
                    }
                }
                else if (this.type == 0x19)
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                    for (int num85 = 0; num85 < 20; num85++)
                    {
                        color2 = new Color();
                        int num86 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 2f);
                        Main.dust[num86].noGravity = true;
                        Dust dust7 = Main.dust[num86];
                        dust7.velocity = (Vector2) (dust7.velocity * 2f);
                        color2 = new Color();
                        num86 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 1f);
                        Dust dust8 = Main.dust[num86];
                        dust8.velocity = (Vector2) (dust8.velocity * 2f);
                    }
                }
                else if (this.type == 0x21)
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                    for (int num87 = 0; num87 < 20; num87++)
                    {
                        color2 = new Color();
                        int num88 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 2f);
                        Main.dust[num88].noGravity = true;
                        Dust dust9 = Main.dust[num88];
                        dust9.velocity = (Vector2) (dust9.velocity * 2f);
                        color2 = new Color();
                        num88 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 1f);
                        Dust dust10 = Main.dust[num88];
                        dust10.velocity = (Vector2) (dust10.velocity * 2f);
                    }
                }
                else if (((this.type == 0x1a) || (this.type == 0x1b)) || ((this.type == 0x1c) || (this.type == 0x1d)))
                {
                    if (this.life > 0)
                    {
                        for (int num89 = 0; num89 < ((dmg / ((double) this.lifeMax)) * 100.0); num89++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num90 = 0; num90 < 50; num90++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x30);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x31);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x31);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                    }
                }
                else if (this.type == 30)
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                    for (int num91 = 0; num91 < 20; num91++)
                    {
                        color2 = new Color();
                        int num92 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 2f);
                        Main.dust[num92].noGravity = true;
                        Dust dust11 = Main.dust[num92];
                        dust11.velocity = (Vector2) (dust11.velocity * 2f);
                        color2 = new Color();
                        num92 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color2, 1f);
                        Dust dust12 = Main.dust[num92];
                        dust12.velocity = (Vector2) (dust12.velocity * 2f);
                    }
                }
                else if (this.type == 0x2a)
                {
                    if (this.life > 0)
                    {
                        for (int num93 = 0; num93 < ((dmg / ((double) this.lifeMax)) * 100.0); num93++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                        }
                    }
                    else
                    {
                        for (int num94 = 0; num94 < 50; num94++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                        }
                        Gore.NewGore(this.position, this.velocity, 70);
                        Gore.NewGore(this.position, this.velocity, 0x47);
                    }
                }
                else if ((this.type == 0x2b) || (this.type == 0x38))
                {
                    if (this.life > 0)
                    {
                        for (int num95 = 0; num95 < ((dmg / ((double) this.lifeMax)) * 100.0); num95++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 40, (float) hitDirection, -1f, this.alpha, this.color, 1.2f);
                        }
                    }
                    else
                    {
                        for (int num96 = 0; num96 < 50; num96++)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 40, (float) hitDirection, -2f, this.alpha, this.color, 1.2f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x48);
                        Gore.NewGore(this.position, this.velocity, 0x48);
                    }
                }
                else if (this.type == 0x30)
                {
                    if (this.life > 0)
                    {
                        for (int num97 = 0; num97 < ((dmg / ((double) this.lifeMax)) * 100.0); num97++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num98 = 0; num98 < 50; num98++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 80);
                        Gore.NewGore(this.position, this.velocity, 0x51);
                    }
                }
                else if ((this.type == 0x3e) || (this.type == 0x42))
                {
                    if (this.life > 0)
                    {
                        for (int num99 = 0; num99 < ((dmg / ((double) this.lifeMax)) * 100.0); num99++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color2, 1f);
                        }
                    }
                    else
                    {
                        for (int num100 = 0; num100 < 50; num100++)
                        {
                            color2 = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color2, 1f);
                        }
                        Gore.NewGore(this.position, this.velocity, 0x5d);
                        Gore.NewGore(this.position, this.velocity, 0x5e);
                        Gore.NewGore(this.position, this.velocity, 0x5e);
                    }
                }
            }
        }

        public static int NewNPC(int X, int Y, int Type, int Start = 0)
        {
            int index = -1;
            for (int i = Start; i < 0x3e8; i++)
            {
                if (!Main.npc[i].active)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
            {
                return 0x3e8;
            }
            Main.npc[index] = new NPC();
            Main.npc[index].SetDefaults(Type);
            Main.npc[index].position.X = X - (Main.npc[index].width / 2);
            Main.npc[index].position.Y = Y - Main.npc[index].height;
            Main.npc[index].active = true;
            Main.npc[index].timeLeft = (int) (activeTime * 1.25);
            Main.npc[index].wet = Collision.WetCollision(Main.npc[index].position, Main.npc[index].width, Main.npc[index].height);
            if (Type == 50)
            {
                if (Main.netMode == 0)
                {
                    Main.NewText(Main.npc[index].name + " has awoken!", 0xaf, 0x4b, 0xff);
                    return index;
                }
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, Main.npc[index].name + " has awoken!", 0xff, 175f, 75f, 255f, 0);
                }
            }
            return index;
        }

        public void NPCLoot()
        {
            if ((this.type == 1) || (this.type == 0x10))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x17, Main.rand.Next(1, 3), false);
            }
            if (this.type == 2)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x26, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xec, 1, false);
                }
            }
            if (this.type == 0x3a)
            {
                if (Main.rand.Next(500) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x107, 1, false);
                }
                else if (Main.rand.Next(40) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x76, 1, false);
                }
            }
            if ((this.type == 3) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd8, 1, false);
            }
            if (this.type == 0x42)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x10b, 1, false);
            }
            if ((this.type == 0x3e) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x110, 1, false);
            }
            if (this.type == 0x34)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xfb, 1, false);
            }
            if (this.type == 0x35)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xef, 1, false);
            }
            if (this.type == 0x36)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 260, 1, false);
            }
            if (this.type == 0x37)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x105, 1, false);
            }
            if ((this.type == 0x45) && (Main.rand.Next(10) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x143, 1, false);
            }
            if (this.type == 4)
            {
                int stack = Main.rand.Next(30) + 20;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x2f, stack, false);
                stack = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, stack, false);
                stack = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, stack, false);
                stack = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, stack, false);
                stack = Main.rand.Next(3) + 1;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3b, stack, false);
            }
            if ((this.type == 6) && (Main.rand.Next(3) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x44, 1, false);
            }
            if (((this.type == 7) || (this.type == 8)) || (this.type == 9))
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x44, Main.rand.Next(1, 3), false);
                }
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x45, Main.rand.Next(3, 9), false);
            }
            if ((((this.type == 10) || (this.type == 11)) || (this.type == 12)) && (Main.rand.Next(500) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd7, 1, false);
            }
            if ((this.type == 0x2f) && (Main.rand.Next(0x4b) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xf3, 1, false);
            }
            if (((this.type == 0x27) || (this.type == 40)) || (this.type == 0x29))
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 220, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xda, 1, false);
                }
            }
            if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
            {
                int num2 = Main.rand.Next(1, 4);
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x56, num2, false);
                if (Main.rand.Next(2) == 0)
                {
                    num2 = Main.rand.Next(2, 6);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num2, false);
                }
                if (this.boss)
                {
                    num2 = Main.rand.Next(10, 30);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num2, false);
                    num2 = Main.rand.Next(10, 0x1f);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num2, false);
                }
            }
            if ((this.type == 0x3f) || (this.type == 0x40))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x11a, Main.rand.Next(1, 5), false);
            }
            if ((this.type == 0x15) || (this.type == 0x2c))
            {
                if (Main.rand.Next(0x19) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x76, 1, false);
                }
                else if (this.type == 0x2c)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xa6, Main.rand.Next(1, 4), false);
                }
            }
            if (this.type == 0x2d)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xee, 1, false);
            }
            if (this.type == 50)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, Main.rand.Next(0x100, 0x103), 1, false);
            }
            if ((this.type == 0x17) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x74, 1, false);
            }
            if (this.type == 0x18)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x70, 1, false);
                }
                else if (Main.rand.Next(500) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xf4, 1, false);
                }
            }
            if ((this.type == 0x1f) || (this.type == 0x20))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x9a, 1, false);
            }
            if (((this.type == 0x1a) || (this.type == 0x1b)) || ((this.type == 0x1c) || (this.type == 0x1d)))
            {
                if (Main.rand.Next(400) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x80, 1, false);
                }
                else if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 160, 1, false);
                }
                else if (Main.rand.Next(2) == 0)
                {
                    int num3 = Main.rand.Next(1, 6);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xa1, num3, false);
                }
            }
            if (this.type == 0x2a)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd1, 1, false);
            }
            if ((this.type == 0x2b) && (Main.rand.Next(5) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 210, 1, false);
            }
            if (this.type == 0x41)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x10c, 1, false);
                }
                else
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x13f, 1, false);
                }
            }
            if ((this.type == 0x30) && (Main.rand.Next(5) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 320, 1, false);
            }
            if (this.boss)
            {
                if (this.type == 4)
                {
                    downedBoss1 = true;
                }
                if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                {
                    downedBoss2 = true;
                    this.name = "Eater of Worlds";
                }
                if (this.type == 0x23)
                {
                    downedBoss3 = true;
                    this.name = "Skeletron";
                }
                int num4 = Main.rand.Next(5, 0x10);
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x1c, num4, false);
                int num5 = Main.rand.Next(5) + 5;
                for (int i = 0; i < num5; i++)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
                if (Main.netMode == 0)
                {
                    Main.NewText(this.name + " has been defeated!", 0xaf, 0x4b, 0xff);
                }
                else if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, this.name + " has been defeated!", 0xff, 175f, 75f, 255f, 0);
                }
            }
            if ((Main.rand.Next(7) == 0) && (this.lifeMax > 1))
            {
                if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statMana < Main.player[Player.FindClosest(this.position, this.width, this.height)].statManaMax))
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xb8, 1, false);
                }
                else if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statLife < Main.player[Player.FindClosest(this.position, this.width, this.height)].statLifeMax))
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
            }
            float num7 = this.value * (1f + (Main.rand.Next(-20, 0x15) * 0.01f));
            if (Main.rand.Next(5) == 0)
            {
                num7 *= 1f + (Main.rand.Next(5, 11) * 0.01f);
            }
            if (Main.rand.Next(10) == 0)
            {
                num7 *= 1f + (Main.rand.Next(10, 0x15) * 0.01f);
            }
            if (Main.rand.Next(15) == 0)
            {
                num7 *= 1f + (Main.rand.Next(15, 0x1f) * 0.01f);
            }
            if (Main.rand.Next(20) == 0)
            {
                num7 *= 1f + (Main.rand.Next(20, 0x29) * 0.01f);
            }
            while (((int) num7) > 0)
            {
                if (num7 > 1000000f)
                {
                    int num8 = (int) (num7 / 1000000f);
                    if ((num8 > 50) && (Main.rand.Next(2) == 0))
                    {
                        num8 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num8 /= Main.rand.Next(3) + 1;
                    }
                    num7 -= 0xf4240 * num8;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x4a, num8, false);
                }
                else
                {
                    if (num7 > 10000f)
                    {
                        int num9 = (int) (num7 / 10000f);
                        if ((num9 > 50) && (Main.rand.Next(2) == 0))
                        {
                            num9 /= Main.rand.Next(3) + 1;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            num9 /= Main.rand.Next(3) + 1;
                        }
                        num7 -= 0x2710 * num9;
                        Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x49, num9, false);
                        continue;
                    }
                    if (num7 > 100f)
                    {
                        int num10 = (int) (num7 / 100f);
                        if ((num10 > 50) && (Main.rand.Next(2) == 0))
                        {
                            num10 /= Main.rand.Next(3) + 1;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            num10 /= Main.rand.Next(3) + 1;
                        }
                        num7 -= 100 * num10;
                        Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x48, num10, false);
                        continue;
                    }
                    int num11 = (int) num7;
                    if ((num11 > 50) && (Main.rand.Next(2) == 0))
                    {
                        num11 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num11 /= Main.rand.Next(4) + 1;
                    }
                    if (num11 < 1)
                    {
                        num11 = 1;
                    }
                    num7 -= num11;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x47, num11, false);
                }
            }
        }

        public void SetDefaults(int Type)
        {
            npcSlots = 1f;
            this.lavaImmune = false;
            this.lavaWet = false;
            this.wetCount = 0;
            this.wet = false;
            this.townNPC = false;
            this.homeless = false;
            this.homeTileX = -1;
            this.homeTileY = -1;
            this.friendly = false;
            this.behindTiles = false;
            this.boss = false;
            this.noTileCollide = false;
            this.rotation = 0f;
            this.active = true;
            this.alpha = 0;
            this.color = new Color();
            this.collideX = false;
            this.collideY = false;
            this.direction = 0;
            this.oldDirection = this.direction;
            this.frameCounter = 0.0;
            this.netUpdate = false;
            this.knockBackResist = 1f;
            this.name = "";
            this.noGravity = false;
            this.scale = 1f;
            this.soundHit = 0;
            this.soundKilled = 0;
            this.spriteDirection = -1;
            this.target = 0xff;
            this.oldTarget = this.target;
            this.targetRect = new Rectangle();
            this.timeLeft = activeTime;
            this.type = Type;
            this.value = 0f;
            for (int i = 0; i < maxAI; i++)
            {
                this.ai[i] = 0f;
            }
            if (this.type == 1)
            {
                this.name = "Blue Slime";
                this.width = 0x18;
                this.height = 0x12;
                this.aiStyle = 1;
                this.damage = 7;
                this.defense = 2;
                this.lifeMax = 0x19;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 0xaf;
                this.color = new Color(0, 80, 0xff, 100);
                this.value = 25f;
            }
            else if (this.type == 2)
            {
                this.name = "Demon Eye";
                this.width = 30;
                this.height = 0x20;
                this.aiStyle = 2;
                this.damage = 0x12;
                this.defense = 2;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 75f;
            }
            else if (this.type == 3)
            {
                this.name = "Zombie";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 14;
                this.defense = 6;
                this.lifeMax = 0x2d;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 60f;
            }
            else if (this.type == 4)
            {
                this.name = "Eye of Cthulhu";
                this.width = 100;
                this.height = 110;
                this.aiStyle = 4;
                this.damage = 0x12;
                this.defense = 12;
                this.lifeMax = 0xbb8;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0f;
                this.noGravity = true;
                this.noTileCollide = true;
                this.timeLeft = activeTime * 30;
                this.boss = true;
                this.value = 30000f;
            }
            else if (this.type == 5)
            {
                this.name = "Servant of Cthulhu";
                this.width = 20;
                this.height = 20;
                this.aiStyle = 5;
                this.damage = 0x17;
                this.defense = 0;
                this.lifeMax = 8;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
            }
            else if (this.type == 6)
            {
                npcSlots = 0.75f;
                this.name = "Eater of Souls";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 5;
                this.damage = 0x17;
                this.defense = 8;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.knockBackResist = 0.5f;
                this.value = 90f;
            }
            else if (this.type == 7)
            {
                npcSlots = 3.5f;
                this.name = "Devourer Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 0x20;
                this.defense = 2;
                this.lifeMax = 30;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 140f;
            }
            else if (this.type == 8)
            {
                this.name = "Devourer Body";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 0x10;
                this.defense = 6;
                this.lifeMax = 0x2d;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 140f;
            }
            else if (this.type == 9)
            {
                this.name = "Devourer Tail";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 13;
                this.defense = 10;
                this.lifeMax = 70;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 140f;
            }
            else if (this.type == 10)
            {
                this.name = "Giant Worm Head";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 8;
                this.defense = 0;
                this.lifeMax = 10;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 40f;
            }
            else if (this.type == 11)
            {
                this.name = "Giant Worm Body";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 4;
                this.lifeMax = 15;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 40f;
            }
            else if (this.type == 12)
            {
                this.name = "Giant Worm Tail";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 6;
                this.lifeMax = 20;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 40f;
            }
            else if (this.type == 13)
            {
                npcSlots = 8f;
                this.name = "Eater of Worlds Head";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 0x2b;
                this.defense = 0;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
                this.scale = 1.1f;
            }
            else if (this.type == 14)
            {
                this.name = "Eater of Worlds Body";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 0x12;
                this.defense = 6;
                this.lifeMax = 210;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
                this.scale = 1.1f;
            }
            else if (this.type == 15)
            {
                this.name = "Eater of Worlds Tail";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 11;
                this.defense = 7;
                this.lifeMax = 300;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
                this.scale = 1.1f;
            }
            else if (this.type == 0x10)
            {
                npcSlots = 2f;
                this.name = "Mother Slime";
                this.width = 0x24;
                this.height = 0x18;
                this.aiStyle = 1;
                this.damage = 20;
                this.defense = 7;
                this.lifeMax = 90;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 120;
                this.color = new Color(0, 0, 0, 50);
                this.value = 75f;
                this.scale = 1.25f;
                this.knockBackResist = 0.6f;
            }
            else if (this.type == 0x11)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Merchant";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x12)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Nurse";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x13)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Arms Dealer";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 20)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Dryad";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x15)
            {
                this.name = "Skeleton";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 8;
                this.lifeMax = 60;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 100f;
            }
            else if (this.type == 0x16)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Guide";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x17)
            {
                this.name = "Meteor Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 5;
                this.damage = 40;
                this.defense = 6;
                this.lifeMax = 0x1a;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 80f;
                this.knockBackResist = 0.4f;
            }
            else if (this.type == 0x18)
            {
                npcSlots = 3f;
                this.name = "Fire Imp";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 30;
                this.defense = 20;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
                this.lavaImmune = true;
                this.value = 400f;
            }
            else if (this.type == 0x19)
            {
                this.name = "Burning Sphere";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 0x19;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.alpha = 100;
            }
            else if (this.type == 0x1a)
            {
                this.name = "Goblin Peon";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 12;
                this.defense = 4;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.8f;
                this.value = 100f;
            }
            else if (this.type == 0x1b)
            {
                this.name = "Goblin Thief";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 6;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.7f;
                this.value = 200f;
            }
            else if (this.type == 0x1c)
            {
                this.name = "Goblin Warrior";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 0x19;
                this.defense = 8;
                this.lifeMax = 110;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
                this.value = 150f;
            }
            else if (this.type == 0x1d)
            {
                this.name = "Goblin Sorcerer";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 2;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.6f;
                this.value = 200f;
            }
            else if (this.type == 30)
            {
                this.name = "Chaos Ball";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 20;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.alpha = 100;
                this.knockBackResist = 0f;
            }
            else if (this.type == 0x1f)
            {
                this.name = "Angry Bones";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 30;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.6f;
                this.value = 130f;
            }
            else if (this.type == 0x20)
            {
                this.name = "Dark Caster";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 50;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.6f;
                this.value = 140f;
                npcSlots = 2f;
            }
            else if (this.type == 0x21)
            {
                this.name = "Water Sphere";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 20;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.alpha = 100;
                this.knockBackResist = 0f;
            }
            else if (this.type == 0x22)
            {
                this.name = "Cursed Skull";
                this.width = 0x1a;
                this.height = 0x1c;
                this.aiStyle = 10;
                this.damage = 0x23;
                this.defense = 6;
                this.lifeMax = 80;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 150f;
                this.knockBackResist = 0f;
                npcSlots = 0.75f;
            }
            else if (this.type == 0x23)
            {
                this.name = "Skeletron Head";
                this.width = 80;
                this.height = 0x66;
                this.aiStyle = 11;
                this.damage = 30;
                this.defense = 10;
                this.lifeMax = 0x1388;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 50000f;
                this.knockBackResist = 0f;
                this.boss = true;
                npcSlots = 6f;
            }
            else if (this.type == 0x24)
            {
                this.name = "Skeletron Hand";
                this.width = 0x34;
                this.height = 0x34;
                this.aiStyle = 12;
                this.damage = 0x19;
                this.defense = 0x12;
                this.lifeMax = 800;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
            }
            else if (this.type == 0x25)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Old Man";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x26)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Demolitionist";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x27)
            {
                npcSlots = 6f;
                this.name = "Bone Serpent Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 40;
                this.defense = 10;
                this.lifeMax = 120;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            else if (this.type == 40)
            {
                this.name = "Bone Serpent Body";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 30;
                this.defense = 12;
                this.lifeMax = 150;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            else if (this.type == 0x29)
            {
                this.name = "Bone Serpent Tail";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 20;
                this.defense = 0x12;
                this.lifeMax = 200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            else if (this.type == 0x2a)
            {
                this.name = "Hornet";
                this.width = 0x22;
                this.height = 0x20;
                this.aiStyle = 5;
                this.damage = 40;
                this.defense = 12;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 200f;
                this.noGravity = true;
            }
            else if (this.type == 0x2b)
            {
                this.noGravity = true;
                this.noTileCollide = true;
                this.behindTiles = true;
                this.name = "Man Eater";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 13;
                this.damage = 60;
                this.defense = 14;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.knockBackResist = 0f;
                this.soundKilled = 1;
                this.value = 350f;
            }
            else if (this.type == 0x2c)
            {
                this.name = "Undead Miner";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 0x16;
                this.defense = 9;
                this.lifeMax = 70;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 250f;
            }
            else if (this.type == 0x2d)
            {
                this.name = "Tim";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.6f;
                this.value = 5000f;
            }
            else if (this.type == 0x2e)
            {
                this.name = "Bunny";
                this.friendly = true;
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 7;
                this.damage = 0;
                this.defense = 0;
                this.lifeMax = 5;
                this.soundHit = 1;
                this.soundKilled = 1;
            }
            else if (this.type == 0x2f)
            {
                this.name = "Corrupt Bunny";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 70;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x30)
            {
                this.name = "Harpy";
                this.width = 0x18;
                this.height = 0x22;
                this.aiStyle = 14;
                this.damage = 0x19;
                this.defense = 8;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.6f;
                this.soundKilled = 1;
                this.value = 300f;
            }
            else if (this.type == 0x31)
            {
                npcSlots = 0.5f;
                this.name = "Cave Bat";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 14;
                this.damage = 15;
                this.defense = 2;
                this.lifeMax = 0x19;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 4;
                this.value = 90f;
            }
            else if (this.type == 50)
            {
                this.boss = true;
                this.name = "King Slime";
                this.width = 0x62;
                this.height = 0x5c;
                this.aiStyle = 15;
                this.damage = 40;
                this.defense = 10;
                this.lifeMax = 0x7d0;
                this.knockBackResist = 0f;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 30;
                this.value = 10000f;
                this.scale = 1.25f;
            }
            else if (this.type == 0x33)
            {
                npcSlots = 0.5f;
                this.name = "Jungle Bat";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 14;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 4;
                this.value = 80f;
            }
            else if (this.type == 0x34)
            {
                this.name = "Doctor Bones";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 10;
                this.lifeMax = 500;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 1000f;
            }
            else if (this.type == 0x35)
            {
                this.name = "The Groom";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 14;
                this.defense = 8;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 1000f;
            }
            else if (this.type == 0x36)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Clothier";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x37)
            {
                this.friendly = true;
                this.noGravity = true;
                this.name = "Goldfish";
                this.width = 20;
                this.height = 0x12;
                this.aiStyle = 0x10;
                this.damage = 0;
                this.defense = 0;
                this.lifeMax = 5;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x38)
            {
                this.noTileCollide = true;
                this.behindTiles = true;
                this.noGravity = true;
                this.name = "Snatcher";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 13;
                this.damage = 0x19;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0f;
                this.soundKilled = 1;
                this.value = 90f;
            }
            else if (this.type == 0x39)
            {
                this.noGravity = true;
                this.name = "Corrupt Goldfish";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 0x10;
                this.damage = 30;
                this.defense = 6;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x3a)
            {
                npcSlots = 0.5f;
                this.noGravity = true;
                this.name = "Piranha";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 0x10;
                this.damage = 0x19;
                this.defense = 2;
                this.lifeMax = 30;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 50f;
            }
            else if (this.type == 0x3b)
            {
                this.name = "Lava Slime";
                this.width = 0x18;
                this.height = 0x12;
                this.aiStyle = 1;
                this.damage = 15;
                this.defense = 10;
                this.lifeMax = 50;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.scale = 1.1f;
                this.alpha = 50;
                this.lavaImmune = true;
            }
            else if (this.type == 60)
            {
                npcSlots = 0.5f;
                this.name = "Hellbat";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 14;
                this.damage = 30;
                this.defense = 8;
                this.lifeMax = 0x37;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 4;
                this.value = 120f;
                this.scale = 1.1f;
                this.lavaImmune = true;
            }
            else if (this.type == 0x3d)
            {
                this.name = "Vulture";
                this.width = 0x24;
                this.height = 0x24;
                this.aiStyle = 0x11;
                this.damage = 15;
                this.defense = 4;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 60f;
            }
            else if (this.type == 0x3e)
            {
                npcSlots = 2f;
                this.name = "Demon";
                this.width = 0x1c;
                this.height = 0x30;
                this.aiStyle = 14;
                this.damage = 50;
                this.defense = 8;
                this.lifeMax = 120;
                this.soundHit = 1;
                this.knockBackResist = 0.6f;
                this.soundKilled = 1;
                this.value = 300f;
                this.lavaImmune = true;
            }
            else if (this.type == 0x3f)
            {
                this.noGravity = true;
                this.name = "Blue Jellyfish";
                this.width = 0x1a;
                this.height = 0x1a;
                this.aiStyle = 0x12;
                this.damage = 20;
                this.defense = 2;
                this.lifeMax = 30;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 100f;
                this.alpha = 20;
            }
            else if (this.type == 0x40)
            {
                this.noGravity = true;
                this.name = "Pink Jellyfish";
                this.width = 0x1a;
                this.height = 0x1a;
                this.aiStyle = 0x12;
                this.damage = 30;
                this.defense = 6;
                this.lifeMax = 70;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 100f;
                this.alpha = 20;
            }
            else if (this.type == 0x41)
            {
                this.noGravity = true;
                this.name = "Shark";
                this.width = 100;
                this.height = 0x18;
                this.aiStyle = 0x10;
                this.damage = 40;
                this.defense = 2;
                this.lifeMax = 300;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 400f;
                this.knockBackResist = 0.7f;
            }
            else if (this.type == 0x42)
            {
                npcSlots = 2f;
                this.name = "Voodoo Demon";
                this.width = 0x1c;
                this.height = 0x30;
                this.aiStyle = 14;
                this.damage = 50;
                this.defense = 8;
                this.lifeMax = 120;
                this.soundHit = 1;
                this.knockBackResist = 0.6f;
                this.soundKilled = 1;
                this.value = 1000f;
                this.lavaImmune = true;
            }
            else if (this.type == 0x43)
            {
                this.name = "Crab";
                this.width = 0x1c;
                this.height = 20;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 10;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 60f;
            }
            else if (this.type == 0x44)
            {
                this.name = "Dungeon Guardian";
                this.width = 80;
                this.height = 0x66;
                this.aiStyle = 11;
                this.damage = 0x2328;
                this.defense = 0x2328;
                this.lifeMax = 0x270f;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
            }
            else if (this.type == 0x45)
            {
                this.name = "Antlion";
                this.width = 0x18;
                this.height = 0x18;
                this.aiStyle = 0x13;
                this.damage = 10;
                this.defense = 6;
                this.lifeMax = 0x2d;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0f;
                this.value = 60f;
                this.behindTiles = true;
            }
            if (Main.dedServ)
            {
                this.frame = new Rectangle();
            }
            else
            {
                this.frame = new Rectangle(0, 0, Main.npcTexture[this.type].Width, Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type]);
            }
            this.width = (int) (this.width * this.scale);
            this.height = (int) (this.height * this.scale);
            this.life = this.lifeMax;
        }

        public void SetDefaults(string Name)
        {
            this.SetDefaults(0);
            if (Name == "Green Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.9f;
                this.damage = 6;
                this.defense = 0;
                this.life = 14;
                this.knockBackResist = 1.2f;
                this.color = new Color(0, 220, 40, 100);
                this.value = 3f;
            }
            else if (Name == "Pinky")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.6f;
                this.damage = 5;
                this.defense = 5;
                this.life = 150;
                this.knockBackResist = 1.4f;
                this.color = new Color(250, 30, 90, 90);
                this.value = 10000f;
            }
            else if (Name == "Baby Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.9f;
                this.damage = 13;
                this.defense = 4;
                this.life = 30;
                this.knockBackResist = 0.95f;
                this.alpha = 120;
                this.color = new Color(0, 0, 0, 50);
                this.value = 10f;
            }
            else if (Name == "Black Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 15;
                this.defense = 4;
                this.life = 0x2d;
                this.color = new Color(0, 0, 0, 50);
                this.value = 20f;
            }
            else if (Name == "Purple Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 1.2f;
                this.damage = 12;
                this.defense = 6;
                this.life = 40;
                this.knockBackResist = 0.9f;
                this.color = new Color(200, 0, 0xff, 150);
                this.value = 10f;
            }
            else if (Name == "Red Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 12;
                this.defense = 4;
                this.life = 0x23;
                this.color = new Color(0xff, 30, 0, 100);
                this.value = 8f;
            }
            else if (Name == "Yellow Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 1.2f;
                this.damage = 15;
                this.defense = 7;
                this.life = 0x2d;
                this.color = new Color(0xff, 0xff, 0, 100);
                this.value = 10f;
            }
            else if (Name == "Jungle Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 0x12;
                this.defense = 6;
                this.life = 60;
                this.scale = 1.1f;
                this.color = new Color(0x8f, 0xd7, 0x5d, 100);
                this.value = 500f;
            }
            else if (Name == "Little Eater")
            {
                this.SetDefaults(6);
                this.name = Name;
                this.scale = 0.85f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) (this.damage * this.scale);
                this.life = (int) (this.life * this.scale);
                this.value = (int) (this.value * this.scale);
                npcSlots *= this.scale;
                this.knockBackResist *= 2f - this.scale;
            }
            else if (Name == "Big Eater")
            {
                this.SetDefaults(6);
                this.name = Name;
                this.scale = 1.15f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) (this.damage * this.scale);
                this.life = (int) (this.life * this.scale);
                this.value = (int) (this.value * this.scale);
                npcSlots *= this.scale;
                this.knockBackResist *= 2f - this.scale;
            }
            else if (Name == "Short Bones")
            {
                this.SetDefaults(0x1f);
                this.name = Name;
                this.scale = 0.9f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) (this.damage * this.scale);
                this.life = (int) (this.life * this.scale);
                this.value = (int) (this.value * this.scale);
            }
            else if (Name == "Big Boned")
            {
                this.SetDefaults(0x1f);
                this.name = Name;
                this.scale = 1.15f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) ((this.damage * this.scale) * 1.1);
                this.life = (int) ((this.life * this.scale) * 1.1);
                this.value = (int) (this.value * this.scale);
                npcSlots = 2f;
                this.knockBackResist *= 2f - this.scale;
            }
            else if (Name == "Little Stinger")
            {
                this.SetDefaults(0x2a);
                this.name = Name;
                this.scale = 0.85f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) (this.damage * this.scale);
                this.life = (int) (this.life * this.scale);
                this.value = (int) (this.value * this.scale);
                npcSlots *= this.scale;
                this.knockBackResist *= 2f - this.scale;
            }
            else if (Name == "Big Stinger")
            {
                this.SetDefaults(0x2a);
                this.name = Name;
                this.scale = 1.15f;
                this.defense = (int) (this.defense * this.scale);
                this.damage = (int) (this.damage * this.scale);
                this.life = (int) (this.life * this.scale);
                this.value = (int) (this.value * this.scale);
                npcSlots *= this.scale;
                this.knockBackResist *= 2f - this.scale;
            }
            else if (Name != "")
            {
                for (int i = 1; i < 70; i++)
                {
                    this.SetDefaults(i);
                    if (this.name == Name)
                    {
                        break;
                    }
                    if (i == 0x45)
                    {
                        this.SetDefaults(0);
                        this.active = false;
                    }
                }
            }
            else
            {
                this.active = false;
            }
            this.lifeMax = this.life;
        }

        public static void SpawnNPC()
        {
            if (noSpawnCycle)
            {
                noSpawnCycle = false;
            }
            else
            {
                bool flag = false;
                bool flag2 = false;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < 0xff; i++)
                {
                    if (Main.player[i].active)
                    {
                        num3++;
                    }
                }
                for (int j = 0; j < 0xff; j++)
                {
                    if (!Main.player[j].active || Main.player[j].dead)
                    {
                        continue;
                    }
                    bool flag3 = false;
                    bool flag4 = false;
                    if (((Main.player[j].active && (Main.invasionType > 0)) && ((Main.invasionDelay == 0) && (Main.invasionSize > 0))) && (Main.player[j].position.Y < ((Main.worldSurface * 16.0) + sHeight)))
                    {
                        int num6 = 0xbb8;
                        if ((Main.player[j].position.X > ((Main.invasionX * 16.0) - num6)) && (Main.player[j].position.X < ((Main.invasionX * 16.0) + num6)))
                        {
                            flag3 = true;
                        }
                    }
                    flag = false;
                    spawnRate = defaultSpawnRate;
                    maxSpawns = defaultMaxSpawns;
                    if (Main.player[j].position.Y > ((Main.maxTilesY - 200) * 0x10))
                    {
                        spawnRate = (int) (spawnRate * 0.4f);
                        maxSpawns = (int) (maxSpawns * 2.1f);
                    }
                    else if (Main.player[j].position.Y > ((Main.rockLayer * 16.0) + sHeight))
                    {
                        spawnRate = (int) (spawnRate * 0.4);
                        maxSpawns = (int) (maxSpawns * 1.9f);
                    }
                    else if (Main.player[j].position.Y > ((Main.worldSurface * 16.0) + sHeight))
                    {
                        spawnRate = (int) (spawnRate * 0.5);
                        maxSpawns = (int) (maxSpawns * 1.7f);
                    }
                    else if (!Main.dayTime)
                    {
                        spawnRate = (int) (spawnRate * 0.6);
                        maxSpawns = (int) (maxSpawns * 1.3f);
                        if (Main.bloodMoon)
                        {
                            spawnRate = (int) (spawnRate * 0.3);
                            maxSpawns = (int) (maxSpawns * 1.8f);
                        }
                    }
                    if (Main.player[j].zoneDungeon)
                    {
                        spawnRate = (int) (defaultSpawnRate * 0.22);
                        maxSpawns = defaultMaxSpawns * 2;
                    }
                    else if (Main.player[j].zoneJungle)
                    {
                        spawnRate = (int) (spawnRate * 0.3);
                        maxSpawns = (int) (maxSpawns * 1.6f);
                    }
                    else if (Main.player[j].zoneEvil)
                    {
                        spawnRate = (int) (spawnRate * 0.4);
                        maxSpawns = (int) (maxSpawns * 1.6f);
                    }
                    else if (Main.player[j].zoneMeteor)
                    {
                        spawnRate = (int) (spawnRate * 0.4);
                        maxSpawns = (int) (maxSpawns * 1.1f);
                    }
                    if (Main.player[j].activeNPCs < (maxSpawns * 0.2))
                    {
                        spawnRate = (int) (spawnRate * 0.6f);
                    }
                    else if (Main.player[j].activeNPCs < (maxSpawns * 0.4))
                    {
                        spawnRate = (int) (spawnRate * 0.7f);
                    }
                    else if (Main.player[j].activeNPCs < (maxSpawns * 0.6))
                    {
                        spawnRate = (int) (spawnRate * 0.8f);
                    }
                    else if (Main.player[j].activeNPCs < (maxSpawns * 0.8))
                    {
                        spawnRate = (int) (spawnRate * 0.9f);
                    }
                    if (((Main.player[j].position.Y * 16f) > ((Main.worldSurface + Main.rockLayer) / 2.0)) || Main.player[j].zoneEvil)
                    {
                        if (Main.player[j].activeNPCs < (maxSpawns * 0.2))
                        {
                            spawnRate = (int) (spawnRate * 0.7f);
                        }
                        else if (Main.player[j].activeNPCs < (maxSpawns * 0.4))
                        {
                            spawnRate = (int) (spawnRate * 0.9f);
                        }
                    }
                    if (Main.player[j].inventory[Main.player[j].selectedItem].type == 0x94)
                    {
                        spawnRate = (int) (spawnRate * 0.75);
                        maxSpawns = (int) (maxSpawns * 1.5f);
                    }
                    if (Main.player[j].enemySpawns)
                    {
                        spawnRate = (int) (spawnRate * 0.5);
                        maxSpawns = (int) (maxSpawns * 2f);
                    }
                    if (spawnRate < (defaultSpawnRate * 0.1))
                    {
                        spawnRate = (int) (defaultSpawnRate * 0.1);
                    }
                    if (maxSpawns > (defaultMaxSpawns * 3))
                    {
                        maxSpawns = defaultMaxSpawns * 3;
                    }
                    if (flag3)
                    {
                        maxSpawns = (int) (defaultMaxSpawns * (1.0 + (0.4 * num3)));
                        spawnRate = 30;
                    }
                    if (Main.player[j].zoneDungeon && !downedBoss3)
                    {
                        spawnRate = 10;
                    }
                    bool flag5 = false;
                    if ((!flag3 && (!Main.bloodMoon || Main.dayTime)) && ((!Main.player[j].zoneDungeon && !Main.player[j].zoneEvil) && !Main.player[j].zoneMeteor))
                    {
                        if (Main.player[j].townNPCs == 1f)
                        {
                            if (Main.rand.Next(3) <= 1)
                            {
                                flag5 = true;
                                maxSpawns = (int) (maxSpawns * 0.6);
                            }
                            else
                            {
                                spawnRate = (int) (spawnRate * 2f);
                            }
                        }
                        else if (Main.player[j].townNPCs == 2f)
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                flag5 = true;
                                maxSpawns = (int) (maxSpawns * 0.6);
                            }
                            else
                            {
                                spawnRate = (int) (spawnRate * 3f);
                            }
                        }
                        else if (Main.player[j].townNPCs >= 3f)
                        {
                            flag5 = true;
                            maxSpawns = (int) (maxSpawns * 0.6);
                        }
                    }
                    if ((Main.player[j].active && !Main.player[j].dead) && ((Main.player[j].activeNPCs < maxSpawns) && (Main.rand.Next(spawnRate) == 0)))
                    {
                        int minValue = ((int) (Main.player[j].position.X / 16f)) - spawnRangeX;
                        int maxValue = ((int) (Main.player[j].position.X / 16f)) + spawnRangeX;
                        int num9 = ((int) (Main.player[j].position.Y / 16f)) - spawnRangeY;
                        int maxTilesY = ((int) (Main.player[j].position.Y / 16f)) + spawnRangeY;
                        int num11 = ((int) (Main.player[j].position.X / 16f)) - safeRangeX;
                        int num12 = ((int) (Main.player[j].position.X / 16f)) + safeRangeX;
                        int num13 = ((int) (Main.player[j].position.Y / 16f)) - safeRangeY;
                        int num14 = ((int) (Main.player[j].position.Y / 16f)) + safeRangeY;
                        if (minValue < 0)
                        {
                            minValue = 0;
                        }
                        if (maxValue > Main.maxTilesX)
                        {
                            maxValue = Main.maxTilesX;
                        }
                        if (num9 < 0)
                        {
                            num9 = 0;
                        }
                        if (maxTilesY > Main.maxTilesY)
                        {
                            maxTilesY = Main.maxTilesY;
                        }
                        for (int k = 0; k < 50; k++)
                        {
                            int num16 = Main.rand.Next(minValue, maxValue);
                            int num17 = Main.rand.Next(num9, maxTilesY);
                            if (!Main.tile[num16, num17].active || !Main.tileSolid[Main.tile[num16, num17].type])
                            {
                                if (Main.wallHouse[Main.tile[num16, num17].wall])
                                {
                                    goto Label_0AD4;
                                }
                                if (((!flag3 && (num17 < (Main.worldSurface * 0.30000001192092896))) && !flag5) && ((num16 < (Main.maxTilesX * 0.35)) || (num16 > (Main.maxTilesX * 0.65))))
                                {
                                    byte type = Main.tile[num16, num17].type;
                                    num = num16;
                                    num2 = num17;
                                    flag = true;
                                    flag2 = true;
                                }
                                else
                                {
                                    for (int m = num17; m < Main.maxTilesY; m++)
                                    {
                                        if (Main.tile[num16, m].active && Main.tileSolid[Main.tile[num16, m].type])
                                        {
                                            if (((num16 < num11) || (num16 > num12)) || ((m < num13) || (m > num14)))
                                            {
                                                byte num29 = Main.tile[num16, m].type;
                                                num = num16;
                                                num2 = m;
                                                flag = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    int num19 = num - (spawnSpaceX / 2);
                                    int num20 = num + (spawnSpaceX / 2);
                                    int num21 = num2 - spawnSpaceY;
                                    int num22 = num2;
                                    if (num19 < 0)
                                    {
                                        flag = false;
                                    }
                                    if (num20 > Main.maxTilesX)
                                    {
                                        flag = false;
                                    }
                                    if (num21 < 0)
                                    {
                                        flag = false;
                                    }
                                    if (num22 > Main.maxTilesY)
                                    {
                                        flag = false;
                                    }
                                    if (flag)
                                    {
                                        for (int n = num19; n < num20; n++)
                                        {
                                            for (int num24 = num21; num24 < num22; num24++)
                                            {
                                                if (Main.tile[n, num24].active && Main.tileSolid[Main.tile[n, num24].type])
                                                {
                                                    flag = false;
                                                    break;
                                                }
                                                if (Main.tile[n, num24].lava)
                                                {
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (flag || flag)
                            {
                                break;
                            }
                        Label_0AD4:;
                        }
                    }
                    if (flag)
                    {
                        Rectangle rectangle = new Rectangle(num * 0x10, num2 * 0x10, 0x10, 0x10);
                        for (int num25 = 0; num25 < 0xff; num25++)
                        {
                            if (Main.player[num25].active)
                            {
                                Rectangle rectangle2 = new Rectangle(((((int) Main.player[num25].position.X) + (Main.player[num25].width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) Main.player[num25].position.Y) + (Main.player[num25].height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                                if (rectangle.Intersects(rectangle2))
                                {
                                    flag = false;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        if (Main.player[j].zoneDungeon && (!Main.tileDungeon[Main.tile[num, num2].type] || (Main.tile[num, num2 - 1].wall == 0)))
                        {
                            flag = false;
                        }
                        if (((Main.tile[num, num2 - 1].liquid > 0) && (Main.tile[num, num2 - 2].liquid > 0)) && !Main.tile[num, num2 - 1].lava)
                        {
                            flag4 = true;
                        }
                    }
                    if (flag)
                    {
                        flag = false;
                        int num26 = Main.tile[num, num2].type;
                        int index = 0x3e8;
                        if (flag2)
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x30, 0);
                        }
                        else if (flag3)
                        {
                            if (Main.rand.Next(9) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1d, 0);
                            }
                            else if (Main.rand.Next(5) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1a, 0);
                            }
                            else if (Main.rand.Next(3) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1b, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1c, 0);
                            }
                        }
                        else if ((flag4 && ((num < 250) || (num > (Main.maxTilesX - 250)))) && ((num26 == 0x35) && (num2 < Main.rockLayer)))
                        {
                            if (Main.rand.Next(8) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x41, 0);
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x43, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x40, 0);
                            }
                        }
                        else if (flag4 && (((num2 > Main.rockLayer) && (Main.rand.Next(2) == 0)) || (num26 == 60)))
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3a, 0);
                        }
                        else if ((flag4 && (num2 > Main.worldSurface)) && (Main.rand.Next(3) == 0))
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3f, 0);
                        }
                        else if (flag4 && (Main.rand.Next(4) == 0))
                        {
                            if (Main.player[j].zoneEvil)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x39, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x37, 0);
                            }
                        }
                        else if (flag5)
                        {
                            if (!flag4)
                            {
                                if (num26 != 2)
                                {
                                    return;
                                }
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2e, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x37, 0);
                            }
                        }
                        else if (Main.player[j].zoneDungeon)
                        {
                            if (!downedBoss3)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x44, 0);
                            }
                            else if (Main.rand.Next(3) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x22, 0);
                            }
                            else if (Main.rand.Next(6) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x20, 0);
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1f, 0);
                                if (Main.rand.Next(4) == 0)
                                {
                                    Main.npc[index].SetDefaults("Big Boned");
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    Main.npc[index].SetDefaults("Short Bones");
                                }
                            }
                        }
                        else if (Main.player[j].zoneMeteor)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x17, 0);
                        }
                        else if (Main.player[j].zoneEvil && (Main.rand.Next(50) == 0))
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 7, 1);
                        }
                        else if (((num26 == 60) && (Main.rand.Next(500) == 0)) && !Main.dayTime)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x34, 0);
                        }
                        else if ((num26 == 60) && (num2 > ((Main.worldSurface + Main.rockLayer) / 2.0)))
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2b, 0);
                                Main.npc[index].ai[0] = num;
                                Main.npc[index].ai[1] = num2;
                                Main.npc[index].netUpdate = true;
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2a, 0);
                                if (Main.rand.Next(4) == 0)
                                {
                                    Main.npc[index].SetDefaults("Little Stinger");
                                }
                                else if (Main.rand.Next(4) == 0)
                                {
                                    Main.npc[index].SetDefaults("Big Stinger");
                                }
                            }
                        }
                        else if ((num26 == 60) && (Main.rand.Next(4) == 0))
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x33, 0);
                        }
                        else if ((num26 == 60) && (Main.rand.Next(8) == 0))
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x38, 0);
                            Main.npc[index].ai[0] = num;
                            Main.npc[index].ai[1] = num2;
                            Main.npc[index].netUpdate = true;
                        }
                        else if (((num26 == 0x16) && Main.player[j].zoneEvil) || ((num26 == 0x17) || (num26 == 0x19)))
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 6, 0);
                            if (Main.rand.Next(3) == 0)
                            {
                                Main.npc[index].SetDefaults("Little Eater");
                            }
                            else if (Main.rand.Next(3) == 0)
                            {
                                Main.npc[index].SetDefaults("Big Eater");
                            }
                        }
                        else if (num2 <= Main.worldSurface)
                        {
                            if (Main.dayTime)
                            {
                                int num28 = Math.Abs((int) (num - Main.spawnTileX));
                                if (((num28 < (Main.maxTilesX / 3)) && (Main.rand.Next(10) == 0)) && (num26 == 2))
                                {
                                    NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2e, 0);
                                }
                                else if (((num28 > (Main.maxTilesX / 3)) && (num26 == 2)) && ((Main.rand.Next(300) == 0) && !AnyNPCs(50)))
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 50, 0);
                                }
                                else if (((num26 == 0x35) && (Main.rand.Next(5) == 0)) && !flag4)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x45, 0);
                                }
                                else if ((num26 == 0x35) && !flag4)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3d, 0);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                                    if (num26 == 60)
                                    {
                                        Main.npc[index].SetDefaults("Jungle Slime");
                                    }
                                    else if ((Main.rand.Next(3) == 0) || (num28 < 200))
                                    {
                                        Main.npc[index].SetDefaults("Green Slime");
                                    }
                                    else if ((Main.rand.Next(10) == 0) && (num28 > 400))
                                    {
                                        Main.npc[index].SetDefaults("Purple Slime");
                                    }
                                }
                            }
                            else if ((Main.rand.Next(6) == 0) || ((Main.moonPhase == 4) && (Main.rand.Next(2) == 0)))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 2, 0);
                            }
                            else if ((Main.rand.Next(250) == 0) && Main.bloodMoon)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x35, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 3, 0);
                            }
                        }
                        else if (num2 <= Main.rockLayer)
                        {
                            if (Main.rand.Next(30) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 10, 1);
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                                if (Main.rand.Next(5) == 0)
                                {
                                    Main.npc[index].SetDefaults("Yellow Slime");
                                }
                                else if (Main.rand.Next(2) == 0)
                                {
                                    Main.npc[index].SetDefaults("Blue Slime");
                                }
                                else
                                {
                                    Main.npc[index].SetDefaults("Red Slime");
                                }
                            }
                        }
                        else if (num2 > (Main.maxTilesY - 190))
                        {
                            if ((Main.rand.Next(40) == 0) && !AnyNPCs(0x27))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x27, 1);
                            }
                            else if (Main.rand.Next(20) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x18, 0);
                            }
                            else if (Main.rand.Next(12) == 0)
                            {
                                if (Main.rand.Next(10) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x42, 0);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3e, 0);
                                }
                            }
                            else if (Main.rand.Next(3) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3b, 0);
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 60, 0);
                            }
                        }
                        else if (Main.rand.Next(0x23) == 0)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 10, 1);
                        }
                        else if (Main.rand.Next(10) == 0)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x10, 0);
                        }
                        else if (Main.rand.Next(4) == 0)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                            if (Main.player[j].zoneJungle)
                            {
                                Main.npc[index].SetDefaults("Jungle Slime");
                            }
                            else
                            {
                                Main.npc[index].SetDefaults("Black Slime");
                            }
                        }
                        else if (Main.rand.Next(2) == 0)
                        {
                            if ((num2 > ((Main.rockLayer + Main.maxTilesY) / 2.0)) && (Main.rand.Next(700) == 0))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2d, 0);
                            }
                            else if (Main.rand.Next(15) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2c, 0);
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x15, 0);
                            }
                        }
                        else if (Main.player[j].zoneJungle)
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x33, 0);
                        }
                        else
                        {
                            index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x31, 0);
                        }
                        if ((Main.npc[index].type == 1) && (Main.rand.Next(250) == 0))
                        {
                            Main.npc[index].SetDefaults("Pinky");
                        }
                        if ((Main.netMode == 2) && (index < 0x3e8))
                        {
                            NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f, 0);
                            return;
                        }
                        break;
                    }
                }
            }
        }

        public static void SpawnOnPlayer(int plr, int Type)
        {
            if (Main.netMode != 1)
            {
                bool flag = false;
                int num = 0;
                int num2 = 0;
                int minValue = ((int) (Main.player[plr].position.X / 16f)) - (spawnRangeX * 3);
                int maxValue = ((int) (Main.player[plr].position.X / 16f)) + (spawnRangeX * 3);
                int num5 = ((int) (Main.player[plr].position.Y / 16f)) - (spawnRangeY * 3);
                int maxTilesY = ((int) (Main.player[plr].position.Y / 16f)) + (spawnRangeY * 3);
                int num7 = ((int) (Main.player[plr].position.X / 16f)) - safeRangeX;
                int num8 = ((int) (Main.player[plr].position.X / 16f)) + safeRangeX;
                int num9 = ((int) (Main.player[plr].position.Y / 16f)) - safeRangeY;
                int num10 = ((int) (Main.player[plr].position.Y / 16f)) + safeRangeY;
                if (minValue < 0)
                {
                    minValue = 0;
                }
                if (maxValue > Main.maxTilesX)
                {
                    maxValue = Main.maxTilesX;
                }
                if (num5 < 0)
                {
                    num5 = 0;
                }
                if (maxTilesY > Main.maxTilesY)
                {
                    maxTilesY = Main.maxTilesY;
                }
                for (int i = 0; i < 0x3e8; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        int num13 = Main.rand.Next(minValue, maxValue);
                        int num14 = Main.rand.Next(num5, maxTilesY);
                        if (!Main.tile[num13, num14].active || !Main.tileSolid[Main.tile[num13, num14].type])
                        {
                            if (Main.tile[num13, num14].wall == 1)
                            {
                                continue;
                            }
                            for (int k = num14; k < Main.maxTilesY; k++)
                            {
                                if (Main.tile[num13, k].active && Main.tileSolid[Main.tile[num13, k].type])
                                {
                                    if (((num13 < num7) || (num13 > num8)) || ((k < num9) || (k > num10)))
                                    {
                                        byte type = Main.tile[num13, k].type;
                                        num = num13;
                                        num2 = k;
                                        flag = true;
                                    }
                                    break;
                                }
                            }
                            if (flag)
                            {
                                int num16 = num - (spawnSpaceX / 2);
                                int num17 = num + (spawnSpaceX / 2);
                                int num18 = num2 - spawnSpaceY;
                                int num19 = num2;
                                if (num16 < 0)
                                {
                                    flag = false;
                                }
                                if (num17 > Main.maxTilesX)
                                {
                                    flag = false;
                                }
                                if (num18 < 0)
                                {
                                    flag = false;
                                }
                                if (num19 > Main.maxTilesY)
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    for (int m = num16; m < num17; m++)
                                    {
                                        for (int n = num18; n < num19; n++)
                                        {
                                            if (Main.tile[m, n].active && Main.tileSolid[Main.tile[m, n].type])
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (flag || flag)
                        {
                            break;
                        }
                    }
                    if (flag)
                    {
                        Rectangle rectangle = new Rectangle(num * 0x10, num2 * 0x10, 0x10, 0x10);
                        for (int num22 = 0; num22 < 0xff; num22++)
                        {
                            if (Main.player[num22].active)
                            {
                                Rectangle rectangle2 = new Rectangle(((((int) Main.player[num22].position.X) + (Main.player[num22].width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) Main.player[num22].position.Y) + (Main.player[num22].height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                                if (rectangle.Intersects(rectangle2))
                                {
                                    flag = false;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    int index = 0x3e8;
                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, Type, 1);
                    Main.npc[index].target = plr;
                    string name = Main.npc[index].name;
                    if (Main.npc[index].type == 13)
                    {
                        name = "Eater of Worlds";
                    }
                    if (Main.npc[index].type == 0x23)
                    {
                        name = "Skeletron";
                    }
                    if ((Main.netMode == 2) && (index < 0x3e8))
                    {
                        NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f, 0);
                    }
                    if (Main.netMode == 0)
                    {
                        Main.NewText(name + " has awoken!", 0xaf, 0x4b, 0xff);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, name + " has awoken!", 0xff, 175f, 75f, 255f, 0);
                    }
                }
            }
        }

        public static void SpawnSkeletron()
        {
            //Mod
            bool flag = true;
            bool flag2 = false;
            int index = -1;
            Vector2 position = new Vector2();
            int width = 0;
            int height = 0;

            //this bit of code is to make sure only one skeletron can be spawned, we want more than one skeletron, so comment it out
            /*for (int i = 0; i < 0x3e8; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == 0x23))
                {
                    flag = false;
                    break;
                }
            }*/

            //this loop is to make sure we have the old man, we want to spawn skeletron whenever we want
            oldman:
            for (int j = 0; j < 0x3e8; j++)
            {
                if (Main.npc[j].active && (Main.npc[j].type == 0x25))
                {
                    flag2 = true;
                    Main.npc[j].ai[3] = 1f;
                    position = Main.npc[j].position;
                    width = Main.npc[j].width;
                    height = Main.npc[j].height;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x17, -1, -1, "", j, 0f, 0f, 0f, 0);
                    }
                }
            }

            if (!flag2)
            {
                Main.dayTime = false; //make it night
                Main.time = 0.0;
                if (Main.netMode == 2)
                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);

                Vector2 position2;

                position2.X = ((Main.spawnTileX * 0x10) + 8);
                position2.Y = (Main.spawnTileY * 0x10) - 10;
                index = NPC.NewNPC(((int)position2.X), ((int)position2.Y), 0x25, 0);
                goto oldman;
            }

            if (flag && flag2)
            {
                Console.WriteLine("Skeletron data sending.");

                index = NewNPC(((int) position.X) + (width / 2), ((int) position.Y) + (height / 2), 0x23, 0);
                Main.npc[index].netUpdate = true;
                string str = "Skeletron";
                if (Main.netMode == 0)
                {
                    Main.NewText(str + " has awoken!", 0xaf, 0x4b, 0xff);
                }
                else if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, str + " has awoken!", 0xff, 175f, 75f, 255f, 0);
                    NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f, 0);
                }
            }
        }

        public double StrikeNPC(int Damage, float knockBack, int hitDirection)
        {
            if (!this.active || (this.life <= 0))
            {
                return 0.0;
            }
            double dmg = Main.CalculateDamage(Damage, this.defense);
            if (this.damage != 0x270f)
            {
                if (this.friendly)
                {
                    CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(0xff, 80, 90, 0xff), ((int) dmg).ToString());
                }
                else
                {
                    CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(0xff, 160, 80, 0xff), ((int) dmg).ToString());
                }
            }
            if (dmg < 1.0)
            {
                return 0.0;
            }
            if (this.townNPC)
            {
                this.ai[0] = 1f;
                this.ai[1] = 300 + Main.rand.Next(300);
                this.ai[2] = 0f;
                this.direction = hitDirection;
                this.netUpdate = true;
            }
            if ((this.aiStyle == 8) && (Main.netMode != 1))
            {
                this.ai[0] = 400f;
                this.TargetClosest(true);
            }
            this.life -= (int) dmg;
            if ((knockBack > 0f) && (this.knockBackResist > 0f))
            {
                if (!this.noGravity)
                {
                    this.velocity.Y = (-knockBack * 0.75f) * this.knockBackResist;
                }
                else
                {
                    this.velocity.Y = (-knockBack * 0.5f) * this.knockBackResist;
                }
                this.velocity.X = (knockBack * hitDirection) * this.knockBackResist;
            }
            this.HitEffect(hitDirection, dmg);
            if (this.soundHit > 0)
            {
                Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, this.soundHit);
            }
            if (this.life <= 0)
            {
                noSpawnCycle = true;
                if (this.townNPC && (this.type != 0x25))
                {
                    if (Main.netMode == 0)
                    {
                        Main.NewText(this.name + " was slain...", 0xff, 0x19, 0x19);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, this.name + " was slain...", 0xff, 255f, 25f, 25f, 0);
                    }
                }
                if ((this.townNPC && (Main.netMode != 1)) && (this.homeless && (WorldGen.spawnNPC == this.type)))
                {
                    WorldGen.spawnNPC = 0;
                }
                if (this.soundKilled > 0)
                {
                    Main.PlaySound(4, (int) this.position.X, (int) this.position.Y, this.soundKilled);
                }
                this.NPCLoot();
                this.active = false;
                if (((this.type != 0x1a) && (this.type != 0x1b)) && ((this.type != 0x1c) && (this.type != 0x1d)))
                {
                    return dmg;
                }
                Main.invasionSize--;
            }
            return dmg;
        }

        public void TargetClosest(bool faceTarget = true)
        {
            float num = -1f;
            for (int i = 0; i < 0xff; i++)
            {
                if ((Main.player[i].active && !Main.player[i].dead) && ((num == -1f) || ((Math.Abs((float) (((Main.player[i].position.X + (Main.player[i].width / 2)) - this.position.X) + (this.width / 2))) + Math.Abs((float) (((Main.player[i].position.Y + (Main.player[i].height / 2)) - this.position.Y) + (this.height / 2)))) < num)))
                {
                    num = Math.Abs((float) (((Main.player[i].position.X + (Main.player[i].width / 2)) - this.position.X) + (this.width / 2))) + Math.Abs((float) (((Main.player[i].position.Y + (Main.player[i].height / 2)) - this.position.Y) + (this.height / 2)));
                    this.target = i;
                }
            }
            if ((this.target < 0) || (this.target >= 0xff))
            {
                this.target = 0;
            }
            this.targetRect = new Rectangle((int) Main.player[this.target].position.X, (int) Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
            if (faceTarget)
            {
                this.direction = 1;
                if ((this.targetRect.X + (this.targetRect.Width / 2)) < (this.position.X + (this.width / 2)))
                {
                    this.direction = -1;
                }
                this.directionY = 1;
                if ((this.targetRect.Y + (this.targetRect.Height / 2)) < (this.position.Y + (this.height / 2)))
                {
                    this.directionY = -1;
                }
            }
            if (((this.direction != this.oldDirection) || (this.directionY != this.oldDirectionY)) || (this.target != this.oldTarget))
            {
                this.netUpdate = true;
            }
        }

        public void Transform(int newType)
        {
            if (Main.netMode != 1)
            {
                Vector2 velocity = this.velocity;
                int spriteDirection = this.spriteDirection;
                this.SetDefaults(newType);
                this.spriteDirection = spriteDirection;
                this.TargetClosest(true);
                this.velocity = velocity;
                if (Main.netMode == 2)
                {
                    this.netUpdate = true;
                    NetMessage.SendData(0x17, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
                }
            }
        }

        public void UpdateNPC(int i)
        {
            this.whoAmI = i;
            if (this.active)
            {
                if ((Main.netMode != 1) && Main.bloodMoon)
                {
                    if (this.type == 0x2e)
                    {
                        this.Transform(0x2f);
                    }
                    else if (this.type == 0x37)
                    {
                        this.Transform(0x39);
                    }
                }
                float num = 10f;
                float num2 = 0.3f;
                if (this.wet)
                {
                    num2 = 0.2f;
                    num = 7f;
                }
                if (this.soundDelay > 0)
                {
                    this.soundDelay--;
                }
                if (this.life <= 0)
                {
                    this.active = false;
                }
                this.oldTarget = this.target;
                this.oldDirection = this.direction;
                this.oldDirectionY = this.directionY;
                this.AI();
                if (this.type == 0x2c)
                {
                    Lighting.addLight((((int) this.position.X) + (this.width / 2)) / 0x10, ((int) (this.position.Y + 4f)) / 0x10, 0.6f);
                }
                for (int j = 0; j < 0x100; j++)
                {
                    if (this.immune[j] > 0)
                    {
                        this.immune[j]--;
                    }
                }
                if (!this.noGravity)
                {
                    this.velocity.Y += num2;
                    if (this.velocity.Y > num)
                    {
                        this.velocity.Y = num;
                    }
                }
                if ((this.velocity.X < 0.005) && (this.velocity.X > -0.005))
                {
                    this.velocity.X = 0f;
                }
                if (((Main.netMode != 1) && this.friendly) && (this.type != 0x25))
                {
                    if (this.life < this.lifeMax)
                    {
                        this.friendlyRegen++;
                        if (this.friendlyRegen > 300)
                        {
                            this.friendlyRegen = 0;
                            this.life++;
                            this.netUpdate = true;
                        }
                    }
                    if (this.immune[0xff] == 0)
                    {
                        Rectangle rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                        for (int k = 0; k < 0x3e8; k++)
                        {
                            if ((Main.npc[k].active && !Main.npc[k].friendly) && (Main.npc[k].damage > 0))
                            {
                                Rectangle rectangle2 = new Rectangle((int) Main.npc[k].position.X, (int) Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height);
                                if (rectangle.Intersects(rectangle2))
                                {
                                    int damage = Main.npc[k].damage;
                                    int num6 = 6;
                                    int hitDirection = 1;
                                    if ((Main.npc[k].position.X + (Main.npc[k].width / 2)) > (this.position.X + (this.width / 2)))
                                    {
                                        hitDirection = -1;
                                    }
                                    Main.npc[i].StrikeNPC(damage, (float) num6, hitDirection);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(0x1c, -1, -1, "", i, (float) damage, (float) num6, (float) hitDirection, 0);
                                    }
                                    this.netUpdate = true;
                                    this.immune[0xff] = 30;
                                }
                            }
                        }
                    }
                }
                if (!this.noTileCollide)
                {
                    bool flag = Collision.LavaCollision(this.position, this.width, this.height);
                    if (flag)
                    {
                        this.lavaWet = true;
                        if ((!this.lavaImmune && (Main.netMode != 1)) && (this.immune[0xff] == 0))
                        {
                            this.immune[0xff] = 30;
                            this.StrikeNPC(50, 0f, 0);
                            if ((Main.netMode == 2) && (Main.netMode != 0))
                            {
                                NetMessage.SendData(0x1c, -1, -1, "", this.whoAmI, 50f, 0f, 0f, 0);
                            }
                        }
                    }
                    if (Collision.WetCollision(this.position, this.width, this.height))
                    {
                        if (!this.wet && (this.wetCount == 0))
                        {
                            this.wetCount = 10;
                            if (!flag)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    Color newColor = new Color();
                                    int index = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x21, 0f, 0f, 0, newColor, 1f);
                                    Main.dust[index].velocity.Y -= 4f;
                                    Main.dust[index].velocity.X *= 2.5f;
                                    Main.dust[index].scale = 1.3f;
                                    Main.dust[index].alpha = 100;
                                    Main.dust[index].noGravity = true;
                                }
                                if (((this.type != 1) && (this.type != 0x3b)) && !this.noGravity)
                                {
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                                }
                            }
                            else
                            {
                                for (int n = 0; n < 10; n++)
                                {
                                    Color color2 = new Color();
                                    int num11 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color2, 1f);
                                    Main.dust[num11].velocity.Y -= 1.5f;
                                    Main.dust[num11].velocity.X *= 2.5f;
                                    Main.dust[num11].scale = 1.3f;
                                    Main.dust[num11].alpha = 100;
                                    Main.dust[num11].noGravity = true;
                                }
                                if (((this.type != 1) && (this.type != 0x3b)) && !this.noGravity)
                                {
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                }
                            }
                        }
                        this.wet = true;
                    }
                    else if (this.wet)
                    {
                        this.velocity.X *= 0.5f;
                        this.wet = false;
                        if (this.wetCount == 0)
                        {
                            this.wetCount = 10;
                            if (!this.lavaWet)
                            {
                                for (int num12 = 0; num12 < 30; num12++)
                                {
                                    Color color3 = new Color();
                                    int num13 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color3, 1f);
                                    Main.dust[num13].velocity.Y -= 4f;
                                    Main.dust[num13].velocity.X *= 2.5f;
                                    Main.dust[num13].scale = 1.3f;
                                    Main.dust[num13].alpha = 100;
                                    Main.dust[num13].noGravity = true;
                                }
                                if (((this.type != 1) && (this.type != 0x3b)) && !this.noGravity)
                                {
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                                }
                            }
                            else
                            {
                                for (int num14 = 0; num14 < 10; num14++)
                                {
                                    Color color4 = new Color();
                                    int num15 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color4, 1f);
                                    Main.dust[num15].velocity.Y -= 1.5f;
                                    Main.dust[num15].velocity.X *= 2.5f;
                                    Main.dust[num15].scale = 1.3f;
                                    Main.dust[num15].alpha = 100;
                                    Main.dust[num15].noGravity = true;
                                }
                                if (((this.type != 1) && (this.type != 0x3b)) && !this.noGravity)
                                {
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                }
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
                    bool fallThrough = false;
                    if (this.aiStyle == 10)
                    {
                        fallThrough = true;
                    }
                    if ((this.aiStyle == 3) && (this.directionY == 1))
                    {
                        fallThrough = true;
                    }
                    this.oldVelocity = this.velocity;
                    this.collideX = false;
                    this.collideY = false;
                    if (this.wet)
                    {
                        Vector2 velocity = this.velocity;
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                        if (Collision.up)
                        {
                            this.velocity.Y = 0.01f;
                        }
                        Vector2 vector2 = (Vector2) (this.velocity * 0.5f);
                        if (this.velocity.X != velocity.X)
                        {
                            vector2.X = this.velocity.X;
                            this.collideX = true;
                        }
                        if (this.velocity.Y != velocity.Y)
                        {
                            vector2.Y = this.velocity.Y;
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += vector2;
                    }
                    else
                    {
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                        if (Collision.up)
                        {
                            this.velocity.Y = 0.01f;
                        }
                        if (this.oldVelocity.X != this.velocity.X)
                        {
                            this.collideX = true;
                        }
                        if (this.oldVelocity.Y != this.velocity.Y)
                        {
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += this.velocity;
                    }
                }
                else
                {
                    this.oldPosition = this.position;
                    this.position += this.velocity;
                }
                if (!this.active)
                {
                    this.netUpdate = true;
                }
                if ((Main.netMode == 2) && this.netUpdate)
                {
                    NetMessage.SendData(0x17, -1, -1, "", i, 0f, 0f, 0f, 0);
                }
                this.FindFrame();
                this.CheckActive();
                this.netUpdate = false;
            }
        }
    }
}

