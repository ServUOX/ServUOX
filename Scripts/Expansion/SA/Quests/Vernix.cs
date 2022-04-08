using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Vernix : MondainQuester
    {
        [Constructible]
        public Vernix()
            : base("Vernix")
        {
        }

        public Vernix(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(UntanglingTheWebQuest),
                    typeof(GreenWithEnvyQuest),
                };

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Body = 723;

            Frozen = true;
            Direction = Direction.East;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
        }

        public override void Advertise()
        {
            Say(Utility.RandomBool() ? 1095119 : 1095051);
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

            Frozen = true;
            Direction = Direction.East;
        }
    }
}
