﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SQADemicApp.BL;
using SQADemicApp;
using System.IO;
using System.Linq;

namespace SQADemicAppTest
{
    [TestClass]
    public class GameBoardModelsTest
    {
        List<Card> _returnedList = new List<Card>();
        List<Card> _cardList = new List<Card>();
        //Create createTestClass = new Create();

 /**       [TestInitialize]
        public void setup()
        {
            Create.setUpCreate();
        }**/

        #region cardList
        [TestMethod]
        public void TestEasyDifficultyEpidemicCards()
        {
            string[] playerRoles = {"Medic", "Scientist"};
            Card[] playerDeck = Create.MakePlayerDeck(DifficultySetting.Easy, playerRoles);
            List<Card> cards =  new List<Card>(playerDeck);
            int countEpidemic = cards.Count(m => m.CardType == Card.Cardtype.Epidemic);
            Assert.AreEqual(countEpidemic, 4);
        }

        [TestMethod]
        public void TestNormalDifficultyEpidemicCards()
        {
            string[] playerRoles = { "Medic", "Scientist" };
            Card[] playerDeck = Create.MakePlayerDeck(DifficultySetting.Medium, playerRoles);
            List<Card> cards = new List<Card>(playerDeck);
            int countEpidemic = cards.Count(m => m.CardType == Card.Cardtype.Epidemic);
            Assert.AreEqual(countEpidemic, 5);
        }

        [TestMethod]
        public void TestHeroicDifficultyEpidemicCards()
        {
            string[] playerRoles = { "Medic", "Scientist" };
            Card[] playerDeck = Create.MakePlayerDeck(DifficultySetting.Hard, playerRoles);
            List<Card> cards = new List<Card>(playerDeck);
            int countEpidemic = cards.Count(m => m.CardType == Card.Cardtype.Epidemic);
            Assert.AreEqual(countEpidemic, 6);
        }

