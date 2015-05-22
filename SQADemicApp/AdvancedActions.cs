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
    public partial class AdvancedActions : Form
    {
        GameBoard board;
        public AdvancedActions(GameBoard board)
        {
            InitializeComponent();
            this.board = board;
        }

        private void ShareKnowledge_Click(object sender, EventArgs e)
        {
            SQADemicApp.ShareCardForm SCForm = new ShareCardForm(board);
            SCForm.Show();
            this.Dispose();
            this.Close();
        }

        private void BuildResearchStation_Click(object sender, EventArgs e)
        {
            if (!PlayerActionsBL.BuildAResearchStationOption(GameBoardModels.players[GameBoardModels.CurrentPlayerIndex]))
            {
                MessageBox.Show("Research Station unable to be built");
            }
            else
            {
                board.UpdatePlayerForm();
                this.Dispose();
                this.Close();
            }
        }

        private void CreateCure_Click(object sender, EventArgs e)
        {
            SQADemicApp.CureForm cureForm = new CureForm(board);
            cureForm.Show();
            this.Dispose();
            this.Close();
        }
    }
}
