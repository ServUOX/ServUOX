using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a giant beetle corpse")]
    public class Beetle : BaseMount
    {
        public virtual double BoostedSpeed => 0.1;

        [Constructible]
        public Beetle()
            : this("a giant beetle")
        {
        }

        public override bool SubdueBeforeTame => true;
        public override bool ReduceSpeedWithDamage => false;

        [Constructible]
        public Beetle(string name)
            : base(name, 0x317, 0x3EBC, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.25, 0.5)
        {
            SetStr(300);
            SetDex(100);
            SetInt(500);

            SetHits(200);

            SetDamage(ResistType.Phys, 100, 0, 7, 20);

            SetResist(ResistType.Phys, 30, 40);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 20, 30);

            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 4000;
            Karma = -4000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 29.1;

            Container pack = Backpack;

            if (pack != null)
                pack.Delete();

            pack = new StrongBackpack
            {
                Movable = false
            };

            AddItem(pack);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            base.OnDeath(CorpseLoot);
        }

        public override int GetAngerSound() { return 0x21D; }
        public override int GetIdleSound() { return 0x21D; }
        public override int GetAttackSound() { return 0x162; }
        public override int GetHurtSound() { return 0x163; }
        public override int GetDeathSound() { return 0x21D; }

        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAutoStable => (Backpack == null || Backpack.Items.Count == 0) && base.CanAutoStable;

        public Beetle(Serial serial)
            : base(serial)
        {
        }

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

        #region Pack Animal Methods
        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath())
                return false;

            PackAnimal.CombineBackpacks(this);

            return true;
        }

        public override DeathMoveResult GetInventoryMoveResultFor(Item item)
        {
            return DeathMoveResult.MoveToCorpse;
        }

        public override bool IsSnoop(Mobile from)
        {
            if (PackAnimal.CheckAccess(this, from))
                return false;

            return base.IsSnoop(from);
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (CheckFeed(from, item))
                return true;

            if (PackAnimal.CheckAccess(this, from))
            {
                AddToBackpack(item);
                return true;
            }

            return base.OnDragDrop(from, item);
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override bool CheckNonlocalLift(Mobile from, Item item)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            PackAnimal.GetContextMenuEntries(this, from, list);
        }

        #endregion

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
