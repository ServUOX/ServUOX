using Server;
using System;
using Server.Mobiles;
using Server.Engines.Points;

namespace Server.Items
{
    public class ShameCrystal : Item
    {
        public override int LabelNumber => 1151624;  // Crystal of Shame

        [Constructible]
        public ShameCrystal() : this(1)
        {
        }

        [Constructible]
        public ShameCrystal(int amount) : base(16395)
        {
            Stackable = true;
            Amount = amount;

            Hue = 2611;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                PointsSystem.ShameCrystals.AwardPoints(from, Amount);
                Delete();
            }
        }

        public ShameCrystal(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}