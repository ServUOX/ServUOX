using Server.ContextMenus;
using Server.Engines.Harvest;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public abstract class BasePoleArm : BaseMeleeWeapon, IHarvestTool
    {
        public BasePoleArm(int itemID)
            : base(itemID)
        {
        }

        public BasePoleArm(Serial serial)
            : base(serial)
        {
        }

        public override int DefHitSound => 0x237;
        public override int DefMissSound => 0x238;
        public override SkillName DefSkill => SkillName.Swords;
        public override WeaponType DefType => WeaponType.Polearm;
        public override WeaponAnimation DefAnimation => WeaponAnimation.Slash2H;

        public virtual HarvestSystem HarvestSystem => Lumberjacking.System;

        public override void OnDoubleClick(Mobile from)
        {
            if (HarvestSystem == null)
            {
                return;
            }

            if (IsChildOf(from.Backpack) || Parent == from)
            {
                HarvestSystem.BeginHarvesting(from, this);
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (HarvestSystem != null)
            {
                BaseHarvestTool.AddContextMenuEntries(from, this, list, HarvestSystem);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(3);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                    break;
                case 2:
                    {
                        if (version == 2)
                        {
                            ShowUsesRemaining = reader.ReadBool();
                        }

                        goto case 1;
                    }
                case 1:
                    {
                        if (version == 2)
                        {
                            UsesRemaining = reader.ReadInt();
                        }

                        goto case 0;
                    }
                case 0:
                    {
                        break;
                    }
            }
        }

        public override void OnHit(Mobile attacker, IDamageable defender, double damageBonus)
        {
            base.OnHit(attacker, defender, damageBonus);

            if (!Core.AOS && defender is Mobile && (attacker.Player || attacker.Body.IsHuman) && Layer == Layer.TwoHanded && (attacker.Skills[SkillName.Anatomy].Value / 400.0) >= Utility.RandomDouble())
            {
                StatMod mod = ((Mobile)defender).GetStatMod("Concussion");

                if (mod == null)
                {
                    ((Mobile)defender).SendMessage("You receive a concussion blow!");
                    ((Mobile)defender).AddStatMod(new StatMod(StatType.Int, "Concussion", -(((Mobile)defender).RawInt / 2), TimeSpan.FromSeconds(30.0)));

                    attacker.SendMessage("You deliver a concussion blow!");
                    attacker.PlaySound(0x11C);
                }
            }
        }
    }
}
