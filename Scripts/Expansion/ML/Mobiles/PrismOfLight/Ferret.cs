using System;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    [CorpseName("a ferret corpse")]
    public class Ferret : BaseCreature
    {
        private static readonly string[] m_Vocabulary = new string[]
        {
            "dook",
            "dook dook",
            "dook dook dook!"
        };

        private bool m_CanTalk;

        [Constructible]
        public Ferret()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a ferret";
            Body = 0x117;

            SetStr(41, 48);
            SetDex(55);
            SetInt(75);

            SetHits(45, 50);

            SetDamage(ResistType.Phys, 100, 0, 7, 9);

            SetResist(ResistType.Phys, 45, 50);
            SetResist(ResistType.Fire, 10, 14);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 21, 25);
            SetResist(ResistType.Engy, 20, 25);

            SetSkill(SkillName.MagicResist, 4.0);
            SetSkill(SkillName.Tactics, 4.0);
            SetSkill(SkillName.Wrestling, 4.0);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -21.3;

            m_CanTalk = true;
        }

        public Ferret(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.Fish;

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m is Ferret ferret && m.InRange(this, 3) && m.Alive)
                Talk(ferret);
        }

        public void Talk()
        {
            Talk(null);
        }

        public void Talk(Ferret to)
        {
            if (m_CanTalk)
            {
                if (to != null)
                    QuestSystem.FocusTo(this, to);

                Say(m_Vocabulary[Utility.Random(m_Vocabulary.Length)]);

                if (to != null && Utility.RandomBool())
                    Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(5, 8)), new TimerCallback(delegate () { to.Talk(); }));

                m_CanTalk = false;

                Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(20, 30)), new TimerCallback(delegate () { m_CanTalk = true; }));
            }
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

            m_CanTalk = true;
        }
    }
}
