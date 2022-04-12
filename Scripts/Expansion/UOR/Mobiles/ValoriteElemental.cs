using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class ValoriteElemental : BaseCreature
    {
        [Constructible]
        public ValoriteElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            // TODO: Gas attack
            Name = "a valorite elemental";
            Body = 112;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(ResistType.Phys, 25, 0, 28);
            SetDamage(ResistType.Fire, 25);
            SetDamage(ResistType.Cold, 25);
            SetDamage(ResistType.Engy, 25);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 38;

            SetAreaEffect(AreaEffect.PoisonBreath);
        }

        public ValoriteElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void OnDeath(Container CorspeLoot)
        {
            CorspeLoot.DropItem(new ValoriteOre(25));
            //ore.ItemID = 0x19B9;
            base.OnDeath(CorspeLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Gems, 4);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature bc)
            {
                if (bc.Controlled || bc.BardTarget == this)
                    damage = 0; // Immune to pets and provoked creatures
            }
            else
            {
                AOS.Damage(from, this, damage / 2, 0, 0, 0, 0, 0, 0, 100);
            }

            base.AlterMeleeDamageFrom(from, ref damage);
        }

        public override bool OnBeforeDeath()
        {
            if (Map == null)
                return base.OnBeforeDeath();

            FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);

            IPooledEnumerable eable = Map.GetMobilesInRange(Location, 4);
            var list = new System.Collections.Generic.List<Mobile>();

            foreach (Mobile m in eable)
            {
                if (m != this && m.Alive && m.AccessLevel == AccessLevel.Player &&
                    (m is PlayerMobile || (m is BaseCreature && !((BaseCreature)m).IsMonster)))
                {
                    list.Add(m);
                }
            }

            foreach (var m in list)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(.5), mob =>
                {
                    mob.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                    mob.PlaySound(0x307);
                    AOS.Damage(mob, this, Utility.RandomMinMax(25, 50), 50, 50, 0, 0, 0);
                }, m);
            }

            ColUtility.Free(list);

            return base.OnBeforeDeath();
        }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = true; // Every spell is reflected back to the caster
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
