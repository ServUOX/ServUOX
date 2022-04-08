using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Prugyilonus : MondainQuester
    {
        public static Prugyilonus Instance { get; set; }

        public static void Initialize()
        {
            if (Core.SA && Instance == null)
            {
                Instance = new Prugyilonus();
                Instance.MoveToWorld(new Point3D(750, 3344, 61), Map.TerMur);
            }
        }

        public override Type[] Quests => new Type[] { typeof(ScalesOfADreamSerpentQuest) };

        public Prugyilonus()
            : base("Prugyilonus", "the Advisor to the Queen")
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);
            Race = Race.Gargoyle;

            CantWalk = true;

            Hue = 34547;
            HairItemID = Race.RandomHair(false);
            HairHue = Race.RandomHairHue();
        }

        public override void InitOutfit()
        {
            SetWearable(new GargishFancyRobe(), 1345);
        }

        public Prugyilonus(Serial serial) : base(serial)
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
