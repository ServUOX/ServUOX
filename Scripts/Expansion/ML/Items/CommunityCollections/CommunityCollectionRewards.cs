namespace Server.Items
{
    #region Reward Clothing
    public class ZooMemberBonnet : Bonnet
    {
        public override int LabelNumber => 1073221; // Britannia Royal Zoo Member

        [Constructible]
        public ZooMemberBonnet()
            : this(0)
        {
        }

        [Constructible]
        public ZooMemberBonnet(int hue)
            : base(hue)
        {
        }

        public ZooMemberBonnet(Serial serial)
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

    public class ZooMemberFloppyHat : FloppyHat
    {
        public override int LabelNumber => 1073221; // Britannia Royal Zoo Member

        [Constructible]
        public ZooMemberFloppyHat()
            : this(0)
        {
        }

        [Constructible]
        public ZooMemberFloppyHat(int hue)
            : base(hue)
        {
        }

        public ZooMemberFloppyHat(Serial serial)
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

    public class LibraryFriendFeatheredHat : FeatheredHat
    {
        public override int LabelNumber => 1073347; // Friends of the Library Feathered Hat

        [Constructible]
        public LibraryFriendFeatheredHat()
            : this(0)
        {
        }

        [Constructible]
        public LibraryFriendFeatheredHat(int hue)
            : base(hue)
        {
        }

        public LibraryFriendFeatheredHat(Serial serial)
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

    public class JesterHatOfChuckles : JesterHat
    {
        public override int LabelNumber => 1073256;// Jester Hat of Chuckles - Museum of Vesper Replica

        public override int BasePhysicalResistance => 12;
        public override int BaseFireResistance => 12;
        public override int BaseColdResistance => 12;
        public override int BasePoisonResistance => 12;
        public override int BaseEnergyResistance => 12;

        public override int InitMinHits => 100;
        public override int InitMaxHits => 100;

        [Constructible]
        public JesterHatOfChuckles()
            : this(0)
        {
        }

        [Constructible]
        public JesterHatOfChuckles(int hue)
            : base(hue)
        {
            Attributes.Luck = 150;
        }

        public JesterHatOfChuckles(Serial serial)
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

    public class NystulsWizardsHat : WizardsHat
    {
        public override int LabelNumber => 1073255;// Nystul's Wizard's Hat - Museum of Vesper Replica

        public override int BasePhysicalResistance => 10;
        public override int BaseFireResistance => 10;
        public override int BaseColdResistance => 10;
        public override int BasePoisonResistance => 10;
        public override int BaseEnergyResistance => 25;

        public override int InitMinHits => 100;
        public override int InitMaxHits => 100;

        [Constructible]
        public NystulsWizardsHat()
            : this(0)
        {
        }

        [Constructible]
        public NystulsWizardsHat(int hue)
            : base(hue)
        {
            Attributes.LowerManaCost = 15;
        }

        public NystulsWizardsHat(Serial serial)
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

    public class GypsyHeaddress : SkullCap
    {
        public override int LabelNumber => 1073254;// Gypsy Headdress - Museum of Vesper Replica

        public override int BasePhysicalResistance => 15;
        public override int BaseFireResistance => 20;
        public override int BaseColdResistance => 20;
        public override int BasePoisonResistance => 15;
        public override int BaseEnergyResistance => 15;

        public override int InitMinHits => 100;
        public override int InitMaxHits => 100;

        [Constructible]
        public GypsyHeaddress()
            : this(0)
        {
        }

        [Constructible]
        public GypsyHeaddress(int hue)
            : base(hue)
        {
        }

        public GypsyHeaddress(Serial serial)
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
    #endregion
}
