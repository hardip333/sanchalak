using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamFeeConfigurationHeadMap
    {

        public ExamFeeConfigurationHeadMap setRowIntoObjForAdmin(ExamFeeConfigurationHeadMap element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeConfigurationId = Convert.ToInt32(dr[1].ToString());
            element.ExamFeesHeadId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FeesAmount = (float)Convert.ToDouble(dr[3].ToString());
            }
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.IsEditable = Convert.ToBoolean(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.IsDayWiseApplicable = Convert.ToBoolean(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.IncrementedBy = (float)Convert.ToDouble(dr[6].ToString());
            }
            element.IsActive = Convert.ToBoolean(dr[7].ToString());

            return element;

        }

        public ExamFeeConfigurationHeadMap setRowIntoObjForUser(ExamFeeConfigurationHeadMap element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeConfigurationId = Convert.ToInt32(dr[1].ToString());
            element.ExamFeesHeadId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FeesAmount = (float)Convert.ToDouble(dr[3].ToString());
            }
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.IsEditable = Convert.ToBoolean(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.IsDayWiseApplicable = Convert.ToBoolean(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.IncrementedBy = (float)Convert.ToDouble(dr[6].ToString());
            }
            return element;
        }


        public ExamFeeConfigurationHeadMap setRowIntoObjForAll(ExamFeeConfigurationHeadMap element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeConfigurationId = Convert.ToInt32(dr[1].ToString());
            element.ExamFeesHeadId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FeesAmount = (float)Convert.ToDouble(dr[3].ToString());
            }
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.IsEditable = Convert.ToBoolean(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.IsDayWiseApplicable = Convert.ToBoolean(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.IncrementedBy = (float)Convert.ToDouble(dr[6].ToString());
            }
            element.IsActive = Convert.ToBoolean(dr[7].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[8].ToString());
            element.CreatedBy = Convert.ToInt32(dr[9].ToString());
            if (!string.IsNullOrEmpty(dr[10].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[10].ToString());
            if (!string.IsNullOrEmpty(dr[11].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[11].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[12].ToString());
            return element;
        }

        public List<ExamFeeConfigurationHeadMap> getExamFeeConfigurationHeadMapList(string flag, int? IsDeleted, int? IsActive, int? ExamFeesHeadId, int? ExamFeeConfigurationId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFeeConfigurationHeadMapListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamFeesHeadId", ExamFeesHeadId);
                Cmd.Parameters.AddWithValue("@ExamFeeConfigurationId", ExamFeeConfigurationId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFeeConfigurationHeadMap> ObjListExamFeeConfigurationHeadMap = new List<ExamFeeConfigurationHeadMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeeConfigurationHeadMap objExamFeeConfigurationHeadMap = new ExamFeeConfigurationHeadMap();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamFeeConfigurationHeadMap, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamFeeConfigurationHeadMap, Dt.Rows[i]);
                        }

                        ObjListExamFeeConfigurationHeadMap.Add(objExamFeeConfigurationHeadMap);
                    }
                }
                return ObjListExamFeeConfigurationHeadMap;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ExamFeeCongfigGet getPendingExamFeeConfigurationList(int? ProgrammePartTermId, int? FacultyExamMapId, int? ExamMasterId,int? AppearanceTypeId)
        {
            try
            {
                ExamFeeCongfigGet examFeeCongfigGet = new ExamFeeCongfigGet();
                if (ProgrammePartTermId > 0 && FacultyExamMapId > 0)
                {

                    SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    SqlDataAdapter Da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    SqlCommand Cmd = new SqlCommand("GetMinDateOfFacultyExamMapById", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@Id", FacultyExamMapId); ;
                   
                    Da.SelectCommand = Cmd;

                    Da.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return null;
                    }
                  
                    int n = 0, count = dt.Rows.Count;

                    string strminDate = dt.Rows[0][0].ToString();
                    string strStartDate = dt.Rows[0][1].ToString();

                    DateTime StartDate = Convert.ToDateTime(strStartDate);
                    DateTime MinDate = Convert.ToDateTime(strminDate);
                    var numberOfDays = (StartDate - MinDate).TotalDays;


                    DataTable Dt = new DataTable();
                    Cmd = new SqlCommand("getPendingExamFeeConfigurationList", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                    Cmd.Parameters.AddWithValue("@AppearanceTypeId", AppearanceTypeId);
                    Cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                    Da.SelectCommand = Cmd;

                    Da.Fill(Dt);

                    if (Dt == null || Dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    n = 0; count = Dt.Rows.Count;


                    examFeeCongfigGet.feesConfiguration = new ExamFeesConfiguration();

                    examFeeCongfigGet.feesConfiguration.AppearanceTypeId = AppearanceTypeId;
                    examFeeCongfigGet.feesConfiguration.FacultyExamMapId = FacultyExamMapId;
                    examFeeCongfigGet.feesConfiguration.ProgrammePartTermId = ProgrammePartTermId;
                    examFeeCongfigGet.feesConfiguration.ExamMasterId = ExamMasterId;


                    examFeeCongfigGet.ExamFeeConfigurationHeadMaps = new List<ExamFeeConfigurationHeadMap>();
                    while (n < count)
                    {
                        ExamFeeConfigurationHeadMap element = new ExamFeeConfigurationHeadMap();
                        if (!string.IsNullOrEmpty(Dt.Rows[n][0].ToString()))
                            element.Id = Convert.ToInt32(Dt.Rows[n][0].ToString());

                        element.ExamFeesHeadId = Convert.ToInt32(Dt.Rows[n][1].ToString());
                        element.ExamFeesHead = Dt.Rows[n][2].ToString();
                        if (!string.IsNullOrEmpty(Dt.Rows[n][3].ToString()))
                            element.ExamFeeConfigurationId = Convert.ToInt32(Dt.Rows[n][3].ToString());
                        else
                        {
                            ExamFeesConfiguration ExamFeesConfigurationElement = new ExamFeesConfiguration();
                            ExamFeesConfigurationElement.AppearanceTypeId = AppearanceTypeId;
                            ExamFeesConfigurationElement.ExamMasterId = ExamMasterId;
                            ExamFeesConfigurationElement.ProgrammePartTermId = ProgrammePartTermId;
                            element.ExamFeeConfiguration= ExamFeesConfigurationElement;
                        }
                        if (!string.IsNullOrEmpty(Dt.Rows[n][4].ToString()))
                            element.FeesAmount = (float)Convert.ToDouble(Dt.Rows[n][4].ToString());
                        if (!string.IsNullOrEmpty(Dt.Rows[n][5].ToString()))
                            element.IsEditable = Convert.ToBoolean(Dt.Rows[n][5].ToString());
                        if (!string.IsNullOrEmpty(Dt.Rows[n][6].ToString()))
                            element.IsDayWiseApplicable = Convert.ToBoolean(Dt.Rows[n][6].ToString());
                        if (!string.IsNullOrEmpty(Dt.Rows[n][7].ToString()))
                            element.IncrementedBy = (float)Convert.ToDouble(Dt.Rows[n][7].ToString());
                        if (!string.IsNullOrEmpty(Dt.Rows[n][8].ToString()))
                            element.DayRange = Convert.ToInt16(Dt.Rows[n][8].ToString());

                        examFeeCongfigGet.ExamFeeConfigurationHeadMaps.Add(element);
                        n++;
                    }
                }
                return examFeeCongfigGet;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}