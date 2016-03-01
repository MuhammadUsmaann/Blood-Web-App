using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace CodeGenerator.MVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // #region Properties
        Util.IDbBuilder _iDbBuilder = null;
        Util.IObjectBuilder _iObjectBuilder = null;
        DataTable _dtTables = null;
        string _strProjectName = "BloodDonationWebApp.Entities";
        string _strTableName = "";
        string _strFileSavedDirectory = "";
        string _strMessageBoxCaption = "Entity and Service Code Generator C# .NET";

        public MainWindow()
        {
            InitializeComponent();
            //_strProjectName = Util.Utility.GetProjectName();
            _strFileSavedDirectory = Util.Utility.GetSaveDirectoty();

            GetDbBuilder();
            BindDropDownList();
        }

       
        //#region UserFunctions
        private void GetDbBuilder()
        {
           
           if (Util.Utility.GetSelectedDB().Equals("SqlServer"))
            {
                _iDbBuilder = new Util.DbBuilderSqlServer();
                _iObjectBuilder = new Util.ObjectBuilderSqlServer();
            }
            else
            {
                MessageBox.Show("No Database Selected!", _strMessageBoxCaption, MessageBoxButton.OK);
            }
        }

        private void BindDropDownList()
        {
            _dtTables = DAL.SPCreatorDAL.GetTables();

            foreach (DataRow row in _dtTables.Rows)
            {
                ddlTables.Items.Add(row["TABLE_NAME"]);
            }
            ddlTables.Items.Insert(0, "ALL");
            ddlTables.SelectedIndex = 0;
        }

        private DataTable GetColumnsDesc()
        {
            return DAL.SPCreatorDAL.GetColumnsDesc(_strTableName);
        }
        //#endregion

        #region Events
   
      
        #endregion

        private void btnEntityClassCreator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dir = @"Entities\";

                ClearFolder(_strFileSavedDirectory + dir);

                if (!Directory.Exists(_strFileSavedDirectory + dir))
                {
                    Directory.CreateDirectory(_strFileSavedDirectory + dir);
                }

                string strProjectName = _strProjectName;
                if (ddlTables.SelectedValue != null && ddlTables.SelectedValue.ToString().Equals("ALL"))
                {
                   // if (MessageBox.Show("Do you want to Create Object Class for All tables?", _strMessageBoxCaption, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (DataRow row in _dtTables.Rows)
                        {
                            _strTableName = row["TABLE_NAME"].ToString();
                            string strObjectName = _strTableName.Replace("TBL_", "").Replace("tbl_", "").Replace("TBL", "").Replace("tbl", "").Replace("T_", "").Replace("t_", "");
                            DataTable dt = GetColumnsDesc();
                            StringBuilder sb = _iObjectBuilder.BuildEntity(strProjectName, strObjectName, dt, "IBaseEntity");

                            string filePath = _strFileSavedDirectory + dir + strObjectName + ".cs";

                            if (!string.IsNullOrEmpty(sb.ToString()))
                            {
                                Util.Utility.WriteToDisk(filePath, sb.ToString());
                            }
                            else
                            {
                                MessageBox.Show("OOPs!! There's nothing to create...", _strMessageBoxCaption, MessageBoxButton.OK);
                            }
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _strMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void btnSerivceClassCreator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dir = @"Services\";


                ClearFolder(_strFileSavedDirectory + dir);

                if (!Directory.Exists(_strFileSavedDirectory + dir))
                {
                    Directory.CreateDirectory(_strFileSavedDirectory + dir);
                }

                string strProjectName = _strProjectName;
                if (ddlTables.SelectedValue != null && ddlTables.SelectedValue.ToString().Equals("ALL"))
                {
                    foreach (DataRow row in _dtTables.Rows)
                    {
                        _strTableName = row["TABLE_NAME"].ToString();
                        string strObjectName = _strTableName.Replace("TBL_", "").Replace("tbl_", "").Replace("TBL", "").Replace("tbl", "").Replace("T_", "").Replace("t_", "");
                        DataTable dt = GetColumnsDesc();
                        StringBuilder sb = _iObjectBuilder.BuildSevice(strProjectName, strObjectName, dt, "IRepository");

                        string filePath = _strFileSavedDirectory + dir + strObjectName + "Repository.cs";

                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            Util.Utility.WriteToDisk(filePath, sb.ToString());
                        }
                        else
                        {
                            MessageBox.Show("OOPs!! There's nothing to create...", _strMessageBoxCaption, MessageBoxButton.OK);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _strMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
    }
}
