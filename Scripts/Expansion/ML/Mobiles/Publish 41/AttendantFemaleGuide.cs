using Server.Items;

namespace Server.Mobiles
{
    public class AttendantFemaleGuide : BaseAttendantGuide
    {
        [Constructible]
        public AttendantFemaleGuide()
            : base()
        {
        }

        public AttendantFemaleGuide(Serial serial)
            : base(serial)
        {
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

            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
        }

        public override void InitOutfit()
        {
            AddItem(new Shoes(Utility.RandomBlueHue()));
            AddItem(new Shirt(0x8FD));
            AddItem(new FeatheredHat(Utility.RandomBlueHue()));
            AddItem(new Kilt(Utility.RandomBlueHue()));

            Item item = new Spellbook
            {
                Hue = Utility.RandomBlueHue()
            };
            AddItem(item);
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
