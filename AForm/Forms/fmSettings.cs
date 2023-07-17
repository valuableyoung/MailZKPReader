using ALogic.DBConnector;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace AForm.Forms
{
    public partial class fmSettings : Form
    {
        public object gridobject;
        public string appname;

        public fmSettings()
        {
            InitializeComponent();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (gridobject == null) { return; }

                if (teSaveSettings.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Не заполнено поле с наименованием настройки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show($"Сохранить настройку '{teSaveSettings.Text.Trim()}'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }

                Stream LayoutStream = new MemoryStream();

                if (gridobject is PivotGridControl)
                {
                    (gridobject as PivotGridControl).SaveLayoutToStream(LayoutStream);
                }

                if (gridobject is GridView)
                {
                    (gridobject as GridView).SaveLayoutToStream(LayoutStream);
                }

                if (gridobject is BandedGridView)
                {
                    (gridobject as BandedGridView).SaveLayoutToStream(LayoutStream);
                }

                LayoutStream.Position = 0;
                StreamReader reader = new StreamReader(LayoutStream);
                string text = reader.ReadToEnd();

                string scnt = $@"select count(*) cnt from UserSettingsCS (nolock) where iduser = {ALogic.Logic.SPR.User.CurrentUserId} and settingsname = '{teSaveSettings.Text.Trim()}'
                            and appname = '{appname}'";
                int cnt = Convert.ToInt32(DBExecutor.SelectSchalar(scnt));

                if (cnt > 0) { MessageBox.Show($"Уже существуют сохраненные настройки '{teSaveSettings.Text.Trim()}' для этого окна!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                string sql = $@"insert into UserSettingsCS(appname, iduser, settingsxml, settingsname) values(@appname, @userid, @settings, @setname)";
                SqlParameter parAppName = new SqlParameter("appname", appname);
                SqlParameter parUserId = new SqlParameter("userid", ALogic.Logic.SPR.User.CurrentUserId);
                SqlParameter parSettings = new SqlParameter("settings", text);
                SqlParameter parSetName = new SqlParameter("setname", teSaveSettings.Text.Trim());
                DBExecutor.ExecuteQuery(sql, parAppName, parUserId, parSettings, parSetName);

                teSaveSettings.ResetText();

                RefreshSettingsforCurrentUser(ALogic.Logic.SPR.User.CurrentUserId);
            }
            catch { }
            finally { this.Cursor = Cursors.Default; }
        }

        private void RefreshSettingsforCurrentUser(int iduser)
        {
            string sql = $@"select id, settingsname from UserSettingsCS (nolock) where iduser = {iduser} and appname = '{appname}'";
            DataTable table = DBExecutor.SelectTable(sql);

            leLoadSettings.Properties.DataSource = table;

            leLoadSettings.Properties.DisplayMember = "settingsname";
            leLoadSettings.Properties.ValueMember = "id";
            leLoadSettings.Properties.KeyMember = "id";
            leLoadSettings.Properties.PopulateColumns();
        }

        private void fmSettings_Load(object sender, EventArgs e)
        {
            RefreshSettingsforCurrentUser(ALogic.Logic.SPR.User.CurrentUserId);
        }

        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (leLoadSettings.EditValue == null) { return; }

                string sql = $@"select settingsxml from UserSettingsCS (nolock) where iduser = {ALogic.Logic.SPR.User.CurrentUserId} and id = '{leLoadSettings.EditValue}'";
                string setting = Convert.ToString(DBExecutor.SelectSchalar(sql));

                Stream LayoutStream = new MemoryStream();

                var stringBytes = System.Text.Encoding.UTF8.GetBytes(setting);
                LayoutStream.Write(stringBytes, 0, stringBytes.Length);

                if (gridobject is PivotGridControl)
                {
                    LayoutStream.Position = 0;
                    (gridobject as PivotGridControl).RestoreLayoutFromStream(LayoutStream);
                }

                if (gridobject is GridView)
                {
                    LayoutStream.Position = 0;
                    (gridobject as GridView).RestoreLayoutFromStream(LayoutStream);
                }

                if (gridobject is BandedGridView)
                {
                    LayoutStream.Position = 0;
                    (gridobject as BandedGridView).RestoreLayoutFromStream(LayoutStream);
                }
            }
            catch { }
            finally { this.Cursor = Cursors.Default; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (leLoadSettings.EditValue == null || leLoadSettings.ItemIndex == -1) { return; }
            if (MessageBox.Show($"Удалить настройку '{leLoadSettings.Text.Trim()}'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }

            string sql = $@"delete from UserSettingsCS where id = {leLoadSettings.EditValue}";
            DBExecutor.ExecuteQuery(sql);
            RefreshSettingsforCurrentUser(ALogic.Logic.SPR.User.CurrentUserId);

        }
    }
}
