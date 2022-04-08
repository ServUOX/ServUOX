namespace Server.Items
{
    public class MerchantsTrinket : GoldEarrings
    {
        private bool _Greater;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bonus => Greater ? 10 : 5;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Greater { get => _Greater; set { _Greater = value; InvalidateProperties(); } }

        public override int LabelNumber => _Greater ? 1156828 : 1156827;  // Merchant's Trinket - 5% / 10%

        [Constructible]
        public MerchantsTrinket()
            : this(false)
        {
        }

        [Constructible]
        public MerchantsTrinket(bool greater)
        {
            Greater = greater;
            LootType = LootType.Blessed;
        }

        public MerchantsTrinket(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(_Greater);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
            _Greater = reader.ReadBool();
        }
    }
}
