app.controller('PaperPRNStatisticsCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Paper PRN Statistics";

    $scope.cardTitle = "Paper PRN Statistics Operation";


    //$scope.ApplicationStatisticsTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.AppStats
    //});

    $scope.resetPaperPRNStatistics = function () {
        $scope.PaperPRNStats = {};

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

    $scope.getMstInstitute = function () {

        $http({
            method: 'POST',
            url: 'api/PaperPRNStatistics/MstInstituteGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
                //$scope.getMstInstituteInstNameById();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstInstituteInstNameById = function () {
        if ($scope.PaperPRNStats.InstituteId > 0) {
            var Institute = { InstituteId: $scope.PaperPRNStats.InstituteId };
            $http({
                method: 'POST',
                url: 'api/PaperPRNStatistics/MstInstituteGetInstNameById',
                data: Institute,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.InstituteNameList = response.obj[0];
                    //$scope.ProgStats.InstituteName = $scope.InstituteNameList.InstituteName;

                })
                .error(function (res) {
                    alert(res);
                });
        }
        if ($scope.PaperPRNStats.InstituteId == 0) {
            $scope.PaperPRNStats.InstituteName = "All Faculty";
        }
    };

    $scope.getIncAcadYearCodeById = function () {
        var AcadId = { AcademicYearId: $scope.ProgStats.AcademicYearId };
        $http({
            method: 'POST',
            url: 'api/PaperPRNStatistics/IncAcadYearCodeGetById',
            data: AcadId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadYearCodeList = response.obj[0];
                //$scope.ProgStats.AcademicYearCode = $scope.AcadYearCodeList.AcademicYearCode;


            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                //$scope.PaperPRNStats.FacultyId = $scope.PaperPRNStats.Id;
                $scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {

        var InstituteId = { InstituteId: $localStorage.facultyDepartIntituteId };

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
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            // data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getPaperPRNStatisticsGetByFacultyId = function (InstituteId, ProgrammeInstancePartTermId) {


        $http({
            method: 'POST',
            url: 'api/PaperPRNStatistics/PaperPRNStatisticsGetByFacultyId',
            data: { InstituteId: $localStorage.facultyDepartIntituteId, ProgrammeInstancePartTermId: $scope.PaperPRNStats.ProgrammeInstancePartTermId },
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

                        $scope.PaperPRNStatisticsTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.PaperPRNStatisticsData = response.obj[0];
                        $scope.ModelCount = {};
                        $scope.ModelCount.Intake = $scope.PaperPRNStatisticsData.Intake;
                        $scope.ModelCount.AdmittedStudent = $scope.PaperPRNStatisticsData.AdmittedStudent;
                        $scope.ModelCount.PrnGenerated = $scope.PaperPRNStatisticsData.PrnGenerated;
                        $scope.ModelCount.PaperSelected = $scope.PaperPRNStatisticsData.PaperSelected;
                        $scope.ModelCount.PaperPendingSelection = $scope.PaperPRNStatisticsData.PaperPendingSelection;

                        console.log("===");
                        console.log($scope.PaperPRNStatisticsData);
                    }
                console.log(response.obj);
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPaperPRNStatisticsByInstituteId = function () {

        var FacId = { InstituteId: $scope.PaperPRNStats.InstituteId };

        $http({
            method: 'POST',
            url: 'api/PaperPRNStatistics/PaperPRNStatisticsGetByAcademics',
            data: FacId,
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

                        $scope.PaperPRNStatisticsByInstIdTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.PaperPRNStatisticsByInstIdData = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.move = function (PIPTId) {
        $localStorage.Stats = {};
        $localStorage.Stats.ProgramInstancePartTermId = PIPTId;
        $localStorage.Stats.FlagFromAppStats = true;

    };

    $scope.moveAcademic = function (PIPTId) {
        $localStorage.Stats = {};
        $localStorage.Stats.ProgramInstancePartTermId = PIPTId;
        $localStorage.Stats.InstituteId = $scope.PaperPRNStats.InstituteId;
        $localStorage.Stats.FlagFromAppStats = true;

    };

    $scope.exportPaperPRNStatsData = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Institute Name: ' + InstituteName + '<br>' +
                    '  Application Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Intake', title: 'Intake Capacity' },
                { columnid: 'AdmittedStudent', title: 'Admitted Student' },
                { columnid: 'PrnGenerated', title: 'PRN Generated' },
                { columnid: 'PaperSelected', title: 'Paper Selected' },
                { columnid: 'PaperPendingSelection', title: 'Paper Pending Selection' }


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaperPRNStatisticsData]);

        // var FacultyName = $scope.Institute.FacultyName;
        // var InstituteName = $scope.Institute.InstituteName;
        // var LongDate = new Date($.now());
        // //alert(LongDate);

        // var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        // //alert(ShortDate);

        // var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        // //alert(DateWithoutDashed);

        // var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();                
        //// alert(time);

        // var dateAndTime = ShortDate + time;
        // var ExcelFileName = "ApplicationStatistics_" + ShortDate + time;     
        // //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        // var uri = 'data:application/vnd.ms-excel;base64,'           
        //     , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>Application Statistics ({dateAndTime}) </h2>{table}</table></body></html>'
        //     , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //     , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        // var table = document.getElementById("ApplicationStatsId");                
        // var filters = $('.ng-table-filters').remove();
        // var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML};       

        // $('.ng-table-sort-header').after(filters);
        // var url = uri + base64(format(template, ctx));

        // var a = document.createElement('a');
        // a.href = url;
        // a.download = ExcelFileName + '.xls';
        // a.click();
    };

    $scope.exportPaperPRNStatsByInstituteData = function () {

        //if ($scope.ApplicationStatisticsByInstIdData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //var FacultyName = $scope.Institute.FacultyName;
        //var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title:// 'FacultyName: ' + FacultyName + '<br>' +
                    //'   Institute Name: ' + InstituteName + '<br>' +
                    '  Application Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmittedStudent', title: 'Admitted Student' },
                { columnid: 'PrnGenerated', title: 'PRN Generated' },
                { columnid: 'PaperSelected', title: 'Paper Selected' },
                { columnid: 'PaperPendingSelection', title: 'Paper Pending Selection' }


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaperPRNStatisticsByInstIdData]);
    };



});