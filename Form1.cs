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
        protected static DataTable dtStationInfo;
        private static List<StationDetail> stationList;
        private static List<StationDetail> favoriteList;
        private int SelectedID;
        private int SelectedIdx;

        public Form1()
        {
            InitializeComponent();
        }

        protected void GetUBikeStationInformation()
        {
            string url = "https://datacenter.taichung.gov.tw/swagger/OpenData/91deb8b8-7547-4a60-8cae-7c95c0df2fda";
            var Result = ConnectInternet.ConnInternet.InvokeHTTP_GET(url);
            stationList = JsonConvert.DeserializeObject<List<StationDetail>>(Result);
            dgvAllList.AutoGenerateColumns = false;
            dgvAllList.DataSource = stationList;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Create Favorite Station Entity
            favoriteList = new List<StationDetail>();

            SelectedID = -1;
            SelectedIdx = -1;

            GetUBikeStationInformation();
            GetAreaItemAddToComboBox();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetUBikeStationInformation();
        }

        protected void GetAreaItemAddToComboBox()
        {
            var list = stationList.Select(dr => dr.CArea).Distinct();
            foreach (object obj in list)
            {
                cbArea.Items.Add(obj);
            }
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = stationList.Where(dr => dr.CArea == (string)cbArea.SelectedItem);
            dgvAllList.DataSource = list.ToList();
        }

        private void dgvAllList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var idx = (int)dgvAllList.Rows[e.RowIndex].Cells[0].Value;

            var detail = stationList.Where(dr => dr.ID == idx).ToList();

            tbAddress.Text = detail[0].CAddress;
            tbAvCnt.Text = detail[0].AvailableCNT.ToString();
            tbEmpCnt.Text = detail[0].EmpCNT.ToString();
            tbBkCnt.Text = (detail[0].AvailableCNT - detail[0].EmpCNT).ToString();

            SelectedID = detail[0].ID;
        }

        private void dgvFavorite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var idx = (int)dgvFavorite.Rows[e.RowIndex].Cells[0].Value;

            var detail = favoriteList.Where(dr => dr.ID == idx).ToList();

            tbAddress.Text = detail[0].CAddress;
            tbAvCnt.Text = detail[0].AvailableCNT.ToString();
            tbEmpCnt.Text = detail[0].EmpCNT.ToString();
            tbBkCnt.Text = (detail[0].AvailableCNT - detail[0].EmpCNT).ToString();

            SelectedIdx = e.RowIndex;
        }

        protected void CreateStationDataTable()
        {
            dtStationInfo = new DataTable("dtStationInfo");
            dtStationInfo.Columns.Add("ID", typeof(System.Int32));
            dtStationInfo.Columns.Add("Position", typeof(System.String));
            dtStationInfo.Columns.Add("Area", typeof(System.String));
            dtStationInfo.Columns.Add("Address", typeof(System.String));
            dtStationInfo.Columns.Add("AvailableCNT", typeof(System.Int32));
            dtStationInfo.Columns.Add("EmpCNT", typeof(System.Int32));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    btnAddFav.Enabled = false;
                    btnRemoveFav.Enabled = true;
                    break;
                case 1:
                    btnAddFav.Enabled = true;
                    btnRemoveFav.Enabled = false;
                    break;
            }

            tbAddress.Text = string.Empty;
            tbAvCnt.Text = string.Empty;
            tbEmpCnt.Text = string.Empty;
            tbBkCnt.Text = string.Empty;
        }

        private void btnAddFav_Click(object sender, EventArgs e)
        {

            if (SelectedID == -1)
            {
                MessageBox.Show("Please select a BikeStation from List", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dgvFavorite.DataSource = null;

            var item = stationList.Where(dr => dr.ID == SelectedID).ToList();

            int idx = favoriteList.Count == 0 ? 0 : favoriteList.Count;
            favoriteList.Insert(idx, (StationDetail)item[0]);
            dgvFavorite.AutoGenerateColumns = false;
            dgvFavorite.DataSource = favoriteList;
        }

        private void btnRemoveFav_Click(object sender, EventArgs e)
        {
            if (SelectedIdx == -1)
            {
                MessageBox.Show("Please select a BikeStation from List", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvFavorite.DataSource = null;
            favoriteList.RemoveAt(SelectedIdx);
            dgvFavorite.DataSource = favoriteList;

            tbAddress.Text = string.Empty;
            tbAvCnt.Text = string.Empty;
            tbEmpCnt.Text = string.Empty;
            tbBkCnt.Text = string.Empty;
        }
    }
}
