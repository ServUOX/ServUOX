using System;
using Server.Engines.VeteranRewards;

namespace Server.Items
{
    [Flipable(0x2FB9, 0x3173)]
    public class MaleElvenRobe : BaseOuterTorso
    {
        public override Race RequiredRace => Race.Elf;

        [Constructible]
        public MaleElvenRobe()
            : this(0)
        {
        }

        [Constructible]
        public MaleElvenRobe(int hue)
            : base(0x2FB9, hue)
        {
            Weight = 2.0;
        }

        public MaleElvenRobe(Serial serial)
            : base(serial)
        {
        }

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

    [Flipable(0x2FBA, 0x3174)]
    public class FemaleElvenRobe : BaseOuterTorso
    {
        public override Race RequiredRace => Race.Elf;
        [Constructible]
        public FemaleElvenRobe()
            : this(0)
        {
        }

        [Constructible]
        public FemaleElvenRobe(int hue)
            : base(0x2FBA, hue)
        {
            Weight = 2.0;
        }

        public override bool AllowMaleWearer => false;

        public FemaleElvenRobe(Serial serial)
            : base(serial)
        {
        }

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

    public class FloweredDress : BaseOuterTorso
    {
        public override int LabelNumber => 1109622;  // Flowered Dress

        [Constructible]
        public FloweredDress()
            : this(0)
        {
        }

        [Constructible]
        public FloweredDress(int hue)
            : base(0x781E, hue)
        {
        }

        public FloweredDress(Serial serial)
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

    public class EveningGown : BaseOuterTorso
    {
        public override int LabelNumber => 1109625;  // Evening Gown

        [Constructible]
        public EveningGown()
            : this(0)
        {
        }

        [Constructible]
        public EveningGown(int hue)
            : base(0x7821, hue)
        {
        }

        public EveningGown(Serial serial)
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

    public class Epaulette : BaseClothing
    {
        public override int LabelNumber => 1123325;  // Epaulette

        [Constructible]
        public Epaulette()
            : this(0)
        {
        }

        [Constructible]
        public Epaulette(int hue)
            : base(0x9985, Layer.OuterTorso, hue)
        {
            Weight = 1.0;
        }

        public Epaulette(Serial serial)
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
