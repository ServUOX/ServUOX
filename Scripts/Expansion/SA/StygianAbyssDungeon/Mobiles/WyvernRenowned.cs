using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Wyvern [Renowned] corpse")]
    public class WyvernRenowned : BaseRenowned
    {
        [Constructible]
        public WyvernRenowned()
            : base(AIType.AI_Mage)
        {
            Name = "Wyvern";
            Title = "[Renowned]";
            Body = 62;
            Hue = 243;
            BaseSoundID = 362;

            SetStr(1370, 1422);
            SetDex(103, 151);
            SetInt(835, 1002);

            SetHits(2412, 2734);
            SetStam(103, 151);
            SetMana(835, 1002);

            SetDamage(29, 35);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);

            SetResist(ResistanceType.Physical, 60, 70);
            SetResist(ResistanceType.Fire, 80, 90);
            SetResist(ResistanceType.Cold, 70, 80);
            SetResist(ResistanceType.Poison, 60, 70);
            SetResist(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.Magery, 107.7, 109.1);
            SetSkill(SkillName.Meditation, 63.9, 78.2);
            SetSkill(SkillName.EvalInt, 106.8, 111.1);
            SetSkill(SkillName.Wrestling, 108.6, 109.4);
            SetSkill(SkillName.MagicResist, 125.8, 127.6);
            SetSkill(SkillName.Tactics, 112.8, 123.7);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 70;
        }

        public WyvernRenowned(Serial serial)
            : base(serial)
        {
        }

        public override Type[] UniqueSAList => new Type[] { };

        public override Type[] SharedSAList => new[] { typeof(AnimatedLegsoftheInsaneTinker), typeof(PillarOfStrength), typeof(StormCaller) };

        public override bool ReacquireOnMovement => true;

        public override Poison PoisonImmunity => Poison.Deadly;

        public override Poison HitPoison => Poison.Deadly;

        public override bool AutoDispel => true;

        public override bool BardImmunity => true;

        public override int TreasureMapLevel => 5;

        public override int Meat => 10;

        public override int Hides => 20;

        public override HideType HideType => HideType.Horned;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich);
        }

        public override int GetAttackSound()
        {
            return 713;
        }

        public override int GetAngerSound()
        {
            return 718;
        }

        public override int GetDeathSound()
        {
            return 716;
        }

        public override int GetHurtSound()
        {
            return 721;
        }

        public override int GetIdleSound()
        {
            return 725;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}