using System;

namespace Server.Items
{
    public class FountainOfLifeDeed : BaseAddonContainerDeed
    {
        private int m_Charges;
        [Constructible]
        public FountainOfLifeDeed()
            : this(10)
        {
        }

        [Constructible]
        public FountainOfLifeDeed(int charges)
            : base()
        {
            LootType = LootType.Blessed;
            m_Charges = charges;
        }

        public FountainOfLifeDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1075197;// Fountain of Life
        public override BaseAddonContainer Addon => new FountainOfLife(m_Charges);
        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get
            {
                return m_Charges;
            }
            set
            {
                m_Charges = Math.Min(value, 10);
                InvalidateProperties();
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);

            writer.Write(m_Charges);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            _ = reader.ReadEncodedInt();

            m_Charges = reader.ReadInt();
        }
    }
}
