using System;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Grubbix : MondainQuester
    {
        public override Type[] Quests => new Type[] { typeof(FilthyLifeStealersQuest) };

        public static Grubbix Instance { get; set; }

        public static void Initialize()
        {
            if (Core.SA && Instance == null)
            {
                Instance = new Grubbix();
                Instance.MoveToWorld(new Point3D(1106, 3138, -43), Map.TerMur);
            }
        }

        public Grubbix()
            : base("Grubbix", "the Soulbinder")
        {
        }

        public override void InitBody()
        {
            CantWalk = true;

            InitStats(100, 100, 25);
            Body = 0x100;
            Hue = 2076;
        }

        public Grubbix(Serial serial) : base(serial)
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
