using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class PersonalAttendantDeed : Item
    {
        private Mobile m_Owner;
        [Constructible]
        public PersonalAttendantDeed()
            : this(null)
        {
        }

        [Constructible]
        public PersonalAttendantDeed(Mobile owner)
            : base(0x14F0)
        {
            m_Owner = owner;

            LootType = LootType.Blessed;
            Weight = 1.0;
        }

        public PersonalAttendantDeed(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1076030;// A Contract for a Personal Attendant
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get
            {
                return m_Owner;
            }
            set
            {
                m_Owner = value;
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                if (m_Owner == null || m_Owner == from)
                {
                    if (!BasePersonalAttendant.CheckAttendant(from))
                    {
                        from.CloseGump(typeof(PersonalAttendantDeedGump));
                        from.SendGump(new PersonalAttendantDeedGump(this));
                    }
                    else
                        from.SendLocalizedMessage(1076053); // You already have an attendant.
                }
                else
                    from.SendLocalizedMessage(501023); // You must be the owner to use this item.
            }
            else
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Owner != null)
                list.Add(1076144, m_Owner.Name); // Property of ~1_OWNER~
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);

            writer.Write(m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();

            m_Owner = reader.ReadMobile();
        }
    }
}
