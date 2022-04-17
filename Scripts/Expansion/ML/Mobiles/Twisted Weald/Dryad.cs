using System;
using Server.Engines.Plants;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a dryad's corpse")]
    public class Dryad : BaseCreature
    {
        public override bool InitialInnocent => true;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        [Constructible]
        public Dryad()
            : base(AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = "a dryad";
            Body = 266;

            SetStr(132, 149);
            SetDex(152, 168);
            SetInt(251, 280);

            SetHits(304, 321);

            SetDamage(ResistType.Phys, 100, 0, 11, 20);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 40, 45);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.Meditation, 80.0, 90.0);
            SetSkill(SkillName.EvalInt, 70.0, 80.0);
            SetSkill(SkillName.Magery, 70.0, 80.0);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 70.0, 80.0);
            SetSkill(SkillName.Wrestling, 70.0, 80.0);

            Fame = 5000;
            Karma = 5000;

            VirtualArmor = 28;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            if (Core.ML && Utility.RandomDouble() < .60)
                PackItem(Seed.RandomPeculiarSeed(1));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override int Meat => 1;

        public override void OnThink()
        {
            base.OnThink();

            AreaPeace();
            AreaUndress();
        }

        private DateTime m_NextPeace;

        public void AreaPeace()
        {
            if (Combatant == null || Deleted || !Alive || m_NextPeace > DateTime.UtcNow || 0.1 < Utility.RandomDouble())
                return;

            TimeSpan duration = TimeSpan.FromSeconds(Utility.RandomMinMax(20, 80));
            IPooledEnumerable eable = GetMobilesInRange(RangePerception);

            foreach (Mobile m in eable)
            {
                PlayerMobile p = m as PlayerMobile;

                if (IsValidTarget(p))
                {
                    p.PeacedUntil = DateTime.UtcNow + duration;
                    p.SendLocalizedMessage(1072065); // You gaze upon the dryad's beauty, and forget to continue battling!
                    p.FixedParticles(0x376A, 1, 20, 0x7F5, EffectLayer.Waist);
                    p.Combatant = null;
                }
            }
            eable.Free();

            m_NextPeace = DateTime.UtcNow + TimeSpan.FromSeconds(10);
            PlaySound(0x1D3);
        }

        public bool IsValidTarget(PlayerMobile m)
        {
            if (m != null && m.PeacedUntil < DateTime.UtcNow && !m.Hidden && m.IsPlayer() && CanBeHarmful(m))
                return true;

            return false;
        }

        private DateTime m_NextUndress;

        public void AreaUndress()
        {
            if (Combatant == null || Deleted || !Alive || m_NextUndress > DateTime.UtcNow || 0.005 < Utility.RandomDouble())
                return;

            IPooledEnumerable eable = GetMobilesInRange(RangePerception);

            foreach (Mobile m in eable)
            {
                if (m != null && m.Player && !m.Female && !m.Hidden && m.IsPlayer() && CanBeHarmful(m))
                {
                    UndressItem(m, Layer.OuterTorso);
                    UndressItem(m, Layer.InnerTorso);
                    UndressItem(m, Layer.MiddleTorso);
                    UndressItem(m, Layer.Pants);
                    UndressItem(m, Layer.Shirt);

                    m.SendLocalizedMessage(1072197); // The dryad's beauty makes your blood race. Your clothing is too confining.
                }
            }
            eable.Free();

            m_NextUndress = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        }

        public void UndressItem(Mobile m, Layer layer)
        {
            Item item = m.FindItemOnLayer(layer);

            if (item != null && item.Movable)
                m.PlaceInBackpack(item);
        }

        public override int GetDeathSound() => 0x57A;
        public override int GetAttackSound() => 0x57B;
        public override int GetIdleSound() => 0x57C;
        public override int GetAngerSound() => 0x57D;
        public override int GetHurtSound() => 0x57E;

        public Dryad(Serial serial)
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
        }
    }
}
