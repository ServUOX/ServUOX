namespace Server.Items
{
    [Flipable(0x1BD7, 0x1BDA)]
    public class BaseWoodBoard : Item, ICommodity, IResource
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get => m_Resource;
            set { m_Resource = value; InvalidateProperties(); }
        }

        TextDefinition ICommodity.Description => LabelNumber;

        public override int LabelNumber => 1015101;

        bool ICommodity.IsDeedable => true;

        [Constructible]
        public BaseWoodBoard()
            : this(1)
        {
        }

        [Constructible]
        public BaseWoodBoard(int amount)
            : this(CraftResource.RegularWood, amount)
        {
        }

        public BaseWoodBoard(Serial serial)
            : base(serial)
        {
        }

        [Constructible]
        public BaseWoodBoard(CraftResource resource) : this(resource, 1)
        {
        }

        [Constructible]
        public BaseWoodBoard(CraftResource resource, int amount)
            : base(0x1BD7)
        {
            Stackable = true;
            Amount = amount;
            m_Resource = resource;
            Hue = CraftResources.GetHue(resource);
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!CraftResources.IsStandard(m_Resource))
            {
                int num = CraftResources.GetLocalizationNumber(m_Resource);

                if (num > 0)
                {
                    list.Add(num);
                }
                else
                {
                    list.Add(CraftResources.GetName(m_Resource));
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(4);
            writer.Write((int)m_Resource);
        }

        public static bool UpdatingBaseClass;
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version == 3)
            {
                UpdatingBaseClass = true;
            }

            switch (version)
            {
                case 4:
                case 3:
                case 2:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
            }

            if ((version == 0 && Weight == 0.1) || (version <= 2 && Weight == 2))
            {
                Weight = -1;
            }

            if (version <= 1)
            {
                m_Resource = CraftResource.RegularWood;
            }
        }
    }

    public class Board : BaseWoodBoard
    {
        [Constructible]
        public Board()
            : this(1)
        {
        }

        [Constructible]
        public Board(int amount)
            : base(CraftResource.RegularWood, amount)
        {
        }

        public Board(Serial serial)
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
            if (UpdatingBaseClass)
            {
                return;
            }

            _ = reader.ReadInt();
        }
    }
}
