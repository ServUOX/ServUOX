using System;
using Server;

namespace Server.Items
{
    public class RareFish : BaseHighseasFish
    {
        private Mobile m_CaughtBy;
        private DateTime m_DateCaught;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Fisher { get { return m_CaughtBy; } set { m_CaughtBy = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime DateCaught { get { return m_DateCaught; } set { m_DateCaught = value; } }

        public RareFish(int itemID)
            : base(itemID)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1070857, m_CaughtBy != null ? m_CaughtBy.Name : "An Unknown Fisher"); //Caught by ~1_fisherman~

            if (m_DateCaught != DateTime.MinValue)
                list.Add("[{0}]", m_DateCaught.ToShortDateString());
        }

        public RareFish(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(m_CaughtBy);
            writer.Write(m_DateCaught);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_CaughtBy = reader.ReadMobile();
            m_DateCaught = reader.ReadDateTime();
        }
    }

    public class AutumnDragonfish : RareFish
    {
        public override int LabelNumber => 1116090;
        public override Item GetCarved => new AutumnDragonfishSteak();

        [Constructible]
        public AutumnDragonfish()
            : base(Utility.RandomMinMax(17637, 17638))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public AutumnDragonfish(Serial serial) : base(serial) { }

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

    public class BullFish : RareFish
    {
        public override int LabelNumber => 1116095;
        public override Item GetCarved => new BullFishSteak();

        [Constructible]
        public BullFish()
            : base(Utility.RandomMinMax(17605, 17606))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public BullFish(Serial serial) : base(serial) { }

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

    public class CrystalFish : RareFish
    {
        public override int LabelNumber => 1116092;
        public override Item GetCarved => new CrystalFishSteak();

        [Constructible]
        public CrystalFish()
            : base(Utility.RandomMinMax(17154, 17155))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public CrystalFish(Serial serial) : base(serial) { }

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

    public class FairySalmon : RareFish
    {
        public override int LabelNumber => 1116089;
        public override Item GetCarved => new FairySalmonSteak();

        [Constructible]
        public FairySalmon()
            : base(Utility.RandomMinMax(17154, 17155))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public FairySalmon(Serial serial) : base(serial) { }

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

    public class FireFish : RareFish
    {
        public override int LabelNumber => 1116093;
        public override Item GetCarved => new FireFishSteak();

        [Constructible]
        public FireFish()
            : base(Utility.RandomMinMax(17158, 17159))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public FireFish(Serial serial) : base(serial) { }

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

    public class GiantKoi : RareFish
    {
        public override int LabelNumber => 1116088;
        public override Item GetCarved => new GiantKoiSteak();

        [Constructible]
        public GiantKoi()
            : base(Utility.RandomMinMax(17605, 17606))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public GiantKoi(Serial serial) : base(serial) { }

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

    public class GreatBarracuda : RareFish
    {
        public override int LabelNumber => 1116100;
        public override Item GetCarved => new GreatBarracudaSteak();

        [Constructible]
        public GreatBarracuda()
            : base(Utility.RandomMinMax(17603, 17604))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public GreatBarracuda(Serial serial) : base(serial) { }

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

    public class HolyMackerel : RareFish
    {
        public override int LabelNumber => 1116087;
        public override Item GetCarved => new HolyMackerelSteak();

        [Constructible]
        public HolyMackerel()
            : base(Utility.RandomMinMax(17154, 17155))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public HolyMackerel(Serial serial) : base(serial) { }

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

    public class LavaFish : RareFish
    {
        public override int LabelNumber => 1116096;
        public override Item GetCarved => new LavaFishSteak();

        [Constructible]
        public LavaFish()
            : base(Utility.RandomMinMax(17156, 17157))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public LavaFish(Serial serial) : base(serial) { }

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

    public class ReaperFish : RareFish
    {
        public override int LabelNumber => 1116094;
        public override Item GetCarved => new ReaperFishSteak();

        [Constructible]
        public ReaperFish()
            : base(Utility.RandomMinMax(17603, 17604))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public ReaperFish(Serial serial) : base(serial) { }

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

