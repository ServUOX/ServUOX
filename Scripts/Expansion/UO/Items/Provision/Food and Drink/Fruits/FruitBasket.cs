
namespace Server.Items
{
    public class FruitBasket : Food
    {
        private bool _DailyRare;

        public bool DailyRare
        {
            get => _DailyRare;
            set
            {
                _DailyRare = value;

                if (_DailyRare)
                {
                    Movable = false;
                }
            }
        }

        [Constructible]
        public FruitBasket()
            : this(false)
        {
        }

        [Constructible]
        public FruitBasket(bool rare)
            : base(1, 0x993)
        {
            Weight = 2.0;
            FillFactor = 5;
            Stackable = false;

            DailyRare = rare;
        }

        public FruitBasket(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!DailyRare)
            {
                base.OnDoubleClick(from);
                return;
            }

            if (from.InRange(GetWorldLocation(), 1))
            {
                Eat(from);
            }
        }

        public override bool Eat(Mobile from)
        {
            var p = Location;

            if (!base.Eat(from))
            {
                return false;
            }

            var basket = new Basket();

            if (Parent == null && DailyRare)
            {
                basket.MoveToWorld(p, from.Map);
            }
            else
            {
                from.AddToBackpack(new Basket());
            }

            return true;
        }

        public override bool TryEat(Mobile from)
        {
            if (!DailyRare)
            {
                return base.TryEat(from);
            }

            if (Deleted || !from.CheckAlive() || !CheckItemUse(from))
            {
                return false;
            }

            return Eat(from);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write(DailyRare);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    DailyRare = reader.ReadBool();
                    break;
            }
        }
    }
}
