namespace Server.Items
{
    public class SpinedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource => CraftResource.SpinedLeather;

        [Constructible]
        public SpinedLeather()
            : this(1)
        {
        }

        [Constructible]
        public SpinedLeather(int amount)
            : base(CraftResource.SpinedLeather, amount)
        {
        }

        public SpinedLeather(Serial serial)
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
            _ = reader.ReadInt();
        }
    }
    public class HornedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource => CraftResource.HornedLeather;

        [Constructible]
        public HornedLeather()
            : this(1)
        {
        }

        [Constructible]
        public HornedLeather(int amount)
            : base(CraftResource.HornedLeather, amount)
        {
        }

        public HornedLeather(Serial serial)
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
            _ = reader.ReadInt();
        }
    }
    public class BarbedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource => CraftResource.BarbedLeather;

        [Constructible]
        public BarbedLeather()
            : this(1)
        {
        }

        [Constructible]
        public BarbedLeather(int amount)
            : base(CraftResource.BarbedLeather, amount)
        {
        }

        public BarbedLeather(Serial serial)
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
            _ = reader.ReadInt();
        }
    }
}
