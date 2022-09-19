app.controller('PostSMSEmailConfigByAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
   
    $scope.PostProgInst = {};
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

    ////Function for expand columns in row click
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
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
             
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {

        var FacultyId = { FacultyId: $scope.PostProgInst.Id };
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFaculty',
            data: FacultyId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByFacIdList = response.obj;
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

                    if ($scope.OldAdmFeeDate <= $scope.currentDate || $scope.OldAdmFeeDate == 'Invalid Date' || $scope.OldAdmFeeDate == "") {
                        alert("As Admission Fee End Date is not valid, So Configure first.");
                        $scope.AdmissionDateFlag = true;
                        $scope.AdmFeeDateFlag = true;
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
        //if ($scope.OldAdmFeeDate > $scope.ConfigureDates.AdmissionFeesStopDate) {
        //    alert("Please select proper date...!")
        //    $scope.AdmFeeDateFlag = true;
        //}
        //else {
        //    $scope.AdmFeeDateFlag = false;
        //}

        $scope.currentDate = new Date();
        $scope.currentDate.setHours(0, 0, 0, 0);

        /*alert($scope.OldAdmFeeDate + "==========" + $scope.ConfigureDates.AdmissionFeesStopDate + "===" + $scope.currentDate);*/

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

        if ($scope.PostProgInst.Id == null || $scope.PostProgInst.Id == "" || $scope.PostProgInst.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else if ($scope.PostProgInst.ProgrammeInstancePartTermId == null || $scope.PostProgInst.ProgrammeInstancePartTermId == "" || $scope.PostProgInst.ProgrammeInstancePartTermId === undefined) {
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
                    $scope.PostApplicantListTableparam = new NgTableParams({

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
                    }

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
            data:data,
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

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/SendVerificationBulkSMStoApplicant',
            data: $scope.applicantTable,
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
            let StuEmail = [];

            for (var i in $scope.applicantTable) {
                $scope.applicantTable[i].EmailKeyName = $scope.EmailKeyName;
                $scope.applicantTable[i].FinalAdmFeeDate = $scope.FinalAdmFeeDate;
                if ($scope.applicantTable[i].IsVerificationEmail == false) {
                    StuEmail.push($scope.applicantTable[i].EmailId)
                }
            }
            $scope.applicantTable.push({ "StuEmailList": StuEmail.join(",") });

            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PostConfigurationAdmission/SendVerificationBulkEmailtoApplicant',
                data: $scope.applicantTable,
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

     //==================Code for select all SMS checkbox End=========================






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



