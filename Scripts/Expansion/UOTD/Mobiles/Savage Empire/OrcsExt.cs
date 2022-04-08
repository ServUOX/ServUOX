using System;
using Server.Items;
using Server.Misc;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class OrcBomber : BaseCreature
    {
        private DateTime m_NextBomb;
        private int m_Thrown;
        [Constructible]
        public OrcBomber()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 182;

            Name = "an orc bomber";
            BaseSoundID = 0x45A;

            SetStr(147, 215);
            SetDex(91, 115);
            SetInt(61, 85);

            SetHits(95, 123);

            SetDamage(1, 8);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 15, 20);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 70.1, 85.0);
            SetSkill(SkillName.Swords, 60.1, 85.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 60.1, 85.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 30;
        }

        public OrcBomber(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new SulfurousAsh(Utility.RandomMinMax(6, 10)));
            CorpseLoot.DropItem(new MandrakeRoot(Utility.RandomMinMax(6, 10)));
            CorpseLoot.DropItem(new BlackPearl(Utility.RandomMinMax(6, 10)));
            CorpseLoot.DropItem(new MortarPestle());

            CorpseLoot.DropItem(new LesserExplosionPotion());

            if (Core.UOR && Utility.RandomDouble() < 0.2)
                CorpseLoot.DropItem(new BolaBall());

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override void OnActionCombat()
        {
            Mobile combatant = Combatant as Mobile;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 12) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;

            if (DateTime.UtcNow >= m_NextBomb)
            {
                ThrowBomb(combatant);

                m_Thrown++;

                if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(3.0);
                else
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(5.0 + (10.0 * Utility.RandomDouble())); // 5-15 seconds
            }
        }

        public void ThrowBomb(Mobile m)
        {
            DoHarmful(m);

            MovingParticles(m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

            new InternalTimer(m, this).Start();
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

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly Mobile m_From;
            public InternalTimer(Mobile m, Mobile from)
                : base(TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_From = from;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                m_Mobile.PlaySound(0x11D);
                AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(10, 20), 0, 100, 0, 0, 0);
            }
        }
    }

    [CorpseName("an orcish corpse")]
    public class OrcBrute : BaseCreature
    {
        [Constructible]
        public OrcBrute()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 189;

            Name = "an orc brute";
            BaseSoundID = 0x45A;

            SetStr(767, 945);
            SetDex(66, 75);
            SetInt(46, 70);

            SetHits(476, 552);

            SetDamage(20, 25);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Macing, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 125.1, 140.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 50;
        }

        public OrcBrute(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmunity => !Core.AOS;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int Meat => 2;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;
        public override bool CanRummageCorpses => true;
        public override bool AutoDispel => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new ShadowIronOre(25));

            CorpseLoot.DropItem(new IronIngot(10));

            if (Core.UOTD && Utility.RandomDouble() > 0.05)
                CorpseLoot.DropItem(new OrcishKinMask());

            if (Core.UOR && Utility.RandomDouble() < 0.2)
                CorpseLoot.DropItem(new BolaBall());

            if (Core.HS) CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            var damage = base.Damage(amount, from, informMount, checkDisrupt);

            if (from != null && from != this && !Controlled && !Summoned && Utility.RandomDouble() <= 0.2)
            {
                SpawnOrcLord(from);
            }

            return damage;
        }

        public void SpawnOrcLord(Mobile target)
        {
            Map map = target.Map;

            if (map == null)
                return;

            int orcs = 0;
            IPooledEnumerable eable = GetMobilesInRange(10);

            foreach (Mobile m in eable)
            {
                if (m is OrcishLord)
                    ++orcs;
            }

            eable.Free();

            if (orcs < 10)
            {
                BaseCreature orc = new SpawnedOrcishLord();

                orc.Team = Team;

                Point3D loc = target.Location;
                bool validLocation = false;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = target.X + Utility.Random(3) - 1;
                    int y = target.Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                orc.MoveToWorld(loc, map);

                orc.Combatant = target;
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
        }
    }

    [CorpseName("an orcish corpse")]
    public class OrcChopper : BaseCreature
    {
        [Constructible]
        public OrcChopper()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an orc chopper";
            Body = 7;
            BaseSoundID = 0x45A;
            Hue = 0x96D;

            SetStr(147, 245);
            SetDex(91, 115);
            SetInt(61, 85);

            SetHits(97, 139);

            SetDamage(4, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 60.1, 85.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 60.1, 85.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public OrcChopper(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            // TODO: Skull?
            switch (Utility.Random(7))
            {
                case 0:
                    CorpseLoot.DropItem(new Arrow());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Lockpick());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Shaft());
                    break;
                case 3:
                    CorpseLoot.DropItem(new Ribs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Bandage());
                    break;
                case 5:
                    CorpseLoot.DropItem(new BeverageBottle(BeverageType.Wine));
                    break;
                case 6:
                    CorpseLoot.DropItem(new Jug(BeverageType.Cider));
                    break;
            }

            CorpseLoot.DropItem(new Log(Utility.RandomMinMax(1, 10)));
            CorpseLoot.DropItem(new Board(Utility.RandomMinMax(10, 20)));
            CorpseLoot.DropItem(new ExecutionersAxe());

            if (Core.UOTD && Utility.RandomDouble() < 0.1)
                CorpseLoot.DropItem(new EvilOrcHelm());

            if (Core.AOS)
                CorpseLoot.DropItem(Loot.RandomNecromancyReagent());

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
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
        }
    }

    [CorpseName("an orcish corpse")]
    public class OrcScout : BaseCreature
    {
        public override double HealChance => 1.0;

        [Constructible]
        public OrcScout()
            : base(AIType.AI_OrcScout, FightMode.Closest, 10, 7, 0.2, 0.4)
        {
            Name = "an orc scout";
            Body = 0xB5;
            BaseSoundID = 0x45A;

            SetStr(96, 120);
            SetDex(101, 130);
            SetInt(36, 60);

            SetHits(58, 72);
            SetMana(30, 60);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 15, 20);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);

            SetSkill(SkillName.Fencing, 50.1, 70.0);
            SetSkill(SkillName.Archery, 80.1, 120.0);
            SetSkill(SkillName.Parry, 40.1, 60.0);
            SetSkill(SkillName.Healing, 80.1, 100.0);
            SetSkill(SkillName.Anatomy, 50.1, 90.0);
            SetSkill(SkillName.DetectHidden, 100.1, 120.0);
            SetSkill(SkillName.Hiding, 100.0, 120.0);
            SetSkill(SkillName.Stealth, 80.1, 120.0);

            Fame = 1500;
            Karma = -1500;
        }

        public OrcScout(Serial serial)
            : base(serial)
        { }

        public override bool CanRummageCorpses => true;
        public override bool CanStealth => true;
        public override int Meat => 1;

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;
        public override TribeType Tribe => TribeType.Orc;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new Apple(Utility.RandomMinMax(3, 5)));
            CorpseLoot.DropItem(new Arrow(Utility.RandomMinMax(60, 70)));
            CorpseLoot.DropItem(new Bandage(Utility.RandomMinMax(1, 15)));

            if (Utility.RandomDouble() < 0.1)
            {
                CorpseLoot.DropItem(new OrcishBow());
            }
            else
            {
                CorpseLoot.DropItem(new Bow());
            }

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
            {
                return false;
            }

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override void OnThink()
        {
            if (Utility.RandomDouble() < 0.2)
            {
                TryToDetectHidden();
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
        }

        private Mobile FindTarget()
        {
            IPooledEnumerable eable = GetMobilesInRange(10);
            foreach (Mobile m in eable)
            {
                if (m.Player && m.Hidden && m.IsPlayer())
                {
                    eable.Free();
                    return m;
                }
            }

            eable.Free();
            return null;
        }

        private void TryToDetectHidden()
        {
            Mobile m = FindTarget();

            if (m != null)
            {
                if (Core.TickCount >= NextSkillTime && UseSkill(SkillName.DetectHidden))
                {
                    Target targ = Target;

                    if (targ != null)
                    {
                        targ.Invoke(this, this);
                    }

                    Effects.PlaySound(Location, Map, 0x340);
                }
            }
        }
    }
}
