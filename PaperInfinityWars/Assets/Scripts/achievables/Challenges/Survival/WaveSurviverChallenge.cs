using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenges
{
    class WaveSurviverChallenge : ChallengeBase
    {
        int[] Goals = new int[5] { 10, 25, 50, 100, 250 };
        int[] Rewards = new int[5] { 10, 15, 25, 50, 100 };

        int progress = 0;

        public WaveSurviverChallenge()
        {
            challengeID = 2;
            highestTier = 5;
            challengeName = "Wave rider";
            challengeDescription = "Survive a total of {0} waves without dying";
            challengeCategory = ChallengeCategory.Survival;
            GameManager.instance.eventManager.OnLoad.AddListener(OnLoad);
        }

        public override void RegisterForEvents()
        {
            GameManager.instance.eventManager.WMOnRoundEnd.AddListener(OnRoundEnd);
            GameManager.instance.eventManager.OnSave.AddListener(OnSave);
        }

        public override void DeRegisterForEvents()
        {
            GameManager.instance.eventManager.WMOnRoundEnd.RemoveListener(OnRoundEnd);
            GameManager.instance.eventManager.OnLoad.RemoveListener(OnLoad);
            GameManager.instance.eventManager.OnSave.RemoveListener(OnSave);
        }

        void OnRoundEnd(int round)
        {
            progress++;
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
                return Goals[highestTier - 1];
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
