app.controller('RefundRequestByFacultyCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.RefundRequestByFaculty = {}

    $scope.TakeLocalStoradeValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.RefundRequestByFaculty.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getRefundRequestByFaculty();
        }
        else {
            $localStorage.Stats = null;
            $scope.RefundRequestByFaculty = null;
        }
    };


    $scope.GetLocalStorageValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.RefundRequestByFaculty.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getRefundRequestByFaculty();
        }
        else {
            $localStorage.Stats = null;
            $scope.RefundRequestByFaculty = null;
        }
    };

    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {

        $http({
            method: 'Post',
            url: 'api/RefundRequestByFaculty/IncProgInsPartTermListGetByInstituteId',
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
            url: 'api/RefundRequestByFaculty/ApplicationListGet',
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
    $scope.getRefundRequestByFaculty = function () {

        // alert($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.RefundRequestByFaculty.ProgrammeInstancePartTermId == "" ||
            $scope.RefundRequestByFaculty.ProgrammeInstancePartTermId == null ||
            $scope.RefundRequestByFaculty.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/RefundRequestByFaculty/RefundRequestByFacultyGet',
            data: { ProgrammeInstancePartTermId: $scope.RefundRequestByFaculty.ProgrammeInstancePartTermId },
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
                        $scope.RefundRequestByFacultyTableParams = new NgTableParams({
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
                    $scope.RefundRequestByFacultyTableParams = new NgTableParams({
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



    //Excel Refund Request By Student
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
        var ExcelFileName = "RefundRequestByFacultyReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refund Request By Faculty Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationId', title: 'Application No.' },
                { columnid: 'FullName', title: 'Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },               
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'BranchName', title: 'Branch Name' }
                

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };

    



});

