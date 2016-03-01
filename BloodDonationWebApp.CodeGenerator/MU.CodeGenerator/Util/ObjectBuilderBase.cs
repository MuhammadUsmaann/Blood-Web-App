using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CodeGenerator.MVP.Util;

namespace CodeGenerator.MVP.Util
{
    public class ObjectBuilderBase : IObjectBuilder
    {
        protected string _NUMID = "";
        protected string _STRUID = "";
        protected string _STRLASTUID = "";
        protected string _DTUDT = "";
        protected string _DTLASTUDT = "";

        public ObjectBuilderBase()
        {
            _NUMID = Util.Utility.GetPkColName();
            _STRUID = Util.Utility.GetRecordCreatorColName();
            _STRLASTUID = Util.Utility.GetRecordModifierColName();
            _DTUDT = Util.Utility.GetRecordCreateDateColName();
            _DTLASTUDT = Util.Utility.GetRecordModifiedDateColName();
        }



        public StringBuilder BuildEntity(string strProjectName, string strObjectName, DataTable dt, string InterfaceClass)
        {
             StringBuilder sb = new StringBuilder();

             sb.Append(" using System;");
             
             sb.Append("\n\n namespace " + strProjectName);
             sb.Append("\n {");
             sb.Append("\n\tpublic partial class " + strObjectName);
             sb.Append("\n\t{");

             StringBuilder privateProperties = new StringBuilder();
             StringBuilder publicProperties = new StringBuilder();

             var sd = dt.Rows;

            foreach (DataRow row in dt.Rows)
            {
                if (row["COLUMN_NAME"].ToString().Contains(_STRUID) || row["COLUMN_NAME"].ToString().Contains(_STRLASTUID) ||
                    row["COLUMN_NAME"].ToString().Contains(_DTUDT) || row["COLUMN_NAME"].ToString().Contains(_DTLASTUDT))
                    continue;

                string strDataType = Utility.GetDotNetDataType(row["DATA_TYPE"].ToString().ToUpper(), row["PRECISION"].ToString(), row["SCALE"].ToString());

                string temp_private_property = (" m_" + row["COLUMN_NAME"]).ToLower();

                if (strDataType.ToLower() == "datetime" || strDataType.ToLower() == "date")
                {
                    strDataType = "string";
                }

                privateProperties.Append("\n\t\tprivate " + strDataType + temp_private_property + ";");

                

                publicProperties.Append("\n\t\tpublic " + strDataType + " " + row["COLUMN_NAME"]);
                publicProperties.Append("\n\t\t{");

                if (row["COLUMN_NAME"].ToString() == "creation_date" || row["COLUMN_NAME"].ToString() == "updated_date")
                {
                    publicProperties.Append("\n\t\t\tget { return GetCurrentDate(); } ");
                }    
                else
                {
                    publicProperties.Append("\n\t\t\tget { return " + temp_private_property + "; } ");
                }

                //publicProperties.Append("\n\t\t\tget { return " + temp_private_property + "; } ");
                publicProperties.Append("\n\t\t\tset{" + temp_private_property + " = value;}");
                publicProperties.Append("\n\t\t}");
                publicProperties.AppendLine();
            }

            sb.Append(privateProperties.ToString());
            sb.AppendLine();
            sb.Append(publicProperties.ToString());
            sb.Append("\n\t}");
            sb.Append("\n}");

            return sb;
        }
        public StringBuilder BuildSevice(string strProjectName, string strObjectName, DataTable dt, string InterfaceClass)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nusing BloodDonationWebApp.Database;");
            sb.Append("using BloodDonationWebApp.Entities;");
            sb.Append("\nusing System.Collections.Generic;");
            sb.Append("\nusing System.Linq;");

            sb.Append("\n\nnamespace BloodDonationWebApp.Services");
            sb.Append("\n{");
            sb.Append("\n\tpublic partial class " + strObjectName + "Repository : " + InterfaceClass + "<" + strObjectName + ">");
            sb.Append("\n\t{");

            StringBuilder insertCol = new StringBuilder();
            StringBuilder insertValue = new StringBuilder();
            StringBuilder UpdateCols = new StringBuilder();
            string whereCon = "";
            string primaryKey = "";

