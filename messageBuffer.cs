namespace Terraria
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using System;
    using System.Text;

    public class messageBuffer
    {
        public bool broadcast;
        public bool checkBytes;
        public int maxSpam;
        public int messageLength;
        public byte[] readBuffer = new byte[0xffff];
        public const int readBufferMax = 0xffff;
        public int spamCount;
        public int totalData;
        public int whoAmI;
        public byte[] writeBuffer = new byte[0xffff];
        public const int writeBufferMax = 0xffff;
        public bool writeLocked;

        public void GetData(int start, int length)
        {
            byte num61;
            int num62;
            int num63;
            int num64;
            int num161;
            int num162;
            int team;
            string str16;
            int num164;
            if (this.whoAmI < 0x100)
            {
                Netplay.serverSock[this.whoAmI].timeOut = 0;
            }
            else
            {
                Netplay.clientSock.timeOut = 0;
            }
            byte msgType = 0;
            int startIndex = 0;
            startIndex = start + 1;
            msgType = this.readBuffer[start];
            if ((Main.netMode == 1) && (Netplay.clientSock.statusMax > 0))
            {
                Netplay.clientSock.statusCount++;
            }
            if (Main.verboseNetplay)
            {
                for (int i = start; i < (start + length); i++)
                {
                }
                for (int j = start; j < (start + length); j++)
                {
                    byte num1 = this.readBuffer[j];
                }
            }
            if (((Main.netMode == 2) && (msgType != 0x26)) && (Netplay.serverSock[this.whoAmI].state == -1))
            {
                NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f, 0);
                return;
            }
            if ((((Main.netMode == 2) && (Netplay.serverSock[this.whoAmI].state < 10)) && ((msgType > 12) && (msgType != 0x10))) && (((msgType != 0x2a) && (msgType != 50)) && (msgType != 0x26)))
            {
                NetMessage.BootPlayer(this.whoAmI, "Invalid operation at this state.");
            }
            if ((msgType == 1) && (Main.netMode == 2))
            {
                if (Main.dedServ && Netplay.CheckBan(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
                {
                    NetMessage.SendData(2, this.whoAmI, -1, "You are banned from this server.", 0, 0f, 0f, 0f, 0);
                    return;
                }
                if (Netplay.serverSock[this.whoAmI].state == 0)
                {
                    if (Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1) == ("Terraria" + Main.curRelease))
                    {
                        if ((Netplay.password == null) || (Netplay.password == ""))
                        {
                            Netplay.serverSock[this.whoAmI].state = 1;
                            NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
                            return;
                        }
                        Netplay.serverSock[this.whoAmI].state = -1;
                        NetMessage.SendData(0x25, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
                        return;
                    }
                    NetMessage.SendData(2, this.whoAmI, -1, "You are not using the same version as this server.", 0, 0f, 0f, 0f, 0);
                    return;
                }
                return;
            }
            if ((msgType == 2) && (Main.netMode == 1))
            {
                Netplay.disconnect = true;
                Main.statusText = Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1);
                return;
            }
            if ((msgType == 3) && (Main.netMode == 1))
            {
                if (Netplay.clientSock.state == 1)
                {
                    Netplay.clientSock.state = 2;
                }
                int index = this.readBuffer[start + 1];
                if (index != Main.myPlayer)
                {
                    Main.player[index] = (Player) Main.player[Main.myPlayer].Clone();
                    Main.player[Main.myPlayer] = new Player();
                    Main.player[index].whoAmi = index;
                    Main.myPlayer = index;
                }
                NetMessage.SendData(4, -1, -1, Main.player[Main.myPlayer].name, Main.myPlayer, 0f, 0f, 0f, 0);
                NetMessage.SendData(0x10, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                NetMessage.SendData(0x2a, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                NetMessage.SendData(50, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
                for (int k = 0; k < 0x2c; k++)
                {
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[k].name, Main.myPlayer, (float) k, 0f, 0f, 0);
                }
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 44f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 45f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 46f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 47f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 48f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 49f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 50f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 51f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 52f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 53f, 0f, 0f, 0);
                NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 54f, 0f, 0f, 0);
                NetMessage.SendData(6, -1, -1, "", 0, 0f, 0f, 0f, 0);
                if (Netplay.clientSock.state == 2)
                {
                    Netplay.clientSock.state = 3;
                    return;
                }
                return;
            }
            switch (msgType)
            {
                case 5:
                {
                    int whoAmI = this.readBuffer[start + 1];
                    if (Main.netMode == 2)
                    {
                        whoAmI = this.whoAmI;
                    }
                    if (whoAmI == Main.myPlayer)
                    {
                        return;
                    }
                    lock (Main.player[whoAmI])
                    {
                        int num12 = this.readBuffer[start + 2];
                        int num13 = this.readBuffer[start + 3];
                        string itemName = Encoding.ASCII.GetString(this.readBuffer, start + 4, length - 4);
                        if (num12 < 0x2c)
                        {
                            Main.player[whoAmI].inventory[num12] = new Item();
                            Main.player[whoAmI].inventory[num12].SetDefaults(itemName);
                            Main.player[whoAmI].inventory[num12].stack = num13;
                        }
                        else
                        {
                            Main.player[whoAmI].armor[num12 - 0x2c] = new Item();
                            Main.player[whoAmI].armor[num12 - 0x2c].SetDefaults(itemName);
                            Main.player[whoAmI].armor[num12 - 0x2c].stack = num13;
                        }
                        if ((Main.netMode == 2) && (whoAmI == this.whoAmI))
                        {
                            NetMessage.SendData(5, -1, this.whoAmI, itemName, whoAmI, (float) num12, 0f, 0f, 0);
                        }
                        return;
                    }
                    break;
                }
                case 6:
                    if (Main.netMode == 2)
                    {
                        if (Netplay.serverSock[this.whoAmI].state == 1)
                        {
                            Netplay.serverSock[this.whoAmI].state = 2;
                        }
                        NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
                        return;
                    }
                    return;

                case 7:
                    if (Main.netMode == 1)
                    {
                        Main.time = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.dayTime = false;
                        if (this.readBuffer[startIndex] == 1)
                        {
                            Main.dayTime = true;
                        }
                        startIndex++;
                        Main.moonPhase = this.readBuffer[startIndex];
                        startIndex++;
                        int num14 = this.readBuffer[startIndex];
                        startIndex++;
                        if (num14 == 1)
                        {
                            Main.bloodMoon = true;
                        }
                        else
                        {
                            Main.bloodMoon = false;
                        }
                        Main.maxTilesX = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.maxTilesY = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.spawnTileX = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.spawnTileY = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.worldSurface = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.rockLayer = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.worldID = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        byte num15 = this.readBuffer[startIndex];
                        if ((num15 & 1) == 1)
                        {
                            WorldGen.shadowOrbSmashed = true;
                        }
                        if ((num15 & 2) == 2)
                        {
                            NPC.downedBoss1 = true;
                        }
                        if ((num15 & 4) == 4)
                        {
                            NPC.downedBoss2 = true;
                        }
                        if ((num15 & 8) == 8)
                        {
                            NPC.downedBoss3 = true;
                        }
                        startIndex++;
                        Main.worldName = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                        if (Netplay.clientSock.state == 3)
                        {
                            Netplay.clientSock.state = 4;
                            return;
                        }
                    }
                    return;

                case 4:
                {
                    bool flag = false;
                    int num7 = this.readBuffer[start + 1];
                    if (Main.netMode == 2)
                    {
                        num7 = this.whoAmI;
                    }
                    if (num7 != Main.myPlayer)
                    {
                        int num8 = this.readBuffer[start + 2];
                        if (num8 >= 0x11)
                        {
                            num8 = 0;
                        }
                        Main.player[num7].hair = num8;
                        Main.player[num7].whoAmi = num7;
                        startIndex += 2;
                        Main.player[num7].hairColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].hairColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].hairColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].skinColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].skinColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].skinColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].eyeColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].eyeColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].eyeColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shirtColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shirtColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shirtColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].underShirtColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].underShirtColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].underShirtColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].pantsColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].pantsColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].pantsColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shoeColor.R = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shoeColor.G = this.readBuffer[startIndex];
                        startIndex++;
                        Main.player[num7].shoeColor.B = this.readBuffer[startIndex];
                        startIndex++;
                        if (this.readBuffer[startIndex] == 0)
                        {
                            Main.player[num7].hardCore = false;
                        }
                        else
                        {
                            Main.player[num7].hardCore = true;
                        }
                        startIndex++;
                        string text = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start).Trim();
                        Main.player[num7].name = text.Trim();
                        if (Main.netMode == 2)
                        {
                            if (Netplay.serverSock[this.whoAmI].state < 10)
                            {
                                for (int m = 0; m < 0xff; m++)
                                {
                                    if (((m != num7) && (text == Main.player[m].name)) && Netplay.serverSock[m].active)
                                    {
                                        flag = true;
                                    }
                                }
                            }
                            if (flag)
                            {
                                NetMessage.SendData(2, this.whoAmI, -1, text + " is already on this server.", 0, 0f, 0f, 0f, 0);
                                return;
                            }
                            if (text.Length > Player.nameLen)
                            {
                                NetMessage.SendData(2, this.whoAmI, -1, "Name is too long.", 0, 0f, 0f, 0f, 0);
                                return;
                            }
                            if (text == "")
                            {
                                NetMessage.SendData(2, this.whoAmI, -1, "Empty name.", 0, 0f, 0f, 0f, 0);
                                return;
                            }
                            Netplay.serverSock[this.whoAmI].oldName = text;
                            Netplay.serverSock[this.whoAmI].name = text;
                            NetMessage.SendData(4, -1, this.whoAmI, text, num7, 0f, 0f, 0f, 0);
                            return;
                        }
                    }
                    return;
                }
                case 8:
                    if (Main.netMode == 2)
                    {
                        int x = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        int y = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        bool flag3 = true;
                        if ((x == -1) || (y == -1))
                        {
                            flag3 = false;
                        }
                        else if ((x < 10) || (x > (Main.maxTilesX - 10)))
                        {
                            flag3 = false;
                        }
                        else if ((y < 10) || (y > (Main.maxTilesY - 10)))
                        {
                            flag3 = false;
                        }
                        int number = 0x546;
                        if (flag3)
                        {
                            number *= 2;
                        }
                        if (Netplay.serverSock[this.whoAmI].state == 2)
                        {
                            Netplay.serverSock[this.whoAmI].state = 3;
                        }
                        NetMessage.SendData(9, this.whoAmI, -1, "Receiving tile data", number, 0f, 0f, 0f, 0);
                        Netplay.serverSock[this.whoAmI].statusText2 = "is receiving tile data";
                        ServerSock sock1 = Netplay.serverSock[this.whoAmI];
                        sock1.statusMax += number;
                        int sectionX = Netplay.GetSectionX(Main.spawnTileX);
                        int sectionY = Netplay.GetSectionY(Main.spawnTileY);
                        for (int n = sectionX - 2; n < (sectionX + 3); n++)
                        {
                            for (int num22 = sectionY - 1; num22 < (sectionY + 2); num22++)
                            {
                                NetMessage.SendSection(this.whoAmI, n, num22);
                            }
                        }
                        if (flag3)
                        {
                            x = Netplay.GetSectionX(x);
                            y = Netplay.GetSectionY(y);
                            for (int num23 = x - 2; num23 < (x + 3); num23++)
                            {
                                for (int num24 = y - 1; num24 < (y + 2); num24++)
                                {
                                    NetMessage.SendSection(this.whoAmI, num23, num24);
                                }
                            }
                            NetMessage.SendData(11, this.whoAmI, -1, "", x - 2, (float) (y - 1), (float) (x + 2), (float) (y + 1), 0);
                        }
                        NetMessage.SendData(11, this.whoAmI, -1, "", sectionX - 2, (float) (sectionY - 1), (float) (sectionX + 2), (float) (sectionY + 1), 0);
                        for (int num25 = 0; num25 < 200; num25++)
                        {
                            if (Main.item[num25].active)
                            {
                                NetMessage.SendData(0x15, this.whoAmI, -1, "", num25, 0f, 0f, 0f, 0);
                                NetMessage.SendData(0x16, this.whoAmI, -1, "", num25, 0f, 0f, 0f, 0);
                            }
                        }
                        for (int num26 = 0; num26 < 0x3e8; num26++)
                        {
                            if (Main.npc[num26].active)
                            {
                                NetMessage.SendData(0x17, this.whoAmI, -1, "", num26, 0f, 0f, 0f, 0);
                            }
                        }
                        NetMessage.SendData(0x31, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
                        return;
                    }
                    return;

                case 9:
                    if (Main.netMode == 1)
                    {
                        int num27 = BitConverter.ToInt32(this.readBuffer, start + 1);
                        string str4 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
                        Netplay.clientSock.statusMax += num27;
                        Netplay.clientSock.statusText = str4;
                        return;
                    }
                    return;
            }
            if ((msgType == 10) && (Main.netMode == 1))
            {
                short num28 = BitConverter.ToInt16(this.readBuffer, start + 1);
                int num29 = BitConverter.ToInt32(this.readBuffer, start + 3);
                int num30 = BitConverter.ToInt32(this.readBuffer, start + 7);
                startIndex = start + 11;
                for (int num32 = num29; num32 < (num29 + num28); num32++)
                {
                    if (Main.tile[num32, num30] == null)
                    {
                        Main.tile[num32, num30] = new Tile();
                    }
                    byte num31 = this.readBuffer[startIndex];
                    startIndex++;
                    bool active = Main.tile[num32, num30].active;
                    if ((num31 & 1) == 1)
                    {
                        Main.tile[num32, num30].active = true;
                    }
                    else
                    {
                        Main.tile[num32, num30].active = false;
                    }
                    if ((num31 & 2) == 2)
                    {
                        Main.tile[num32, num30].lighted = true;
                    }
                    if ((num31 & 4) == 4)
                    {
                        Main.tile[num32, num30].wall = 1;
                    }
                    else
                    {
                        Main.tile[num32, num30].wall = 0;
                    }
                    if ((num31 & 8) == 8)
                    {
                        Main.tile[num32, num30].liquid = 1;
                    }
                    else
                    {
                        Main.tile[num32, num30].liquid = 0;
                    }

                    //Mod: portal functionality
                    if ((num31 & 16) == 16)
                    {
                        Main.tile[num32, num30].portal = true;
                    }
                    else
                    {
                        Main.tile[num32, num30].portal = false;
                    }

                    if (Main.tile[num32, num30].active)
                    {
                        int type = Main.tile[num32, num30].type;
                        Main.tile[num32, num30].type = this.readBuffer[startIndex];
                        startIndex++;
                        if (Main.tileFrameImportant[Main.tile[num32, num30].type])
                        {
                            Main.tile[num32, num30].frameX = BitConverter.ToInt16(this.readBuffer, startIndex);
                            startIndex += 2;
                            Main.tile[num32, num30].frameY = BitConverter.ToInt16(this.readBuffer, startIndex);
                            startIndex += 2;
                        }
                        else if (!active || (Main.tile[num32, num30].type != type))
                        {
                            Main.tile[num32, num30].frameX = -1;
                            Main.tile[num32, num30].frameY = -1;
                        }
                    }
                    if (Main.tile[num32, num30].wall > 0)
                    {
                        Main.tile[num32, num30].wall = this.readBuffer[startIndex];
                        startIndex++;
                    }
                    if (Main.tile[num32, num30].liquid > 0)
                    {
                        Main.tile[num32, num30].liquid = this.readBuffer[startIndex];
                        startIndex++;
                        byte num34 = this.readBuffer[startIndex];
                        startIndex++;
                        if (num34 == 1)
                        {
                            Main.tile[num32, num30].lava = true;
                        }
                        else
                        {
                            Main.tile[num32, num30].lava = false;
                        }
                    }

                    //Mod: portal functionality
                    if (Main.tile[num32, num30].portal)
                    {
                        Main.tile[num32, num30].portalPartner.X = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        Main.tile[num32, num30].portalPartner.Y = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                    }
                }
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(msgType, -1, this.whoAmI, "", num28, (float) num29, (float) num30, 0f, 0);
                    return;
                }
                return;
            }
            if (msgType == 11)
            {
                if (Main.netMode == 1)
                {
                    int startX = BitConverter.ToInt16(this.readBuffer, startIndex);
                    startIndex += 4;
                    int startY = BitConverter.ToInt16(this.readBuffer, startIndex);
                    startIndex += 4;
                    int endX = BitConverter.ToInt16(this.readBuffer, startIndex);
                    startIndex += 4;
                    int endY = BitConverter.ToInt16(this.readBuffer, startIndex);
                    startIndex += 4;
                    WorldGen.SectionTileFrame(startX, startY, endX, endY);
                    return;
                }
                return;
            }
            if (msgType == 12)
            {
                int num39 = this.readBuffer[startIndex];
                if (Main.netMode == 2)
                {
                    num39 = this.whoAmI;
                }
                startIndex++;
                Main.player[num39].SpawnX = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                Main.player[num39].SpawnY = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                Main.player[num39].Spawn();
                if ((Main.netMode == 2) && (Netplay.serverSock[this.whoAmI].state >= 3))
                {
                    if (Netplay.serverSock[this.whoAmI].state == 3)
                    {
                        Netplay.serverSock[this.whoAmI].state = 10;
                        NetMessage.greetPlayer(this.whoAmI);
                        NetMessage.syncPlayers();
                        NetMessage.buffer[this.whoAmI].broadcast = true;
                        NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
                        return;
                    }
                    NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
                    return;
                }
                return;
            }
            if (msgType == 13)
            {
                int num40 = this.readBuffer[startIndex];
                if (num40 != Main.myPlayer)
                {
                    if ((Main.netMode == 1) && !Main.player[num40].active)
                    {
                        NetMessage.SendData(15, -1, -1, "", 0, 0f, 0f, 0f, 0);
                    }
                    if (Main.netMode == 2)
                    {
                        num40 = this.whoAmI;
                    }
                    startIndex++;
                    int num41 = this.readBuffer[startIndex];
                    startIndex++;
                    int num42 = this.readBuffer[startIndex];
                    startIndex++;
                    float num43 = BitConverter.ToSingle(this.readBuffer, startIndex);
                    startIndex += 4;
                    float num44 = BitConverter.ToSingle(this.readBuffer, startIndex);
                    startIndex += 4;
                    float num45 = BitConverter.ToSingle(this.readBuffer, startIndex);
                    startIndex += 4;
                    float num46 = BitConverter.ToSingle(this.readBuffer, startIndex);
                    startIndex += 4;
                    Main.player[num40].selectedItem = num42;
                    Main.player[num40].position.X = num43;
                    Main.player[num40].position.Y = num44;
                    Main.player[num40].velocity.X = num45;
                    Main.player[num40].velocity.Y = num46;
                    Main.player[num40].oldVelocity = Main.player[num40].velocity;
                    Main.player[num40].fallStart = (int) (num44 / 16f);
                    Main.player[num40].controlUp = false;
                    Main.player[num40].controlDown = false;
                    Main.player[num40].controlLeft = false;
                    Main.player[num40].controlRight = false;
                    Main.player[num40].controlJump = false;
                    Main.player[num40].controlUseItem = false;
                    Main.player[num40].direction = -1;
                    if ((num41 & 1) == 1)
                    {
                        Main.player[num40].controlUp = true;
                    }
                    if ((num41 & 2) == 2)
                    {
                        Main.player[num40].controlDown = true;
                    }
                    if ((num41 & 4) == 4)
                    {
                        Main.player[num40].controlLeft = true;
                    }
                    if ((num41 & 8) == 8)
                    {
                        Main.player[num40].controlRight = true;
                    }
                    if ((num41 & 0x10) == 0x10)
                    {
                        Main.player[num40].controlJump = true;
                    }
                    if ((num41 & 0x20) == 0x20)
                    {
                        Main.player[num40].controlUseItem = true;
                    }
                    if ((num41 & 0x40) == 0x40)
                    {
                        Main.player[num40].direction = 1;
                    }
                    if ((Main.netMode == 2) && (Netplay.serverSock[this.whoAmI].state == 10))
                    {
                        NetMessage.SendData(13, -1, this.whoAmI, "", num40, 0f, 0f, 0f, 0);
                        return;
                    }
                }
                return;
            }
            if (msgType == 14)
            {
                if (Main.netMode == 1)
                {
                    int num47 = this.readBuffer[startIndex];
                    startIndex++;
                    int num48 = this.readBuffer[startIndex];
                    if (num48 == 1)
                    {
                        if (!Main.player[num47].active)
                        {
                            Main.player[num47] = new Player();
                        }
                        Main.player[num47].active = true;
                        return;
                    }
                    Main.player[num47].active = false;
                    return;
                }
                return;
            }
            if (msgType == 15)
            {
                if (Main.netMode == 2)
                {
                    NetMessage.syncPlayers();
                    return;
                }
                return;
            }
            if (msgType == 0x10)
            {
                int num49 = this.readBuffer[startIndex];
                startIndex++;
                if (num49 != Main.myPlayer)
                {
                    int num50 = BitConverter.ToInt16(this.readBuffer, startIndex);
                    startIndex += 2;
                    int num51 = BitConverter.ToInt16(this.readBuffer, startIndex);
                    if (Main.netMode == 2)
                    {
                        num49 = this.whoAmI;
                    }
                    Main.player[num49].statLife = num50;
                    Main.player[num49].statLifeMax = num51;
                    if (Main.player[num49].statLife <= 0)
                    {
                        Main.player[num49].dead = true;
                    }
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x10, -1, this.whoAmI, "", num49, 0f, 0f, 0f, 0);
                        return;
                    }
                }
                return;
            }
            if (msgType != 0x11)
            {
                if (msgType == 0x12)
                {
                    if (Main.netMode == 1)
                    {
                        byte num57 = this.readBuffer[startIndex];
                        startIndex++;
                        int num58 = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        short num59 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        short num60 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        if (num57 == 1)
                        {
                            Main.dayTime = true;
                        }
                        else
                        {
                            Main.dayTime = false;
                        }
                        Main.time = num58;
                        Main.sunModY = num59;
                        Main.moonModY = num60;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x12, -1, this.whoAmI, "", 0, 0f, 0f, 0f, 0);
                            return;
                        }
                    }
                    return;
                }
                if (msgType != 0x13)
                {
                    if (msgType == 20)
                    {
                        short num66 = BitConverter.ToInt16(this.readBuffer, start + 1);
                        int num67 = BitConverter.ToInt32(this.readBuffer, start + 3);
                        int num68 = BitConverter.ToInt32(this.readBuffer, start + 7);
                        startIndex = start + 11;
                        for (int num70 = num67; num70 < (num67 + num66); num70++)
                        {
                            for (int num71 = num68; num71 < (num68 + num66); num71++)
                            {
                                if (Main.tile[num70, num71] == null)
                                {
                                    Main.tile[num70, num71] = new Tile();
                                }
                                byte num69 = this.readBuffer[startIndex];
                                startIndex++;
                                bool flag6 = Main.tile[num70, num71].active;
                                if ((num69 & 1) == 1)
                                {
                                    Main.tile[num70, num71].active = true;
                                }
                                else
                                {
                                    Main.tile[num70, num71].active = false;
                                }
                                if ((num69 & 2) == 2)
                                {
                                    Main.tile[num70, num71].lighted = true;
                                }
                                if ((num69 & 4) == 4)
                                {
                                    Main.tile[num70, num71].wall = 1;
                                }
                                else
                                {
                                    Main.tile[num70, num71].wall = 0;
                                }
                                if ((num69 & 8) == 8)
                                {
                                    Main.tile[num70, num71].liquid = 1;
                                }
                                else
                                {
                                    Main.tile[num70, num71].liquid = 0;
                                }

                                //Mod: portal functionality
                                if ((num69 & 16) == 16)
                                {
                                    Main.tile[num70, num71].portal = true;
                                }
                                else
                                {
                                    Main.tile[num70, num71].portal = false;
                                }

                                if (Main.tile[num70, num71].active)
                                {
                                    int num72 = Main.tile[num70, num71].type;
                                    Main.tile[num70, num71].type = this.readBuffer[startIndex];
                                    startIndex++;
                                    if (Main.tileFrameImportant[Main.tile[num70, num71].type])
                                    {
                                        Main.tile[num70, num71].frameX = BitConverter.ToInt16(this.readBuffer, startIndex);
                                        startIndex += 2;
                                        Main.tile[num70, num71].frameY = BitConverter.ToInt16(this.readBuffer, startIndex);
                                        startIndex += 2;
                                    }
                                    else if (!flag6 || (Main.tile[num70, num71].type != num72))
                                    {
                                        Main.tile[num70, num71].frameX = -1;
                                        Main.tile[num70, num71].frameY = -1;
                                    }
                                }
                                if (Main.tile[num70, num71].wall > 0)
                                {
                                    Main.tile[num70, num71].wall = this.readBuffer[startIndex];
                                    startIndex++;
                                }
                                if (Main.tile[num70, num71].liquid > 0)
                                {
                                    Main.tile[num70, num71].liquid = this.readBuffer[startIndex];
                                    startIndex++;
                                    byte num73 = this.readBuffer[startIndex];
                                    startIndex++;
                                    if (num73 == 1)
                                    {
                                        Main.tile[num70, num71].lava = true;
                                    }
                                    else
                                    {
                                        Main.tile[num70, num71].lava = false;
                                    }
                                }

                                //Mod: portal functionality
                                if (Main.tile[num70, num71].portal)
                                {
                                    Main.tile[num70, num71].portalPartner.X = BitConverter.ToInt32(this.readBuffer, startIndex);
                                    startIndex += 4;
                                    Main.tile[num70, num71].portalPartner.Y = BitConverter.ToInt32(this.readBuffer, startIndex);
                                    startIndex += 4;
                                }
                            }
                        }
                        WorldGen.RangeFrame(num67, num68, num67 + num66, num68 + num66);
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(msgType, -1, this.whoAmI, "", num66, (float) num67, (float) num68, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x15)
                    {
                        short num74 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        float num75 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num76 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num77 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num78 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        byte stack = this.readBuffer[startIndex];
                        startIndex++;
                        string str5 = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                        if (Main.netMode == 1)
                        {
                            if (str5 == "0")
                            {
                                Main.item[num74].active = false;
                                return;
                            }
                            Main.item[num74].SetDefaults(str5);
                            Main.item[num74].stack = stack;
                            Main.item[num74].position.X = num75;
                            Main.item[num74].position.Y = num76;
                            Main.item[num74].velocity.X = num77;
                            Main.item[num74].velocity.Y = num78;
                            Main.item[num74].active = true;
                            Main.item[num74].wet = Collision.WetCollision(Main.item[num74].position, Main.item[num74].width, Main.item[num74].height);
                            return;
                        }
                        if (str5 != "0")
                        {
                            bool flag7 = false;
                            if (num74 == 200)
                            {
                                flag7 = true;
                            }
                            if (flag7)
                            {
                                Item item = new Item();
                                item.SetDefaults(str5);
                                num74 = (short) Item.NewItem((int) num75, (int) num76, item.width, item.height, item.type, stack, true);
                            }
                            Main.item[num74].SetDefaults(str5);
                            Main.item[num74].stack = stack;
                            Main.item[num74].position.X = num75;
                            Main.item[num74].position.Y = num76;
                            Main.item[num74].velocity.X = num77;
                            Main.item[num74].velocity.Y = num78;
                            Main.item[num74].active = true;
                            Main.item[num74].owner = Main.myPlayer;
                            if (flag7)
                            {
                                NetMessage.SendData(0x15, -1, -1, "", num74, 0f, 0f, 0f, 0);
                                Main.item[num74].ownIgnore = this.whoAmI;
                                Main.item[num74].ownTime = 100;
                                Main.item[num74].FindOwner(num74);
                                return;
                            }
                            NetMessage.SendData(0x15, -1, this.whoAmI, "", num74, 0f, 0f, 0f, 0);
                            return;
                        }
                        if (num74 < 200)
                        {
                            Main.item[num74].active = false;
                            NetMessage.SendData(0x15, -1, -1, "", num74, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x16)
                    {
                        short num80 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        byte num81 = this.readBuffer[startIndex];
                        if ((Main.netMode != 2) || (Main.item[num80].owner == this.whoAmI))
                        {
                            Main.item[num80].owner = num81;
                            if (num81 == Main.myPlayer)
                            {
                                Main.item[num80].keepTime = 15;
                            }
                            else
                            {
                                Main.item[num80].keepTime = 0;
                            }
                            if (Main.netMode == 2)
                            {
                                Main.item[num80].owner = 0xff;
                                Main.item[num80].keepTime = 15;
                                NetMessage.SendData(0x16, -1, -1, "", num80, 0f, 0f, 0f, 0);
                                return;
                            }
                        }
                        return;
                    }
                    if ((msgType == 0x17) && (Main.netMode == 1))
                    {
                        short num82 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        float num83 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num84 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num85 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num86 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        int num87 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        int num88 = this.readBuffer[startIndex] - 1;
                        startIndex++;
                        byte num186 = this.readBuffer[startIndex];
                        startIndex++;
                        int num89 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        float[] numArray = new float[NPC.maxAI];
                        for (int num90 = 0; num90 < NPC.maxAI; num90++)
                        {
                            numArray[num90] = BitConverter.ToSingle(this.readBuffer, startIndex);
                            startIndex += 4;
                        }
                        string name = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                        //Main.NewText(name + " is here!!!!", 0xaf, 0x4b, 0xff);
                        if (!Main.npc[num82].active || (Main.npc[num82].name != name))
                        {
                            Main.npc[num82].active = true;
                            Main.npc[num82].SetDefaults(name);
                        }
                        Main.npc[num82].position.X = num83;
                        Main.npc[num82].position.Y = num84;
                        Main.npc[num82].velocity.X = num85;
                        Main.npc[num82].velocity.Y = num86;
                        Main.npc[num82].target = num87;
                        Main.npc[num82].direction = num88;
                        Main.npc[num82].life = num89;
                        if (num89 <= 0)
                        {
                            Main.npc[num82].active = false;
                        }
                        for (int num91 = 0; num91 < NPC.maxAI; num91++)
                        {
                            Main.npc[num82].ai[num91] = numArray[num91];
                        }
                        return;
                    }
                    if (msgType == 0x18)
                    {
                        short num92 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        byte num93 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num93 = (byte) this.whoAmI;
                        }
                        Main.npc[num92].StrikeNPC(Main.player[num93].inventory[Main.player[num93].selectedItem].damage, Main.player[num93].inventory[Main.player[num93].selectedItem].knockBack, Main.player[num93].direction);
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x18, -1, this.whoAmI, "", num92, (float) num93, 0f, 0f, 0);
                            NetMessage.SendData(0x17, -1, -1, "", num92, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x19)
                    {
                        int num94 = this.readBuffer[start + 1];
                        if (Main.netMode == 2)
                        {
                            num94 = this.whoAmI;
                        }
                        byte r = this.readBuffer[start + 2];
                        byte g = this.readBuffer[start + 3];
                        byte b = this.readBuffer[start + 4];
                        if (Main.netMode == 2)
                        {
                            r = 0xff;
                            g = 0xff;
                            b = 0xff;
                        }
                        string str7 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);

                        if (Main.netMode == 1)
                        {
                            string newText = str7;
                            if (num94 < 0xff)
                            {
                                newText = "<" + Main.player[num94].name + "> " + str7;
                                Main.player[num94].chatText = str7;
                                Main.player[num94].chatShowTime = Main.chatLength / 2;
                            }
                            Main.NewText(newText, r, g, b);
                            return;
                        }
                        if (Main.netMode == 2)
                        {
                            string str9 = str7.ToLower();
                            if (str9 == "/playing")
                            {
                                string str10 = "";
                                for (int num98 = 0; num98 < 0xff; num98++)
                                {
                                    if (Main.player[num98].active)
                                    {
                                        if (str10 == "")
                                        {
                                            str10 = str10 + Main.player[num98].name;
                                        }
                                        else
                                        {
                                            str10 = str10 + ", " + Main.player[num98].name;
                                        }
                                    }
                                }
                                NetMessage.SendData(0x19, this.whoAmI, -1, "Current players: " + str10 + ".", 0xff, 255f, 240f, 20f, 0);
                                return;
                            }
                            if ((str9.Length >= 4) && (str9.Substring(0, 4) == "/me "))
                            {
                                NetMessage.SendData(0x19, -1, -1, "*" + Main.player[this.whoAmI].name + " " + str7.Substring(4), 0xff, 200f, 100f, 0f, 0);
                                return;
                            }
                            if ((str9.Length >= 3) && (str9.Substring(0, 3) == "/p "))
                            {
                                if (Main.player[this.whoAmI].team != 0)
                                {
                                    for (int num99 = 0; num99 < 0xff; num99++)
                                    {
                                        if (Main.player[num99].team == Main.player[this.whoAmI].team)
                                        {
                                            NetMessage.SendData(0x19, num99, -1, str7.Substring(3), num94, (float) Main.teamColor[Main.player[this.whoAmI].team].R, (float) Main.teamColor[Main.player[this.whoAmI].team].G, (float) Main.teamColor[Main.player[this.whoAmI].team].B, 0);
                                        }
                                    }
                                    return;
                                }
                                NetMessage.SendData(0x19, this.whoAmI, -1, "You are not in a party!", 0xff, 255f, 240f, 20f, 0);
                                return;
                            }
                            if (Main.player[this.whoAmI].hardCore)
                            {
                                r = Main.hcColor.R;
                                g = Main.hcColor.G;
                                b = Main.hcColor.B;
                            }

                            string sendString = str7;

                            //Mod
                            if (str7.Length >= 5)
                            {
                                if ((str7.Substring(0, 4) == "/pw:"))
                                {
                                    for (int i = 4; i < str7.Length; i++)
                                    {
                                        sendString = sendString.Remove(i, 1);
                                        sendString = sendString.Insert(i, "*");
                                    }
                                }
                            }
                            if (str7.Length >= 7) //Mod
                            {
                                if ((str7.Substring(0, 7) == "/setpw:"))
                                {
                                    for (int i = 7; i < str7.Length; i++)
                                    {
                                        sendString = sendString.Remove(i, 1);
                                        sendString = sendString.Insert(i, "*");
                                    }
                                }
                            }
                            NetMessage.SendData(0x19, -1, -1, sendString, num94, (float)r, (float)g, (float)b, 0);
                            
                            
                            if (Main.dedServ)
                            {
                                Console.WriteLine("<" + Main.player[this.whoAmI].name + "> " + str7);

                                //Mod do our commands
                                if (str7[0] == '/')
                                {
                                    if (str7 == "/pw:" + Main.adminPassword)
                                    {
                                        Console.WriteLine(Main.player[this.whoAmI].name + " is now an admin!");
                                        Main.player[this.whoAmI].admin = true;
                                    }

                                    if (Main.player[this.whoAmI].admin)
                                    {
                                        if (str7.Length >= 7)
                                        {
                                            if ((str7.Substring(0, 7) == "/setpw:"))
                                            {
                                                Console.WriteLine(Main.player[this.whoAmI].name + " changed the password to " + str7.Substring(7));
                                                Main.adminPassword = str7.Substring(7);
                                            }
                                        }
                                        if ((str7.Length >= 6) && (str7.Substring(0, 6) == "/kick "))
                                        {
                                            string kickName = str7.Substring(6).ToLower();
                                            for (int j = 0; j < 0xff; j++)
                                            {
                                                if (Main.player[j].active && (Main.player[j].name.ToLower() == kickName))
                                                {
                                                    NetMessage.SendData(2, j, -1, "Kicked from server.", 0, 0f, 0f, 0f, 0);
                                                }
                                            }
                                        }
                                        if ((str7.Length >= 5) && (str7.Substring(0, 5) == "/ban "))
                                        {
                                            string banName = str7.Substring(5).ToLower();

                                            for (int k = 0; k < 0xff; k++)
                                            {
                                                if (Main.player[k].active && (Main.player[k].name.ToLower() == banName))
                                                {
                                                    Netplay.AddBan(k);
                                                    NetMessage.SendData(2, k, -1, "Banned from server.", 0, 0f, 0f, 0f, 0);
                                                }
                                           }
                                        }

                                        switch (str7)
                                        {
                                            case "/indestructable_tiles_on":
                                                {
                                                    WorldGen.noKillStrongTiles = true;
                                                    break;
                                                }
                                            case "/indestructable_tiles_off":
                                                {
                                                    WorldGen.noKillStrongTiles = false;
                                                    break;
                                                }
                                            case "/min_indestructable_tile":
                                                {
                                                    WorldGen.minStrongTilesX = (Int32)Main.player[this.whoAmI].position.X / 16;
                                                    WorldGen.minStrongTilesY = (Int32)Main.player[this.whoAmI].position.Y / 16;
                                                    break;
                                                }
                                            case "/max_indestructable_tile":
                                                {
                                                    WorldGen.maxStrongTilesX = (Int32)Main.player[this.whoAmI].position.X / 16;
                                                    WorldGen.maxStrongTilesY = (Int32)Main.player[this.whoAmI].position.Y / 16;
                                                    break;
                                                }
                                            case "/goblin_invasion":
                                                {
                                                    Console.WriteLine(Main.player[this.whoAmI].name + " started a goblin invasion!");
                                                    Main.StartInvasion();
                                                    break;
                                                }

                                            case "/spawn_demon_eye":
                                                {
                                                    Console.WriteLine(Main.player[this.whoAmI].name + " summoned Eye Of Cathulu!");

                                                    Vector2 position2;

                                                    //spawn demon Eye
                                                    position2.X = ((Main.spawnTileX * 0x10) + 8);
                                                    position2.Y = (Main.spawnTileY * 0x10) - 10;

                                                    NPC.NewNPC(((int)position2.X), ((int)position2.Y), 4, 0);

                                                    break;
                                                }

                                            case "/spawn_EOW":
                                                {
                                                    Console.WriteLine(Main.player[this.whoAmI].name + " summoned Eater of Worlds!");

                                                    NPC.SpawnOnPlayer(this.whoAmI, 13);
                                                    break;
                                                }

                                            case "/spawn_skeletron":
                                                {
                                                    Console.WriteLine(Main.player[this.whoAmI].name + " summoned Skeletron!");

                                                    NPC.SpawnSkeletron();

                                                    break;
                                                }

                                            case "/ultimate_invasion":
                                                {
                                                    Console.WriteLine(Main.player[this.whoAmI].name + " started an ultimate invasion!");

                                                    Main.StartInvasion();
                                                    Main.invasionX = Main.spawnTileX + 1; //right at our doorstep

                                                    NPC.SpawnSkeletron();

                                                    Vector2 position2;

                                                    //spawn demon Eye
                                                    position2.X = ((Main.spawnTileX * 0x10) + 8);
                                                    position2.Y = (Main.spawnTileY * 0x10) - 10;

                                                    for (int s = 0; s < 3; s++)
                                                        NPC.NewNPC(((int)position2.X) + ((s - 1) * 700), ((int)position2.Y), 4, 0);

                                                    //eater of worlds
                                                    for (int s = 0; s < 2; s++)
                                                        NPC.NewNPC((int)position2.X, (int)position2.Y, 13, 1);

                                                    Main.bloodMoon = true;

                                                    break;
                                                }
                                            case "/settle":
                                                {
                                                    if (!Liquid.panicMode)
                                                    {
                                                        Liquid.StartPanic();
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Water is already settling");
                                                    }
                                                    break;
                                                }
                                            case "/dawn":
                                                {
                                                    Main.dayTime = true;
                                                    Main.time = 0.0;
                                                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                                                    break;
                                                }
                                            case "/dusk":
                                                {
                                                    Main.dayTime = false;
                                                    Main.time = 0.0;
                                                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                                                    break;
                                                }
                                            case "/noon":
                                                {
                                                    Main.dayTime = true;
                                                    Main.time = 27000.0;
                                                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                                                    break;
                                                }
                                            case "/midnight":
                                                {
                                                    Main.dayTime = false;
                                                    Main.time = 16200.0;
                                                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                                                    break;
                                                }
                                            case "/exit-nosave":
                                                {
                                                    Netplay.disconnect = true;
                                                    break;
                                                }
                                            case "/exit":
                                                {
                                                    WorldGen.saveWorld(false);
                                                    Netplay.disconnect = true;
                                                    break;
                                                }
                                            case "/save":
                                                {
                                                    WorldGen.saveWorld(false);
                                                    break;
                                                }
                                        }
                                    }
                                }
                                return;
                            }
                        }
                        return;
                    }
                    if (msgType == 0x1a)
                    {
                        byte num100 = this.readBuffer[startIndex];
                        if (((Main.netMode != 2) || (this.whoAmI == num100)) || (Main.player[num100].hostile && Main.player[this.whoAmI].hostile))
                        {
                            startIndex++;
                            int hitDirection = this.readBuffer[startIndex] - 1;
                            startIndex++;
                            short damage = BitConverter.ToInt16(this.readBuffer, startIndex);
                            startIndex += 2;
                            byte num103 = this.readBuffer[startIndex];
                            startIndex++;
                            bool pvp = false;
                            string deathText = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                            if (num103 != 0)
                            {
                                pvp = true;
                            }
                            Main.player[num100].Hurt(damage, hitDirection, pvp, true, deathText);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x1a, -1, this.whoAmI, deathText, num100, (float) hitDirection, (float) damage, (float) num103, 0);
                                return;
                            }
                        }
                        return;
                    }
                    if (msgType == 0x1b)
                    {
                        short num104 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        float num105 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num106 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num107 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num108 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        float num109 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        short num110 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        byte num111 = this.readBuffer[startIndex];
                        startIndex++;
                        byte num112 = this.readBuffer[startIndex];
                        startIndex++;
                        float[] numArray2 = new float[Projectile.maxAI];
                        for (int num113 = 0; num113 < Projectile.maxAI; num113++)
                        {
                            numArray2[num113] = BitConverter.ToSingle(this.readBuffer, startIndex);
                            startIndex += 4;
                        }
                        int num114 = 0x3e8;
                        for (int num115 = 0; num115 < 0x3e8; num115++)
                        {
                            if (((Main.projectile[num115].owner == num111) && (Main.projectile[num115].identity == num104)) && Main.projectile[num115].active)
                            {
                                num114 = num115;
                                break;
                            }
                        }
                        if (num114 == 0x3e8)
                        {
                            for (int num116 = 0; num116 < 0x3e8; num116++)
                            {
                                if (!Main.projectile[num116].active)
                                {
                                    num114 = num116;
                                    break;
                                }
                            }
                        }
                        if (!Main.projectile[num114].active || (Main.projectile[num114].type != num112))
                        {
                            Main.projectile[num114].SetDefaults(num112);
                            if (Main.netMode == 2)
                            {
                                ServerSock sock4 = Netplay.serverSock[this.whoAmI];
                                sock4.spamProjectile++;
                            }
                        }
                        Main.projectile[num114].identity = num104;
                        Main.projectile[num114].position.X = num105;
                        Main.projectile[num114].position.Y = num106;
                        Main.projectile[num114].velocity.X = num107;
                        Main.projectile[num114].velocity.Y = num108;
                        Main.projectile[num114].damage = num110;
                        Main.projectile[num114].type = num112;
                        Main.projectile[num114].owner = num111;
                        Main.projectile[num114].knockBack = num109;
                        for (int num117 = 0; num117 < Projectile.maxAI; num117++)
                        {
                            Main.projectile[num114].ai[num117] = numArray2[num117];
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x1b, -1, this.whoAmI, "", num114, 0f, 0f, 0f, 0);
                        }
                        return;
                    }
                    if (msgType == 0x1c)
                    {
                        short num118 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        short num119 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        float knockBack = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        int num121 = this.readBuffer[startIndex] - 1;
                        if (num119 >= 0)
                        {
                            Main.npc[num118].StrikeNPC(num119, knockBack, num121);
                        }
                        else
                        {
                            Main.npc[num118].life = 0;
                            Main.npc[num118].HitEffect(0, 10.0);
                            Main.npc[num118].active = false;
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x1c, -1, this.whoAmI, "", num118, (float) num119, knockBack, (float) num121, 0);
                            NetMessage.SendData(0x17, -1, -1, "", num118, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x1d)
                    {
                        short num122 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        byte num123 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num123 = (byte) this.whoAmI;
                        }
                        for (int num124 = 0; num124 < 0x3e8; num124++)
                        {
                            if (((Main.projectile[num124].owner == num123) && (Main.projectile[num124].identity == num122)) && Main.projectile[num124].active)
                            {
                                Main.projectile[num124].Kill();
                                break;
                            }
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x1d, -1, this.whoAmI, "", num122, (float) num123, 0f, 0f, 0);
                        }
                        return;
                    }
                    if (msgType == 30)
                    {
                        byte num125 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num125 = (byte) this.whoAmI;
                        }
                        startIndex++;
                        byte num126 = this.readBuffer[startIndex];
                        if (num126 == 1)
                        {
                            Main.player[num125].hostile = true;
                        }
                        else
                        {
                            Main.player[num125].hostile = false;
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(30, -1, this.whoAmI, "", num125, 0f, 0f, 0f, 0);
                            string str12 = " has enabled PvP!";
                            if (num126 == 0)
                            {
                                str12 = " has disabled PvP!";
                            }
                            NetMessage.SendData(0x19, -1, -1, Main.player[num125].name + str12, 0xff, (float) Main.teamColor[Main.player[num125].team].R, (float) Main.teamColor[Main.player[num125].team].G, (float) Main.teamColor[Main.player[num125].team].B, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x1f)
                    {
                        if (Main.netMode == 2)
                        {
                            int num127 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            startIndex += 4;
                            int num128 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            startIndex += 4;
                            int num129 = Chest.FindChest(num127, num128);
                            if ((num129 > -1) && (Chest.UsingChest(num129) == -1))
                            {
                                for (int num130 = 0; num130 < Chest.maxItems; num130++)
                                {
                                    NetMessage.SendData(0x20, this.whoAmI, -1, "", num129, (float) num130, 0f, 0f, 0);
                                }
                                NetMessage.SendData(0x21, this.whoAmI, -1, "", num129, 0f, 0f, 0f, 0);
                                Main.player[this.whoAmI].chest = num129;
                                return;
                            }
                        }
                        return;
                    }
                    if (msgType == 0x20)
                    {
                        int num131 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        int num132 = this.readBuffer[startIndex];
                        startIndex++;
                        int num133 = this.readBuffer[startIndex];
                        startIndex++;
                        string str13 = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                        if (Main.chest[num131] == null)
                        {
                            Main.chest[num131] = new Chest();
                        }
                        if (Main.chest[num131].item[num132] == null)
                        {
                            Main.chest[num131].item[num132] = new Item();
                        }
                        Main.chest[num131].item[num132].SetDefaults(str13);
                        Main.chest[num131].item[num132].stack = num133;
                        return;
                    }
                    if (msgType == 0x21)
                    {
                        int num134 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        int num135 = BitConverter.ToInt32(this.readBuffer, startIndex);
                        startIndex += 4;
                        int num136 = BitConverter.ToInt32(this.readBuffer, startIndex);
                        if (Main.netMode == 1)
                        {
                            if (Main.player[Main.myPlayer].chest == -1)
                            {
                                Main.playerInventory = true;
                                Main.PlaySound(10, -1, -1, 1);
                            }
                            else if ((Main.player[Main.myPlayer].chest != num134) && (num134 != -1))
                            {
                                Main.playerInventory = true;
                                Main.PlaySound(12, -1, -1, 1);
                            }
                            else if ((Main.player[Main.myPlayer].chest != -1) && (num134 == -1))
                            {
                                Main.PlaySound(11, -1, -1, 1);
                            }
                            Main.player[Main.myPlayer].chest = num134;
                            Main.player[Main.myPlayer].chestX = num135;
                            Main.player[Main.myPlayer].chestY = num136;
                            return;
                        }
                        Main.player[this.whoAmI].chest = num134;
                        return;
                    }
                    if (msgType == 0x22)
                    {
                        if (Main.netMode == 2)
                        {
                            int num137 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            startIndex += 4;
                            int num138 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            if (Main.tile[num137, num138].type == 0x15)
                            {
                                WorldGen.KillTile(num137, num138, false, false, false);
                                if (!Main.tile[num137, num138].active)
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 0, (float) num137, (float) num138, 0f, 0);
                                    return;
                                }
                            }
                        }
                        return;
                    }
                    if (msgType == 0x23)
                    {
                        int num139 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num139 = this.whoAmI;
                        }
                        startIndex++;
                        int healAmount = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        if (num139 != Main.myPlayer)
                        {
                            Main.player[num139].HealEffect(healAmount);
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x23, -1, this.whoAmI, "", num139, (float) healAmount, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x24)
                    {
                        int num141 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num141 = this.whoAmI;
                        }
                        startIndex++;
                        int num142 = this.readBuffer[startIndex];
                        startIndex++;
                        int num143 = this.readBuffer[startIndex];
                        startIndex++;
                        int num144 = this.readBuffer[startIndex];
                        startIndex++;
                        int num145 = this.readBuffer[startIndex];
                        startIndex++;
                        if (num142 == 0)
                        {
                            Main.player[num141].zoneEvil = false;
                        }
                        else
                        {
                            Main.player[num141].zoneEvil = true;
                        }
                        if (num143 == 0)
                        {
                            Main.player[num141].zoneMeteor = false;
                        }
                        else
                        {
                            Main.player[num141].zoneMeteor = true;
                        }
                        if (num144 == 0)
                        {
                            Main.player[num141].zoneDungeon = false;
                        }
                        else
                        {
                            Main.player[num141].zoneDungeon = true;
                        }
                        if (num145 == 0)
                        {
                            Main.player[num141].zoneJungle = false;
                        }
                        else
                        {
                            Main.player[num141].zoneJungle = true;
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x24, -1, this.whoAmI, "", num141, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x25)
                    {
                        if (Main.netMode == 1)
                        {
                            if (Main.autoPass)
                            {
                                NetMessage.SendData(0x26, -1, -1, Netplay.password, 0, 0f, 0f, 0f, 0);
                                Main.autoPass = false;
                                return;
                            }
                            Netplay.password = "";
                            Main.menuMode = 0x1f;
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x26)
                    {
                        if (Main.netMode == 2)
                        {
                            if (Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start) == Netplay.password)
                            {
                                Netplay.serverSock[this.whoAmI].state = 1;
                                NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
                                return;
                            }
                            NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if ((msgType == 0x27) && (Main.netMode == 1))
                    {
                        short num146 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        Main.item[num146].owner = 0xff;
                        NetMessage.SendData(0x16, -1, -1, "", num146, 0f, 0f, 0f, 0);
                        return;
                    }
                    if (msgType == 40)
                    {
                        byte num147 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num147 = (byte) this.whoAmI;
                        }
                        startIndex++;
                        int num148 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        Main.player[num147].talkNPC = num148;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(40, -1, this.whoAmI, "", num147, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x29)
                    {
                        byte num149 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num149 = (byte) this.whoAmI;
                        }
                        startIndex++;
                        float num150 = BitConverter.ToSingle(this.readBuffer, startIndex);
                        startIndex += 4;
                        int num151 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        Main.player[num149].itemRotation = num150;
                        Main.player[num149].itemAnimation = num151;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x29, -1, this.whoAmI, "", num149, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x2a)
                    {
                        int num152 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num152 = this.whoAmI;
                        }
                        startIndex++;
                        int num153 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        int num154 = BitConverter.ToInt16(this.readBuffer, startIndex);
                        if (Main.netMode == 2)
                        {
                            num152 = this.whoAmI;
                        }
                        Main.player[num152].statMana = num153;
                        Main.player[num152].statManaMax = num154;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x2a, -1, this.whoAmI, "", num152, 0f, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x2b)
                    {
                        int num155 = this.readBuffer[startIndex];
                        if (Main.netMode == 2)
                        {
                            num155 = this.whoAmI;
                        }
                        startIndex++;
                        int manaAmount = BitConverter.ToInt16(this.readBuffer, startIndex);
                        startIndex += 2;
                        if (num155 != Main.myPlayer)
                        {
                            Main.player[num155].ManaEffect(manaAmount);
                        }
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendData(0x2b, -1, this.whoAmI, "", num155, (float) manaAmount, 0f, 0f, 0);
                            return;
                        }
                        return;
                    }
                    if (msgType == 0x2c)
                    {
                        byte num157 = this.readBuffer[startIndex];
                        if (num157 != Main.myPlayer)
                        {
                            if (Main.netMode == 2)
                            {
                                num157 = (byte) this.whoAmI;
                            }
                            startIndex++;
                            int num158 = this.readBuffer[startIndex] - 1;
                            startIndex++;
                            short num159 = BitConverter.ToInt16(this.readBuffer, startIndex);
                            startIndex += 2;
                            byte num160 = this.readBuffer[startIndex];
                            startIndex++;
                            string str15 = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                            bool flag9 = false;
                            if (num160 != 0)
                            {
                                flag9 = true;
                            }
                            Main.player[num157].KillMe((double) num159, num158, flag9, str15);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(0x2c, -1, this.whoAmI, str15, num157, (float) num158, (float) num159, (float) num160, 0);
                                return;
                            }
                        }
                        return;
                    }
                    if (msgType != 0x2d)
                    {
                        switch (msgType)
                        {
                            case 0x2e:
                                if (Main.netMode == 2)
                                {
                                    int num165 = BitConverter.ToInt32(this.readBuffer, startIndex);
                                    startIndex += 4;
                                    int num166 = BitConverter.ToInt32(this.readBuffer, startIndex);
                                    startIndex += 4;
                                    int num167 = Sign.ReadSign(num165, num166);
                                    if (num167 >= 0)
                                    {
                                        NetMessage.SendData(0x2f, this.whoAmI, -1, "", num167, 0f, 0f, 0f, 0);
                                        return;
                                    }
                                }
                                return;

                            case 0x2f:
                            {
                                int num168 = BitConverter.ToInt16(this.readBuffer, startIndex);
                                startIndex += 2;
                                int num169 = BitConverter.ToInt32(this.readBuffer, startIndex);
                                startIndex += 4;
                                int num170 = BitConverter.ToInt32(this.readBuffer, startIndex);
                                startIndex += 4;
                                string str17 = Encoding.ASCII.GetString(this.readBuffer, startIndex, (length - startIndex) + start);
                                Main.sign[num168] = new Sign();
                                Main.sign[num168].x = num169;
                                Main.sign[num168].y = num170;
                                Sign.TextSign(num168, str17);
                                if (((Main.netMode == 1) && (Main.sign[num168] != null)) && (num168 != Main.player[Main.myPlayer].sign))
                                {
                                    Main.playerInventory = false;
                                    Main.player[Main.myPlayer].talkNPC = -1;
                                    Main.editSign = false;
                                    Main.PlaySound(10, -1, -1, 1);
                                    Main.player[Main.myPlayer].sign = num168;
                                    Main.npcChatText = Main.sign[num168].text;
                                    return;
                                }
                                return;
                            }
                        }
                        if (msgType == 0x30)
                        {
                            int num171 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            startIndex += 4;
                            int num172 = BitConverter.ToInt32(this.readBuffer, startIndex);
                            startIndex += 4;
                            byte num173 = this.readBuffer[startIndex];
                            startIndex++;
                            byte num174 = this.readBuffer[startIndex];
                            startIndex++;
                            if ((Main.netMode == 2) && Netplay.spamCheck)
                            {
                                int num175 = this.whoAmI;
                                int num176 = ((int) Main.player[num175].position.X) + (Main.player[num175].width / 2);
                                int num177 = ((int) Main.player[num175].position.Y) + (Main.player[num175].height / 2);
                                int num178 = 10;
                                int num179 = num176 - num178;
                                int num180 = num176 + num178;
                                int num181 = num177 - num178;
                                int num182 = num177 + num178;
                                if (((num176 < num179) || (num176 > num180)) || ((num177 < num181) || (num177 > num182)))
                                {
                                    NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Liquid spam");
                                    return;
                                }
                            }
                            if (Main.tile[num171, num172] == null)
                            {
                                Main.tile[num171, num172] = new Tile();
                            }
                            lock (Main.tile[num171, num172])
                            {
                                Main.tile[num171, num172].liquid = num173;
                                if (num174 == 1)
                                {
                                    Main.tile[num171, num172].lava = true;
                                }
                                else
                                {
                                    Main.tile[num171, num172].lava = false;
                                }
                                if (Main.netMode == 2)
                                {
                                    WorldGen.SquareTileFrame(num171, num172, true);
                                }
                                return;
                            }
                        }
                        if (msgType == 0x31)
                        {
                            if (Netplay.clientSock.state == 6)
                            {
                                Netplay.clientSock.state = 10;
                                Main.player[Main.myPlayer].Spawn();
                                return;
                            }
                        }
                        else if (msgType == 50)
                        {
                            int num183 = this.readBuffer[startIndex];
                            startIndex++;
                            if (Main.netMode == 2)
                            {
                                num183 = this.whoAmI;
                            }
                            else if (num183 == Main.myPlayer)
                            {
                                return;
                            }
                            for (int num184 = 0; num184 < 10; num184++)
                            {
                                Main.player[num183].buffType[num184] = this.readBuffer[startIndex];
                                if (Main.player[num183].buffType[num184] > 0)
                                {
                                    Main.player[num183].buffTime[num184] = 60;
                                }
                                else
                                {
                                    Main.player[num183].buffTime[num184] = 0;
                                }
                                startIndex++;
                            }
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(50, -1, this.whoAmI, "", num183, 0f, 0f, 0f, 0);
                                return;
                            }
                        }
                        else if (msgType == 0x33)
                        {
                            byte num185 = this.readBuffer[startIndex];
                            if (num185 == 1)
                            {
                                NPC.SpawnSkeletron();
                            }
                        }
                        return;
                    }
                    num161 = this.readBuffer[startIndex];
                    if (Main.netMode == 2)
                    {
                        num161 = this.whoAmI;
                    }
                    startIndex++;
                    num162 = this.readBuffer[startIndex];
                    startIndex++;
                    team = Main.player[num161].team;
                    Main.player[num161].team = num162;
                    if (Main.netMode != 2)
                    {
                        return;
                    }
                    NetMessage.SendData(0x2d, -1, this.whoAmI, "", num161, 0f, 0f, 0f, 0);
                    str16 = "";
                    switch (num162)
                    {
                        case 0:
                            str16 = " is no longer on a party.";
                            goto Label_3A02;

                        case 1:
                            str16 = " has joined the red party.";
                            goto Label_3A02;

                        case 2:
                            str16 = " has joined the green party.";
                            goto Label_3A02;

                        case 3:
                            str16 = " has joined the blue party.";
                            goto Label_3A02;

                        case 4:
                            str16 = " has joined the yellow party.";
                            goto Label_3A02;
                    }
                    goto Label_3A02;
                }
                num61 = this.readBuffer[startIndex];
                startIndex++;
                num62 = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                num63 = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                num64 = this.readBuffer[startIndex];
                int direction = 0;
                if (num64 == 0)
                {
                    direction = -1;
                }
                switch (num61)
                {
                    case 0:
                        WorldGen.OpenDoor(num62, num63, direction);
                        goto Label_1C9C;

                    case 1:
                        WorldGen.CloseDoor(num62, num63, true);
                        goto Label_1C9C;
                }
            }
            else
            {
                byte num52 = this.readBuffer[startIndex];
                startIndex++;
                int num53 = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                int num54 = BitConverter.ToInt32(this.readBuffer, startIndex);
                startIndex += 4;
                byte num55 = this.readBuffer[startIndex];
                startIndex++;
                int style = this.readBuffer[startIndex];
                bool fail = false;
                if (num55 == 1)
                {
                    fail = true;
                }
                if (Main.tile[num53, num54] == null)
                {
                    Main.tile[num53, num54] = new Tile();
                }
                if (Main.netMode == 2)
                {
                    if (!fail)
                    {
                        if (((num52 == 0) || (num52 == 2)) || (num52 == 4))
                        {
                            ServerSock sock2 = Netplay.serverSock[this.whoAmI];
                            sock2.spamDelBlock++;
                        }
                        else if ((num52 == 1) || (num52 == 3))
                        {
                            ServerSock sock3 = Netplay.serverSock[this.whoAmI];
                            sock3.spamAddBlock++;
                        }
                    }
                    if (!Netplay.serverSock[this.whoAmI].tileSection[Netplay.GetSectionX(num53), Netplay.GetSectionY(num54)])
                    {
                        fail = true;
                    }
                }

                bool noKillTilesFlag = true;

                if (Main.netMode == 2)
                {
                    noKillTilesFlag = WorldGen.noKillStrongTiles; //Mod

                    //Mod for indestructable tiles
                    if (Main.player[this.whoAmI].admin)
                    {
                        WorldGen.noKillStrongTiles = false;
                    }
                }

                switch (num52)
                {
                    case 0:
                        {
                            WorldGen.KillTile(num53, num54, fail, false, false);

                            //Mod for indestructable tiles
                            if (Main.netMode == 2 && num53 > WorldGen.minStrongTilesX && num53 < WorldGen.maxStrongTilesX && num54 > WorldGen.minStrongTilesY && num54 < WorldGen.maxStrongTilesY && WorldGen.noKillStrongTiles) //Mod
                            {
                                NetMessage.SendData(0x11, this.whoAmI, -1, "", 1, (float)num53, (float)num54, (float)Main.tile[num53, num54].type, 0);
                            }

                            break;
                        }

                    case 1:
                        {
                            //Mod: Portal functionality
                            if (this.whoAmI >= 0 && num55 == 254)
                            {
                                if (Main.player[this.whoAmI].lastPortalMadeX >= 0)
                                {
                                    Main.tile[num53, num54].portalPartner.X = Main.player[this.whoAmI].lastPortalMadeX;
                                    Main.tile[num53, num54].portalPartner.Y = Main.player[this.whoAmI].lastPortalMadeY;
                                    Main.tile[Main.player[this.whoAmI].lastPortalMadeX, Main.player[this.whoAmI].lastPortalMadeY].portalPartner.X = num53;
                                    Main.tile[Main.player[this.whoAmI].lastPortalMadeX, Main.player[this.whoAmI].lastPortalMadeY].portalPartner.Y = num54;
                                    Main.player[this.whoAmI].lastPortalMadeX = -1; //for new portals
                                }
                                else
                                {
                                    Main.player[this.whoAmI].lastPortalMadeX = num53;
                                    Main.player[this.whoAmI].lastPortalMadeY = num54;
                                }
                            }

                            WorldGen.PlaceTile(num53, num54, num55, false, true, -1, style);

                            break;
                        }

                    case 2:
                        {
                            WorldGen.KillWall(num53, num54, fail);

                            //Mod for indestructable tiles
                            if (Main.netMode == 2 && num53 > WorldGen.minStrongTilesX && num53 < WorldGen.maxStrongTilesX && num54 > WorldGen.minStrongTilesY && num54 < WorldGen.maxStrongTilesY && WorldGen.noKillStrongTiles) //Mod
                            {
                                NetMessage.SendData(0x11, this.whoAmI, -1, "", 3, (float)num53, (float)num54, (float)Main.tile[num53, num54].type, 0);
                            }
                            break;
                        }

                    case 3:
                        WorldGen.PlaceWall(num53, num54, num55, false);
                        break;

                    case 4:
                        {
                            WorldGen.KillTile(num53, num54, fail, false, true);

                            //Mod for indestructable tiles
                            if (Main.netMode == 2 && num53 > WorldGen.minStrongTilesX && num53 < WorldGen.maxStrongTilesX && num54 > WorldGen.minStrongTilesY && num54 < WorldGen.maxStrongTilesY && WorldGen.noKillStrongTiles) //Mod
                            {
                                NetMessage.SendData(0x11, this.whoAmI, -1, "", 1, (float)num53, (float)num54, (float)Main.tile[num53, num54].type, 0);
                            }

                            break;
                        }
                }
                if (Main.netMode == 2)
                {
                    if (num52 == 0 || num52 == 2 || num52 == 4)
                    {
                        if (!(num53 > WorldGen.minStrongTilesX && num53 < WorldGen.maxStrongTilesX && num54 > WorldGen.minStrongTilesY && num54 < WorldGen.maxStrongTilesY && WorldGen.noKillStrongTiles)) //Mod
                        {
                            NetMessage.SendData(0x11, -1, this.whoAmI, "", num52, (float)num53, (float)num54, (float)num55, 0);
                        }
                    }
                    else
                        NetMessage.SendData(0x11, -1, this.whoAmI, "", num52, (float)num53, (float)num54, (float)num55, 0);

                    WorldGen.noKillStrongTiles = noKillTilesFlag; //Mod

                    if ((num52 != 1) || (num55 != 0x35))
                    {
                        return;
                    }
                    NetMessage.SendTileSquare(-1, num53, num54, 1);
                }

                return;
            }
        Label_1C9C:
            if (Main.netMode == 2)
            {
                NetMessage.SendData(0x13, -1, this.whoAmI, "", num61, (float) num62, (float) num63, (float) num64, 0);
            }
            return;
        Label_3A02:
            num164 = 0;
            while (num164 < 0xff)
            {
                if (((num164 == this.whoAmI) || ((team > 0) && (Main.player[num164].team == team))) || ((num162 > 0) && (Main.player[num164].team == num162)))
                {
                    NetMessage.SendData(0x19, num164, -1, Main.player[num161].name + str16, 0xff, (float) Main.teamColor[num162].R, (float) Main.teamColor[num162].G, (float) Main.teamColor[num162].B, 0);
                }
                num164++;
            }
        }

        public void Reset()
        {
            this.writeBuffer = new byte[0xffff];
            this.writeLocked = false;
            this.messageLength = 0;
            this.totalData = 0;
            this.spamCount = 0;
            this.broadcast = false;
            this.checkBytes = false;
        }
    }
}

