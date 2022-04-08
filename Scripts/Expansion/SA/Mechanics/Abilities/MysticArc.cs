using System;
using System.Collections.Generic;

namespace Server.Items
{
    public class MysticArc : WeaponAbility
    {
        private readonly int m_Damage = 15;
        private Mobile m_Target;
        private Mobile m_Mobile;

        public override int BaseMana => 20;

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!CheckMana(attacker, true) && defender != null)
                return;

            BaseThrown weapon = attacker.Weapon as BaseThrown;

            if (weapon == null)
                return;

            List<Mobile> targets = new List<Mobile>();
            IPooledEnumerable eable = attacker.GetMobilesInRange(weapon.MaxRange);

            foreach (Mobile m in eable)
            {
                if (m == defender)
                    continue;

                if (m.Combatant != attacker)
                    continue;

                targets.Add(m);
            }

            eable.Free();

            if (targets != null && targets.Count > 0)
                m_Target = targets[Utility.Random(targets.Count)];

            AOS.Damage(defender, attacker, m_Damage, 0, 0, 0, 0, 0, 100);

            if (m_Target != null)
            {
                defender?.MovingEffect(m_Target, weapon.ItemID, 18, 1, false, false);
                Timer.DelayCall(TimeSpan.FromMilliseconds(333.0), new TimerCallback(ThrowAgain));
                m_Mobile = attacker;
            }

            ClearCurrentAbility(attacker);
        }

        public void ThrowAgain()
        {
            if (m_Target != null && m_Mobile != null)
            {
                if (!(m_Mobile.Weapon is BaseThrown weapon))
                    return;

                if (GetCurrentAbility(m_Mobile) is MysticArc)
                    ClearCurrentAbility(m_Mobile);

                if (weapon != null && weapon.CheckHit(m_Mobile, m_Target))
                {
                    weapon.OnHit(m_Mobile, m_Target, 0.0);
                    AOS.Damage(m_Target, m_Mobile, m_Damage, 0, 0, 0, 0, 100);
                }
            }
        }
    }
}
