using Server.Targeting;

namespace Server.Items
{
    public class WheatSheaf : Item
    {
        [Constructible]
        public WheatSheaf()
            : this(1)
        {
        }

        [Constructible]
        public WheatSheaf(int amount)
            : base(7869)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
            {
                return;
            }

            from.BeginTarget(4, false, TargetFlags.None, new TargetCallback(OnTarget));
        }

        public virtual void OnTarget(Mobile from, object obj)
        {
            if (obj is AddonComponent)
            {
                obj = (obj as AddonComponent).Addon;
            }
            if (obj is IFlourMill mill)
            {
                int needs = mill.MaxFlour - mill.CurFlour;

                if (needs > Amount)
                {
                    needs = Amount;
                }

                mill.CurFlour += needs;
                Consume(needs);
            }
        }

        public WheatSheaf(Serial serial)
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
