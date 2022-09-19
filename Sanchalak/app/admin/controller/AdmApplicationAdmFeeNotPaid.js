app.controller('AdmApplicationAdmFeeNotPaidCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Application Admission Fee Not Paid";

    $scope.cardTitle = "Adm Application Admission Fee Not Paid Operation";


    //$scope.AdmFeeNotPaidTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.AdmFeeNotPaid
    //});

    $scope.resetAdmFeeNotPaid = function () {
        $scope.AdmFeeNotPaid = {};
    }

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
        
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code               
               
                $scope.getAdmApplicationAdmFeeNotPaidListByInstituteId();

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmApplicationAdmFeeNotPaidListByInstituteId = function () {
     
        var InstId = {
           // InstituteId:15,
            InstituteId:$scope.Institute.InstituteId,
            ProgrammeInstancePartTermId: $localStorage.Stats.ProgramInstancePartTermId
        };   
        $http({
            method: 'POST',
            url: 'api/AdmApplicationAdmFeeNotPaid/AdmApplicationAdmFeeNotPaidGetByInstituteId',
            data: InstId,
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
                        $scope.AdmFeeNotPaidList = response.obj;
                        $scope.AdmFeeNotPaidTableParams = new NgTableParams({}, { dataset: response.obj });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.exportAdmFeeNotPaidData = function () {
        //if ($scope.AdmFeeNotPaidList == undefined) {

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
        var ExcelFileName = "ApplicantListwhoseAdmissionFeeNotPaid_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Institute Name: ' + InstituteName + '<br>' +
                    '  Applicant List whose Admission Fee Not Paid | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Full Name' },
                { columnid: 'InstancePartTermName', title: 'Semester\Part Term Name' },
                { columnid: 'BranchName', title: 'Specialisation\Branch Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'FeeCategoryName', title: 'Attached Fee Category Name' },            

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.AdmFeeNotPaidList]);


        //var LongDate = new Date($.now());
        ////alert(LongDate);

        //var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        ////alert(ShortDate);

        //var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        ////alert(DateWithoutDashed);

        //var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        //// alert(time);

        //var dateAndTime = ShortDate + time;
        //var ExcelFileName = "AdmissionFeeNotPaidApplicantList_" + ShortDate + time;
        ////var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        //var uri = 'data:application/vnd.ms-excel;base64,'
        //    //, template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head>Application/Form Statistics</head><body><table>{table}</table></body></html>'
        //    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h1>Admission Fee Not Paid Applicant List ({dateAndTime})</h1>{table}</table></body></html>'
        //    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        //var table = document.getElementById("AdmFeeNotPaidId");
        //// var tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
        //var filters = $('.ng-table-filters').remove();
        //var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };
        ////var ctx1 = { dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };

        //$('.ng-table-sort-header').after(filters);
        //var url = uri + base64(format(template, ctx));
        ////var url = uri + base64(format(template, ctx1));

        //var a = document.createElement('a');
        //a.href = url;
        //a.download = ExcelFileName + '.xls';
        //// alert(a.download);
        //a.click();
    };

  

});