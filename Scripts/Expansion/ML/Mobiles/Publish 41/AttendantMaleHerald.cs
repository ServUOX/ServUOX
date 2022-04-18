using Server.Items;

namespace Server.Mobiles
{
    public class AttendantMaleHerald : BaseAttendantHerald
    {
        [Constructible]
        public AttendantMaleHerald()
            : base()
        {
        }

        public AttendantMaleHerald(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            SetStr(50, 60);
            SetDex(20, 30);
            SetInt(100, 110);

            Name = NameList.RandomName("male");
            Female = false;
            Race = Race.Human;
            Hue = Race.RandomSkinHue();

            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
        }

        public override void InitOutfit()
        {
            AddItem(new FurBoots());
            AddItem(new LongPants(0x901));
            AddItem(new TricorneHat());
            AddItem(new FormalShirt(Utility.RandomBlueHue()));
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
