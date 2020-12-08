using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectInternet;
using System.Net;
using Newtonsoft.Json;
using System.CodeDom;
using System.Runtime.CompilerServices;

namespace HKU_UBikeStation
{
    public partial class Form1 : Form
    {
        // Define Object and Values

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // When Form be Show

            // Create Favorite Station Entity

            // GetAreaItemAddToComboBox()

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //---- Founction --------------------------------------------------
            // Get iBike Station Information from Internet

            // Use References to  Connect Http portocol and get Json Result

            // Data gridview setting
            dgvAllList.AutoGenerateColumns = false;
            //-----------------------------------------------------------------
        }
 

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvAllList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvFavorite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAddFav_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveFav_Click(object sender, EventArgs e)
        {
           
        }
    }
}
