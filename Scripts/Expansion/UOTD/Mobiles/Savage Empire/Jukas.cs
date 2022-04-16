using System;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a juka corpse")]
    public class JukaLord : BaseCreature
    {
        public override double HealChance => 1.0;

        [Constructible]
        public JukaLord()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = "a juka lord";
            Body = 766;

            SetStr(401, 500);
            SetDex(81, 100);
            SetInt(151, 200);

            SetHits(241, 300);

            SetDamage(ResistType.Phys, 100, 0, 10, 12);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 45, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 20, 25);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Anatomy, 90.1, 100.0);
            SetSkill(SkillName.Archery, 95.1, 100.0);
            SetSkill(SkillName.Healing, 80.1, 100.0);
            SetSkill(SkillName.MagicResist, 120.1, 130.0);
            SetSkill(SkillName.Swords, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 95.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 28;

            AddItem(new JukaBow());
            AddItem(new Arrow(Utility.RandomMinMax(15, 25)));
        }

        public JukaLord(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;

        public override int GetIdleSound() { return 0x262; }
        public override int GetAngerSound() { return 0x263; }
        public override int GetHurtSound() { return 0x1D0; }
        public override int GetDeathSound() { return 0x28D; }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new Arrow(Utility.RandomMinMax(25, 35)));
            CorpseLoot.DropItem(new Bandage(Utility.RandomMinMax(5, 15)));
            CorpseLoot.DropItem(new Bandage(Utility.RandomMinMax(5, 15)));
            CorpseLoot.DropItem(Loot.RandomGem());
            CorpseLoot.DropItem(new ArcaneGem());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != null && !willKill && amount > 5 && from.Player && 5 > Utility.Random(100))
            {
                string[] toSay = new string[]
                {
                    "{0}!!  You will have to do better than that!",
                    "{0}!!  Prepare to meet your doom!",
                    "{0}!!  My armies will crush you!",
                    "{0}!!  You will pay for that!"
                };

                Say(true, string.Format(toSay[Utility.Random(toSay.Length)], from.Name));
            }

            base.OnDamage(amount, from, willKill);
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

    [CorpseName("a juka corpse")]
    public class JukaMage : BaseCreature
    {
        private DateTime m_NextAbilityTime;

        [Constructible]
        public JukaMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a juka mage";
            Body = 765;

            SetStr(201, 300);
            SetDex(71, 90);
            SetInt(451, 500);

            SetHits(121, 180);

            SetDamage(ResistType.Phys, 100, 0, 4, 10);

            SetResist(ResistType.Phys, 20, 30);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 10, 20);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.Anatomy, 80.1, 90.0);
            SetSkill(SkillName.EvalInt, 80.2, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.Meditation, 80.2, 100.0);
            SetSkill(SkillName.MagicResist, 140.1, 150.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 16;

            m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
        }

        public JukaMage(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;
        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;

        public override int GetIdleSound() { return 0x1AC; }
        public override int GetAngerSound() { return 0x1CD; }
        public override int GetHurtSound() { return 0x1D0; }
        public override int GetDeathSound() { return 0x28D; }

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

            CorpseLoot.DropItem(new ArcaneGem());

            if (Core.ML && Utility.RandomDouble() < .33)
                CorpseLoot.DropItem(Engines.Plants.Seed.RandomPeculiarSeed(2));

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnThink()
        {
            if (DateTime.UtcNow >= m_NextAbilityTime)
            {
                JukaLord toBuff = null;
                IPooledEnumerable eable = GetMobilesInRange(8);

                foreach (Mobile m in eable)
                {
                    if (m is JukaLord lord && IsFriend(m) && m.Combatant != null && CanBeBeneficial(m) && m.CanBeginAction(typeof(JukaMage)) && InLOS(m))
                    {
                        toBuff = lord;
                        break;
                    }
                }
                eable.Free();

                if (toBuff != null)
                {
                    if (CanBeBeneficial(toBuff) && toBuff.BeginAction(typeof(JukaMage)))
                    {
                        m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));

                        toBuff.Say(true, "Give me the power to destroy my enemies!");
                        Say(true, "Fight well my lord!");

                        DoBeneficial(toBuff);

                        object[] state = new object[] { toBuff, toBuff.HitsMaxSeed, toBuff.RawStr, toBuff.RawDex };

                        SpellHelper.Turn(this, toBuff);

                        int toScale = toBuff.HitsMaxSeed;

                        if (toScale > 0)
                        {
                            toBuff.HitsMaxSeed += AOS.Scale(toScale, 75);
                            toBuff.Hits += AOS.Scale(toScale, 75);
                        }

                        toScale = toBuff.RawStr;

                        if (toScale > 0)
                            toBuff.RawStr += AOS.Scale(toScale, 50);

                        toScale = toBuff.RawDex;

                        if (toScale > 0)
                        {
                            toBuff.RawDex += AOS.Scale(toScale, 50);
                            toBuff.Stam += AOS.Scale(toScale, 50);
                        }

                        toBuff.Hits = toBuff.Hits;
                        toBuff.Stam = toBuff.Stam;

                        toBuff.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
                        toBuff.PlaySound(0x1EE);

                        Timer.DelayCall(TimeSpan.FromSeconds(20.0), new TimerStateCallback(Unbuff), state);
                    }
                }
                else
                {
                    m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
                }
            }

            base.OnThink();
        }

        private void Unbuff(object state)
        {
            object[] states = (object[])state;

            JukaLord toDebuff = (JukaLord)states[0];

            toDebuff.EndAction(typeof(JukaMage));

            if (toDebuff.Deleted)
                return;

            toDebuff.HitsMaxSeed = (int)states[1];
            toDebuff.RawStr = (int)states[2];
            toDebuff.RawDex = (int)states[3];

            toDebuff.Hits = toDebuff.Hits;
            toDebuff.Stam = toDebuff.Stam;
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

    [CorpseName("a juka corpse")]
    public class JukaWarrior : BaseCreature
    {
        [Constructible]
        public JukaWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a juka warrior";
            Body = 764;

            SetStr(251, 350);
            SetDex(61, 80);
            SetInt(101, 150);

            SetHits(151, 210);

            SetDamage(ResistType.Phys, 100, 0, 7, 9);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 10, 20);
            SetResist(ResistType.Engy, 10, 20);

            SetSkill(SkillName.Anatomy, 80.1, 90.0);
            SetSkill(SkillName.Fencing, 80.1, 90.0);
            SetSkill(SkillName.Macing, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 120.1, 130.0);
            SetSkill(SkillName.Swords, 80.1, 90.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 10000;
            Karma = -10000;

            VirtualArmor = 22;
        }

        public JukaWarrior(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;

        public override int GetIdleSound() { return 0x1AC; }
        public override int GetAngerSound() { return 0x1CD; }
        public override int GetHurtSound() { return 0x1D0; }
        public override int GetDeathSound() { return 0x28D; }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomDouble() < 0.1)
                CorpseLoot.DropItem(new ArcaneGem());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Gems, 1);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.2 < Utility.RandomDouble())
                return;

            switch (Utility.Random(3))
            {
                case 0:
                    {
                        defender.SendLocalizedMessage(1004014); // You have been stunned!
                        defender.Freeze(TimeSpan.FromSeconds(4.0));
                        break;
                    }
                case 1:
                    {
                        defender.SendAsciiMessage("You have been hit by a paralyzing blow!");
                        defender.Freeze(TimeSpan.FromSeconds(3.0));
                        break;
                    }
                case 2:
                    {
                        AOS.Damage(defender, this, Utility.Random(10, 5), 100, 0, 0, 0, 0);
                        defender.SendAsciiMessage("You have been hit by a critical strike!");
                        break;
                    }

                default:
                    break;
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
}
