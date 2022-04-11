using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Lady Jennifyr corpse")]
    public class LadyJennifyr : SkeletalKnight
    {
        private static readonly Dictionary<Mobile, ExpireTimer> m_Table = new Dictionary<Mobile, ExpireTimer>();
        [Constructible]
        public LadyJennifyr()
        {
            Name = "Lady Jennifyr";
            Hue = 0x76D;

            SetStr(208, 309);
            SetDex(91, 118);
            SetInt(44, 101);

            SetHits(1113, 1285);

            SetDamage(15, 25);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Cold, 60);

            SetResist(ResistanceType.Physical, 56, 65);
            SetResist(ResistanceType.Fire, 41, 49);
            SetResist(ResistanceType.Cold, 71, 80);
            SetResist(ResistanceType.Poison, 41, 50);
            SetResist(ResistanceType.Energy, 50, 58);

            SetSkill(SkillName.Wrestling, 127.9, 137.1);
            SetSkill(SkillName.Tactics, 128.4, 141.9);
            SetSkill(SkillName.MagicResist, 102.1, 119.5);
            SetSkill(SkillName.Anatomy, 129.0, 137.5);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 29;
        }

        public LadyJennifyr(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            if (Utility.RandomDouble() < 0.15)
                c.DropItem(new DisintegratingThesisNotes());

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new ParrotItem());

            base.OnDeath(c);
        }

        /*public override bool GivesMLMinorArtifact
        {
            get
            {
                return true;
            }
        }*/

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (Utility.RandomDouble() < 0.1)
            {
                if (m_Table.TryGetValue(defender, out ExpireTimer timer))
                    timer.DoExpire();

                defender.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                defender.PlaySound(0x208);
                defender.SendLocalizedMessage(1070833); // The creature fans you with fire, reducing your resistance to fire attacks.

                ResistanceMod mod = new ResistanceMod(ResistanceType.Fire, -10);
                defender.AddResistanceMod(mod);

                m_Table[defender] = timer = new ExpireTimer(defender, mod);
                timer.Start();
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

        private class ExpireTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly ResistanceMod m_Mod;
            public ExpireTimer(Mobile m, ResistanceMod mod)
                : base(TimeSpan.FromSeconds(10))
            {
                m_Mobile = m;
                m_Mod = mod;
                Priority = TimerPriority.TwoFiftyMS;
            }

            public void DoExpire()
            {
                m_Mobile.RemoveResistanceMod(m_Mod);

                Stop();
                m_Table.Remove(m_Mobile);
            }

            protected override void OnTick()
            {
                m_Mobile.SendLocalizedMessage(1070834); // Your resistance to fire attacks has returned.
                DoExpire();
            }
        }
    }
}
