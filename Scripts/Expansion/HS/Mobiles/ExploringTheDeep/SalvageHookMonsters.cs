using Server.Items;
using System;

namespace Server.Mobiles
{
    public class HPKraken : Kraken
    {
        [Constructible]
        public HPKraken() : base()
        {
            Timer.DelayCall(TimeSpan.FromMinutes(30), Delete);
        }

        public HPKraken(Serial serial) : base(serial)
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

            Delete();
        }
    }

    public class HPDeepSeaSerpent : DeepSeaSerpent
    {
        [Constructible]
        public HPDeepSeaSerpent() : base()
        {
            Timer.DelayCall(TimeSpan.FromMinutes(30), Delete);
        }

        public HPDeepSeaSerpent(Serial serial) : base(serial)
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

            Delete();
        }
    }

    public class HPSeaSerpent : SeaSerpent
    {
        [Constructible]
        public HPSeaSerpent() : base()
        {
            Timer.DelayCall(TimeSpan.FromMinutes(30), Delete);
        }

        public HPSeaSerpent(Serial serial) : base(serial)
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

            Delete();
        }
    }

    public class HPWaterElemental : WaterElemental
    {
        [Constructible]
        public HPWaterElemental() : base()
        {
            Timer.DelayCall(TimeSpan.FromMinutes(30), Delete);
        }

        public HPWaterElemental(Serial serial) : base(serial)
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

            Delete();
        }
    }
}