using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Skeletal Dragon [Renowned] corpse")]
    public class SkeletalDragonRenowned : BaseRenowned
    {
        [Constructible]
        public SkeletalDragonRenowned()
            : base(AIType.AI_Mage)
        {
            Name = "Skeletal Dragon";
            Title = "[Renowned]";
            Body = 104;
            BaseSoundID = 0x488;

            Hue = 906;

            SetStr(898, 1030);
            SetDex(100, 200);
            SetInt(488, 620);

            SetHits(558, 599);

            SetDamage(29, 35);

            SetDamageType(ResistType.Phys, 75);
            SetDamageType(ResistType.Fire, 25);

            SetResist(ResistType.Phys, 75, 80);
            SetResist(ResistType.Fire, 40, 60);
            SetResist(ResistType.Cold, 40, 60);
            SetResist(ResistType.Pois, 70, 80);
            SetResist(ResistType.Engy, 40, 60);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.3, 130.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 80;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public SkeletalDragonRenowned(Serial serial)
            : base(serial)
        {
        }

        // TODO: Undead summoning?
        public override Type[] UniqueSAList => new Type[] { };
        public override Type[] SharedSAList => new Type[] { typeof(AxeOfAbandon), typeof(DemonBridleRing), typeof(VoidInfusedKilt), typeof(BladeOfBattle) };
        public override bool ReacquireOnMovement => true;
        public override double BonusPetDamageScalar => (Core.SE) ? 3.0 : 1.0;
        public override bool AutoDispel => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool BleedImmunity => true;
        public override int Meat => 19;
        public override int Hides => 20;
        public override HideType HideType => HideType.Barbed;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
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
