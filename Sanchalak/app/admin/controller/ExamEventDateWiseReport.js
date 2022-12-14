app.controller("ExamEventDateWiseCtrl", function ($scope, $http, $rootScope,$filter, $state, $cookies, $mdDialog, NgTableParams) {

    $scope.Schedule = {};

    $scope.getExamEvent = function () {

        $http({
            method: 'POST',
            url: 'api/ExamEventDateWiseReport/ExamEventGetForDropDown',
            data: $scope.Schedule,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamMasterListGetActiveList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getData = function (ReportType) {

        $scope.Schedule.ReportType = ReportType;
       
        if ($scope.Schedule.ReportType == "TimeTableSchedule") {

            $http({
                method: 'POST',
                url: 'api/ExamEventDateWiseReport/ExamEventDateWiseReportTimeTableSchedule',
                data: $scope.Schedule,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    debugger
                    if (response.response_code != '200') {
                        //$state.go('login');
                    }
                    else {
                        
                        if (response.obj != "No Record Found") {

                            $scope.TimeTableScheduleTableParam = new NgTableParams({}, { dataset: response.obj });
                            $scope.TimeTableSchedule = response.obj;
                            $scope.exportDataofTimeTableSchedule();
                        }
                        else {
                            alert("No Record Found For This Report");
                        }
                        
                    }
                })
                .error(function (response) {
                });
        }

        if ($scope.Schedule.ReportType == "StudentPaperMapData") {

            $http({
                method: 'POST',
                url: 'api/ExamEventDateWiseReport/ExamEventDateWiseReportStudentPaperMapData',
                data: $scope.Schedule,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    debugger
                    if (response.response_code != '200') {
                        //$state.go('login');
                    }
                    else {
                        
                        if (response.obj != "No Record Found") {

                            $scope.StudentPaperMapDataTableParam = new NgTableParams({}, { dataset: response.obj });
                            $scope.StudentPaperMapData = response.obj;
                            $scope.exportDataofStudentPaperMapData();
                        }
                        else {
                            alert("No Record Found For This Report");
                        }
                        
                    }
                })
                .error(function (response) {
                });
        }
        
        if ($scope.Schedule.ReportType == "ConflictedStudentlistData") {

            $http({
                method: 'POST',
                url: 'api/ExamEventDateWiseReport/ExamEventDateWiseReportConflictedStudentlistData',
                data: $scope.Schedule,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    debugger
                    if (response.response_code != '200') {
                        //$state.go('login');
                    }
                    else {

                        if (response.obj != "No Record Found") {

                            $scope.ConflictedStudentlistDataTableParam = new NgTableParams({}, { dataset: response.obj });
                            $scope.ConflictedStudentlistData = response.obj;
                            $scope.exportDataofConflictedStudentlistData();
                        }
                        else {
                            alert("No Record Found For This Report");
                        }

                    }
                })
                .error(function (response) {
                });
        }
        
    };

    $scope.resetSelection = function () {
        $scope.Schedule = {};
    };

    $scope.exportDataofTimeTableSchedule = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "TimeTableScheduleData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            sheetid: 'Schedule',
            /*style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper Map List | Date and Time: ' + DateAndTime,
            },*/
            columns: [                
                //{ columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'CourseAbbr', title: 'CourseAbbr' },
				{ columnid: 'PaperCode', title: 'PaperCode' },
                { columnid: 'PaperName', title: 'PaperName' },                
                { columnid: 'TeachingLearningMethodName', title: 'TchLrnMth' },                
                { columnid: 'AssessmentMethodName', title: 'AssessmentMethod' },
                { columnid: 'AssessmentType', title: 'AssessmentType' },
                { columnid: 'ExamDate', title: 'ExamDate' },
                { columnid: 'S1', title: 'Start_Time_HR' },
                { columnid: 'S2', title: 'Start_Time_MIN' },
                { columnid: 'S3', title: 'Start_AM_PM' },
                { columnid: 'E1', title: 'End_Time_HR' },
                { columnid: 'E2', title: 'End_Time_MIN' },
                { columnid: 'E3', title: 'End_AM_PM' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.TimeTableSchedule]);
    };

    $scope.exportDataofStudentPaperMapData = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var ExamDate = $filter('date')($scope.Schedule.ExamDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentPaperMapData_" + ExamDate +"_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            //sheetid: 'Schedule',
            /*style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper Map List | Date and Time: ' + DateAndTime,
            },*/
            columns: [
                { columnid: 'PRN', title: 'PRN' },                    
                { columnid: 'PaperCode', title: 'Paper Code' },   
                //{ columnid: 'PaperName', title: 'PaperName' },
                { columnid: 'SeatNumber', title: 'Seat Number' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentPaperMapData]);
    };

    $scope.exportDataofConflictedStudentlistData = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var ExamDate = $filter('date')($scope.Schedule.ExamDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ConflictedStudentlistData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            //sheetid: 'Schedule',
            /*style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper Map List | Date and Time: ' + DateAndTime,
            },*/
            columns: [
                { columnid: 'StudentPRN', title: 'Student PRN' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'ExamPaperDate', title: 'Exam Paper Date' },
                { columnid: 'TimeSlotCode', title: 'Time Slot Code' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ConflictedStudentlistData]);
    };

});

