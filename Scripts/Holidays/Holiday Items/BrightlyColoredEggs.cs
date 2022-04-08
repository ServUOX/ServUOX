namespace Server.Items
{
    public class BrightlyColoredEggs : CookableFood
    {
        public override string DefaultName => "brightly colored eggs";//Rare Item Needs more research so far only holiday item

        [Constructible]
        public BrightlyColoredEggs()
            : base(0x9B5, 15)
        {
            Weight = 0.5;
            Hue = 3 + (Utility.Random(20) * 5);
        }

        public BrightlyColoredEggs(Serial serial)
            : base(serial)
        {
        }

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

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }
}
