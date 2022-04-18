using Server.Items;

namespace Server.Mobiles
{
    public class AttendantFemaleLuckyDealer : BaseAttendantLuckyDealer
    {
        [Constructible]
        public AttendantFemaleLuckyDealer()
            : base()
        {
        }

        public AttendantFemaleLuckyDealer(Serial serial)
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
            Race = Race.Elf;
            Hue = Race.RandomSkinHue();

            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
        }

        public override void InitOutfit()
        {
            AddItem(new ElvenBoots());
            AddItem(new ElvenPants());
            AddItem(new ElvenShirt());
            AddItem(new JesterHat());
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
