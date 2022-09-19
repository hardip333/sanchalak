app.controller('PreVerificationVerifriedCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.PreVerificationVerifried = {}

    $scope.TakeLocalStoradeValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.PreVerificationVerifried.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getPreVerificationVerifried();
        }
        else {
            $localStorage.Stats = null;
            $scope.PreVerificationVerifried = null;
        }
    };


    $scope.GetLocalStorageValue = function () {
        //debugger;
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.PreVerificationVerifried.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getPreVerificationVerifried();
        }
        else {
            $localStorage.Stats = null;
            $scope.VerifiedByFaculty = null;
        }
    };

    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {

        $http({
            method: 'Post',
            url: 'api/PreVerificationVerifried/IncProgInsPartTermListGetByInstituteId',
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
            url: 'api/PreVerificationVerifried/ApplicationListGet',
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
    $scope.getPreVerificationVerifried = function () {

        // alert($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.PreVerificationVerifried.ProgrammeInstancePartTermId == "" ||
            $scope.PreVerificationVerifried.ProgrammeInstancePartTermId == null ||
            $scope.PreVerificationVerifried.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/PreVerificationVerifried/VerifiedByFacultyGet',
            data: { ProgrammeInstancePartTermId: $scope.PreVerificationVerifried.ProgrammeInstancePartTermId },
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
                        $scope.VerifiedByFacultyTableParams = new NgTableParams({
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
                    $scope.VerifiedByFacultyTableParams = new NgTableParams({
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
        var ExcelFileName = "PreVerificationVerifriedReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'PreVerification By Faculty (Verified) Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationId', title: 'Application No' },
                { columnid: 'NameAsPerMarksheet', title: 'Name' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'ApplicationReservationName', title: 'Programme Name' },
                { columnid: 'SocialCategoryName', title: 'Branch Name' },
                { columnid: 'CommitteeName', title: 'Institute Name' },
                { columnid: 'IsPhysicallyChanllenged', title: 'IsPhysicallyChanllenged' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'VerificationApplicant', title: 'Verification of Applicant' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
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

