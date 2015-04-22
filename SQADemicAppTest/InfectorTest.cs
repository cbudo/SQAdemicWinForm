﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQADemicApp.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using SQADemicApp;

namespace SQADemicAppTest
{
    [TestClass]
    public class InfectorTest
    {
        private LinkedList<String> deck;
        private LinkedList<String> pile;
        private int infectionRate;
        private int infectionIndex;

        [TestInitialize]
        public void SetUpArrays()
        {
            GameBoardModels.outbreakMarker = 0;
            deck = new LinkedList<string>();
            pile = new LinkedList<string>();
            infectionRate = 3;
            infectionIndex = 4;
        }

        [TestMethod]
        public void TestInfectTwoCities()
        {
            deck.AddFirst("Saint Petersburg");
            deck.AddFirst("Sydney");
            List<String> removedCities = InfectorBL.InfectCities(deck, pile, 2);
            List<String> answer = new List<string> { "Sydney", "Saint Petersburg" };
            CollectionAssert.AreEqual(answer, removedCities);
        }

        [TestMethod]
        public void TestInfectThreeCities()
        {
            deck.AddFirst("Saint Petersburg");
            deck.AddFirst("Sydney");
            deck.AddFirst("New York");
            List<String> removedCities = InfectorBL.InfectCities(deck, pile, 3);
            List<String> answer = new List<string> { "New York", "Sydney", "Saint Petersburg" };
            CollectionAssert.AreEqual(answer, removedCities);
        }

        [TestMethod]
        public void TestInfectTwoCitiesTwice()
        {
            deck.AddFirst("Saint Petersburg");
            deck.AddFirst("Sydney");
            List<String> removedCities = InfectorBL.InfectCities(deck, pile, 2);
            List<String> answer = new List<string> { "Sydney", "Saint Petersburg" };
            CollectionAssert.AreEqual(answer, removedCities);
            deck.AddFirst("New York");
            deck.AddFirst("Chicago");
            removedCities = InfectorBL.InfectCities(deck, pile, 2);
            answer = new List<string> { "Chicago", "New York" };
            CollectionAssert.AreEqual(answer, removedCities);

        }

        [TestMethod]
        public void TestInfectTwoCitiesUpdatePile()
        {
            deck.AddFirst("Saint Petersburg");
            deck.AddFirst("Sydney");
            pile = new LinkedList<String>();
            List<String> removedCities = InfectorBL.InfectCities(deck, pile, 2);
            LinkedList<String> answer = new LinkedList<string>();
            answer.AddFirst("Sydney");
            answer.AddFirst("Saint Petersburg");
            CollectionAssert.AreEqual(answer, pile);
        }

        [TestMethod]
        public void TestEpidemicIncreaseInfectionCounter2to2()
        {
            //InfectionRates = { 2, 2, 2, 3, 3, 4, 4 };
            deck.AddFirst("Chicago");
            infectionIndex = 1;
            InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(2, infectionRate);
        }

        [TestMethod]
        public void TestEpidemicIncreaseInfectionCounter2to3()
        {
            //InfectionRates = { 2, 2, 2, 3, 3, 4, 4 };
            deck.AddFirst("Chicago");
            infectionIndex = 2;
            InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(3, infectionRate);
        }

        [TestMethod]
        public void TestEpidemicIncreaseInfectionCounter3to4()
        {
            //InfectionRates = { 2, 2, 2, 3, 3, 4, 4 };
            deck.AddFirst("Chicago");
            infectionRate = 3;
            infectionIndex = 4;
            InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(4, infectionRate);
        }

        [TestMethod]
        public void TestEpidemicIncreaseInfectionIndex()
        {
            deck.AddFirst("Chicago");
            infectionRate = 3;
            infectionIndex = 4;
            InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(5, infectionIndex);
        }

        [TestMethod]
        public void TestEpidemicDrawLastCardChicago()
        {
            deck.AddFirst("Chicago");
            deck.AddFirst("New York");
            string lastCity = InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual("Chicago", lastCity);
        }

        [TestMethod]
        public void TestEpidemicDrawLastCardNewYork()
        {
            deck.AddFirst("New York");
            deck.AddFirst("Chicago");
            string lastCity = InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual("New York", lastCity);
        }

        [TestMethod]
        public void TestEpidemicEmptyPileOnTopDeck()
        {
            deck = new LinkedList<string>();
            pile = new LinkedList<string>();
            deck.AddFirst("New York");
            deck.AddFirst("Chicago");
            pile.AddFirst("Saint Petersburg");
            pile.AddFirst("Sydney");
            InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(0, pile.Count);
            Assert.AreEqual(4, deck.Count);

            //Look at print statments to manualy asses random/diffrent placing
            string[] deckarray = deck.ToArray<String>();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(deckarray[i]);
            }
        }

