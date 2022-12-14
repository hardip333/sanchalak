app.controller('ExamScheduleReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Time Table Conflicts Report";
    $scope.TimeTableConflicts = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.ExamEventGet = function () {
        $scope.ExamEventList = {};
        $http({
            method: 'POST',
            url: 'api/ExamScheduleReport/ExamEventGet',
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
                        $scope.ExamEventList = {};

                    }
                }
                else {
                    $scope.ExamEventList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };


    $scope.getExamScheduleReport = function () {
      
        if ($scope.ExamSchedule.ExamMasterId == null || $scope.ExamSchedule.ExamMasterId === undefined || $scope.TimeTableConflicts.ExamMasterId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Exam Event DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ExamScheduleReport/ExamScheduleReportGetByExamMasterId',
                data: $scope.ExamSchedule,
                headers: { "Content-Type": 'application/json' }
            })

                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {

                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                        $scope.NoRecLabel = true;
                        $scope.IsTableVisible = false;
                        $scope.IsExcelButton = false;

                    }

                    else {

                        $scope.offSpinner();
                        $scope.ExamScheduleReport = response.obj;
                        console.log($scope.ExamScheduleReport);

                        $scope.IsTableVisible = true;
                        $scope.IsExcelButton = true;
                        $scope.NoRecLabel = false;
                        $scope.ExamScheduleReportTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })

                    }

                })

                .error(function (res) {


                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

                });
        }
    };

    $scope.cancelExamScheduleReport = function () {
        $scope.ExamSchedule = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
    };

    $scope.exportData = function () {


        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ExamScheduleReport" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'ExamScheduleReport|Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ProgrammeModeName', title: 'Programme Mode Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'PartName', title: 'Part Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'ExamDate', title: 'Exam Date' },              
                { columnid: 'TeachingLearningMethodName', title: 'Teaching Learning Method Name' },
                { columnid: 'AssessmentMethodName', title: 'Assessment Method Name' },
                { columnid: 'AssessmentType', title: 'Assessment Type' },
                { columnid: 'StudentCount', title: 'Student Count' },






            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExamScheduleReport]);
    };


});