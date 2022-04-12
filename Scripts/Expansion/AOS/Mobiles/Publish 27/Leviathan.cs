using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a leviathan corpse")]
    public class Leviathan : BaseCreature
    {
        private DateTime m_NextWaterBall;

        [Constructible]
        public Leviathan()
            : this(null)
        {
        }

        [Constructible]
        public Leviathan(Mobile fisher)
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Fisher = fisher;
            m_NextWaterBall = DateTime.UtcNow;

            Name = "a leviathan";
            Body = 77;
            BaseSoundID = 353;

            Hue = 0x481;

            SetStr(1000);
            SetDex(501, 520);
            SetInt(501, 515);

            SetHits(1500);

            SetDamage(25, 33);

            SetDamageType(ResistType.Phys, 70);
            SetDamageType(ResistType.Cold, 30);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 45, 55);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Pois, 35, 45);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.EvalInt, 97.6, 107.5);
            SetSkill(SkillName.Magery, 97.6, 107.5);
            SetSkill(SkillName.MagicResist, 97.6, 107.5);
            SetSkill(SkillName.Meditation, 97.6, 107.5);
            SetSkill(SkillName.Tactics, 97.6, 107.5);
            SetSkill(SkillName.Wrestling, 97.6, 107.5);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 50;

            CanSwim = true;
            CantWalk = true;

            PackItem(new MessageInABottle());

            Rope rope = new Rope();
            rope.ItemID = 0x14F8;
            PackItem(rope);

            rope = new Rope();
            rope.ItemID = 0x14FA;
            PackItem(rope);

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Leviathan(Serial serial)
            : base(serial)
        {
        }
        // list of artifacts 
        // luna lance 0
        // violet courage 1
        // cavorting club 2
        // captain quacklebush’s cutlass 3
        // night’s kiss 4
        // ship model of the h.m.s. cape 5
        // the admiral’s hearty rum 6
        // candelabra of souls 7
        // iolo’s lute 8
        // gwenno’s harp 9
        // arctic death dealer 10
        // enchanted titan leg bone 11
        // nox ranger’s heavy crossbow 12
        // blaze of death 13
        // dread pirate hat 14
        // burglar’s bandana 15
        // gold bricks 16
        // alchemist’s bauble 17
        // phillip’s wooden steed 18
        // polar bear mask 1

        public static Type[] Artifacts { get; } = new Type[]
        {

            typeof(LunaLance),
            typeof(VioletCourage),
            typeof(CavortingClub),
            typeof(CaptainQuacklebushsCutlass),
            typeof(NightsKiss),
            typeof(ShipModelOfTheHMSCape),
            typeof(AdmiralsHeartyRum),
            typeof(CandelabraOfSouls),
            typeof(IolosLute),
            typeof(GwennosHarp),
            typeof(ArcticDeathDealer),
            typeof(EnchantedTitanLegBone),
            typeof(NoxRangersHeavyCrossbow),
            typeof(BlazeOfDeath),
            typeof(DreadPirateHat),
            typeof(BurglarsBandana),
            typeof(GoldBricks),
            typeof(AlchemistsBauble),
            typeof(PhillipsWoodenSteed),
            typeof(PolarBearMask),
            typeof(GhostShipAnchor),       //Added With SE Publish 28
            typeof(SeahorseStatuette),     //Added With SE Publish 28       
        };

        public Mobile Fisher { get; set; }

        public override int DefaultHitsRegen
        {
            get
            {
                int regen = base.DefaultHitsRegen;

                return IsParagon ? regen : regen += 40;
            }
        }

        public override int DefaultStamRegen
        {
            get
            {
                int regen = base.DefaultStamRegen;

                return IsParagon ? regen : regen += 40;
            }
        }

        public override int DefaultManaRegen
        {
            get
            {
                int regen = base.DefaultManaRegen;

                return IsParagon ? regen : regen += 40;
            }
        }

        public override double TreasureMapChance => 0.25;
        public override int TreasureMapLevel => 5;

        public override void OnActionCombat()
        {
            Mobile combatant = Combatant as Mobile;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 12) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;

            if (DateTime.UtcNow >= m_NextWaterBall)
            {
                double damage = combatant.Hits * 0.3;

                if (damage < 10.0)
                    damage = 10.0;
                else if (damage > 40.0)
                    damage = 40.0;

                DoHarmful(combatant);
                MovingParticles(combatant, 0x36D4, 5, 0, false, false, 195, 0, 9502, 3006, 0, 0, 0);
                AOS.Damage(combatant, this, (int)damage, 100, 0, 0, 0, 0);

                if (combatant is PlayerMobile && combatant.Mount != null)
                {
                    (combatant as PlayerMobile).SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(10), true);
                }

                m_NextWaterBall = DateTime.UtcNow + TimeSpan.FromMinutes(1);
            }
        }

        public static void GiveArtifactTo(Mobile m)
        {
            Item item = Loot.Construct(Artifacts);

            if (item == null)
                return;

            // TODO: Confirm messages
            if (m.AddToBackpack(item))
                m.SendMessage("As a reward for slaying the mighty leviathan, an artifact has been placed in your backpack.");
            else
                m.SendMessage("As your backpack is full, your reward for destroying the legendary leviathan has been placed at your feet.");
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 5);
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

            m_NextWaterBall = DateTime.UtcNow;
        }

        public override void OnKilledBy(Mobile mob)
        {
            base.OnKilledBy(mob);

            if (Paragon.CheckArtifactChance(mob, this))
            {
                GiveArtifactTo(mob);

                if (mob == Fisher)
                    Fisher = null;
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Fisher != null && 25 > Utility.Random(100))
                GiveArtifactTo(Fisher);

            Fisher = null;
        }
    }
}