        [TestMethod]
        public void TestEpidemicLastCityMixedIn()
        {
            deck = new LinkedList<string>();
            pile = new LinkedList<string>();
            deck.AddFirst("Saint Petersburg");
            deck.AddFirst("Sydney");
            deck.AddFirst("New York");
            deck.AddFirst("Chicago");
            string lastcity = InfectorBL.Epidemic(deck, pile, ref infectionIndex, ref infectionRate);
            Assert.AreEqual(lastcity, deck.First.Value);
        }

        [TestMethod]
        public void TestInfectCityWithNoBlocks()
        {
            SQADemicApp.City chicago = new SQADemicApp.City(COLOR.blue, "Chicago");
            int numOfBlueCubes = SQADemicApp.BL.InfectorBL.InfectCity(chicago, new HashSet<City>());
            Assert.AreEqual(1, numOfBlueCubes);
        }

        [TestMethod]
        public void TestInfectCityWithOneCube()
        {
            SQADemicApp.City chicago = new SQADemicApp.City(COLOR.blue, "Chicago");
            chicago.blueCubes = 1;
            int numBlueCubes = SQADemicApp.BL.InfectorBL.InfectCity(chicago, new HashSet<City>());
            Assert.AreEqual(2, numBlueCubes);
        }

        [TestMethod]
        public void TestInfectCityWithTwoCubes()
        {
            SQADemicApp.City chicago = new SQADemicApp.City(COLOR.blue, "Chicago");
            chicago.blueCubes = 2;
            int numBlueCubes = SQADemicApp.BL.InfectorBL.InfectCity(chicago, new HashSet<City>());
            Assert.AreEqual(3, numBlueCubes);
        }

        [TestMethod]
        public void TestDiffColorInfectCityWithOneCube()
        {
            SQADemicApp.City lima = new SQADemicApp.City(COLOR.yellow, "Lima");
            lima.yellowCubes = 1;
            int numYellowCubes = SQADemicApp.BL.InfectorBL.InfectCity(lima, new HashSet<City>());
            Assert.AreEqual(2, numYellowCubes);
            
        }

        [TestMethod]
        public void TestRedWithTwoInfect()
        {
            SQADemicApp.City tokyo = new SQADemicApp.City(COLOR.red, "Tokyo");
            tokyo.redCubes = 2;
            int numRedCubes = SQADemicApp.BL.InfectorBL.InfectCity(tokyo, new HashSet<City>());
            Assert.AreEqual(3, numRedCubes);
        }

        [TestMethod]
        public void TestBlueInfectAndOutbreak()
        {
            SQADemicApp.City chicago = new SQADemicApp.City(COLOR.blue, "Chicago");
            chicago.blueCubes = 3;
            int numBlueCubes = SQADemicApp.BL.InfectorBL.InfectCity(chicago, new HashSet<City>());
            Assert.AreEqual(3, numBlueCubes);
        }

        [TestMethod]
        public void TestOutbreakSimple()
        {
            HashSet<City> infected = new HashSet<City>();
            SQADemicApp.City lima = new SQADemicApp.City(COLOR.yellow, "Lima");
            SQADemicApp.City santiago = new SQADemicApp.City(COLOR.yellow, "Santiago");
            infected.Add(santiago);
            santiago.adjacentCities.Add(lima);
            santiago.yellowCubes = 3;
            SQADemicApp.BL.InfectorBL.Outbreak(santiago, COLOR.yellow, santiago.adjacentCities, infected);
            Assert.AreEqual(1, lima.yellowCubes);
        }

        [TestMethod]
        public void TestIncrementOutbreakMarker()
        {
            HashSet<City> infected = new HashSet<City>();
            SQADemicApp.City lima = new SQADemicApp.City(COLOR.yellow, "Lima");
            SQADemicApp.City santiago = new SQADemicApp.City(COLOR.yellow, "Santiago");
            infected.Add(santiago);
            santiago.adjacentCities.Add(lima);
            santiago.yellowCubes = 3;
            SQADemicApp.BL.InfectorBL.Outbreak(santiago, COLOR.yellow, santiago.adjacentCities, infected);
            Assert.AreEqual(1, GameBoardModels.outbreakMarker);
        }

        [TestMethod]
        public void TestIncrementOutbreakMarker2()
        {
            HashSet<City> infected = new HashSet<City>();
            SQADemicApp.City lima = new SQADemicApp.City(COLOR.yellow, "Lima");
            SQADemicApp.City santiago = new SQADemicApp.City(COLOR.yellow, "Santiago");
            //infected.Add(santiago);
            santiago.adjacentCities.Add(lima);
            santiago.yellowCubes = 3;
            GameBoardModels.outbreakMarker += 5;
            SQADemicApp.BL.InfectorBL.Outbreak(santiago, COLOR.yellow, santiago.adjacentCities, infected);
            Assert.AreEqual(6, GameBoardModels.outbreakMarker);
        }
    }
}
