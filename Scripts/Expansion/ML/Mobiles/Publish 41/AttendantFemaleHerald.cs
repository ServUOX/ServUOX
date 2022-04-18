using Server.Items;

namespace Server.Mobiles
{
    public class AttendantFemaleHerald : BaseAttendantHerald
    {
        [Constructible]
        public AttendantFemaleHerald()
            : base()
        {
        }

        public AttendantFemaleHerald(Serial serial)
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
            Lantern lantern = new Lantern();
            lantern.Ignite();

            AddItem(lantern);
            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Bonnet(Utility.RandomPinkHue()));
            AddItem(new PlainDress(Utility.RandomPinkHue()));
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