    public class SummerDragonfish : RareFish
    {
        public override int LabelNumber => 1116091;
        public override Item GetCarved => new SummerDragonfishSteak();

        [Constructible]
        public SummerDragonfish()
            : base(Utility.RandomMinMax(17637, 17638))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public SummerDragonfish(Serial serial) : base(serial) { }

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

    public class UnicornFish : RareFish
    {
        public override int LabelNumber => 1116086;
        public override Item GetCarved => new UnicornFishSteak();

        [Constructible]
        public UnicornFish()
            : base(Utility.RandomMinMax(17156, 17157))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public UnicornFish(Serial serial) : base(serial) { }

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

    public class YellowtailBarracuda : RareFish
    {
        public override int LabelNumber => 1116098;
        public override Item GetCarved => new YellowtailBarracudaSteak();

        [Constructible]
        public YellowtailBarracuda()
            : base(Utility.RandomMinMax(17603, 17604))
        {
            Hue = FishInfo.GetFishHue(GetType());
        }

        public YellowtailBarracuda(Serial serial) : base(serial) { }

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

    public class AutumnDragonfishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116314;

        [Constructible]
        public AutumnDragonfishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(AutumnDragonfish));
        }

        public AutumnDragonfishSteak(Serial serial) : base(serial) { }

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


    public class BullFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116310;

        [Constructible]
        public BullFishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(BullFish));
        }

        public BullFishSteak(Serial serial) : base(serial) { }

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


    public class CrystalFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116309;

        [Constructible]
        public CrystalFishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(CrystalFish));
        }

        public CrystalFishSteak(Serial serial) : base(serial) { }

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

    public class FairySalmonSteak : RawFishSteak
    {
        public override int LabelNumber => 1116312;

        [Constructible]
        public FairySalmonSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(FairySalmon));
        }

        public FairySalmonSteak(Serial serial) : base(serial) { }

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


    public class FireFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116307;

        [Constructible]
        public FireFishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(FireFish));
        }

        public FireFishSteak(Serial serial) : base(serial) { }

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


    public class GiantKoiSteak : RawFishSteak
    {
        public override int LabelNumber => 1116306;

        [Constructible]
        public GiantKoiSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(GiantKoi));
        }

        public GiantKoiSteak(Serial serial) : base(serial) { }

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

    public class GreatBarracudaSteak : RawFishSteak
    {
        public override int LabelNumber => 1116298;

        [Constructible]
        public GreatBarracudaSteak()
        {
            Hue = 1287; // FishInfo.GetFishHue(typeof(GreatBarracuda));
        }

        public GreatBarracudaSteak(Serial serial) : base(serial) { }

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


    public class HolyMackerelSteak : RawFishSteak
    {
        public override int LabelNumber => 1116315;

        [Constructible]
        public HolyMackerelSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(HolyMackerel));
        }

        public HolyMackerelSteak(Serial serial) : base(serial) { }

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


    public class LavaFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116313;

        [Constructible]
        public LavaFishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(LavaFish));
        }

        public LavaFishSteak(Serial serial) : base(serial) { }

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


    public class ReaperFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116308;

        [Constructible]
        public ReaperFishSteak()
        {
            Hue = 1152; // FishInfo.GetFishHue(typeof(ReaperFish));
        }

        public ReaperFishSteak(Serial serial) : base(serial) { }

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


    public class SummerDragonfishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116311;

        [Constructible]
        public SummerDragonfishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(SummerDragonfish));
        }

        public SummerDragonfishSteak(Serial serial) : base(serial) { }

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


    public class UnicornFishSteak : RawFishSteak
    {
        public override int LabelNumber => 1116316;

        [Constructible]
        public UnicornFishSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(UnicornFish));
        }

        public UnicornFishSteak(Serial serial) : base(serial) { }

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

    public class YellowtailBarracudaSteak : RawFishSteak
    {
        public override int LabelNumber => 1116301;

        [Constructible]
        public YellowtailBarracudaSteak()
        {
            Hue = FishInfo.GetFishHue(typeof(YellowtailBarracuda));
        }

        public YellowtailBarracudaSteak(Serial serial) : base(serial) { }

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