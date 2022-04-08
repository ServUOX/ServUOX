namespace Server.Items
{
    [Flipable(0xA3DE, 0xA3DF)]
    public class CopperWings : Item
    {
        private string IDisplayName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string DisplayName { get => IDisplayName; set { IDisplayName = value; InvalidateProperties(); } }

        public override int LabelNumber => 1159146;  // Copper Wings

        [Constructible]
        public CopperWings()
            : base(0xA3DE)
        {
            IDisplayName = Names[Utility.Random(Names.Length)];
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!string.IsNullOrEmpty(IDisplayName))
            {
                list.Add(1159153, IDisplayName); // <BASEFONT COLOR=#FFD24D>Symbolizing Glory During the ~1_NAME~<BASEFONT COLOR=#FFFFFF>
            }

            if (Hue == 2951)
            {
                list.Add(1076187); // Antique
            }
        }

        public CopperWings(Serial serial)
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
            "Hook's Pirate War", "Endless Struggle Between Platinum And Crimson", "Ophidian War", "Battle Of The Bloody Plains", "Expedition Against Khal Ankur", "Evacuation Of Haven", "Defeat Of Virtuebane", "Siege Of Ver Lor Reg",
            "Assault On The Temple Of The Abyss", "Fall Of Trinsic", "Despise Onslaught"
        };
    }
}
