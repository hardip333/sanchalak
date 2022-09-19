app.controller('PreExaminationStatisticsCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage PreExamination Statistics";

    $scope.cardTitle = "Pre Examination Statistics";

    $scope.PreExaminationStatistics = {};

    $scope.resetPreExaminationStatistics = function () {
        $scope.PreExaminationStatistics = {};
    };

    $scope.resetConsolidatedReport = function () {
        $scope.PreExaminationStatistics = {};
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

    $scope.getExamEventMasterList = function () {

        $http({
            method: 'POST',
            url: 'api/PreExaminationStatistics/ExamEventGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ExamEventList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

                if ($rootScope.Checkls == true) {
                    if ($localStorage.Stats.ExamEventId != null || $localStorage.Stats.ExamEventId != undefined) {
                        $scope.PreExaminationStatistics.ExamEventId = $localStorage.Stats.ExamEventId;
                        $scope.getPreExaminationStatistics();
                    }
                }
                else {
                    $localStorage.Stats = {};
                }


            })
            .error(function (res) {
                alert(res);
            });
    };



    $scope.getPreExaminationStatistics = function () {

        var data = { ExamEventId: $scope.PreExaminationStatistics.ExamEventId };

        $http({
            method: 'POST',
            url: 'api/PreExaminationStatistics/PreExaminationStatisticsGet',
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
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.PreExaminationStatisticstTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.PreExaminationStatisticstTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.PreExaminationStatisticsData = response.obj[0];
                        $scope.ExcelPreExamData = response.obj;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.getPreExaminationStatisticsGetPPT = function () {
        //debugger
        //$scope.ExamEventId = ExamEventId;
        //$scope.FacultyId = FacultyId;
        //$scope.InstituteId = InstituteId;
        var data = {

            ExamEventId: $localStorage.PreExamData.ExamEventId,
            FacultyId: $localStorage.PreExamData.FacultyId,
            InstituteId: $localStorage.PreExamData.InstituteId,
            FacultyExamMapId: $localStorage.PreExamData.FacultyExamMapId

        };
        //console.log(data.InstituteId)

        $http({
            method: 'POST',
            url: 'api/PreExaminationStatistics/PreExaminationStatisticsfromPPT',
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
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.PreExaminationStatisticsGetPPTTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        //debugger
                        $scope.PreExaminationStatisticsfromPPTTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        //$scope.PreExaminationStatisticsGetPPTData = response.obj[0];
                        //$scope.ExcelPreExamData = response.obj;
                        $scope.PreExam = response.obj;
                        /*$state.go('PreExaminationStatisticsGetPPT');*/

                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    //if ($state.current.name == 'PreExaminationStatisticsGetPPT') {

    //    $scope.getPreExaminationStatisticsGetPPT($scope.ExamEventId, $scope.FacultyId, $scope.InstituteId);

    //}

    $scope.getmyFunction = function (ExamEventId, FacultyId, InstituteId, FacultyExamMapId) {
        //debugger
        $localStorage.PreExamData = {};

        $localStorage.PreExamData.ExamEventId = ExamEventId;
        $localStorage.PreExamData.FacultyId = FacultyId;
        $localStorage.PreExamData.InstituteId = InstituteId;
        $localStorage.PreExamData.FacultyExamMapId = FacultyExamMapId;
        $state.go('PreExaminationStatisticsGetPPT');
    }

    $scope.move = function (ExamEventId, FacultyExamMapId, InstituteId, ProgrammePartTermId, FacultyId, SpecialisationId, flag) {
        //debugger
        $localStorage.Stats = {};
        $localStorage.Stats.ExamEventId = ExamEventId;
        $localStorage.Stats.FacultyId = FacultyId;
        $localStorage.Stats.InstituteId = InstituteId;
        $localStorage.Stats.ProgrammePartTermId = ProgrammePartTermId;
        $localStorage.Stats.FacultyExamMapId = FacultyExamMapId;
        $localStorage.Stats.SpecialisationId = SpecialisationId;
        $localStorage.Stats.Flag = flag;



    };



    $scope.exportPreExaminationStatistics = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        var DisplayName = $scope.PreExaminationStatisticsData.DisplayName;
        var ScheduleCode = $scope.PreExaminationStatisticsData.ScheduleCode;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PreExaminationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Display Name: ' + DisplayName + '<br>' +
                    'Schedule Code: ' + ScheduleCode + '<br>' +
                    '  PreExamination Statistics| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ScheduleCode', title: 'Schedule Code' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'AdmittedStudents', title: 'Admitted Student' },
                { columnid: 'PaperSelected', title: 'Paper Selected' },
                { columnid: 'ExamFormGenerated', title: 'Exam Form Generated' },
                { columnid: 'SeatNoGenerated', title: 'Seat No Generated' },
                { columnid: 'VenueAllocation', title: 'Venue Allocation' },
                { columnid: 'InwardModeStudent', title: 'Inward Student' },
                { columnid: 'NotInwardModeStudent', title: 'No Inward Student' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExcelPreExamData]);

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

    $scope.backToList = function () {

        $state.go('PreExaminationStatistics');

    }

});