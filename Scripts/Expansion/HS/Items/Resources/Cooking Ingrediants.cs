using Server;
using System;
using Server.Targeting;

namespace Server.Items
{
    public class MentoSeasoning : Item
    {
        public override int LabelNumber => 1116299;

        [Constructible]
        public MentoSeasoning()
            : base(2454)
        {
            Hue = 95;
        }

        public MentoSeasoning(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class SamuelsSecretSauce : Item
    {
        public override int LabelNumber => 1116338;

        [Constructible]
        public SamuelsSecretSauce() : base(2463)
        {
            Hue = 1461;
        }

        public SamuelsSecretSauce(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class DarkTruffle : Item
    {
        public override int LabelNumber => 1116300;

        [Constructible]
        public DarkTruffle()
            : base(3352)
        {
            Hue = 1908;
        }

        public DarkTruffle(Serial serial)
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
            int version = reader.ReadInt();
        }
    }

    public class FreshGinger : Item
    {
        public override int LabelNumber => 1031235;

        [Constructible]
        public FreshGinger()
            : base(11235)
        {
        }

        public FreshGinger(Serial serial)
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
            int version = reader.ReadInt();
        }
    }

    public class FishOilFlask : Item
    {
        public override int LabelNumber => 1150863;

        [Constructible]
        public FishOilFlask()
            : base(7192)
        {
            Hue = 2404;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
                from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private FishOilFlask m_Flask;

            public InternalTarget(FishOilFlask flask) : base(-1, false, TargetFlags.None)
            {
                m_Flask = flask;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is OracleOfTheSea)
                {
                    if (((OracleOfTheSea)targeted).UsesRemaining >= 5)
                        from.SendMessage("That is already fully charged!");
                    else
                    {
                        ((OracleOfTheSea)targeted).UsesRemaining = 5;
                        from.SendMessage("You charge the oracle with the fish oil.");
                        m_Flask.Delete();
                    }
                }
            }
        }

        public FishOilFlask(Serial serial)
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
            int version = reader.ReadInt();
        }
    }
}