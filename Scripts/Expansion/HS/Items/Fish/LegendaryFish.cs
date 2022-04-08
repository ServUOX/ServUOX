using System;
using Server;

namespace Server.Items
{
    public class AbyssalDragonfish : RareFish
    {
        public override int LabelNumber => 1116118;

        [Constructible]
        public AbyssalDragonfish()
            : base(Utility.RandomMinMax(17637, 17638))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public AbyssalDragonfish(Serial serial) : base(serial) { }

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

    public class BlackMarlin : RareFish
    {
        public override int LabelNumber => 1116099;

        [Constructible]
        public BlackMarlin()
            : base(Utility.RandomMinMax(17156, 17157))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public BlackMarlin(Serial serial) : base(serial) { }

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

    public class BlueMarlin : RareFish
    {
        public override int LabelNumber => 1116097;

        [Constructible]
        public BlueMarlin()
            : base(Utility.RandomMinMax(17156, 17157))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public BlueMarlin(Serial serial) : base(serial) { }

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

    public class DungeonPike : RareFish
    {
        public override int LabelNumber => 1116107;

        [Constructible]
        public DungeonPike()
            : base(Utility.RandomMinMax(17603, 17604))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public DungeonPike(Serial serial) : base(serial) { }

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

    public class GiantSamuraiFish : RareFish
    {
        public override int LabelNumber => 1116103;

        [Constructible]
        public GiantSamuraiFish()
            : base(Utility.RandomMinMax(17158, 17159))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public GiantSamuraiFish(Serial serial) : base(serial) { }

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

    public class GoldenTuna : RareFish
    {
        public override int LabelNumber => 1116102;

        [Constructible]
        public GoldenTuna()
            : base(Utility.RandomMinMax(17154, 17155))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public GoldenTuna(Serial serial) : base(serial) { }

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

    public class Kingfish : RareFish
    {
        public override int LabelNumber => 1116085;

        [Constructible]
        public Kingfish()
            : base(Utility.RandomMinMax(17158, 17159))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public Kingfish(Serial serial) : base(serial) { }

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

    public class LanternFish : RareFish
    {
        public override int LabelNumber => 1116106;

        [Constructible]
        public LanternFish()
            : base(Utility.RandomMinMax(17605, 17606))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public LanternFish(Serial serial) : base(serial) { }

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

    public class RainbowFish : RareFish
    {
        public override int LabelNumber => 1116108;

        [Constructible]
        public RainbowFish()
            : base(Utility.RandomMinMax(17154, 17155))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public RainbowFish(Serial serial) : base(serial) { }

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

    public class SeekerFish : RareFish
    {
        public override int LabelNumber => 1116109;

        [Constructible]
        public SeekerFish()
            : base(Utility.RandomMinMax(17158, 17159))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public SeekerFish(Serial serial) : base(serial) { }

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

    public class SpringDragonfish : RareFish
    {
        public override int LabelNumber => 1116104;

        [Constructible]
        public SpringDragonfish()
            : base(Utility.RandomMinMax(17637, 17638))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public SpringDragonfish(Serial serial) : base(serial) { }

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

    public class StoneFish : RareFish
    {
        public override int LabelNumber => 1116110;

        [Constructible]
        public StoneFish()
            : base(Utility.RandomMinMax(17605, 17606))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public StoneFish(Serial serial) : base(serial) { }

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

    public class WinterDragonfish : RareFish
    {
        public override int LabelNumber => 1116105;

        [Constructible]
        public WinterDragonfish()
            : base(Utility.RandomMinMax(17637, 17638))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public WinterDragonfish(Serial serial) : base(serial) { }

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

    public class ZombieFish : RareFish
    {
        public override int LabelNumber => 1116101;

        [Constructible]
        public ZombieFish()
            : base(Utility.RandomMinMax(17603, 17604))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public ZombieFish(Serial serial) : base(serial) { }

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