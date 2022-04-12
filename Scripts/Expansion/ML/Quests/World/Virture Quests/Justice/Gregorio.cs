using Server.Engines.Quests;
using Server.Items;

namespace Server.Mobiles
{
    public class Gregorio : BaseCreature
    {
        [Constructible]
        public Gregorio()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Race = Race.Human;
            Name = "Gregorio";
            Title = "the Brigand";

            InitBody();
            InitOutfit();

            SetStr(86, 100);
            SetDex(81, 95);
            SetInt(61, 75);

            SetDamage(15, 27);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 10, 15);
            SetResist(ResistType.Fire, 10, 15);
            SetResist(ResistType.Pois, 10, 15);
            SetResist(ResistType.Engy, 10, 15);

            SetSkill(SkillName.MagicResist, 25.0, 50.0);
            SetSkill(SkillName.Tactics, 80.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.0, 100.0);

            PackItem(Loot.PackGold(50, 150));
        }

        public Gregorio(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;

        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            if (from is BaseCreature && ((BaseCreature)from).GetMaster() is PlayerMobile)
            {
                from = ((BaseCreature)from).GetMaster();
            }

            if (from is PlayerMobile pm)
            {
                BaseQuest quest = QuestHelper.GetQuest(pm, typeof(GuiltyQuest));

                if (quest != null && !quest.Completed)
                {
                    return base.Damage(amount, from, informMount, checkDisrupt);
                }
            }

            from.SendLocalizedMessage(1075456); // You are not allowed to damage this NPC unless your on the Guilty Quest
            return 0;
        }

        public override bool CanBeHarmedBy(Mobile from, bool message)
        {
            if (from is BaseCreature && ((BaseCreature)from).GetMaster() is PlayerMobile)
            {
                from = ((BaseCreature)from).GetMaster();
            }

            if (from is PlayerMobile pm)
            {
                BaseQuest quest = QuestHelper.GetQuest(pm, typeof(GuiltyQuest));

                if (quest != null && !quest.Completed)
                {
                    return base.CanBeHarmedBy(from, message);
                }
            }

            from.SendLocalizedMessage(1075456); // You are not allowed to damage this NPC unless your on the Guilty Quest
            return false;
        }

        public void InitBody()
        {
            InitStats(100, 100, 25);

            Hue = 0x8412;
            Female = false;

            HairItemID = 0x203C;
            HairHue = 0x47A;
            FacialHairItemID = 0x204D;
            FacialHairHue = 0x47A;
        }

        public void InitOutfit()
        {
            AddItem(new Sandals(0x75E));
            AddItem(new Shirt());
            AddItem(new ShortPants(0x66C));
            AddItem(new SkullCap(0x649));
            AddItem(new Pitchfork());
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
