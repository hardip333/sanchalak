app.controller('AddonDetailsReportCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Add on Details Reports ";

    $scope.AddonDetailsReport = {}

    $scope.exporttoexcel = [

        { columnid: 'ApplicationId', title: 'Application Form No.' },
        { columnid: 'FirstName', title: 'FirstName' },
        { columnid: 'FacultyName', title: 'Faculty Name' },
        { columnid: 'ProgrammeName', title: 'Programme Name' },
        { columnid: 'BranchName', title: 'Branch Name' },
        { columnid: 'TitleName', title: 'Adon Question' },
        { columnid: 'AddOnValue', title: 'Answer By Applicant' },
    ];




    /*Reset Academic Year Level*/
    $scope.resetAddonDetailsReport = function () {
        $scope.AddonDetailsReport = {};
    };

    /*Get Application Payment Report*/
    $scope.getAddonDetailsReport = function () {
        //debugger;

        //var data = new Object();
        var obj = {

            ProgrammeInstancePartTermId: $scope.AddonDetailsReport.ProgInstPartTermId,
        }
        console.log(obj);
        $http({
            method: 'POST',
            url: 'api/AddonDetailsReport/AddonDetailsReportGet',
            data: obj,
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

                    $scope.AddonDetailsReportTableParams = new NgTableParams({
                        page: 1,
                        count: 10
                        //count:response.obj.length

                    }, {
                        dataset: response.obj

                    });
                    $scope.exporttoexcel = response.obj;
                    console.log($scope.exporttoexcel);
                }
                //alert($scope.exporttoexcel[0].InstancePartTermName);
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Academic Year*/



    $scope.getProgInstPartTerm = function () {
        $http({
            method: 'POST',
            url: 'api/AddonDetailsReport/IncProgrammeInstancePartTermGet',
            data: $scope.AddonDetailsReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgInstPartTermList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
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
    }



    $scope.exportDataFull = function () {
        //debugger;
        //alert($scope.AddonDetailsReport.ProgInstPartTermId);
        //alert($scope.Institute.InstituteName);
        var FacultyName = $localStorage.facultyName;
        var ProgInstPartTermName = $scope.exporttoexcel[0].InstancePartTermName;
        //var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        //alert(LongDate);



        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        //alert(ShortDate);



        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        //alert(DateWithoutDashed);



        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        // alert(time);

        var dateAndTime = ShortDate + time;
        var ExcelFileName = "AddonDetailsReport_" + ShortDate + time;
        //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}</h2><h2>Part Term Name: {ProgInstPartTermName}</h2><h2>Destination Request ({dateAndTime}) </h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        //count: response.obj.length;

        var table = document.getElementById("test");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime, FacultyName: FacultyName, ProgInstPartTermName: ProgInstPartTermName, table: table.innerHTML };

        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        a.click();
    };




});

