
app.controller('PostApprovalListCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Post Approval List";

    $scope.checklistFaculty = false;
    $scope.checklistAcedemic = false;
    $scope.checklistFeePaid == false;
    $scope.tableData; //Using this array it will reduce server hit because we are storing data at the intialization of the page.
    $scope.PostProgInst = {};
    $scope.PostApprovedStudentsparam = {};
    
    //$scope.PostApprovedStudentsparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.getApprovedList
    //});

    //Function for Get Faculty by Id
    $scope.getFacultyById = function (page = 'none') {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            //data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               //$scope.Faculty = response.obj[0];
                $scope.Institute = response.obj[0];
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                $scope.getPostEligibleApprovalList();
                if (page == 'none') {
                    $scope.getPostApprovedStudentsList();
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

/*For Post Approval Eligible List Start */

    //Function for Getting list using Checkbox value
    $scope.getListByCheckBox = function () {
       
        $scope.ApprovalList = [];
        if ($scope.checklistFaculty == true && $scope.checklistAcedemic == false) {
            for (let i = 0; i < $scope.tableData.length; i++) {
                if ($scope.tableData[i].EligibilityStatus == 'Eligible') {
                    $scope.ApprovalList.push($scope.tableData[i]);
                }
            }
        } else if ($scope.checklistAcedemic == true || ($scope.checklistFaculty == true && $scope.checklistAcedemic == true)) {
            for (let i = 0; i < $scope.tableData.length; i++) {
                if ($scope.tableData[i].EligibilityStatus == 'Eligible' && $scope.tableData[i].EligibilityByAcademics == 'Eligible') {
                    $scope.ApprovalList.push($scope.tableData[i]);
                }
            }
        } else if ($scope.checklistFeePaid == true) {
            for (let i = 0; i < $scope.tableData.length; i++) {
                if ($scope.tableData[i].IsAdmissionFeePaid == true) {
                    $scope.ApprovalList.push($scope.tableData[i]);
                }
            }
        }
        
        $scope.PostApprovedStudentsparam = new NgTableParams({}, { dataset: $scope.ApprovalList });//use for filter data in table filter box
    };

    //Function for Get Eligible Applicant List
    $scope.getPostEligibleApprovalList = function () {
     
        //var FacId = { FacultyId: $scope.Faculty.Id };
        var InstituteId = {
            InstituteId: $scope.Institute.InstituteId
            //ProgrammeInstancePartTermId: 14
            //ProgrammeInstancePartTermId: $localStorage.Stats.ProgramInstancePartTermId
        };


        $http({
            method: 'POST',
            url: 'api/PostApprovalList/PostEligibleApprovalListGet',
            data: InstituteId,
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
                        $scope.tableData = response.obj;
                        //$scope.ApprovalList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for Export data to Excel for Eligible Applicant List
    $scope.ExportDatatoExcel = function () {

        var LongDate = new Date($.now());
        
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");

        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");

        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();

        var dateAndTime = ShortDate + time;
        var ExcelFileName = "APPROVAL_LIST_" + ShortDate + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>ELIGIBLE APPROVAL LIST ON {dateAndTime}</h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = document.getElementById("EligibleList");
        // var tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'APPROVAL_LIST', dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };
        //var ctx1 = { dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };

        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));
        //var url = uri + base64(format(template, ctx1));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        // alert(a.download);
        a.click();
    };

/*For Post Approval Eligible List End */



/*For Post Approved List Start */

    //Function for Get Approved Students List
    $scope.getPostApprovedStudentsList = function () {

        //var FacId = { FacultyId: $scope.Faculty.Id };
        var InstituteId = {
            InstituteId: $scope.Institute.InstituteId,
            //ProgrammeInstancePartTermId: 5
            ProgrammeInstancePartTermId: $localStorage.Stats.ProgramInstancePartTermId
        };

        $http({
            method: 'POST',
            url: 'api/PostApprovalList/PostApprovedStudentsListGet',
            data: InstituteId,
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
                        //$scope.ApprovedList = response.obj;
                        $scope.PostApprovedStudentsparam = new NgTableParams({}, { dataset: response.obj });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for Export data to Excel for Approved Students
    $scope.ExcelForApprovedStudents = function () {

        var LongDate = new Date($.now());

        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");

        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");

        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();

        var dateAndTime = ShortDate + time;
        var ExcelFileName = "APPROVED_STUDENT_LIST_" + ShortDate + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>APPROVED STUDENT LIST ON {dateAndTime}</h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = document.getElementById("ApprovedList");
        // var tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'APPROVED_STUDENT_LIST', dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };
        //var ctx1 = { dateAndTime: dateAndTime || dateAndTime, table: table.innerHTML };

        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));
        //var url = uri + base64(format(template, ctx1));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        // alert(a.download);
        a.click();
    };

/*For Post Approved List End */

});