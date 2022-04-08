using System;
using Server.Items;

namespace Server.Mobiles
{
    public class GargishNoble : BaseEscortable
    {
        [Constructible]
        public GargishNoble() //: base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
            Title = "a Gargish noble";

            SetSkill(SkillName.Parry, 80.0, 100.0);
            SetSkill(SkillName.Swords, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 80.0, 100.0);

            if (Female = Utility.RandomBool())
            {
                Body = 667;
                HairItemID = 17067;
                HairHue = 1762;
                AddItem(new GargishClothChest(Utility.RandomNeutralHue()));
                AddItem(new GargishClothKilt(Utility.RandomNeutralHue()));
            }
            else
            {
                Body = 666;
                HairItemID = 16987;
                HairHue = 1801;
                AddItem(new GargishClothChest(Utility.RandomNeutralHue()));
                AddItem(new GargishClothKilt(Utility.RandomNeutralHue()));
                AddItem(new GargishClothLegs(Utility.RandomNeutralHue()));

                PackItem(Loot.PackGold(200, 250));
            }
        }

        public GargishNoble(Serial serial)
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