            foreach (DataRow row in dt.Rows)
            {
                if (row["COLUMN_NAME"].ToString().Contains(_STRUID) || row["COLUMN_NAME"].ToString().Contains(_STRLASTUID) ||
                    row["COLUMN_NAME"].ToString().Contains(_DTUDT) || row["COLUMN_NAME"].ToString().Contains(_DTLASTUDT))
                    continue;

                string strDataType = Utility.GetDotNetDataType(row["DATA_TYPE"].ToString().ToUpper(), row["PRECISION"].ToString(), row["SCALE"].ToString());

                if (row["IS_IDENTITY"] == "1")
                {
                    primaryKey = row["COLUMN_NAME"].ToString();
                    whereCon += row["COLUMN_NAME"] + " = @" + row["COLUMN_NAME"];
                    continue;
                }

                if (row["COLUMN_NAME"].ToString().ToLower() != "isactive")
                {
                    if (insertCol.ToString() != null && insertCol.ToString() != "")
                    {
                        insertCol.Append(", " + row["COLUMN_NAME"]);
                    }
                    else
                    {
                        insertCol.Append(row["COLUMN_NAME"]);
                    }
                    if (insertValue.ToString() != null && insertValue.ToString() != "")
                    {
                        insertValue.Append(", @" + row["COLUMN_NAME"]);
                    }
                    else
                    {
                        insertValue.Append("@" + row["COLUMN_NAME"]);
                    }
                 
                }
                if (row["COLUMN_NAME"].ToString().ToLower() != "creation_date".ToLower() && row["COLUMN_NAME"].ToString().ToLower() != "created_by".ToLower())
                {
                    if (UpdateCols.ToString() != null && UpdateCols.ToString() != "")
                    {
                        UpdateCols.Append(", ");
                    }
                    UpdateCols.Append(row["COLUMN_NAME"] + " = @" + row["COLUMN_NAME"]);
                }
            }

            sb.Append("\n\t\tprivate const string SqlTableName = \"" + strObjectName + "\";");
            sb.Append("\n\t\tprivate const string SqlSelectCommand = \" SELECT * FROM \" + SqlTableName + \" Where isActive = 1 \";");
            sb.Append("\n\t\tprivate const string SqlInsertCommand = \" INSERT INTO \" + SqlTableName + \" ( " + insertCol.ToString() + ") OUTPUT Inserted." + primaryKey + " Values(" + insertValue + ")\";");
            sb.Append("\n\t\tprivate const string SqlUpdateCommand = \" UPDATE \" + SqlTableName + \" Set " + UpdateCols.ToString() + " where ( " + whereCon + " ) AND  isActive = 1 \";");
            sb.Append("\n\t\tprivate const string SqlDeleteCommand = \" Update \" + SqlTableName + \" Set isActive = 0 where ( " + whereCon + " ) AND isActive = 1 \";");


            sb.Append("\n\n\t\tpublic override int Insert(" + strObjectName + " model)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Query<int>(SqlInsertCommand, model).Single();");
            sb.Append("\n\t\t}");

            sb.Append("\n\t\tpublic override int Remove(int id)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Execute(SqlDeleteCommand, new { " + primaryKey + " = id });");
            sb.Append("\n\t\t}");

            sb.Append("\n\t\tpublic override  " + strObjectName + " FindByID(int id)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Query<" + strObjectName + ">(SqlSelectCommand + \" AND " + whereCon + " \", new { " + primaryKey + " = id }).FirstOrDefault();");
            sb.Append("\n\t\t}");


            sb.Append("\n\t\tpublic override IEnumerable<" + strObjectName + "> FindByQuery(string query)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Query<" + strObjectName + ">(SqlSelectCommand + \" AND \" + query);");
            sb.Append("\n\t\t}");


            sb.Append("\n\t\tpublic override IEnumerable<" + strObjectName + "> GetAll()");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Query<" + strObjectName + ">(SqlSelectCommand);");
            sb.Append("\n\t\t}");


            sb.Append("\n\t\tpublic override IEnumerable<" + strObjectName + "> GetTop(int count)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Query<" + strObjectName + ">(string.Format(\"SELECT TOP {0} * FROM {1}\", count, SqlTableName)).ToList();");
            sb.Append("\n\t\t}");


            sb.Append("\n\t\tpublic override int Update(" + strObjectName + " item)");
            sb.Append("\n\t\t{");
            sb.Append("\n\t\t\treturn Execute(SqlUpdateCommand, item);");
            sb.Append("\n\t\t}");

            sb.Append("\n\t}");
            sb.Append("\n}");

            return sb;
        }
    }
}
