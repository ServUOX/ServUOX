using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a satyr's corpse")]
    public class Satyr : BaseCreature
    {
        [Constructible]
        public Satyr()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a satyr";
            Body = 271;
            BaseSoundID = 0x586;

            SetStr(177, 195);
            SetDex(251, 269);
            SetInt(153, 170);

            SetHits(350, 400);

            SetDamage(ResistType.Phys, 100, 0, 13, 24);

            SetResist(ResistType.Phys, 55, 60);
            SetResist(ResistType.Fire, 25, 35);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 55.0, 65.0);
            SetSkill(SkillName.Tactics, 80.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.0, 100.0);

            SetSkill(SkillName.Musicianship, 100);
            SetSkill(SkillName.Discordance, 100);
            SetSkill(SkillName.Provocation, 100);
            SetSkill(SkillName.Peacemaking, 100);

            Fame = 5000;
            Karma = 0;

            VirtualArmor = 28;
        }

        public Satyr(Serial serial)
            : base(serial)
        {
        }

        public override TribeType Tribe => TribeType.Fey;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override bool CanDiscord => true;
        public override bool CanPeace => true;
        public override bool CanProvoke => true;
        public override int Meat => 1;

        public override int GetDeathSound() => 0x586;
        public override int GetAttackSound() => 0x587;
        public override int GetIdleSound() => 0x588;
        public override int GetAngerSound() => 0x589;
        public override int GetHurtSound() => 0x58A;

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.MlRich);
            AddLoot(LootPack.MedScrolls);
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