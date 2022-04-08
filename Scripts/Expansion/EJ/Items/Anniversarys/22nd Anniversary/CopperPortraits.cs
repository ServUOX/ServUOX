namespace Server.Items
{
    [Flipable(0xA3E0, 0xA3E4)]
    public class CopperPortrait1 : Item
    {
        private string IDisplayName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string DisplayName { get => IDisplayName; set { IDisplayName = value; InvalidateProperties(); } }

        public override int LabelNumber => 1159154;  // Copper Portrait

        [Constructible]
        public CopperPortrait1()
            : base(0xA3E0)
        {
            IDisplayName = Names[Utility.Random(Names.Length)];
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!string.IsNullOrEmpty(IDisplayName))
            {
                list.Add(1159151, IDisplayName); // <BASEFONT COLOR=#FFD24D>Relief of ~1_NAME~<BASEFONT COLOR=#FFFFFF>
            }

            if (Hue == 2951)
            {
                list.Add(1076187); // Antique
            }
        }

        public CopperPortrait1(Serial serial)
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

        public static string[] Names =
        {
            "Long Tooth Riccia", "Long Leg Topaz", "Glass Tongue Takako", "Iron Fist Riccia", "Fat Eye Takako", "Bloody Back Greg", "Cursed Powder Mercury", "Lone Tongue Erebus", "Mad Powder Sarah", "Long Beard Jim",
            "Lazy Eye Thrixx", "Cursed Patch Artemis", "Mad Back Aeon", "Glass Tooth Asiantam", "Iron Mouth Artemis", "Stink Back Elizabella", "Lost Blade Mercury", "Lazy Mouth Malachi", "Glass Back Nekomata",
            "Tooth Silver Fox"
        };
    }

    [Flipable(0xA3E3, 0xA3E7)]
    public class CopperPortrait2 : Item
    {
        private string IDisplayName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string DisplayName { get => IDisplayName; set { IDisplayName = value; InvalidateProperties(); } }

        public override int LabelNumber => 1159154;  // Copper Portrait

        [Constructible]
        public CopperPortrait2()
            : base(0xA3E3)
        {
            IDisplayName = CopperPortrait1.Names[Utility.Random(CopperPortrait1.Names.Length)];
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!string.IsNullOrEmpty(IDisplayName))
            {
                list.Add(1159151, IDisplayName); // <BASEFONT COLOR=#FFD24D>Relief of ~1_NAME~<BASEFONT COLOR=#FFFFFF>
            }

            if (Hue == 2951)
            {
                list.Add(1076187); // Antique
            }
        }

        public CopperPortrait2(Serial serial)
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
    }
}
