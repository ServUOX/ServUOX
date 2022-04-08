using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Aliabeth : MondainQuester
    {
        public override void InitSBInfo()
        {
            SBInfos.Add(new SBTinker(this));
        }

        [Constructible]
        public Aliabeth()
            : base("Aliabeth", "the Tinker")
        {
            SetSkill(SkillName.Lockpicking, 60.0, 83.0);
            SetSkill(SkillName.RemoveTrap, 75.0, 98.0);
            SetSkill(SkillName.Tinkering, 64.0, 100.0);
        }

        public Aliabeth(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(AWorthyPropositionQuest),
                    typeof(TheExchangeQuest),
                };
        public override void InitBody()
        {
            Female = true;
            //this.CantWalk = true;
            Race = Race.Human;

            base.InitBody();
        }
        public override void InitOutfit()
        {
            AddItem(new Backpack());

            AddItem(new Kilt(Utility.RandomNeutralHue()));
            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new Sandals());
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
