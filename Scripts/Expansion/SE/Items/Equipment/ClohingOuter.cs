using System;

namespace Server.Items
{
    [Flipable(0x2799, 0x27E4)]
    public class Kamishimo : BaseOuterTorso
    {
        [Constructible]
        public Kamishimo()
            : this(0)
        {
        }

        [Constructible]
        public Kamishimo(int hue)
            : base(0x2799, hue)
        {
            Weight = 3.0;
        }

        public Kamishimo(Serial serial)
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

    [Flipable(0x279C, 0x27E7)]
    public class HakamaShita : BaseOuterTorso
    {
        [Constructible]
        public HakamaShita()
            : this(0)
        {
        }

        [Constructible]
        public HakamaShita(int hue)
            : base(0x279C, hue)
        {
            Weight = 3.0;
        }

        public HakamaShita(Serial serial)
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

    [Flipable(0x2782, 0x27CD)]
    public class MaleKimono : BaseOuterTorso
    {
        [Constructible]
        public MaleKimono()
            : this(0)
        {
        }

        [Constructible]
        public MaleKimono(int hue)
            : base(0x2782, hue)
        {
            Weight = 3.0;
        }

        public override bool AllowFemaleWearer => false;

        public MaleKimono(Serial serial)
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

    [Flipable(0x2783, 0x27CE)]
    public class FemaleKimono : BaseOuterTorso
    {
        [Constructible]
        public FemaleKimono()
            : this(0)
        {
        }

        [Constructible]
        public FemaleKimono(int hue)
            : base(0x2783, hue)
        {
            Weight = 3.0;
        }

        public override bool AllowMaleWearer => false;

        public FemaleKimono(Serial serial)
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
