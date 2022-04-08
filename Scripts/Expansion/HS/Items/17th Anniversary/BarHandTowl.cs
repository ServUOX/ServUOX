using System;

namespace Server.Items
{
    [Furniture]
    public class BarHandTowl : Item
    {
        public override int LabelNumber { get { return 1155421; } }/*An Ornately Embroidered Hand Towel*/
        private static string[] HandTowlNames = new string[]
	    {
				"Mariah",
				"Geoffrey",
				"Katrina",
				"Jaana",
				"Shamino",
				"Dupre",
				"Blackthorn",
				"Dawn",
				"Iolo",
				"Minax",
				"Mondain",
				"Gwenno",
				"Julia",
				"Hawkwind",
				"Nystul",
				"Anon",
				"Clainin",
				"Relvinian",
				"Exodus",
				"Captain Johne",
				"Astaroth",
				"Faulinei",
				"Nosfentor"
	    };

        [Constructible]
        public BarHandTowl()
            : base(0x9C48)
        {
            Weight = 1.0;
        }

        public BarHandTowl(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            AddNameProperty(list);

            list.Add(HandTowlNames[Utility.Random(HandTowlNames.Length)]);

            if (DisplayWeight)
                AddWeightProperty(list);

            if (IsSecure)
                AddSecureProperty(list);
            else if (IsLockedDown)
                AddLockedDownProperty(list);

            if (DisplayLootType)
                AddLootTypeProperty(list);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 15.0)
                Weight = 2.0;
        }
    }
}