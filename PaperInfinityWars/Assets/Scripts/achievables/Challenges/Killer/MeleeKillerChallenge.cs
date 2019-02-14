using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Challenges
{
    class MeleeKillerChallenge : ChallengeBase
    {
        int[] Goals = new int[5] {25, 50, 100, 250, 500};
        int[] Rewards = new int[5] {10, 15, 25, 50, 100};
        
        int progress = 0;

        public MeleeKillerChallenge()
        {
            challengeID = 1;
            highestTier = 5;
            challengeName = "Melee killer";
            challengeDescription = "Get {0} kills with melee weapons";
            challengeCategory = ChallengeCategory.Killer;
            GameManager.instance.eventManager.OnLoad.AddListener(OnLoad);
            GetNotification();
        }

        public override void RegisterForEvents()
        {
            GameManager.instance.eventManager.EnemyDeath.AddListener(OnEnemyDeath);
            GameManager.instance.eventManager.OnSave.AddListener(OnSave);
        }

        public override void DeRegisterForEvents()
        {
            GameManager.instance.eventManager.EnemyDeath.RemoveListener(OnEnemyDeath);
            GameManager.instance.eventManager.OnLoad.RemoveListener(OnLoad);
            GameManager.instance.eventManager.OnSave.RemoveListener(OnSave);
        }

        void OnEnemyDeath(KillablePawn oVictim, KillablePawn oKiller, Weapon oWeapon)
        {
            if (oWeapon.weaponType == Weapon.WeaponType.Melee)
            {
                progress ++;
                if (progress == Goals[currentTier])
                {
                    GameManager.instance.eventManager.XPDrop.Invoke(Rewards[currentTier]);
                    Notification notification = new Notification();
                    GameManager.instance.eventManager.ShowNotification.Invoke(notificationObject);
                    currentTier++;
                    if (currentTier == highestTier)
                    {
                        completed = true;
                    }
                }
            }
        }

        public override int GetProgress()
        {
            return progress;
        }

        public override int GetReward()
        {
            if (currentTier != highestTier)
            {
                return Rewards[currentTier];
            }
            else
            {
                return 0;
            }
        }

        public override int GetGoal()
        {
            if (currentTier != highestTier)
            {
                return Goals[currentTier];
            }
            else
            {
                return Goals[highestTier-1];
            }
        }

        public override string GetDescription()
        {
            if (completed)
                return string.Format(challengeDescription, Goals[currentTier - 1]);
            else
                return string.Format(challengeDescription, Goals[currentTier]);
        }

        void OnLoad(SaveData oSaveData)
        {
            if (oSaveData.challenges.Count != 0)
            {
                ChallengeProgress challengeProgress = oSaveData.challenges.Find(X => X.challengeID == this.challengeID);
                if (challengeProgress != null)
                {
                    this.currentTier = challengeProgress.currenttier;
                    this.progress = challengeProgress.progress;
                    this.completed = challengeProgress.Completed;
                }
            }
        }

        void OnSave()
        {
            ChallengeProgress challengeProgress = new ChallengeProgress();
            challengeProgress.challengeID = this.challengeID;
            challengeProgress.currenttier = this.currentTier;
            challengeProgress.progress = this.progress;
            challengeProgress.Completed = this.completed;
            GameManager.instance.savegameManager.saveData.challenges.RemoveAll(X => X.challengeID == this.challengeID);
            GameManager.instance.savegameManager.saveData.challenges.Add(challengeProgress);

        }
    }
}
