using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenges
{
    class WaveMasterChallenge : ChallengeBase
    {
        int Goal = 25;
        int Reward = 200;

        int progress = 0;

        public WaveMasterChallenge()
        {
            challengeID = 3;
            highestTier = 1;
            challengeName = "Wave master";
            challengeDescription = "Survive {0} rounds in 1 game";
            challengeCategory = ChallengeCategory.Survival;
            GameManager.instance.eventManager.OnLoad.AddListener(OnLoad);
            GetNotification();
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
            if (round == Goal)
            {
                GameManager.instance.eventManager.XPDrop.Invoke(Reward);
                Notification notification = new Notification();
                GameManager.instance.eventManager.ShowNotification.Invoke(notificationObject);
                currentTier++;
                completed = true;
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
                return Reward;
            }
            else
            {
                return 0;
            }
        }

        public override int GetGoal()
        {
            return Goal;
        }

        public override string GetDescription()
        {
            return string.Format(challengeDescription, Goal);
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
