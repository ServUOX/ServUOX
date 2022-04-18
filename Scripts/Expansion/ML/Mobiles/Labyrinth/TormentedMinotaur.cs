using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a tormented minotaur corpse")]
    public class TormentedMinotaur : BaseCreature
    {
        [Constructible]
        public TormentedMinotaur()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Tormented Minotaur";
            Body = 262;

            SetStr(822, 930);
            SetDex(401, 415);
            SetInt(128, 138);

            SetHits(4000, 4200);

            SetDamage(ResistType.Phys, 100, 0, 16, 30);

            SetResist(ResistType.Phys, 62);
            SetResist(ResistType.Fire, 74);
            SetResist(ResistType.Cold, 54);
            SetResist(ResistType.Pois, 56);
            SetResist(ResistType.Engy, 54);

            SetSkill(SkillName.Wrestling, 110.1, 111.0);
            SetSkill(SkillName.Tactics, 100.7, 102.8);
            SetSkill(SkillName.MagicResist, 104.3, 116.3);

            Fame = 20000;
            Karma = -20000;

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public TormentedMinotaur(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Deadly;
        public override int TreasureMapLevel => 3;

        public override void GenerateLoot() => AddLoot(LootPack.FilthyRich, 10);

        public override int GetAngerSound() => 0x59A;
        public override int GetIdleSound() => 0x599;
        public override int GetAttackSound() => 0x598;
        public override int GetHurtSound() => 0x59b;
        public override int GetDeathSound() => 0x597;

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
