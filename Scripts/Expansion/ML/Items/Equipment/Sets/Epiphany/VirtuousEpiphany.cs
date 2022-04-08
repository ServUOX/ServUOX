using System;
using System.Linq;

using Server.Mobiles;

namespace Server.Items
{
    public class HelmOfVirtuousEpiphany : PlateHelm, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150233;  // Helm of Virtuous Epiphany

        [Constructible]
        public HelmOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public HelmOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class GorgetOfVirtuousEpiphany : PlateGorget, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150234;  // Gorget of Virtuous Epiphany

        [Constructible]
        public GorgetOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public GorgetOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class BreastplateOfVirtuousEpiphany : PlateChest, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150235;  // Breastplate of Virtuous Epiphany

        [Constructible]
        public BreastplateOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public BreastplateOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class ArmsOfVirtuousEpiphany : PlateArms, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150236;  // Arms of Virtuous Epiphany

        [Constructible]
        public ArmsOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public ArmsOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class GauntletsOfVirtuousEpiphany : PlateGloves, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150237;  // Gauntlets of Virtuous Epiphany

        [Constructible]
        public GauntletsOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public GauntletsOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class LegsOfVirtuousEpiphany : PlateLegs, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150238;  // Leggings of Virtuous Epiphany

        [Constructible]
        public LegsOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public LegsOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class KiltOfVirtuousEpiphany : GargishPlateKilt, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150262;  // Kilt of Virtuous Epiphany

        [Constructible]
        public KiltOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public KiltOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class EarringsOfVirtuousEpiphany : GargishEarrings, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150262;  // Earrings of Virtuous Epiphany

        [Constructible]
        public EarringsOfVirtuousEpiphany()
        {
            Hue = 2076;
            ArmorAttributes.MageArmor = 1;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public EarringsOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class GargishBreastplateOfVirtuousEpiphany : GargishPlateChest, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150235;  // Breastplate of Virtuous Epiphany

        [Constructible]
        public GargishBreastplateOfVirtuousEpiphany()
        {
            Hue = 2076;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public GargishBreastplateOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class GargishArmsOfVirtuousEpiphany : GargishPlateArms, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150236;  // Arms of Virtuous Epiphany

        [Constructible]
        public GargishArmsOfVirtuousEpiphany()
        {
            Hue = 2076;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public GargishArmsOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class NecklaceOfVirtuousEpiphany : GargishNecklace, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150261;  // Necklace of Virtuous Epiphany

        [Constructible]
        public NecklaceOfVirtuousEpiphany()
        {
            Hue = 2076;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public NecklaceOfVirtuousEpiphany(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }

    public class GargishLegsOfVirtuousEpiphany : GargishPlateLegs, IEpiphanyArmor
    {
        public Alignment Alignment => Alignment.Good;
        public SurgeType Type => SurgeType.Mana;
        public int Frequency => EpiphanyHelper.GetFrequency(Parent as Mobile, this);
        public int Bonus => EpiphanyHelper.GetBonus(Parent as Mobile, this);

        public override int LabelNumber => 1150238;  // Legs of Virtuous Epiphany

        [Constructible]
        public GargishLegsOfVirtuousEpiphany()
        {
            Hue = 2076;
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            EpiphanyHelper.AddProperties(this, list);
        }

        public override bool OnEquip(Mobile from)
        {
            bool canEquip = base.OnEquip(from);

            if (canEquip)
            {
                foreach (var armor in from.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }

            return canEquip;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                var m = (Mobile)parent;

                foreach (var armor in m.Items.Where(i => i is IEpiphanyArmor))
                {
                    armor.InvalidateProperties();
                }
            }
        }

        public GargishLegsOfVirtuousEpiphany(Serial serial)
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
            int version = reader.ReadInt();
        }
    }
}