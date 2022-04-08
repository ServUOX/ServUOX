using Server.Guilds;
using Server.Items;

namespace Server.Mobiles
{
    public class ChaosGuard : BaseShieldGuard
    {
        [Constructible]
        public ChaosGuard()
        {
        }

        public ChaosGuard(Serial serial)
            : base(serial)
        {
        }

        public override int Keyword => 0x22;// *chaos shield*
        public override BaseShield Shield => new ChaosShield();
        public override int SignupNumber => 1007140;// Sign up with a guild of chaos if thou art interested.
        public override GuildType Type => GuildType.Chaos;
        public override bool BardImmunity => true;

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
