using Microsoft.Ajax.Utilities;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALPaper
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Paper List Get
        public List<MstPaper> PaperListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstPaperGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstPaper> ObjLstPaper = new List<MstPaper>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();
                        List<PaperTLMMapDetails> ObjMapDataLst = new List<PaperTLMMapDetails>();

                        objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objPaper.SubjectId = Convert.ToString(Dt.Rows[i]["SubjectId"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SubjectId"]);
                        objPaper.SubjectName = Convert.ToString(Dt.Rows[i]["SubjectName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        objPaper.FacultyId = Convert.ToString(Dt.Rows[i]["FacultyId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objPaper.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        objPaper.IsCredit = Convert.ToString(Dt.Rows[i]["IsCredit"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCredit"]);
                        objPaper.MaxMarks = Convert.ToString(Dt.Rows[i]["MaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objPaper.MinMarks = Convert.ToString(Dt.Rows[i]["MinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        //if (Dt.Rows[i]["Credits"].ToString() != "")
                        //{
                            objPaper.Credits = Convert.ToString(Dt.Rows[i]["Credits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["Credits"]);
                        //}
                        //else { objPaper.Credits = 0; }                       
                        objPaper.IsSeparatePassingHead = Convert.ToString(Dt.Rows[i]["IsSeparatePassingHead"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSeparatePassingHead"]);
                        objPaper.EvaluationId = Convert.ToString(Dt.Rows[i]["EvaluationId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objPaper.EvaluationName = Convert.ToString(Dt.Rows[i]["EvaluationName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objPaper.IsActive = Convert.ToString(Dt.Rows[i]["IsActive"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]); ;
                        objPaper.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);

                        SqlCommand cmd1 = new SqlCommand();
                        SqlDataAdapter cmdda1 = new SqlDataAdapter("PaperTLMMAPDetailsGet", Con);
                        cmdda1.SelectCommand.CommandType = CommandType.StoredProcedure;

                        cmdda1.SelectCommand.Parameters.AddWithValue("PaperId",objPaper.Id);

                        DataTable Dt1 = new DataTable();
                        cmdda1.Fill(Dt1);

                        if (Dt1.Rows.Count > 0)
                        {
                            for (int j = 0; j < Dt1.Rows.Count; j++)
                            {
                                PaperTLMMapDetails ObjMapData = new PaperTLMMapDetails();
                                ObjMapData.TeachingLearningMethodName = Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]);
                                ObjMapData.AssessmentType = Convert.ToString(Dt1.Rows[j]["AssessmentType"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentType"]);
                                ObjMapData.AssessmentMethodName = Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]);
                                ObjMapData.AssessmentMethodMarks = Convert.ToString(Dt1.Rows[j]["AssessmentMethodMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentMethodMarks"]);
                                ObjMapData.AssessmentTypeMaxMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMaxMarks"]);
                                ObjMapData.AssessmentTypeMinMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMinMarks"]);
                                ObjMapData.NoOfCredits = Convert.ToString(Dt1.Rows[j]["NoOfCredits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt1.Rows[j]["NoOfCredits"]);
                                ObjMapData.NoOfHoursPerWeek = Convert.ToString(Dt1.Rows[j]["NoOfHoursPerWeek"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["NoOfHoursPerWeek"]);
                                ObjMapDataLst.Add(ObjMapData);
                            }
                        }
                        objPaper.PaperTLMMapData = ObjMapDataLst;
                        count = count + 1;
                        objPaper.IndexId = count;
                        ObjLstPaper.Add(objPaper);
                    }
                }
                return ObjLstPaper;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Paper List Get For Drop Down
        public List<MstPaper> PaperListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstPaperGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstPaper> ObjLstPaper = new List<MstPaper>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();

                        objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);                       
                        objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);                        
                        ObjLstPaper.Add(objPaper);
                    }
                }
                return ObjLstPaper;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Megha Code

        public List<MstPaper> GetPaperListBySpecialisationIdAndProgPartTermId(int? ProgrammePartTermId, string SpecialisationId, int? ExamMasterId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();

                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetPaperListBySpecialisationIdAndProgPartTermId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgramPartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);

                Da.SelectCommand = Cmd;

                Da.Fill(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                int n = 0, count = dt.Rows.Count;
                List<MstPaper> paperList = new List<MstPaper>();
                while (n < count)
                {
                    MstPaper element = new MstPaper();
                    element.Id = Convert.ToInt32(dt.Rows[n]["PaperId"].ToString());
                    element.PaperName = dt.Rows[n]["PaperName"].ToString();
                    paperList.Add(element);
                    n++;
                }
                return paperList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public MstPaper MstPaperById(int? Id,string AssessType, int? TLMId, int? AMId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstPaperGetById", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Id", Id);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                MstPaper objPaper = new MstPaper();

                if (Dt.Rows.Count > 0)
                {
                    for(int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if(Dt.Rows[i]["AssessmentType"].ToString() == AssessType
                            && Convert.ToInt32(Dt.Rows[i]["TLMId"]) == TLMId
                            && Convert.ToInt32(Dt.Rows[i]["AMId"]) == AMId
                            )
                        {
                            objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                            objPaper.SubjectId = Convert.ToString(Dt.Rows[i]["SubjectId"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SubjectId"]);
                            objPaper.SubjectName = Convert.ToString(Dt.Rows[i]["SubjectName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                    //objPaper.FacultyId = Convert.ToString(Dt.Rows[0]["FacultyId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    //objPaper.FacultyName = Convert.ToString(Dt.Rows[0]["FacultyName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                            objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                            objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                            objPaper.IsCredit = Convert.ToString(Dt.Rows[i]["IsCredit"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCredit"]);
                            objPaper.MaxMarks = Convert.ToString(Dt.Rows[i]["AssessmentTypeMaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMaxMarks"]);
                            objPaper.MinMarks = Convert.ToString(Dt.Rows[i]["AssessmentTypeMinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMinMarks"]);
                            if (Dt.Rows[i]["Credits"].ToString() != "")
                    {
                                objPaper.Credits = Convert.ToString(Dt.Rows[i]["Credits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["Credits"]);
                    }
                    else { objPaper.Credits = 0; }
                            objPaper.IsSeparatePassingHead = Convert.ToString(Dt.Rows[i]["IsSeparatePassingHead"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSeparatePassingHead"]);
                            objPaper.EvaluationId = Convert.ToString(Dt.Rows[i]["EvaluationId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                    //objPaper.EvaluationName = Convert.ToString(Dt.Rows[0]["EvaluationName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["EvaluationName"]);
                            objPaper.IsActive = Convert.ToString(Dt.Rows[i]["IsActive"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]); ;
                    //objPaper.IsActiveSts = Convert.ToString(Dt.Rows[0]["IsActiveSts"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                            objPaper.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]); ;
                            objPaper.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        }
                        
                    }

                    


                }
                return objPaper ;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        /*#region Board Of Study List By Id
        public BoardOfStudy BosGetbyId(int BosId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstBoardOfStudyGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", BosId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    BoardOfStudy objBos = new BoardOfStudy();

                    objBos.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objBos.BoardName = (Convert.ToString(Dt.Rows[0]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BoardName"]);
                    objBos.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objBos.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    //objBos.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    //objBos.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objBos;

                }
                else
                    return null;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion*/

        /* #region Board of study List By Faculty Id
        public List<BoardOfStudy> MstBoardofStudyListGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstBoardOfStudyGetByFacultyId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<BoardOfStudy> ObjLstBOS = new List<BoardOfStudy>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudy ObjBOS = new BoardOfStudy();

                        ObjBOS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjBOS.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BoardName"]);
                        ObjBOS.FacultyId = (Convert.ToInt32(Dt.Rows[i]["FacultyId"]).ToString()).IsNullOrWhiteSpace() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjBOS.FacultyName = ((Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["FacultyName"])));
                        //ObjBOS.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjBOS.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //ObjBOS.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstBOS.Add(ObjBOS);
                    }

                }

                return ObjLstBOS;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion*/
        //Gayatri's Code
        public MstPaper MstPaperByIdandSection(int? Id, string AssessType, int? TLMId, int? AMId, string SectionName)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstPaperGetByIdandSection", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Id", Id);
                Cmd.Parameters.AddWithValue("@SectionName", SectionName);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                MstPaper objPaper = new MstPaper();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if (Dt.Rows[i]["AssessmentType"].ToString() == AssessType
                            && Convert.ToInt32(Dt.Rows[i]["TLMId"]) == TLMId
                            && Convert.ToInt32(Dt.Rows[i]["AMId"]) == AMId
                            )
                        {
                            objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                            objPaper.SubjectId = Convert.ToString(Dt.Rows[i]["SubjectId"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SubjectId"]);
                            objPaper.SubjectName = Convert.ToString(Dt.Rows[i]["SubjectName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                            //objPaper.FacultyId = Convert.ToString(Dt.Rows[0]["FacultyId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                            //objPaper.FacultyName = Convert.ToString(Dt.Rows[0]["FacultyName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                            objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                            objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                            objPaper.IsCredit = Convert.ToString(Dt.Rows[i]["IsCredit"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCredit"]);
                            objPaper.MaxMarks = Convert.ToString(Dt.Rows[i]["AssessmentTypeMaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMaxMarks"]);
                            objPaper.MinMarks = Convert.ToString(Dt.Rows[i]["AssessmentTypeMinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMinMarks"]);
                            if (Dt.Rows[i]["Credits"].ToString() != "")
                            {
                                objPaper.Credits = Convert.ToString(Dt.Rows[i]["Credits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["Credits"]);
                            }
                            else { objPaper.Credits = 0; }
                            objPaper.IsSeparatePassingHead = Convert.ToString(Dt.Rows[i]["IsSeparatePassingHead"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSeparatePassingHead"]);
                            objPaper.EvaluationId = Convert.ToString(Dt.Rows[i]["EvaluationId"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                            //objPaper.EvaluationName = Convert.ToString(Dt.Rows[0]["EvaluationName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["EvaluationName"]);
                            objPaper.IsActive = Convert.ToString(Dt.Rows[i]["IsActive"]).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]); ;
                            //objPaper.IsActiveSts = Convert.ToString(Dt.Rows[0]["IsActiveSts"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                            objPaper.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]); ;
                            objPaper.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                            objPaper.SectionName = Convert.ToString(Dt.Rows[i]["SectionName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SectionName"]);
                            objPaper.SectionMarks = Convert.ToString(Dt.Rows[i]["SectionMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SectionMarks"]);
                        }

                    }




                }
                return objPaper;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}