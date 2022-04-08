using Server.Multis;
using System.Linq;

namespace Server.Items
{
    [Flipable(0x4C26, 0x4C27)]
    public class DecorativeWoodCarving : Item
    {
        public string _ShipName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string ShipName { get => _ShipName; set { _ShipName = value; InvalidateProperties(); } }

        [Constructible]
        public DecorativeWoodCarving()
            : base(0x4C26)
        {
            Hue = 2968;
        }

        public void AssignRandomName()
        {
            var list = BaseBoat.Boats.Where(b => !string.IsNullOrEmpty(b.ShipName)).Select(x => x.ShipName).ToList();

            if (list.Count > 0)
            {
                _ShipName = list[Utility.Random(list.Count)];
            }
            else
            {
                _ShipName = _ShipNames[Utility.Random(_ShipNames.Length)];
            }

            InvalidateProperties();
            ColUtility.Free(list);
        }

        private static string[] _ShipNames =
        {
            "Adventure Galley",
            "Queen Anne's Revenge",
            "Fancy",
            "Whydah",
            "Royal Fortune",
            "The Black Pearl",
            "Satisfaction",
            "The Golden Fleece",
            "Bachelor's Delight",
            "The Revenge",
            "The Flying Dragon",
            "The Gabriel",
            "Privateer's Death",
            "Kiss of Death",
            "Devil's Doom",
            "Monkeebutt",
            "Mourning Star",
            "Cursed Sea-Dog",
            "The Howling Lusty Wench",
            "Scourage of the Seven Seas",
            "Neptune's Plague",
            "Sea's Hellish Plague",
            "The Salty Bastard"
        };

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (string.IsNullOrEmpty(_ShipName))
            {
                list.Add(1158943); // Wood Carving of [Ship's Name]
            }
            else
            {
                list.Add(1158921, _ShipName); // Wood Carving of ~1_name~
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (string.IsNullOrEmpty(_ShipName))
            {
                list.Add(1158953); // Named with a random famous ship, or if yer lucky - named after you!
            }
        }

        public DecorativeWoodCarving(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(_ShipName);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();

            _ShipName = reader.ReadString();
        }
    }
}
