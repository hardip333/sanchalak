var app = angular.module('MSUISApp', ["ui.router", 'ngStorage', 'ngMessages', 'ngCookies', "ngMaterial", "ngTable", "ngFileUpload", 'dx']);

app.factory('httpRequestInterceptor', function ($cookies) {
    return {
        request: function (config) {
            if (config.url.indexOf('api/') === 0) {


                //config.url = "http://admission.msubaroda.ac.in/MSUIS_AdminAPI/" + config.url;
				config.url = "https://localhost:44374/" + config.url;
				
                config.headers['token'] = $cookies.get("token");
                
                config.headers['userRoleToken'] = $cookies.get("userRoleToken"); 
                config.headers['roleId'] = $cookies.get("roleId");
				config.headers['passbookImage'] = $cookies.get("passbookImage");
                config.headers['InstGroupChangeRTGSForm'] = $cookies.get("InstGroupChangeRTGSForm");
                config.headers['facultyDepartIntituteId'] = $cookies.get("facultyDepartIntituteId");
                config.headers['FacultyId'] = $cookies.get("FacultyId");
                config.headers['InstituteId'] = $cookies.get("InstituteId");
                config.headers['DepartmentId'] = $cookies.get("DepartmentId");
                config.headers['typePrefix'] = $cookies.get("typePrefix");
                config.headers['PassbookDoc'] = $cookies.get("PassbookDoc");
                config.headers['RTGSForm'] = $cookies.get("RTGSForm");
                config.headers['dsFileName'] = $cookies.get("dsFileName");
                config.headers['DualAdmissionDoc'] = $cookies.get("DualAdmissionDoc");
                config.headers['PersonalDocs'] = $cookies.get("PersonalDocs"); // Added by Mohini on 04-June-2022
                config.headers['AdmApplicantRegiUserName'] = $cookies.get("AdmApplicantRegiUserName"); // Added by Mohini on 04-June-2022
                config.headers['EducationFileName'] = $cookies.get("EducationFileName");
                config.headers['fname'] = $cookies.get("fname");
				config.headers['HallTicketInsSignature'] = $cookies.get("HallTicketInsSignature");
				config.headers['HandWrittenDocument'] = $cookies.get("HandWrittenDocument");
				
                if (!config.url.includes("UploadPhoto") && (!config.url.includes("UploadStudentHandWrittenDocument")) && (!config.url.includes("UploadHallTicketSignature")) && (!config.url.includes("UploadInstituteQueryDocument")) && (!config.url.includes("UploadUserNotificationDocument")) && (!config.url.includes("UploadPassbookPhoto")) && (!config.url.includes("UploadRTGSForm")) && (!config.url.includes("UploadFile_Sign")) && (!config.url.includes("UploadDualAdmissionDoc")) && (!config.url.includes("UploadPersonalDocs")) && (!config.url.includes("UploadEduFiles"))) {
                    config.headers['Content-Type'] = 'application/json';
                }
                else {
                    config.headers['InstituteQueryImageDoc'] = $cookies.get("InstituteQueryImageDoc");
                    config.headers['UserNotificationImageDoc'] = $cookies.get("UserNotificationImageDoc");
                    config.headers['ImportExcelFile'] = $cookies.get("ImportExcelFile");
                }                
                
            }

            if (config.url.indexOf('MSUISApi/') === 0) {

                
                //config.url = "http://admission.msubaroda.ac.in/" + config.url;
                config.url = "http://172.25.15.22/" + config.url;

                config.headers['token'] = $cookies.get("token");

                config.headers['userRoleToken'] = $cookies.get("userRoleToken");
                config.headers['roleId'] = $cookies.get("roleId");
                config.headers['passbookImage'] = $cookies.get("passbookImage");
                config.headers['InstGroupChangeRTGSForm'] = $cookies.get("InstGroupChangeRTGSForm");
                config.headers['facultyDepartIntituteId'] = $cookies.get("facultyDepartIntituteId");
                config.headers['FacultyId'] = $cookies.get("FacultyId");
                config.headers['InstituteId'] = $cookies.get("InstituteId");
                config.headers['DepartmentId'] = $cookies.get("DepartmentId");
                config.headers['typePrefix'] = $cookies.get("typePrefix");
                config.headers['PassbookDoc'] = $cookies.get("PassbookDoc");
                config.headers['RTGSForm'] = $cookies.get("RTGSForm");
                config.headers['dsFileName'] = $cookies.get("dsFileName");
                config.headers['DualAdmissionDoc'] = $cookies.get("DualAdmissionDoc");
                config.headers['PersonalDocs'] = $cookies.get("PersonalDocs"); // Added by Mohini on 04-June-2022
                config.headers['AdmApplicantRegiUserName'] = $cookies.get("AdmApplicantRegiUserName"); // Added by Mohini on 04-June-2022
                config.headers['EducationFileName'] = $cookies.get("EducationFileName");
                config.headers['fname'] = $cookies.get("fname");
                config.headers['HallTicketInsSignature'] = $cookies.get("HallTicketInsSignature");

                if (!config.url.includes("UploadPhoto") && (!config.url.includes("UploadHallTicketSignature")) && (!config.url.includes("UploadInstituteQueryDocument")) && (!config.url.includes("UploadUserNotificationDocument")) && (!config.url.includes("UploadPassbookPhoto")) && (!config.url.includes("UploadRTGSForm")) && (!config.url.includes("UploadFile_Sign")) && (!config.url.includes("UploadDualAdmissionDoc")) && (!config.url.includes("UploadPersonalDocs")) && (!config.url.includes("UploadEduFiles"))) {
                    config.headers['Content-Type'] = 'application/json';
                }
                else {
                    config.headers['InstituteQueryImageDoc'] = $cookies.get("InstituteQueryImageDoc");
                    config.headers['UserNotificationImageDoc'] = $cookies.get("UserNotificationImageDoc");
                    config.headers['ImportExcelFile'] = $cookies.get("ImportExcelFile");
                }

            }

            return config;
        }
    };
});

