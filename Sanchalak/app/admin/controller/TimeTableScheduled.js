app.controller('TimeTableScheduledCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Time Table Scheduled";
   
    $scope.TimeTableScheduled = {};
    $scope.ExamEvent = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    } 

    //Academic Year List Get
    $scope.GetAcademicYearList = function () {
       
        $http({
            method: 'POST',
            url: 'api/TimeTableScheduled/GetAcademicYearList',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Exam Event List Get
    $scope.GetExamEventList = function () {
     
        $http({
            method: 'POST',
            url: 'api/TimeTableScheduled/GetExamEventList',
            //data: $scope.ExamEvent,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamEventList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for Time Table Scheduled Data
    $scope.GetTimeTableScheduledData = function () {

        $scope.checkDataExists = false;

        if ($scope.ExamEvent.AcademicYearId == null || $scope.ExamEvent.AcademicYearId == "" || $scope.ExamEvent.AcademicYearId === undefined) {
            alert("Please select Academic Year...!");
            $scope.checkDataExists = true;
        }
        else if ($scope.ExamEvent.ExamEventId == null || $scope.ExamEvent.ExamEventId == "" || $scope.ExamEvent.ExamEventId === undefined) {
            alert("Please select Exam Event...!");
            $scope.checkDataExists = true;
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/TimeTableScheduled/GetTimeTableScheduledData',
                data: $scope.ExamEvent,
                headers: { "Content-Type": 'application/json' }
            })


                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        if (response.response_code == "201") {
                            $scope.AcademicList = {};
                            $scope.checkDataExists = true;
                        }
                    }
                    else {
                        $scope.TimeTableScheduledTableparam = new NgTableParams({

                        },
                            { dataset: response.obj });
                        $scope.TimeTableScheduledData = response.obj;
                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }
                    }
                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    $scope.cancelTimeTableSchedule = function () {
        $scope.ExamEvent = {};
        $scope.showFormFlag = false;
    };

    //Excel Code for Schedule Time-Table
    $scope.ExportTimeTableScheduledDatatoExcel = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Time-Table_Scheduled_Data " + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            //caption: {
            //    title: 'Time-Table Scheduled Data on ' + DateAndTime,
            //},
            columns: [
                //{ columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'PaperCode', title: 'PaperCode' },
                { columnid: 'PaperName', title: 'PaperName' },
                { columnid: 'ExamDate', title: 'ExamDate' },
                { columnid: 'Start_Time_HR', title: 'Start_Time_HR' },
                { columnid: 'Start_Time_MIN', title: 'Start_Time_MIN' },
                { columnid: 'Start_AM_PM', title: 'Start_AM_PM' },
                { columnid: 'End_Time_HR', title: 'End_Time_HR' },
                { columnid: 'End_Time_MIN', title: 'End_Time_MIN' },
                { columnid: 'End_AM_PM', title: 'End_AM_PM' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.TimeTableScheduledData]);
    };

});



