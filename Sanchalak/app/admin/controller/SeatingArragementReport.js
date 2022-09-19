app.controller('SeatingArragementReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Seating Arrangement";

    $scope.resetSeatingArrangementReport = function () {

        $scope.SeatingArrangementReport = {};
        console.log($scope.SeatingArrangementReport);
    }
    


    $scope.getInstituteById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/InstituteGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.InstList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getExamEventMasterList = function () {

        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/ExamEventMasterListGet',
            data: $scope.SeatingArrangementReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ExamEventList = response.obj;
                console.log($scope.ExamEventList);
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getExamVenueList = function () {
       
        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/ExamVenueListGet',
            data: { InstituteId: $scope.SeatingArrangementReport.InstituteId},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ExamVenueList = response.obj;
                console.log($scope.ExamVenueList);
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getExamSlotMaster = function () {

        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/ExamSlotMasterGet',
            data: { ExamDate: $scope.SeatingArrangementReport.ExamDate},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ExamSlotMasterList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getExamVenueExamCenter = function () {

        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/ExamVenueExamCenterGet',
            data: { ExamVenueId: $scope.SeatingArrangementReport.ExamVenueId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ExamVenueExamCenterList = response.obj;
                console.log($scope.ExamVenueList);
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getSeatingArrangementReport = function (ExamMasterId, ExamVenueId) {
        //debugger
        $scope.contentPresent = false;
        var Obj = {
            
            InstituteId: $scope.SeatingArrangementReport.InstituteId,
            ExamMasterId: $scope.SeatingArrangementReport.ExamMasterId,
            ExamVenueId: $scope.SeatingArrangementReport.ExamVenueId,
            ExamSlotId: $scope.SeatingArrangementReport.ExamSlotId,
            ExamVenueExamCenterId: $scope.SeatingArrangementReport.ExamVenueExamCenterId,
            ExamDate: $scope.SeatingArrangementReport.ExamDate,
            
        }
        console.log(Obj)
        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/SeatingArrangementReportGet',
            data: Obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.SeatingArrangementReportTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.SeatingArrangementReportTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        
                        $scope.MyTable = response.obj;
                        
                        $scope.PrintInstituteName = $scope.MyTable[0].InstituteName;
                        $scope.PrintExamDate = $scope.MyTable[0].ExamDate;
                        $scope.PrintExamCenterName = $scope.MyTable[0].ExamCenterName;
                        $scope.PrintStartTime = $scope.MyTable[0].PaperStartTime;
                        $scope.PrintEndTime = $scope.MyTable[0].PaperEndTime;
                                               
                        
                        
                    }
                }
                if (response.obj.length == 0) {
                    $scope.contentPresent = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    $scope.getBlockWisePaperReport = function (ExamMasterId, ExamVenueId) {
        //debugger
        $scope.contentPresent = false;
        var Obj = {

            InstituteId: $scope.SeatingArrangementReport.InstituteId,
            ExamMasterId: $scope.SeatingArrangementReport.ExamMasterId,
            ExamVenueId: $scope.SeatingArrangementReport.ExamVenueId,
            ExamSlotId: $scope.SeatingArrangementReport.ExamSlotId,
            ExamVenueExamCenterId: $scope.SeatingArrangementReport.ExamVenueExamCenterId,
            ExamDate: $scope.SeatingArrangementReport.ExamDate,

        }
        console.log(Obj)
        $http({
            method: 'POST',
            url: 'api/SeatingArrangementReport/BlockWisePaperReportGet',
            data: Obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.BlockWisePaperReportTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.BlockWisePaperReportTableParams = new NgTableParams({},
                            { dataset: response.obj });

                        $scope.MyTable1 = response.obj;
                       // debugger
                        $scope.PrintInstituteName1 = $scope.MyTable1[0].InstituteName;
                        $scope.PrintExamDate1 = $scope.MyTable1[0].ExamDate;
                        $scope.PrintExamCenterName1 = $scope.MyTable1[0].ExamVenueExamCenterName;
                        $scope.PrintStartTime1 = $scope.MyTable1[0].PaperStartTime;
                        $scope.PrintEndTime1 = $scope.MyTable1[0].PaperEndTime;



                    }
                }
                if (response.obj.length == 0) {
                    $scope.contentPresent = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };







    $scope.EnableButton = function (data) {

        //debugger;
        $scope.xyz = data;

        if ($scope.xyz == true) {
            $scope.ShowFlag = true;
            $scope.ShowFlag1 = false;
            $scope.MyTable = {};
            $scope.SeatingArrangementReportTableParams = {};
        }
        if ($scope.xyz == false) {
            $scope.ShowFlag1 = true;
            $scope.ShowFlag = false;
            $scope.MyTable1 = {};
            $scope.BlockWisePaperReportTableParams = {};

        }
    };


    
    
});