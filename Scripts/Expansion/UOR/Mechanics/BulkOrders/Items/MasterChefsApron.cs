namespace Server.Items
{
    public class MasterChefsApron : FullApron
    {
        private int IBonus;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bonus { get => IBonus; set { IBonus = value; InvalidateProperties(); } }

        public override int LabelNumber => 1157228;  // Master Chef's Apron

        [Constructible]
        public MasterChefsApron()
        {
            Hue = 1990;

            while (IBonus == 0)
            {
                IBonus = Utility.RandomMinMax(20, 30);
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1072395, "#{0}\t{1}", AosSkillBonuses.GetLabel(SkillName.Cooking), IBonus); // ~1_NAME~ Exceptional Bonus: ~2_val~%
        }

        public MasterChefsApron(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(IBonus);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
            IBonus = reader.ReadInt();
        }
    }
}
