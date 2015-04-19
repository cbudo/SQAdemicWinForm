﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQADemicApp.BL
{
    public class PlayerActionsBL
    {
        /// <summary>
        /// Finds the possible cities a player can move to
        /// </summary>
        /// <param name="currentCity"></param>
        /// <returns>List of Cities</returns>
        public static HashSet<City> DiveOptions(City currentCity)
        {
            return currentCity.getAdjacentCities();
        }

        /// <summary>
        /// Finds the possible cities a player can fly to by burning a card
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="currentCity"></param>
        /// <returns></returns>
        public static List<String> DirectFlightOption(List<GameBoardModels.Card> hand, City currentCity)
        {
            var reducedHand = hand.Where(c => !c.CityName.Equals(currentCity.Name) && c.CardType == GameBoardModels.CARDTYPE.City );

            var returnlist = new List<String>();
            foreach (GameBoardModels.Card card in reducedHand)
            {
                returnlist.Add(card.CityName);
            }

            return returnlist;
        }

        /// <summary>
        /// Determins if the player can use a Charter Flight
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="currentCity"></param>
        /// <returns></returns>
        public static bool CharterFlightOption(List<GameBoardModels.Card> hand, City currentCity)
        {
            return (hand.Where(c => c.CityName == currentCity.Name).Count() == 1);
        }

        public static bool ShuttleFlightOption(List<GameBoardModels.Card> hand, City currentCity)
        {
            //need list of cities that have research centers
            return false;
        }

        public static bool BuildAResearchStationOption(List<GameBoardModels.Card> hand, City currentCity, PlayerModels.ROLE role)
        {
            //need list of cities with reaserch centers for:
            //count
            //don't double build

            return false;
        }

        public static bool CureOption(List<GameBoardModels.Card> hand, City currentCity, PlayerModels.ROLE role)
        {
            //need a list of existing cures
            return false;
        }

        public static bool TreatDiseaseOption(City currentCity, PlayerModels.ROLE role)
        {
            //need list of cures
            return false;
        }

        public static bool ShareKnowledgeOption(City currentCity, PlayerModels.ROLE role)
        {
            //need all of the players
            return false;
        }




    }
}
