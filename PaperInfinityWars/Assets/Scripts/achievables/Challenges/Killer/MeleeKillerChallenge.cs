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
        int currentTier = 0;
        bool completed = false;

        public MeleeKillerChallenge()
        {
            challengeID = 1;
            challengeName = "Melee Killer";
            challengeDescription = "Get {0} kills with melee weapons";
            challengeCategory = ChallengeCategory.Killer;
            GameManager.instance.eventManager.OnLoad.AddListener(OnLoad);
            Debug.Log(challengeName + " instantiated");
            GetNotification();
        }

        public override void RegisterForEvents()
        {
            GameManager.instance.eventManager.EnemyDeath.AddListener(OnEnemyDeath);
            GameManager.instance.eventManager.OnSave.AddListener(OnSave);
            Debug.Log("registered for progress!");
        }

        public override void DeRegisterForEvents()
        {
            GameManager.instance.eventManager.EnemyDeath.RemoveListener(OnEnemyDeath);
            GameManager.instance.eventManager.OnLoad.RemoveListener(OnLoad);
            GameManager.instance.eventManager.OnSave.RemoveListener(OnSave);
        }

        void OnEnemyDeath(KillablePawn oVictim, KillablePawn oKiller, Weapon oWeapon)
        {
            Debug.Log("kill registered!");
            if (oWeapon.weaponType == Weapon.WeaponType.Melee)
            {
                progress += 1;
                if (progress == Goals[currentTier])
                {
                    Debug.Log("kill registered!");
                    GameManager.instance.eventManager.XPDrop.Invoke(Rewards[currentTier]);
                    Notification notification = new Notification();
                    GameManager.instance.eventManager.ShowNotification.Invoke(notificationObject);
                    currentTier++;
                }
            }
        }

        public override void GetProgress()
        {
            throw new NotImplementedException();
        }

        public override void GetCurrentTier()
        {
            throw new NotImplementedException();
        }

        void OnLoad(SaveData oSaveData)
        {
            if (oSaveData.challenges.Count != 0)
            {
                ChallengeProgress challengeProgress = oSaveData.challenges.Find(X => X.challengeID == this.challengeID);
                this.currentTier = challengeProgress.currenttier;
                this.progress = challengeProgress.progress;
                this.completed = challengeProgress.Completed;
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
