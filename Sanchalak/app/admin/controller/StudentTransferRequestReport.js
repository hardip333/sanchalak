app.controller('StudentTransferRequestReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Statistics";

    $scope.cardTitle = "Application Statistics Operation";

    $scope.ProgInst = {};
    $scope.ProgInstPartTerm = {};   
    $scope.TransferRequestReport = {};

    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {

        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.AcademicList = {};
                }
                else {
                    $scope.AcademicList = response.obj;
                }
            })
            .error(function (res) {
                //alert(res);
            });
    };

    /* Faculty List Get Method*/
    $scope.FacultyGet = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacultyList = {};
                }
                else {
                    $scope.FacultyList = response.obj;
                }

            })
            .error(function (res) {
                //alert(res);
            });
    };

    /*Programme Instance List By Academic Year Id and Faculty Id */
    $scope.getProgrammeInstanceListByAcadId = function () {
        
        $scope.ProgInst.FacultyId = $scope.TransferRequestReport.FacultyId;
        $scope.ProgInst.AcademicYearId = $scope.TransferRequestReport.AcademicYearId;
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.InstList = {};
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Branch/Specialisation List By Programme Instance Id */
    $scope.getBranchListByProgInstId = function () {
        $scope.ProgInst.Id = $scope.TransferRequestReport.ProgrammeInstanceId;

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList = {};
                }
                else {
                    $scope.BranchList = response.obj;
                }

            })
            .error(function (res) {
                //alert(res);
            });
    };

    /*Programme Part List By Programme Instance Id */
    $scope.getProgrammePartListByProgInstId = function () {

        $scope.ProgInst.Id = $scope.TransferRequestReport.ProgrammeInstanceId;
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/ProgrammePartGetByProgInstId',
            data: $scope.TransferRequestReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Programme Part Term List By Programme Instance Id */
    $scope.getProgPartTermListByProgInstPartId = function () {

        //$scope.ProgInstPartTerm.ProgrammeInstanceId = $scope.TransferRequestReport.ProgrammeInstanceId;
        $scope.ProgInstPartTerm.ProgrammeInstancePartId = $scope.TransferRequestReport.ProgrammePartId;
        $scope.ProgInstPartTerm.SpecialisationId = $scope.TransferRequestReport.SpecialisationId;
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/ProgrammePartTermGetByProgInstId',
            data: $scope.ProgInstPartTerm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartTermList = {};
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getStudentTransferRequestData = function () {
        if ($scope.TransferRequestReport.AcademicYearId === null || $scope.TransferRequestReport.AcademicYearId === undefined || 
            $scope.TransferRequestReport.FacultyId === null || $scope.TransferRequestReport.FacultyId === undefined ||
            $scope.TransferRequestReport.ProgrammeInstanceId === null || $scope.TransferRequestReport.ProgrammeInstanceId === undefined ||
            $scope.TransferRequestReport.ProgrammePartId === null || $scope.TransferRequestReport.ProgrammePartId === undefined ||
            $scope.TransferRequestReport.SpecialisationId === null || $scope.TransferRequestReport.SpecialisationId === undefined ||
            $scope.TransferRequestReport.ProgrammeInstancePartTermId === null || $scope.TransferRequestReport.ProgrammeInstancePartTermId === undefined)
        {
            alert("Please Select All Fields!");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/StudentTransferRequest/StudentTransferRequestData',
                data: { ProgInstPartTermId: $scope.TransferRequestReport.ProgrammeInstancePartTermId },
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

                            $scope.StudentTransferRequestReportTableParams = new NgTableParams({}, { dataset: response.obj });
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.exportDataByProgInstPartTermId = function () {

        var acYear = $('#acYear option:selected').text();
        $scope.TransferRequestReport.AcademicYearCode = acYear;

        var AcademicYearCode = $scope.TransferRequestReport.AcademicYearCode;

        var LongDate = new Date($.now());
        //alert(LongDate);

        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        //alert(ShortDate);

        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        //alert(DateWithoutDashed);

        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        // alert(time);

        var dateAndTime = ShortDate + time;
        var ExcelFileName = "StudentTransferRequestReport_" + ShortDate + time;
        //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2 style="text-align: center;">Student Transfer Request Report ({dateAndTime}) </h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        var table = document.getElementById("StudentTransferRequestReport");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, AcademicYearCode: AcademicYearCode || AcademicYearCode, table: table.innerHTML };

        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        a.click();

    };

});