app.controller('PostSMSEmailConfigByFacultyCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
   
    $scope.PostProgInst = {};
    $scope.applicantTableSendSMS = [];
    $scope.new_list = [];
    $scope.AdmissionDateFlag = false;

    //$scope.PostProgInstPartTermTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.getProgInstList
    //});

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //Function for expand columns in row click
    //$scope.expand_row = function (id) {
    //    let element = document.getElementById('expand' + id).classList
    //    if (element.contains("collapse")) {
    //        document.getElementById("first_col" + id).innerHTML = "-"
    //        element.remove("collapse")
    //    } else {
    //        document.getElementById("first_col" + id).innerHTML = "+"
    //        element.add("collapse")
    //    }
    //}

    $scope.getEmailByKeyName = function (data) {
        return $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/GenericConfigurationGetByKeyName',
            data: { KeyName: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    $scope.resetProgInstPartTerm = function () {
        $scope.PostProgInst = {};
    };

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                $scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

                //if ($localStorage.BacktoPostPage.AcademicYearId != null) {

                //    $scope.getIncProgPartTermByFacIdList();
                //}

                //$scope.PostProgInst.AcademicYearId = $localStorage.BacktoPostPage.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {

        $scope.ProgPartTermByFacIdList = {};
        //var InstituteId = { InstituteId: $scope.Faculty.InstituteId };
        $scope.PostProgInst.InstituteId = $scope.Faculty.InstituteId;
        $scope.PostProgInst.AcademicYearId = $scope.PostProgInst.AcademicYearId;

        $http({
            method: 'POST',
            //url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFacultyId',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFacIdandYearId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
           
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartTermByFacIdList = {};
                    }
                }
                else {
                    $scope.ProgPartTermByFacIdList = response.obj;
                  
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get Admission Fee End Date for sending SMS and Email
    $scope.getAdmFeeEndDatebyIncPTID = function () {

        $scope.AdmissionDateFlag = false;
        $scope.AdmFeeDateFlag = false;
        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostGetAdmFeeEndDate',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.obj == 'No Record Found') {
                        $scope.ConfigureDates.AdmissionFeesStopDate = null;
                        alert("Please configure Admission Fee End Date.");
                        $scope.AdmissionDateFlag = true;
                        $scope.AdmFeeDateFlag = true;
                    }
                    else {
                        $scope.AdmFeeDateFlag = false;
                    }

                } 
                else {
                    $scope.ConfigureDates = response.obj[0];

                    var admEndDate = $scope.ConfigureDates.AdmissionFeesStopDate.split("-");
                    $scope.ConfigureDates.AdmissionFeesStopDate = new Date(admEndDate[2], (admEndDate[1] >= 1) ? (admEndDate[1] - 1) : admEndDate[1], admEndDate[0]);
                    $scope.OldAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
                   
                    $scope.currentDate = new Date();
                    $scope.currentDate.setHours(0, 0, 0, 0);

                    if ($scope.OldAdmFeeDate < $scope.currentDate || $scope.OldAdmFeeDate == 'Invalid Date' || $scope.OldAdmFeeDate == "") {
                        alert("As Admission Fee End Date is not valid, So Configure first.");
                        $scope.AdmissionDateFlag = true;
                        $scope.AdmFeeDateFlag = true;

                    }
                    else if ($scope.OldAdmFeeDate == $scope.currentDate) {
                        $scope.AdmFeeDateFlag = false;
                    }
                    else {
                        $scope.AdmFeeDateFlag = false;
                    }
                    
                }
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for do validation on AdmFeeEndDate calender
    $scope.validationforDate = function () {

        $scope.currentDate = new Date();
        $scope.currentDate.setHours(0, 0, 0, 0);
      
        if ($scope.OldAdmFeeDate >= $scope.ConfigureDates.AdmissionFeesStopDate && $scope.currentDate <= $scope.ConfigureDates.AdmissionFeesStopDate) {
            $scope.AdmFeeDateFlag = false;
        }
        else {
            alert("Please select date between current date to existing date...!")
            $scope.AdmFeeDateFlag = true;
        }
    };

    //Function for get No. of Count for Sent SMS and Email
    $rootScope.GetApplicantsCountforSentEmailSMS = function () {

        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostApplicantsCountforSentEmailSMS',
            data: { ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.GetApplicantsCountforSentEmailSMS = response.obj[0];
                $scope.ModelSMSEmail = {};
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

    //Function for get Applicant list by Programme Instance PartTerm Id
    $scope.getApplicantListByProgPartTerm = function (InstPartTermId) {

        $scope.checkDataExists = false;
        $scope.AllSmsFlag = false;
        $scope.AllEmailFlag = false;

        $localStorage.InstancePartTermId = InstPartTermId;
        

        if ($scope.PostProgInst.ProgrammeInstancePartTermId == null || $scope.PostProgInst.ProgrammeInstancePartTermId == "" || $scope.PostProgInst.ProgrammeInstancePartTermId === undefined) {
            alert("Please select Programme...!");
        }
        else {
            $scope.onSpinner();

            $http({
                method: 'POST',
                url: 'api/PostConfigurationAdmission/PostApplicantListwithStatusEligibleAndFeeAttached',
                data: $scope.PostProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.PostProgInstPartTermTableparam = new NgTableParams({

                    }, { dataset: response.obj });
                    $scope.offSpinner();
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }
                    $scope.applicantTable = response.obj;
                    for (let i = 0; i < response.obj.length; i++) {
                        if ($scope.applicantTable[i].IsVerificationSms == false) {
                            $scope.AllSmsFlag = true;
                        }
                        if ($scope.applicantTable[i].IsVerificationEmail == false) {
                            $scope.AllEmailFlag = true;
                        }
                        //if ($scope.applicantTable[i].AdmissionFeesStopDate) {
                        //    $scope.AdmissionFeesStopDate = $scope.applicantTable[i].AdmissionFeesStopDate;
                        //}  
                    }
                    //$scope.ModelDate = {};
                    //var admEndDate = $scope.AdmissionFeesStopDate.split("-");
                    //$scope.ModelDate.AdmissionFeesStopDate = new Date(admEndDate[2], (admEndDate[1] >= 1) ? (admEndDate[1] - 1) : admEndDate[1], admEndDate[0]);
                    //$scope.OldAdmFeeDate = $scope.ModelDate.AdmissionFeesStopDate;

                    //Start - Use for converting date as dd-mm-yyyy from Universal time format
                    $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
                    let date = new Date($scope.NewAdmFeeDate);
                    let dd = date.getDate();
                    let mm = date.getMonth() + 1;
                    let yyyy = date.getFullYear();
                    $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
                    //End - Use for converting date as dd-mm-yyyy from Universal time format

                    $scope.AdmissionDateFlag = true;
                    $rootScope.GetApplicantsCountforSentEmailSMS();
   
                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Function for send Verification SMS to Applicant
    $scope.SendSMStoApplicant = function (data) {
      
        if ($scope.FinalAdmFeeDate == undefined) {
            $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
            let date = new Date($scope.NewAdmFeeDate);

            let dd = date.getDate();
            let mm = date.getMonth() + 1;
            let yyyy = date.getFullYear();
            $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
        }
        for (var i in $scope.applicantTable) {
            $scope.applicantTable[i].FinalAdmFeeDate = $scope.FinalAdmFeeDate;
          
        }

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/SendVerificationSMStoApplicant',
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
                    $scope.getApplicantListByProgPartTerm();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for send bulk Verification SMS to Applicant
    $scope.SendBulkSMStoApplicant = function (data) {
        
        if ($scope.FinalAdmFeeDate == undefined) {
            $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
            let date = new Date($scope.NewAdmFeeDate);

            let dd = date.getDate();
            let mm = date.getMonth() + 1;
            let yyyy = date.getFullYear();
            $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
        }
       

        for (var i in $scope.applicantTable) {
            $scope.applicantTable[i].FinalAdmFeeDate = $scope.FinalAdmFeeDate;
        }
        $scope.applicantTableSendSMS = $scope.applicantTable;
        
        var list2 = JSON.parse(JSON.stringify($scope.applicantTableSendSMS));
        for (i in list2) {
          
            delete list2[i].AcademicYearCode;
            delete list2[i].AdmFeePaidCount;
            delete list2[i].AdminRemarkByAcademics;
            delete list2[i].AdminRemarkByFaculty;
            delete list2[i].AdmissionFeesStopDate;
            delete list2[i].ApplicantRegistrationId;
            delete list2[i].ApplicationReservationCode;
            delete list2[i].ApprovedByAcademics;
            delete list2[i].ApprovedByFaculty;
            delete list2[i].BranchName;
            delete list2[i].EligibilityByAcademics;
            delete list2[i].EligibilityStatus;
            delete list2[i].EligibleCountByAcademic;
            delete list2[i].EligibleCountByFaculty;
            delete list2[i].EmailId;
            delete list2[i].EmailKeyName;
            delete list2[i].FacultyEligibilityStatus;
            delete list2[i].FacultyId;
            delete list2[i].FeeCategoryName;
            delete list2[i].FeeTypeCode;
            //delete list2[i].FinalAdmFeeDate;
            delete list2[i].FirstName;
            delete list2[i].FullName;
            //Id: 523611694671
            delete list2[i].IndexId;
            delete list2[i].InstancePartTermName;
            delete list2[i].InstituteId;
            delete list2[i].IsAdmissionFeePaid;
            delete list2[i].IsVerificationEmail;
            //IsVerificationSms: false
            delete list2[i].IsVerificationEmailBy;
            delete list2[i].IsVerificationSmsBy;
            delete list2[i].LastName;
            delete list2[i].MiddleName;
            //MobileNo: "9999999999"
            delete list2[i].ProgrammeInstancePartTermId;
            delete list2[i].ProgrammeName;
            delete list2[i].SMSFailureCount;
            delete list2[i].SMSSuccessCount;
            delete list2[i].SelectedCheck;
            delete list2[i].SelectedCheckEmail;
            delete list2[i].StuEmailList;
            delete list2[i].TotalCount;

        }

        $scope.FinalSmsSendList = list2;

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/SendVerificationBulkSMStoApplicant',
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
                    $scope.getApplicantListByProgPartTerm();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for send Verification Email to Applicant
    $scope.SendEmailtoApplicant = function (data) {

        $scope.getEmailByKeyName('Fac_Verify_Stu_Email').then(function (response) {
            $scope.EmailKeyName = response.data.obj[0].Value;
            data.EmailKeyName = $scope.EmailKeyName;

            if ($scope.FinalAdmFeeDate == undefined) {
                $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
                let date = new Date($scope.NewAdmFeeDate);

                let dd = date.getDate();
                let mm = date.getMonth() + 1;
                let yyyy = date.getFullYear();
                $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
            }
            data.FinalAdmFeeDate = $scope.FinalAdmFeeDate;

            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PostConfigurationAdmission/SendVerificationEmailtoApplicant',
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
                        $scope.getApplicantListByProgPartTerm();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        })
    };

    //Function for send bulk Verification Email to Applicant
    $scope.SendBulkEmailtoApplicant = function (data) {

        $scope.getEmailByKeyName('Fac_Verify_Stu_Email').then(function (response) {
            $scope.EmailKeyName = response.data.obj[0].Value;
            //$scope.applicantTable.EmailKeyName = $scope.EmailKeyName;
            let StuEmail = [];

            //var i = 0;
            //for (var i = 0; i < 6; i++) {
            //    $scope.applicantTable[i].EmailKeyName = $scope.EmailKeyName;
            //    if ($scope.applicantTable[i].IsVerificationEmail == false) {
            //        StuEmail.push($scope.applicantTable[i].EmailId)
            //    }
            //}

            for (var i in $scope.applicantTable) {
                $scope.applicantTable[i].EmailKeyName = $scope.EmailKeyName;
                $scope.applicantTable[i].FinalAdmFeeDate = $scope.FinalAdmFeeDate;
                if ($scope.applicantTable[i].IsVerificationEmail == false) {
                    StuEmail.push($scope.applicantTable[i].EmailId)
                }
            }
            $scope.applicantTable.push({ "StuEmailList": StuEmail.join(",") });

         
            $scope.applicantTableSendEmail = $scope.applicantTable;

            var list2 = JSON.parse(JSON.stringify($scope.applicantTableSendEmail));
            for (i in list2) {

                //delete list2[i].AcademicYearCode;
                delete list2[i].AdmFeePaidCount;
                delete list2[i].AdminRemarkByAcademics;
                delete list2[i].AdminRemarkByFaculty;
                delete list2[i].AdmissionFeesStopDate;
                delete list2[i].ApplicantRegistrationId;
                delete list2[i].ApplicationReservationCode;
                delete list2[i].ApprovedByAcademics;
                delete list2[i].ApprovedByFaculty;
                //delete list2[i].BranchName;
                delete list2[i].EligibilityByAcademics;
                delete list2[i].EligibilityStatus;
                delete list2[i].EligibleCountByAcademic;
                delete list2[i].EligibleCountByFaculty;
                delete list2[i].EmailId;
                //delete list2[i].EmailKeyName;
                delete list2[i].FacultyEligibilityStatus;
                delete list2[i].FacultyId;
                delete list2[i].FeeCategoryName;
                delete list2[i].FeeTypeCode;
                //delete list2[i].FinalAdmFeeDate;
                delete list2[i].FirstName;
                delete list2[i].FullName;
                //delete list2[i].Id;
                delete list2[i].IndexId;
                delete list2[i].InstancePartTermName;
                delete list2[i].InstituteId;
                delete list2[i].IsAdmissionFeePaid;
                //delete list2[i].IsVerificationEmail;
                delete list2[i].IsVerificationSms;
                delete list2[i].IsVerificationEmailBy;
                delete list2[i].IsVerificationSmsBy;
                delete list2[i].LastName;
                delete list2[i].MiddleName;
                delete list2[i].MobileNo;
                delete list2[i].ProgrammeInstancePartTermId;
                //delete list2[i].ProgrammeName;
                delete list2[i].SMSFailureCount;
                delete list2[i].SMSSuccessCount;
                delete list2[i].SelectedCheck;
                delete list2[i].SelectedCheckEmail;
                //delete list2[i].StuEmailList;
                delete list2[i].TotalCount;

            }

            $scope.FinalEmailSendList = list2;

            $scope.onSpinner(); 

            $http({
                method: 'POST',
                url: 'api/PostConfigurationAdmission/SendVerificationBulkEmailtoApplicant',
                data: $scope.FinalEmailSendList,
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
                        $scope.getApplicantListByProgPartTerm();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        })
    };


    //Function for send Verification SMS and Email to Applicant
    $scope.SendSMSEmailtoApplicant = function (data) {

        $scope.getEmailByKeyName('Fac_Verify_Stu_Email').then(function (response) {
            $scope.EmailKeyName = response.data.obj[0].Value;
            //data.EmailKeyName = $scope.EmailKeyName;

            if ($scope.FinalAdmFeeDate == undefined) {
                $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
                let date = new Date($scope.NewAdmFeeDate);

                let dd = date.getDate();
                let mm = date.getMonth() + 1;
                let yyyy = date.getFullYear();
                $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
            }
            //data.FinalAdmFeeDate = $scope.FinalAdmFeeDate;

            for (var i in $scope.applicantTable) {
                $scope.applicantTable[i].EmailKeyName = $scope.EmailKeyName;
                $scope.applicantTable[i].FinalAdmFeeDate = $scope.FinalAdmFeeDate;
            }

            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PostConfigurationAdmission/SendVerificationSMSEmailtoApplicant',
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
                        $scope.getApplicantListByProgPartTerm();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        })
    };


    $scope.cancelPostProgInsPartTermList = function () {
        $scope.PostProgInst = {};
        $scope.showFormFlag = false;
    };

    //==================Code for select all SMS checkbox Start=========================

    // This property will be bound to checkbox in table header
    $scope.PostProgInst.allItemsSelected = false;

    $scope.selectEntity = function () {
        // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
        for (var i = 0; i < $scope.applicantTable.length; i++) {
            if (!$scope.applicantTable[i].SelectedCheck) {
                $scope.PostProgInst.allItemsSelected = false;
                return;
            }
        }

        //If not the check the "allItemsSelected" checkbox
        $scope.PostProgInst.allItemsSelected = true;
    };

    // This executes when checkbox in table header is checked
    $scope.selectAll = function () {
        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.applicantTable.length; i++) {
            $scope.applicantTable[i].SelectedCheck = $scope.PostProgInst.allItemsSelected;
        }
    };

     //==================Code for select SMS all checkbox End===========================





    //==================Code for select all Email checkbox Start=========================

    // This property will be bound to checkbox in table header
    $scope.PostProgInst.allItemsSelectedEmail = false;

    $scope.selectEntityEmail = function () {
        // If any entity is not checked, then uncheck the "allItemsSelectedEmail" checkbox
        for (var i = 0; i < $scope.applicantTable.length; i++) {
            if (!$scope.applicantTable[i].SelectedCheckEmail) {
                $scope.PostProgInst.allItemsSelectedEmail = false;
                return;
            }
        }

        //If not the check the "allItemsSelectedEmail" checkbox
        $scope.PostProgInst.allItemsSelectedEmail = true;
    };

    // This executes when checkbox in table header is checked
    $scope.selectAllEmail = function () {
        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.applicantTable.length; i++) {
            $scope.applicantTable[i].SelectedCheckEmail = $scope.PostProgInst.allItemsSelectedEmail;
        }
    };

     //==================Code for select Email all checkbox End=========================

});



