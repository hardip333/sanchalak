app.controller('StudentAdmissionFeesPaidReportCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.StudentAdmissionFeesPaidReport = {}

    $scope.TakeLocalStoradeValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getStudentAdmissionFeesPaidReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.StudentAdmissionFeesPaidReport = null;
        }
    };


    $scope.GetLocalStorageValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getStudentAdmissionFeesPaidReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.StudentAdmissionFeesPaidReport = null;
        }
    };

    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {

        $http({
            method: 'Post',
            url: 'api/StudentAdmissionFeesPaidReport/IncProgInsPartTermListGetByInstituteId',
            data: { InstituteId: $localStorage.Stats.InstituteId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.alGet = response.obj;
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetPartTermList = function () {

        if ($localStorage.Stats.InstituteId == null || $localStorage.Stats.InstituteId == undefined) { $scope.ApplicationListGet(); }
        else { $scope.getIncProgInsPartTermListByInstituteId(); }
    };


    // This method is for getting InstancePartTerm
    $scope.ApplicationListGet = function () {
        //alert("Institute");
        $http({
            method: 'GET',
            url: 'api/StudentAdmissionFeesPaidReport/ApplicationListGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.alGet = response.obj;
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };


    //Get Students Admission Fees Paid Report
    $scope.getStudentAdmissionFeesPaidReport = function () {

        // alert($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == "" ||
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == null ||
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/StudentAdmissionFeesPaidReport/StudentAdmissionFeesPaidReportGet',
            data: { ProgrammeInstancePartTermId: $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                alert("Please wait, Data is processing...");
                //$scope.onSpinner();

                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.StudentAdmissionFeesPaidReportTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });
                        $scope.exportDataFull = undefined;
                        //$scope.searchCaseResultFull = undefined;
                        //$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.StudentAdmissionFeesPaidReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFull = response.obj;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.backToList = function () {
        debugger;
        //alert($localStorage.InstId);
        if ($localStorage.InstId == null || $localStorage.InstId == undefined) {
            $state.go('ApplicationStatistics');

        }
        else {


            $rootScope.Checkls = true;
            $state.go('ApplicationStatisticsByInstitute');
        }



    };



    //Excel Students Admission Fees Paid Report
    $scope.exportData = function () {

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
        var ExcelFileName = "StudentAdmissionFeesPaidReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Student Admission Fees Paid Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationId', title: 'Application Id' },
                { columnid: 'FullName', title: 'Name' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'ApplicationReservationName', title: 'Programme Name' },
                { columnid: 'SocialCategoryName', title: 'Branch Name' },
                { columnid: 'CommitteeName', title: 'Institute Name' },
                { columnid: 'IsPhysicallyChanllenged', title: 'IsPhysicallyChanllenged' },
                { columnid: 'FacultyName', title: 'Faculty Name' },                
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'GroupName', title: 'Group Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'IsAdmissionFeePaid', title: 'Is Admission Fee Paid' },
                { columnid: 'IsInstalmentSelected', title: 'Is Instalment Selected' },
                { columnid: 'IsVerificationEmail', title: 'Is Verification Email' },
                { columnid: 'IsVerificationSms', title: 'Is Verification Sms' },
                { columnid: 'IsVerificationEmailOns', title: 'Is Verification Email On' },
                { columnid: 'IsVerificationSmsOns', title: 'Is Verification Sms On' },
                { columnid: 'TotalAmount', title: 'Total Amount' },
                { columnid: 'AmountPaid', title: 'Amount Paid' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };

    /* From Here This method is Students Admission Fees Not Paid Report */
    //Get Students Admission Fees Not Paid Report

    $scope.TakeLocalStoradeValueNotPaid = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getStudentAdmissionFeesNotPaidReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.StudentAdmissionFeesPaidReport = null;
        }
    };


    $scope.GetLocalStorageValueNotPaid = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getStudentAdmissionFeesNotPaidReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.StudentAdmissionFeesPaidReport = null;
        }
    };




    $scope.getStudentAdmissionFeesNotPaidReport = function () {

        //alert($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == "" ||
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == null ||
            $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/StudentAdmissionFeesPaidReport/StudentAdmissionFeesNotPaidReport',
            data: { ProgrammeInstancePartTermId: $scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                alert("Please wait, Data is processing...");
                //$scope.onSpinner();

                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.StudentAdmissionFeesNotPaidReportTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });
                        $scope.exportNotPaidDataFull = undefined;
                        //$scope.searchCaseResultFull = undefined;
                        //$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.StudentAdmissionFeesNotPaidReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportNotPaidDataFull = response.obj;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.exportNotPaidData = function () {

        if ($scope.exportNotPaidDataFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentAdmissionFeesNotPaidReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Student Admission Fees Not Paid Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationId', title: 'Application Id' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'ApplicationReservationName', title: 'Programme Name' },
                { columnid: 'SocialCategoryName', title: 'Branch Name' },
                { columnid: 'CommitteeName', title: 'Institute Name' },
                { columnid: 'IsPhysicallyChanllenged', title: 'IsPhysicallyChanllenged' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'GroupName', title: 'Group Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'IsAdmissionFeePaid', title: 'Is Admission Fee Paid' },
                { columnid: 'IsInstalmentSelected', title: 'Is Instalment Selected' },
                { columnid: 'IsVerificationEmail', title: 'Is Verification Email' },
                { columnid: 'IsVerificationSms', title: 'Is Verification Sms' },
                { columnid: 'IsVerificationEmailOns', title: 'Is Verification Email On' },
                { columnid: 'IsVerificationSmsOns', title: 'Is Verification Sms On' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportNotPaidDataFull]);
    };

    $scope.backToList = function () {
        //debugger;
        //alert($localStorage.InstId);
        if ($localStorage.InstId == null || $localStorage.InstId == undefined) {
            $state.go('ApplicationStatistics');

        }
        else {


            $rootScope.Checkls = true;
            $state.go('ApplicationStatisticsByInstitute');
        }



    };



});

