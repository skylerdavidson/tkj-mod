﻿namespace Terraria
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class Netplay
    {
        public static bool anyClients = false;
        public static string banFile = "banlist.txt";
        public const int bufferSize = 0x400;
        public static ClientSock clientSock = new ClientSock();
        public static bool disconnect = false;
        public const int maxConnections = 0x100;
        public static string password = "";
        public static IPAddress serverIP;
        public static IPAddress serverListenIP;
        public static int serverPort = 7796;
        public static ServerSock[] serverSock = new ServerSock[0x100];
        public static bool ServerUp = false;
        public static bool spamCheck = false;
        public static bool stopListen = false;
        public static TcpListener tcpListener;

        public static void AddBan(int plr)
        {
            string str = serverSock[plr].tcpClient.Client.RemoteEndPoint.ToString();
            string str2 = str;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(i, 1) == ":")
                {
                    str2 = str.Substring(0, i);
                }
            }
            using (StreamWriter writer = new StreamWriter(banFile, true))
            {
                writer.WriteLine("//" + Main.player[plr].name);
                writer.WriteLine(str2);
            }
        }

        public static bool CheckBan(string ip)
        {
            try
            {
                string str = ip;
                for (int i = 0; i < ip.Length; i++)
                {
                    if (ip.Substring(i, 1) == ":")
                    {
                        str = ip.Substring(0, i);
                    }
                }
                if (System.IO.File.Exists(banFile))
                {
                    using (StreamReader reader = new StreamReader(banFile))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2 == str)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public static void ClientLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                Main.rand = new Random((int) DateTime.Now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                WorldGen.genRand = new Random((int) DateTime.Now.Ticks);
            }
            Main.player[Main.myPlayer].hostile = false;
            Main.clientPlayer = (Player) Main.player[Main.myPlayer].clientClone();
            Main.menuMode = 10;
            Main.menuMode = 14;
            if (!Main.autoPass)
            {
                Main.statusText = "Connecting to " + serverIP;
            }
            Main.netMode = 1;
            disconnect = false;
            clientSock = new ClientSock();
            clientSock.tcpClient.NoDelay = true;
            clientSock.readBuffer = new byte[0x400];
            clientSock.writeBuffer = new byte[0x400];
            bool flag = true;
            while (flag)
            {
                flag = false;
                try
                {
                    clientSock.tcpClient.Connect(serverIP, serverPort);
                    clientSock.networkStream = clientSock.tcpClient.GetStream();
                    flag = false;
                    continue;
                }
                catch
                {
                    if (!disconnect && Main.gameMenu)
                    {
                        flag = true;
                    }
                    continue;
                }
            }
            NetMessage.buffer[0x100].Reset();
            for (int i = -1; !disconnect; i = clientSock.state)
            {
                if (clientSock.tcpClient.Connected)
                {
                    if (NetMessage.buffer[0x100].checkBytes)
                    {
                        NetMessage.CheckBytes(0x100);
                    }
                    clientSock.active = true;
                    if (clientSock.state == 0)
                    {
                        Main.statusText = "Found server";
                        clientSock.state = 1;
                        NetMessage.SendData(1, -1, -1, "", 0, 0f, 0f, 0f, 0);
                    }
                    if ((clientSock.state == 2) && (i != clientSock.state))
                    {
                        Main.statusText = "Sending player data...";
                    }
                    if ((clientSock.state == 3) && (i != clientSock.state))
                    {
                        Main.statusText = "Requesting world information";
                    }
                    if (clientSock.state == 4)
                    {
                        WorldGen.worldCleared = false;
                        clientSock.state = 5;
                        WorldGen.clearWorld();
                    }
                    if ((clientSock.state == 5) && WorldGen.worldCleared)
                    {
                        clientSock.state = 6;
                        Main.player[Main.myPlayer].FindSpawn();
                        NetMessage.SendData(8, -1, -1, "", Main.player[Main.myPlayer].SpawnX, (float) Main.player[Main.myPlayer].SpawnY, 0f, 0f, 0);
                    }
                    if ((clientSock.state == 6) && (i != clientSock.state))
                    {
                        Main.statusText = "Requesting tile data";
                    }
                    if ((!clientSock.locked && !disconnect) && clientSock.networkStream.DataAvailable)
                    {
                        clientSock.locked = true;
                        clientSock.networkStream.BeginRead(clientSock.readBuffer, 0, clientSock.readBuffer.Length, new AsyncCallback(clientSock.ClientReadCallBack), clientSock.networkStream);
                    }
                    if ((clientSock.statusMax > 0) && (clientSock.statusText != ""))
                    {
                        if (clientSock.statusCount >= clientSock.statusMax)
                        {
                            Main.statusText = clientSock.statusText + ": Complete!";
                            clientSock.statusText = "";
                            clientSock.statusMax = 0;
                            clientSock.statusCount = 0;
                        }
                        else
                        {
                            Main.statusText = string.Concat(new object[] { clientSock.statusText, ": ", (int) ((((float) clientSock.statusCount) / ((float) clientSock.statusMax)) * 100f), "%" });
                        }
                    }
                    Thread.Sleep(1);
                }
                else if (clientSock.active)
                {
                    Main.statusText = "Lost connection";
                    disconnect = true;
                }
            }
            try
            {
                clientSock.networkStream.Close();
                clientSock.networkStream = clientSock.tcpClient.GetStream();
            }
            catch
            {
            }
            if (!Main.gameMenu)
            {
                Main.netMode = 0;
                Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
                Main.gameMenu = true;
                Main.menuMode = 14;
            }
            NetMessage.buffer[0x100].Reset();
            if ((Main.menuMode == 15) && (Main.statusText == "Lost connection"))
            {
                Main.menuMode = 14;
            }
            if ((clientSock.statusText != "") && (clientSock.statusText != null))
            {
                Main.statusText = "Lost connection";
            }
            clientSock.statusCount = 0;
            clientSock.statusMax = 0;
            clientSock.statusText = "";
            Main.netMode = 0;
        }

        public static int GetSectionX(int x)
        {
            return (x / 200);
        }

        public static int GetSectionY(int y)
        {
            return (y / 150);
        }

        public static void Init()
        {
            for (int i = 0; i < 0x101; i++)
            {
                if (i < 0x100)
                {
                    serverSock[i] = new ServerSock();
                    serverSock[i].tcpClient.NoDelay = true;
                }
                NetMessage.buffer[i] = new messageBuffer();
                NetMessage.buffer[i].whoAmI = i;
            }
            clientSock.tcpClient.NoDelay = true;
        }

        public static void ListenForClients(object threadContext)
        {
            while (!disconnect && !stopListen)
            {
                int index = -1;
                for (int i = 0; i < Main.maxNetPlayers; i++)
                {
                    if (!serverSock[i].tcpClient.Connected)
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                {
                    try
                    {
                        serverSock[index].tcpClient = tcpListener.AcceptTcpClient();
                        serverSock[index].tcpClient.NoDelay = true;
                        Console.WriteLine(serverSock[index].tcpClient.Client.RemoteEndPoint + " is connecting...");
                    }
                    catch (Exception exception)
                    {
                        if (!disconnect)
                        {
                            Main.menuMode = 15;
                            Main.statusText = exception.ToString();
                            disconnect = true;
                        }
                    }
                }
                else
                {
                    stopListen = true;
                    tcpListener.Stop();
                }
            }
        }

        public static void ServerLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                Main.rand = new Random((int) DateTime.Now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                WorldGen.genRand = new Random((int) DateTime.Now.Ticks);
            }
            Main.myPlayer = 0xff;
            serverIP = IPAddress.Any;
            serverListenIP = serverIP;
            Main.menuMode = 14;
            Main.statusText = "Starting server...";
            Main.netMode = 2;
            disconnect = false;
            for (int i = 0; i < 0x100; i++)
            {
                serverSock[i] = new ServerSock();
                serverSock[i].Reset();
                serverSock[i].whoAmI = i;
                serverSock[i].tcpClient = new TcpClient();
                serverSock[i].tcpClient.NoDelay = true;
                serverSock[i].readBuffer = new byte[0x400];
                serverSock[i].writeBuffer = new byte[0x400];
            }
            tcpListener = new TcpListener(serverListenIP, serverPort);
            try
            {
                tcpListener.Start();
            }
            catch (Exception exception)
            {
                Main.menuMode = 15;
                Main.statusText = exception.ToString();
                disconnect = true;
            }
            if (!disconnect)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ListenForClients), 1);
                Main.statusText = "Server started";
            }
            int num2 = 0;
            while (!disconnect)
            {
                if (stopListen)
                {
                    int num3 = -1;
                    for (int m = 0; m < Main.maxNetPlayers; m++)
                    {
                        if (!serverSock[m].tcpClient.Connected)
                        {
                            num3 = m;
                            break;
                        }
                    }
                    if (num3 >= 0)
                    {
                        if (Main.ignoreErrors)
                        {
                            try
                            {
                                tcpListener.Start();
                                stopListen = false;
                                ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ListenForClients), 1);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            tcpListener.Start();
                            stopListen = false;
                            ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ListenForClients), 1);
                        }
                    }
                }
                int num5 = 0;
                for (int k = 0; k < 0x100; k++)
                {
                    if (NetMessage.buffer[k].checkBytes)
                    {
                        NetMessage.CheckBytes(k);
                    }
                    if (serverSock[k].kill)
                    {
                        serverSock[k].Reset();
                        NetMessage.syncPlayers();
                    }
                    else if (serverSock[k].tcpClient.Connected)
                    {
                        if (!serverSock[k].active)
                        {
                            serverSock[k].state = 0;
                        }
                        serverSock[k].active = true;
                        num5++;
                        if (!serverSock[k].locked)
                        {
                            try
                            {
                                serverSock[k].networkStream = serverSock[k].tcpClient.GetStream();
                                if (serverSock[k].networkStream.DataAvailable)
                                {
                                    serverSock[k].locked = true;
                                    serverSock[k].networkStream.BeginRead(serverSock[k].readBuffer, 0, serverSock[k].readBuffer.Length, new AsyncCallback(serverSock[k].ServerReadCallBack), serverSock[k].networkStream);
                                }
                            }
                            catch
                            {
                                serverSock[k].kill = true;
                            }
                        }
                        if ((serverSock[k].statusMax > 0) && (serverSock[k].statusText2 != ""))
                        {
                            if (serverSock[k].statusCount >= serverSock[k].statusMax)
                            {
                                serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " ", serverSock[k].statusText2, ": Complete!" });
                                serverSock[k].statusText2 = "";
                                serverSock[k].statusMax = 0;
                                serverSock[k].statusCount = 0;
                            }
                            else
                            {
                                serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " ", serverSock[k].statusText2, ": ", (int) ((((float) serverSock[k].statusCount) / ((float) serverSock[k].statusMax)) * 100f), "%" });
                            }
                        }
                        else if (serverSock[k].state == 0)
                        {
                            serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is connecting..." });
                        }
                        else if (serverSock[k].state == 1)
                        {
                            serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is sending player data..." });
                        }
                        else if (serverSock[k].state == 2)
                        {
                            serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " requested world information" });
                        }
                        else if ((serverSock[k].state != 3) && (serverSock[k].state == 10))
                        {
                            serverSock[k].statusText = string.Concat(new object[] { "(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is playing" });
                        }
                    }
                    else if (serverSock[k].active)
                    {
                        serverSock[k].kill = true;
                    }
                    else
                    {
                        serverSock[k].statusText2 = "";
                        if (k < 0xff)
                        {
                            Main.player[k].active = false;
                        }
                    }
                }
                num2++;
                if (num2 > 10)
                {
                    Thread.Sleep(1);
                    num2 = 0;
                }
                else
                {
                    Thread.Sleep(0);
                }
                if (!WorldGen.saveLock && !Main.dedServ)
                {
                    if (num5 == 0)
                    {
                        Main.statusText = "Waiting for clients...";
                    }
                    else
                    {
                        Main.statusText = num5 + " clients connected";
                    }
                }
                if (num5 == 0)
                {
                    anyClients = false;
                }
                else
                {
                    anyClients = true;
                }
                ServerUp = true;
            }
            tcpListener.Stop();
            for (int j = 0; j < 0x100; j++)
            {
                serverSock[j].Reset();
            }
            if (Main.menuMode != 15)
            {
                Main.netMode = 0;
                Main.menuMode = 10;
                WorldGen.saveWorld(false);
                while (WorldGen.saveLock)
                {
                }
                Main.menuMode = 0;
            }
            else
            {
                Main.netMode = 0;
            }
            Main.myPlayer = 0;
        }

        public static bool SetIP(string newIP)
        {
            try
            {
                serverIP = IPAddress.Parse(newIP);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SetIP2(string newIP)
        {
            try
            {
                IPAddress[] addressList = Dns.GetHostEntry(newIP).AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (addressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        serverIP = addressList[i];
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void StartClient()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ClientLoop), 1);
        }

        public static void StartServer()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ServerLoop), 1);
        }
    }
}

