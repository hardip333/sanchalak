app.controller('PreExamStatisticsFacultyCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    if ($localStorage.Stats) {
        //debugger
        $scope.PreExaminationStatistics = {};
        $scope.PreExaminationStatistics.FacultyExamId = $localStorage.Stats.FacultyExamMapId;
        $scope.PreExaminationStatistics.ExamEventId = $localStorage.Stats.ExamEventId;
        $scope.PreExaminationStatistics.FacultyId = $localStorage.Stats.FacultyId;
        $scope.PreExaminationStatistics.InstituteId = $localStorage.Stats.InstituteId;
        $scope.PreExaminationStatistics.ProgrammePartTermId = $localStorage.Stats.ProgrammePartTermId
        $scope.PreExaminationStatistics.SpecialisationId = $localStorage.Stats.SpecialisationId
        $scope.PreExaminationStatistics.Flag = $localStorage.Stats.Flag;

    }

    $scope.getPreExamStatisticsStudentDataFaculty = function () {
        //debugger
        var data = {
            FacultyExamMapId: $scope.PreExaminationStatistics.FacultyExamId,
            ExamEventId: $scope.PreExaminationStatistics.ExamEventId,
            InstituteId: $scope.PreExaminationStatistics.InstituteId,
            FacultyId: $scope.PreExaminationStatistics.FacultyId,
            ProgrammePartTermId: $scope.PreExaminationStatistics.ProgrammePartTermId,
            SpecialisationId: $scope.PreExaminationStatistics.SpecialisationId,
            Flag: $scope.PreExaminationStatistics.Flag
        };

        $http({
            method: 'POST',
            url: 'api/PreExamStatisticsforStudentList/PreExamStatisticsforStudentListFaculty',
            data: data,
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
                    $scope.PreExamStatisticsforStudentListTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.PreExamStatisticsforStudentdata = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportData = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        //var ExcelFileName = "PaperData_" + DateWithoutDashed + time;

        $scope.title = {};
        if ($scope.PreExaminationStatistics.Flag == 1) { $scope.title = "Admitted Student" }
        if ($scope.PreExaminationStatistics.Flag == 2) { $scope.title = "Paper Selected" }
        if ($scope.PreExaminationStatistics.Flag == 3) { $scope.title = "Exam form Generated" }
        if ($scope.PreExaminationStatistics.Flag == 4) { $scope.title = "Seat number Generated" }

        var ExcelFileName = $scope.title + "Report_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: $scope.title + '| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'SeatNumber', title: 'Seat Number' },
                { columnid: 'FullName', title: 'Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Semester Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'ExamVenueName', title: 'Venue Name' },
                { columnid: 'InwardMode', title: 'Inward Mode' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PreExamStatisticsforStudentdata]);
    };

    $scope.backToList = function () {
        //debugger
        if ($localStorage.Stats.ExamEventId != null || $localStorage.Stats.ExamEventId != undefined ||
            $localStorage.Stats.FacultyId != null || $localStorage.Stats.FacultyId != undefined) {

            $rootScope.Checkls = true;
            $state.go('PreExaminationStatisticsFaculty');

        }


    }


});