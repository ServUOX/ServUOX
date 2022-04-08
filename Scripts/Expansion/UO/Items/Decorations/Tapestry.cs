namespace Server.Items
{
    public class Tapestry1N : Item
    {
        private Tapestry1NItem m_Item;
        [Constructible]
        public Tapestry1N()
            : base(0xEAA)
        {
            Movable = false;

            m_Item = new Tapestry1NItem(this);
        }

        public Tapestry1N(Serial serial)
            : base(serial)
        {
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            if (m_Item != null)
            {
                m_Item.Location = new Point3D(X + 1, Y, Z);
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

            m_Item = reader.ReadItem() as Tapestry1NItem;
        }

        private class Tapestry1NItem : Item
        {
            private Tapestry1N m_Item;
            public Tapestry1NItem(Tapestry1N item)
                : base(0xEAB)
            {
                Movable = true;

                m_Item = item;
            }

            public Tapestry1NItem(Serial serial)
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

                m_Item = reader.ReadItem() as Tapestry1N;
            }
        }
    }

    public class Tapestry2N : Item
    {
        private Tapestry2NItem m_Item;
        [Constructible]
        public Tapestry2N()
            : base(0xEAC)
        {
            Movable = false;

            m_Item = new Tapestry2NItem(this);
        }

        public Tapestry2N(Serial serial)
            : base(serial)
        {
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            if (m_Item != null)
            {
                m_Item.Location = new Point3D(X + 1, Y, Z);
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

            m_Item = reader.ReadItem() as Tapestry2NItem;
        }

        private class Tapestry2NItem : Item
        {
            private Tapestry2N m_Item;
            public Tapestry2NItem(Tapestry2N item)
                : base(0xEAD)
            {
                Movable = true;

                m_Item = item;
            }

            public Tapestry2NItem(Serial serial)
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

                m_Item = reader.ReadItem() as Tapestry2N;
            }
        }
    }

    public class Tapestry2W : Item
    {
        private Tapestry2WItem m_Item;
        [Constructible]
        public Tapestry2W()
            : base(0xEAE)
        {
            Movable = false;

            m_Item = new Tapestry2WItem(this);
        }

        public Tapestry2W(Serial serial)
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

            m_Item = reader.ReadItem() as Tapestry2WItem;
        }

        private class Tapestry2WItem : Item
        {
            private Tapestry2W m_Item;
            public Tapestry2WItem(Tapestry2W item)
                : base(0xEAF)
            {
                Movable = true;

                m_Item = item;
            }

            public Tapestry2WItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y + 1, Z);
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

                m_Item = reader.ReadItem() as Tapestry2W;
            }
        }
    }

    public class Tapestry3N : Item
    {
        private Tapestry3NItem m_Item;
        [Constructible]
        public Tapestry3N()
            : base(0xFD6)
        {
            Movable = false;

            m_Item = new Tapestry3NItem(this);
        }

        public Tapestry3N(Serial serial)
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

            m_Item = reader.ReadItem() as Tapestry3NItem;
        }

        private class Tapestry3NItem : Item
        {
            private Tapestry3N m_Item;
            public Tapestry3NItem(Tapestry3N item)
                : base(0xFD5)
            {
                Movable = true;

                m_Item = item;
            }

            public Tapestry3NItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X + 2, Y, Z);
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

                m_Item = reader.ReadItem() as Tapestry3N;
            }
        }
    }

    public class Tapestry3W : Item
    {
        private Tapestry3WItem m_Item;
        [Constructible]
        public Tapestry3W()
            : base(0xFD7)
        {
            Movable = false;

            m_Item = new Tapestry3WItem(this);
        }

        public Tapestry3W(Serial serial)
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

            m_Item = reader.ReadItem() as Tapestry3WItem;
        }

        private class Tapestry3WItem : Item
        {
            private Tapestry3W m_Item;
            public Tapestry3WItem(Tapestry3W item)
                : base(0xFD8)
            {
                Movable = true;

                m_Item = item;
            }

            public Tapestry3WItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y + 2, Z);
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

                m_Item = reader.ReadItem() as Tapestry3W;
            }
        }
    }

    public class Tapestry4N : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry4N()
            : base(0xFDA)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry4N(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry4N m_Item;
            public InternalItem(Tapestry4N item)
                : base(0xFD9)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X + 1, Y, Z);
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

                m_Item = reader.ReadItem() as Tapestry4N;
            }
        }
    }

    public class Tapestry4W : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry4W()
            : base(0xFDB)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry4W(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry4W m_Item;
            public InternalItem(Tapestry4W item)
                : base(0xFDC)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y + 1, Z);
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

                m_Item = reader.ReadItem() as Tapestry4W;
            }
        }
    }

    public class Tapestry5N : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry5N()
            : base(0xFDE)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry5N(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry5N m_Item;
            public InternalItem(Tapestry5N item)
                : base(0xFDD)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X + 1, Y, Z);
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

                m_Item = reader.ReadItem() as Tapestry5N;
            }
        }
    }

    public class Tapestry5W : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry5W()
            : base(0xFDF)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry5W(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry5W m_Item;
            public InternalItem(Tapestry5W item)
                : base(0xFE0)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y + 1, Z);
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

                m_Item = reader.ReadItem() as Tapestry5W;
            }
        }
    }

    public class Tapestry6N : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry6N()
            : base(0xFE2)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry6N(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry6N m_Item;
            public InternalItem(Tapestry6N item)
                : base(0xFE1)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X + 1, Y, Z);
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

                m_Item = reader.ReadItem() as Tapestry6N;
            }
        }
    }

    public class Tapestry6W : Item
    {
        private InternalItem m_Item;
        [Constructible]
        public Tapestry6W()
            : base(0xFE3)
        {
            Movable = false;

            m_Item = new InternalItem(this);
        }

        public Tapestry6W(Serial serial)
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

            m_Item = reader.ReadItem() as InternalItem;
        }

        private class InternalItem : Item
        {
            private Tapestry6W m_Item;
            public InternalItem(Tapestry6W item)
                : base(0xFE4)
            {
                Movable = true;

                m_Item = item;
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void OnLocationChange(Point3D oldLocation)
            {
                if (m_Item != null)
                {
                    m_Item.Location = new Point3D(X, Y + 1, Z);
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

                m_Item = reader.ReadItem() as Tapestry6W;
            }
        }
    }
}
