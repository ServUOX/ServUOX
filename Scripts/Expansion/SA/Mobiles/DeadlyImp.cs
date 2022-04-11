using Server.Mobiles;

namespace Server.Engines.Quests.Samurai
{
    public class DeadlyImp : BaseCreature
    {
        [Constructible]
        public DeadlyImp()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a deadly imp";
            Body = 74;
            BaseSoundID = 422;
            Hue = 0x66A;

            SetStr(91, 115);
            SetDex(61, 80);
            SetInt(86, 105);

            SetHits(1000);

            SetDamage(50, 80);

            SetDamageType(ResistanceType.Fire, 100);

            SetResist(ResistanceType.Physical, 95, 98);
            SetResist(ResistanceType.Fire, 95, 98);
            SetResist(ResistanceType.Cold, 95, 98);
            SetResist(ResistanceType.Poison, 95, 98);
            SetResist(ResistanceType.Energy, 95, 98);

            SetSkill(SkillName.Magery, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Wrestling, 120.0);

            Fame = 2500;
            Karma = -2500;

            CantWalk = true;
        }

        public DeadlyImp(Serial serial)
            : base(serial)
        {
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            PlayerMobile player = aggressor as PlayerMobile;
            if (player != null)
            {
                QuestSystem qs = player.Quest;
                if (qs is HaochisTrialsQuest)
                {
                    QuestObjective obj = qs.FindObjective(typeof(SecondTrialAttackObjective));
                    if (obj != null && !obj.Completed)
                    {
                        obj.Complete();
                        qs.AddObjective(new SecondTrialReturnObjective(false));
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();
        }
    }
}
