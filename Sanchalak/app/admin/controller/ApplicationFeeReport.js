app.controller('ApplicationFeeReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Statistics";

    $scope.cardTitle = "Application Statistics Operation";

    $scope.AppFeeReport = {};
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.AppFeeReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                //alert($scope.AcadList.AcademicYearCode);
                //console.log($scope.AcadList);
            })
            .error(function (res) {
                alert(res);
            });
    };   
   
    $scope.getApplicationFeeReportByAcadId = function () {

        if ($scope.AppFeeReport.AcademicYearId === null || $scope.AppFeeReport.AcademicYearId === undefined) {
            alert("Please Select Academic Year");
        }        
        else {
            $http({
                method: 'POST',
                url: 'api/ApplicationFeeReport/ApplicationFeeReportDataByAcadId',
                data: { AcademicYearId: $scope.AppFeeReport.AcademicYearId },
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

                            $scope.ApplicationFeeReportTableParams = new NgTableParams({}, { dataset: response.obj });
                            $scope.ApplicationFeeReportByAcadYear = response.obj;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.getApplicationFeeReportByAcadIdAndInstituteId = function () {

        if ($scope.AppFeeReport.AcademicYearId === null || $scope.AppFeeReport.AcademicYearId === undefined) {
            alert("Please Select Academic Year");
        }
        else {

        $http({
            method: 'POST',
            url: 'api/ApplicationFeeReport/ApplicationFeeReportDataByAcadIdAndInstId',
            data: { AcademicYearId: $scope.AppFeeReport.AcademicYearId, InstituteId: $scope.Institute.InstituteId },
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

                        $scope.ApplicationFeeReportTableParams1 = new NgTableParams({}, { dataset: response.obj });
                        $scope.ApplicationFeeReportByFaculty = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    }; 
    
    $scope.exportDataByAcadId = function () {
   
        alert("Please wait, Excel is being prepared...");

        var acYear = $('#acYear option:selected').text();
        $scope.AppFeeReport.AcademicYearCode = acYear;

        var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationFeeReport(consolidated)_" + DateWithoutDashed + time;

        var test = AcademicYearCode + "_" + DateAndTime;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Application Fee Report | Academic Year: ' + test,

            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term Name' },
                { columnid: 'TotalAmount', title: 'Total Amount' },
                { columnid: 'IsPublishedSts', title: 'Published Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ApplicationFeeReportByAcadYear]);
    };      

    $scope.exportDataByAcadIdAndInstId = function () {

        alert("Please wait, Excel is being prepared...");

        var acYear = $('#acYear option:selected').text();
        $scope.AppFeeReport.AcademicYearCode = acYear;

        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationFeeReport(Faculty)_" + DateWithoutDashed + time;

        var test = FacultyName + "_" + InstituteName + "_" +AcademicYearCode + "_" + DateAndTime;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Application Fee Report | Faculty Name | Institute Name | Academic Year: ' + test,

            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },                
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term Name' },
                { columnid: 'TotalAmount', title: 'Total Amount' },
                { columnid: 'IsPublishedSts', title: 'Published Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ApplicationFeeReportByFaculty]);
    };  

});