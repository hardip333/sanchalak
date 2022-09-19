app.controller('BlockApplicantsFromAdmissionCtrl', function ($scope, $http, $rootScope, $window, $state, $filter, $cookies, $mdDialog, $localStorage, NgTableParams) {


    $rootScope.pageTitle = "Block Applicants From Admission";

    var InstPartList = [];


    //$scope.AppCount = 0;
    $scope.newApplicationPreference = {};

    $scope.BlockApplicantsFromAdmission = {};

    $scope.InstPartList1 = {};

    $scope.BlockApplicantsFromAdmissionTableParams = {};

    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
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


    $scope.getBlockApplicantsFromAdmission = function () {
        //debugger;
        
        $scope.BlockApplicantsFromAdmissionTableParams = {};
        
        $http({
            method: 'POST',
            url: 'api/BlockApplicantsFromAdmission/GetApplicantListforBlockAdmission',
            data: {
                ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId,
                AdmittedInstituteId: $scope.BlockApplicantsFromAdmission.InstituteId,
                AcademicYearId: $scope.PostProgInst.AcademicYearId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.ApplicantByProgPartTermList = response.obj;

                if (response.obj == "No Record Found") {
                    alert("No Record Found");
                    $scope.BlockApplicantsFromAdmissionTableParams = new NgTableParams({

                    }, {


                        dataset: []

                    });

                }
                else {
                    $scope.BlockApplicantsFromAdmissionTableParams = {};
                    $scope.BlockApplicantsFromAdmissionTableParams = new NgTableParams({

                    }, {


                        dataset: response.obj

                    });
                };
                $scope.exportDataFull = response.obj;
                // $scope.InstPartList1 = response.obj;


                //Start - Use for converting date as dd-mm-yyyy from Universal time format
                $scope.NewAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
                let date = new Date($scope.NewAdmFeeDate);
                let dd = date.getDate();
                let mm = date.getMonth() + 1;
                let yyyy = date.getFullYear();
                $scope.FinalAdmFeeDate = (dd + "-" + mm + "-" + yyyy);
                //End - Use for converting date as dd-mm-yyyy from Universal time format

                $scope.BlockApplicantsFromAdmission.IsVerificationSmsOn = new Date($scope.BlockApplicantsFromAdmission.IsVerificationSmsOn);
                $scope.BlockApplicantsFromAdmission.IsVerificationSmsOn = date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear();
                console.log($scope.BlockApplicantsFromAdmission.IsVerificationSmsOn);



                $scope.AdmissionDateFlag = true;
                //$scope.getIncProgPartTermByFacIdList();
                //$scope.ShowBlockAdmission();
                //$scope.HideBlockAdmission();

            })
            .error(function (res) {
                alert(res);
            });




    };


    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/BlockApplicantsFromAdmission/MstFacultyGetbyId',
            data: $scope.BlockApplicantsFromAdmission,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                $scope.getAcademicList();
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

                

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {

        //var InstituteId = { InstituteId: $scope.Faculty.InstituteId, AcademicYearId: $scope.PostProgInst.AcademicYearId };
        $scope.PostProgInst.InstituteId = $scope.Faculty.InstituteId;
        $scope.PostProgInst.AcademicYearId = $scope.PostProgInst.AcademicYearId;
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFacIdandYearId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByFacIdList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstInstituteGetByProgInstPartTermId = function (AcademicYearId, ProgrammeInstancePartTermId) {
         //debugger;
        //$scope.BlockApplicantsFromAdmissionTableParams = {};
        //alert($scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId);
        $http({
            method: 'POST',
            url: 'api/BlockApplicantsFromAdmission/MstInstituteGetByProgInstPartTermId',
            data: {
                ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId,
                AcademicYearId: $scope.PostProgInst.AcademicYearId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MstInstList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
        $scope.BlockApplicantsFromAdmission.InstituteId = null;
    };

    //Function for get Admission Fee End Date for sending SMS and Email
    $scope.getAdmFeeEndDatebyIncPTID = function () {
        //debugger
        $scope.AdmissionDateFlag = false;
        $scope.AdmFeeDateFlag = false;
        //$scope.BlockApplicantsFromAdmissionTableParams.clear();
        $http({
            method: 'POST',
            url: 'api/BlockApplicantsFromAdmission/PostGetAdmFeeEndDate',
            data: { ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ConfigureDates = response.obj[0];

                var admEndDate = $scope.ConfigureDates.AdmissionFeesStopDate.split("-");
                $scope.ConfigureDates.AdmissionFeesStopDate = new Date(admEndDate[2], (admEndDate[1] >= 1) ? (admEndDate[1] - 1) : admEndDate[1], admEndDate[0]);
                $scope.OldAdmFeeDate = $scope.ConfigureDates.AdmissionFeesStopDate;
            })
            .error(function (res) {
                alert(res);
            });
        //$scope.ConfigureDates.AdmissionFeesStopDate = null;
    };


    //Function for do validation on AdmFeeEndDate calender
    $scope.validationforDate = function () {
        if ($scope.OldAdmFeeDate > $scope.ConfigureDates.AdmissionFeesStopDate) {
            alert("Please select proper date...!")
            $scope.AdmFeeDateFlag = true;
        }
        else {
            $scope.AdmFeeDateFlag = false;
        }
    };

    /*Active Enable BlockApplicantsFromAdmission*/
    $scope.ShowBlockAdmission = function (data) {
        // alert("Mona");
        //debugger;
        if ($scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId == 0) {
            $scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId = $localStorage.SelectedInstancePartTermId;
        }
        console.log("=======");
        console.log(data);

        $scope.BlockApplicantsFromAdmission = data;
        console.log("=======+++++++");
        console.log($scope.BlockApplicantsFromAdmission);
        if ($scope.BlockApplicantsFromAdmission.BlockedRemark === null || $scope.BlockApplicantsFromAdmission.BlockedRemark === undefined) {
            alert("Please Enter Reason to UnBlock");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/BlockApplicantsFromAdmission/BlockApplicantsFromAdmissionIsActive',
                data: $scope.BlockApplicantsFromAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getBlockApplicantsFromAdmission();


                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });



        }

    };


    /*Active Disable BlockApplicantsFromAdmission*/
    $scope.HideBlockAdmission = function (data) {
        //alert("Mona11====");
        // debugger;
        if ($scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId == 0) {
            $scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId = $localStorage.SelectedInstancePartTermId;
        }

        //$scope.BlockApplicantsFromAdmission = data;

        $scope.BlockApplicantsFromAdmission = data;
        if ($scope.BlockApplicantsFromAdmission.BlockedRemark === null || $scope.BlockApplicantsFromAdmission.BlockedRemark === undefined) {
            alert("Please Enter Reason to Block");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/BlockApplicantsFromAdmission/BlockApplicantsFromAdmissionIsSuspended',
                data: $scope.BlockApplicantsFromAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getBlockApplicantsFromAdmission();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };


    /*Edit Disable BlockApplicantsFromAdmission*/
    $scope.updateBlockApplicantsFromAdmission = function () {
        // debugger;
        //alert("PTID1st====" + $scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId);
        if ($scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId == 0) {
            $scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId = $localStorage.SelectedInstancePartTermId;

        }
        //alert("PTID2nd====" + $scope.BlockApplicantsFromAdmission.ProgrammeInstancePartTermId);


        $scope.BlockApplicantsFromAdmission.Data1 = $scope.InstPartList1;
        //$scope.BlockApplicantsFromAdmission = data;
        if ($scope.BlockApplicantsFromAdmission.BlockedRemark1 === null || $scope.BlockApplicantsFromAdmission.BlockedRemark1 === undefined) {
            alert("Please Enter Reason to Block");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/BlockApplicantsFromAdmission/BlockApplicantsFromAdmissionEdit',
                data: $scope.BlockApplicantsFromAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getBlockApplicantsFromAdmission();
                        $scope.InstPartList = $scope.InstPartList1;
                        $scope.BlockApplicantsFromAdmission.BlockedRemark1 = null;
                        //$scope.flagdisable = true;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }


    }
    //$scope.getApplicationInstitutePreference();

    $scope.exportData = function () {
        //debugger;
        if ($scope.exportDataFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "BlockApplicantsFromAdmission_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Block Applicants From Admission | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'Id', title: 'Application Id' },
                { columnid: 'AdmissionBlocked', title: 'Admission Blocked' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'ApplicantUserName', title: 'Applicant User Name.' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'IsVerificationEmailOns', title: 'Is Verification Email On' },
                { columnid: 'IsVerificationSmsOns', title: 'Is Verification Sms On' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };

});