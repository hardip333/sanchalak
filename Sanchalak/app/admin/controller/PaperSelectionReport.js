app.controller('PaperSelectionReportCtrl', function ($scope, $http, $rootScope, $window, $state, $filter, $cookies, $mdDialog, $localStorage, NgTableParams) {


    $rootScope.pageTitle = "Paper Selection Report";

    var InstPartList = [];

    $scope.ShowFlag = false;
    //$scope.AppCount = 0;
    

    $scope.PaperSelectionReport = {};

    

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code               
                $scope.PaperSelectionReport.InstituteId = $scope.Institute.InstituteId;

                $scope.getAcademicYear();

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getAcademicYear = function () {

        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/AcademicYearGetForDropDown',
            data: $scope.PaperSelectionReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammePartTermGetByInstIdAcaId = function () {

        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/ProgrammePartTermGetByInstIdAcaId',
            data: $scope.PaperSelectionReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartTermList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgPartTermGetByInstIdAcaId = function () {

        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/ProgPartTermGetByInstIdAcaId',
            data: $scope.PaperSelectionReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartTermList = response.obj;
                $scope.ShowFlag = true;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.EnableButton = function (data) {

        //debugger;
        $scope.xyz = data;

        if ($scope.xyz == true) {
            $scope.ShowFlag = true;
            $scope.ShowFlag1 = false;
            $scope.ProgrammePartTermList = {};
            $scope.PaperSelectionReportStudentTableParams = {};
        }
        if ($scope.xyz == false) {
            $scope.ShowFlag1 = true;
            $scope.ShowFlag = false;
            $scope.ProgrammePartTermList = {};
            $scope.PaperSelectionReportPaperTableParams = {};
            
        }
    };


    $scope.getPaperSelectionReportPaper = function () {

        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/PaperSelectionReportPaperGet',
            data: {
                ProgrammePartTermId: $scope.PaperSelectionReport.ProgrammePartTermId,
                InstituteId: $scope.PaperSelectionReport.InstituteId,
                AcademicYearId: $scope.PaperSelectionReport.AcademicYearId,
            },
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
                    $scope.PaperSelectionReportPaperTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFullPaper = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    $scope.getPaperSelectionReportStudent = function () {

        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/PaperSelectionReportStudentGet',
            data: {
                ProgrammePartTermId: $scope.PaperSelectionReport.ProgrammePartTermId,
                InstituteId: $scope.PaperSelectionReport.InstituteId,
                AcademicYearId: $scope.PaperSelectionReport.AcademicYearId,
            },
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
                    $scope.PaperSelectionReportStudentTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFull = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.exportDataStudent = function () {

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
        var ExcelFileName = "PaperSelectionReportForStudent_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Paper Selection Report For Student | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PartTermName', title: 'Part Term Name' },                
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Student Name' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' }
                

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };


    $scope.getPaperSelectionReportPaperExcel = function (PaperId) {

        var PaperId = {

            PaperId: PaperId
        }
        $http({
            method: 'POST',
            url: 'api/PaperSelectionReport/PaperSelectionReportPaperExcelGet',
            data: PaperId,
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
                    $scope.PaperSelectionReportPaperExportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFullPaperExport = response.obj;
                    $scope.exportDataPaperExcel();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportDataPaperExcel = function () {

        if ($scope.exportDataFullPaperExport == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PaperSelectionReportPaperExcel_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Paper Selection Report For Student By PaperId | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Student Name' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' }


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFullPaperExport]);
    };



});