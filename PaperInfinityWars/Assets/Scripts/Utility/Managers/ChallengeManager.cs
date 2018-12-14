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
        int bootcampLevel = 7;
        int killerLevel = 10;
        int survivalLevel = 15;
        bool initialunlock = true;

        public void Initialize()
        {
            Debug.Log("challengemanager enabled!");
            GameManager.instance.eventManager.LevelUp.AddListener(OnLevelUp);
            challenges.Add(new MeleeKillerChallenge());
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
                    case ChallengeBase.ChallengeCategory.Bootcamp:
                        if (oLevel == bootcampLevel)
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
                    case ChallengeBase.ChallengeCategory.Bootcamp:
                        if (oLevel >= bootcampLevel)
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
    }
}
