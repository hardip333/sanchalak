app.controller('IncStudentAcademicInformationCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Student Academic Information";

    $scope.IncStudentAcademicInformationByReport = {}

    

    $scope.test = "All";

    $scope.InstPartList = []

    /*Reset Academic Year Level*/
    $scope.resetIncStudentAcademicInformationByReport = function () {
        $scope.IncStudentAcademicInformationByReport = {};
    };

    /*Get Application Payment Report*/
    $scope.getIncStudentAcademicInformationByReport = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/IncStudentAcademicInformationByReport/IncStudentAcademicInformationByReportGet',
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

                    //$scope.getApplicationPaymentReport = response.obj;
                    //$scope.refreshtable();
                    $scope.IncStudentAcademicInformationByReportTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj
                        

                    });

                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Academic Year*/
    

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

        var LongDate = new Date($.now());
        //alert(LongDate);

        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        //alert(ShortDate);

        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        //alert(DateWithoutDashed);

        var time = "_" + LongDate.getHours() +
            + LongDate.getMinutes() +
            + LongDate.getSeconds();

        //alert(time);
        //alert("ExportedTable_" + DateWithoutDashed + time);

        var ExcelFileName = "StudentAcademicInformation_" + DateWithoutDashed + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = document.getElementById("studentinfo");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML };
        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));
        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        a.click();
    };
    


    
    

});

        