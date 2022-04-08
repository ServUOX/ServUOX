using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x170f, 0x1710)]
    public class Shoes : BaseShoes
    {
        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructible]
        public Shoes()
            : this(0)
        {
        }

        [Constructible]
        public Shoes(int hue)
            : base(0x170F, hue)
        {
            Weight = 2.0;
        }

        public Shoes(Serial serial)
            : base(serial)
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
            _ = reader.ReadInt();
        }
    }
}
