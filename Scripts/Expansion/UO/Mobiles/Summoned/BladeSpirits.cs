using System;
using System.Collections;

namespace Server.Mobiles
{
    [CorpseName("a blade spirit corpse")]
    public class BladeSpirits : BaseCreature
    {
        [Constructible]
        public BladeSpirits()
            : this(false)
        {
        }

        [Constructible]
        public BladeSpirits(bool summoned)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6)
        {
            Name = "a blade spirit";
            Body = 574;

            bool weak = summoned && Siege.SiegeShard;

            SetStr(weak ? 100 : 150);
            SetDex(weak ? 100 : 150);
            SetInt(100);

            SetHits((Core.SE && !weak) ? 160 : 80);
            SetStam(250);
            SetMana(0);

            SetDamage(10, 14);

            SetDamageType(ResistType.Physical, 60);
            SetDamageType(ResistType.Poison, 20);
            SetDamageType(ResistType.Energy, 20);

            SetResist(ResistType.Physical, 30, 40);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Poison, 100);
            SetResist(ResistType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 70.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Wrestling, 90.0);

            Fame = 0;
            Karma = 0;

            VirtualArmor = 40;
            ControlSlots = (Core.SE) ? 2 : 1;
        }

        public BladeSpirits(Serial serial)
            : base(serial)
        {
        }

        public override bool DeleteCorpseOnDeath => Core.AOS;
        public override bool IsHouseSummonable => true;
        public override double DispelDifficulty => 0.0;
        public override double DispelFocus => 20.0;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;

        public override double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
        {
            return (m.Str + m.Skills[SkillName.Tactics].Value) / Math.Max(GetDistanceToSqrt(m), 1.0);
        }

        public override int GetAngerSound()
        {
            return 0x23A;
        }

        public override int GetAttackSound()
        {
            return 0x3B8;
        }

        public override int GetHurtSound()
        {
            return 0x23A;
        }

        public override void OnThink()
        {
            if (Core.SE && Summoned)
            {
                ArrayList spirtsOrVortexes = new ArrayList();
                IPooledEnumerable eable = GetMobilesInRange(5);

                foreach (Mobile m in eable)
                {
                    if (m is EnergyVortex || m is BladeSpirits)
                    {
                        if (((BaseCreature)m).Summoned)
                            spirtsOrVortexes.Add(m);
                    }
                }

                eable.Free();

                while (spirtsOrVortexes.Count > 6)
                {
                    int index = Utility.Random(spirtsOrVortexes.Count);
                    Dispel(((Mobile)spirtsOrVortexes[index]));
                    spirtsOrVortexes.RemoveAt(index);
                }
            }

            base.OnThink();
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
