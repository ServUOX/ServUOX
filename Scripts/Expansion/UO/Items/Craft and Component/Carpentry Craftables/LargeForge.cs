namespace Server.Items
{
    [Server.Engines.Craft.Forge]
    public class LargeForgeWest : Item
    {
        private LargeForgeWestItem m_Item;
        private LargeForgeWestItem2 m_Item2;
        [Constructible]
        public LargeForgeWest()
            : base(0x199A)
        {
            Movable = false;

            m_Item = new LargeForgeWestItem(this);
            m_Item2 = new LargeForgeWestItem2(this);
        }

        public LargeForgeWest(Serial serial)
            : base(serial)
        {
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            if (m_Item != null)
            {
                m_Item.Location = new Point3D(X, Y + 1, Z);
            }

            if (m_Item2 != null)
            {
                m_Item2.Location = new Point3D(X, Y + 2, Z);
            }
        }

        public override void OnMapChange()
        {
            if (m_Item != null)
            {
                m_Item.Map = Map;
            }

            if (m_Item2 != null)
            {
                m_Item2.Map = Map;
            }
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (m_Item != null)
            {
                m_Item.Delete();
            }

            if (m_Item2 != null)
            {
                m_Item2.Delete();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(m_Item);
            writer.Write(m_Item2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            m_Item = reader.ReadItem() as LargeForgeWestItem;
            m_Item2 = reader.ReadItem() as LargeForgeWestItem2;
        }

        [Engines.Craft.Forge]
        private class LargeForgeWestItem : Item
        {
            private LargeForgeWest m_Item;
            public LargeForgeWestItem(LargeForgeWest item)
                : base(0x1996)
            {
                Movable = false;

                m_Item = item;
            }

            public LargeForgeWestItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y - 1, Z);
                }
            }

            public override void OnMapChange()
            {
                if (m_Item != null)
                {
                    m_Item.Map = Map;
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Item != null)
                {
                    m_Item.Delete();
                }
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(m_Item);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadInt();

                m_Item = reader.ReadItem() as LargeForgeWest;
            }
        }

        [Engines.Craft.Forge]
        private class LargeForgeWestItem2 : Item
        {
            private LargeForgeWest m_Item;
            public LargeForgeWestItem2(LargeForgeWest item)
                : base(0x1992)
            {
                Movable = false;

                m_Item = item;
            }

            public LargeForgeWestItem2(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y - 2, Z);
                }
            }

            public override void OnMapChange()
            {
                if (m_Item != null)
                {
                    m_Item.Map = Map;
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Item != null)
                {
                    m_Item.Delete();
                }
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(m_Item);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadInt();

                m_Item = reader.ReadItem() as LargeForgeWest;
            }
        }
    }

    [Engines.Craft.Forge]
    public class LargeForgeEast : Item
    {
        private LargeForgeEastItem m_Item;
        private LargeForgeEastItem2 m_Item2;
        [Constructible]
        public LargeForgeEast()
            : base(0x197A)
        {
            Movable = false;

            m_Item = new LargeForgeEastItem(this);
            m_Item2 = new LargeForgeEastItem2(this);
        }

        public LargeForgeEast(Serial serial)
            : base(serial)
        {
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            if (m_Item != null)
            {
                m_Item.Location = new Point3D(X + 1, Y, Z);
            }

            if (m_Item2 != null)
            {
                m_Item2.Location = new Point3D(X + 2, Y, Z);
            }
        }

        public override void OnMapChange()
        {
            if (m_Item != null)
            {
                m_Item.Map = Map;
            }

            if (m_Item2 != null)
            {
                m_Item2.Map = Map;
            }
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (m_Item != null)
            {
                m_Item.Delete();
            }

            if (m_Item2 != null)
            {
                m_Item2.Delete();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(m_Item);
            writer.Write(m_Item2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            m_Item = reader.ReadItem() as LargeForgeEastItem;
            m_Item2 = reader.ReadItem() as LargeForgeEastItem2;
        }

        [Engines.Craft.Forge]
        private class LargeForgeEastItem : Item
        {
            private LargeForgeEast m_Item;
            public LargeForgeEastItem(LargeForgeEast item)
                : base(0x197E)
            {
                Movable = false;

                m_Item = item;
            }

            public LargeForgeEastItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X - 1, Y, Z);
                }
            }

            public override void OnMapChange()
            {
                if (m_Item != null)
                {
                    m_Item.Map = Map;
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Item != null)
                {
                    m_Item.Delete();
                }
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(m_Item);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadInt();

                m_Item = reader.ReadItem() as LargeForgeEast;
            }
        }

        [Engines.Craft.Forge]
        private class LargeForgeEastItem2 : Item
        {
            private LargeForgeEast m_Item;
            public LargeForgeEastItem2(LargeForgeEast item)
                : base(0x1982)
            {
                Movable = false;

                m_Item = item;
            }

            public LargeForgeEastItem2(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X - 2, Y, Z);
                }
            }

            public override void OnMapChange()
            {
                if (m_Item != null)
                {
                    m_Item.Map = Map;
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Item != null)
                {
                    m_Item.Delete();
                }
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(m_Item);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadInt();

                m_Item = reader.ReadItem() as LargeForgeEast;
            }
        }
    }
}
