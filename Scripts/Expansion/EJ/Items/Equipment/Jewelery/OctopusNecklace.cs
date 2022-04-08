namespace Server.Items
{
    public class OctopusNecklace : BaseNecklace
    {
        [Constructible]
        public OctopusNecklace()
            : base(0xA349)
        {
            AssignRandomGem();
        }

        private void AssignRandomGem()
        {
            var ran = Utility.RandomMinMax(1, 9);
            GemType = (GemType)ran;
        }

        public override void OnGemTypeChange(GemType old)
        {
            if (old == GemType)
            {
                return;
            }

            switch (GemType)
            {
                default:
                case GemType.None: Hue = 0; break;
                case GemType.StarSapphire: Hue = 1928; break;
                case GemType.Emerald: Hue = 1914; break;
                case GemType.Sapphire: Hue = 1926; break;
                case GemType.Ruby: Hue = 1911; break;
                case GemType.Citrine: Hue = 1955; break;
                case GemType.Amethyst: Hue = 1919; break;
                case GemType.Tourmaline: Hue = 1924; break;
                case GemType.Amber: Hue = 1923; break;
                case GemType.Diamond: Hue = 2067; break;
            }
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (GemType != GemType.None)
            {
                list.Add(1159018, $"#{GemLocalization()}"); // ~1_type~ octopus necklace
            }
            else
            {
                list.Add(1125825); // octopus necklace
            }
        }

        public OctopusNecklace(Serial serial)
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
    }
}
