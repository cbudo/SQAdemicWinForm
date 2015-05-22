﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQADemicApp.BL;

namespace SQADemicApp
{
    public partial class PlayerPanel : Form
    {
        private GameBoard board;
        public PlayerPanel(GameBoard board)
        {
            this.board = board;
            InitializeComponent();
        }
        
        private void MoveButton_Click(object sender, EventArgs e)
        {
            GameBoard.CurrentState = GameBoard.STATE.Move;
        }

        private void CureCityButton_Click(object sender, EventArgs e)
        {
            TreatDiseaseForm TDForm = new TreatDiseaseForm(board);
            TDForm.Show();
        }

        private void AAButton_Click(object sender, EventArgs e)
        {
            AdvancedActions AAForm = new AdvancedActions(board);
            AAForm.Show();
        }

        private void DispatcherMove_Click(object sender, EventArgs e)
        {

        }

        private void EndSequenceBtn_Click(object sender, EventArgs e)
        {
            if (GameBoard.turnpart == GameBoard.TURNPART.Draw)
                drawcards();
            else if (GameBoard.turnpart == GameBoard.TURNPART.Infect)
                infectCities();
            board.UpdatePlayerForm();
        }

        private void infectCities()
        {
            List<string> infectedcites = InfectorBL.InfectCities(GameBoardModels.infectionDeck, GameBoardModels.infectionPile, GameBoardModels.InfectionRate);
            InfectorBL.InfectCities(infectedcites);
            GameBoard.turnpart = GameBoard.TURNPART.Action;
            GameBoardModels.CurrentPlayerIndex = (GameBoardModels.CurrentPlayerIndex + 1) % GameBoardModels.players.Count();
        }

        private void drawcards()
        {
            //Draw Two cards
            Card drawCard1 = GameBoardModels.drawCard();
            Card drawCard2 = GameBoardModels.drawCard();

            //Epidemic code
            if (drawCard1.CityName.Equals("EPIDEMIC"))
            {
                string infectcityname = InfectorBL.Epidemic(GameBoardModels.infectionDeck, GameBoardModels.infectionPile, ref GameBoardModels.InfectionRateIndex, ref GameBoardModels.InfectionRate);
                for(int i = 0; i < 3; i++)
                {
                    InfectorBL.InfectCities(new List<string> {infectcityname});
                }
            }
            else if (drawCard1.CardType == Card.CARDTYPE.Special)
                GameBoardModels.eventCards.Add(drawCard1);
            else
                GameBoardModels.players[GameBoardModels.CurrentPlayerIndex].hand.Add(drawCard1);

            if (drawCard2.CityName.Equals("EPIDEMIC"))
            {
                string infectcityname = InfectorBL.Epidemic(GameBoardModels.infectionDeck, GameBoardModels.infectionPile, ref GameBoardModels.InfectionRateIndex, ref GameBoardModels.InfectionRate);
                for (int i = 0; i < 3; i++)
                {
                    InfectorBL.InfectCities(new List<string> { infectcityname });
                }
            }
            else if (drawCard2.CardType == Card.CARDTYPE.Special)
                GameBoardModels.eventCards.Add(drawCard2);
            else
                GameBoardModels.players[GameBoardModels.CurrentPlayerIndex].hand.Add(drawCard2);

            //Move to infection phase
            GameBoard.turnpart = GameBoard.TURNPART.Infect;
        }
    }
}
