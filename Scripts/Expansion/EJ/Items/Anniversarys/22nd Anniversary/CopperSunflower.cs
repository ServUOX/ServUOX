namespace Server.Items
{
    [Flipable(0xA35D, 0xA35E)]
    public class CopperSunflower : Item
    {
        private string IDisplayName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string DisplayName { get => IDisplayName; set { IDisplayName = value; InvalidateProperties(); } }

        public override int LabelNumber => 1159149;  // Copper Sunflower

        [Constructible]
        public CopperSunflower()
            : base(0xA35D)
        {
            IDisplayName = Names[Utility.Random(Names.Length)];
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!string.IsNullOrEmpty(IDisplayName))
            {
                list.Add(1159150, IDisplayName); // <BASEFONT COLOR=#FFD24D>Cast from Flowers Grown in The Warm Sun of ~1_NAME~<BASEFONT COLOR=#FFFFFF>
            }

            if (Hue == 2951)
            {
                list.Add(1076187); // Antique
            }
        }

        public CopperSunflower(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(IDisplayName);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            IDisplayName = reader.ReadString();
        }

        private static string[] Names =
        {
            "Trinsic", "Jhelom", "Vesper", "Ocllo", "Yew", "Britain", "Minoc", "Moonglow", "Skara Brae", "Delucia"
        };
    }
}
