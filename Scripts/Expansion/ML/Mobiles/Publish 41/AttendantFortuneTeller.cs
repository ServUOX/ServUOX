using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;

namespace Server.Mobiles
{
    public class AttendantFortuneTeller : BasePersonalAttendant
    {
        [Constructible]
        public AttendantFortuneTeller()
            : this(false)
        {
        }

        [Constructible]
        public AttendantFortuneTeller(bool female)
            : base("the Fortune Teller")
        {
            Female = female;
        }

        public AttendantFortuneTeller(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Alive && IsOwner(from))
            {
                from.CloseGump(typeof(AttendantFortuneTellerGump));
                from.SendGump(new AttendantFortuneTellerGump(this));
            }
            else
                base.OnDoubleClick(from);
        }

        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive && IsOwner(from))
                list.Add(new AttendantUseEntry(this, 6245));

            base.AddCustomContextEntries(from, list);
        }

        public override void InitBody()
        {
            SetStr(50, 60);
            SetDex(20, 30);
            SetInt(100, 110);

            Name = NameList.RandomName("female");
            Female = true;
            Race = Race.Human;
            Hue = Race.RandomSkinHue();

            Utility.AssignRandomHair(this, Utility.RandomHairHue());
        }

        public override void InitOutfit()
        {
            AddItem(new Shoes(Utility.RandomPinkHue()));
            AddItem(new Shirt(Utility.RandomPinkHue()));
            AddItem(new SkullCap(Utility.RandomPinkHue()));

            if (Utility.RandomBool())
                AddItem(new Kilt(Utility.RandomPinkHue()));
            else
                AddItem(new Skirt(Utility.RandomPinkHue()));
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
