app.controller('ApplicationFeesReportCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Application Fees Report";
    $scope.AFP = {};

    $scope.AcademicYearGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.AcademicList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FacultyGet',
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
                        $scope.FacultyList = {};

                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.AFP,
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
                        $scope.InstList = {};

                    }
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.getProgrammePartListByProgInstId = function () {
        //$scope.FEECONFIG.ProgrammeInstanceId = $scope.FEECONFIG.ProgrammeInstanceId;


        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/MstProgrammePartGetByProgrammeIdAndProgInstId',
            data: $scope.AFP,
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
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getBranchListByProgInstId = function () {
        $scope.BranchList = {};

        $scope.AFP.Id = $scope.AFP.ProgrammeInstanceId;
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.AFP,
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
                        $scope.BranchList = {};

                    }
                }
                else {
                    $scope.BranchList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getProgPartTermListByProgInstPartId = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartTermGetByProgInstId',
            data: $scope.AFP,
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
                        $scope.ProgPartTermList = {};

                    }
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getFTList = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.ApplicationFeesPaid = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/ApplicationFeesPaid',
            data: $scope.AFP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FeesPaidList = response.obj;


                    if ($.fn.dataTable.isDataTable('#example')) {
                        $('#example').dataTable().fnClearTable();
                        $('#example').DataTable().destroy();

                    }
                    $(document).ready(function () {
                        $('#example').DataTable({
                            "ordering": false,
                            dom: 'Bfrtip',
                            buttons: [
                                //{
                                //    extend: 'csv', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.FTR.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" }

                                //}
                                //,
                                { extend: 'excel', title: $scope.FeesPaidList[0].InstancePartTermName + ' ' + 'Application Fee Paid Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'pdf', orientation: 'landscape', title: $scope.FeesPaidList[0].InstancePartTermName + '\n' + 'Application Fee Paid Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                /* , { extend: 'print', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.PublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }*/

                            ]
                        });
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    

    $scope.ApplicationFeesUnpaid = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/ApplicationFeesUnpaid',
            data: $scope.AFP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FeesUnPaidList = response.obj;


                    if ($.fn.dataTable.isDataTable('#example1')) {
                        $('#example1').dataTable().fnClearTable();
                        $('#example1').DataTable().destroy();

                    }
                    $(document).ready(function () {
                        $('#example1').DataTable({
                            "ordering": false,
                            dom: 'Bfrtip',
                            buttons: [
                                //{
                                //    extend: 'csv', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.FTR.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" }

                                //}
                                //,
                                { extend: 'excel', title: $scope.FeesUnPaidList[0].InstancePartTermName + ' ' + 'Application Fee Unpaid Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'pdf', orientation: 'landscape', title: $scope.FeesUnPaidList[0].InstancePartTermName + '\n' + 'Application Fee Unpaid Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                /* , { extend: 'print', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.PublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }*/

                            ]
                        });
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


});