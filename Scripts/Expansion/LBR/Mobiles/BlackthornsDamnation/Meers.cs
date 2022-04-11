using System;
using System.Collections;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a meer corpse")]
    public class MeerCaptain : BaseCreature
    {
        private DateTime m_NextAbilityTime;
        [Constructible]
        public MeerCaptain()
            : base(AIType.AI_Paladin, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = "a meer captain";
            Body = 773;

            SetStr(96, 110);
            SetDex(186, 200);
            SetInt(96, 110);

            SetHits(58, 66);

            SetDamage(5, 15);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 45, 55);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Poison, 35, 45);
            SetResist(ResistType.Energy, 35, 45);

            SetSkill(SkillName.Archery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 91.0, 100.0);
            SetSkill(SkillName.Swords, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 91.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.9, 89.9);

            Fame = 2000;
            Karma = 5000;

            VirtualArmor = 28;

            m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));

            AddItem(new Crossbow());
            AddItem(new Bolt(Utility.RandomMinMax(15, 25)));
        }

        public MeerCaptain(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override bool InitialInnocent => true;

        public override void OnDeath(Container CorpseLoot)
        {
            Container bag = new Bag();

            int count = Utility.RandomMinMax(10, 20);

            for (int i = 0; i < count; ++i)
            {
                Item item = Loot.RandomReagent();

                if (item == null)
                    continue;

                if (!bag.TryDropItem(this, item, false))
                    item.Delete();
            }

            CorpseLoot.DropItem(bag);

            switch (Utility.Random(6))
            {
                case 0:
                    CorpseLoot.DropItem(new Broadsword());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Cutlass());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Katana());
                    break;
                case 3:
                    CorpseLoot.DropItem(new Longsword());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Scimitar());
                    break;
                case 5:
                    CorpseLoot.DropItem(new VikingSword());
                    break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override int GetHurtSound() { return 0x14D; }
        public override int GetDeathSound() { return 0x314; }
        public override int GetAttackSound() { return 0x75; }

        public override void OnThink()
        {
            if (Combatant != null && MagicDamageAbsorb < 1)
            {
                MagicDamageAbsorb = Utility.RandomMinMax(5, 7);
                FixedParticles(0x375A, 10, 15, 5037, EffectLayer.Waist);
                PlaySound(0x1E9);
            }

            if (DateTime.UtcNow >= m_NextAbilityTime)
            {
                m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 15));

                ArrayList list = new ArrayList();
                IPooledEnumerable eable = GetMobilesInRange(8);

                foreach (Mobile m in eable)
                {
                    if (m is MeerWarrior && IsFriend(m) && CanBeBeneficial(m) && m.Hits < m.HitsMax && !m.Poisoned && !MortalStrike.IsWounded(m))
                        list.Add(m);
                }
                eable.Free();

                for (int i = 0; i < list.Count; ++i)
                {
                    Mobile m = (Mobile)list[i];

                    DoBeneficial(m);

                    int toHeal = Utility.RandomMinMax(20, 30);

                    SpellHelper.Turn(this, m);

                    m.Heal(toHeal, this);

                    m.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
                    m.PlaySound(0x202);
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

    [CorpseName("a meer's corpse")]
    public class MeerEternal : BaseCreature
    {
        private DateTime m_NextAbilityTime;
        [Constructible]
        public MeerEternal()
            : base(AIType.AI_Spellweaving, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a meer eternal";
            Body = 772;

            SetStr(416, 505);
            SetDex(146, 165);
            SetInt(566, 655);

            SetHits(250, 303);

            SetDamage(11, 13);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 45, 55);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 150.5, 200.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);
            SetSkill(SkillName.Spellweaving, 90.1, 100.0);

            Fame = 18000;
            Karma = 18000;

            VirtualArmor = 34;

            m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
        }

        public MeerEternal(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int TreasureMapLevel => Core.AOS ? 5 : 4;
        public override bool InitialInnocent => true;

        public override int GetHurtSound() { return 0x167; }
        public override int GetDeathSound() { return 0xBC; }
        public override int GetAttackSound() { return 0x28B; }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(12))
            {
                case 0: CorpseLoot.DropItem(new StrangleScroll()); break;
                case 1: CorpseLoot.DropItem(new WitherScroll()); break;
                case 2: CorpseLoot.DropItem(new VampiricEmbraceScroll()); break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 2);
        }

        public override void OnThink()
        {
            if (DateTime.UtcNow >= m_NextAbilityTime)
            {
                Mobile combatant = Combatant as Mobile;

                if (combatant != null && combatant.Map == Map && combatant.InRange(this, 12))
                {
                    m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 15));

                    int ability = Utility.Random(4);

                    switch (ability)
                    {
                        case 0:
                            DoFocusedLeech(combatant, "Thine essence will fill my withering body with strength!");
                            break;
                        case 1:
                            DoFocusedLeech(combatant, "I rebuke thee, worm, and cleanse thy vile spirit of its tainted blood!");
                            break;
                        case 2:
                            DoFocusedLeech(combatant, "I devour your life's essence to strengthen my resolve!");
                            break;
                        case 3:
                            DoAreaLeech();
                            break;
                            // TODO: Resurrect ability
                    }
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

        private void DoAreaLeech()
        {
            m_NextAbilityTime += TimeSpan.FromSeconds(2.5);

            Say(true, "Beware, mortals!  You have provoked my wrath!");
            FixedParticles(0x376A, 10, 10, 9537, 33, 0, EffectLayer.Waist);

            Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(DoAreaLeech_Finish));
        }

        private void DoAreaLeech_Finish()
        {
            ArrayList list = new ArrayList();
            IPooledEnumerable eable = GetMobilesInRange(6);

            foreach (Mobile m in eable)
            {
                if (CanBeHarmful(m) && IsEnemy(m))
                    list.Add(m);
            }
            eable.Free();

            if (list.Count == 0)
            {
                Say(true, "Bah! You have escaped my grasp this time, mortal!");
            }
            else
            {
                double scalar;

                if (list.Count == 1)
                    scalar = 0.75;
                else if (list.Count == 2)
                    scalar = 0.50;
                else
                    scalar = 0.25;

                for (int i = 0; i < list.Count; ++i)
                {
                    Mobile m = (Mobile)list[i];

                    int damage = (int)(m.Hits * scalar);

                    damage += Utility.RandomMinMax(-5, 5);

                    if (damage < 1)
                        damage = 1;

                    m.MovingParticles(this, 0x36F4, 1, 0, false, false, 32, 0, 9535, 1, 0, (EffectLayer)255, 0x100);
                    m.MovingParticles(this, 0x0001, 1, 0, false, true, 32, 0, 9535, 9536, 0, (EffectLayer)255, 0);

                    DoHarmful(m);
                    Hits += AOS.Damage(m, this, damage, 100, 0, 0, 0, 0);
                }

                Say(true, "If I cannot cleanse thy soul, I will destroy it!");
            }
        }

        private void DoFocusedLeech(Mobile combatant, string message)
        {
            Say(true, message);

            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerStateCallback(DoFocusedLeech_Stage1), combatant);
        }

        private void DoFocusedLeech_Stage1(object state)
        {
            Mobile combatant = (Mobile)state;

            if (CanBeHarmful(combatant))
            {
                MovingParticles(combatant, 0x36FA, 1, 0, false, false, 1108, 0, 9533, 1, 0, (EffectLayer)255, 0x100);
                MovingParticles(combatant, 0x0001, 1, 0, false, true, 1108, 0, 9533, 9534, 0, (EffectLayer)255, 0);
                PlaySound(0x1FB);

                Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(DoFocusedLeech_Stage2), combatant);
            }
        }

        private void DoFocusedLeech_Stage2(object state)
        {
            Mobile combatant = (Mobile)state;

            if (CanBeHarmful(combatant))
            {
                combatant.MovingParticles(this, 0x36F4, 1, 0, false, false, 32, 0, 9535, 1, 0, (EffectLayer)255, 0x100);
                combatant.MovingParticles(this, 0x0001, 1, 0, false, true, 32, 0, 9535, 9536, 0, (EffectLayer)255, 0);

                PlaySound(0x209);
                DoHarmful(combatant);
                Hits += AOS.Damage(combatant, this, Utility.RandomMinMax(30, 40) - (Core.AOS ? 0 : 10), 100, 0, 0, 0, 0);
            }
        }
    }

    [CorpseName("a meer's corpse")]
    public class MeerMage : BaseCreature
    {
        private static readonly Hashtable m_Table = new Hashtable();
        private DateTime m_NextAbilityTime;
        [Constructible]
        public MeerMage()
            : base(AIType.AI_Spellweaving, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a meer mage";
            Body = 770;

            SetStr(171, 200);
            SetDex(126, 145);
            SetInt(276, 305);

            SetHits(103, 120);

            SetDamage(24, 26);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 45, 55);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 50);
            SetResist(ResistType.Poison, 25, 35);
            SetResist(ResistType.Energy, 25, 35);

            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 80.1, 100.0);
            SetSkill(SkillName.Tactics, 70.1, 90.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);
            SetSkill(SkillName.Spellweaving, 70.1, 80.0);

            Fame = 8000;
            Karma = 8000;

            VirtualArmor = 16;

            m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
        }

        public MeerMage(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 3;
        public override bool InitialInnocent => true;
        public static bool UnderEffect(Mobile m)
        {
            return m_Table.Contains(m);
        }

        public static void StopEffect(Mobile m, bool message)
        {
            Timer t = (Timer)m_Table[m];

            if (t != null)
            {
                if (message)
                    m.PublicOverheadMessage(Network.MessageType.Emote, m.SpeechHue, true, "* The open flame begins to scatter the swarm of insects *");

                t.Stop();
                m_Table.Remove(m);
            }
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(12))
            {
                case 0: CorpseLoot.DropItem(new StrangleScroll()); break;
                case 1: CorpseLoot.DropItem(new WitherScroll()); break;
                case 2: CorpseLoot.DropItem(new VampiricEmbraceScroll()); break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.MedScrolls, 2);
            // TODO: Daemon bone ...
        }

        public override int GetHurtSound() { return 0x14D; }
        public override int GetDeathSound() { return 0x314; }
        public override int GetAttackSound() { return 0x75; }

        public override void OnThink()
        {
            if (DateTime.UtcNow >= m_NextAbilityTime)
            {
                Mobile combatant = Combatant as Mobile;

                if (combatant != null && combatant.Map == Map && combatant.InRange(this, 12) && IsEnemy(combatant) && !UnderEffect(combatant))
                {
                    m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(20, 30));

                    if (combatant is BaseCreature)
                    {
                        BaseCreature bc = (BaseCreature)combatant;

                        if (bc.Controlled && bc.ControlMaster != null && !bc.ControlMaster.Deleted && bc.ControlMaster.Alive)
                        {
                            if (bc.ControlMaster.Map == Map && bc.ControlMaster.InRange(this, 12) && !UnderEffect(bc.ControlMaster))
                            {
                                Combatant = combatant = bc.ControlMaster;
                            }
                        }
                    }

                    if (Utility.RandomDouble() < .1)
                    {
                        int[][] coord =
                        {
                            new int[] { -4, -6 }, new int[] { 4, -6 }, new int[] { 0, -8 }, new int[] { -5, 5 }, new int[] { 5, 5 }
                        };

                        BaseCreature rabid;

                        for (int i = 0; i < 5; i++)
                        {
                            int x = combatant.X + coord[i][0];
                            int y = combatant.Y + coord[i][1];

                            Point3D loc = new Point3D(x, y, combatant.Map.GetAverageZ(x, y));

                            if (!combatant.Map.CanSpawnMobile(loc))
                                continue;

                            switch (i)
                            {
                                case 0:
                                    rabid = new EnragedRabbit(this);
                                    break;
                                case 1:
                                    rabid = new EnragedHind(this);
                                    break;
                                case 2:
                                    rabid = new EnragedHart(this);
                                    break;
                                case 3:
                                    rabid = new EnragedBlackBear(this);
                                    break;
                                default:
                                    rabid = new EnragedEagle(this);
                                    break;
                            }

                            rabid.FocusMob = combatant;
                            rabid.MoveToWorld(loc, combatant.Map);
                        }
                        Say(1071932); //Creatures of the forest, I call to thee!  Aid me in the fight against all that is evil!
                    }
                    else if (combatant.Player)
                    {
                        Say(true, "I call a plague of insects to sting your flesh!");
                        m_Table[combatant] = Timer.DelayCall(TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(7.0), new TimerStateCallback(DoEffect), new object[] { combatant, 0 });
                    }
                }
            }

            base.OnThink();
        }

        public void DoEffect(object state)
        {
            object[] states = (object[])state;

            Mobile m = (Mobile)states[0];
            int count = (int)states[1];

            if (!m.Alive)
            {
                StopEffect(m, false);
            }
            else
            {
                Torch torch = m.FindItemOnLayer(Layer.TwoHanded) as Torch;

                if (torch != null && torch.Burning)
                {
                    StopEffect(m, true);
                }
                else
                {
                    if ((count % 4) == 0)
                    {
                        m.LocalOverheadMessage(Network.MessageType.Emote, m.SpeechHue, true, "* The swarm of insects bites and stings your flesh! *");
                        m.NonlocalOverheadMessage(Network.MessageType.Emote, m.SpeechHue, true, string.Format("* {0} is stung by a swarm of insects *", m.Name));
                    }

                    m.FixedParticles(0x91C, 10, 180, 9539, EffectLayer.Waist);
                    m.PlaySound(0x00E);
                    m.PlaySound(0x1BC);

                    AOS.Damage(m, this, Utility.RandomMinMax(30, 40) - (Core.AOS ? 0 : 10), 100, 0, 0, 0, 0);

                    states[1] = count + 1;

                    if (!m.Alive)
                        StopEffect(m, false);
                }
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

    [CorpseName("a meer corpse")]
    public class MeerWarrior : BaseCreature
    {
        [Constructible]
        public MeerWarrior()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a meer warrior";
            Body = 771;

            SetStr(86, 100);
            SetDex(186, 200);
            SetInt(86, 100);

            SetHits(52, 60);

            SetDamage(12, 19);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 35, 45);
            SetResist(ResistType.Fire, 5, 15);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Poison, 25, 35);
            SetResist(ResistType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 91.0, 100.0);
            SetSkill(SkillName.Tactics, 91.0, 100.0);
            SetSkill(SkillName.Wrestling, 91.0, 100.0);

            VirtualArmor = 22;

            Fame = 2000;
            Karma = 5000;
        }

        public MeerWarrior(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override bool InitialInnocent => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != null && !willKill && amount > 3 && !InRange(from, 7))
            {
                MovingEffect(from, 0xF51, 10, 0, false, false);
                SpellHelper.Damage(TimeSpan.FromSeconds(1.0), from, this, Utility.RandomMinMax(30, 40) - (Core.AOS ? 0 : 10), 100, 0, 0, 0, 0);
            }

            base.OnDamage(amount, from, willKill);
        }

        public override int GetHurtSound() { return 0x156; }
        public override int GetDeathSound() { return 0x15C; }

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
