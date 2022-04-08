using System;

namespace Server.Items
{
    public class Blight : Item, ICommodity
    {
        [Constructible]
        public Blight()
            : this(1)
        {
        }

        [Constructible]
        public Blight(int amount)
            : base(0x3183)
        {
            Stackable = true;
            Amount = amount;
        }

        public Blight(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class LuminescentFungi : Item, ICommodity
    {
        [Constructible]
        public LuminescentFungi()
            : this(1)
        {
        }

        [Constructible]
        public LuminescentFungi(int amount)
            : base(0x3191)
        {
            Stackable = true;
            Amount = amount;
        }

        public LuminescentFungi(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class CapturedEssence : Item, ICommodity
    {
        [Constructible]
        public CapturedEssence()
            : this(1)
        {
        }

        [Constructible]
        public CapturedEssence(int amount)
            : base(0x318E)
        {
            Stackable = true;
            Amount = amount;
        }

        public CapturedEssence(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class EyeOfTheTravesty : Item, ICommodity
    {
        [Constructible]
        public EyeOfTheTravesty()
            : this(1)
        {
        }

        [Constructible]
        public EyeOfTheTravesty(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public EyeOfTheTravesty(int amount)
            : base(0x318D)
        {
            Stackable = true;
            Amount = amount;
        }

        public EyeOfTheTravesty(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class Corruption : Item, ICommodity
    {
        [Constructible]
        public Corruption()
            : this(1)
        {
        }

        [Constructible]
        public Corruption(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Corruption(int amount)
            : base(0x3184)
        {
            Stackable = true;
            Amount = amount;
        }

        public Corruption(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class DreadHornMane : Item, ICommodity
    {
        [Constructible]
        public DreadHornMane()
            : this(1)
        {
        }

        [Constructible]
        public DreadHornMane(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public DreadHornMane(int amount)
            : base(0x318A)
        {
            Stackable = true;
            Amount = amount;
        }

        public DreadHornMane(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class ParasiticPlant : Item, ICommodity
    {
        [Constructible]
        public ParasiticPlant()
            : this(1)
        {
        }

        [Constructible]
        public ParasiticPlant(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public ParasiticPlant(int amount)
            : base(0x3190)
        {
            Stackable = true;
            Amount = amount;
        }

        public ParasiticPlant(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class Muculent : Item, ICommodity
    {
        [Constructible]
        public Muculent()
            : this(1)
        {
        }

        [Constructible]
        public Muculent(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Muculent(int amount)
            : base(0x3188)
        {
            Stackable = true;
            Amount = amount;
        }

        public Muculent(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class DiseasedBark : Item, ICommodity
    {
        [Constructible]
        public DiseasedBark()
            : this(1)
        {
        }

        [Constructible]
        public DiseasedBark(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public DiseasedBark(int amount)
            : base(0x318B)
        {
            Stackable = true;
            Amount = amount;
        }

        public DiseasedBark(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class BarkFragment : Item, ICommodity
    {
        [Constructible]
        public BarkFragment()
            : this(1)
        {
        }

        [Constructible]
        public BarkFragment(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public BarkFragment(int amount)
            : base(0x318F)
        {
            Stackable = true;
            Amount = amount;
        }

        public BarkFragment(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class GrizzledBones : Item, ICommodity
    {
        [Constructible]
        public GrizzledBones()
            : this(1)
        {
        }

        [Constructible]
        public GrizzledBones(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public GrizzledBones(int amount)
            : base(0x318C)
        {
            Stackable = true;
            Amount = amount;
        }

        public GrizzledBones(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version <= 0 && ItemID == 0x318F)
                ItemID = 0x318C;
        }
    }

    public class LardOfParoxysmus : Item, ICommodity
    {
        [Constructible]
        public LardOfParoxysmus()
            : this(1)
        {
        }

        [Constructible]
        public LardOfParoxysmus(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public LardOfParoxysmus(int amount)
            : base(0x3189)
        {
            Stackable = true;
            Amount = amount;
        }

        public LardOfParoxysmus(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class PerfectEmerald : Item, ICommodity
    {
        [Constructible]
        public PerfectEmerald()
            : this(1)
        {
        }

        [Constructible]
        public PerfectEmerald(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public PerfectEmerald(int amount)
            : base(0x3194)
        {
            Stackable = true;
            Amount = amount;
        }

        public PerfectEmerald(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class DarkSapphire : Item, ICommodity
    {
        [Constructible]
        public DarkSapphire()
            : this(1)
        {
        }

        [Constructible]
        public DarkSapphire(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public DarkSapphire(int amount)
            : base(0x3192)
        {
            Stackable = true;
            Amount = amount;
        }

        public DarkSapphire(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class Turquoise : Item, ICommodity
    {
        [Constructible]
        public Turquoise()
            : this(1)
        {
        }

        [Constructible]
        public Turquoise(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Turquoise(int amount)
            : base(0x3193)
        {
            Stackable = true;
            Amount = amount;
        }

        public Turquoise(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class EcruCitrine : Item, ICommodity
    {
        [Constructible]
        public EcruCitrine()
            : this(1)
        {
        }

        [Constructible]
        public EcruCitrine(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public EcruCitrine(int amount)
            : base(0x3195)
        {
            Stackable = true;
            Amount = amount;
        }

        public EcruCitrine(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class WhitePearl : Item, ICommodity
    {
        [Constructible]
        public WhitePearl()
            : this(1)
        {
        }

        [Constructible]
        public WhitePearl(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public WhitePearl(int amount)
            : base(0x3196)
        {
            Stackable = true;
            Amount = amount;
        }

        public WhitePearl(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class FireRuby : Item, ICommodity
    {
        [Constructible]
        public FireRuby()
            : this(1)
        {
        }

        [Constructible]
        public FireRuby(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public FireRuby(int amount)
            : base(0x3197)
        {
            Stackable = true;
            Amount = amount;
        }

        public FireRuby(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class BlueDiamond : Item, ICommodity
    {
        [Constructible]
        public BlueDiamond()
            : this(1)
        {
        }

        [Constructible]
        public BlueDiamond(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public BlueDiamond(int amount)
            : base(0x3198)
        {
            Stackable = true;
            Amount = amount;
        }

        public BlueDiamond(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class BrilliantAmber : Item, ICommodity
    {
        [Constructible]
        public BrilliantAmber()
            : this(1)
        {
        }

        [Constructible]
        public BrilliantAmber(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public BrilliantAmber(int amount)
            : base(0x3199)
        {
            Stackable = true;
            Amount = amount;
        }

        public BrilliantAmber(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class Scourge : Item, ICommodity
    {
        [Constructible]
        public Scourge()
            : this(1)
        {
        }

        [Constructible]
        public Scourge(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Scourge(int amount)
            : base(0x3185)
        {
            Stackable = true;
            Amount = amount;
            Hue = 150;
        }

        public Scourge(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    [TypeAlias("Server.Items.Putrefication")]
    public class Putrefaction : Item, ICommodity
    {
        [Constructible]
        public Putrefaction()
            : this(1)
        {
        }

        [Constructible]
        public Putrefaction(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Putrefaction(int amount)
            : base(0x3186)
        {
            Stackable = true;
            Amount = amount;
            Hue = 883;
        }

        public Putrefaction(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class Taint : Item, ICommodity
    {
        [Constructible]
        public Taint()
            : this(1)
        {
        }

        [Constructible]
        public Taint(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public Taint(int amount)
            : base(0x3187)
        {
            Stackable = true;
            Amount = amount;
            Hue = 731;
        }

        public Taint(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    [Flipable(0x315A, 0x315B)]
    public class PristineDreadHorn : Item, ICommodity
    {
        [Constructible]
        public PristineDreadHorn()
            : base(0x315A)
        {
        }

        public PristineDreadHorn(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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

    public class SwitchItem : Item, ICommodity
    {
        [Constructible]
        public SwitchItem()
            : this(1)
        {
        }

        [Constructible]
        public SwitchItem(int amountFrom, int amountTo)
            : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public SwitchItem(int amount)
            : base(0x2F5F)
        {
            Stackable = true;
            Amount = amount;
        }

        public SwitchItem(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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
