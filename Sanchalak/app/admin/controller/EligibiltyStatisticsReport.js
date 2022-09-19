app.controller('EligibiltyStatisticsReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Eligibiltity Statistics";

    $scope.cardTitle = "Eligibiltity Statistics Operation";


    //$scope.ApplicationStatisticsTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.AppStats
    //});

    $scope.resetEligibilityStatistics = function () {
        $scope.EligibiltyStatisticsReport = {};

    };


    $scope.expand_row = function (Id) {
        let element = document.getElementById('expand' + Id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + Id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + Id).innerHTML = "+"
            element.add("collapse")
        }
    };

    $scope.IncAcademicYearListGet = function () {
        // debugger
        $http({
            method: 'POST',
            url: 'api/EligibiltyStatisticsReport/IncAcademicYearListGet',
            data: $scope.EligibiltyStatisticsReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                if ($rootScope.Checkls == true) {
                    //debugger
                    $scope.EligibiltyStatisticsReport = {};
                    $scope.EligibiltyStatisticsReport.AcademicYearId = $localStorage.AcademicYearId;
                    $scope.EligibiltyStatisticsReport.FacultyId = $localStorage.FacultyId;
                    $scope.EligibiltyStatisticsReport.InstituteId = $localStorage.InstituteId;

                    $scope.getEligibilityStatistics();
                    $scope.getFacultyById($scope.EligibiltyStatisticsReport.FacultyId);
                    $scope.getMstInstituteGetByFacultyId($scope.EligibiltyStatisticsReport.FacultyId);

                    //$scope.getApplicationStatisticsListByFacultyId();
                }
                else {

                    $localStorage.AcademicYearId = {};
                    //$localStorage.FacultyId = {};
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function (FacultyId) {

        $http({
            method: 'POST',
            url: 'api/EligibiltyStatisticsReport/FacultyGetById',
            data: { FacultyId: FacultyId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacList = response.obj;


            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstInstituteGetByFacultyId = function (FacultyId) {
        //debugger
        var obj = { FacultyId: FacultyId }

        $http({
            method: 'POST',
            url: 'api/EligibiltyStatisticsReport/MstInstituteGetByFacultyId',
            data: obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.InstituteList = response.obj;


            })
            .error(function (res) {
                alert(res);
            });
    };



    $scope.getEligibilityStatistics = function () {
        //debugger
        var data = {
            FacultyId: $scope.EligibiltyStatisticsReport.FacultyId,
            AcademicYearId: $scope.EligibiltyStatisticsReport.AcademicYearId,
            InstituteId: $scope.EligibiltyStatisticsReport.InstituteId
        };

        $http({
            method: 'POST',
            url: 'api/EligibiltyStatisticsReport/EligibilityStatisticsGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        // debugger
                        $scope.EligibilityStatisticsTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.EligibilityStatisticsData = response.obj[0];
                        $scope.ExcelEligibilityStatistics = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.moveAcademic = function (PIPTId) {
        // debugger
        $localStorage.Stats = {};
        $localStorage.Stats.ProgramInstancePartTermId = PIPTId;
        $localStorage.Stats.AcademicYearId = $scope.EligibiltyStatisticsReport.AcademicYearId
        $localStorage.Stats.FacultyId = $scope.EligibiltyStatisticsReport.FacultyId;
        $localStorage.Stats.InstituteId = $scope.EligibiltyStatisticsReport.InstituteId;
        $localStorage.Stats.FlagFromAppStats = true;

    };



});