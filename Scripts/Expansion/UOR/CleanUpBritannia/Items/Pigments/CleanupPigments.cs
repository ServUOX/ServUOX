using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public class IntenseTealPigment : CompassionPigment
    {
        [Constructible]
        public IntenseTealPigment()
            : base(CompassionPigmentType.IntenseTeal)
        {
        }

        public IntenseTealPigment(Serial serial)
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

    public class TyrianPurplePigment : CompassionPigment
    {
        [Constructible]
        public TyrianPurplePigment()
            : base(CompassionPigmentType.TyrianPurple)
        {
        }

        public TyrianPurplePigment(Serial serial)
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

    public class MottledSunsetBluePigment : CompassionPigment
    {
        [Constructible]
        public MottledSunsetBluePigment()
            : base(CompassionPigmentType.MottledSunsetBlue)
        {
        }

        public MottledSunsetBluePigment(Serial serial)
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

    public class MossyGreenPigment : CompassionPigment
    {
        [Constructible]
        public MossyGreenPigment()
            : base(CompassionPigmentType.MossyGreen)
        {
        }

        public MossyGreenPigment(Serial serial)
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

    public class VibrantOcherPigment : CompassionPigment
    {
        [Constructible]
        public VibrantOcherPigment()
            : base(CompassionPigmentType.VibrantOcher)
        {
        }

        public VibrantOcherPigment(Serial serial)
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

    public class OliveGreenPigment : CompassionPigment
    {
        [Constructible]
        public OliveGreenPigment()
            : base(CompassionPigmentType.OliveGreen)
        {
        }

        public OliveGreenPigment(Serial serial)
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

    public class PolishedBronzePigment : CompassionPigment
    {
        [Constructible]
        public PolishedBronzePigment()
            : base(CompassionPigmentType.PolishedBronze)
        {
        }

        public PolishedBronzePigment(Serial serial)
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

    public class GlossyBluePigment : CompassionPigment
    {
        [Constructible]
        public GlossyBluePigment()
            : base(CompassionPigmentType.GlossyBlue)
        {
        }

        public GlossyBluePigment(Serial serial)
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

    public class BlackAndGreenPigment : CompassionPigment
    {
        [Constructible]
        public BlackAndGreenPigment()
            : base(CompassionPigmentType.BlackAndGreen)
        {
        }

        public BlackAndGreenPigment(Serial serial)
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

    public class DeepVioletPigment : CompassionPigment
    {
        [Constructible]
        public DeepVioletPigment()
            : base(CompassionPigmentType.DeepViolet)
        {
        }

        public DeepVioletPigment(Serial serial)
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

    public class AuraOfAmberPigment : CompassionPigment
    {
        [Constructible]
        public AuraOfAmberPigment()
            : base(CompassionPigmentType.AuraOfAmber)
        {
        }

        public AuraOfAmberPigment(Serial serial)
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

    public class MurkySeagreenPigment : CompassionPigment
    {
        [Constructible]
        public MurkySeagreenPigment()
            : base(CompassionPigmentType.MurkySeagreen)
        {
        }

        public MurkySeagreenPigment(Serial serial)
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

    public class ShadowyBluePigment : CompassionPigment
    {
        [Constructible]
        public ShadowyBluePigment()
            : base(CompassionPigmentType.ShadowyBlue)
        {
        }

        public ShadowyBluePigment(Serial serial)
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

    public class GleamingFuchsiaPigment : CompassionPigment
    {
        [Constructible]
        public GleamingFuchsiaPigment()
            : base(CompassionPigmentType.GleamingFuchsia)
        {
        }

        public GleamingFuchsiaPigment(Serial serial)
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

    public class GlossyFuchsiaPigment : CompassionPigment
    {
        [Constructible]
        public GlossyFuchsiaPigment()
            : base(CompassionPigmentType.GlossyFuchsia)
        {
        }

        public GlossyFuchsiaPigment(Serial serial)
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

    public class DeepBluePigment : CompassionPigment
    {
        [Constructible]
        public DeepBluePigment()
            : base(CompassionPigmentType.DeepBlue)
        {
        }

        public DeepBluePigment(Serial serial)
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

    public class VibranSeagreenPigment : CompassionPigment
    {
        [Constructible]
        public VibranSeagreenPigment()
            : base(CompassionPigmentType.VibranSeagreen)
        {
        }

        public VibranSeagreenPigment(Serial serial)
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

    public class MurkyAmberPigment : CompassionPigment
    {
        [Constructible]
        public MurkyAmberPigment()
            : base(CompassionPigmentType.MurkyAmber)
        {
        }

        public MurkyAmberPigment(Serial serial)
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

    public class VibrantCrimsonPigment : CompassionPigment
    {
        [Constructible]
        public VibrantCrimsonPigment()
            : base(CompassionPigmentType.VibrantCrimson)
        {
        }

        public VibrantCrimsonPigment(Serial serial)
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

    public class ReflectiveShadowPigment : CompassionPigment
    {
        [Constructible]
        public ReflectiveShadowPigment()
            : base(CompassionPigmentType.ReflectiveShadow)
        {
        }

        public ReflectiveShadowPigment(Serial serial)
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

    public class StarBluePigment : CompassionPigment
    {
        [Constructible]
        public StarBluePigment()
            : base(CompassionPigmentType.StarBlue)
        {
        }

        public StarBluePigment(Serial serial)
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

    public class MotherOfPearlPigment : CompassionPigment
    {
        [Constructible]
        public MotherOfPearlPigment()
            : base(CompassionPigmentType.MotherOfPearl)
        {
        }

        public MotherOfPearlPigment(Serial serial)
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

    public class LiquidSunshinePigment : CompassionPigment
    {
        [Constructible]
        public LiquidSunshinePigment()
            : base(CompassionPigmentType.LiquidSunshine)
        {
        }

        public LiquidSunshinePigment(Serial serial)
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

    public class DarkVoidPigment : CompassionPigment
    {
        [Constructible]
        public DarkVoidPigment()
            : base(CompassionPigmentType.DarkVoid)
        {
        }

        public DarkVoidPigment(Serial serial)
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
