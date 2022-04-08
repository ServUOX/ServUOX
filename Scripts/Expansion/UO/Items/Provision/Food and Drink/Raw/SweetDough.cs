using Server.Targeting;
using System;

namespace Server.Items
{
    public class SweetDough : Item
    {
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get => _Quality; set { _Quality = value; InvalidateProperties(); } }

        public override int LabelNumber => 1041340;// sweet dough

        [Constructible]
        public SweetDough()
            : base(0x103d)
        {
            Stackable = Core.ML;
            Weight = 1.0;
            Hue = 150;
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public SweetDough(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1);

            writer.Write((int)_Quality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version > 0)
            {
                _Quality = (ItemQuality)reader.ReadInt();
            }

            if (Hue == 51)
            {
                Hue = 150;
            }
        }

#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new SweetDoughTarget( this );
		}
#endif

        private class SweetDoughTarget : Target
        {
            private readonly SweetDough m_Item;

            public SweetDoughTarget(SweetDough item)
                : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                {
                    return;
                }

                if (targeted is BowlFlour)
                {
                    m_Item.Delete();
                    ((BowlFlour)targeted).Delete();

                    from.AddToBackpack(new CakeMix());
                }
                else if (targeted is Campfire)
                {
                    from.PlaySound(0x225);
                    m_Item.Delete();
                    InternalTimer t = new InternalTimer(from, (Campfire)targeted);
                    t.Start();
                }
            }

            private class InternalTimer : Timer
            {
                private readonly Mobile m_From;
                private readonly Campfire m_Campfire;

                public InternalTimer(Mobile from, Campfire campfire)
                    : base(TimeSpan.FromSeconds(5.0))
                {
                    m_From = from;
                    m_Campfire = campfire;
                }

                protected override void OnTick()
                {
                    if (m_From.GetDistanceToSqrt(m_Campfire) > 3)
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                        return;
                    }

                    if (m_From.CheckSkill(SkillName.Cooking, 0, 10))
                    {
                        if (m_From.AddToBackpack(new Muffins()))
                        {
                            m_From.PlaySound(0x57);
                        }
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                    }
                }
            }
        }
    }
}
