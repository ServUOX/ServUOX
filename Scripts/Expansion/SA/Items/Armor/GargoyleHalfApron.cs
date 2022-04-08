
namespace Server.Items
{
    [Flipable(0x50D8, 0x50D9)]
    public class GargoyleHalfApron : BaseWaist
    {
        [Constructible]
        public GargoyleHalfApron()
            : this(0)
        {
        }

        [Constructible]
        public GargoyleHalfApron(int hue)
            : base(0x50D8, hue)
        {
            Weight = 2.0;
        }

        public GargoyleHalfApron(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
