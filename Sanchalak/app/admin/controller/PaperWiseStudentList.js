app.controller('PaperWiseStudentListCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    if ($localStorage.Stats) {
        //debugger
        $scope.PaperWiseStudentCount = {};
        $scope.PaperWiseStudentCount.PaperId = $localStorage.Stats.PaperId;      
        
    }

    $scope.getPaperWiseStudentList = function () {
        //alert($scope.PaperWiseStudentCount.PaperId);
        $http({
            method: 'POST',
            url: 'api/PaperWiseStudentList/PaperWiseStudentListGet',
            data: { PaperId: $scope.PaperWiseStudentCount.PaperId },
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
                    $scope.PaperWiseStudentListTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.PaperWiseStudentListData = response.obj[0];
                    $scope.ExcelPaperWiseStud = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
        
    $scope.exportPaperWiseStudentList = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        var PaperCode = $scope.PaperWiseStudentListData.PaperCode;
        var PaperName = $scope.PaperWiseStudentListData.PaperName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PaperWiseStudentList_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper Name: ' + PaperName + '<br>' +
                    'Paper Code: ' + PaperCode + '<br>' +
                    '  Paper Wise Student List| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ExamDateView', title: 'Exam Date' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'StudentPRN', title: 'Student PRN' },
                { columnid: 'SeatNumber', title: 'Seat Number' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExcelPaperWiseStud]);

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
        $state.go('PaperWiseStudentCount');
    }




});