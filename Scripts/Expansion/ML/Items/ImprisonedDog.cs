using Server.Mobiles;

namespace Server.Items
{
    public class ImprisonedDog : BaseImprisonedMobile
    {
        [Constructible]
        public ImprisonedDog()
            : base(0x1F1C)
        {
            Weight = 1.0;
            Hue = 0x485;
        }

        public ImprisonedDog(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1075091;// An Imprisoned Dog
        public override BaseCreature Summon => new TravestyDog();
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
