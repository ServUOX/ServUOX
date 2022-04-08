using Server.Targeting;

namespace Server.Items
{
    public class JarHoney : Item
    {
        [Constructible]
        public JarHoney()
            : base(0x9ec)
        {
            Weight = 1.0;
            Stackable = true;
        }

        public JarHoney(Serial serial)
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

        /*public override void OnDoubleClick( Mobile from )
        {
        if ( !Movable )
        return;

        from.Target = new JarHoneyTarget( this );
        }*/

        private class JarHoneyTarget : Target
        {
            private readonly JarHoney m_Item;

            public JarHoneyTarget(JarHoney item)
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

                if (targeted is Dough)
                {
                    m_Item.Delete();
                    ((Dough)targeted).Consume();

                    from.AddToBackpack(new SweetDough());
                }

                if (targeted is BowlFlour)
                {
                    m_Item.Consume();
                    ((BowlFlour)targeted).Delete();

                    from.AddToBackpack(new CookieMix());
                }
            }
        }
    }
}
