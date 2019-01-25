using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Challenges;

namespace Challenges
{
    public class ChallengeManager : MonoBehaviour
    {
        List<ChallengeBase> challenges = new List<ChallengeBase>();

        //required levels for unlocking challenges
        public int careerLevel = 6;
        public int killerLevel = 10;
        public int survivalLevel = 10;
        bool initialunlock = true;

        public void Initialize()
        {
            GameManager.instance.eventManager.LevelUp.AddListener(OnLevelUp);

            //adding challenges
            //carreer challenges
            challenges.Add(new CareerKillerChallenge());

            //killer challenges
            challenges.Add(new MeleeKillerChallenge());

            //survivalChallenges
            challenges.Add(new WaveSurviverChallenge());
            challenges.Add(new WaveMasterChallenge());
        }

        void OnEnable()
        {
            if (initialunlock)
            {
                int lvl = GameManager.instance.currencyManager.GetLevel();
                if (lvl > 0)
                {
                    UnlockUntillLevel(lvl);
                    initialunlock = false;
                }
            }
        }

        void OnDisable()
        {
            GameManager.instance.eventManager.LevelUp.RemoveListener(OnLevelUp);
        }

        void OnLevelUp(CurrencyManager oCurrencyManager)
        {
            UnlockForLevel(oCurrencyManager.GetLevel());
        }

        void UnlockForLevel(int oLevel)
        {
            foreach(ChallengeBase challenge in challenges)
            {
                switch (challenge.challengeCategory)
                {
                    case ChallengeBase.ChallengeCategory.Career:
                        if (oLevel == careerLevel)
                            challenge.RegisterForEvents();
                        break;
                    case ChallengeBase.ChallengeCategory.Killer:
                        if (oLevel == killerLevel)
                            challenge.RegisterForEvents();
                        break;
                    case ChallengeBase.ChallengeCategory.Survival:
                        if (oLevel == survivalLevel)
                            challenge.RegisterForEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        void UnlockUntillLevel(int oLevel)
        {
            Debug.Log("unlocked till level " + oLevel);
            foreach (ChallengeBase challenge in challenges)
            {
                switch (challenge.challengeCategory)
                {
                    case ChallengeBase.ChallengeCategory.Career:
                        if (oLevel >= careerLevel)
                            challenge.RegisterForEvents();
                        break;
                    case ChallengeBase.ChallengeCategory.Killer:
                        if (oLevel >= killerLevel)
                            challenge.RegisterForEvents();
                        break;
                    case ChallengeBase.ChallengeCategory.Survival:
                        if (oLevel >= survivalLevel)
                            challenge.RegisterForEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        public List<ChallengeBase> GetAllChallenges()
        {
            return challenges;
        }

        public ChallengeBase GetChallengeByID(int iChallengeID)
        {
            return challenges.Find(X => X.challengeID == iChallengeID);
        }
    }
}
