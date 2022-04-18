using System;
using System.Linq;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a monstrous interred grizzle corpse")]
    public class MonstrousInterredGrizzle : BasePeerless
    {
        [Constructible]
        public MonstrousInterredGrizzle()
            : base(AIType.AI_Spellweaving, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a monstrous interred grizzle";
            Body = 0x103;

            SetStr(1198, 1207);
            SetDex(127, 135);
            SetInt(595, 646);

            SetHits(50000);

            SetDamage(27, 31);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 48, 52);
            SetResist(ResistType.Fire, 77, 82);
            SetResist(ResistType.Cold, 56, 61);
            SetResist(ResistType.Pois, 32, 40);
            SetResist(ResistType.Engy, 69, 71);

            SetSkill(SkillName.Wrestling, 112.6, 116.9);
            SetSkill(SkillName.Tactics, 118.5, 119.2);
            SetSkill(SkillName.MagicResist, 120);
            SetSkill(SkillName.Anatomy, 111.0, 111.7);
            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.EvalInt, 100);
            SetSkill(SkillName.Meditation, 100);
            SetSkill(SkillName.Spellweaving, 100.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 80;

            PackResources(8);
            PackTalismans(5);

            SetSpecialAbility(SpecialAbility.HowlOfCacophony);
        }

        public MonstrousInterredGrizzle(Serial serial)
            : base(serial)
        {
        }

        public override bool GivesMLMinorArtifact => true;
        public override int TreasureMapLevel => 5;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosSuperBoss, 8);
        }

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(1, 6); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            c.DropItem(new GrizzledBones());

            switch (Utility.Random(4))
            {
                case 0:
                    c.DropItem(new TombstoneOfTheDamned());
                    break;
                case 1:
                    c.DropItem(new GlobOfMonstreousInterredGrizzle());
                    break;
                case 2:
                    c.DropItem(new MonsterousInterredGrizzleMaggots());
                    break;
                case 3:
                    c.DropItem(new GrizzledSkullCollection());
                    break;
                default:
                    c.DropItem(new GrizzledSkullCollection());
                    break;
            }

            if (Utility.RandomDouble() < 0.6)
                c.DropItem(new ParrotItem());

            if (Utility.RandomDouble() < 0.05)
                c.DropItem(new GrizzledMareStatuette());

            if (Utility.RandomDouble() < 0.05)
            {
                switch (Utility.Random(5))
                {
                    case 0:
                        c.DropItem(new GrizzleGauntlets());
                        break;
                    case 1:
                        c.DropItem(new GrizzleGreaves());
                        break;
                    case 2:
                        c.DropItem(new GrizzleHelm());
                        break;
                    case 3:
                        c.DropItem(new GrizzleTunic());
                        break;
                    case 4:
                        c.DropItem(new GrizzleVambraces());
                        break;
                    default:
                        c.DropItem(new GrizzleHelm());
                        break;
                }
            }

            base.OnDeath(c);
        }

        public override int GetDeathSound() => 0x580;
        public override int GetAttackSound() => 0x581;
        public override int GetIdleSound() => 0x582;
        public override int GetAngerSound() => 0x583;
        public override int GetHurtSound() => 0x584;

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

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (Utility.RandomDouble() < 0.06)
            {
                PlaySound(0x585);
                SpillAcid(null, Utility.RandomMinMax(1, 3));
            }

            base.OnDamage(amount, from, willKill);
        }

        public override Item NewHarmfulItem()
        {
            return new InfernalOoze(this, Utility.RandomBool());
        }
    }

    public class InfernalOoze : Item
    {
        private readonly int m_Damage;
        private readonly Mobile m_Owner;
        private Timer m_Timer;

        private readonly DateTime m_StartTime;

        public InfernalOoze(Mobile owner)
            : this(owner, false)
        {
        }

        [Constructible]
        public InfernalOoze(Mobile owner, bool corrosive, int damage = 40)
            : base(0x122A)
        {
            Movable = false;
            m_Owner = owner;
            Hue = 0x95;

            m_Damage = damage;

            Corrosive = corrosive;
            m_StartTime = DateTime.UtcNow;

            m_Timer = Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), OnTick);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Corrosive { get; set; }

        private void OnTick()
        {
            if (ItemID == 0x122A && m_StartTime + TimeSpan.FromSeconds(30) < DateTime.UtcNow)
            {
                ItemID++;
            }
            else if (m_StartTime + TimeSpan.FromSeconds(35) < DateTime.UtcNow)
            {
                Delete();
                return;
            }

            if (m_Owner == null)
                return;

            if (!Deleted && Map != Map.Internal && Map != null)
            {
                foreach (var m in SpellHelper.AcquireIndirectTargets(m_Owner, Location, Map, 0).OfType<Mobile>())
                {
                    OnMoveOver(m);
                }
            }
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (Map == null)
                return base.OnMoveOver(m);

            if ((m is BaseCreature creature && creature.GetMaster() is PlayerMobile) || m.Player)
            {
                Damage(m);
            }

            return base.OnMoveOver(m);
        }

        public virtual void Damage(Mobile m)
        {
            if (Corrosive)
            {
                for (int i = 0; i < m.Items.Count; i++)
                {
                    if (m.Items[i] is IDurability item && Utility.RandomDouble() < 0.25)
                    {
                        if (item.HitPoints > 10)
                            item.HitPoints -= 10;
                        else
                            item.HitPoints -= 1;
                    }
                }
            }
            else
            {
                int dmg = m_Damage;

                if (m is PlayerMobile)
                {
                    PlayerMobile pm = m as PlayerMobile;
                    dmg = (int)BalmOfProtection.HandleDamage(pm, dmg);
                    AOS.Damage(m, m_Owner, dmg, 0, 0, 0, 100, 0);
                }
                else
                {
                    AOS.Damage(m, m_Owner, dmg, 0, 0, 0, 100, 0);
                }
            }
        }

        public override void Delete()
        {
            base.Delete();

            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }
        }

        public InfernalOoze(Serial serial)
            : base(serial)
        {
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

            Delete();
        }
    }
}
