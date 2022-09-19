app.controller('VerifiedApplicantCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Verified Applicant";

    $scope.VerifiedApplicant = {};

    var favoriteCookie = $cookies.get('token');

    if (!favoriteCookie) {
        $state.go('login');
    }

    //Spinner ON
    $scope.onSpinner = function on() {

        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //$scope.expand_row1 = function (PRN) {
    //    let element = document.getElementById('expand' + PRN).classList
    //    if (element.contains("collapse")) {
    //        document.getElementById("first_col" + PRN).innerHTML = "-"
    //        element.remove("collapse")
    //    } else {
    //        document.getElementById("first_col" + PRN).innerHTML = "+"
    //        element.add("collapse")
    //    }
    //}

    //Expand additional data in table by + and -
    $scope.expand_row = function (username) {
        let element = document.getElementById('expand' + username).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + username).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + username).innerHTML = "+"
            element.add("collapse")
        }
    }

    //Get Faculty By Id
    /*$scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.VerifiedApplicant,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0]; 
                $scope.VerifiedApplicant.FacultyId = $scope.Faculty.Id;
                $scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };*/
    //Get Faculty List
    $scope.getFacultyById = function () {

        //$scope.Faculty = {};
        $scope.VerifiedApplicant.Id = {};
        $scope.ProgPartTermByFacIdList = {};

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj;

                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get InstanceProgrammePartTerm By Faculty Id
    /*$scope.getIncProgPartTermByFacIdList = function () {
        
        var InstituteId = { InstituteId: $scope.Faculty.InstituteId };
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFacultyId',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByFacIdList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };*/
    //Get Faculty List
    $scope.getIncProgPartTermByFacIdList = function () {

        $scope.ProgPartTermByFacIdList = {};

        var FacultyId = $scope.VerifiedApplicant.Id;
        var AcademicYearId = $scope.VerifiedApplicant.AcademicYearId;
        var MyData = { FacultyId, AcademicYearId };
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFaculty',
            data: MyData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByFacIdList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get Verified Applicant List By ProgrammeInstancePartTermId
    $scope.getVerifiedApplicantListByProgPartTerm = function (ProgrammeInstancePartTermId) {
        $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant = ProgrammeInstancePartTermId;
        //$scope.ProgrammeInstancePartTermIdOfVerifiedApplicant;
        // $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant = ProgrammeInstancePartTermId;
        //alert($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/VerifiedApplicantList',
            data: $scope.VerifiedApplicant,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.ApplicantByProgPartTermList = response.obj;
                //$scope.offSpinner();
                $scope.VerifiedApplicantListByInstPartTermTableParam = new NgTableParams({}, { dataset: response.obj });

                $scope.VerifiedApplicantListByProgPartTermLengthGet = response.obj.length;
                if (response.obj.length > 0) {
                    $scope.offSpinner();
                    //alert($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                    $scope.DataTableBlock = true;
                    var a = true;
                    angular.forEach(response.obj, function (IsAd) {
                        if (IsAd.IsAdmitted == false)
                            a = false;
                    });
                    if (a == false) {
                        //$scope.offSpinner();
                        $scope.TransferAll = true;
                    }
                    else {
                        //$scope.offSpinner();
                        $scope.TransferAll = false;

                    }
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                else {
                    $scope.offSpinner();
                    /*if ($scope.VerifiedApplicantListByProgPartTermLengthGet = 0) {
                        alert('There are no data found.');
                    }*/
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                    if ($scope.TransferredStudentLengthGet == 0 && $scope.VerifiedApplicantListByProgPartTermLengthGet == 0) {
                        alert('There are no data found.');
                    }
                    $scope.DataTableBlock = false;
                    $scope.DataTableBlock2 = false;
                    $scope.TransferAll = false;
                }
                $scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                alert(res);
            });
        //$scope.offSpinner();
    };


    //Proceed To Make Single Applicant To Student
    $scope.ProceedToMakeSingleStudent = function (Username) {

        $scope.onSpinner();
        var UserName = Username;
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/ProceedToMakeSingleStudent',
            data: { UserName: UserName, ProgrammeInstancePartTermId: $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                //$scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    //$scope.Notification(response.obj); //Need to add 

                } else {
                    //alert(UserName+" transfered successfully as student.");
                    $scope.offSpinner();
                    $scope.Notification(UserName + ' transfer successfully as student');
                    $scope.getVerifiedApplicantListByProgPartTerm($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                //$scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);

            });
        //$scope.offSpinner();
    };


    //Transferred Student Get By ProgrammeInstancePartTermId
    $scope.TransferredStudentGetByProgrammeInstancePartTermId = function (programmeInstancePartTermId) {
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/TransferredStudentGetByProgrammeInstancePartTermId',
            data: { ProgrammeInstancePartTermId: programmeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //$scope.offSpinner();
                $scope.TransferredStudentGetByProgrammeInstancePartTermIdTableParam = new NgTableParams({}, { dataset: response.obj });
                $scope.TransferredStudentLengthGet = response.obj.length;

                if (response.obj.length > 0) {
                    $scope.EmailCount = response.obj, IsEmailSendEmailCount = response.obj.reduce((c, { IsEmailSend: key }) => (c[key] = (c[key] || 0) + 1, c), {});
                    $scope.SMSCount = response.obj, IsSMSSendSMSCount = response.obj.reduce((c, { IsSMSSend: key }) => (c[key] = (c[key] || 0) + 1, c), {});
                    //For Send SMS Count
                    if (IsSMSSendSMSCount.true == undefined || IsSMSSendSMSCount.true == null) {
                        $scope.IsSMSSendSMSCountSend = 0;
                    }
                    else {
                        $scope.IsSMSSendSMSCountSend = IsSMSSendSMSCount.true;
                    }
                    //For Pending SMS Count
                    if (IsSMSSendSMSCount.false == undefined || IsSMSSendSMSCount.false == null) {
                        $scope.IsSMSSendSMSCountPending = 0;
                    }
                    else {
                        $scope.IsSMSSendSMSCountPending = IsSMSSendSMSCount.false;
                    }

                    //For Send Email Count
                    if (IsEmailSendEmailCount.true == undefined || IsEmailSendEmailCount.true == null) {
                        $scope.IsEmailSendEmailCountSend = 0;
                    }
                    else {
                        $scope.IsEmailSendEmailCountSend = IsEmailSendEmailCount.true;
                    }
                    //For Pending Email Count
                    if (IsEmailSendEmailCount.false == undefined || IsEmailSendEmailCount.false == null) {
                        $scope.IsEmailSendEmailCountPending = 0
                    }
                    else {
                        $scope.IsEmailSendEmailCountPending = IsEmailSendEmailCount.false;
                    }

                    if (IsEmailSendEmailCount.false > 0) {
                        $scope.BulkMail = true;
                    }
                    else {
                        $scope.BulkMail = false;
                    }
                    if (IsSMSSendSMSCount.false > 0) {
                        $scope.BulkSMS = true;
                    }
                    else {
                        $scope.BulkSMS = false;
                    }
                    //$scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                    $scope.offSpinner();
                    $scope.DataTableBlock2 = true;

                }
                else {
                    $scope.offSpinner();
                    $scope.DataTableBlock2 = false;
                }

            })
            .error(function (res) {
                $scope.offSpinner();
                alert(res);
            });
        //$scope.offSpinner();
    };

    //Send Email To Single Transferred Student
    $scope.SendEmailToSingleTransferredStudent = function (Prn, Fullname, Email) {

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/SendEmailToSingleTransferredStudent',
            data: { PRN: Prn, FullName: Fullname, EmailId: Email },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                //$scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.Notification(response.obj);
                } else {
                    $scope.offSpinner();
                    $scope.Notification('Email sent successfully to ' + Email);

                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                //$scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);
            });
    };

    //Send Email To All Transferred Student
    $scope.SendEmailToAllTransferredStudent = function (ProgInstPartTermId) {

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/SendEmailToAllTransferredStudent',
            data: { ProgrammeInstancePartTermId: $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                //$scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                }
                else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.Notification(response.obj);
                }
                else {
                    $scope.offSpinner();
                    $scope.Notification('Email sent successfully to all');
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                $scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);
            });
        //$scope.offSpinner();
    };

    //Send SMS To Single Transferred Student
    $scope.SendSMSToSingleTransferredStudent = function (Prn, Mobile) {

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/SingleTransferredStudentForSMSSend',
            data: { PRN: Prn, MobileNo: Mobile },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                //$scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.Notification(response.obj);
                } else {
                    $scope.offSpinner();
                    $scope.Notification('SMS sent successfully to ' + Mobile);
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                $scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);
            });
        //$scope.offSpinner();
    };

    //Send Email To All Transferred Student
    $scope.SendSMSToAllTransferredStudent = function (ProgInstPartTermId) {
        //var PIPTid = ProgInstPartTermId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/SendSMSToAllTransferredStudent',
            data: { ProgrammeInstancePartTermId: ProgInstPartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                //$scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.Notification(response.obj);
                } else {
                    $scope.offSpinner();
                    $scope.Notification('SMS sent successfully to all');
                    $scope.TransferredStudentGetByProgrammeInstancePartTermId($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                $scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);
            });
        //$scope.offSpinner();
    };

    //Show Notification
    $scope.Notification = function (msg) {
        $.growl({
            message: msg
        }, {
            type: 'success',
            allow_dismiss: true,
            label: 'Cancel',
            className: 'btn btn-lg',
            placement: {
                from: 'top',
                align: 'right'
            },
            //delay: 3500,
            animate: {
                enter: 'animated zoomIn',
                exit: 'animated zoomOut'
            },
            offset: {
                x: 30,
                y: 30
            }
        });
    };

    //Proceed To Make All Applicant To Student
    $scope.ProceedToMakeAllStudent = function (ProgrammeInstancePartTermId) {
        $scope.onSpinner();
        $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant = ProgrammeInstancePartTermId;
        $http({
            method: 'POST',
            url: 'api/VerifiedApplicantForStudentProcess/ProceedToMakeAllStudent',
            data: { ProgrammeInstancePartTermId: $scope.ProgrammeInstancePartTermIdOfVerifiedApplicant },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                $scope.offSpinner();
                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.Notification(response.obj);
                } else {
                    //alert(UserName+" transfered successfully as student.");
                    $scope.offSpinner();
                    $scope.Notification('All applicant transfer successfully as student');
                    $scope.getVerifiedApplicantListByProgPartTerm($scope.ProgrammeInstancePartTermIdOfVerifiedApplicant);
                }
                $scope.offSpinner();
            })
            .error(function (res) {
                $scope.offSpinner();
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.Notification(res.obj);
            });
        //$scope.offSpinner();
    };


    //Start - Mohini's code Added on 18-May-2022

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.VerifiedApplicant,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //End - Mohini's code Added on 18-May-2022

});



