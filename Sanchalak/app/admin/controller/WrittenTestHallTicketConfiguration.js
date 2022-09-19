﻿app.controller('WrittenTestHallTicketConfigCtrl', function ($scope, $http, Upload, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Entrance Test Hall Ticket Configuration";
    $scope.WTHTConfig = {};
    $scope.WTHTConfigFac = {};
    $scope.RadioALL = {};
    $scope.RadioVerified = {};
  

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.WTHTConfigFac,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                $scope.WTHTConfigFac.FacultyId = $scope.Faculty.Id;
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {
        $scope.AcadList = {};
        $http({
            method: 'POST',
            url: 'api/WrittenTestHallTicketConfiguration/AcademicYearGet',
            // data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.ProgrammeGetbyFacAcadId = function () {
        
        $scope.ProgrammeList = {};
        $scope.WTHTConfig = { FacultyId: $scope.WTHTConfigFac.FacultyId, AcademicYearId: $scope.WTHTConfig.AcademicYearId }
        
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/ProgrammeListGetByFacAcadId',
            data: $scope.WTHTConfig,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgrammeList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.PIPTGetByProgId = function () {
        $scope.PTList = {};
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/ProgrammeInstancePartTermGetByFAPId',
            data: $scope.WTHTConfig,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.PTList = response.obj;
              
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetVenue = function () {

        if ($scope.WTHTConfig.AcademicYearId === null || $scope.WTHTConfig.AcademicYearId === undefined || $scope.WTHTConfig.AcademicYearId === "" ||
            $scope.WTHTConfig.ProgrammeId === null || $scope.WTHTConfig.ProgrammeId === undefined || $scope.WTHTConfig.ProgrammeId === "" ||
            $scope.WTHTConfig.ProgrammeInstancePartTermId === null || $scope.WTHTConfig.ProgrammeInstancePartTermId === undefined || $scope.WTHTConfig.ProgrammeInstancePartTermId === ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select The Above DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.RadioVerified = {};
            $scope.RadioALL = {};
            $scope.NoRecLabelForVerifiedApplicant = false;
            $scope.IsVenueVisible = false;
            $scope.IsTableVisible = false;
            $scope.IsVisibleApplicantCount = false;

            $scope.GetStudentVenueDetailsbyPIPTId();



        }


    };

    $scope.UpdateAttachVenue = function () {

        var flag = true;
        var strExamDate = document.getElementById("txtExamDate").value;
        var strStartTime = document.getElementById("txtStartTime").value;
        var strEndTime = document.getElementById("txtEndTime").value;
       
        if ($scope.WTHTConfig.ExamDate === 'Invalid Date' || $scope.WTHTConfig.ExamDate === undefined || $scope.WTHTConfig.ExamDate === "" || $scope.WTHTConfig.ExamDate === null) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Exam Date")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            flag = false;
        }
        else if ($scope.WTHTConfig.DtStartTime === null || $scope.WTHTConfig.DtStartTime === undefined || $scope.WTHTConfig.DtStartTime === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Start Time")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            flag = false;
        }
        else if ($scope.WTHTConfig.DtEndTime === null || $scope.WTHTConfig.DtEndTime === undefined || $scope.WTHTConfig.DtEndTime === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select End Time")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            flag = false;
        }

        else if (strExamDate == "") {
            alert("Please Select Exam Date");
            flag = false;
        }

        else if (strStartTime == "") {
            alert("Please Select Start Time");
            flag = false;
        }


        else if (strEndTime == "") {
            alert("Please Select End Time");
            flag = false;
        }
        var startTime = new Date().setHours($scope.GetHours(strStartTime), $scope.GetMinutes(strStartTime), 0);
        var endTime = new Date(startTime)
        endTime = endTime.setHours($scope.GetHours(strEndTime), $scope.GetMinutes(strEndTime), 0);

        if (startTime > endTime) {
            alert("Start Time is Greater than End time");
            flag = false;
        }
        else if (startTime == endTime) {
            alert("Start Time equals End time");
            flag = false;
        }
        //if (startTime < endTime) {
        //    alert("Start Time is less than end time");
        //}
        if (flag == true) {

            $http({
                method: 'POST',
                url: 'api/WrittenTestHallTicketConfiguration/AttachVenueUpdate',
                data: $scope.WTHTConfig,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {

                        alert(response.obj);
                        $scope.NoRecLabel = false;
                        $scope.NoRecLabApp = true;
                        $scope.IsTableVisible = false;
                        $scope.IsVisibleRadioBtn = false;
                    }
                    else {
                        alert(response.obj);
                        $scope.GetStudentVenueDetailsbyPIPTId();
                        $scope.RadioVerified = {};
                        $scope.RadioALL = {};
                        $scope.NoRecLabel = false;
                        $scope.NoRecLabApp = false;
                        $scope.IsVisibleRadioBtn = true;
                        $scope.IsTableVisible = false;
                        $scope.IsVenueVisible = false;
                        $scope.IsVisibleApplicantCount = false;
                        $scope.NoRecLabelForVerifiedApplicant = false;





                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.GetPreVerifiedStudentVenueDetailsbyPIPTId = function (RadioVerified) {

        $scope.RadioALL = {};
        $scope.WTHTConfig.RadioALL = {};
        $scope.WTHTConfig.RadioVerifiedFlag = RadioVerified;
        $scope.checkDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;
        $scope.onSpinner();
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/StudentVenueListGetByPIPTId',
            data: {
                ProgrammeInstancePartTermId: $scope.WTHTConfig.ProgrammeInstancePartTermId,
                RadioVerifiedFlag: $scope.WTHTConfig.RadioVerifiedFlag,
                RadioALLFlag:null
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                  
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.NoRecLabelForVerifiedApplicant = true;
                    $scope.IsTableVisible = false;
                    $scope.AllSmsFlag = false;
                    $scope.IsVenueFormList = false;
                    $scope.IsVisibleApplicantCount = false;
                    $scope.IsVisibleRadioBtn = true;
                    $scope.IsVenueVisible = false;
                    $scope.NoRecLabel = false;
                    $scope.offSpinner();
                    

                }

                else {
                    
                    $scope.offSpinner();
                    
                    $scope.NoRecLabel = false;
                    $scope.NoRecLabApp = false;
                    $scope.StudentVenueDetailsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
                    

                  
                        $scope.applicantTable = response.obj;
                        

                        $scope.NoRecLabel = false;     
                        $scope.ImportExcelFile = true;
                        $scope.IsVisibleImportExcelBtn = true;
                        $scope.IsVenueFormList = true;
                        $scope.IsVisibleRadioBtn = true;
                        $scope.IsTableVisible = false;
                        $scope.IsVenueVisible = false;
                        $scope.IsVisibleApplicantCount = false;
                        $scope.NoRecLabelForVerifiedApplicant = false;
                        $scope.NoRecLabel = false;

                        for (let i = 0; i < response.obj.length; i++) {

                            if ($scope.applicantTable[i].IsEntranceTestSms == false) {
                                $scope.AllSmsFlag = true;
                            }


                            if ($scope.applicantTable[i].IsEntranceTestEmail == false) {
                                $scope.AllEmailFlag = true;
                            }
                                                                   

                        }

                        for (let i = 0; i < $scope.applicantTable.length; i++) {

                            if ($scope.applicantTable[i].VenueName == null || $scope.applicantTable[i].BlockName == null ||

                                $scope.applicantTable[i].Address == null || $scope.applicantTable[i].EntranceTestSeatNo == 0) {

                                $scope.IsTableVisible = false;
                                $scope.IsVisibleApplicantCount = false;
                            }
                            else {
                               
                                

                                $scope.IsTableVisible = true;
                                $scope.IsVisibleApplicantCount = true;
                               
                            }

                        }
                        for (let i = 0; i < $scope.applicantTable.length; i++) {

                            $scope.ApplicantVenueDetails = {};
                            $scope.ApplicantVenueDetails.InstancePartTermName = $scope.applicantTable[i].InstancePartTermName;
                            $scope.ApplicantVenueDetails.ExamDate = $scope.applicantTable[i].ExamDateView;
                            $scope.ApplicantVenueDetails.StartTimeView = $scope.applicantTable[i].StartTimeView;
                            $scope.ApplicantVenueDetails.EndTimeView = $scope.applicantTable[i].EndTimeView;


                        }
                    

                    if ($scope.RadioVerified == 'Verified_Applicant') {
                        $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.RadioVerified);
                    }


                   

                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    }

    $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel = function (RadioVerified) {
       
        $scope.RadioALL = {};
        $scope.WTHTConfig.RadioALL = {};
        $scope.WTHTConfig.RadioVerifiedFlag = RadioVerified;
        $scope.checkDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;
        $scope.onSpinner();
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/GetStudentVenueDetailsByPIPTIdAfterImportExcel',
            data: {
                ProgrammeInstancePartTermId: $scope.WTHTConfig.ProgrammeInstancePartTermId,
                RadioVerifiedFlag: $scope.WTHTConfig.RadioVerifiedFlag,
                RadioALLFlag: null
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.NoRecLabelForVerifiedApplicant = true;
                    $scope.IsTableVisible = false;
                    $scope.AllSmsFlag = false;
                    $scope.IsVenueFormList = false;
                    $scope.IsVisibleApplicantCount = true;
                    $scope.IsVisibleRadioBtn = true;
                    $scope.IsVenueVisible = false;
                    $scope.NoRecLabel = false;
                    $rootScope.GetApplicantsCountforSentEmailSMS();


                }

                else {

                    $scope.offSpinner();
                    $scope.NoRecLabel = false;
                    $scope.NoRecLabApp = false;
                    $scope.StudentVenueDetailsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })

                   
                        $scope.applicantTable = response.obj;


                        $scope.NoRecLabel = false;
                        $scope.ImportExcelFile = true;
                        $scope.IsVisibleImportExcelBtn = true;
                        $scope.IsVenueFormList = true;
                        $scope.IsVisibleRadioBtn = true;
                        $scope.IsTableVisible = true;
                        $scope.IsVenueVisible = true;
                        $scope.IsVisibleApplicantCount = true;
                        $scope.NoRecLabelForVerifiedApplicant = false;
                        $scope.NoRecLabel = false;

                        for (let i = 0; i < response.obj.length; i++) {

                            if ($scope.applicantTable[i].IsEntranceTestSms == false) {
                                $scope.AllSmsFlag = true;
                            }


                            if ($scope.applicantTable[i].IsEntranceTestEmail == false) {
                                $scope.AllEmailFlag = true;
                            }


                        }

                        //for (let i = 0; i < $scope.applicantTable.length; i++) {

                        //    if ($scope.applicantTable[i].VenueName == null || $scope.applicantTable[i].BlockName == null ||

                        //        $scope.applicantTable[i].Address == null || $scope.applicantTable[i].EntranceTestSeatNo == 0) {

                        //        $scope.IsTableVisible = false;
                        //        $scope.IsVisibleApplicantCount = false;
                        //    }
                        //    else {
                        //        $scope.IsTableVisible = true;
                        //        $scope.IsVisibleApplicantCount = true;
                        //    }

                        //}
                        for (let i = 0; i < $scope.applicantTable.length; i++) {

                            $scope.ApplicantVenueDetails = {};
                            $scope.ApplicantVenueDetails.InstancePartTermName = $scope.applicantTable[i].InstancePartTermName;
                            $scope.ApplicantVenueDetails.ExamDate = $scope.applicantTable[i].ExamDateView;
                            $scope.ApplicantVenueDetails.StartTimeView = $scope.applicantTable[i].StartTimeView;
                            $scope.ApplicantVenueDetails.EndTimeView = $scope.applicantTable[i].EndTimeView;


                        }

                    if ($scope.WTHTConfig.RadioVerifiedFlag == 'Verified_Applicant') {

                            $rootScope.GetApplicantsCountforSentEmailSMS();
                        }

                     


                   

                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    }

    $scope.GetAllStudentVenueDetailsbyPIPTId = function (RadioALL) {
   
        $scope.RadioVerified = {};
        $scope.WTHTConfig.RadioVerified = {};
        $scope.WTHTConfig.RadioALLFlag = RadioALL;
        $scope.checkDataExists = false;
        $scope.checkAllDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;
        $scope.onSpinner();
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/StudentVenueListGetByPIPTId',
            data: {
                ProgrammeInstancePartTermId: $scope.WTHTConfig.ProgrammeInstancePartTermId,
                RadioALLFlag: $scope.WTHTConfig.RadioALLFlag,
                RadioVerifiedFlag:null
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.NoRecLabel = true;
                    $scope.NoRecLabApp = true;
                    $scope.IsTableVisible = false;
                    $scope.checkDataExists = true;
                    $scope.checkAllDataExists = true;
                    $scope.IsTableVisible = false;
                    $scope.AllSmsFlag = false;
                    $scope.IsVenueFormList = false;
                    $scope.IsVisibleApplicantCount = false;
                    $scope.IsVenueVisible = false;
                    $scope.NoRecLabel = false;
                    $scope.IsVisibleRadioBtn = false;


                }

                else {

                    $scope.offSpinner();              
                    $scope.NoRecLabApp = false;
                    $scope.StudentVenueDetailsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
             
                       
                        $scope.applicantTable = response.obj;
                        $scope.ImportExcelFile = true;
                        $scope.IsVisibleImportExcelBtn = true;
                        $scope.IsVenueFormList = true;
                        $scope.IsVisibleRadioBtn = true;                     
                        $scope.IsTableVisible = false;
                        $scope.IsVenueVisible = false;
                        $scope.IsVisibleApplicantCount = false;
                        $scope.NoRecLabelForVerifiedApplicant = false;
                        
                        $scope.NoRecLabel = false;
                     

                        for (let i = 0; i < response.obj.length; i++) {
                          
                            if ($scope.applicantTable[i].IsEntranceTestSms == false) {
                                $scope.AllSmsFlag = true;
                            }


                            else if ($scope.applicantTable[i].IsEntranceTestEmail == false) {
                                $scope.AllEmailFlag = true;
                            }
                        
                        }
                        for (let i = 0; i < $scope.applicantTable.length; i++) {

                            if ($scope.applicantTable[i].VenueName == null || $scope.applicantTable[i].BlockName == null ||

                                $scope.applicantTable[i].Address == null || $scope.applicantTable[i].EntranceTestSeatNo == 0) {
                                
                                $scope.IsTableVisible = false;
                                $scope.IsVisibleApplicantCount = false;
                            }
                            else {
                               
                               

                                //if ($scope.RadioVerified == 'Verified_Applicant') {
                                //    $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.RadioVerified);
                                //}
                   
                                $scope.IsTableVisible = true;
                                $scope.IsVisibleApplicantCount = true;
                                
                           
                            
                        }

                        }
                        for (let i = 0; i < $scope.applicantTable.length; i++) {

                            $scope.ApplicantVenueDetails = {};
                            $scope.ApplicantVenueDetails.InstancePartTermName = $scope.applicantTable[i].InstancePartTermName;                  
                            $scope.ApplicantVenueDetails.ExamDate = $scope.applicantTable[i].ExamDateView;
                            $scope.ApplicantVenueDetails.StartTimeView = $scope.applicantTable[i].StartTimeView;
                            $scope.ApplicantVenueDetails.EndTimeView = $scope.applicantTable[i].EndTimeView;


                        }
                 
                      
                       if ($scope.RadioALL == 'All_Applicant') {
                                $scope.GetAllStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.RadioALL);
                            }

                   

                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };

    $scope.GetAllStudentVenueDetailsbyPIPTIdAfterImportExcel = function (RadioALL) {
        
        $scope.RadioVerified = {};
        $scope.WTHTConfig.RadioVerified = {};
        $scope.WTHTConfig.RadioALLFlag = RadioALL;
        $scope.checkDataExists = false;
        $scope.checkAllDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;
        $scope.onSpinner();
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/GetStudentVenueDetailsByPIPTIdAfterImportExcel',
            data: {
                ProgrammeInstancePartTermId: $scope.WTHTConfig.ProgrammeInstancePartTermId,
                RadioALLFlag: $scope.WTHTConfig.RadioALLFlag,
                RadioVerifiedFlag: null
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                }
                else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.NoRecLabel = true;
                    $scope.NoRecLabApp = true;
                    $scope.IsTableVisible = false;
                    $scope.checkDataExists = true;
                    $scope.checkAllDataExists = true;
                    $scope.IsTableVisible = false;
                    $scope.AllSmsFlag = false;
                    $scope.IsVenueFormList = false;
                    $scope.IsVisibleApplicantCount = true;
                    $scope.IsVenueVisible = false;
                    $scope.IsVisibleRadioBtn = false;
                    $rootScope.GetApplicantsCountforSentEmailSMS();


                }

                else {

                    $scope.offSpinner();
                    $scope.NoRecLabApp = false;
                    $scope.StudentVenueDetailsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })

                        $scope.applicantTable = response.obj;
                        $scope.ImportExcelFile = true;
                        $scope.IsVisibleImportExcelBtn = true;
                        $scope.IsVenueFormList = true;
                        $scope.IsVisibleRadioBtn = true;
                        $scope.IsTableVisible = true;
                        $scope.IsVenueVisible = true;
                        $scope.IsVisibleApplicantCount = true;
                        $scope.NoRecLabelForVerifiedApplicant = false;
                        $scope.NoRecLabel = false;


                    for (let i = 0; i < $scope.applicantTable.length; i++) {

                            if ($scope.applicantTable[i].IsEntranceTestSms == false) {
                                $scope.AllSmsFlag = true;
                            }


                            else if ($scope.applicantTable[i].IsEntranceTestEmail == false) {
                                $scope.AllEmailFlag = true;
                            }




                    }
                   
                    for (let i = 0; i < $scope.applicantTable.length; i++) {

                            $scope.ApplicantVenueDetails = {};
                            $scope.ApplicantVenueDetails.InstancePartTermName = $scope.applicantTable[i].InstancePartTermName;
                            $scope.ApplicantVenueDetails.ExamDate = $scope.applicantTable[i].ExamDateView;
                            $scope.ApplicantVenueDetails.StartTimeView = $scope.applicantTable[i].StartTimeView;
                            $scope.ApplicantVenueDetails.EndTimeView = $scope.applicantTable[i].EndTimeView;


                    }

                    for (let i = 0; i < $scope.applicantTable.length; i++) {

                            if ($scope.applicantTable[i].VenueName != null || $scope.applicantTable[i].BlockName != null ||

                                $scope.applicantTable[i].Address != null || $scope.applicantTable[i].EntranceTestSeatNo != 0) {
                               
                                $scope.IsTableVisible = true;
                                $scope.IsVisibleApplicantCount = true;
                               
                              
                            }
                            

                        }


                    if ($scope.WTHTConfig.RadioALLFlag == 'All_Applicant') {
                       
                        $rootScope.GetApplicantsCountforSentEmailSMS();
                    }
                     
                   
                    }

               

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };

    $scope.GetStudentVenueDetailsbyPIPTId = function () {
      
        $scope.checkDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;
        $scope.onSpinner();
        $http({
            method: 'Post',
            url: 'api/WrittenTestHallTicketConfiguration/StudentVenueListGetByPIPTId',
            data: $scope.WTHTConfig,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.checkDataExists = true;
                    $scope.IsTableVisible = false;
                    $scope.AllSmsFlag = false;
                    $scope.IsVenueFormList = false;
                    $scope.IsVisibleApplicantCount = false;
                    $scope.IsVenueVisible = false;
                    $scope.NoRecLabel = false;
                    $scope.IsVisibleRadioBtn = false;
                    $scope.IsVisibleImportExcelBtn = false;
                    $scope.ImportExcelFile = false;
                   

                }

                else {

                    $scope.offSpinner();
                    $scope.NoRecLabel = false;
                    $scope.NoRecLabApp = false;
                    $scope.StudentVenueDetailsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })

                    if (response.obj.length == 0) {
                       
                        $scope.checkDataExists = true;
                        $scope.IsTableVisible = false;
                        $scope.AllSmsFlag = false;
                        $scope.IsVenueFormList = false;
                        $scope.IsVisibleApplicantCount = false;
                        $scope.IsVenueVisible = false;
                        $scope.NoRecLabel = false;
                        $scope.IsVisibleRadioBtn = false;
                        $scope.IsVisibleImportExcelBtn = false;
                        $scope.ImportExcelFile = false;


                    }

                    else {
                        $scope.IsVenueFormList = true;
                        $scope.applicantTable = response.obj;
                       
                        for (let i = 0; i<$scope.applicantTable.length; i++) {
                           
                          
                            if ($scope.applicantTable[i].ExamDateView == "-" || $scope.applicantTable[i].StartTimeView == "-" || $scope.applicantTable[i].EndTimeView == "-") {
                               
                               $scope.IsTableVisible = false;
                               $scope.IsVisibleRadioBtn = false;
                               $scope.AllSmsFlag = false;
                               $scope.IsVisibleApplicantCount = false;
                               $scope.IsVenueVisible = false;
                               $scope.NoRecLabel = true;
                               $scope.IsVisibleImportExcelBtn = false;
                               $scope.ImportExcelFile = false;
                              
                       }
                          
                      

                        }

                        for (let i = 0; i < $scope.applicantTable.length; i++) {
                           
                            $scope.ApplicantVenueDetails = {};
                            $scope.ApplicantVenueDetails.InstancePartTermName = $scope.applicantTable[i].InstancePartTermName;
                            $scope.ApplicantVenueDetails.ExamDate = $scope.applicantTable[i].ExamDateView;
                            $scope.ApplicantVenueDetails.StartTimeView = $scope.applicantTable[i].StartTimeView;
                            $scope.ApplicantVenueDetails.EndTimeView = $scope.applicantTable[i].EndTimeView;


                        }
                        for (let i = 0; i < $scope.applicantTable.length; i++) {
                            
                            var ExamDate = $scope.applicantTable[i].ExamDateView.split("-");
                            var splitDateStartTime = $scope.applicantTable[i].StartTimeView.split(':');
                            var splitDateEndTime = $scope.applicantTable[i].EndTimeView.split(':');

                            $scope.WTHTConfig.DtStartTime = new Date(1970, 0, 1, splitDateStartTime[0], splitDateStartTime[1], splitDateStartTime[2]);
                            $scope.WTHTConfig.DtEndTime = new Date(1970, 0, 1, splitDateEndTime[0], splitDateEndTime[1], splitDateEndTime[2]);
                       
                            $scope.WTHTConfig.ExamDate = new Date(ExamDate[2], (ExamDate[1] >= 1) ? (ExamDate[1] - 1) : ExamDate[1], ExamDate[0]);
                          
                            $scope.WTHTConfig.VenueName = $scope.applicantTable[i].VenueName;
                           
                           

                        }

                        //$rootScope.GetApplicantsCountforSentEmailSMS();


                    }
                    
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };
 
    //Function for get No. of Count for Sent SMS and Email
    $rootScope.GetApplicantsCountforSentEmailSMS = function () {
       
        $http({
            method: 'POST',
            url: 'api/WrittenTestHallTicketConfiguration/EntranceTestCountforSentEmailSMS',
            data: {
                ProgrammeInstancePartTermId: $scope.WTHTConfig.ProgrammeInstancePartTermId,
                RadioALLFlag: $scope.WTHTConfig.RadioALLFlag,
                RadioVerifiedFlag: $scope.WTHTConfig.RadioVerifiedFlag
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                
                $scope.GetApplicantsCountforSentEmailSMS = response.obj[0];
               
                $scope.ModelSMSEmail = {};
                $scope.WTHTConfig.RadioALLFlag = null;
                $scope.WTHTConfig.RadioVerifiedFlag = null;
                $scope.ModelSMSEmail.TotalCount = $scope.GetApplicantsCountforSentEmailSMS.TotalCount;
                $scope.ModelSMSEmail.SMSSuccessCount = $scope.GetApplicantsCountforSentEmailSMS.SMSSuccessCount;
                $scope.ModelSMSEmail.SMSFailureCount = $scope.GetApplicantsCountforSentEmailSMS.SMSFailureCount;
                $scope.ModelSMSEmail.EmailSuccessCount = $scope.GetApplicantsCountforSentEmailSMS.EmailSuccessCount;
                $scope.ModelSMSEmail.EmailFailureCount = $scope.GetApplicantsCountforSentEmailSMS.EmailFailureCount;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for send Notification SMS and Email to Applicant
    $scope.SendSMSEmailtoStudent = function (data) {
   
        $scope.getEmailByKeyName('Fac_Verify_Stu_Email').then(function (response) {
         $scope.EmailKeyName = response.data.obj[0].Value;
            data.EmailKeyName = $scope.EmailKeyName;

            for (var i in $scope.applicantTable) {
               $scope.applicantTable[i].EmailKeyName = $scope.EmailKeyName;
              
            }
          
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/WrittenTestHallTicketConfiguration/SendVerificationSMSEmailtoApplicant',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $scope.offSpinner();
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.offSpinner();
                        alert(response.obj);
                       
                        //if ($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag == "All_Applicant") {
                        //    $scope.GetAllStudentVenueDetailsbyPIPTId($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag);
                        //}
                        //else if ($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag == "Verified_Applicant")
                        //{

                        //    $scope.GetPreVerifiedStudentVenueDetailsbyPIPTId($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag);
                        //}
                        if ($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag == "All_Applicant") {
                            $scope.GetAllStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag);
                        }
                        else if ($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag == "Verified_Applicant") {

                            $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag);
                        }
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        })


    };

    $scope.SendBulkSMStoApplicant = function (data) {

        $scope.applicantTableSendSMS = $scope.applicantTable;

        var list2 = JSON.parse(JSON.stringify($scope.applicantTableSendSMS));
        for (i in list2) {
            delete list2[i].EmailId;
            //delete list2[i].EmailKeyName;
            delete list2[i].AcademicYearId
            delete list2[i].IndexId;
            delete list2[i].InstancePartTermName;
            delete list2[i].ApplicantUserName;
            delete list2[i].NameAsPerMarksheet;
            //delete list2[i].MobileNo;
            delete list2[i].ExamDate;
            delete list2[i].FacultyId;
            delete list2[i].ExamDate;
            //delete list2[i].IsEntranceTestSms;
            delete list2[i].ProgrammeId;
            delete list2[i].ProgrammeInstancePartTermId;
            delete list2[i].StartTime;
            delete list2[i].EndTime;
            delete list2[i].DtEndTime;
            delete list2[i].DtStartTime;
            delete list2[i].StartTimeView;
            delete list2[i].EndTimeView;
            delete list2[i].NameAsPerMarksheet;
            delete list2[i].ExamDate;
            delete list2[i].EmailId;
            delete list2[i].EmailKeyName;
            //Id: 523611694671
            //IsVerificationSms: false
         
            //MobileNo: "9999999999"
           

        }

        $scope.FinalSmsSendList = list2;

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/WrittenTestHallTicketConfiguration/SendVerificationBulkSMStoApplicant',
            data: $scope.FinalSmsSendList,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.offSpinner();
                    alert(response.obj);
                    //if ($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag == "All_Applicant") {
                    //    $scope.GetAllStudentVenueDetailsbyPIPTId($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag);
                    //}
                    //else if ($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag == "Verified_Applicant") {

                    //    $scope.GetPreVerifiedStudentVenueDetailsbyPIPTId($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag);
                    //}
                     if ($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag == "All_Applicant") {
                         $scope.GetAllStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.GetApplicantsCountforSentEmailSMS.RadioALLFlag);
                    }
                    else if ($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag == "Verified_Applicant") {

                         $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.GetApplicantsCountforSentEmailSMS.RadioVerifiedFlag);
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.GetHours = function (d) {
        var h = parseInt(d.split(':')[0]);
        if (d.split(':')[1].split(' ')[1] == "PM") {
            h = h + 12;
        }
        return h;
    }

    $scope.GetMinutes = function (d) {
        return parseInt(d.split(':')[1].split(' ')[0]);
    }

    $scope.getEmailByKeyName = function (data) {
        return $http({
            method: 'POST',
            url: 'api/WrittenTestHallTicketConfiguration/GenericConfigurationGetByKeyName',
            data: { KeyName: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    $scope.cancelWrittenTestHallTicketConfiguration = function () {

        $scope.WTHTConfig = {};
        $scope.IsVenueFormList = false;
        $scope.IsVisibleRadioBtn = false;
        $scope.IsVisibleApplicantCount = false;
        $scope.IsVenueVisible = false;
        $scope.IsTableVisible = false;
        $scope.checkDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.NoRecLabel = false;
        $scope.ImportExcelFile = false;
        $scope.NoRecLabelForVerifiedApplicant = false;
        $scope.IsVisibleImportExcelBtn = false;
    }


    //Excel Students List For Entrance Test Report
    $scope.ExportStudentlistForAttachVenue = function () {

        alert("Please wait, Excel is being prepared...");
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Entrance_Test_Hall_Ticket_Instruction_for_Faculty" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Entrance_Test_Hall_Ticket_Instruction_for_Faculty(All_Applicants) on ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No' },
                { columnid: 'ApplicantUserName', title: 'PRN' },
                { columnid: 'AppId', title: 'ApplicationId' },
                { columnid: 'NameAsPerMarksheet', title: 'ApplicantName' },
                { columnid: 'MobileNo', title: 'MobileNo' },
                { columnid: 'EmailId', title: 'EmailId' },
                { columnid: 'VenueName', title: 'VenueName' },
                { columnid: 'Address', title: 'Address' },
                { columnid: 'BlockName', title: 'BlockName' },
                { columnid: 'EntranceTestSeatNo', title: 'EntranceTestSeatNo' },
               
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.applicantTable]);
    };


    /*Start - Import Code for Refund data in BOB */

    $scope.exceljson = {};

    //Read excel data to our database
    $scope.ReadStudentExcelFile = function () {
        $scope.onSpinner();
        var fileCheck = {};
        fileCheck = document.getElementById("ngexcelfile").value;
        var allowedExtensions = /(.xlsx|.xls)$/i;

        if (fileCheck == "" ||
            fileCheck === undefined) {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "Must upload the file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            $scope.offSpinner();
            return false;
        }
        else if (!allowedExtensions.exec(fileCheck)) {

            document.getElementById("ErrorMsgUploadFile").innerHTML = "It only accepts .xlx and .xlsx file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            fileCheck.value = '';
            $scope.offSpinner();
            return false;
        }

        else {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "";
            /*Checks whether the file is a valid excel file*/
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
            var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/

            if ($("#ngexcelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
                xlsxflag = true;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = {};
                data = e.target.result;

                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }

                var sheet_name_list = workbook.SheetNames;
                var cnt = 0;
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/

                    if (xlsxflag) {
                        $scope.exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    }
                    else {
                        $scope.exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }

                    if ($scope.exceljson.length > 0) {
                        $scope.data = [];
                        for (var i = 0; i < $scope.exceljson.length; i++) {

                            var customer_info = {
                                "ApplicationId": $scope.exceljson[i].ApplicationId,
                                "VenueName": $scope.exceljson[i].VenueName,
                                "Address": $scope.exceljson[i].Address,
                                "BlockName": $scope.exceljson[i].BlockName,
                                "EntranceTestSeatNo": $scope.exceljson[i].EntranceTestSeatNo,
                            };
                            $scope.data.push(customer_info);
                            $scope.$apply();
                           
                        }

                        
                    }
                });
                $scope.save($scope.data);
            }
            if (xlsxflag) {
                reader.readAsArrayBuffer($("#ngexcelfile")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#ngexcelfile")[0].files[0]);
            }

        }

    };

    //Save excel data to our database
    $scope.save = function (data) {
        var params = [];
        
       
        
        $scope.exceljson = data;
        for (var i = 0; i < $scope.exceljson.length; i++) {
           if ($scope.exceljson[i].ApplicationId === undefined || $scope.exceljson[i].ApplicationId === null || $scope.exceljson[i].ApplicationId === 'null') {

                $("#ngexcelfile").val("");
                document.getElementById("ErrorMsgUploadFile").innerHTML = "Upload Proper Excel File.Application Id Is Invalid!";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
               $scope.offSpinner();
               return false;
               
            }

            else if ($scope.exceljson[i].VenueName === undefined || $scope.exceljson[i].VenueName === null || $scope.exceljson[i].VenueName==='null' ) {
                $("#ngexcelfile").val("");
                document.getElementById("ErrorMsgUploadFile").innerHTML = "Upload Proper Excel File.Venue Name Is NULL!";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
               $scope.offSpinner();
               return false;
              
            }
            else if ($scope.exceljson[i].Address === undefined || $scope.exceljson[i].Address === null || $scope.exceljson[i].Address === 'null') {
                $("#ngexcelfile").val("");
                document.getElementById("ErrorMsgUploadFile").innerHTML = "Upload Proper Excel File.Address Is NULL!";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
               $scope.offSpinner();
               return false;
               
            }
            else if ($scope.exceljson[i].BlockName === undefined || $scope.exceljson[i].BlockName === null || $scope.exceljson[i].BlockName === 'null') {
                $("#ngexcelfile").val("");
                document.getElementById("ErrorMsgUploadFile").innerHTML = "Upload Proper Excel File.BlockName Is NULL!";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
               $scope.offSpinner();
                return false;
            }
            else if ($scope.exceljson[i].EntranceTestSeatNo === undefined || $scope.exceljson[i].EntranceTestSeatNo === null || $scope.exceljson[i].EntranceTestSeatNo === '0' || $scope.exceljson[i].EntranceTestSeatNo === 'null') {
                
                $("#ngexcelfile").val("");
                document.getElementById("ErrorMsgUploadFile").innerHTML = "Upload Proper Excel File.EntranceTestSeatNo Is NULL!";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
               $scope.offSpinner();
               return false;
               
            }
               
               

           
            else {
                var VenueDetails_info = {
                    "ApplicationId": $scope.exceljson[i].ApplicationId,
                    "VenueName": $scope.exceljson[i].VenueName,
                    "Address": $scope.exceljson[i].Address,
                    "BlockName": $scope.exceljson[i].BlockName,
                    "EntranceTestSeatNo": $scope.exceljson[i].EntranceTestSeatNo,
                };
                params.push(VenueDetails_info);

          }
              
           
        }

      
        $http({
            method: 'POST',
            url: 'api/WrittenTestHallTicketConfiguration/UpdateVenueDetailsFromExcelForWTHC',
            data: params,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert("Uploading Data in Excel File Failed"+":" + response.obj);
                   
                }


                else {
                    /* if (response.obj == 'null') {
                            $scope.msg = "Error : Something Wrong! Please Upload Your Excel In below format.";
                            alert("error");
                        }*/
                    $scope.offSpinner();
                    alert(response.obj);
                  
                    $("#ngexcelfile").val('');
                  
                  
                    if ($scope.RadioALL == 'All_Applicant') {
                        $scope.GetAllStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.RadioALL);
                    }
                    if ($scope.RadioVerified == 'Verified_Applicant') {
                        $scope.GetPreVerifiedStudentVenueDetailsbyPIPTIdAfterImportExcel($scope.RadioVerified);
                    }  
                   
                
                  
                  


                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


        //  }

    }

/*End - Import Coode for Store data into Database */



    
    



  

});