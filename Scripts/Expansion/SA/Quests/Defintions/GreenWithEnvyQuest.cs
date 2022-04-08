using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class GreenWithEnvyQuest : BaseQuest
    {
        public GreenWithEnvyQuest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(EyeOfNavrey), "eye of Navrey", 1, 0x1F1C));

            AddReward(new BaseReward(typeof(RewardBox), 1072584));
        }

        /* Green with Envy */
        public override object Title => 1095118;
        /* Kill the queen spider, Navrey Night-Eyes.  Bring proof of her death to King Vernix.<br><center>-----</center>
        <br>Vernix plan is now ready.  Man thing from outside make good champion for Green Goblins.  King Vernix will 
        let him in on plan.  Gray goblin power comes from their god, Navrey Night-Eyes.  Navery is great spider. 
        Very nasty.  Gray Goblins and Green Goblins used to be one tribe, but Gray Goblins gain power from Navery and 
        make Green Goblins slaves.  Green Goblins escape tribe and find own place.<br><br>Navery Night-Eyes has big hunger. 
        Always need more blood.  Gray Goblins want to sacrifice outside kind to Navery so she not eat them.  You kill Navery, 
        you solve big problem for outside kind and goblin kind.<br><br>If you do this, you take big risk so Vernix make it 
        worth your while.  Kill Navery Night-Eyes. */
        public override object Description => 1095120;
        /* You no kill her, then many outside people disappear at night when others sleep.  You think about it, then
        come back when you ready to make deal. */
        public override object Refuse => 1095121;
        /* You not have much time.  Navery Night-Eyes is hungry. */
        public override object Uncomplete => 1095122;
        /* You do good service to your people.  Now Green Goblins will do the rest.  Without power from Navery Night-Eyes,
        we will have our revenge.  Vernix keep Green Goblin end of deal. */
        public override object Complete => 1095123;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
