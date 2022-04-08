using System;

namespace Server.Items
{

    [Flipable(0x2794, 0x27DF)]
    public class ClothNinjaJacket : BaseShirt
    {
        [Constructible]
        public ClothNinjaJacket()
            : this(0)
        {
        }

        [Constructible]
        public ClothNinjaJacket(int hue)
            : base(0x2794, hue)
        {
            Weight = 5.0;
            Layer = Layer.InnerTorso;
        }

        public ClothNinjaJacket(Serial serial)
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
        }
    }

    public class ElvenShirt : BaseShirt
    {
        [Constructible]
        public ElvenShirt()
            : this(0)
        {
        }

        [Constructible]
        public ElvenShirt(int hue)
            : base(0x3175, hue)
        {
            Weight = 2.0;
        }

        public ElvenShirt(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace => Race.Elf;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class ElvenDarkShirt : BaseShirt
    {
        [Constructible]
        public ElvenDarkShirt()
            : this(0)
        {
        }

        [Constructible]
        public ElvenDarkShirt(int hue)
            : base(0x3176, hue)
        {
            Weight = 2.0;
        }

        public ElvenDarkShirt(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace => Race.Elf;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
