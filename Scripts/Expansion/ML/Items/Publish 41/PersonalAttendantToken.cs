using Server.Gumps;

namespace Server.Items
{
    public class PersonalAttendantToken : Item
    {
        [Constructible]
        public PersonalAttendantToken()
            : base(0x2AAA)
        {
            LootType = LootType.Blessed;
            Weight = 5.0;
        }

        public PersonalAttendantToken(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1070997;// A promotional token
        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(PersonalAttendantTokenGump));
                from.SendGump(new PersonalAttendantTokenGump(this));
            }
            else
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1070998, string.Format("#{0}", 1075997));  // Use this to redeem<br>Personal Attendant
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
