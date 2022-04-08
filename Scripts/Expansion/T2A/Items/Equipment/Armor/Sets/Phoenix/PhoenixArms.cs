namespace Server.Items
{
    public class PhoenixArms : RingmailArms
    {
        [Constructible]
        public PhoenixArms()
        {
            Hue = 0x8E;
            LootType = LootType.Blessed;
        }

        public PhoenixArms(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1041607;// ringmail sleeves of the phoenix

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
