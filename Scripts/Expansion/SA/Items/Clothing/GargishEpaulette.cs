using System;

namespace Server.Items
{
    public class GargishEpaulette : BaseClothing
    {
        public override int LabelNumber => 1123326;  // Gargish Epaulette

        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;

        [Constructible]
        public GargishEpaulette()
            : this(0)
        {
        }

        [Constructible]
        public GargishEpaulette(int hue)
            : base(0x9986, Layer.OuterTorso, hue)
        {
            Weight = 1.0;
        }

        public GargishEpaulette(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Layer != Layer.OuterTorso)
                Layer = Layer.OuterTorso;
        }
    }
}
