using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Nythalia : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructible]
        public Nythalia()
            : base("the Student")
        {
            Name = "Nythalia";
        }

        public Nythalia(Serial serial)
            : base(serial)
        {
        }

        public override bool CanTeach => false;
        public override bool IsInvulnerable => true;
        protected override List<SBInfo> SBInfos => m_SBInfos;
        public override void InitSBInfo()
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = true;
            Race = Race.Elf;

            Hue = 0x840C;
            HairItemID = 0x2045;
            HairHue = 0x453;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new Sandals(0x74A));
            AddItem(new Robe(0x498));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
