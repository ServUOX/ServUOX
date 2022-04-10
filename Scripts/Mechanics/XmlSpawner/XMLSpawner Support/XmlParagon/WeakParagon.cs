namespace Server.Engines.XmlSpawner2
{
    public class WeakParagon : XmlParagon
    {
        // string that is displayed on the xmlspawner when this is attached
        public override string OnIdentify(Mobile from)
        {
            return string.Format("Weak {0}", base.OnIdentify(from));
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

        public WeakParagon(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public WeakParagon()
            : base()
        {
            // reduced buff modifiers
            HitsBuff = 4.0;
            StrBuff = 1.05;
            IntBuff = 1.10;
            DexBuff = 1.10;
            SkillsBuff = 1.20;
            SpeedBuff = 1.20;
            FameBuff = 1.40;
            KarmaBuff = 1.40;
            DamageBuff = 4;
        }
    }
}
