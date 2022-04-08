using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class WrongPrisoner : BaseEscort
    {
        [Constructible]
        public WrongPrisoner()
            : base()
        {
            Title = "the prisoner";
            IsPrisoner = true;
            Female = false;
            Body = 0x190;
            Hue = 33802;
            Name = NameList.RandomName("male");

            SetWearable(new PlateChest());
            SetWearable(new PlateArms());
            SetWearable(new PlateGloves());
            SetWearable(new PlateLegs());
        }

        public WrongPrisoner(Serial serial)
            : base(serial)
        {
        }

        public override void InitOutfit() { }

        public override bool IsInvulnerable => !Controlled;

        public override Type[] Quests => new Type[]
                {
                    typeof(EscortToWrongEntrance)
                };

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
