using System;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("A Drelgor The Impaler Corpse")]
    public class Drelgor : BaseCreature
    {
        private bool init = false; //Don't change this.
        private double msgevery = 1.0; //Recurring message. Change to 0 to disable.
        private DateTime m_NextMsgTime;

        [Constructible]
        public Drelgor()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Drelgor the Impaler";
            Body = 147;
            BaseSoundID = 451;

            SetStr(127, 137);
            SetDex(82, 94);
            SetInt(48, 55);

            SetHits(131, 136);

            SetDamage(6, 8);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Cold, 60);

            SetResist(ResistanceType.Physical, 35, 45);
            SetResist(ResistanceType.Fire, 20, 30);
            SetResist(ResistanceType.Cold, 50, 60);
            SetResist(ResistanceType.Poison, 20, 30);
            SetResist(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Wrestling, 60);
            SetSkill(SkillName.Tactics, 60);
            SetSkill(SkillName.MagicResist, 60);

            Fame = 3600;
            Karma = -3600;

            VirtualArmor = 40;
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new Scimitar());
            CorpseLoot.DropItem(new WoodenShield());

            switch (Utility.Random(5))
            {
                case 0:
                    CorpseLoot.DropItem(new BoneArms());
                    break;
                case 1:
                    CorpseLoot.DropItem(new BoneChest());
                    break;
                case 2:
                    CorpseLoot.DropItem(new BoneGloves());
                    break;
                case 3:
                    CorpseLoot.DropItem(new BoneLegs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new BoneHelm());
                    break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
        }

        public override bool BleedImmunity => true;

        #region Start/Stop
        private void Start()
        {
            m_NextMsgTime = DateTime.UtcNow + TimeSpan.FromMinutes(msgevery);

            BroadcastMessage();
            init = true;
        }
        #endregion

        #region Broadcast Message
        public void BroadcastMessage()
        {
            foreach (NetState state in NetState.Instances)
            {
                Mobile m = state.Mobile;

                if (m != null && Region.Name == "Old Haven Training" && m.Region.Name == "Old Haven Training")
                {
                    m.SendLocalizedMessage(1077840, "", 34); // // Who dares to defile Haven? I am Drelgor the Impaler! I shall claim your souls as payment for this intrusion!
                    m.PlaySound(0x14);
                }
            }
        }
        #endregion

        #region OnThink
        public override void OnThink()
        {
            if (!init)
                Start();

            if (msgevery != 0 && DateTime.UtcNow >= m_NextMsgTime)
            {
                BroadcastMessage();
                m_NextMsgTime = DateTime.UtcNow + TimeSpan.FromMinutes(msgevery);
            }

            base.OnThink();
        }
        #endregion

        public Drelgor(Serial serial)
            : base(serial)
        {
        }

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(init);
            writer.Write(m_NextMsgTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            init = reader.ReadBool();
            m_NextMsgTime = reader.ReadDateTime();
        }
    }
}

