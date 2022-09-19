app.controller('FeeTypeReportCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Fee Type Report";
    $scope.FTR = {};

    var newCategorylist = new Array();

    

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

    $scope.getFTList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeGet',
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
                    $scope.FTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.PublishedSubmit = function () {
        
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeTypeReportPublishGet',
            data: $scope.FTR,
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
                    $scope.PublishList = response.obj;

                    console.log($scope.PublishList);
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
                                ,
                                { extend: 'excel', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.PublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'pdf', orientation: 'landscape', title: $scope.PublishList[0].FeeTypeName + ' ' + 'Publish Report' + '\n' + 'Academic Year' + '-' + $scope.PublishList[0].AcademicYearCode, exportOptions: { columns: "thead th:not(.noExport)" } }
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
    
    $scope.UnPublishedSubmit = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeTypeReportUnPublishGet',
            data: $scope.FTR,
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
                    $scope.UnPublishList = response.obj;
                
                    
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
                                //    extend: 'csv', title: 'Fee UnPublish Report' + $scope.UnPublishList[0].AcademicYearCode + ':: ' + $scope.FTR.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" }

                                //}
                                ,
                                { extend: 'excel', title: 'Fee UnPublish Report' + $scope.UnPublishList[0].AcademicYearCode + ':: ' + $scope.UnPublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'pdf', orientation: 'landscape', title: $scope.UnPublishList[0].FeeTypeName + ' ' + 'UnPublish Report' + '\n' + 'Academic Year' + '-' + $scope.UnPublishList[0].AcademicYearCode, exportOptions: { columns: "thead th:not(.noExport)" } }
                                //, { extend: 'print', title: 'Fee UnPublish Report' + $scope.UnPublishList[0].AcademicYearCode + ':: ' + $scope.UnPublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }

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