app.config(["$stateProvider", "$urlRouterProvider", "$httpProvider", function (e, t, $httpProvider) {
    
    $httpProvider.interceptors.push('httpRequestInterceptor');

    t.otherwise("/login"),


        e.state("boxed", {
            url: "",
            "abstract": !0,
            templateUrl: "UI/layouts/common/boxed.html"
        });

        e.state("plain", {
            url: "",
            "abstract": !0,
            templateUrl: "UI/layouts/common/plain.html"
        });

        e.state("dashboard1", {
            url: "/dashboard",
            parent: "plain",
            templateUrl: "UI/layouts/admin/Dashboard.html",
            controller: "dashboardCtrl"
        });

    e.state("dashboardadmin", {
        url: "/dashboard",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/Dashboard.html",
        controller: "dashboardCtrl"
    });
	e.state("dashboarddetailsadmin", {
        url: "/dashboarddetailsadmin",
        parent: "dashboard1",
        params: {
            academicYear: null,
            Heading: null,
            checkName: null,
            statTag: null,
            facultyName: null,
            departmentName: null,
            modifiedInstituteId: null,
            modifiedDepartmentId: null,
            modifiedExamEventId: null
            
        },
        templateUrl: "UI/pages/admin/DashboardDetails.html",
        controller: "dashboardDetailsCtrl"
    });
e.state("Mandate", {
        url: "/Mandate",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/Mandate.html",
        controller: "MandateCtrl"
    });
 e.state("StudentTransferByDestInst", {
        url: "/StudentTransferByDestInst",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentTransferRequestByDestInst.html",
        controller: "StudentTransferRequestCtrl"
    });

    e.state("StudentTransferProfile", {
        url: "/StudentTransferProfile",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentTransferProfile.html",
        controller: "StudentTransferRequestCtrl"
    });

    e.state("StudentTransferRequest", {
        url: "/StudentTransferRequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentTransferRequestInstituteWise.html",
        controller: "StudentTransferRequestCtrl"
    });
  e.state("AdmPaymentTransactionReportByInstitute", {
        url: "/AdmPaymentTransactionReportByInstitute",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmPaymentTransactionReportByInstitute.html",
        controller: "AdmPaymentTransactionCtrl"
    });
    e.state("StudentPaymentTransactionInfo", {
        url: "/StudentPaymentTransactionInfo",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentAdmPaymentTransactionInfo.html",
        controller: "AdmPaymentTransactionCtrl"
    });
    e.state("AdmPaymentTransactionReport", {
        url: "/AdmPaymentTransactionReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmPaymentTransactionReport.html",
        controller: "AdmPaymentTransactionCtrl"
    });

 e.state("AdditionalPassingHeadConfigAdd", {
        url: "/AdditionalPassingHeadConfigAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResConfigAPHAdd.html",
        controller: "ResConfigAPHCtrl"
    });

    e.state("AdditionalPassingHeadEdit", {
        url: "/AdditionalPassingHeadEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResDefineAPHEdit.html",
        controller: "ResDefineAPHCtrl"
    });
    e.state("AdditionalPassingHead", {
        url: "/AdditionalPassingHead",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResDefineAPHAdd.html",
        controller: "ResDefineAPHCtrl"
    });

    e.state("ResBasisForAwarding", {
        url: "/ResBasisForAwarding",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResBasisForAwardingAdd.html",
        controller: "ResBasisForAwardingCtrl"
    });

    e.state("ResCalculation", {
        url: "/ResCalculation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResCalculationAdd.html",
        controller: "ResCalculationCtrl"
    });

    e.state("ResGPATemplateConfigAdd", {
        url: "/ResGPATemplateConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGPATemplateConfigAdd.html",
        controller: "ResGPATemplateConfigCtrl"
    });

    e.state("ResGPATemplateAdd", {
        url: "/ResGPATemplate/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGPATemplateAdd.html",
        controller: "ResGPATemplateCtrl"
    });

    e.state("ResGPATemplateEdit", {
        url: "/ResGPATemplate/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGPATemplateEdit.html",
        controller: "ResGPATemplateCtrl"
    });
    e.state("ResGradeLevelsAdd", {
        url: "/ResGradeLevels/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGradeLevelsAdd.html",
        controller: "ResGradeLevelsCtrl"
    });

    e.state("ResGradeLevelsEdit", {
        url: "/ResGradeLevels/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGradeLevelsEdit.html",
        controller: "ResGradeLevelsCtrl"
    });

    e.state("ResGradeScalesAdd", {
        url: "/ResGradeScales/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGradeScalesAdd.html",
        controller: "ResGradeScalesCtrl"
    });

    e.state("ResGradeScalesEdit", {
        url: "/ResGradeScales/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResGradeScalesEdit.html",
        controller: "ResGradeScalesCtrl"
    });

    e.state("ResMarksTemplateConfigAdd", {
        url: "/ResMarksTemplateConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResMarksTemplateConfigAdd.html",
        controller: "ResMarksTemplateConfigCtrl"
    });

    e.state("ResMarksTemplateAdd", {
        url: "/ResMarksTemplate/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResMarksTemplateAdd.html",
        controller: "ResMarksTemplateCtrl"
    });

    e.state("ResMarksTemplateEdit", {
        url: "/ResMarksTemplate/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResMarksTemplateEdit.html",
        controller: "ResMarksTemplateCtrl"
    });

    e.state("ResMarksLevelsAdd", {
        url: "/ResMarksLevels/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResMarksLevelsAdd.html",
        controller: "ResMarksLevelsCtrl"
    });

    e.state("ResMarksLevelsEdit", {
        url: "/ResMarksLevels/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResMarksLevelsEdit.html",
        controller: "ResMarksLevelsCtrl"
    });
	e.state("ApplicationStatisticsByInstitute", {
        url: "/ApplicationStatisticsByInstitute",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationStatisticsByInstitute.html",
        controller: "ApplicationStatisticsCtrl"
    });
e.state("ApplicationListAppFeeNotPaid", {
        url: "/ApplicationListAppFeeNotPaid",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationListWhoseAppFeeNotPaid.html",
        controller: "ApplicationListCtrl"
    });
	e.state("ProgrammeStatisticsByInstitute", {
        url: "/ProgrammeStatisticsByInstitute",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeConfigurationStatisticsByInstitute.html",
        controller: "ApplicationStatisticsCtrl"
    });
 e.state("ProgrammeStatistics", {
        url: "/ProgrammeStatistics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeConfigurationStatistics.html",
        controller: "ApplicationStatisticsCtrl"
    });
 e.state("AdmApplicationAdmFeeNotPaid", {
        url: "/AdmApplicationAdmFeeNotPaid",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmApplicationAdmFeeNotPaid.html",
        controller: "AdmApplicationAdmFeeNotPaidCtrl"
    });

    e.state("ApplicationStatistics", {
        url: "/ApplicationStatistics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationStatistics.html",
        controller: "ApplicationStatisticsCtrl"
    });

    e.state("ApplicationList", {
        url: "/ApplicationList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationList.html",
        controller: "ApplicationListCtrl"
    });

e.state("AdminChangePassword", {
        url: "/AdminChangePassword",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdminChangePassword.html",
        controller: "AdminChangePasswordCtrl"
    });

    e.state("PreProgInstanceConfigAdd", {
        url: "/PreProgInstanceConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstanceConfigAdd.html",
        controller: "PreProgramInstanceCtrl"
    });

    e.state("PreProgInstanceConfigEdit", {
        url: "/PreProgInstanceConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstanceConfigEdit.html",
        controller: "PreProgramInstanceCtrl"
    });

    e.state("PreProgInstancePartConfigAdd", {
        url: "/PreProgInstancePartConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstancePartConfigAdd.html",
        controller: "PreProgrammeInstancePartCtrl"
    });

    e.state("PreProgInstancePartConfigEdit", {
        url: "/PreProgInstancePartConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstancePartConfigEdit.html",
        controller: "PreProgrammeInstancePartCtrl"
    });

    e.state("PreProgInstancePartTermConfigAdd", {
        url: "/PreProgInstancePartTermConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstancePartTermConfigAdd.html",
        controller: "PreProgrammeInstancePartTermCtrl"
    });

    e.state("PreProgInstancePartTermConfigEdit", {
        url: "/PreProgInstancePartTermConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgInstancePartTermConfigEdit.html",
        controller: "PreProgrammeInstancePartTermCtrl"
    });

    e.state("ProgrammeInstanceAdd", {
        url: "/ProgInstanceConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstanceAdd.html",
        controller: "ProgrammeInstanceCtrl"
    });

    e.state("ProgrammeInstanceEdit", {
        url: "/ProgInstanceConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstanceEdit.html",
        controller: "ProgrammeInstanceCtrl"
    });

    e.state("ProgrammeInstancePartAdd", {
        url: "/ProgInstancePartConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstancePartAdd.html",
        controller: "ProgrammeInstancePartCtrl"
    });

    e.state("ProgrammeInstancePartEdit", {
        url: "/ProgInstancePartConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstancePartEdit.html",
        controller: "ProgrammeInstancePartCtrl"
    });

    e.state("ProgrammeInstancePartTermAdd", {
        url: "/ProgInstancePartTermConfig/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstancePartTermAdd.html",
        controller: "ProgrammeInstancePartTermCtrl"
    });

    e.state("ProgrammeInstancePartTermEdit", {
        url: "/ProgInstancePartTermConfig/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeInstancePartTermEdit.html",
        controller: "ProgrammeInstancePartTermCtrl"
    });

    e.state("PreApplicationConfigurationAdd", {
        url: "/PreApplicationConfiguration/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreApplicationConfigurationAdd.html",
        controller: "PreApplicationConfigCtrl"
    });

    e.state("PreApplicationConfigurationEdit", {
        url: "/PreApplicationConfiguration/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreApplicationConfigurationEdit.html",
        controller: "PreApplicationConfigCtrl"
    });

    e.state("PostConfiguration", {
        url: "/PostConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostConfiguration.html",
        controller: "PostConfigurationCtrl"
    });

    e.state("PostApplicantVerification", {
        url: "/PostApplicantVerification",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostApplicantVerification.html",
        controller: "PostApplicantVerificationCtrl"
    });
	
	e.state("PostAdmProgrammeAddOnCriteria", {
        url: "/PostAdmProgrammeAddOnCriteria",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostAdmProgrammeAddOnCriteria.html",
        controller: "PostApplicantVerificationCtrl"
    });
	
	e.state("PreAdmProgrammeAddOnCriteria", {
        url: "/PreAdmProgrammeAddOnCriteria",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreAdmProgrammeAddOnCriteria.html",
        controller: "PreApplicantVerificationCtrl"
    });

    e.state("PostConfigurationByAcademic", {
        url: "/PostConfigurationByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostConfigurationByAcademic.html",
        controller: "PostConfigurationByAcademicCtrl"
    });

    e.state("PostApplicantVerificationByAcademic", {
        url: "/PostApplicantVerificationByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostApplicantVerificationByAcademic.html",
        controller: "PostApplicantVerificationByAcademicCtrl"
    });

    e.state("PostSMSEmailConfigByAcademic", {
        url: "/PostSMSEmailConfigByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostSMSEmailConfigByAcademic.html",
        controller: "PostSMSEmailConfigByAcademicCtrl"
    });

    e.state("PostSMSEmailConfigByFaculty", {
        url: "/PostSMSEmailConfigByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostSMSEmailConfigByFaculty.html",
        controller: "PostSMSEmailConfigByFacultyCtrl"
    });

    e.state("RefundCaseByAcademic", {
        url: "/RefundCaseByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseByAcademic.html",
        controller: "RefundCaseByAcademicCtrl"
    });

    e.state("RefundCaseApplicantDetails", {
        url: "/RefundCaseApplicantDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseApplicantDetails.html",
        controller: "RefundCaseByAcademicCtrl"
    });


    e.state("RefundCaseByAudit", {
        url: "/RefundCaseByAudit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseByAudit.html",
        controller: "RefundCaseByAuditCtrl"
    });

    e.state("RefundCaseApplicantDetailsByAudit", {
        url: "/RefundCaseApplicantDetailsByAudit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseApplicantDetailsByAudit.html",
        controller: "RefundCaseByAuditCtrl"
    });

    e.state("RefundCaseByAccount", {
        url: "/RefundCaseByAccount",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseByAccount.html",
        controller: "RefundCaseByAccountCtrl"
    });

    e.state("RefundCaseCancelByAccount", {
        url: "/RefundCaseCancelByAccount",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelByAccount.html",
        controller: "RefundCaseCancelByAccountCtrl"
    });

    e.state("RefundCaseCancelByAcademic", {
        url: "/RefundCaseCancelByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelByAcademic.html",
        controller: "RefundCaseCancelByAcademicCtrl"
    });

    e.state("RefundCaseCancelApplicantDetails", {
        url: "/RefundCaseCancelApplicantDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelApplicantDetails.html",
        controller: "RefundCaseCancelByAcademicCtrl"
    });

    e.state("RefundCaseCancelByAudit", {
        url: "/RefundCaseCancelByAudit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelByAudit.html",
        controller: "RefundCaseCancelByAuditCtrl"
    });

    e.state("RefundCaseCancelApplicantDetailsByAudit", {
        url: "/RefundCaseCancelApplicantDetailsByAudit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelApplicantDetailsByAudit.html",
        controller: "RefundCaseCancelByAuditCtrl"
    });

    e.state("RefundCaseCancelRefundReport", {
        url: "/RefundCaseCancelRefundReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseCancelRefundReport.html",
        controller: "RefundCaseCancelRefundReportCtrl"
    });

    e.state("RefundCaseRefundReport", {
        url: "/RefundCaseRefundReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundCaseRefundReport.html",
        controller: "RefundCaseRefundReportCtrl"
    });

    e.state("DualAdmissionByAcademic", {
        url: "/DualAdmissionByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DualAdmissionByAcademic.html",
        controller: "DualAdmissionByAcademicCtrl"
    });

    e.state("PaperStudentReportAcademic", {
        url: "/PaperStudentReportAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SelectedPaperReportAcademic.html",
        controller: "SelectedPaperReportAcademicCtrl"
    });

    e.state("TimeTableScheduled", {
        url: "/TimeTableScheduled",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TimeTableScheduled.html",
        controller: "TimeTableScheduledCtrl"
    });

    e.state("MultipleProgrammesAdmittedList", {
        url: "/MultipleProgrammesAdmittedList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MultipleProgrammesAdmittedList.html",
        controller: "MultipleProgrammesAdmittedListCtrl"
    });

    e.state("ApplicationConfigurationByAcademic", {
        url: "/ApplicationConfigurationByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationConfigurationByAcademic.html",
        controller: "ApplicationConfigurationByAcademicCtrl"
    });

    e.state("MstDesignation", {
        url: "/MstDesignation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstDesignation.html",
        controller: "MstDesignationCtrl"
    });

    e.state("MstUniversitySectionNew", {
        url: "/MstUniversitySectionNew",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstUniversitySectionNew.html",
        controller: "MstUniversitySectionNewCtrl"
    });

    e.state("MstUserNew", {
        url: "/MstUserNew",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstUserNew.html",
        controller: "MstUserNewCtrl"
    });

    e.state("RolePermissionMap", {
        url: "/RolePermissionMap",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RolePermissionMap.html",
        controller: "RolePermissionMapCtrl"
    });

    e.state("UserRoleMap", {
        url: "/UserRoleMap",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/UserRoleMap.html",
        controller: "UserRoleMapCtrl"
    });
	
	e.state("ReportForJrSupervisor", {
        url: "/ReportForJrSupervisor",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ReportForJrSupervisor.html",
        controller: "ReportForJrSupervisorCtrl"
    });

    e.state("VenueAllocation", {
        url: "/VenueAllocation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VenueAllocation.html",
        controller: "VenueAllocationCtrl"
    });
	
	e.state("VenueReAllocation", {
        url: "/VenueReAllocation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VenueReAllocation.html",
        controller: "VenueReAllocationCtrl"
    });
	
	e.state("TimeSlot", {
        url: "/TimeSlot",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TimeSlot.html",
        controller: "TimeSlotCtrl"
    });

    e.state("ExamVenueExamCenter", {
        url: "/ExamVenueExamCenter",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamVenueExamCenter.html",
        controller: "ExamVenueExamCenterCtrl"
    });
	
	 e.state("ReportForJrSupervisorCenterWise", {
        url: "/ReportForJrSupervisorCenterWise",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ReportForJrSupervisorCenterWise.html",
        controller: "ReportForJrSupervisorCenterWiseCtrl"
    });

	e.state("ReportForJrSupervisorIAExam", {
        url: "/ReportForJrSupervisorIAExam",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ReportForJrSupervisorIAExam.html",
        controller: "ReportForJrSupervisorIAExamCtrl"
    });
	
	e.state("PreConfigurationForApplication", {
        url: "/PreConfigurationForApplication",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreConfigurationForApplication.html",
        controller: "PreConfigurationForApplicationCtrl"
    });

    e.state("PreApplicantVerification", {
        url: "/PreApplicantVerification",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreApplicantVerification.html",
        controller: "PreApplicantVerificationCtrl"
    });
	
	 e.state("DownloadWrittenTestList", {
        url: "/DownloadWrittenTestList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DownloadWrittenTestList.html",
        controller: "DownloadWrittenTestListCtrl"
    });
	
	e.state("InwardStatistics", {
        url: "/InwardStatistics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InwardStatistics.html",
        controller: "InwardStatCtrl"
    });
	
    // Mohini's State's
	
    e.state("PostApprovalList", {
        url: "/PostApprovalList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostApprovalList.html",
        controller: "PostApprovalListCtrl"
    });

    e.state("PostApprovedStudentsList", {
        url: "/PostApprovedStudentsList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PostApprovedStudentsList.html",
        controller: "PostApprovalListCtrl"
    });

    e.state("eligible-degree", {
        url: "/eligible-degree",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/eligible-degree.html",
        controller: "eligibledegreeCtrl"
    });

    e.state("eligibility-specialization", {
        url: "/eligibility-specialization",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/eligibility-specialization.html",
        controller: "eligibilityspecializationCtrl"
    });

    e.state("AdmEligibilityGroupAdd", {
        url: "/EligibilityGroup/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmEligibilityGroupAdd.html",
        controller: "AdmEligibilityGroupCtrl"
    });

    e.state("AdmEligibilityGroupEdit", {
        url: "/EligibilityGroup/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmEligibilityGroupEdit.html",
        controller: "AdmEligibilityGroupCtrl"
    });
    e.state("AdmEligibilityGroupComponentAdd", {
        url: "/EligibilityGroupComponent/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmEligibilityGroupComponentAdd.html",
        controller: "AdmEligibilityGroupComponentCtrl"
    });

    e.state("AdmEligibilityGroupComponentEdit", {
        url: "/EligibilityGroupComponent/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmEligibilityGroupComponentEdit.html",
        controller: "AdmEligibilityGroupComponentCtrl"
    });
    e.state("AdmProgrammeAddOnCriteriaAdd", {
        url: "/ProgrammeAddOnCriteria/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmProgrammeAddOnCriteriaAdd.html",
        controller: "AdmProgrammeAddOnCriteriaCtrl"
    });

    e.state("AdmProgrammeAddOnCriteriaEdit", {
        url: "/ProgrammeAddOnCriteria/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmProgrammeAddOnCriteriaEdit.html",
        controller: "AdmProgrammeAddOnCriteriaCtrl"
    });
    e.state("AdmRequiredDocumentsProgramAdd", {
        url: "/RequiredDocumentsProgram/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmRequiredDocumentsProgramAdd.html",
        controller: "AdmRequiredDocumentsProgramCtrl"
    });

    e.state("AdmRequiredDocumentsProgramEdit", {
        url: "/RequiredDocumentsProgram/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmRequiredDocumentsProgramEdit.html",
        controller: "AdmRequiredDocumentsProgramCtrl"
    });
    e.state("AdmRequiredDocumentsProgram", {
        url: "/AdmRequiredDocumentsProgram",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmRequiredDocumentsProgram.html",
        controller: "AdmRequiredDocumentsProgramCtrl"
    });
    e.state("AdmEligibilityCriteriaComponent", {
        url: "/AdmEligibilityCriteria",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmEligibilityCriteriaComponent.html",
        controller: "AdmEligibilityCriteriaComponentCtrl"
    });
    //e.state("PreConfiguration", {
    //    url: "/PreConfiguration",
    //    parent: "dashboard1",
    //    templateUrl: "UI/pages/admin/PreConfiguration.html",
    //    controller: "ProgrammeInstancePartTermCtrl"
    //});

    //Hardip , Jay States Start

    e.state("ExamFormDiscrepancy", {
        url: "/ExamFormDiscrepancyReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormDisc.html",
        controller: "ExamFormDiscCtrl"
    });

    //Hardip , Jay States End;

    e.state("PreConfiguration", {
        url: "/PreConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreConfiguration.html",
        controller: "PreConfigurationCtrl"
    });
    e.state("SemesterConfiguration", {
        url: "/SemesterConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreProgPartTermConfig.html",
        controller: "PreProgrammeInstancePartTermCtrl"
    });
    e.state("test2", {
        url: "/test",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/test2.html",
        controller: "ProgrammeInstancePartTermCtrl"
    });

    e.state("login", {
        url: "/login",
        parent: "plain",
        templateUrl: "UI/pages/admin/login.html",
        controller: "loginCtrl"
    });

    e.state("index", {
        url: "/index",
        parent: "plain",
        templateUrl: "UI/pages/admin/login.html",
        controller: "IndexCtrl"
    });

    e.state("home", {
        url: "/home",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/home.html",
        controller: "usersCtrl"
    });

    e.state("users", {
        url: "/users",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/users.html",
        controller: "usersCtrl"
    });

    e.state("students", {
        url: "/students",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/student.html",
        controller: "studentsCtrl"
    });   

  
	e.state("EmpDashboard1", {
        url: "/ConfirmRole1",
        parent: "plain",
        templateUrl: "UI/pages/admin/confirmRole1.html",
        controller: "EmpDashboardCtrl"
     });
 //Gayatri Start State

    e.state("CountryMasterEdit", {
        url: "/CountryMaster/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CountryEdit.html",
        controller: "CountryMasterCtrl"
    });
    e.state("CountryMasterAdd", {
        url: "/CountryMaster/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CountryAdd.html",
        controller: "CountryMasterCtrl"
    });
    e.state("StateMasterEdit", {
        url: "/StateMaster/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StateEdit.html",
        controller: "StateMasterCtrl"
    });
    e.state("StateMasterAdd", {
        url: "/StateMaster/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StateAdd.html",
        controller: "StateMasterCtrl"
    });
    e.state("InstructionNameAdd", {
        url: "/InstructionName/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstructionNameAdd.html",
        controller: "InstructionNameCtrl"
    });
    e.state("InstructionNameEdit", {
        url: "/InstructionName/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstructionNameEdit.html",
        controller: "InstructionNameCtrl"
    });
	e.state("MstSocialCategoryEdit", {
        url: "/SocialCategory/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SocialCategoryEdit.html",
        controller: "SocialCategoryCtrl"
    });
    e.state("MstSocialCategoryAdd", {
        url: "/SocialCategory/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SocialCategoryAdd.html",
        controller: "SocialCategoryCtrl"
    });
	e.state("MstUniversitySectionAdd", {
        url: "/MstUniversitySection/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstUniversitySectionAdd.html",
        controller: "MstUniversitySectionCtrl"
    });
    e.state("MstUniversitySectionEdit", {
        url: "/MstUniversitySection/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstUniversitySectionEdit.html",
        controller: "MstUniversitySectionCtrl"
    });
    e.state("MstProgrammePartTermGroupMapAdd", {
        url: "/MstProgrammePartTermGroupMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartTermGroupMapAdd.html",
        controller: "MstProgrammePartTermGroupMapCtrl"
    });
    e.state("MstProgrammePartTermGroupMapEdit", {
        url: "/MstProgrammePartTermGroupMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartTermGroupMapEdit.html",
        controller: "MstProgrammePartTermGroupMapCtrl"
    });
    e.state("PublishFeeReport", {
        url: "/PublishFeeReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PublishFeeReport.html",
        controller: "FeeTypeReportCtrl"
    });
    e.state("UnpublishFeeReport", {
        url: "/UnpublishFeeReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/UnpublishFeeReport.html",
        controller: "FeeTypeReportCtrl"
    });
    e.state("ApplicationFeesPaid", {
        url: "/ApplicationFeesPaid",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationFeesPaid.html",
        controller: "ApplicationFeesReportCtrl"
    });
    e.state("ApplicationFeesUnpaid", {
        url: "/ApplicationFeesUnpaid",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationFeesUnpaid.html",
        controller: "ApplicationFeesReportCtrl"
    });
    e.state("AdmissionFeesReport", {
        url: "/AdmissionFeesReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmissionFeesReport.html",
        controller: "AdmissionFeesReportCtrl"
    });
    e.state("FeeCategoryChange", {
        url: "/FeeCategoryChange",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeCategoryChange.html",
        controller: "FeeCategoryChangeCtrl"
    });
	e.state("ResCourseEvalSystem", {
        url: "/ResCourseEvalSystem",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResCourseEvalSystem.html",
        controller: "ResCourseEvalSystemCtrl"
    });
    e.state("ResExemptionConfig", {
        url: "/ResExemptionConfig",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ResExemptionConfig.html",
        controller: "ResExemptionConfigCtrl"
    });
	e.state("ResExemptionDependency", {
        url: "/ResExemptionDependency",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NewResExemptionDependency.html",
        controller: "NewResExemptionDependencyCtrl" //NewResExemptionDependencyCtrl
    });
    e.state("NewResExemptionDependency1", {
        url: "/NewResExemptionDependency1",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NewResExemptionDependency1.html",
        controller: "NewResExemptionDependencyCtrl"
    });
    e.state("Sectionwiseblankmarksheet", {
        url: "/Sectionwiseblankmarksheet",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/Sectionwiseblankmarksheet.html",
        controller: "SectionwiseblankmarksheetCtrl"
    });
	e.state("LaunchResultConfiguration", {
        url: "/LaunchResultConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/LaunchResultConfiguration.html",
        controller: "LaunchResultConfigurationCtrl"
    });
	//Gayatri End State
    
	
    //Start Jay Project State 24032021

    e.state("VerificationReport", {
        url: "/Verification/Report",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerificationReport.html",
        controller: "VerificationReportCtrl"
    });


/* Branch Change*/
    //Jay Dated 07102021
    e.state("BranchChange", {
        url: "/BranchChangeRequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/BranchChange.html",
        controller: "BranchChangeCtrl"
    });

    //Jay Dated 14032022
    e.state("HallTicketPublish", {
        url: "/HallTicket/Publish",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PublishHallTicket.html",
        controller: "PublishHallTicketCtrl"
    });
    //Jay Dated 20042022
    e.state("ReportForJrSupervisorAllBlock", {
        url: "/ReportForJrSupervisorAllBlock",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ReportForJrSupervisorAllBlock.html",
        controller: "ReportForJrSupervisorAllBlockCtrl"
    });

    //Jay Dated 22012022
    e.state("StudentPartTermPaperReport", {
        url: "/SemesterwiseStudent-PaperList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentPartTermPaperReport.html",
        controller: "StudentPartTermPaperReportCtrl"
    });
    /* Group and paper report  AND Assessment report*/
    e.state("GetGAPReport", {
        url: "/Groupandpaperreport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GetGAPReport.html",
        controller: "SRARCtrl"
    });
    e.state("StructureReport", {
        url: "/StructureReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StructureReport.html",
        controller: "SRARCtrl"
    });
    
    e.state("AssessmentReport", {
        url: "/AssessmentReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AssessmentReport.html",
        controller: "SRARCtrl"
    });
    e.state("GAPSelectFaculty", {
        url: "/Paper/Selection",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GAPSelect.html",
        controller: "GAPRCtrl"
    });
    e.state("GAPReport", {
        url: "/PaperSelection",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GAPReport.html",
        controller: "GAPRCtrl"
    });
    e.state("PaperStudentReport", {
        url: "/PaperStudentReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SelectedPaperReport.html",
        controller: "SelectedPaperReportCtrl"
    });
    /* Group ,PartTerm and Preference mapping */
    e.state("PreferencePTGroupMap", {
        url: "/PreferenceGroupSelect",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreferencePTGroupMap.html",
        controller: "GAPRCtrl"
    });
    e.state("PreferencePTGroupMapEdit", {
        url: "/PreferenceGroupSelectList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreferencePTGroupMapEdit.html",
        controller: "GAPRCtrl"
    });

    e.state("IPTPMEdit", {
        url: "/InstitutePaper/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IPTPMEdit.html",
        controller: "IPTPMCtrl"
    });
    e.state("IPTPMAdd", {
        url: "/InstitutePaper/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IPTPMAdd.html",
        controller: "IPTPMCtrl"
    });
    /* PreRequisite */
    e.state("PrereqWithinAdd", {
        url: "/PrerequisiteAddWithinSem",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PrereqWithinAdd.html",
        controller: "PrereqCtrl"
    });
    e.state("PrereqAcrossAdd", {
        url: "/PrerequisiteAddAcrossSem",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PrereqAcrossAdd.html",
        controller: "PrereqCtrl"
    });
    e.state("PrereqManage", {
        url: "/PrerequisiteManage",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ManagePrereq.html",
        controller: "PrereqCtrl"
    });

    e.state("ApplicationFeeEdit", {
        url: "/ApplicationFee/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstApplicationFeeEdit.html",
        controller: "ApplicationFeeCtrl"
    });
    e.state("ApplicationFeeAdd", {
        url: "/ApplicationFee/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstApplicationFeeAdd.html",
        controller: "ApplicationFeeCtrl"
    });

    e.state("FeeCategoryTypeAdd", {
        url: "/FeeCategoryType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeCategoryTypeAdd.html",
        controller: "FeeCategoryTypeCtrl"
    });
    e.state("FeeCategoryTypeEdit", {
        url: "/FeeCategoryType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeCategoryTypeEdit.html",
        controller: "FeeCategoryTypeCtrl"
    });

    e.state("FCAdd", {
        url: "/FeeCategory/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FCAdd.html",
        controller: "FCCtrl"
    });
    e.state("FCEdit", {
        url: "/FeeCategory/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FCEdit.html",
        controller: "FCCtrl"
    });

    e.state("FTAdd", {
        url: "/FeeType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTAdd.html",
        controller: "FTCtrl"
    });
    e.state("FTEdit", {
        url: "/FeeType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTEdit.html",
        controller: "FTCtrl"
    });

    e.state("FHAdd", {
        url: "/FeeHead/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FHAdd.html",
        controller: "FHCtrl"
    });
    e.state("FHEdit", {
        url: "/FeeHead/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FHEdit.html",
        controller: "FHCtrl"
    });

    e.state("FSHAdd", {
        url: "/FeeSubHead/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FSHAdd.html",
        controller: "FSHCtrl"
    });
    e.state("FSHEdit", {
        url: "/FeeSubHead/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FSHEdit.html",
        controller: "FSHCtrl"
    });

    e.state("FTFCMAdd", {
        url: "/FeeTypeCategoryMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTFCMAdd.html",
        controller: "FTFCMCtrl"
    });
    e.state("FTFCMEdit", {
        url: "/FeeTypeCategoryMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTFCMEdit.html",
        controller: "FTFCMCtrl"
    });

    e.state("FTFHMAdd", {
        url: "/FeeTypeHeadMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTFHMAdd.html",
        controller: "FTFHMCtrl"
    });
    e.state("FTFHMEdit", {
        url: "/FeeTypeHeadMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FTFHMEdit.html",
        controller: "FTFHMCtrl"
    });

    e.state("FEECONFIGAdd", {
        url: "/FeeConfiguration/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGAdd.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FEECONFIGEdit", {
        url: "/FeeConfiguration/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGEdit.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FEECONFIGAddNext", {
        url: "/FeeConfiguration/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGAddNext.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FEECONFIGPublish", {
        url: "/FeeConfiguration/Publish",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGPublish.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FEECONFIGUnPublish", {
        url: "/FeeConfiguration/UnPublish",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGUnPublish.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FeeUnpublish", {
        url: "/FeeConfiguration/UnPublish_ok",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeUnpublish.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FEECONFIGVerify", {
        url: "/FeeConfiguration/Verify",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECONFIGVerify.html",
        controller: "FEECONFIGCtrl"
    });
    e.state("FeePrintdata", {
        url: "/FeeConfiguration/Print",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEEPrint.html",
        controller: "FEECONFIGCtrl"
    });

    e.state("FEEADMIN", {
        url: "/FeeConfiguration/Dashboard",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEEADMIN.html",
        controller: "FEECONFIGCtrl"
    });

    e.state("FEECopy", {
        url: "/FeeConfiguration/CopyFee",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FEECopy.html",
        controller: "FEECONFIGCtrl"
    });

    e.state("FEEReport", {
        url: "/FeeConfiguration/Report",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeReport.html",
        controller: "FEECONFIGCtrl"
    });

    e.state("IBMAdd", {
        url: "/InstituteBranchMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IBMAdd.html",
        controller: "IBMCtrl"
    });
    e.state("IBMEdit", {
        url: "/InstituteBranchMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IBMEdit.html",
        controller: "IBMCtrl"
    });
    //End Jay Project State
	
	//Start Bhushan Project State
	
	e.state("InstituteQuery", {
        url: "/InstituteQuery",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteQuery.html",
        controller: "InstituteQueryCtrl",
        params: { obj: null }
    });
    e.state("ApplicationPaymentReport", {
        url: "/ApplicationPaymentReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationPaymentReport.html",
        controller: "ApplicationPaymentReportCtrl"
    });
	
	e.state("ApplicationPaymentReportByInstitute", {
        url: "/ApplicationPaymentReportByInstitute",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationPaymentReportByInstitute.html",
        controller: "ApplicationPaymentReportCtrl"
    });
	
    e.state("PaymentDetails", {
        url: "/PaymentDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaymentDetails.html",
        controller: "PaymentDetailsCtrl",
        params: { obj: null} 
    });
    e.state("CancleStudentApplication", {
        url: "/CancleStudentApplication",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CancleStudentApplication.html",
        controller: "CancleStudentApplicationCtrl",
        params: { obj: null } 
    });
	e.state("MstPreferenceGroupAdd", {
        url: "/MstPreferenceGroup/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreferenceGroupAdd.html",
        controller: "MstPreferenceGroupCtrl"
    });
    e.state("MstPreferenceGroupEdit", {
        url: "/MstPreferenceGroup/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreferenceGroupEdit.html",
        controller: "MstPreferenceGroupCtrl"
    });
    e.state("MstGroupCodeAdd", {
        url: "/MstGroupCode/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstGroupCodeAdd.html",
        controller: "MstGroupCodeCtrl"
    });
    e.state("MstGroupCodeEdit", {
        url: "/MstGroupCode/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstGroupCodeEdit.html",
        controller: "MstGroupCodeCtrl"
    });
    e.state("MstPreferenceCodeMapAdd", {
        url: "/MstPreferenceCodeMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreferenceCodeMapAdd.html",
        controller: "MstPreferenceCodeMapCtrl"
    });
    e.state("MstPreferenceCodeMapEdit", {
        url: "/MstPreferenceCodeMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreferenceCodeMapEdit.html",
        controller: "MstPreferenceCodeMapCtrl"
    });
	e.state("IncStudentAcademicInformation", {
        url: "/IncStudentAcademicInformation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IncStudentAcademicInformation.html",
        controller: "IncStudentAcademicInformationCtrl"
    });
	e.state("AdmApplicationEligibilityStatus", {
        url: "/AdmApplicationEligibilityStatus",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmApplicationEligibilityStatus.html",
        controller: "AdmApplicationEligibilityStatusCtrl"
    });
	e.state("AppearanceTypeAdd", {
        url: "/AppearanceType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AppearanceTypeAdd.html",
        controller: "AppearanceTypeMasterCtrl"
    });

    e.state("AppearanceTypeEdit", {
        url: "/AppearanceType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AppearanceTypeEdit.html",
        controller: "AppearanceTypeMasterCtrl"
    });
    e.state("ExaminationBodyAdd", {
        url: "/ExaminationBody/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExaminationBodyAdd.html",
        controller: "ExaminationBodyCtrl"
    });

    e.state("ExaminationBodyEdit", {
        url: "/ExaminationBody/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExaminationBodyEdit.html",
        controller: "ExaminationBodyCtrl"
    });
    e.state("AdmSpecializationMasterAdd", {
        url: "/AdmSpecializationMaster/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmSpecializationMasterAdd.html",
        controller: "AdmSpecializationMasterCtrl"
    });

    e.state("AdmSpecializationMasterEdit", {
        url: "/AdmSpecializationMaster/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmSpecializationMasterEdit.html",
        controller: "AdmSpecializationMasterCtrl"
    });
    e.state("MstInstituteProgrammeMapAdd", {
        url: "/MstInstituteProgrammeMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteProgrammeMapAdd.html",
        controller: "MstInstituteProgrammeMapCtrl"
    });

    e.state("MstInstituteProgrammeMapEdit", {
        url: "/MstInstituteProgrammeMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteProgrammeMapEdit.html",
        controller: "MstInstituteProgrammeMapCtrl"
    });
    e.state("AddonDetailsReport", {
        url: "/AddonDetailsReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AddonDetailsReport.html",
        controller: "AddonDetailsReportCtrl"
    });
	e.state("AdmAdditionalDocumentsReport", {
        url: "/AdmAdditionalDocumentsReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmAdditionalDocumentsReport.html",
        controller: "AdmAdditionalDocumentsReportCtrl"
    });
	e.state("MstAdmissionCommitteeAdd", {
        url: "/MstAdmissionCommittee/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstAdmissionCommitteeAdd.html",
        controller: "MstAdmissionCommitteeCtrl"
    });
    e.state("MstAdmissionCommitteeEdit", {
        url: "/MstAdmissionCommittee/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstAdmissionCommitteeEdit.html",
        controller: "MstAdmissionCommitteeCtrl"
    });
	e.state("VenueMasterAdd", {
        url: "/VenueMaster/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VenueMasterAdd.html",
        controller: "VenueMasterCtrl"
    });

    e.state("VenueMasterEdit", {
        url: "/VenueMaster/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VenueMasterEdit.html",
        controller: "VenueMasterCtrl"
    });
	e.state("EligibilityStatusMasterAdd", {
        url: "/EligibilityStatusMaster/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EligibilityStatusMasterAdd.html",
        controller: "EligibilityStatusMasterCtrl"
    });

    e.state("EligibilityStatusMasterEdit", {
        url: "/EligibilityStatusMaster/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EligibilityStatusMasterEdit.html",
        controller: "EligibilityStatusMasterCtrl"
    });
	e.state("MstInstituteTypeAdd", {
        url: "/MstInstituteType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteTypeAdd.html",
        controller: "EligibilityStatusMasterCtrl"
    });

    e.state("MstInstituteTypeEdit", {
        url: "/MstInstituteType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteTypeEdit.html",
        controller: "MstInstituteTypeCtrl"
    });
	e.state("UserNotificationAdd", {
        url: "/UserNotification/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/UserNotificationAdd.html",
        controller: "UserNotificationCtrl"
    });

    e.state("UserNotificationEdit", {
        url: "/UserNotification/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/UserNotificationEdit.html",
        controller: "UserNotificationCtrl"
    });
    e.state("NotificationTypeAdd", {
        url: "/NotificationType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NotificationTypeAdd.html",
        controller: "NotificationTypeCtrl"
    });

    e.state("NotificationTypeEdit", {
        url: "/NotificationType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NotificationTypeEdit.html",
        controller: "NotificationTypeCtrl"
    });
	e.state("InstitutePartTermMapAdd", {
        url: "/InstitutePartTermMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstitutePartTermMapAdd.html",
        controller: "InstitutePartTermMapCtrl"
    });

    e.state("InstitutePartTermMapEdit", {
        url: "/InstitutePartTermMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstitutePartTermMapEdit.html",
        controller: "InstitutePartTermMapCtrl"
    });
	e.state("MstGroupTypeAdd", {
        url: "/MstGroupType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstGroupTypeAdd.html",
        controller: "MstGroupTypeCtrl"
    });

    e.state("MstGroupTypeEdit", {
        url: "/MstGroupType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstGroupTypeEdit.html",
        controller: "MstGroupTypeCtrl"
    });
	 e.state("InstancePreferenceCountAdd", {
        url: "/InstancePreferenceCount/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstancePreferenceCountAdd.html",
        controller: "InstancePreferenceCountCtrl"
    });

    e.state("InstancePreferenceCountEdit", {
        url: "/InstancePreferenceCount/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstancePreferenceCountEdit.html",
        controller: "InstancePreferenceCountCtrl"
    });
	e.state("MstPreRequisiteTypeAdd", {
        url: "/MstPreRequisiteType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreRequisiteTypeAdd.html",
        controller: "MstPreRequisiteTypeCtrl"
    });
    e.state("MstPreRequisiteTypeEdit", {
        url: "/MstPreRequisiteType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreRequisiteTypeEdit.html",
        controller: "MstPreRequisiteTypeCtrl"
    }); 
    e.state("MstPreRequisiteLableAdd", {
        url: "/MstPreRequisiteLable/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreRequisiteLableAdd.html",
        controller: "MstPreRequisiteLableCtrl"
    });
    e.state("MstPreRequisiteLableEdit", {
        url: "/MstPreRequisiteLable/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPreRequisiteLableEdit.html",
        controller: "MstPreRequisiteLableCtrl"
    });
	e.state("StudentAdmissionFeesPaidReport", {
        url: "/StudentAdmissionFeesPaidReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentAdmissionFeesPaidReport.html",
        controller: "StudentAdmissionFeesPaidReportCtrl"
    });
    e.state("StudentAdmissionFeesNotPaidReport", {
        url: "/StudentAdmissionFeesNotPaidReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentAdmissionFeesNotPaidReport.html",
        controller: "StudentAdmissionFeesPaidReportCtrl"
    });
    e.state("BlockApplicantsFromAdmission", {
        url: "/BlockApplicantsFromAdmission",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/BlockApplicantsFromAdmission.html",
        controller: "BlockApplicantsFromAdmissionCtrl"
    });
    e.state("AdmissionFeesReportByAcademic", {
        url: "/AdmissionFeesReportByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmissionFeesReportByAcademic.html",
        controller: "AdmissionFeesReportByAcademicCtrl"
    });
    e.state("AdmissionFeesReportByFaculty", {
        url: "/AdmissionFeesReportByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmissionFeesReportByFaculty.html",
        controller: "AdmissionFeesReportByFacultyCtrl"
    });
    e.state("CancelledByStudent", {
        url: "/CancelledByStudent",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CancelledByStudent.html",
        controller: "CancelledByStudentCtrl"
    });
    e.state("CancelledByFaculty", {
        url: "/CancelledByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CancelledByFaculty.html",
        controller: "CancelledByFacultyCtrl"
    });
    e.state("CancelledByAcademics", {
        url: "/CancelledByAcademics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CancelledByAcademics.html",
        controller: "CancelledByAcademicsCtrl"
    });
    e.state("AdmittedStudent", {
        url: "/AdmittedStudent",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmittedStudent.html",
        controller: "AdmittedStudentCtrl"
    });
	e.state("RefundStudentReportForFaculty", {
        url: "/RefundStudentReportForFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundStudentsReportForFaculty.html",
        controller: "RefundStudentReportForFacultyCtrl"
    });
    e.state("RefundStudentsReportForAcademics", {
        url: "/RefundStudentsReportForAcademics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundStudentsReportForAcademics.html",
        controller: "RefundStudentsReportForAcademicsCtrl"
    });
    e.state("PaperSelected", {
        url: "/PaperSelected",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperSelected.html",
        controller: "PaperSelectedCtrl"
    });
    e.state("PrnGenerated", {
        url: "/PrnGenerated",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PrnGenerated.html",
        controller: "PrnGeneratedCtrl"
    });
    e.state("ProgDefStatisticsFaculty", {
        url: "/ProgDefStatisticsFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgDefStatisticsFaculty.html",
        controller: "ProgDefStatisticsFacultyCtrl"
    });
    e.state("ProgDefStatisticsAcademics", {
        url: "/ProgDefStatisticsAcademics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgDefStatisticsAcademics.html",
        controller: "ProgDefStatisticsAcademicsCtrl"
    });
    e.state("ApprovedByFaculty", {
        url: "/ApprovedByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApprovedByFaculty.html",
        controller: "ApprovedByFacultyCtrl"
    });
    e.state("ApprovedByAcademic", {
        url: "/ApprovedByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApprovedByAcademic.html",
        controller: "ApprovedByAcademicCtrl"
    });
    e.state("NotApprovedByFaculty", {
        url: "/NotApprovedByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NotApprovedByFaculty.html",
        controller: "NotApprovedByFacultyCtrl"
    });
    e.state("NotApprovedByAcademic", {
        url: "/NotApprovedByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/NotApprovedByAcademic.html",
        controller: "NotApprovedByAcademicCtrl"
    });
    e.state("PendingApprovedByAcademic", {
        url: "/PendingApprovedByAcademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PendingApprovedByAcademic.html",
        controller: "PendingApprovedByAcademicCtrl"
    });
    e.state("PRNNotGenerated", {
        url: "/PRNNotGenerated",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PRNNotGenerated.html",
        controller: "PRNNotGeneratedCtrl"
    });
    e.state("MobileEmailData", {
        url: "/MobileEmailData",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MobileEmailData.html",
        controller: "MobileEmailDataCtrl"
    });
    e.state("PreVerificationVerifried", {
        url: "/PreVerificationVerifried",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreVerificationVerifried.html",
        controller: "PreVerificationVerifriedCtrl"
    });
    e.state("PreVerificationPending", {
        url: "/PreVerificationPending",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreVerificationPending.html",
        controller: "PreVerificationPendingCtrl"
    });
    e.state("PreVerificationNotApproved", {
        url: "/PreVerificationNotApproved",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreVerificationNotApproved.html",
        controller: "PreVerificationNotApprovedCtrl"
    });
    e.state("PreVerificationPendingVerified", {
        url: "/PreVerificationPendingVerified",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreVerificationPendingVerified.html",
        controller: "PreVerificationPendingVerifiedCtrl"
    });
    e.state("ProgDefStatusAcademics", {
        url: "/ProgDefStatusAcademics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgDefStatusAcademics.html",
        controller: "ProgDefStatusAcademicsCtrl"
    });
    e.state("PaperSelectionReport", {
        url: "/PaperSelectionReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperSelectionReport.html",
        controller: "PaperSelectionReportCtrl"
    });
	e.state("GetGAPReportFaculty", {
        url: "/GroupandpaperreportFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GetGAPReportFaculty.html",
        controller: "SRARFacCtrl"
    });
    e.state("StructureReportFaculty", {
        url: "/StructureReportFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StructureReportFaculty.html",
        controller: "SRARFacCtrl"
    });

    e.state("AssessmentReportFaculty", {
        url: "/AssessmentReportFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AssessmentReportFaculty.html",
        controller: "SRARFacCtrl"
    });
    e.state("RefundRequestByStudent", {
        url: "/RefundRequestByStudent",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundRequestByStudent.html",
        controller: "RefundRequestByStudentCtrl"
    });
    e.state("RefundRequestByFaculty", {
        url: "/RefundRequestByFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundRequestByFaculty.html",
        controller: "RefundRequestByFacultyCtrl"
    });
    e.state("RefundRequestByAcademics", {
        url: "/RefundRequestByAcademics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundRequestByAcademics.html",
        controller: "RefundRequestByAcademicsCtrl"
    });
    e.state("RefundRequestByAudit", {
        url: "/RefundRequestByAudit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundRequestByAudit.html",
        controller: "RefundRequestByAuditCtrl"
    });
    e.state("RefundRequestByAccount", {
        url: "/RefundRequestByAccount",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RefundRequestByAccount.html",
        controller: "RefundRequestByAccountCtrl"
    });
    e.state("ExamSlotMasterAdd", {
        url: "/ExamSlotMasterAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamSlotMasterAdd.html",
        controller: "ExamSlotMasterCtrl"
    });

    e.state("ExamSlotMasterEdit", {
        url: "/ExamSlotMasterEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamSlotMasterEdit.html",
        controller: "ExamSlotMasterCtrl"
    });
    e.state("PreExaminationStatistics", {
        url: "/PreExaminationStatistics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExaminationStatistics.html",
        controller: "PreExaminationStatisticsCtrl"
    });
    e.state("PreExamStatisticsforStudentList", {
        url: "/PreExamStatisticsStudentDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExamStatisticsforStudentList.html",
        controller: "PreExamStatisticsCtrl"
    });
    e.state("PreExaminationStatisticsGetPPT", {
        url: "/PreExaminationStatisticsGetPPT",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExaminationStatisticsGetPPT.html",
        controller: "PreExaminationStatisticsCtrl"
    });
    e.state("PaperWiseStudentCount", {
        url: "/PaperWiseStudentCount",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperWiseStudentCount.html",
        controller: "PaperWiseStudentCountCtrl"
    });
    e.state("PaperWiseStudentList", {
        url: "/PaperWiseStudentList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperWiseStudentList.html",
        controller: "PaperWiseStudentListCtrl"
    });
    e.state("OESRegistrationPending", {
        url: "/OESRegistrationPending",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/OESRegistrationPending.html",
        controller: "OESRegPendingCtrl"
    });
    e.state("PreExaminationStatisticsFaculty", {
        url: "/PreExaminationStatisticsFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExaminationStatisticsFaculty.html",
        controller: "PreExaminationStatisticsFacultyCtrl"
    });
    e.state("PreExaminationStatisticsFacultyGetPPT", {
        url: "/PreExaminationStatisticsFacultyGetPPT",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExaminationStatisticsFacultyGetPPT.html",
        controller: "PreExaminationStatisticsFacultyCtrl"
    });
    e.state("PreExamStatisticsforStudentListFaculty", {
        url: "/PreExamStatisticsforStudentListFaculty",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExamStatisticsforStudentListFaculty.html",
        controller: "PreExamStatisticsFacultyCtrl"
    });
    e.state("InstituteSubjectMapAdd", {
        url: "/InstituteSubjectMapAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteSubjectMapAdd.html",
        controller: "InstituteSubjectMapCtrl"
    });
    e.state("InstituteSubjectMapEdit", {
        url: "/InstituteSubjectMapEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteSubjectMapEdit.html",
        controller: "InstituteSubjectMapCtrl"
    });
    e.state("DatewiseCenterwisePaperReport", {
        url: "/DatewiseCenterwisePaperReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DatewiseCenterwisePaperReport.html",
        controller: "DatewiseCenterwisePaperReportCtrl"
    });

    e.state("DailyPaperReport", {
        url: "/DailyPaperReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DailyPaperReport.html",
        controller: "DailyPaperReportCtrl"
    });
    e.state("StudentWisePaperList", {
        url: "/StudentWisePaperList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentWisePaperList.html",
        controller: "StudentWisePaperListCtrl"
    });
    e.state("ManualFeesReport", {
        url: "/ManualFeesReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ManualFeesReport.html",
        controller: "ManualFeesReportCtrl"
    });
    e.state("StudentListByVenue", {
        url: "/StudentListByVenue",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentListByVenue.html",
        controller: "StudentListByVenueCtrl"
    });
    e.state("EligibiltyStatisticsReport", {
        url: "/EligibiltyStatisticsReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EligibiltyStatisticsReport.html",
        controller: "EligibiltyStatisticsReportCtrl"
    });
    e.state("ProvisionallyEligibleStudentsReport", {
        url: "/ProvisionallyEligibleStudentsReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProvisionallyEligibleStudentsReport.html",
        controller: "ProvisionallyEligibleStudentsReportCtrl"
    });
    e.state("EligibleStudentsReport", {
        url: "/EligibleStudentsReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EligibleStudentsReport.html",
        controller: "EligibleStudentsReportCtrl"
    });
    e.state("SeatingArragementReport", {
        url: "/SeatingArragementReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SeatingArragementReport.html",
        controller: "SeatingArragementReportCtrl"
    });
    e.state("GenderLocalOutsideCount", {
        url: "/GenderLocalOutsideCount",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GenderLocalOutsideCount.html",
        controller: "GenderLocalOutsideCountCtrl"
    });
    e.state("CopyCourseProgrammeDefination", {
        url: "/CopyCourseProgrammeDefination",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CopyCourseProgrammeDefination.html",
        controller: "CopyCourseProgrammeDefinationCtrl"
    });
    e.state("ReplacePaper", {
        url: "/ReplacePaper",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ReplacePaper.html",
        controller: "ReplacePaperCtrl"
    });
    e.state("SeatNumberWiseBlockChange", {
        url: "/SeatNumberWiseBlockChange",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SeatNumberWiseBlockChange.html",
        controller: "SeatNumberWiseBlockChangeCtrl"
    });
    e.state("ExamFormMasterListGenerated", {
        url: "/ExamFormMasterListGenerated",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormMasterListGenerated.html",
        controller: "ExamFormMasterListGeneratedCtrl"
    });
    e.state("PaperListGetByExamFormMaster", {
        url: "/PaperListGetByExamFormMaster",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperListGetByExamFormMaster.html",
        controller: "ExamFormMasterListGeneratedCtrl"
    });
    e.state("PreferenceWiseReport", {
        url: "/PreferenceWiseReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreferenceWiseReport.html",
        controller: "PreferenceWiseReportCtrl"
    });
    e.state("TransferCertificateReport", {
        url: "/TransferCertificateReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TransferCertificateReport.html",
        controller: "TransferCertificateReportCtrl"
    });
    //Nidhi State
    e.state("UfmAbsentStudentList", {
        url: "/UfmAbsentStudentList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/Ufm_Absent Report.html",
        controller: "UfmAbsentReportController"
    });
	//End Bhushan Project State
	
	



//Start Academic State - By krunal Shah
 
    /* Faculty */
    e.state("FacultyAdd", {
        url: "/Faculty/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstFacultyAdd.html",
        controller: "FacultyCtrl"
    });

    e.state("FacultyEdit", {
        url: "/Faculty/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstFacultyEdit.html",
        controller: "FacultyCtrl"
    });

    /* Department */
    e.state("MstDepartmentAdd", {
        url: "/Department/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DepartmentAdd.html",
        controller: "DepartmentCtrl"
    });

    e.state("MstDepartmentEdit", {
        url: "/Department/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DepartmentEdit.html",
        controller: "DepartmentCtrl"
    });

    /* Institute */
    e.state("MstInstituteAdd", {
        url: "/Institute/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteAdd.html",
        controller: "MstInstituteCtrl"
    });

    e.state("MstInstituteEdit", {
        url: "/Institute/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstInstituteEdit.html",
        controller: "MstInstituteCtrl"
    });

    /* Faculty Institute Map */
    e.state("FacultyInstituteMapAdd", {
        url: "/FacultyInstituteMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FacultyInstituteMapAdd.html",
        controller: "FacultyInstituteMapCtrl"
    });

    e.state("FacultyInstituteMapEdit", {
        url: "/FacultyInstituteMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FacultyInstituteMapEdit.html",
        controller: "FacultyInstituteMapCtrl"
    });

    /* Subject */
    e.state("SubjectAdd", {
        url: "/Subject/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstSubjectAdd.html",
        controller: "SubjectCtrl"
    });

    e.state("SubjectEdit", {
        url: "/Subject/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstSubjectEdit.html",
        controller: "SubjectCtrl"
    });

    /* Board Of Study */
    e.state("BOSAdd", {
        url: "/BoardofStudy/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstBOSAdd.html",
        controller: "BOSCtrl"
    });

    e.state("BOSEdit", {
        url: "/BoardofStudy/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstBOSEdit.html",
        controller: "BOSCtrl"
    });

    /* Board Of Study Subject Map */
    e.state("BOSSubjectMapAdd", {
        url: "/BoardofStudySubjectMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/BOSSubjectMapAdd.html",
        controller: "BOSSubjectMapCtrl"
    });

    e.state("BOSSubjectMapEdit", {
        url: "/BoardofStudySubjectMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/BOSSubjectMapEdit.html",
        controller: "BOSSubjectMapCtrl"
    });

    /* Paper */
    e.state("mstPaperAdd", {
        url: "/mstPaperAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPaperAdd.html",
        controller: "mstPaperCtrl"
    });

    e.state("mstPaperEdit", {
        url: "/mstPaperEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstPaperEdit.html",
        controller: "mstPaperCtrl"
    });

    /* Paper TLM Map */
    e.state("PaperTLMMapAdd", {
        url: "/papertlmmapadd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperTLMMapAdd.html",
        controller: "PaperTLMCtrl"
    });

    e.state("PaperTLMMapEdit", {
        url: "/papertlmmapedit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperTLMMapEdit.html",
        controller: "PaperTLMCtrl"
    });    

    /* Inc Programme Instance Paper Map*/
    e.state("ProgInstPaperMap", {
        url: "/ProgInstPaperMap",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IncProgInstancePartTermPaperMap.html",
        controller: "IncProgInstPartTermPaperMapCtrl"
    });

    /* Programme */
    e.state("ProgrammeAdd", {
        url: "/Programme/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeAdd.html",
        controller: "ProgrammeCtrl"
    });

    e.state("ProgrammeEdit", {
        url: "/Programme/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeEdit.html",
        controller: "ProgrammeCtrl"
    });

    /* Programme Part */
    e.state("ProgrammePartAdd", {
        url: "/ProgrammePart/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartAdd.html",
        controller: "ProgrammePart"
    });

    e.state("ProgrammePartEdit", {
        url: "/ProgrammePart/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartEdit.html",
        controller: "ProgrammePart"
    });

    /* Programme Part Term */
    e.state("ProgrammePartTermAdd", {
        url: "/ProgrammePartTerm/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartTermAdd.html",
        controller: "MstProgrammePartTermCtrl"
    });

    e.state("ProgrammePartTermEdit", {
        url: "/ProgrammePartTerm/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartTermEdit.html",
        controller: "MstProgrammePartTermCtrl"
    });

    /* Programme Branch Map */
    e.state("ProgrammeBranchMapAdd", {
        url: "/ProgrammeBranchMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeBranchMapAdd.html",
        controller: "ProgrammeBranchMapCtrl"
    });

    e.state("ProgrammeBranchMapEdit", {
        url: "/ProgrammeBranchMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeBranchMapEdit.html",
        controller: "ProgrammeBranchMapCtrl"
    });

    /* Specialisation */
    e.state("SpecialisationAdd", {
        url: "/Specialisation/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstSpecialisationAdd.html",
        controller: "MstSpecialisationCtrl"
    });

    e.state("SpecialisationEdit", {
        url: "/Specialisation/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstSpecialisationEdit.html",
        controller: "MstSpecialisationCtrl"
    });

    /* Sub-Specialisation */
    e.state("SubSpecialisationAdd", {
        url: "/SubSpecialisation/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SubSpecialisationAdd.html",
        controller: "SubSpecialisationCtrl"
    });

    e.state("SubSpecialisationEdit", {
        url: "/SubSpecialisation/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SubSpecialisationEdit.html",
        controller: "SubSpecialisationCtrl"
    });
	
	/* Inc Programme Instance Part Term Group*/
    e.state("ProgInstPartTermGroup", {
        url: "/ProgInstPartTermGroup",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IncProgInstancePartTermGroup.html",
        controller: "IncProgInstPartTermGroupCtrl"
    });

    /* Group Attachment*/
    e.state("IncProgInstGroupAttachment", {
        url: "/IncProgInstGroupAttachment",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IncProgInstPartTermGroupMap.html",
        controller: "IncProgInstPartTermGroupCtrl"
    });
   
    /* Academic Year */
    e.state("AcademicYearAdd", {
        url: "/AcademicYear/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AcademicYearAdd.html",
        controller: "AcademicYearCtrl"
    });

    e.state("AcademicYearEdit", {
        url: "/AcademicYear/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AcademicYearEdit.html",
        controller: "AcademicYearCtrl"
    });

    /* Programme Level */
    e.state("ProgrammeLevelAdd", {
        url: "/ProgrammeLevel/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeLevelAdd.html",
        controller: "ProgrammeLevelCtrl"
    });

    e.state("ProgrammeLevelEdit", {
        url: "/ProgrammeLevel/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeLevelEdit.html",
        controller: "ProgrammeLevelCtrl"
    });

    /* Programme Mode */
    e.state("ProgrammeModeAdd", {
        url: "/ProgrammeMode/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeModeAdd.html",
        controller: "ProgrammeModeCtrl"
    });

    e.state("ProgrammeModeEdit", {
        url: "/ProgrammeMode/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeModeEdit.html",
        controller: "ProgrammeModeCtrl"
    });

    /* Programme Type */
    e.state("ProgrammeTypeAdd", {
        url: "/ProgrammeType/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeTypeAdd.html",
        controller: "ProgrammeTypeCtrl"
    });

    e.state("ProgrammeTypeEdit", {
        url: "/ProgrammeType/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammeTypeEdit.html",
        controller: "ProgrammeTypeCtrl"
    });

    /* Evaluation */
    e.state("EvaluationAdd", {
        url: "/Evaluation/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EvaluationAdd.html",
        controller: "EvaluationCtrl"
    });

    e.state("EvaluationEdit", {
        url: "/Evaluation/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EvaluationEdit.html",
        controller: "EvaluationCtrl"
    });

    /* Examination Pattern */
    e.state("examinationPatternAdd", {
        url: "/examinationPatternAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExaminationPatternAdd.html",
        controller: "examinationPatternCtrl"
    });

    e.state("examinationPatternEdit", {
        url: "/examinationPatternEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExaminationPatternEdit.html",
        controller: "examinationPatternCtrl"
    });

    /* Instruction Medium */
    e.state("instructionMediumAdd", {
        url: "/InstructionMedium/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstructionMediumAdd.html",
        controller: "InstructionMediumCtrl"
    });

    e.state("instructionMediumEdit", {
        url: "/InstructionMedium/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstructionMediumEdit.html",
        controller: "InstructionMediumCtrl"
    });

    /* Teaching Learning Method */
    e.state("teachingLearningMethodAdd", {
        url: "/TeachingLearningMethod/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstTeachingLearningMethodAdd.html",
        controller: "MstTeachingLearningMethodCtrl"
    });

    e.state("teachingLearningMethodEdit", {
        url: "/TeachingLearningMethod/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstTeachingLearningMethodEdit.html",
        controller: "MstTeachingLearningMethodCtrl"
    });

    /* Assessment Method */
    e.state("assessmentMethodAdd", {
        url: "/AssessmentMethod/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstAssessmentMethodAdd.html",
        controller: "AssessmentMethodCtrl"
    });

    e.state("assessmentMethodEdit", {
        url: "/AssessmentMethod/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstAssessmentMethodEdit.html",
        controller: "AssessmentMethodCtrl"
    });

    /* Teaching Learning Assessment Method Map */
    e.state("TeachingLearningAssessmentMapAdd", {
        url: "/TeachingLearningAssessmentMap/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstTeachingLearningAssessmentMapAdd.html",
        controller: "TeachingLearningAssessmentMapCtrl"
    });

    e.state("TeachingLearningAssessmentMapEdit", {
        url: "/TeachingLearningAssessmentMap/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstTeachingLearningAssessmentMapEdit.html",
        controller: "TeachingLearningAssessmentMapCtrl"
    });    
	
	
	    e.state("ApplicationFeeReportForFaculty", {
        url: "/ApplicationFeeReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationFeeReportByAcadIdAndInstId.html",
        controller: "ApplicationFeeReportCtrl"
    });

    e.state("ApplicationFeeReportForAcademic", {
        url: "/ApplicationFeeReport1",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationFeeReportByAcadId.html",
        controller: "ApplicationFeeReportCtrl"
    });

    e.state("StudentTransferRequestReport", {
        url: "/StudentTransferRequestReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentTransferRequestReport.html",
        controller: "StudentTransferRequestReportCtrl"
    });
	
	e.state("InstGroupChangeRequest", {
        url: "/InstGroupChangeRequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteGroupChangeRequest.html",
        controller: "InstGroupChangeRequestCtrl"
    });
	
	e.state("InstGroupChangeRequestReport", {
        url: "/InstGroupChangeRequestReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteGroupChangeRequestReport.html",
        controller: "InstGroupChangeRequestCtrl"
    });
	
	e.state("VerifyStructureReport", {
        url: "/VerifyStructureReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerifyStructureReport.html",
        controller: "VerifyStructureReportCtrl"
    });
	
	e.state("VerifyStructureReportView", {
        url: "/VerifyStructureReportView",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerifyStructureReportView.html",
        controller: "VerifyStructureReportViewCtrl"
    });

	e.state("VerifyAssessmentReport", {
        url: "/VerifyAssessmentReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerifyAssessmentReport.html",
        controller: "VerifyAssessmentReportCtrl"
    });
	
	e.state("VerifyAssessmentReportView", {
        url: "/VerifyAssessmentReportView",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerifyAssessmentReportView.html",
        controller: "VerifyAssessmentReportViewCtrl"
    });

    e.state("LaunchProgPartTerm", {
        url: "/LaunchProgrammePartTerm",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/LaunchProgrammePartTerm.html",
        controller: "LaunchProgPartTermCtrl"
    });

    e.state("LaunchAssessmentReport", {
        url: "/LaunchAssessmentReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/LaunchAssessmentReport.html",
        controller: "LaunchProgPartTermCtrl"
    });

    e.state("StudentRequest", {
        url: "/StudentRequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentRequest.html",
        controller: "StudentRequestCtrl"
    });

    e.state("StudentRequestForAcad", {
        url: "/StudentRequestForAcad",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentRequestForAcad.html",
        controller: "StudentRequestCtrl"
    });
    
    e.state("ExamEventDateWiseReport", {
        url: "/ExamEventDateWiseReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamEventDateWiseReport.html",
        controller: "ExamEventDateWiseCtrl"
    });

    e.state("EventClosure", {
        url: "/EventClosure",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EventClosure.html",
        controller: "EventClosureCtrl"
    });

    e.state("PreExamDataReport", {
        url: "/PreExamDataReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreExamDataReport.html",
        controller: "PreExamDataReportCtrl"
    });
	
	e.state("HallTicket", {
        url: "/HallTicket",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/HallTicket.html",
        controller: "HallTicketCtrl"
    });

    e.state("GeneralRegisterReport", {
        url: "/GeneralRegisterReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GeneralRegister.html",
        controller: "GeneralRegisterCtrl"
    });

	e.state("ApplicationFormDocument", {
        url: "/ApplicationFormDocument",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationFormDocuments.html",
        controller: "ApplicationFormDocumentCtrl"
    });

    e.state("reconciliation", {
        url: "/reconciliation",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/Reconciliation.html",
        controller: "ReconciliationCtrl"
    });

    //End Academic State - By krunal Shah
	//Steffi Start State
    e.state("MstDocumentEdit", {
        url: "/MstDocument/Edit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstDocumentEdit.html",
        controller: "MstDocumentCtrl"
    });

	  e.state("MstDocumentAdd", {
        url: "/MstDocument/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstDocumentAdd.html",
        controller: "MstDocumentCtrl"
    });
	 e.state("dashboardacademic", {
        url: "/dashboardacademic",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AcademicDashboard.html",
        controller: "academicDashboardCtrl"
     });
    e.state("EmployeeRegistration", {
        url: "/EmployeeRegistration/Add",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EmployeeRegistration.html",
        controller: "EmployeeRegistrationCtrl"
    });
	
	 e.state("StudentQueryMasterFaculty", {
        url: "/StudentQueryPage/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentQueryMasterFaculty.html",
        controller: "StudentQueryMasterFacultyCtrl"
    });
  e.state("StudentQueryFacultyRemarksAdd", {
        url: "/StudentQueryFacultyRemarks/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentQueryFacultyRemarksAdd.html",
        controller: "StudentQueryMasterFacultyCtrl"
    });
	e.state("ApplicationInstitutePreferenceReport", {
        url: "/ApplicationInstitutePreferenceReport/Report",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationInstitutePreferenceReport.html",
        controller: "ApplicationInstitutePreferenceReportCtrl"
    });
    e.state("ApplicationGroupPreferenceReport", {
        url: "/ApplicationGroupPreferencReport/Report",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicationGroupPreferenceReport.html",
        controller: "ApplicationGroupPreferenceReportCtrl"
    });
	 e.state("AddOnInfo", {
        url: "/AddOnInfo/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AddOnInfoDetails.html",
        controller: "AddOnInfoReportCtrl"
    });
	 
	 e.state("MstCentralAdmissionEdit", {
        url: "/MstCentralAdmission/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstCentralAdmissionEdit.html",
        controller: "OfficeInterAffAcademicCtrl"
    });
    e.state("MstCentralAdmissionAdd", {
        url: "/MstCentralAdmission/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstCentralAdmissionAdd.html",
        controller: "OfficeInterAffAcademicCtrl"
    });
    e.state("MstCentralAdmissionFacultyAdd", {
        url: "/MstCentralAdmissionFacultyAdd/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstCentralAdmissionFacultyAdd.html",
        controller: "OfficeInterAffFacultyCtrl"
    });
    e.state("MstCentralAdmissionFacultyEdit", {
        url: "/MstCentralAdmissionFacultyEdit/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstCentralAdmissionFacultyEdit.html",
        controller: "OfficeInterAffFacultyCtrl"
    });
	 e.state("MstProgPartTermPaperGroup", {
        url: "/MstProgrammePartTermPaperMap/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstProgrammePartTermPaperMap.html",
        controller: "MstProgInstPartTermPaperMapCtrl"
    });
	
	 e.state("RequiredDocumentList", {
        url: "/RequiredDocument/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RequiredDocumentList.html",
        controller: "RequiredDocumentListCtrl"
    });
	 e.state("MapToPartTerm", {
        url: "/MapToPartTerm/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InstituteMapToPartTerm.html",
        controller: "InstituteMapToPartTermCtrl"
    });
	e.state("RequiredDocumentPendingList", {
        url: "/RequiredDocumentPending/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RequiredDocumentPendingList.html",
        controller: "RequiredDocumentPendingListCtrl"
    });
	e.state("AdmCancelApplicationForm", {
        url: "/AdmCancelApplicationForm/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmCancelApplicationForm.html",
        controller: "AdmCancelApplicationCtrl"
    });
    e.state("ManualEntryOfFee", {
        url: "/ManualEntryOfFee/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ManualEntryOfFee.html",
        controller: "ManualEntryOfFeeCtrl"
    });
	e.state("AdmissionFeesPaidReceiptList", {
        url: "/AdmissionFeesPaidReceiptList/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmissionFeesPaidReceiptList.html",
        controller: "ManualEntryOfFeeCtrl"
    });
    e.state("AdmissionFeesPaidReceipt", {
        url: "/AdmissionFeesPaidReceipt/",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmissionFeesPaidReceipt.html",
        controller: "ManualEntryOfFeeCtrl"
    });
	e.state("ApplicantsAdmissionInformationDetails", {
        url: "/ApplicantsAdmissionInformationDetails/",
        parent: "dashboard1",
		params: {
            PRN: 0
        },
        templateUrl: "UI/pages/admin/ApplicantsAdmissionInformationDetails.html",
        controller: "ApplicantsAdmissionInformationDetailsCtrl"
    });
	 e.state("FeesPaidReceipt", {
        url: "/FeesPaidReceipt",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AdmApplicationFeesPaid.html",
        controller: "ApplicantsAdmissionInformationDetailsCtrl"
    });
	  e.state("ApplicantAcademicPendingReport", {
        url: "/ApplicantAcademicPendingReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicantAcademicPendingReport.html",
        controller: "ApplicantAcademicPendingReportCtrl"
    });
	 e.state("HallticketInstructionConfiguration", {
        url: "/HallticketInstructionConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/HallticketInstructionConfiguration.html",
        controller: "HallticketInstructionConfigurationCtrl"
    });
	  e.state("AcademicWiseCategoryReport", {
        url: "/AcademicWiseCategoryReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AcademicWiseCategoryReport.html",
        controller: "FacultyWiseCategoryReportCtrl"
    });
    e.state("FacultyWiseCategoryReport", {
        url: "/FacultyWiseCategoryReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FacultyWiseCategoryReport.html",
        controller: "FacultyWiseCategoryReportCtrl"
    });
	  e.state("PaperWiseInwardReport", {
        url: "/PaperWiseInwardReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperWiseInwardReport.html",
        controller: "PaperWiseInwardReportCtrl"
    });
	 e.state("ExamScheduleReport", {
        url: "/ExamScheduleReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamScheduleReport.html",
        controller: "ExamScheduleReportCtrl"
    });
	 e.state("ExamFormDiscrepancyStatistics", {
        url: "/ExamFormDiscrepancyStatistics",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormDiscrepancyStatistics.html",
        controller: "ExamFormDiscrepancyStatisticsCtrl"
    });
	 e.state("ExamFeeConfiguration", {
        url: "/ExamFeeConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFeeConfig.html",
        controller: "ExamFeeConfigCtrl"
    });
    e.state("ExamFeePublish", {
        url: "/ExamFeePublish",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFeePublish.html",
        controller: "ExamFeePublishCtrl"
    });
	  e.state("StudentPaymentPendingReport", {
        url: "/StudentPaymentPendingReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentPaymentPendingReport.html",
        controller: "StudentPaymentPendingReportCtrl"
    });
	 e.state("CenterWiseBlockReport", {
        url: "/CenterWiseBlockReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CenterWiseBlockReport.html",
        controller: "CenterWiseBlockReportCtrl"
    });
	e.state("ProgrammeWisePaperTLMDetails", {
        url: "/ProgrammeWisePaperTLMDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeWisePaperTLMDetails.html",
        controller: "ProgrammeWisePaperTLMDetailsCtrl"
    });
    e.state("ProgrammeWiseMarksCreditDetails", {
        url: "/ProgrammeWiseMarksCreditDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProgrammeWiseMarksCreditDetails.html",
        controller: "ProgrammeWiseMarksCreditDetailsCtrl"
    });
	  e.state("RequiredDocumentsSubmittedByApplicant", {
        url: "/RequiredDocumentsSubmittedByApplicant",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/RequiredDocumentsSubmittedByApplicant.html",
        controller: "RequiredDocumentsSubmittedByApplicantCtrl"
    });
	 e.state("WrittenTestHallTicketConfiguration", {
        url: "/WrittenTestHallTicketConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/WrittenTestHallTicketConfiguration.html",
        controller: "WrittenTestHallTicketConfigCtrl"
    });
	 e.state("StudentBlockChange", {
        url: "/StudentBlockChange",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PaperWiseStudentBlockChange.html",
        controller: "StudentBlockChangeCtrl"
    });
	  e.state("IncStudentReAdmissionRequest", {
        url: "/StudentReAdmissionRequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/IncStudentReAdmissionRequest.html",
        controller: "IncStudentReAdmissionRequestCtrl"
    });
	 e.state("ProvisionalResult", {
        url: "/ProvisionalResult",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ProvisionalResult.html",
        controller: "ProvisionalResultController"
    });
    e.state("InDirectGradeProvisionalResult", {
        url: "/InDirectGradeProvisionalResult",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InDirectGradeProvisionalResult.html",
        controller: "ProvisionalResultController"
    });
    e.state("DirectGradeProvisionalResult", {
        url: "/DirectGradeProvisionalResult",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DirectProvisionalResult.html",
        controller: "ProvisionalResultController"
    });
	 e.state("StudentReAdmissionReport", {
        url: "/StudentReAdmissionReport",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentReAdmissionReport.html",
        controller: "StudentReAdmissionReportController"
    });

	//Steffi End State
	
	  //Start Author : Harsh Mistry
e.state("VerifiedApplicant", {
        url: "/VerifiedApplicant",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/VerifiedApplicant.html",
        controller: "VerifiedApplicantCtrl"
    });

    e.state("ApplicantInformationSearch", {
        url: "/ApplicantInformationSearch",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ApplicantInformationGet.html",
        controller: "ApplicantInformationGetCtrl"
    });
    e.state("DepartmentReports", {
        url: "/Department/Reports",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DepartmentReports.html",
        controller: "DepartmentReportsCtrl"
    });
    //End Author : Harsh Mistry

    /* Start PreRequisite PaperList */
    e.state("PreRequisitePaperList", {
        url: "/PreRequisitePaperList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PreRequisitePaperList.html",
        controller: "IncProgInstPartTermGroupCtrl"
    });
    /* End PreRequisite PaperList */
    // Start State - Parth
    e.state("DeanSign", {
        url: "/DeanSign",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/DeanSign.html",
        controller: "DeanSignCtrl"
    });
    e.state("SecBarcode", {
        url: "/SecBarcode",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SecBarcode.html",
        controller: "SecBarcodeCtrl"
    });
    // End State - Parth


    //start  by cei - PreExamination

    // 1)
    e.state("examevent", {
        url: "/examevent",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamEvent.html",
        controller: "ExamEventCtrl"
    });
    // 2)
    e.state("schedulemaster", {
        url: "/schedulemaster",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ScheduleMaster.html",
        controller: "ScheduleMasterCtrl"
    });
    // 3)
    e.state("examfeeconfiguration", {
        url: "/examfeeconfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFeeConfiguration.html",
        controller: "ExamFeeConfigurationCtrl"
    });
    // 4)
    e.state("examcenter", {
        url: "/examcenter",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamCenter.html",
        controller: "ExamCenterCtrl"
    });

    // 5)
    e.state("examvenue", {
        url: "/examvenue",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamVenue.html",
        controller: "ExamVenueCtrl"
    });

    // 6)
    e.state("createtimetable", {
        url: "/createtimetable",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CreateTimeTable.html",
        controller: "CreateTimeTableCtrl"
    });

    // 7)
    e.state("attachvenue", {
        url: "/attachvenue",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AttachVenue.html",
        controller: "AttachVenueCtrl"
    });

    //e.state("examconfigstatus", {
    //    url: "/examconfigstatus",
    //    parent: "dashboard1",
    //    templateUrl: "UI/pages/admin/ExamConfigStatus.html",
    //    controller: "ExamConfigStatusCtrl"
    //});


    // 8)
    e.state("schedulestatus", {
        url: "/schedulestatus",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ScheduleStatus.html",
        controller: "ScheduleStatusCtrl"
    });

    // 9)
    e.state("examfeehead", {
        url: "/examfeehead",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFeeHead.html",
        controller: "ExamFeeHeadCtrl"
    });

    // 10)
    e.state("examformgeneration", {
        url: "/examformgeneration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormGeneration.html",
        controller: "ExamFormGenerationCtrl"
    });

    // 11)
    e.state("inward", {
        url: "/inward",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/InWard.html",
        controller: "InWardCtrl"
    });

    // 12)
    e.state("seatnogeneration", {
        url: "/seatnogeneration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/SeatNoGeneration.html",
        controller: "SeatNoGenerationCtrl"
    });


    //13
    e.state("copyexamfeeconfiguration", {
        url: "/copyexamfeeconfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/CopyExamFeeConfiguration.html",
        controller: "CopyExamFeeConfigurationCtrl"
    });

    //14 - By Jaybhai - 08Jan2022
    e.state("PublishTimeTable", {
        url: "/PublishTimeTable",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TimeTablePublish.html",
        controller: "CreateTimeTableCtrl"
        
    });
    
    //15
    e.state("prnstatus", {
        url: "/prnstatus",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/PRNStatus.html",
        controller: "PRNStatusCtrl"
    });
	
	e.state("blankmarksheet", {
        url: "/blankmarksheet",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/BlankMarksheet.html",
        controller: "BlankMarksheetCtrl"
    });
    // 16
    e.state("examblocks", {
        url: "/examblocks",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamBlocks.html",
        controller: "ExamBlocksCtrl"
    });

    // 16
    e.state("assignblocks", {
        url: "/assignblocks",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/AssignBlocks.html",
        controller: "AssignBlocksCtrl"
    });

	// 17
    e.state("examformbarcodegeneration", {
        url: "/examformbarcodegeneration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormBarcodeGeneration.html",
        controller: "ExamFormBarcodeGenerationCtrl"
    });

    // 18
    e.state("examformbarcodepdfgenerationrequest", {
        url: "/examformbarcodepdfgenerationrequest",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/ExamFormBarcodePdfGenerationRequest.html",
        controller: "ExamFormBarcodePdfGenerationRequestCtrl"
    });

    // 19
    e.state("meritlistinstance", {
        url: "/meritlistinstance",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MeritListInstance.html",
        controller: "MeritListInstanceCtrl"
    });
    //end  by cei - PreExamination
	
	e.state("TransferCertificate", {
        url: "/TransferCertificate",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TransferCertificate.html",
        controller: "TransferCertiController"
    });

    e.state("VerifyTransferCertificate", {
        url: "/VerifyTransferCertificate",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/TransferCertificateVerification.html",
        controller: "TransferCertiController"
    });
	  e.state("GetStudentTransferDetails", {
        url: "/GetStudentTransferDetails",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GetStudentTransferDetails.html",
        controller: "TransferCertiController"
      });

    e.state("EligibilityCriteriaConfiguration", {
        url: "/EligibilityCriteriaConfiguration",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/EligibilityCriteriaConfiguration.html",
        controller: "EligibilityCriteriaConfigurationController"
    });

    e.state("GetEligibleStudentList", {
        url: "/GetEligibleStudentList",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/GetEligibleStudentList.html",
        controller: "GetEligibleStudentListController"
    });
	
	 e.state("StudentPendingForFeeCategoryMapping", {
        url: "/StudentPendingForFeeCategoryMapping",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/StudentPendingForFeeCategoryMapping.html",
        controller: "StudentPendingForFeeCategoryMappingController"
    });
	
	e.state("MstChoiceTypeAdd", {
        url: "/MstChoiceTypeAdd",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstChoiceTypeAdd.html",
        controller: "MstChoiceTypeController"
    });

    e.state("MstChoiceTypeEdit", {
        url: "/MstChoiceTypeEdit",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/MstChoiceTypeEdit.html",
        controller: "MstChoiceTypeController"
    });

    e.state("FeeCategoryChangeAfterAdmission", {
        url: "/FeeCategoryChangeAfterAdmission",
        parent: "dashboard1",
        templateUrl: "UI/pages/admin/FeeCategoryChangeAfterAdmission.html",
        controller: "FeeCategoryChangeAfterAdmissionController"
    });

}])

app.filter('getById', function () {
    return function (input, id) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (input[i].id == id) {
                return input[i];
            }
        }
        return "none";
    }
});

