
namespace Server.Items
{
    public class WristWatch : Clock
    {
        [Constructible]
        public WristWatch()
            : base(0x1086)
        {
            Weight = 1.0;
            LootType = LootType.Blessed;
            Layer = Layer.Bracelet;
        }

        public WristWatch(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041421;

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
