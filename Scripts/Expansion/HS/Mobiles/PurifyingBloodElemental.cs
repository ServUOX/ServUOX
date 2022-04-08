using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a purifying blood elemental corpse")]
    public class PurifyingBloodElemental : BaseCreature, IBloodCreature
    {
        [Constructible]
        public PurifyingBloodElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a purifying blood elemental";
            Body = 160;
            BaseSoundID = 278;
            Hue = 1911;

            SetStr(900, 980);
            SetDex(950, 999);
            SetInt(900, 950);

            SetHits(3000, 3100);

            SetDamage(17, 27);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 30);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 55, 70);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.EvalInt, 85.1, 100.0);
            SetSkill(SkillName.Magery, 85.1, 100.0);
            SetSkill(SkillName.Meditation, 10.4, 50.0);
            SetSkill(SkillName.MagicResist, 80.1, 95.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);
            SetSkill(SkillName.DetectHidden, 60.0, 70.0);

            Fame = 12500;
            Karma = -12500;

            VirtualArmor = 60;

            SetSpecialAbility(SpecialAbility.StealLife);
            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
        }

        public PurifyingBloodElemental(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 5;

        public override bool CanRummageCorpses => true;

        private Mobile _LastTeleported;

        public override void OnAfterTeleport(Mobile m)
        {
            m.SendLocalizedMessage(1072195); // Without warning, you are magically transported closer to your enemy!
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (defender == _LastTeleported)
            {
                BoneBreakerContext.CheckHit(this, defender);
            }

            _LastTeleported = null;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
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
