using System;
using Server;

namespace Server.Items
{
    public class Cannonball : Item, ICommodity, ICannonAmmo
    {
        public override int LabelNumber => 1116266;
        public override double DefaultWeight => 1.0;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public virtual AmmunitionType AmmoType => AmmunitionType.Cannonball;

        [Constructible]
        public Cannonball() : this(1)
        {
        }

        [Constructible]
        public Cannonball(int amount) : this(amount, 16932)
        {
        }

        [Constructible]
        public Cannonball(int amount, int itemid)
            : base(itemid)
        {
            Stackable = true;
            Amount = amount;
        }

        public Cannonball(Serial serial) : base(serial) { }

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

    public class FrostCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116267;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FrostCannonball;

        [Constructible]
        public FrostCannonball() : this(1)
        {
        }

        [Constructible]
        public FrostCannonball(int amount) : this(amount, 16939)
        {
        }

        [Constructible]
        public FrostCannonball(int amount, int itemid)
            : base(itemid)
        {
            Stackable = true;
            Amount = amount;
        }

        public FrostCannonball(Serial serial) : base(serial) { }

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

    public class FlameCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116759;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FlameCannonball;

        [Constructible]
        public FlameCannonball() : this(1)
        {
        }

        [Constructible]
        public FlameCannonball(int amount) : this(amount, 17601)
        {
        }

        [Constructible]
        public FlameCannonball(int amount, int itemid)
            : base(itemid)
        {
            Stackable = true;
            Amount = amount;
        }

        public FlameCannonball(Serial serial) : base(serial) { }

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

    public class LightCannonball : Item, ICommodity, ICannonAmmo
    {
        public override int LabelNumber => 1116266;
        public override double DefaultWeight => 1.0;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public virtual AmmunitionType AmmoType => AmmunitionType.Cannonball;

        [Constructible]
        public LightCannonball() : this(1)
        {
        }

        [Constructible]
        public LightCannonball(int amount) : this(amount, 16932)
        {
        }

        [Constructible]
        public LightCannonball(int amount, int itemid)
            : base(itemid)
        {
            Stackable = true;
            Amount = amount;
        }

        public LightCannonball(Serial serial) : base(serial) { }

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

    public class HeavyCannonball : Item, ICommodity, ICannonAmmo
    {
        public override int LabelNumber => 1116267;
        public override double DefaultWeight => 1.0;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public virtual AmmunitionType AmmoType => AmmunitionType.Cannonball;

        [Constructible]
        public HeavyCannonball() : this(1)
        {
        }

        [Constructible]
        public HeavyCannonball(int amount) : this(amount, 16932)
        {
        }

        public HeavyCannonball(int amount, int itemID) : base(itemID)
        {
            Stackable = true;
            Amount = amount;
        }

        public HeavyCannonball(Serial serial) : base(serial) { }

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

    public class LightFlameCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116759;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FlameCannonball;

        [Constructible]
        public LightFlameCannonball() : this(1)
        {
        }

        [Constructible]
        public LightFlameCannonball(int amount) : base(amount, 17601)
        {
        }

        public LightFlameCannonball(Serial serial) : base(serial) { }

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

    public class HeavyFlameCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116267;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FlameCannonball;

        [Constructible]
        public HeavyFlameCannonball()
            : this(1)
        {
        }

        [Constructible]
        public HeavyFlameCannonball(int amount)
            : base(amount, 17601)
        {
        }

        public HeavyFlameCannonball(Serial serial) : base(serial) { }

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

    public class LightFrostCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116759;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FrostCannonball;

        [Constructible]
        public LightFrostCannonball()
            : this(1)
        {
        }

        [Constructible]
        public LightFrostCannonball(int amount)
            : base(amount, 16939)
        {
        }

        public LightFrostCannonball(Serial serial) : base(serial) { }

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

    public class HeavyFrostCannonball : Cannonball, ICommodity
    {
        public override int LabelNumber => 1116267;

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override AmmunitionType AmmoType => AmmunitionType.FrostCannonball;

        [Constructible]
        public HeavyFrostCannonball()
            : this(1)
        {
        }

        [Constructible]
        public HeavyFrostCannonball(int amount)
            : base(amount, 16939)
        {
        }

        public HeavyFrostCannonball(Serial serial) : base(serial) { }

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
