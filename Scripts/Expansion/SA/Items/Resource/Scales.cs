using System;

namespace Server.Items
{
    public abstract class BaseScales : Item, ICommodity
    {
        protected virtual CraftResource DefaultResource => CraftResource.RedScales;

        private CraftResource m_Resource;
        public BaseScales(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseScales(CraftResource resource, int amount)
            : base(0x26B4)
        {
            Stackable = true;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            m_Resource = resource;
        }

        public BaseScales(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1053139;// dragon scales
        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                m_Resource = value;
                InvalidateProperties();
            }
        }
        public override double DefaultWeight => 0.1;
        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1: // Reset from Resource System
                    m_Resource = DefaultResource;
                    reader.ReadString();
                    break;
                case 0:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
            }
        }
    }

    public class RedScales : BaseScales
    {
        [Constructible]
        public RedScales()
            : this(1)
        {
        }

        [Constructible]
        public RedScales(int amount)
            : base(CraftResource.RedScales, amount)
        {
        }

        public RedScales(Serial serial)
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

    public class YellowScales : BaseScales
    {
        protected override CraftResource DefaultResource => CraftResource.YellowScales;

        [Constructible]
        public YellowScales()
            : this(1)
        {
        }

        [Constructible]
        public YellowScales(int amount)
            : base(CraftResource.YellowScales, amount)
        {
        }

        public YellowScales(Serial serial)
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

    public class BlackScales : BaseScales
    {
        protected override CraftResource DefaultResource => CraftResource.BlackScales;

        [Constructible]
        public BlackScales()
            : this(1)
        {
        }

        [Constructible]
        public BlackScales(int amount)
            : base(CraftResource.BlackScales, amount)
        {
        }

        public BlackScales(Serial serial)
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

    public class GreenScales : BaseScales
    {
        protected override CraftResource DefaultResource => CraftResource.GreenScales;

        [Constructible]
        public GreenScales()
            : this(1)
        {
        }

        [Constructible]
        public GreenScales(int amount)
            : base(CraftResource.GreenScales, amount)
        {
        }

        public GreenScales(Serial serial)
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

    public class WhiteScales : BaseScales
    {
        protected override CraftResource DefaultResource => CraftResource.WhiteScales;

        [Constructible]
        public WhiteScales()
            : this(1)
        {
        }

        [Constructible]
        public WhiteScales(int amount)
            : base(CraftResource.WhiteScales, amount)
        {
        }

        public WhiteScales(Serial serial)
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

    public class BlueScales : BaseScales
    {
        protected override CraftResource DefaultResource => CraftResource.BlueScales;

        [Constructible]
        public BlueScales()
            : this(1)
        {
        }

        [Constructible]
        public BlueScales(int amount)
            : base(CraftResource.BlueScales, amount)
        {
        }

        public BlueScales(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1053140;// sea serpent scales
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