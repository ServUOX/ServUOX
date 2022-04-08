using Server.Engines.Craft;
using Server.Targeting;
using System;

namespace Server.Items
{
    public class Dough : Item, IQuality
    {
        private ItemQuality IQuality;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get => IQuality; set { IQuality = value; InvalidateProperties(); } }

        public bool PlayerConstructed => true;

        [Constructible]
        public Dough()
            : base(0x103d)
        {
            Stackable = Core.ML;
            Weight = 1.0;
        }

        public override bool WillStack(Mobile from, Item item)
        {
            if (item is IQuality && ((IQuality)item).Quality != IQuality)
            {
                return false;
            }

            return base.WillStack(from, item);
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (IQuality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public Dough(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write((int)IQuality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version > 0)
            {
                IQuality = (ItemQuality)reader.ReadInt();
            }
        }

#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new DoughTarget( this );
		}
#endif

        private class DoughTarget : Target
        {
            private readonly Dough m_Item;

            public DoughTarget(Dough item)
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

                if (targeted is Eggs)
                {
                    m_Item.Delete();

                    ((Eggs)targeted).Consume();

                    from.AddToBackpack(new UnbakedQuiche());
                    from.AddToBackpack(new Eggshells());
                }
                else if (targeted is CheeseWheel)
                {
                    m_Item.Delete();

                    ((CheeseWheel)targeted).Consume();

                    from.AddToBackpack(new CheesePizza());
                }
                else if (targeted is Sausage)
                {
                    m_Item.Delete();

                    ((Sausage)targeted).Consume();

                    from.AddToBackpack(new SausagePizza());
                }
                else if (targeted is Apple)
                {
                    m_Item.Delete();

                    ((Apple)targeted).Consume();

                    from.AddToBackpack(new UnbakedApplePie());
                }
                else if (targeted is Peach)
                {
                    m_Item.Delete();

                    ((Peach)targeted).Consume();

                    from.AddToBackpack(new UnbakedPeachCobbler());
                }
            }
        }
    }
}
