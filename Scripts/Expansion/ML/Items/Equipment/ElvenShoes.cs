using System;
using Server.Engines.Craft;

namespace Server.Items
{

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x2FC4, 0x317A)]
    public class ElvenBoots : BaseShoes
    {
        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        public override Race RequiredRace => Race.Elf;

        [Constructible]
        public ElvenBoots()
            : this(0)
        {
        }

        [Constructible]
        public ElvenBoots(int hue)
            : base(0x2FC4, hue)
        {
            Weight = 2.0;
        }

        public ElvenBoots(Serial serial)
            : base(serial)
        {
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    public class JesterShoes : BaseShoes
    {
        public override int LabelNumber => 1109617;  // Jester Shoes

        [Constructible]
        public JesterShoes()
            : this(0)
        {
        }

        [Constructible]
        public JesterShoes(int hue)
            : base(0x7819, hue)
        {
        }

        public JesterShoes(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
