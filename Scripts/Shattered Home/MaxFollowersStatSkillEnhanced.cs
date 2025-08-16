//   ___|============================|___
//   \  |   Adapted by crediblefool  |  /   MaxFollowersStatSkillEnhanced
//    > |       December 2024        | <
//   /__|============================|__\   Description: Player's max number of followers depends on stats & skills.
//   The original MaxFollowersIntelValued by Felladorin (v 1.1, August 24, 2013) forms the foundation for this
//   feature. However, the idea here is to use total stats and moves the skill modifications here also.
//
//   Usage: Set the Config, on the first lines of this script, to suit your needs.
//
//   Open PlayerMobile.cs and find, in the ValidateEquipment_Sandbox method, the following line:
//   Mobile from = this;
//
//   Under that line, add the line bellow:
//   MaxFollowersStatSkillEnhanced.Evaluate(from);
//  
//   Then, still in PlayerMobile.cs, find:
//   	if (skill.SkillName == SkillName.Herding || skill.SkillName == SkillName.Veterinary || skill.SkillName == SkillName.Druidism || skill.SkillName == SkillName.Taming)
//     		UpdateFollowers();
//
//   And rewrite to:
//      if (skill.SkillName == SkillName.Herding || skill.SkillName == SkillName.Veterinary || skill.SkillName == SkillName.Druidism || skill.SkillName == SkillName.Taming)
//   	{
//   		UpdateFollowers();
//   		MaxFollowersStatSkillEnhanced.Evaluate(this);
//   	}
//
//   Finally, edit the UpdateFollowers() method to:
//   	public virtual void UpdateFollowers()
//   	{
//   		return
//   	}
namespace Server.Mobiles
{
    public static class MaxFollowersStatSkillEnhanced
    {
        public static class Config
        {
            public static int StatsPerFollower = 40;      // For every this many stat points, gain a follower spot.
            public static int MaxFollowersAllowed = 100;
            public static int MinFollowersAllowed = 2;
        }

        public static void Evaluate(Mobile m)
        {

            int SlotsBonusFromStats = (m.RawStr + m.RawDex + m.RawInt) / Config.StatsPerFollower;

            int TotalSlots = Config.MinFollowersAllowed + SlotsBonusFromStats;

            m.FollowersMax = TotalSlots;

            if (m.Skills[SkillName.Herding].Base >= 120)
                m.FollowersMax += 2;

            if (m.Skills[SkillName.Herding].Base >= 100)
                m.FollowersMax += 1;

            if (m.Skills[SkillName.Herding].Base >= 80)
                m.FollowersMax += 1;

            if (m.Skills[SkillName.Herding].Base >= 60)
                m.FollowersMax += 1;

            if ((m.Skills[SkillName.Veterinary].Base >= 120) && (m.Skills[SkillName.AnimalLore].Base >= 120) && (m.Skills[SkillName.AnimalTaming].Base >= 120))
                m.FollowersMax += 2;

            if ((m.Skills[SkillName.Veterinary].Base >= 100) && (m.Skills[SkillName.AnimalLore].Base >= 100) && (m.Skills[SkillName.AnimalTaming].Base >= 100))
                m.FollowersMax += 1;

            if ((m.Skills[SkillName.Veterinary].Base >= 80) && (m.Skills[SkillName.AnimalLore].Base >= 80) && (m.Skills[SkillName.AnimalTaming].Base >= 80))
                m.FollowersMax += 1;

            if ((m.Skills[SkillName.Veterinary].Base >= 60) && (m.Skills[SkillName.AnimalLore].Base >= 60) && (m.Skills[SkillName.AnimalTaming].Base >= 60))
                m.FollowersMax += 1;

            //                   This line is for adding back any ExtraSlots from other systems (deeds, perhaps).
            //                   Probably ought to move it to PlayerMobile.cs or move all Follower calcs from there to here.
            //                   m.FollowersMax += ((PlayerMobile)m).ExtraSlots;


        }


    }
}