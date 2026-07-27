using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Holds all mission definitions. Each mission is configurable with
/// unique dialog, enemy counts, and rewards.
/// </summary>
public static class MissionsDatabase
{
    /// <summary>
    /// All 16 missions. Index 0 = Mission 1 (the existing one),
    /// indices 1–15 are the new missions.
    /// </summary>
    public static IReadOnlyList<MissionDefinition> AllMissions => _missions;

    private static readonly MissionDefinition[] _missions = new MissionDefinition[]
    {
        // ===== MISSION 1 (index 0) =====
        new MissionDefinition(
            missionName: "Lettol City — First Contact",
            subtitle: "Clear the city sector",
            introDialogLines: new string[]
            {
                "Welcome to your first mission, soldier.",
                "This is Lettol City — once the crown jewel of Grore. Now it's a graveyard.",
                "The outbreak started three days ago. 80% of the population is already lost.",
                "Your objective: clear the sector. Every zombie you put down buys the survivors more time.",
                "We've lost too much ground already. Don't let them take any more.",
                "Battle commencing. Make every shot count."
            },
            completionDialogLines: new string[]
            {
                "??? : Impressive work, soldier. Didn't think you'd make it through that.",
                "You : Who are you? Show yourself.",
                "Contractor : Name's the Contractor. I'm the one who posted this job — and the one who signs your paychecks.",
                "Contractor : Every zombie you put down earns you coin. Every wave you clear, a bonus. Simple math.",
                "Contractor : The infection is spreading. The more you clear, the more we can push back.",
                "Contractor : Welcome to Zombie Contracting. It's a dirty job, but someone's gotta do it.",
                "Contractor : Your mission — clear every zone, collect every bounty, and take back this world one street at a time.",
                "Contractor : Use your earnings at the contract board to unlock new gear and upgrades. Stay sharp out there.",
                "Contractor : Check the mission terminal for available contracts. They're color-coded by difficulty.",
                "Contractor : That's enough talk. You've got work to do. Dismissed.",
                "",
                "--- MISSION 1 COMPLETE ---"
            },
            minEnemies: 10,
            maxEnemies: 15,
            coinReward: 50,
            battleIndex: 0
        ),

        // ===== MISSION 2 (index 1) =====
        new MissionDefinition(
            missionName: "Grore Subway — Rat Race",
            subtitle: "Clear the underground tunnels",
            introDialogLines: new string[]
            {
                "Contractor : The subway tunnels are crawling with infected. Survivors are trapped in the lower levels.",
                "Contractor : The tunnels are dark and tight — no room for mistakes.",
                "Contractor : We need those tunnels cleared so supply lines can move through.",
                "Your mission: purge the Grore subway system. Watch your corners.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : Subway's secure. Good work — you moved through those tunnels like a ghost.",
                "Contractor : The evac teams can finally move supplies through. You just saved a lot of lives.",
                "Contractor : Collect your pay. You've earned it.",
                "",
                "--- MISSION 2 COMPLETE ---"
            },
            minEnemies: 12,
            maxEnemies: 18,
            coinReward: 60,
            battleIndex: 1
        ),

        // ===== MISSION 3 (index 2) =====
        new MissionDefinition(
            missionName: "St. Mary's Hospital",
            subtitle: "Secure the medical district",
            introDialogLines: new string[]
            {
                "Radio : St. Mary's Hospital is under siege! We have wounded and doctors trapped inside!",
                "Contractor : The hospital is a critical asset. If we lose it, we lose medical support for the entire region.",
                "Contractor : We're getting reports of a large horde converging on the building.",
                "Your mission: push through to the hospital and clear the perimeter.",
                "The doctors are counting on you. Make every shot count.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Radio : The horde is retreating! Perimeter is secure! Thank you — you saved countless lives today.",
                "Contractor : The hospital is operational again. That's a win for the whole region.",
                "Contractor : Your reputation is growing. People are starting to hear about you.",
                "",
                "--- MISSION 3 COMPLETE ---"
            },
            minEnemies: 15,
            maxEnemies: 22,
            coinReward: 75,
            battleIndex: 2
        ),

        // ===== MISSION 4 (index 3) =====
        new MissionDefinition(
            missionName: "Westbrook Quarantine",
            subtitle: "Hold the quarantine line",
            introDialogLines: new string[]
            {
                "Contractor : Westbrook's quarantine wall is breached. Infected are pouring into the safe zone.",
                "Contractor : We have civilians trapped behind the breach. If we don't seal it, they're dead.",
                "Contractor : I'm sending you in to hold the line while engineering patches the wall.",
                "This is a defensive mission. Hold your ground. Don't let them through.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Engineer : The wall is sealed! Breach contained!",
                "Contractor : You held the line when it mattered most. The civilians in Westbrook owe you their lives.",
                "Contractor : That kind of work doesn't go unnoticed. Your pay reflects it.",
                "",
                "--- MISSION 4 COMPLETE ---"
            },
            minEnemies: 18,
            maxEnemies: 25,
            coinReward: 80,
            battleIndex: 1,
            isAvailable: false
        ),

        // ===== MISSION 5 (index 4) =====
        new MissionDefinition(
            missionName: "Riverside Docks",
            subtitle: "Secure the waterfront",
            introDialogLines: new string[]
            {
                "Contractor : The docks at Riverside are a strategic choke point. We need control of the waterfront.",
                "Contractor : Infected have overrun the shipping yards. There's also reports of a special infected — something big.",
                "Contractor : Clear the docks, secure the warehouses, and hold the pier.",
                "This is a big operation. Stay sharp, stay alive.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : Docks are ours. The supply routes via the river are now open.",
                "Contractor : That 'big one' you put down? That was an Alpha. They're rare — and dangerous.",
                "Contractor : Word is spreading. More contracts are coming your way.",
                "",
                "--- MISSION 5 COMPLETE ---"
            },
            minEnemies: 20,
            maxEnemies: 28,
            coinReward: 100,
            battleIndex: 2,
            isAvailable: false
        ),

        // ===== MISSION 6 (index 5) =====
        new MissionDefinition(
            missionName: "Pinewood Estates",
            subtitle: "Evacuate the survivors",
            introDialogLines: new string[]
            {
                "Radio : Pinewood Estates! We have survivors pinned down in the residential complex!",
                "Contractor : There's a group of about 30 civilians holed up in the Estates. They've been cut off for days.",
                "Contractor : Supplies are running low and the infected are closing in.",
                "Your mission: push through the estates, clear a path, and hold the evacuation point.",
                "The extraction team is on standby. Move out.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Radio : Survivors are extracted! All 32 civilians are safe!",
                "Contractor : 32 lives saved because of you. That's what this is all about.",
                "Contractor : You're becoming something of a legend out here. Don't let it go to your head.",
                "",
                "--- MISSION 6 COMPLETE ---"
            },
            minEnemies: 15,
            maxEnemies: 20,
            coinReward: 90,
            battleIndex: 0,
            isAvailable: false
        ),

        // ===== MISSION 7 (index 6) =====
        new MissionDefinition(
            missionName: "Old Town Market",
            subtitle: "Clear the market district",
            introDialogLines: new string[]
            {
                "Contractor : The Old Town Market was a refugee hub until three days ago. Now it's a feeding ground.",
                "Contractor : Satellites show a massive congregation of infected in the market square.",
                "Contractor : We need that area cleared so we can set up a forward operating base.",
                "This is a hot zone. Expect heavy resistance.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : Market square is clear. The FOB can be established.",
                "Contractor : That position will give us a foothold in the eastern districts. Strategic win.",
                "Contractor : You're making a real difference out here. Keep it up.",
                "",
                "--- MISSION 7 COMPLETE ---"
            },
            minEnemies: 22,
            maxEnemies: 30,
            coinReward: 110,
            battleIndex: 1,
            isAvailable: false
        ),

        // ===== MISSION 8 (index 7) =====
        new MissionDefinition(
            missionName: "Blackwood Forest",
            subtitle: "Investigate the crash site",
            introDialogLines: new string[]
            {
                "Radio : A supply chopper went down over Blackwood Forest! Pilot's beacon is still active!",
                "Contractor : The forest is dense and we can't get a clear visual. Whatever's down there is on foot.",
                "Contractor : The pilot might still be alive. We need you to push through and secure the crash site.",
                "The forest is treacherous. Infected, wildlife, and limited visibility.",
                "Find the pilot. Secure the supplies. Get out.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Radio : Pilot recovered! Crash site secured!",
                "Contractor : Against all odds, you pulled it off. The pilot is safe and the supplies are recovered.",
                "Contractor : That was a tough op. You're proving yourself mission after mission.",
                "",
                "--- MISSION 8 COMPLETE ---"
            },
            minEnemies: 12,
            maxEnemies: 18,
            coinReward: 85,
            battleIndex: 0,
            isAvailable: false
        ),

        // ===== MISSION 9 (index 8) =====
        new MissionDefinition(
            missionName: "Central Station",
            subtitle: "Take the railway hub",
            introDialogLines: new string[]
            {
                "Contractor : Central Station is the railway hub for the entire region. If we control it, we control movement.",
                "Contractor : The infected have fortified the station. It's a nest.",
                "Contractor : We need it cleared, no matter the cost.",
                "This is a high-priority target. Eliminate every infected in the station.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : Central Station is ours. The railway lines are now operational.",
                "Contractor : This opens up supply routes across three districts. Massive strategic gain.",
                "Contractor : You're not just a soldier anymore. You're a force of nature.",
                "",
                "--- MISSION 9 COMPLETE ---"
            },
            minEnemies: 25,
            maxEnemies: 35,
            coinReward: 130,
            battleIndex: 2,
            isAvailable: false
        ),

        // ===== MISSION 10 (index 9) =====
        new MissionDefinition(
            missionName: "Sunset Boulevard",
            subtitle: "Clear the main artery",
            introDialogLines: new string[]
            {
                "Contractor : Sunset Boulevard is the main road connecting the east and west sectors.",
                "Contractor : It's completely overrun. Nothing moves through there except the infected.",
                "Contractor : We need the boulevard cleared so convoys can move supplies across the city.",
                "This is a full-scale combat operation. Expect heavy resistance across the entire stretch.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : Sunset Boulevard is clear. Convoys are moving through as we speak.",
                "Contractor : The east and west sectors are now connected. This is a turning point.",
                "Contractor : You're changing the course of this outbreak. Remember that.",
                "",
                "--- MISSION 10 COMPLETE ---"
            },
            minEnemies: 28,
            maxEnemies: 38,
            coinReward: 140,
            battleIndex: 1,
            isAvailable: false
        ),

        // ===== MISSION 11 (index 10) =====
        new MissionDefinition(
            missionName: "Grande Hotel",
            subtitle: "Rescue the VIP",
            introDialogLines: new string[]
            {
                "Contractor : We have a high-value target trapped in the Grande Hotel. A scientist who was working on a cure.",
                "Contractor : Dr. Elena Vasquez. She's one of the leading minds in virology. We CANNOT lose her.",
                "Contractor : The hotel is swarming. She's on the top floor with her security detail.",
                "Your mission: fight through the hotel, extract Dr. Vasquez, and get her to the evacuation point.",
                "This is the most important mission you've run. Don't fail.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Radio : VIP secured! Dr. Vasquez is safe and en route to the safe zone!",
                "Contractor : You just saved the woman who might cure this entire plague. I don't have words.",
                "Contractor : Humanity owes you a debt that can never be repaid. But I can try — with a bonus.",
                "",
                "--- MISSION 11 COMPLETE ---"
            },
            minEnemies: 20,
            maxEnemies: 28,
            coinReward: 160,
            battleIndex: 2,
            isAvailable: false
        ),

        // ===== MISSION 12 (index 11) =====
        new MissionDefinition(
            missionName: "Iron Bridge",
            subtitle: "Hold the crossing",
            introDialogLines: new string[]
            {
                "Contractor : The Iron Bridge is the last crossing point over the Grore River. If we lose it, the north is cut off.",
                "Contractor : A massive horde is moving toward the bridge as we speak. We need you to hold the line.",
                "Contractor : Reinforcements won't arrive for 30 minutes. You need to survive until they do.",
                "This is a siege. Dig in and don't let them cross.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Reinforcements : Bridge is secure! We're here! You held them off!",
                "Contractor : You held the Iron Bridge against impossible odds. The northern sector is safe because of you.",
                "Contractor : That was legendary. Absolutely legendary.",
                "",
                "--- MISSION 12 COMPLETE ---"
            },
            minEnemies: 30,
            maxEnemies: 40,
            coinReward: 150,
            battleIndex: 1,
            isAvailable: false
        ),

        // ===== MISSION 13 (index 12) =====
        new MissionDefinition(
            missionName: "Abandoned Zoo",
            subtitle: "Secure the facility",
            introDialogLines: new string[]
            {
                "Contractor : We're getting strange readings from the old zoo. Heat signatures that don't match normal infected.",
                "Contractor : The zoo was used for genetic research before the outbreak. Some animals might have been... exposed.",
                "Contractor : We need you to go in, investigate, and neutralize any threats.",
                "This is a recon-and-clear mission. Be prepared for the unexpected.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : The zoo is secure. Your reports of infected animals are... concerning. But we have the data we need.",
                "Contractor : This changes things. We need to prepare for infected wildlife.",
                "Contractor : Good work in there. That couldn't have been easy.",
                "",
                "--- MISSION 13 COMPLETE ---"
            },
            minEnemies: 18,
            maxEnemies: 25,
            coinReward: 95,
            battleIndex: 0,
            isAvailable: false
        ),

        // ===== MISSION 14 (index 13) =====
        new MissionDefinition(
            missionName: "Silver Lake Dam",
            subtitle: "Prevent catastrophe",
            introDialogLines: new string[]
            {
                "Contractor : The Silver Lake Dam is compromised. If it fails, the entire valley floods — infected and all.",
                "Contractor : Infected have overrun the dam facility. We need you to clear it so engineers can stabilize the structure.",
                "Contractor : If the dam breaks, we lose everything. The city, the safe zones, everything.",
                "This is a race against time. Move fast and hit hard.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Engineer : Dam controls are secure! We're stabilizing the structure now!",
                "Contractor : The valley is safe. You prevented a catastrophe that would have killed thousands.",
                "Contractor : I don't say this often, but I'm proud to have you on the team.",
                "",
                "--- MISSION 14 COMPLETE ---"
            },
            minEnemies: 22,
            maxEnemies: 30,
            coinReward: 120,
            battleIndex: 2,
            isAvailable: false
        ),

        // ===== MISSION 15 (index 14) =====
        new MissionDefinition(
            missionName: "Corporate HQ",
            subtitle: "Storm the headquarters",
            introDialogLines: new string[]
            {
                "Contractor : We've traced the source of the outbreak to a corporate research facility downtown.",
                "Contractor : Grore Pharmaceuticals HQ. They were working on something they shouldn't have been.",
                "Contractor : We need you to fight to the top floor and retrieve any research data you can find.",
                "This is a deep infiltration. The building will be swarming with infected — and maybe something worse.",
                "The truth about this outbreak is somewhere in that building. Find it.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "Contractor : You found the research data. This changes everything.",
                "Contractor : Grore Pharmaceuticals was covering something up. Something that started all of this.",
                "Contractor : With this data, we might finally have a chance at stopping this nightmare.",
                "This isn't over. But thanks to you, we're closer than ever.",
                "",
                "--- MISSION 15 COMPLETE ---"
            },
            minEnemies: 25,
            maxEnemies: 35,
            coinReward: 175,
            battleIndex: 1,
            isAvailable: false
        ),

        // ===== MISSION 16 (index 15) =====
        new MissionDefinition(
            missionName: "The Final Stand",
            subtitle: "End this nightmare",
            introDialogLines: new string[]
            {
                "Contractor : This is it. The final operation.",
                "Contractor : We've tracked the Alpha Hive to the old stadium. If we destroy the hive, the infection loses its anchor.",
                "Contractor : Every contract, every mission, every bullet you've fired has led to this moment.",
                "The stadium is ground zero. The hive will be defended by every infected in the city.",
                "This is the end. Make it count.",
                "Battle commencing."
            },
            completionDialogLines: new string[]
            {
                "The Alpha Hive collapses. A shockwave of energy ripples through the stadium.",
                "For the first time in months, the air is silent.",
                "Contractor : ...You did it. The hive is destroyed.",
                "Contractor : The infection will start to recede. It's not over, but... we can see the light now.",
                "Contractor : Welcome to the world after the nightmare. You earned it.",
                "",
                "--- ALL MISSIONS COMPLETE ---",
                "Congratulations. You've cleared every contract. The world can begin to heal."
            },
            minEnemies: 35,
            maxEnemies: 50,
            coinReward: 250,
            battleIndex: 2,
            isAvailable: false
        )
    };

    /// <summary>
    /// Get a mission by its index (0–15).
    /// </summary>
    public static MissionDefinition GetMission(int index)
    {
        if (index < 0 || index >= _missions.Length)
        {
            Debug.LogWarning($"Mission index {index} out of range. Defaulting to mission 0.");
            return _missions[0];
        }
        return _missions[index];
    }

    /// <summary>
    /// Total number of missions defined.
    /// </summary>
    public static int MissionCount => _missions.Length;
}
