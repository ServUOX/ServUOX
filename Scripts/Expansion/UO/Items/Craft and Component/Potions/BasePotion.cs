using Server.Engines.Craft;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public enum PotionEffect
    {
        Nightsight,
        CureLesser,
        Cure,
        CureGreater,
        Agility,
        AgilityGreater,
        Strength,
        StrengthGreater,
        PoisonLesser,
        Poison,
        PoisonGreater,
        PoisonDeadly,
        Refresh,
        RefreshTotal,
        HealLesser,
        Heal,
        HealGreater,
        ExplosionLesser,
        Explosion,
        ExplosionGreater,
        Conflagration,
        ConflagrationGreater,
        MaskOfDeath,
        MaskOfDeathGreater,
        ConfusionBlast,
        ConfusionBlastGreater,
        Invisibility,
        Parasitic,
        Darkglow,
        ExplodingTarPotion,
        #region TOL Publish 93
        Barrab,
        Jukari,
        Kurak,
        Barako,
        Urali,
        Sakkhra,
        #endregion,
        Shatter,
        FearEssence
    }

    public abstract class BasePotion : Item, ICraftable, ICommodity
    {
        private PotionEffect m_PotionEffect;

        public PotionEffect PotionEffect
        {
            get => m_PotionEffect;
            set
            {
                m_PotionEffect = value;
                InvalidateProperties();
            }
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => Core.ML;

        public override int LabelNumber => 1041314 + (int)m_PotionEffect;

        public BasePotion(int itemID, PotionEffect effect)
            : base(itemID)
        {
            m_PotionEffect = effect;

            Stackable = Core.ML;
            Weight = 1.0;
        }

        public BasePotion(Serial serial)
            : base(serial)
        {
        }

        public virtual bool RequireFreeHand => true;

        public static bool HasFreeHand(Mobile m)
        {
            Item handOne = m.FindItemOnLayer(Layer.OneHanded);
            Item handTwo = m.FindItemOnLayer(Layer.TwoHanded);

            if (handTwo is BaseWeapon)
            {
                handOne = handTwo;
            }

            if (handTwo is BaseWeapon wep)
            {
                if (wep.Attributes.BalancedWeapon > 0)
                {
                    return true;
                }
            }

            return handOne == null || handTwo == null;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
            {
                return;
            }

            if (!from.BeginAction(GetType()))
            {
                from.SendLocalizedMessage(500119); // You must wait to perform another action.
                return;
            }

            Timer.DelayCall(TimeSpan.FromMilliseconds(500), () => from.EndAction(GetType()));

            if (from.InRange(GetWorldLocation(), 1))
            {
                if (!RequireFreeHand || HasFreeHand(from))
                {
                    if (this is BaseExplosionPotion && Amount > 1)
                    {
                        BasePotion pot = (BasePotion)Activator.CreateInstance(GetType());

                        if (pot != null)
                        {
                            Amount--;

                            if (from.Backpack != null && !from.Backpack.Deleted)
                            {
                                from.Backpack.DropItem(pot);
                            }
                            else
                            {
                                pot.MoveToWorld(from.Location, from.Map);
                            }
                            pot.Drink(from);
                        }
                    }
                    else
                    {
                        Drink(from);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
                }
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write((int)m_PotionEffect);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                case 0:
                    {
                        m_PotionEffect = (PotionEffect)reader.ReadInt();
                        break;
                    }
            }

            if (version == 0)
            {
                Stackable = Core.ML;
            }
        }

        public abstract void Drink(Mobile from);

        public static void PlayDrinkEffect(Mobile m)
        {
            m.RevealingAction();
            m.PlaySound(0x2D6);
            m.AddToBackpack(new EmptyBottle());

            if (m.Body.IsHuman && !m.Mounted)
            {
                if (Core.SA)
                {
                    m.Animate(AnimationType.Eat, 0);
                }
                else
                {
                    m.Animate(34, 5, 1, true, false, 0);
                }
            }
        }

        public static int EnhancePotions(Mobile m)
        {
            int EP = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
            int skillBonus = m.Skills.Alchemy.Fixed / 330 * 10;

            if (Core.ML && EP > 50 && m.IsPlayer())
            {
                EP = 50;
            }

            return (EP + skillBonus);
        }

        public static TimeSpan Scale(Mobile m, TimeSpan v)
        {
            if (!Core.AOS)
            {
                return v;
            }

            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return TimeSpan.FromSeconds(v.TotalSeconds * scalar);
        }

        public static double Scale(Mobile m, double v)
        {
            if (!Core.AOS)
            {
                return v;
            }

            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return v * scalar;
        }

        public static int Scale(Mobile m, int v)
        {
            if (!Core.AOS)
            {
                return v;
            }

            return AOS.Scale(v, 100 + EnhancePotions(m));
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            return dropped is BasePotion && ((BasePotion)dropped).m_PotionEffect == m_PotionEffect && base.WillStack(from, dropped);
        }

        #region ICraftable Members

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefAlchemy)
            {
                Container pack = from.Backpack;

                if (pack != null)
                {
                    if ((int)PotionEffect >= (int)PotionEffect.Invisibility)
                    {
                        return 1;
                    }

                    List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

                    for (int i = 0; i < kegs.Count; ++i)
                    {
                        PotionKeg keg = kegs[i];

                        if (keg == null)
                        {
                            continue;
                        }

                        if (keg.Held <= 0 || keg.Held >= 100)
                        {
                            continue;
                        }

                        if (keg.Type != PotionEffect)
                        {
                            continue;
                        }

                        ++keg.Held;

                        Consume();
                        from.AddToBackpack(new EmptyBottle());

                        return -1; // signal placed in keg
                    }
                }
            }

            return 1;
        }
        #endregion
    }
}
