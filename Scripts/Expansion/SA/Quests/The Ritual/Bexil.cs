using System;
using Server.Engines.Quests;
using Server.Items;

namespace Server.Mobiles
{
    public class Bexil : MondainQuester
    {
        public override Type[] Quests => new Type[] { typeof(CatchMeIfYouCanQuest) };

        public static Bexil Instance { get; set; }

        public static void Initialize()
        {
            if (Core.SA && Instance == null)
            {
                Instance = new Bexil();
                Instance.MoveToWorld(new Point3D(662, 3819, -43), Map.TerMur);
            }
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.Backpack.GetAmount(typeof(DreamSerpentScale)) == 0)
            {
                base.OnDoubleClick(m);
            }
            else
            {
                SayTo(m, 1151355, 0x3B2); // You may not obtain more than one of this item.
                SayTo(m, 1080107, 0x3B2); // I'm sorry, I have nothing for you at this time.
            }
        }

        public Bexil()
            : base("Bexil", "the Dream Serpent")
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);
            Body = 0xCE;
            Hue = 2069;

            CantWalk = true;
        }

        public Bexil(Serial serial) : base(serial)
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
            reader.ReadInt(); // version

            if (Core.SA)
            {
                Instance = this;
            }
            else
            {
                Delete();
            }
        }
    }
}
