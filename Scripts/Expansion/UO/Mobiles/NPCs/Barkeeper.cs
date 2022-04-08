using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Barkeeper : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();

        [Constructible]
        public Barkeeper()
            : base("the barkeeper")
        { }

        public Barkeeper(Serial serial)
            : base(serial)
        { }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.ThighBoots : VendorShoeType.Boots;
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBarkeeper());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();
            AddItem(new HalfApron(Utility.RandomBrightHue()));
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