        [TestMethod]
        public void TestThatCardListCorrectOneItem()
        {
            _cardList = new List<Card>();
            _cardList.Add(new Card("test", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("Airlift", Card.Cardtype.Special));
            _cardList.Add(new Card("One Quiet Night", Card.Cardtype.Special));
            _cardList.Add(new Card("Resilient Population", Card.Cardtype.Special));
            _cardList.Add(new Card("Government Grant", Card.Cardtype.Special));
            _cardList.Add(new Card("Forecast", Card.Cardtype.Special));
            _returnedList = Create.MakeCardList(new System.IO.StringReader("test; blue"));
            Assert.IsTrue(_cardList[0].Equals(_returnedList[0]));
        }
        [TestMethod]
        public void TestThatCardListCorrectTwoItem()
        {
            _cardList = new List<Card>();
            _cardList.Add(new Card("test", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("test2", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("Airlift", Card.Cardtype.Special));
            _cardList.Add(new Card("One Quiet Night", Card.Cardtype.Special));
            _cardList.Add(new Card("Resilient Population", Card.Cardtype.Special));
            _cardList.Add(new Card("Government Grant", Card.Cardtype.Special));
            _cardList.Add(new Card("Forecast", Card.Cardtype.Special));
            _returnedList = Create.MakeCardList(new System.IO.StringReader("test; blue\ntest2; blue"));
            CollectionAssert.AreEqual(_cardList, _returnedList);
        }
        [TestMethod]
        public void TestThatCardListCorrectThreeItem()
        {
            _cardList = new List<Card>();
            _cardList.Add(new Card("test", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("test2", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("test3", Card.Cardtype.City, Color.Blue));
            _cardList.Add(new Card("Airlift", Card.Cardtype.Special));
            _cardList.Add(new Card("One Quiet Night", Card.Cardtype.Special));
            _cardList.Add(new Card("Resilient Population", Card.Cardtype.Special));
            _cardList.Add(new Card("Government Grant", Card.Cardtype.Special));
            _cardList.Add(new Card("Forecast", Card.Cardtype.Special));
            _returnedList = Create.MakeCardList(new System.IO.StringReader("test; blue\ntest2; blue\ntest3; blue"));
            CollectionAssert.AreEqual(_cardList, _returnedList);
        }
#endregion

        #region testColor
        [TestMethod]
        public void TestThatGetsCorrectColorRed()
        {
            PlayerDeck d = new PlayerDeck(DifficultySetting.Easy, 2);
            Assert.AreEqual(Color.Red, d.getColor("red"));
        }
        [TestMethod]
        public void TestThatGetsCorrectColorsRedBlue()
        {
            PlayerDeck d = new PlayerDeck(DifficultySetting.Easy, 2);
            Assert.AreEqual(Color.Red, d.getColor("red"));
            Assert.AreEqual(Color.Blue, d.getColor("blue"));
        }
        [TestMethod]
        public void TestThatGetsCorrectThreeColors()
        {
            PlayerDeck d = new PlayerDeck(DifficultySetting.Easy, 2);
            Assert.AreEqual(Color.Red, d.getColor("red"));
            Assert.AreEqual(Color.Blue, d.getColor("blue"));
            Assert.AreEqual(Color.Black, d.getColor("black"));
        }
        [TestMethod]
        public void TestThatGetsCorrectAllColors()
        {
            PlayerDeck d = new PlayerDeck(DifficultySetting.Easy, 2);
            Assert.AreEqual(Color.Red, d.getColor("red"));
            Assert.AreEqual(Color.Blue, d.getColor("blue"));
            Assert.AreEqual(Color.Black, d.getColor("black"));
            Assert.AreEqual(Color.Yellow, d.getColor("yellow"));
        }
        #endregion

        #region incrementPlayerTurn
        [TestMethod]
        public void TestThatPlayerTurnIncremented()
        {
            string[] players = {"Dispatcher","Scientist"};
            GameBoardModels model = new GameBoardModels(players);
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            Assert.AreEqual(1, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].GetTurnCount());
        }
        [TestMethod]
        public void TestThatPlayerTurnsResetAfter4Moves()
        {
            string[] players = { "Dispatcher", "Scientist" };
            GameBoardModels model = new GameBoardModels(players);
            // four moves in a player turn
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            Assert.AreEqual(0, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].GetTurnCount());
        }
        #endregion

        #region nextPlayer
        
        [TestMethod]
        public void TestThatPlayerSwitches()
        {
            string[] players = { "Dispatcher", "Scientist" };
            GameBoardModels model = new GameBoardModels(players);
            // four moves in a player turn
            Assert.AreEqual(false, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount());
            Assert.AreEqual(false, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount()); 
            Assert.AreEqual(false, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount());
            Assert.AreEqual(true, GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount());
            //Assert.AreEqual(2, GameBoardModels.CurrentPlayerIndex);
        }
        [TestMethod]
        public void TestFullPlayerRotation()
        {
            string[] players = { "Dispatcher", "Scientist" };
            GameBoardModels model = new GameBoardModels(players);
            // four moves in a player turn
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            // moves for player 2 turn
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            GameBoardModels.Players[GameBoardModels.CurrentPlayerIndex].IncrementTurnCount();
            Assert.AreEqual(0, GameBoardModels.CurrentPlayerIndex);
        }
        #endregion

        #region InfectionDeck
        [TestMethod]
        public void TestInfectionDeckCorrectLength()
        {
            List<string> ls;
            StringReader r = new StringReader("New York,Montreal,Washington,London,Madrid");
            ls = Create.MakeInfectionDeck(r);
            Assert.AreEqual(5, ls.Count);
        }

        [TestMethod]
        public void TestInfectionDeckDoesntHaveDuplicates()
        {
            List<string> ls;
            StringReader r = new StringReader("New York,Montreal,Washington,London,Madrid");
            ls = Create.MakeInfectionDeck(r);
            HashSet<string> hash = new HashSet<string>(ls);
            Assert.AreEqual(5, hash.Count);
        }
        #endregion

        #region InfectionRate
        [TestMethod]
        public void TestDifficultyInfectionRate()
        {
            string[] players = { "Dispatcher", "Scientist" };
            GameBoardModels.Difficulty = DifficultySetting.Easy;
            GameBoardModels model = new GameBoardModels(players);
            Assert.AreEqual(2, GameBoardModels.InfectionRate);
            GameBoardModels.Difficulty = DifficultySetting.Medium;
            model = new GameBoardModels(players);
            Assert.AreEqual(2, GameBoardModels.InfectionRate);
            GameBoardModels.Difficulty = DifficultySetting.Hard;
            model = new GameBoardModels(players);
            Assert.AreEqual(3, GameBoardModels.InfectionRate);
        }
        #endregion
    }
}
