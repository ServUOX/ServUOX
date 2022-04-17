using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a fire beetle corpse")]
    [Server.Engines.Craft.Forge]
    public class FireBeetle : BaseMount
    {
        [Constructible]
        public FireBeetle()
            : base("a fire beetle", 0xA9, 0x3E95, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SetStr(300);
            SetDex(100);
            SetInt(500);

            SetHits(200);

            SetDamage(ResistType.Phys, 0, 0, 7, 20);
            SetDamage(ResistType.Fire, 100);

            SetResist(ResistType.Phys, 40);
            SetResist(ResistType.Fire, 70, 75);
            SetResist(ResistType.Cold, 10);
            SetResist(ResistType.Pois, 30);
            SetResist(ResistType.Engy, 30);

            SetSkill(SkillName.MagicResist, 90.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 4000;
            Karma = -4000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 93.9;

            PackItem(new SulfurousAsh(Utility.RandomMinMax(16, 25)));
            PackItem(new IronIngot(2));

            Hue = 0x489;
        }

        public FireBeetle(Serial serial)
            : base(serial)
        {
        }

        public override bool SubdueBeforeTame => true;// Must be beaten into submission
        public override bool StatLossAfterTame => true;
        public virtual double BoostedSpeed => 0.1;
        public override bool ReduceSpeedWithDamage => false;
        public override int Meat => 16;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override void OnHarmfulSpell(Mobile from)
        {
            if (!Controlled && ControlMaster == null)
                CurrentSpeed = BoostedSpeed;
        }

        public override void OnCombatantChange()
        {
            if (Combatant == null && !Controlled && ControlMaster == null)
                CurrentSpeed = PassiveSpeed;
        }

        public override bool OverrideBondingReqs() => true;

        public override int GetAngerSound() => 0x21D;

        public override int GetIdleSound() => 0x21D;

        public override int GetAttackSound() => 0x162;

        public override int GetHurtSound() => 0x163;

        public override int GetDeathSound() => 0x21D;

        public override double GetControlChance(Mobile m, bool useBaseSkill)
        {
            if (PetTrainingHelper.Enabled)
            {
                var profile = PetTrainingHelper.GetAbilityProfile(this);

                if (profile != null && profile.HasCustomized())
                {
                    return base.GetControlChance(m, useBaseSkill);
                }
            }

            return 1.0;
        }

        public override void OnAfterTame(Mobile tamer)
        {
            base.OnAfterTame(tamer);

            if (Owners.Count == 0 && PetTrainingHelper.Enabled)
            {
                SetInt(500);
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
    }
}
