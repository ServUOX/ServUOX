using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a wyvern corpse")]
    public class Wyvern : BaseCreature
    {
        [Constructible]
        public Wyvern()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a wyvern";
            Body = 62;
            BaseSoundID = 362;

            SetStr(202, 240);
            SetDex(153, 172);
            SetInt(51, 90);

            SetHits(125, 141);

            SetDamage(8, 19);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResist(ResistanceType.Physical, 35, 45);
            SetResist(ResistanceType.Fire, 30, 40);
            SetResist(ResistanceType.Cold, 20, 30);
            SetResist(ResistanceType.Poison, 90, 100);
            SetResist(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 65.1, 90.0);
            SetSkill(SkillName.Wrestling, 65.1, 80.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 40;

            PackItem(new LesserPoisonPotion());
        }

        public Wyvern(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override Poison PoisonImmunity => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 2;
        public override int Meat => 10;
        public override int Hides => 20;
        public override HideType HideType => HideType.Horned;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.MedScrolls);
        }

        public override int GetAttackSound() { return 713; }
        public override int GetAngerSound() { return 718; }
        public override int GetDeathSound() { return 716; }
        public override int GetHurtSound() { return 721; }
        public override int GetIdleSound() { return 725; }

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
