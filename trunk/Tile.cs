namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;

    public class Tile
    {
        public bool active;
        public bool checkingLiquid;
        public byte frameNumber;
        public short frameX;
        public short frameY;
        public bool lava;
        public bool lighted;
        public byte liquid;
        public bool skipLiquid;
        public byte type;
        public byte wall;
        public byte wallFrameNumber;
        public byte wallFrameX;
        public byte wallFrameY;
        
        //Mod
        public bool portal;
        public Vector2 portalPartner; //the location of the portal's partner in the tile array

        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}

