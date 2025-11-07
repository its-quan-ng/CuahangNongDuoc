using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.DataLayer;


namespace CuahangNongduoc.Controller
{
    public class LyDoChiController
    {
        LyDoChiFactory factory = new LyDoChiFactory();

        public void HienthiAutoComboBox(System.Windows.Forms.ComboBox cmb)
        {
            cmb.DataSource = factory.DanhsachLyDo();
            cmb.DisplayMember = "LY_DO";
            cmb.ValueMember = "ID";
        }

        public void HienthiDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn)
        {
            factory.LoadData();
            
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            bs.DataSource = factory.GetDataTable();
            bn.BindingSource = bs;
            dg.DataSource = bs;
            
        }

        /// <summary>
        /// Get DataTable từ Factory để refresh binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return factory.GetDataTable();
        }

        public void HienthiDataGridviewComboBox(System.Windows.Forms.DataGridViewComboBoxColumn cmb)
        {

            cmb.DataSource = factory.DanhsachLyDo();
            cmb.DisplayMember = "LY_DO";
            cmb.ValueMember = "ID";
            cmb.DataPropertyName = "ID_LY_DO_CHI";
            cmb.HeaderText = "Lý do chi";

        }
        
        public LyDoChi LayLyDoChi(long id)
        {
            DataTable tbl = factory.LayLyDoChi(id);
            LyDoChi lydo = new LyDoChi();
            if (tbl.Rows.Count > 0)
            {
                lydo.Id = Convert.ToInt64(tbl.Rows[0]["ID"]);
                lydo.LyDo = Convert.ToString(tbl.Rows[0]["LY_DO"]);
            }
            return lydo;
        }

        public bool Save()
        {
            return factory.Save();
        }
    }
}
