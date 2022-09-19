﻿app.controller('DailyPaperReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Daily Paper Report";

    $scope.cardTitle = "Daily Paper Report";

    $scope.DailyPaperReport = {};
    $scope.BlockWiseTable1 = false;
    $scope.resetDailyPaperReport = function () {
        $scope.DailyPaperReport = {};
    };

    $scope.getExamEventMasterList = function () {

        $http({
            method: 'POST',
            url: 'api/DailyPaperReport/ExamEventGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ExamEventList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getDailyPaperReport = function () {
        //debugger
        $scope.contentPresent = false;
        var Obj = {
            ExamEventId: $scope.DailyPaperReport.ExamEventId,
            ExamDate: $scope.DailyPaperReport.ExamDate,
            ExamDate2: $scope.DailyPaperReport.ExamDate2,
        };
        console.log(Obj)
        $http({
            method: 'POST',
            url: 'api/DailyPaperReport/DailyPaperReportGet',
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
                        $scope.DailyPaperReportTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.DailyPaperReportTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        //$scope.DailyPaperReportData = response.obj[0];
                        $scope.ExcelDailyPaperReport = response.obj;
                        $scope.BlockWiseTable1 = true;
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


    $scope.exportDailyPaperReport = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //debugger
        var ExamEventName = $scope.ExcelDailyPaperReport[0].ExamEventName;
        var ShortDate1 = $filter('date')($scope.DailyPaperReport.ExamDate, "dd-MM-yyyy");
        var ShortDate2 = $filter('date')($scope.DailyPaperReport.ExamDate2, "dd-MM-yyyy");

        //var ExamDate = $scope.DailyPaperReport.ExamDate;
        //var ExamDate2 = $scope.DailyPaperReport.ExamDate2;
        //var ExamDate2 = $scope.DailyPaperReportData.ExamDate2;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        //var ExamDate = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "DailyPaperReport_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: ' Exam Event Name: ' + ExamEventName + '<br>' +
                    ' Exam Date: ' + ShortDate1 + '<br>' + ShortDate2 + '<br>',

            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'CenterCode', title: 'Center Code' },
                { columnid: 'ExamCenter', title: 'Exam Center' },
                { columnid: 'ExamCode', title: 'Exam Code' },
                { columnid: 'ExamVenueName', title: 'Exam Venue Name' },
                { columnid: 'Course', title: 'Course' },
                { columnid: 'ExamDate', title: 'Paper Date' },
                { columnid: 'PaperStartTime', title: 'Paper Start Time' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'StudentCount', title: 'StudentCount' }

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExcelDailyPaperReport]);

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



});