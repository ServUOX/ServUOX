namespace Server.Mobiles
{
    [CorpseName("a corporeal brume corpse")]
    public class CorporealBrume : BaseCreature, IAuraCreature
    {
        [Constructible]
        public CorporealBrume()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a corporeal brume";
            Body = 0x104;
            BaseSoundID = 0x56B;

            SetStr(400, 450);
            SetDex(100, 150);
            SetInt(50, 60);

            SetHits(1150, 1250);

            SetDamage(21, 25);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 100);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Poison, 50, 60);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.Wrestling, 110.0, 115.0);
            SetSkill(SkillName.Tactics, 110.0, 115.0);
            SetSkill(SkillName.MagicResist, 80.0, 95.0);
            SetSkill(SkillName.Anatomy, 100.0, 110.0);

            Fame = 12000;
            Karma = -12000;

            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public CorporealBrume(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;

        public void AuraEffect(Mobile m)
        {
            m.FixedParticles(0x374A, 10, 15, 5038, 1181, 2, EffectLayer.Head);
            m.PlaySound(0x213);

            m.SendLocalizedMessage(1008111, false, Name); //  : The intense cold is damaging you!
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
