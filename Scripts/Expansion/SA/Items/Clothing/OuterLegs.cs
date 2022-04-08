using System;

namespace Server.Items
{
    public class GargishClothKilt : BaseClothing
    {
        [Constructible]
        public GargishClothKilt()
            : this(0)
        {
        }

        [Constructible]
        public GargishClothKilt(int hue)
            : base(0x0408, Layer.Gloves, hue)
        {
            Weight = 2.0;
        }

        public GargishClothKilt(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace => Race.Gargoyle;
        public override bool CanBeWornByGargoyles => true;
        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
            {
                if (((Mobile)parent).Female)
                    ItemID = 0x0407;
                else
                    ItemID = 0x0408;
            }
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

            if (Layer != Layer.Gloves)
                Layer = Layer.Gloves;
        }
    }

    public class FemaleGargishClothKilt : BaseClothing
    {
        [Constructible]
        public FemaleGargishClothKilt()
            : this(0)
        {
        }

        [Constructible]
        public FemaleGargishClothKilt(int hue)
            : base(0x0407, Layer.Gloves, hue)
        {
            Weight = 2.0;
        }

        public FemaleGargishClothKilt(Serial serial)
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

            if (Layer != Layer.Gloves)
                Layer = Layer.Gloves;
        }
    }

    public class MaleGargishClothKilt : BaseClothing
    {
        [Constructible]
        public MaleGargishClothKilt()
            : this(0)
        {
        }

        [Constructible]
        public MaleGargishClothKilt(int hue)
            : base(0x0408, Layer.Gloves, hue)
        {
            Weight = 2.0;
        }

        public MaleGargishClothKilt(Serial serial)
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

            if (Layer != Layer.Gloves)
                Layer = Layer.Gloves;
        }
    }

    public class GuildedKilt : BaseOuterLegs
    {
        public override int LabelNumber => 1109619;  // Guilded Kilt

        [Constructible]
        public GuildedKilt()
            : this(0)
        {
        }

        [Constructible]
        public GuildedKilt(int hue)
            : base(0x781B, hue)
        {
        }

        public GuildedKilt(Serial serial)
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

    public class CheckeredKilt : BaseOuterLegs
    {
        public override int LabelNumber => 1109620;  // Checkered Kilt

        [Constructible]
        public CheckeredKilt()
            : this(0)
        {
        }

        [Constructible]
        public CheckeredKilt(int hue)
            : base(0x781C, hue)
        {
        }

        public CheckeredKilt(Serial serial)
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

    public class FancyKilt : BaseOuterLegs
    {
        public override int LabelNumber => 1109621;  // Fancy Kilt

        [Constructible]
        public FancyKilt()
            : this(0)
        {
        }

        [Constructible]
        public FancyKilt(int hue)
            : base(0x781D, hue)
        {
        }

        public FancyKilt(Serial serial)
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
}
